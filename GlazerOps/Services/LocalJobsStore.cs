using System.Text.Json;
using GlazerOps.Models.Domain;
using Microsoft.JSInterop;

namespace GlazerOps.Services;

public sealed class LocalJobsStore
{
    private const string StorageKey = "jobs.cache";
    private readonly IJSRuntime _jsRuntime;
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public LocalJobsStore(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<IReadOnlyList<Job>> LoadAsync(int maxCount)
    {
        var json = await _jsRuntime.InvokeAsync<string>("storageInterop.getItem", StorageKey);

        if (string.IsNullOrWhiteSpace(json))
        {
            return Array.Empty<Job>();
        }

        try
        {
            var jobs = JsonSerializer.Deserialize<List<Job>>(json, JsonOptions) ?? new List<Job>();
            return jobs.Take(maxCount).ToList();
        }
        catch
        {
            await _jsRuntime.InvokeVoidAsync("storageInterop.removeItem", StorageKey);
            return Array.Empty<Job>();
        }
    }

    public async Task SaveAsync(IEnumerable<Job> jobs, int maxCount)
    {
        var payload = jobs.Take(maxCount).ToList();
        var json = JsonSerializer.Serialize(payload, JsonOptions);
        await _jsRuntime.InvokeVoidAsync("storageInterop.setItem", StorageKey, json);
    }
}
