using Microsoft.JSInterop;

namespace ManuHub.Blazor.Wasm.BrowserStorage;

internal class LocalStore(IJSRuntime jsRuntime) : BaseStore(jsRuntime), ILocalStore
{
    public async ValueTask SetAsync<T>(string key, T value)
    {
        if (string.IsNullOrEmpty(key))
            throw new ArgumentException($"'{nameof(key)}' cannot be null or empty.", nameof(key));

        var module = await ModuleTask.Value;
        await module.InvokeVoidAsync("setLocalItem", key, value);
    }

    public async ValueTask<T?> GetAsync<T>(string key)
    {
        if (string.IsNullOrEmpty(key))
            throw new ArgumentException($"'{nameof(key)}' cannot be null or empty.", nameof(key));

        var module = await ModuleTask.Value;
        return await module.InvokeAsync<T>("getLocalItem", key);
    }

    public async ValueTask<Dictionary<string, object>?> GetAllAsync()
    {
        var module = await ModuleTask.Value; 
        return await module.InvokeAsync<Dictionary<string, object>>("getAllLocalItems");
    }

    public async ValueTask RemoveItemAsync(string key)
    {
        if (string.IsNullOrEmpty(key))
            throw new ArgumentException($"'{nameof(key)}' cannot be null or empty.", nameof(key));

        var module = await ModuleTask.Value;
        await module.InvokeVoidAsync("removeLocalItem", key);
    }

    public async ValueTask ClearAsync()
    {
        var module = await ModuleTask.Value;
        await module.InvokeVoidAsync("clearLocalStorage");
    }
}
