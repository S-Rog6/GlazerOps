using Microsoft.JSInterop;
using Newtonsoft.Json;
using Supabase.Gotrue;
using Supabase.Gotrue.Interfaces;

public sealed class SupabaseLocalStorageSessionHandler : IGotrueSessionPersistence<Session>
{
    private const string Key = "sb.session";
    private readonly IJSInProcessRuntime _js;

    public SupabaseLocalStorageSessionHandler(IJSRuntime js)
        => _js = (IJSInProcessRuntime)js;

    public void SaveSession(Session session)
    {
        var json = JsonConvert.SerializeObject(session);
        _js.InvokeVoid("storageInterop.setItem", Key, json);
    }

    public void DestroySession()
        => _js.InvokeVoid("storageInterop.removeItem", Key);

    public Session? LoadSession()
    {
        var json = _js.Invoke<string>("storageInterop.getItem", Key);
        return string.IsNullOrWhiteSpace(json) ? null : JsonConvert.DeserializeObject<Session>(json);
    }
}