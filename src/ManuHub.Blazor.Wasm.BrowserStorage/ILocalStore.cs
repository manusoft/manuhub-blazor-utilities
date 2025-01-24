
namespace ManuHub.Blazor.Wasm.BrowserStorage;

public interface ILocalStore
{
    ValueTask ClearAsync();
    ValueTask<T?> GetAsync<T>(string key);
    ValueTask<Dictionary<string, object>?> GetAllAsync();
    ValueTask DeleteAsync(string key);
    ValueTask SetAsync<T>(string key, T value);
}