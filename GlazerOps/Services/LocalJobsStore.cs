using System.Text.Json;
using GlazerOps.Models.Domain;
using Microsoft.JSInterop;

namespace GlazerOps.Services;

public sealed class LocalJobsStore
{
    private const string StorageKey = "jobs.cache";
    private readonly IJSRuntime _jsRuntime;
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    private List<Job> _jobs = new();

    public LocalJobsStore(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public IReadOnlyList<Job> Jobs => _jobs;

    public event Action? Changed;

    public async Task<IReadOnlyList<Job>> LoadAsync(int maxCount)
    {
        var json = await _jsRuntime.InvokeAsync<string>("storageInterop.getItem", StorageKey);

        if (string.IsNullOrWhiteSpace(json))
        {
            _jobs = new List<Job>();
            NotifyStateChanged();
            return _jobs;
        }

        try
        {
            _jobs = (JsonSerializer.Deserialize<List<Job>>(json, JsonOptions) ?? new List<Job>())
                .Take(maxCount)
                .ToList();

            NotifyStateChanged();
            return _jobs;
        }
        catch
        {
            await _jsRuntime.InvokeVoidAsync("storageInterop.removeItem", StorageKey);
            _jobs = new List<Job>();
            NotifyStateChanged();
            return _jobs;
        }
    }

    public async Task SaveAsync(IEnumerable<Job> jobs, int maxCount)
    {
        var payload = jobs.Take(maxCount).ToList();
        var json = JsonSerializer.Serialize(payload, JsonOptions);
        await _jsRuntime.InvokeVoidAsync("storageInterop.setItem", StorageKey, json);
        _jobs = payload;
        NotifyStateChanged();
    }

    private void NotifyStateChanged()
    {
        Changed?.Invoke();
    }
}
