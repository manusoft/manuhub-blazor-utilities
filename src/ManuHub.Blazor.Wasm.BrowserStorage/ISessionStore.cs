
namespace ManuHub.Blazor.Wasm.BrowserStorage;

public interface ISessionStore
{
    ValueTask ClearAsync();
    ValueTask<Dictionary<string, object>?> GetAllAsync();
    ValueTask<T?> GetAsync<T>(string key);
    ValueTask RemoveItemAsync(string key);
    ValueTask SetAsync<T>(string key, T value);
}