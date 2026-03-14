using GlazerOps.Models.Data;
using Supabase;

namespace GlazerOps.Services;

public sealed class JobsCacheSyncService : IAsyncDisposable
{
    private const int MaxJobs = 20;
    private static readonly TimeSpan PollInterval = TimeSpan.FromMinutes(10);

    private readonly Client _supabase;
    private readonly LocalJobsStore _localJobsStore;
    private readonly SemaphoreSlim _refreshLock = new(1, 1);
    private readonly SemaphoreSlim _startLock = new(1, 1);

    private CancellationTokenSource? _cts;
    private Task? _runTask;

    public JobsCacheSyncService(Client supabase, LocalJobsStore localJobsStore)
    {
        _supabase = supabase;
        _localJobsStore = localJobsStore;
    }

    public async Task StartAsync()
    {
        if (_runTask is not null)
        {
            return;
        }

        await _startLock.WaitAsync();

        try
        {
            if (_runTask is not null)
            {
                return;
            }

            _cts = new CancellationTokenSource();

            await RefreshJobsCacheAsync(_cts.Token);
            _runTask = RunLoopAsync(_cts.Token);
        }
        finally
        {
            _startLock.Release();
        }
    }

    public Task RefreshNowAsync(CancellationToken cancellationToken = default)
    {
        return RefreshJobsCacheAsync(cancellationToken);
    }

    private async Task RunLoopAsync(CancellationToken cancellationToken)
    {
        using var timer = new PeriodicTimer(PollInterval);

        try
        {
            while (await timer.WaitForNextTickAsync(cancellationToken))
            {
                await RefreshJobsCacheAsync(cancellationToken);
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
        }
    }

    private async Task RefreshJobsCacheAsync(CancellationToken cancellationToken)
    {
        if (!await _refreshLock.WaitAsync(0, cancellationToken))
        {
            return;
        }

        try
        {
            var jobCardsResponse = await _supabase
                .From<JobCardViewData>()
                .Select("*")
                .Get();

            var jobCards = jobCardsResponse.Models ?? new List<JobCardViewData>();

            var mappedJobs = JobMapper.MapCardViewToDomain(jobCards)
                .OrderByDescending(job => job.Id)
                .ToList();

            await _localJobsStore.SaveAsync(mappedJobs, MaxJobs);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Jobs background sync failed: {ex}");
        }
        finally
        {
            _refreshLock.Release();
        }
    }

    public async ValueTask DisposeAsync()
    {
        _cts?.Cancel();

        if (_runTask is not null)
        {
            try
            {
                await _runTask;
            }
            catch (OperationCanceledException)
            {
            }
        }

        _cts?.Dispose();
        _refreshLock.Dispose();
        _startLock.Dispose();
    }
}
