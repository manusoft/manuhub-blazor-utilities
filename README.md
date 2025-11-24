![Static Badge](https://img.shields.io/badge/BrowserStorage.Wasm-red) ![NuGet Version](https://img.shields.io/nuget/v/BrowserStorage.Wasm) ![.NET](https://img.shields.io/badge/.NET-8%20%7C%209%20%7C%2010-blueviolet)

# BrowserStorage.Wasm
![icon](https://github.com/user-attachments/assets/2182b6a4-8d06-4021-8837-af7a2d7e4556)

` BrowserStorage.Wasm` is a Razor library for managing browser storage in WebAssembly (WASM) applications. This package includes interfaces for managing data across cookies, session, and local storage with asynchronous methods. It is recommended to access these storage options only via the provided interfaces.

## Features

- **ICookieStore**: Interface for managing data in browser cookies with async operations.
- **ISessionStore**: Interface for managing session data that is cleared when the session ends.
- **ILocalStore**: Interface for managing persistent data in local storage.


## Usage

To use the package, make sure to access storage via the provided interfaces (`ICookieStore`, `ISessionStore`, `ILocalStore`).

### ICookieStore Example

```csharp
// Inject ICookieStore in your Razor component or service
public class ExampleService
{
    private readonly ICookieStore _cookieStore;

    public ExampleService(ICookieStore cookieStore)
    {
        _cookieStore = cookieStore;
    }

    public async Task ManageCookies()
    {
        await _cookieStore.SetAsync("user", "JohnDoe", expires: 24); // Set cookie with expiration
        var user = await _cookieStore.GetAsync<string>("user"); // Get cookie
        await _cookieStore.DeleteAsync("user"); // Remove cookie
    }
}
```

### ISessionStore Example

```csharp
// Inject ISessionStore in your Razor component or service
public class ExampleService
{
    private readonly ISessionStore _sessionStore;

    public ExampleService(ISessionStore sessionStore)
    {
        _sessionStore = sessionStore;
    }

    public async Task ManageSession()
    {
        await _sessionStore.SetAsync("sessionData", "value"); // Store data for session
        var sessionData = await _sessionStore.GetAsync<string>("sessionData"); // Get session data
        await _sessionStore.RemoveItemAsync("sessionData"); // Remove session data
    }
}
```

### ILocalStore Example

```csharp
// Inject ILocalStore in your Razor component or service
public class ExampleService
{
    private readonly ILocalStore _localStore;

    public ExampleService(ILocalStore localStore)
    {
        _localStore = localStore;
    }

    public async Task ManageLocalStorage()
    {
        await _localStore.SetAsync("persistentData", "value"); // Store data persistently
        var persistentData = await _localStore.GetAsync<string>("persistentData"); // Get persistent data
        await _localStore.RemoveItemAsync("persistentData"); // Remove persistent data
    }
}
```

### Register services in your Blazor app:

```csharp
builder.Services.AddWasmBrowserStorage();
```

## Contributing

We welcome contributions to the project! If you'd like to contribute, please fork the repository and submit a pull request with your changes.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE.txt) file for details.
