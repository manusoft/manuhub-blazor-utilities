
namespace ManuHub.Blazor.Wasm.BrowserStorage;

public interface ICookieStore
{
    ValueTask DeleteAsync(string key);
    ValueTask<T?> GetAsync<T>(string key);
    ValueTask SetAsync<T>(string key, T value, int? expires = null);
}