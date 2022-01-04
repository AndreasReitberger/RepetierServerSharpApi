# RepetierServerSharpApi
An C# based library to to interact with a Repetier Server Pro instance via REST-API.

# Nuget
Get the latest version from nuget.org<br>
[![NuGet](https://img.shields.io/nuget/v/RepetierServerSharpApi.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/RepetierServerSharpApi/)
[![NuGet](https://img.shields.io/nuget/dt/RepetierServerSharpApi.svg)](https://www.nuget.org/packages/RepetierServerSharpApi)

# Usage
You can find some usage examples in the TestProject of the source code.

## Init a new server
Just create a new `RepetierServerPro` object by passing the host, api, port and ssl connection type.
```csharp
RepetierServerPro _server = new RepetierServerPro(_host, _api, _port, _ssl);
await _server.CheckOnlineAsync();
if (_server.IsOnline)
{
    // Sets the first printer active
    if (_server.ActivePrinter == null)
        await _server.SetPrinterActiveAsync(0, true);

    await _server.RefreshAllAsync();
}
```

Since then, you can access all functions from the `RepetierServerPro` object.

## Instance
If you want to use the `RepetierServerPro` from different places, use the `Instance`.
```csharp
RepetierServerPro.Instance = new RepetierServerPro(_host, _api, _port, _ssl);
await RepetierServerPro.Instance.CheckOnlineAsync();
```

Aferwards you can use the OctoPrintServer.Instance property to access all functions 
through your project.
```csharp
ObservableCollection<RepetierModel> models = await _server.GetModelsAsync();
```

# Available methods
Please find the some usage examples for the methods below.

## Get available printers

```csharp
// Get all printers
ObservableCollection<RepetierPrinter> printers = await _server.GetPrintersAsync();
```

## Get models and groups

```csharp
// Load all models from the Server
ObservableCollection<RepetierModel> models = await _server.GetModelsAsync();

// Load all modelgroups from the Server
ObservableCollection<string> models = await _server.GetModelGroupsAsync();
```

## Control heating elements
In order to set a temperature for the Extruder, use following command.

```csharp
// Set Extruder 0 to 30 C°
bool result = await _server.SetExtruderTemperatureAsync(Extruder: 0, Temperature: 30);
```

To read back the current set and read temperature, use following method.

```csharp
RepetierPrinterStateRespone state = await _server.GetStateObjectAsync();
if (state != null && state.Printer != null)
{
    List<RepetierPrinterExtruder> extruders = state.Printer.Extruder;
    if (extruders == null || extruders.Count == 0)
    {
        Assert.Fail("No extrudes available");
        break;
    }
    RepetierPrinterExtruder extruder = extruders[0];
    extruderTemp = extruder.TempRead;
}
```

Same applies to the heated bed and heated chamber.

## Job info and job list
```csharp
// Returns the current job list
ObservableCollection<RepetierJobListItem> jobs = await _server.GetJobListAsync();
```

## External commands
```csharp
// Returns all available commands (like shutdown, restart,...)
ObservableCollection<ExternalCommand> commands = await RepetierServerPro.Instance.GetExternalCommandsAsync();
```

# Dependencies
RCoreSharp: https://github.com/AndreasReitberger/CoreSharp
