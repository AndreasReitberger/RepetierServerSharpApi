using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using AndreasReitberger.API.Repetier;
using AndreasReitberger.API.Repetier.Enum;
using AndreasReitberger.API.Repetier.Models;
using AndreasReitberger.Core.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace RepetierServerSharpApiTest
{
    [TestClass]
    public class RepetierServerSharpApiTest
    {

        private readonly string _host = SecretAppSettingReader.ReadSection<SecretAppSetting>("TestSetup").Ip ?? "";
        private readonly string _user = SecretAppSettingReader.ReadSection<SecretAppSetting>("TestSetup").User ?? "";
        private readonly string _pw = SecretAppSettingReader.ReadSection<SecretAppSetting>("TestSetup").Password ?? "";
        private readonly int _port = 3344;
        private readonly string _api = SecretAppSettingReader.ReadSection<SecretAppSetting>("TestSetup").ApiKey ?? "";
        private readonly bool _ssl = false;

        private readonly bool _skipPrinterActionTests = true;

        [TestMethod]
        public void SerializeJsonTest()
        {
            var dir = @"TestResults\Serialization\";
            Directory.CreateDirectory(dir);
            string serverConfig = Path.Combine(dir, "server.xml");
            if (File.Exists(serverConfig)) File.Delete(serverConfig);
            try
            {

                RepetierClient.Instance = new RepetierClient(_host, _api, _port, _ssl)
                {
                    FreeDiskSpace = 1523165212,
                    TotalDiskSpace = 65621361616161,
                };
                RepetierClient.Instance.SetProxy(true, "https://testproxy.de", 447, "User", "my_awesome_pwd", true);

                var serializedString = System.Text.Json.JsonSerializer.Serialize(RepetierClient.Instance, RepetierClient.DefaultJsonSerializerSettings);
                var serializedObject = System.Text.Json.JsonSerializer.Deserialize<RepetierClient>(serializedString, RepetierClient.DefaultJsonSerializerSettings);
                Assert.IsTrue(serializedObject is RepetierClient server && server != null);

            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [TestMethod]
        public void SerializeNewetonsoftJsonTest()
        {
            var dir = @"TestResults\Serialization\";
            Directory.CreateDirectory(dir);
            string serverConfig = Path.Combine(dir, "server.xml");
            if (File.Exists(serverConfig)) File.Delete(serverConfig);
            try
            {
                RepetierClient.Instance = new RepetierClient(_host, _api, _port, _ssl)
                {
                    FreeDiskSpace = 1523165212,
                    TotalDiskSpace = 65621361616161,
                };
                RepetierClient.Instance.SetProxy(true, "https://testproxy.de", 447, "User", "my_awesome_pwd", true);

                var serializedString = Newtonsoft.Json.JsonConvert.SerializeObject(RepetierClient.Instance, Newtonsoft.Json.Formatting.Indented, RepetierClient.DefaultNewtonsoftJsonSerializerSettings);
                //var serializedObject = Newtonsoft.Json.JsonConvert.DeserializeObject<RepetierClient>(serializedString);
                var serializedObject = RepetierClient.Instance.GetObjectFromJson<RepetierClient>(serializedString, RepetierClient.DefaultNewtonsoftJsonSerializerSettings);
                Assert.IsTrue(serializedObject is RepetierClient server && server != null);

            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [TestMethod]
        public void SerializeAllTypesWithJsonNewtonsoftTest()
        {
            var dir = @"TestResults\Serialization\";
            Directory.CreateDirectory(dir);
            string serverConfig = Path.Combine(dir, "server.xml");
            if (File.Exists(serverConfig)) File.Delete(serverConfig);
            try
            {
                List<Type> types = AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(t => t.GetTypes())
                       .Where(t => t.IsClass && !t.Name.StartsWith("<") && t.Namespace?.StartsWith("AndreasReitberger.API.Repetier") is true)
                       .ToList()
                       ;
                //Regex r = new(@"(?<=\"")[A-Z]*[A-Z][a-zA-Z]*(?=\"")");
                Regex r = new(@"^[A-Z][A-Za-z0-9]*$");
                Regex extract = new(@"(?<=\"").+?(?=\"")");
                foreach (Type t in types)
                {
                    object? obj = null;
                    try
                    {
                        // Not possible for extensions classes, so catch this
                        obj = Activator.CreateInstance(t);
                    }
                    catch (Exception exc)
                    {
                        Debug.WriteLine($"Exception while creating object from type `{t}`: {exc.Message}");
                    }
                    if (obj is null) continue;
                    string serializedString =
                        JsonConvert.SerializeObject(obj, Formatting.Indented, settings: RepetierClient.DefaultNewtonsoftJsonSerializerSettings);
                    if (serializedString == "{}") continue;

                    // Get all property infos
                    List<PropertyInfo> p = t
                        .GetProperties()
                        .Where(prop => prop.GetCustomAttribute<JsonPropertyAttribute>(true) is not null)
                        .ToList()
                        ;

                    // Get the property names from the json text
                    var splitString = serializedString.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    bool skip = false;
                    StringBuilder sb = new();
                    // Cleanup from child nodes, those will be checked individually
                    foreach (var line in splitString)
                    {
                        if (line.Contains(": {") && !line.Contains("{}"))
                        {
                            skip = true;
                            sb.AppendLine(line.Replace(": {", ": null,"));
                        }
                        else if (line.StartsWith("},"))
                        {
                            skip = false;
                        }
                        else if (!skip)
                            sb.AppendLine(line.Trim());
                    }
                    // set to cleanuped string
                    serializedString = sb.ToString();
                    var splitted = serializedString.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    List<string> properties = splitted
                        .Select(row => extract.Match(row ?? "")?.Value ?? string.Empty)
                        .ToList()
                        ;
                    /*
                    serializedString = string.Join(Environment.NewLine, splitString);
                    List<string> properties = serializedString.Split(",", StringSplitOptions.RemoveEmptyEntries)
                        .Select(p => p.Trim())
                        .ToList()
                        ;
                    */
                    foreach (string property in properties)
                    {
                        bool valid = r.IsMatch(property);
                        //string trimmed = extract.Match(property).Value;
                        if (!valid)
                        {
                            PropertyInfo? jsonAttribute = p.Where(prop =>
                                prop.CustomAttributes.Any(attr => attr.ConstructorArguments.Where(arg => arg.Value is string str && str == property).Count() == 1))
                                .ToList()
                                .FirstOrDefault()
                                ;

                            if (jsonAttribute is not null)
                            {
                                CustomAttributeData? ca = jsonAttribute.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(JsonPropertyAttribute));
                                if (ca is not null)
                                {
                                    CustomAttributeTypedArgument cap = ca.ConstructorArguments.FirstOrDefault();
                                    string propertyName = cap.Value?.ToString() ?? string.Empty;
                                    // If the property name is adjusted with the json attribute, it is ok to start with a lower case.
                                    valid = property == propertyName;
                                }
                            }
                        }
                        if (!valid)
                        {

                        }
                        string msg = $"Type: {t} => {property} is {(valid ? "valid" : "invalid")}";
                        Debug.WriteLine(msg);
                        Assert.IsTrue(valid, message: msg);
                    }
                }
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [TestMethod]
        public void SerializeTest()
        {

            var dir = @"TestResults\Serialization\";
            Directory.CreateDirectory(dir);
            string serverConfig = Path.Combine(dir, "server.xml");
            if (File.Exists(serverConfig)) File.Delete(serverConfig);
            try
            {
                var xmlSerializer = new XmlSerializer(typeof(RepetierClient));
                using (var fileStream = new FileStream(serverConfig, FileMode.Create))
                {
                    RepetierClient.Instance = new RepetierClient(_host, _api, _port, _ssl)
                    {
                        ActiveToolheadIndex = 1,
                        FreeDiskSpace = 1523165212,
                        TotalDiskSpace = 65621361616161,
                        IsMultiExtruder = true,
                    };
                    RepetierClient.Instance.SetProxy(true, "https://testproxy.de", 447, "User", "my_awesome_pwd", true);

                    xmlSerializer.Serialize(fileStream, RepetierClient.Instance);
                    Assert.IsTrue(File.Exists(Path.Combine(dir, "server.xml")));
                }

                xmlSerializer = new XmlSerializer(typeof(RepetierClient));
                using (var fileStream = new FileStream(serverConfig, FileMode.Open))
                {
                    var instance = (RepetierClient)xmlSerializer.Deserialize(fileStream);
                }

            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [TestMethod]
        public void ExtendedSerializeTest()
        {
            try
            {
                // Check if all works
                _ = new GcodeCommandInfo() { Sent = true, Command = "G28 M500;", Id = Guid.NewGuid(), Succeeded = true, TimeStamp = DateTime.Now }.ToString();
                _ = new RepetierGcodeScript() { Name = "My Script", Script = "G28 M500" }.ToString();
                _ = new RepetierPrinterConfig()
                {
                    Connection = new()
                    {
                        Ip = new() { Address = "192.168.1.1", Port = 3344 },
                    },
                    Extruders = new()
                    {
                        new() { Acceleration = 5000, Alias = "My #1 Extruder", ExtrudeSpeed = 5000, MaxTemp = 300, Num = 0 },
                        new() { Acceleration = 5000, Alias = "My #2 Extruder", ExtrudeSpeed = 5000, MaxTemp = 300, Num = 1 },
                    },
                    HeatedBeds = new()
                    {
                        new() { Alias = "My Heated bed", MaxTemp = 110, LastTemp = 75, Temperatures = new() { new() { Temp = 75 } } }
                    },
                    HeatedChambers = new()
                    {
                        new() { Alias = "My Heated chamber", MaxTemp = 110, LastTemp = 75, Temperatures = new() { new() { Temp = 75 } } }
                    },
                    Webcams = new()
                    {
                        new() { WebCamUrlDynamic = new("https://some.url.de/"), Position = 0, Orientation = 90 },
                        new() { WebCamUrlDynamic = new("https://some.url.de/"), Position = 1, Orientation = 180 },
                    }

                }.ToString();
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [TestMethod]
        public async Task ServerInitTest()
        {
            try
            {
                RepetierClient _server = new(_host, _api, _port, _ssl);
                await _server.CheckOnlineAsync();
                if (_server.IsOnline)
                {
                    if (_server.ActivePrinter == null)
                        await _server.SetPrinterActiveAsync(0, true);

                    await _server.RefreshAllAsync();
                    Assert.IsTrue(_server.InitialDataFetched);
                }
                else
                    Assert.Fail($"Server {_server.FullWebAddress} is offline.");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [TestMethod]
        public async Task ServerLoginTest()
        {
            try
            {
                RepetierClient _server = new(_host, _port, _ssl);
                await _server.CheckOnlineAsync();
                if (_server.IsOnline)
                {
                    bool succeed = false;
                    // Wait 1 minutes
                    CancellationTokenSource cts = new(new TimeSpan(0, 1, 0));
                    _server.LoginResultReceived += ((sender, args) =>
                    {
                        Assert.IsTrue(args.LoginSucceeded);
                        succeed = true;
                        cts.Cancel();
                    });
                    await _server.SetPrinterActiveAsync();
                    await _server.StartListeningAsync();
                    // Wait till session is esstablished
                    while (_server.Session == null && !cts.IsCancellationRequested)
                    {
                        await Task.Delay(250);
                    }
                    if (_server.ActivePrinter == null)
                        await _server.SetPrinterActiveAsync(0, true);
                    _server.Login(_user, SecureStringHelper.ConvertToSecureString(_pw), _server.SessionId);
                    while (!cts.IsCancellationRequested && !succeed)
                    {
                        await Task.Delay(100);
                    }
                    Assert.IsTrue(succeed);
                    await _server.LogoutAsync();
                }
                else
                    Assert.Fail($"Server {_server.FullWebAddress} is offline.");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [TestMethod]
        public async Task FetchPrintersTest()
        {
            try
            {
                RepetierClient _server = new(_host, _api, _port, _ssl);
                _server.Error += (o, e) =>
                {
                    Assert.Fail(e.ToString());
                };
                await _server.CheckOnlineAsync();
                if (_server.IsOnline)
                {
                    if (_server.ActivePrinter == null)
                        await _server.SetPrinterActiveAsync(0, true);

                    List<IPrinter3d> printers = await _server.GetPrintersAsync();
                    Assert.IsTrue(printers != null && printers.Count > 0);
                }
                else
                    Assert.Fail($"Server {_server.FullWebAddress} is offline.");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [TestMethod]
        public async Task FetchPrintModelGroupsTest()
        {
            try
            {
                RepetierClient _server = new(_host, _api, _port, _ssl);
                _server.Error += (o, e) =>
                {
                    Assert.Fail(e.ToString());
                };
                await _server.CheckOnlineAsync();
                if (_server.IsOnline)
                {
                    if (_server.ActivePrinter == null)
                    {
                        await _server.SetPrinterActiveAsync();
                    }

                    List<IGcodeGroup> modelgroups = await _server.GetModelGroupsAsync();
                    Assert.IsTrue(modelgroups != null && modelgroups.Count > 0);

                    await _server.RefreshModelGroupsAsync();
                    Assert.IsTrue(_server.Groups?.Count > 0);
                }
                else
                    Assert.Fail($"Server {_server.FullWebAddress} is offline.");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [TestMethod]
        public async Task FetchPrintModelsTest()
        {
            try
            {
                RepetierClient _server = new(_host, _api, _port, _ssl);
                _server.Error += (o, e) =>
                {
                    Assert.Fail(e.ToString());
                };
                await _server.CheckOnlineAsync();
                if (_server.IsOnline)
                {
                    if (_server.ActivePrinter == null)
                        await _server.SetPrinterActiveAsync(0, true);

                    IProgress<int> progress = new Progress<int>(prog =>
                        Debug.WriteLine($"Done: {prog:N0}")
                    );
                    Stopwatch sw = Stopwatch.StartNew();
                    List<IGcode> models = await _server.GetModelsAsync("", AndreasReitberger.API.Print3dServer.Core.Enums.GcodeImageType.None, progress);
                    Assert.IsTrue(models?.Count > 0);

                    sw.Stop();
                    Debug.WriteLine($"Time elapsed: {sw.Elapsed} (without images)");

                    sw = Stopwatch.StartNew();
                    models = await _server.GetModelsAsync("", AndreasReitberger.API.Print3dServer.Core.Enums.GcodeImageType.Thumbnail, progress);
                    Assert.IsTrue(models?.Count > 0);

                    sw.Stop();
                    Debug.WriteLine($"Time elapsed: {sw.Elapsed} (with thumbnails)");

                    sw = Stopwatch.StartNew();
                    models = await _server.GetModelsAsync("", AndreasReitberger.API.Print3dServer.Core.Enums.GcodeImageType.Image, progress);
                    Assert.IsTrue(models?.Count > 0);

                    sw.Stop();
                    Debug.WriteLine($"Time elapsed: {sw.Elapsed} (with images)");

                    models = await _server.GetModelsAsync("", AndreasReitberger.API.Print3dServer.Core.Enums.GcodeImageType.None, progress);
                    Assert.IsTrue(models?.Count > 0);

                    List<IGcode> filesCollection = [.. models.Take(25)];
                    Dictionary<long, byte[]>? images = await _server.GetModelImagesAsync(filesCollection, imageType: AndreasReitberger.API.Print3dServer.Core.Enums.GcodeImageType.Image, progress);
                    Assert.IsTrue(images?.Select(kp => kp.Value).Any(image => image.Length > 0));

                    IList<IGcode>? updatedModels = await _server.UpdateModelImagesAsync(filesCollection, AndreasReitberger.API.Print3dServer.Core.Enums.GcodeImageType.Image, progress);
                    Assert.IsTrue(updatedModels?.Select(model => model.Image).Any(image => image?.Length > 0));
                }
                else
                    Assert.Fail($"Server {_server.FullWebAddress} is offline.");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [TestMethod]
        public async Task FetchJobListTest()
        {
            try
            {
                RepetierClient _server = new(_host, _api, _port, _ssl);
                _server.Error += (sender, e) =>
                {
                    Assert.Fail(e.ToString());
                };
                await _server.CheckOnlineAsync();
                if (_server.IsOnline)
                {
                    if (_server.ActivePrinter == null)
                        await _server.SetPrinterActiveAsync(-1, true);

                    ObservableCollection<IPrint3dJob> jobs = await _server.GetJobListAsync();
                    Assert.IsTrue(jobs != null);
                }
                else
                    Assert.Fail($"Server {_server.FullWebAddress} is offline.");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [TestMethod]
        public async Task PrintModelTest()
        {
            try
            {
                if (_skipPrinterActionTests) return;
                RepetierClient _server = new(_host, _api, _port, _ssl);
                await _server.CheckOnlineAsync();
                if (_server.IsOnline)
                {
                    if (_server.ActivePrinter == null)
                        await _server.SetPrinterActiveAsync(-1, true);

                    List<IGcode> models = await _server.GetModelsAsync();
                    if (models?.Count > 0)
                    {
                        bool printed = await _server.CopyModelToPrintQueueAsync(model: models[0], startPrintIfPossible: false);
                        Assert.IsTrue(printed);
                    }
                    else
                    {
                        Assert.Fail($"No models found on server!");
                    }
                }
                else
                    Assert.Fail($"Server {_server.FullWebAddress} is offline.");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [TestMethod]
        public async Task DownloadPrintReport()
        {
            try
            {
                RepetierClient _server = new(_host, _api, _port, _ssl);
                _server.Error += (sender, e) =>
                {
                    Assert.Fail(e.ToString());
                };
                await _server.CheckOnlineAsync();
                if (_server.IsOnline)
                {
                    await _server.SetPrinterActiveAsync();
                    ObservableCollection<RepetierHistorySummaryItem>? history = await _server.GetHistorySummaryItemsAsync("", 2022, true);
                    Assert.IsTrue(history?.Any());

                    ObservableCollection<RepetierHistoryListItem> list = await _server.GetHistoryListAsync("", "", 50, 0, 0, true);
                    Assert.IsTrue(list?.Any());

                    RepetierHistoryListItem? historyItem = list?.FirstOrDefault();
                    Assert.IsNotNull(historyItem);

                    byte[]? report = await RepetierClient.Instance.GetHistoryReportAsync(historyItem.Id);
                    Assert.IsTrue(report?.Length > 0);
                    string downloadTarget = @"report.pdf";
                    await File.WriteAllBytesAsync(downloadTarget, report);
                    Assert.IsTrue(File.Exists(downloadTarget));
                    //Process.Start(downloadTarget);
                }
                else
                    Assert.Fail($"Server {_server.FullWebAddress} is offline.");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [TestMethod]
        public async Task GetGPIOList()
        {
            try
            {
                RepetierClient _server = new(_host, _api, _port, _ssl);
                _server.Error += (o, e) =>
                {
                    Assert.Fail(e.ToString());
                };
                await _server.CheckOnlineAsync();
                if (_server.IsOnline)
                {
                    await _server.SetPrinterActiveAsync(1);
                    ObservableCollection<RepetierGpioListItem> report = await _server.GetGPIOListAsync();
                    Assert.IsTrue(report.Count > 0);
                }
                else
                    Assert.Fail($"Server {_server.FullWebAddress} is offline.");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [TestMethod]
        public async Task GetHistoryList()
        {
            try
            {
                RepetierClient _server = new(_host, _api, _port, _ssl);
                _server.Error += (o, e) =>
                {
                    Assert.Fail(e.ToString());
                };
                await _server.CheckOnlineAsync();
                if (_server.IsOnline)
                {
                    await _server.SetPrinterActiveAsync(1);
                    ObservableCollection<RepetierHistoryListItem>? report = await _server.GetHistoryListAsync(_server?.ActivePrinter?.Slug ?? "");
                    Assert.IsTrue(report.Count > 0);
                }
                else
                    Assert.Fail($"Server {_server.FullWebAddress} is offline.");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [TestMethod]
        public async Task GetWebcalls()
        {
            try
            {
                RepetierClient _server = new(_host, _api, _port, _ssl);
                _server.Error += (o, e) =>
                {
                    Assert.Fail(e.ToString());
                };
                await _server.CheckOnlineAsync();
                if (_server.IsOnline)
                {
                    await _server.SetPrinterActiveAsync(1);
                    ObservableCollection<RepetierWebCallAction> report = await _server.GetWebCallActionsAsync();
                    Assert.IsTrue(report.Count > 0);
                }
                else
                    Assert.Fail($"Server {_server.FullWebAddress} is offline.");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [TestMethod]
        public async Task GetExternalCommands()
        {
            try
            {
                RepetierClient _server = new(_host, _api, _port, _ssl);
                _server.Error += (o, e) =>
                {
                    Assert.Fail(e.ToString());
                };
                await _server.CheckOnlineAsync();
                if (_server.IsOnline)
                {
                    await _server.SetPrinterActiveAsync(1);
                    ObservableCollection<ExternalCommand> commands = await _server.GetExternalCommandsAsync();
                    Assert.IsTrue(commands.Count > 0);
                }
                else
                    Assert.Fail($"Server {_server.FullWebAddress} is offline.");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        /**/
        [TestMethod]
        public async Task OnlineTest()
        {
            //if (_skipOnlineTests) return;
            try
            {
                RepetierClient _server = new(_host, _api, _port, _ssl);
                await _server.SetPrinterActiveAsync(1);
                _server.Error += (o, args) =>
                {
                    Assert.Fail(args.ToString());
                };
                _server.ServerWentOffline += (o, args) =>
                {
                    Assert.Fail(args.ToString());
                };
                await _server.CheckOnlineAsync(3500);//.ConfigureAwait(false);
                // Wait 10 minutes
                CancellationTokenSource cts = new(new TimeSpan(0, 10, 0));
                do
                {
                    await Task.Delay(10000);
                    await _server.CheckOnlineAsync();
                    await _server.RefreshAllAsync();
                    if (_server.IsPrinting)
                    {
                        IPrint3dJobStatus? info = _server.ActiveJob;
                        if (info == null)
                            Assert.Fail("Print info was null");
                    }
                } while (_server.IsOnline && !cts.IsCancellationRequested);
                Assert.IsTrue(cts.IsCancellationRequested);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [TestMethod]
        public async Task WebcamTest()
        {
            try
            {
                RepetierClient _server = new(_host, _api, _port, _ssl);
                await _server.CheckOnlineAsync();
                Assert.IsTrue(_server.IsOnline);

                await _server.SetPrinterActiveAsync();

                _server.Error += (o, args) =>
                {
                    Assert.Fail(args.ToString());
                };

                RepetierWebcamType type = RepetierWebcamType.Dynamic;
                string webcamUriDynamic = await _server.GetWebCamUriAsync(0, type);
                Assert.IsTrue(Uri.TryCreate(webcamUriDynamic, UriKind.RelativeOrAbsolute, out _));

                type = RepetierWebcamType.Static;
                webcamUriDynamic = await _server.GetWebCamUriAsync(0, type);
                Assert.IsTrue(Uri.TryCreate(webcamUriDynamic, UriKind.RelativeOrAbsolute, out _));

                type = RepetierWebcamType.Dynamic;
                var webCams = await _server.GetWebCamConfigsAsync();
                Assert.IsTrue(webCams?.Count > 0);
                foreach (var cam in webCams)
                {
                    webcamUriDynamic = await _server.GetWebCamUriAsync((int)cam.Position, type);
                    Assert.IsTrue(Uri.TryCreate(webcamUriDynamic, UriKind.RelativeOrAbsolute, out _));

                    webcamUriDynamic = _server.GetWebCamUri(cam);
                    Assert.IsTrue(Uri.TryCreate(webcamUriDynamic, UriKind.RelativeOrAbsolute, out _));
                }
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [TestMethod]
        public async Task WebsocketTest()
        {
            try
            {
                Dictionary<DateTime, string> websocketMessages = [];
                Dictionary<string, string> unkownJsonRespones = [];
                RepetierClient _server = new(_host, _api, _port, _ssl);
                await _server.CheckOnlineAsync();
                await _server.SetPrinterActiveAsync();
                DateTime start = DateTime.Now;
                await _server.StartListeningAsync();

                _server.Error += (o, args) =>
                {
                    Assert.Fail(args.ToString());
                };
                _server.ServerWentOffline += (o, args) =>
                {
                    Assert.Fail(args.ToString());
                };

                _server.WebSocketDataReceived += (o, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Message))
                    {
                        websocketMessages.Add(DateTime.Now, args.Message);
                        Console.WriteLine($"WebSocket Data: {args.Message} (Total: {websocketMessages.Count})");
                    }
                };

                _server.WebSocketMessageReceived += (o, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Message))
                    {
                        websocketMessages.Add(DateTime.Now, args.Message);
                        Console.WriteLine($"WebSocket Data: {args.Message} (Total: {websocketMessages.Count})");
                    }
                };
                _server.WebSocketError += (o, args) =>
                {
                    Assert.Fail($"Websocket closed due to an error: {args}");
                };
                _server.IgnoredJsonResultsChanged += (o, args) =>
                {
                    foreach (var keyPair in args.NewIgnoredJsonResults)
                    {
                        if (!unkownJsonRespones.ContainsKey(keyPair.Key))
                            unkownJsonRespones.Add(keyPair.Key, keyPair.Value);
                    }
                };
                // Wait 30 minutes
                CancellationTokenSource cts = new(new TimeSpan(0, 60, 0));
                _server.WebSocketDisconnected += (o, args) =>
                {
                    var duraton = DateTime.Now - start;
                    var messages = websocketMessages;
                    if (!cts.IsCancellationRequested)
                        Assert.Fail($"Websocket unexpectly closed: {args}");
                };

                do
                {
                    await Task.Delay(10000);
                    await _server.CheckOnlineAsync();
                } while (_server.IsOnline && !cts.IsCancellationRequested);
                await _server.StopListeningAsync();


                Assert.IsTrue(cts.IsCancellationRequested && websocketMessages?.Count > 50);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        /**/
        [TestMethod]
        public async Task SetHeatedbedTest()
        {
            if (_skipPrinterActionTests) return;
            try
            {
                RepetierClient _server = new(_host, _api, _port, _ssl);
                await _server.CheckOnlineAsync();
                if (_server.IsOnline)
                {
                    if (_server.ActivePrinter == null)
                        await _server.SetPrinterActiveAsync(1, true);

                    bool result = await _server.SetBedTemperatureAsync(0, 25);
                    // Set timeout to 5 minutes
                    var cts = new CancellationTokenSource(new TimeSpan(0, 5, 0));

                    if (result)
                    {
                        double? temp = 0;
                        // Wait till temp rises
                        while (temp < 23)
                        {
                            var state = await _server.GetStatesAsync();
                            if (state != null && state?.Count > 0)
                            {
                                var beds = state.FirstOrDefault().Value.HeatedBeds;
                                if (beds == null || beds.Count == 0)
                                {
                                    Assert.Fail("No heated bed found");
                                    break;
                                }
                                var bed = beds[0];
                                temp = bed.TempRead;
                            }
                        }
                        Assert.IsTrue(temp >= 23);
                        // Turn off bed
                        result = await _server.SetBedTemperatureAsync(0, 0);
                        // Set timeout to 5 minutes
                        cts = new CancellationTokenSource(new TimeSpan(0, 5, 0));
                        if (result)
                        {

                            while (temp > 23)
                            {
                                var state = await _server.GetStatesAsync();
                                if (state != null && state?.Count > 0)
                                {
                                    var beds = state.FirstOrDefault().Value.HeatedBeds;
                                    if (beds == null || beds.Count == 0)
                                    {
                                        Assert.Fail("No heated bed found");
                                        break;
                                    }
                                    var bed = beds[0];
                                    temp = bed.TempRead;
                                }
                            }
                            Assert.IsTrue(temp <= 23);
                        }
                        else
                            Assert.Fail("Command failed to be sent.");
                    }
                    else
                        Assert.Fail("Command failed to be sent.");
                }
                else
                    Assert.Fail($"Server {_server.FullWebAddress} is offline.");
            }
            catch (TaskCanceledException texc)
            {
                Assert.Fail(texc.Message);
            }

            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [TestMethod]
        public async Task SetExtruderTest()
        {
            if (_skipPrinterActionTests) return;
            try
            {
                RepetierClient _server = new(_host, _api, _port, _ssl);
                await _server.CheckOnlineAsync();
                if (_server.IsOnline)
                {
                    if (_server.ActivePrinter == null)
                        await _server.SetPrinterActiveAsync(1, true);

                    bool result = await _server.SetExtruderTemperatureAsync(extruder: 0, temperature: 30);
                    // Set timeout to 3 minutes
                    var cts = new CancellationTokenSource(new TimeSpan(0, 3, 0));

                    if (result)
                    {
                        double? extruderTemp = 0;
                        // Wait till temp rises
                        while (extruderTemp < 28)
                        {
                            var state = await _server.GetStatesAsync();
                            if (state != null && state?.Count > 0)
                            {
                                List<RepetierPrinterToolhead> extruders = state.FirstOrDefault().Value.Extruder;
                                if (extruders == null || extruders.Count == 0)
                                {
                                    Assert.Fail("No extrudes available");
                                    break;
                                }
                                RepetierPrinterToolhead extruder = extruders[0];
                                extruderTemp = extruder.TempRead;
                            }
                        }
                        Assert.IsTrue(extruderTemp >= 28);
                        // Turn off extruder
                        result = await _server.SetExtruderTemperatureAsync(0, 0);
                        // Set timeout to 3 minutes
                        cts = new CancellationTokenSource(new TimeSpan(0, 3, 0));
                        if (result)
                        {

                            while (extruderTemp > 28)
                            {
                                var state = await _server.GetStatesAsync();
                                if (state != null && state?.Count > 0)
                                {
                                    var extruders = state.FirstOrDefault().Value.Extruder;
                                    if (extruders == null || extruders.Count == 0)
                                    {
                                        Assert.Fail("No extrudes available");
                                        break;
                                    }
                                    var extruder = extruders[0];
                                    extruderTemp = extruder.TempRead;
                                }
                            }
                            Assert.IsTrue(extruderTemp <= 28);
                        }
                        else
                            Assert.Fail("Command failed to be sent.");
                    }
                    else
                        Assert.Fail("Command failed to be sent.");
                }
                else
                    Assert.Fail($"Server {_server.FullWebAddress} is offline.");
            }
            catch (TaskCanceledException texc)
            {
                Assert.Fail(texc.Message);
            }

            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [TestMethod]
        public async Task ConnectionBuilderTest()
        {
            using RepetierClient client = new RepetierClient.RepetierConnectionBuilder()
                .WithServerAddress(_host, _port, false)
                .WithApiKey(_api)
                .Build();
            await client.CheckOnlineAsync();
            Assert.IsTrue(client?.IsOnline ?? false);
        }

        [TestMethod]
        public async Task ServerQueryTests()
        {
            try
            {
                RepetierClient _server = new(_host, _api, _port, _ssl);
                _server.Error += (o, e) =>
                {
                    Assert.Fail(e.ToString());
                };
                await _server.CheckOnlineAsync();
                if (_server.IsOnline)
                {
                    if (_server.ActivePrinter == null)
                        await _server.SetPrinterActiveAsync(0, true);


                    List<IPrinter3d> printers = await _server.GetPrintersAsync();
                    Assert.IsNotNull(printers);
                    string json = JsonConvert.SerializeObject(printers, Formatting.Indented);

                    RepetierAvailableUpdateInfo? update = await _server.GetAvailableServerUpdateAsync();
                    Assert.IsNotNull(update);
                    json = JsonConvert.SerializeObject(update, Formatting.Indented);

                    RepetierCurrentPrintInfo? printInfo = await _server.GetCurrentPrintInfoAsync();
                    Assert.IsNotNull(printInfo);
                    json = JsonConvert.SerializeObject(printInfo, Formatting.Indented);

                    var printInfos = await _server.GetCurrentPrintInfosAsync();
                    Assert.IsNotNull(printInfos);
                    json = JsonConvert.SerializeObject(printInfos, Formatting.Indented);

                    var cmds = await _server.GetExternalCommandsAsync();
                    Assert.IsNotNull(cmds);
                    json = JsonConvert.SerializeObject(cmds, Formatting.Indented);

                    var gpios = await _server.GetGPIOListAsync();
                    Assert.IsNotNull(gpios);
                    json = JsonConvert.SerializeObject(gpios, Formatting.Indented);

                    var history = await _server.GetHistoryListAsync(_server.ActivePrinter?.Slug);
                    Assert.IsNotNull(history);
                    json = JsonConvert.SerializeObject(history, Formatting.Indented);

                    // Only works if enabled in Settings
                    /*
                    var hid = history?.FirstOrDefault()?.Id;
                    var historyReport = await _server.GetHistoryReportAsync(history?.FirstOrDefault()?.Id ?? 0);
                    Assert.IsNotNull(historyReport);
                    json = JsonConvert.SerializeObject(historyReport, Formatting.Indented);
                    */

                    var historySummary = await _server.GetHistorySummaryItemsAsync(_server.ActivePrinter?.Slug, 2023, true);
                    Assert.IsNotNull(historySummary);
                    json = JsonConvert.SerializeObject(historySummary, Formatting.Indented);

                    var jobList = await _server.GetJobListAsync();
                    Assert.IsNotNull(jobList);
                    json = JsonConvert.SerializeObject(jobList, Formatting.Indented);

                    var license = await _server.GetLicenseDataAsync();
                    Assert.IsNotNull(license);
                    json = JsonConvert.SerializeObject(license, Formatting.Indented);

                    var messages = await _server.GetMessagesAsync();
                    Assert.IsNotNull(messages);
                    json = JsonConvert.SerializeObject(messages, Formatting.Indented);

                    var groups = await _server.GetModelGroupsAsync();
                    Assert.IsNotNull(groups);
                    json = JsonConvert.SerializeObject(groups, Formatting.Indented);

                    List<IGcode> files = await _server.GetModelsAsync();
                    Assert.IsNotNull(files);
                    json = JsonConvert.SerializeObject(files, Formatting.Indented);

                    var config = await _server.GetPrinterConfigAsync();
                    Assert.IsNotNull(config);
                    json = JsonConvert.SerializeObject(config, Formatting.Indented);

                    var servers = await _server.GetProjectsListServerAsync();
                    Assert.IsNotNull(servers);
                    json = JsonConvert.SerializeObject(servers, Formatting.Indented);

                    var projects = await _server.GetProjectItemsAsync(servers?.Server?.FirstOrDefault()?.Uuid ?? Guid.Empty);
                    Assert.IsNotNull(projects);
                    json = JsonConvert.SerializeObject(projects, Formatting.Indented);

                    var folders = await _server.GetProjectsGetFolderAsync(servers?.Server?.FirstOrDefault()?.Uuid ?? Guid.Empty);
                    Assert.IsNotNull(folders);
                    json = JsonConvert.SerializeObject(folders, Formatting.Indented);

                    var state = await _server.GetStatesAsync();
                    Assert.IsNotNull(state);
                    json = JsonConvert.SerializeObject(state, Formatting.Indented);

                    //await _server.RefreshAllAsync();
                    //Assert.IsTrue(_server.InitialDataFetched);
                }
                else
                    Assert.Fail($"Server {_server.FullWebAddress} is offline.");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [TestMethod]
        public async Task DownloadGcodeTestAsync()
        {
            try
            {
                RepetierClient _server = new(_host, _api, _port, _ssl);
                _server.Error += (o, e) =>
                {
                    Assert.Fail(e.ToString());
                };
                await _server.CheckOnlineAsync();
                if (_server.IsOnline)
                {
                    if (_server.ActivePrinter == null)
                        await _server.SetPrinterActiveAsync(0, true);


                    List<IGcode> files = await _server.GetFilesAsync();
                    Assert.IsNotNull(files);

                    byte[]? file = await _server.DownloadGcodeAsync(files.FirstOrDefault().Identifier.ToString());
                    Assert.IsTrue(file?.Length > 0);

                    byte[]? file2 = await _server.DownloadGcodeAsync(files.FirstOrDefault(), Encoding.Default);
                    Assert.IsTrue(file2?.Length > 0);

                    Assert.IsTrue(file.Length == file2.Length);
                }
                else
                    Assert.Fail($"Server {_server.FullWebAddress} is offline.");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }
    }
}
