# RepetierServerSharpApi
An C# based library to to interact with a Repetier Server Pro instance via REST-API.

# Support me
If you want to support me, you can order over following affilate links (I'll get a small share from your purchase from the corresponding store).

- Prusa: http://www.prusa3d.com/#a_aid=AndreasReitberger *
- Jake3D: https://tidd.ly/3x9JOBp * 
- Amazon: https://amzn.to/2Z8PrDu *

(*) Affiliate link
Thank you very much for supporting me!

# Important!
With the upcoming version, starting from `1.2.7`, `RepetierServerPro` become `RepetierClient`. also the namespaces will changed and generalized with our other print server api nugets.

| Old                             | New                              |
| ------------------------------- |:--------------------------------:|
| `AndreasReitberger`             | `AndreasReitberger.API.Repetier` |
| `RepetierServerPro`             | `RepetierClient`                 |

# Nuget
Get the latest version from nuget.org<br>
[![NuGet](https://img.shields.io/nuget/v/RepetierServerSharpApi.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/RepetierServerSharpApi/)
[![NuGet](https://img.shields.io/nuget/dt/RepetierServerSharpApi.svg)](https://www.nuget.org/packages/RepetierServerSharpApi)

# Usage
You can find some usage examples in the TestProject of the source code.

# Platform specific setup

## Android

On `Android` you need to allow local connections in the `AndroidManifest.xml`.
For this, create a new xml file and link to it in your manifest at `android:networkSecurityConfig`

Content of the `network_security_config.xml` file
```
<?xml version="1.0" encoding="utf-8" ?>
<network-security-config>
	<base-config cleartextTrafficPermitted="true" />
</network-security-config>

```

The manifest
```xml
<?xml version="1.0" encoding="utf-8"?>
<manifest
	xmlns:android="http://schemas.android.com/apk/res/android"
	android:versionName="1.0.0"
	android:versionCode="1"
	package="com.company.app"
	>
	<application
		android:label="App Name"
		android:allowBackup="true"
		android:icon="@mipmap/appicon" 
		android:roundIcon="@mipmap/appicon_round"
		android:supportsRtl="true"
		android:networkSecurityConfig="@xml/network_security_config"
		>
	</application>
	<uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE" />
	<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />
	<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
	<uses-permission android:name="android.permission.ACCESS_WIFI_STATE" />
	<uses-permission android:name="android.permission.FOREGROUND_SERVICE" />
	<uses-permission android:name="android.permission.WAKE_LOCK" />
	<uses-permission android:name="android.permission.INTERNET" />
</manifest>
```

## Init a new server
Just create a new `RepetierClient` object by passing the host, api, port and ssl connection type.
```csharp
RepetierClient _server = new RepetierClient(_host, _api, _port, _ssl);
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
If you want to use the `RepetierClient` from different places, use the `Instance`.
```csharp
RepetierClient.Instance = new RepetierClient(_host, _api, _port, _ssl);
await RepetierClient.Instance.CheckOnlineAsync();
```

Aferwards you can use the `RepetierClient.Instance` property to access all functions 
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
if (state is not null && state.Printer is not null)
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
