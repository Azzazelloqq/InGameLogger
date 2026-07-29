# In-Game Logger for Unity

> A minimal logging abstraction that keeps diagnostics in the Editor and Development builds.

`InGameLogger` provides a small interface around Unity's `Debug` API. It lets
application code depend on a logger without coupling every class directly to
`UnityEngine.Debug`.

## Features

- `Log`, `LogWarning`, `LogError` and `LogException` methods.
- Supports both string messages and exceptions.
- Uses `UnityEngine.Debug` in the Editor and Development builds.
- Produces no log output in a non-development player build.
- No update loop or global singleton.

## Installation

### Git submodule

```bash
git submodule add https://github.com/Azzazelloqq/InGameLogger.git Assets/InGameLogger
```

### Unity Package Manager

Add this dependency to `Packages/manifest.json`:

```json
{
  "dependencies": {
    "com.azzazello.logger": "https://github.com/Azzazelloqq/InGameLogger.git"
  }
}
```

The package supports Unity `2020.3` and newer.

## Usage

Depend on the interface in application code:

```csharp
using InGameLogger;

public sealed class SaveService
{
    private readonly IInGameLogger _logger;

    public SaveService(IInGameLogger logger)
    {
        _logger = logger;
    }

    public void Save()
    {
        _logger.Log("Save completed.");
    }
}
```

Compose the implementation at the application boundary:

```csharp
using InGameLogger;

using var logger = new UnityInGameLogger();
var saveService = new SaveService(logger);
```

## API

```csharp
public interface IInGameLogger : IDisposable
{
    void Log(string message);
    void LogWarning(string message);
    void LogError(string message);
    void LogError(Exception exception);
    void LogException(Exception exception);
}
```

## Build behaviour

`UnityInGameLogger` writes through to Unity only when one of these symbols is
defined:

```text
UNITY_EDITOR
DEVELOPMENT_BUILD
```

In release player builds the methods remain safe to call but intentionally do
not produce output.

## Assembly

The module exposes the `Logger` assembly and the `InGameLogger` namespace.
