using Microsoft.JSInterop;

namespace ManuHub.Blazor.Wasm.BrowserStorage;

public abstract class BaseStore : IAsyncDisposable
{
    protected readonly Lazy<Task<IJSObjectReference>> ModuleTask;

    public BaseStore(IJSRuntime jsRuntime)
    {
        ModuleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/ManuHub.Blazor.Wasm.BrowserStorage/browserStorage.js").AsTask());
    }
    public async ValueTask DisposeAsync()
    {
        if (ModuleTask.IsValueCreated)
        {
            var module = await ModuleTask.Value;
            await module.DisposeAsync();
        }
    }
}