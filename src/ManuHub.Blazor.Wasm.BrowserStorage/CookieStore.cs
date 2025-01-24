using Microsoft.JSInterop;
using System.Text.Json;

namespace ManuHub.Blazor.Wasm.BrowserStorage;

internal class CookieStore(IJSRuntime jsRuntime) : BaseStore(jsRuntime), ICookieStore
{
    public async ValueTask SetAsync<T>(string key, T value, int? expires = null)
    {
        if (string.IsNullOrEmpty(key))
            throw new ArgumentException($"'{nameof(key)}' cannot be null or empty.", nameof(key));

        var module = await ModuleTask.Value;
        var jsonData = JsonSerializer.Serialize(value);
        await module.InvokeVoidAsync("setCookie", key, jsonData, expires);
    }

    public async ValueTask<T?> GetAsync<T>(string key)
    {
        if (string.IsNullOrEmpty(key))
            throw new ArgumentException($"'{nameof(key)}' cannot be null or empty.", nameof(key));

        var module = await ModuleTask.Value;

        if (typeof(T) == typeof(string) || typeof(T) == typeof(ValueType))
        {
            return await module.InvokeAsync<T>("getCookie", key);
        }
        else
        {
            var jsonData = await module.InvokeAsync<string>("getCookie", key);
            if (!string.IsNullOrWhiteSpace(jsonData))
            {
                var data = JsonSerializer.Deserialize<T>(jsonData);
                return data;
            }

            return default(T);
        }
    }

    public async ValueTask DeleteAsync(string key)
    {
        if (string.IsNullOrEmpty(key))
            throw new ArgumentException($"'{nameof(key)}' cannot be null or empty.", nameof(key));

        var module = await ModuleTask.Value;
        await module.InvokeVoidAsync("deleteCookie", key);
    }

}
