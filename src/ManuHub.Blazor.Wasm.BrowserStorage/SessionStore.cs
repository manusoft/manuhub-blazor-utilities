using Microsoft.JSInterop;
using System.Text.Json;

namespace ManuHub.Blazor.Wasm.BrowserStorage;

internal class SessionStore(IJSRuntime jsRuntime) : BaseStore(jsRuntime), ISessionStore
{
    public async ValueTask SetAsync<T>(string key, T value)
    {
        if (string.IsNullOrEmpty(key))
            throw new ArgumentException($"'{nameof(key)}' cannot be null or empty.", nameof(key));

        var module = await ModuleTask.Value;

        if (typeof(T) == typeof(string) || typeof(T) == typeof(ValueType))
        {
            await module.InvokeVoidAsync("setSessionItem", key, value);
        }
        else
        {
            var jsonData = JsonSerializer.Serialize(value);
            await module.InvokeVoidAsync("setSessionItem", key, jsonData);
        }
    }

    public async ValueTask<T?> GetAsync<T>(string key)
    {
        if (string.IsNullOrEmpty(key))
            throw new ArgumentException($"'{nameof(key)}' cannot be null or empty.", nameof(key));

        var module = await ModuleTask.Value;

        if (typeof(T) == typeof(string) || typeof(T) == typeof(ValueType))
        {
            return await module.InvokeAsync<T>("getSessionItem", key);
        }
        else
        {
            var jsonData = await module.InvokeAsync<string>("getSessionItem", key);
            if (!string.IsNullOrWhiteSpace(jsonData))
            {
                var data = JsonSerializer.Deserialize<T>(jsonData);
                return data;
            }

            return default(T);
        }
    }

    public async ValueTask<Dictionary<string, object>?> GetAllAsync()
    {
        var module = await ModuleTask.Value;
        return await module.InvokeAsync<Dictionary<string, object>>("getAllSessionItems");
    }

    public async ValueTask RemoveItemAsync(string key)
    {
        if (string.IsNullOrEmpty(key))
            throw new ArgumentException($"'{nameof(key)}' cannot be null or empty.", nameof(key));

        var module = await ModuleTask.Value;
        await module.InvokeVoidAsync("removeSessionItem", key);
    }

    public async ValueTask ClearAsync()
    {
        var module = await ModuleTask.Value;
        await module.InvokeVoidAsync("clearSessionStorage");
    }
}
