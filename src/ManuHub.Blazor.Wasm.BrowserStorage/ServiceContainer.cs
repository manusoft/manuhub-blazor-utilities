using Microsoft.Extensions.DependencyInjection;

namespace ManuHub.Blazor.Wasm.BrowserStorage;

public static class ServiceContainer
{
    public static IServiceCollection AddWasmBrowserStorage(this IServiceCollection services)
    {
        services.AddScoped<ICookieStore, CookieStore>();
        services.AddScoped<ILocalStore, LocalStore>();
        services.AddScoped<ISessionStore, SessionStore>();
        return services;
    }
}
