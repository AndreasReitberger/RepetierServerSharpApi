using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using AndreasReitberger.API.Repetier;
using AndreasReitberger.API.Repetier.Enum;
using AndreasReitberger.API.Repetier.Models;
using AndreasReitberger.Shared.Core.Utilities;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace RepetierServerSharpApiTest
{
    public class RepetierServerSharpApiTest
    {

        private readonly string _host = SecretAppSettingReader.ReadSection<SecretAppSetting>("TestSetup").Ip ?? "";
        private readonly string _user = SecretAppSettingReader.ReadSection<SecretAppSetting>("TestSetup").User ?? "";
        private readonly string _pw = SecretAppSettingReader.ReadSection<SecretAppSetting>("TestSetup").Password ?? "";
        private readonly int _port = 3344;
        private readonly string _api = SecretAppSettingReader.ReadSection<SecretAppSetting>("TestSetup").ApiKey ?? "";
        private readonly bool _ssl = false;

        private RepetierClient? client;

        private readonly bool _skipPrinterActionTests = true;

        #region Setup
        [SetUp]
        public void Setup()
        {
            string host = $"{(_ssl ? "https://" : "http://")}{_host}:{_port}";
            string ws = $"{(_ssl ? "wss://" : "ws://")}{_host}:{_port}/socket";
            client = new RepetierClient.RepetierConnectionBuilder()
                .WithServerAddress(host)
                .WithApiKey(_api)
                .WithWebSocket(ws)
                .WithTimeout(100)
                .Build();
            client.Error += (sender, args) =>
            {
                if (!client.ReThrowOnError)
                {
                    Assert.Fail($"Error: {args?.ToString()}");
                }
            };
            client.RestApiError += (sender, args) =>
            {
                if (!client.ReThrowOnError)
                {
                    Assert.Fail($"REST-Error: {args?.ToString()}");
                }
            };
        }
        #endregion

        #region Serialize
        [Test]
        public void SerializeJsonTest()
        {
            var dir = @"TestResults\Serialization\";
            Directory.CreateDirectory(dir);
            string serverConfig = Path.Combine(dir, "server.xml");
            if (File.Exists(serverConfig)) File.Delete(serverConfig);
            try
            {
                string host = $"{(_ssl ? "https://" : "http://")}{_host}:{_port}";
                RepetierClient.Instance = new RepetierClient(host)
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

        [Test]
        public void SerializeNewetonsoftJsonTest()
        {
            var dir = @"TestResults\Serialization\";
            Directory.CreateDirectory(dir);
            string serverConfig = Path.Combine(dir, "server.xml");
            if (File.Exists(serverConfig)) File.Delete(serverConfig);
            try
            {
                string host = $"{(_ssl ? "https://" : "http://")}{_host}:{_port}";
                RepetierClient.Instance = new RepetierClient(host)
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

        [Test]
        public void SerializeAllTypesWithJsonNewtonsoftTest()
        {
            var dir = @"TestResults\Serialization\";
            Directory.CreateDirectory(dir);
            string serverConfig = Path.Combine(dir, "server.xml");
            if (File.Exists(serverConfig)) File.Delete(serverConfig);
            try
            {
                List<Type> types = [.. AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(t => t.GetTypes())
                       .Where(t => t.IsClass && !t.Name.StartsWith("<") && t.Namespace?.StartsWith("AndreasReitberger.API.Repetier") is true)]
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
                    List<PropertyInfo> p = [.. t
                        .GetProperties()
                        .Where(prop => prop.GetCustomAttribute<JsonPropertyAttribute>(true) is not null)]
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
                    List<string> properties = [.. splitted.Select(row => extract.Match(row ?? "")?.Value ?? string.Empty)]
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

        [Test]
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
                    string host = $"{(_ssl ? "https://" : "http://")}{_host}:{_port}";
                    RepetierClient _server = new(host);
                    RepetierClient.Instance = new RepetierClient(host)
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

        [Test]
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
                    Extruders =
                    [
                        new() { Acceleration = 5000, Alias = "My #1 Extruder", ExtrudeSpeed = 5000, MaxTemp = 300, Num = 0 },
                        new() { Acceleration = 5000, Alias = "My #2 Extruder", ExtrudeSpeed = 5000, MaxTemp = 300, Num = 1 },
                    ],
                    HeatedBeds =
                    [
                        new() { Alias = "My Heated bed", MaxTemp = 110, LastTemp = 75, Temperatures = [new() { Temp = 75 }] }
                    ],
                    HeatedChambers =
                    [
                        new() { Alias = "My Heated chamber", MaxTemp = 110, LastTemp = 75, Temperatures = [new() { Temp = 75 }] }
                    ],
                    Webcams =
                    [
                        new() { WebCamUrlDynamic = new("https://some.url.de/"), Position = 0, Orientation = 90 },
                        new() { WebCamUrlDynamic = new("https://some.url.de/"), Position = 1, Orientation = 180 },
                    ]

                }.ToString();
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }
        #endregion

        #region Server Tests
        [Test]
        public async Task ServerInitTest()
        {
            try
            {
                if (client is null) throw new NullReferenceException($"The client was null!");
                //CancellationTokenSource cts = new(new TimeSpan(0, 0, 50));
                await client.CheckOnlineAsync();
                if (client.IsOnline)
                {
                    if (client.ActivePrinter == null)
                        await client.SetPrinterActiveAsync(0, true);
                    // Takes very long, not recommended
                    await client.RefreshAllAsync();
                    Assert.IsTrue(client.InitialDataFetched);
                }
                else
                    Assert.Fail($"Server {client.FullWebAddress} is offline.");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task ServerLoginTest()
        {
            try
            {
                if (client is null) throw new NullReferenceException($"The client was null!");
                await client.CheckOnlineAsync();
                if (client.IsOnline)
                {
                    bool succeed = false;
                    // Wait 1 minutes
                    CancellationTokenSource cts = new(new TimeSpan(0, 1, 0));
                    client.LoginResultReceived += ((sender, args) =>
                    {
                        Assert.IsTrue(args.LoginSucceeded);
                        succeed = true;
                        cts.Cancel();
                    });
                    await client.SetPrinterActiveAsync();
                    await client.StartListeningAsync();
                    // Wait till session is esstablished
                    while (client.Session == null && !cts.IsCancellationRequested)
                    {
                        await Task.Delay(250);
                    }
                    if (client.ActivePrinter == null)
                        await client.SetPrinterActiveAsync(0, true);
                    client.Login(_user, SecureStringHelper.ConvertToSecureString(_pw), client.SessionId);
                    while (!cts.IsCancellationRequested && !succeed)
                    {
                        await Task.Delay(100);
                    }
                    Assert.IsTrue(succeed);
                    await client.LogoutAsync();
                }
                else
                    Assert.Fail($"Server {client.FullWebAddress} is offline.");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task FetchPrintersTest()
        {
            try
            {
                if (client is null) throw new NullReferenceException($"The client was null!");

                await client.CheckOnlineAsync();
                if (client.IsOnline)
                {
                    if (client.ActivePrinter == null)
                        await client.SetPrinterActiveAsync(0, true);

                    List<IPrinter3d> printers = await client.GetPrintersAsync();
                    Assert.IsTrue(printers != null && printers.Count > 0);
                }
                else
                    Assert.Fail($"Server {client.FullWebAddress} is offline.");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task FetchPrintModelGroupsTest()
        {
            try
            {
                if (client is null) throw new NullReferenceException($"The client was null!");

                await client.CheckOnlineAsync();
                if (client.IsOnline)
                {
                    if (client.ActivePrinter == null)
                    {
                        await client.SetPrinterActiveAsync();
                    }

                    List<IGcodeGroup> modelgroups = await client.GetModelGroupsAsync();
                    Assert.IsTrue(modelgroups != null && modelgroups.Count > 0);

                    await client.RefreshModelGroupsAsync();
                    Assert.Greater(client.Groups.Count, 0);
                }
                else
                    Assert.Fail($"Server {client.FullWebAddress} is offline.");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task FetchPrintModelsTest()
        {
            try
            {
                if (client is null) throw new NullReferenceException($"The client was null!");

                await client.CheckOnlineAsync();
                if (client.IsOnline)
                {
                    if (client.ActivePrinter == null)
                        await client.SetPrinterActiveAsync(0, true);

                    IProgress<int> progress = new Progress<int>(prog =>
                        Debug.WriteLine($"Done: {prog:N0}")
                    );
                    Stopwatch sw = Stopwatch.StartNew();
                    List<IGcode> models = await client.GetModelsAsync("", AndreasReitberger.API.Print3dServer.Core.Enums.GcodeImageType.None, progress);
                    Assert.Greater(models.Count, 0);

                    sw.Stop();
                    Debug.WriteLine($"Time elapsed: {sw.Elapsed} (without images)");

                    sw = Stopwatch.StartNew();
                    models = await client.GetModelsAsync("", AndreasReitberger.API.Print3dServer.Core.Enums.GcodeImageType.Thumbnail, progress);
                    Assert.Greater(models.Count, 0);

                    sw.Stop();
                    Debug.WriteLine($"Time elapsed: {sw.Elapsed} (with thumbnails)");

                    sw = Stopwatch.StartNew();
                    models = await client.GetModelsAsync("", AndreasReitberger.API.Print3dServer.Core.Enums.GcodeImageType.Image, progress);
                    Assert.Greater(models.Count, 0);

                    sw.Stop();
                    Debug.WriteLine($"Time elapsed: {sw.Elapsed} (with images)");

                    models = await client.GetModelsAsync("", AndreasReitberger.API.Print3dServer.Core.Enums.GcodeImageType.None, progress);
                    Assert.Greater(models.Count, 0);

                    List<IGcode> filesCollection = [.. models.Take(25)];
                    Dictionary<long, byte[]>? images = await client.GetModelImagesAsync(filesCollection, imageType: AndreasReitberger.API.Print3dServer.Core.Enums.GcodeImageType.Image, progress);
                    Assert.IsTrue(images?.Select(kp => kp.Value).Any(image => image.Length > 0));

                    IList<IGcode>? updatedModels = await client.UpdateModelImagesAsync(filesCollection, AndreasReitberger.API.Print3dServer.Core.Enums.GcodeImageType.Image, progress);
                    Assert.IsTrue(updatedModels?.Select(model => model.Image).Any(image => image?.Length > 0));
                }
                else
                    Assert.Fail($"Server {client.FullWebAddress} is offline.");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task FetchJobListTest()
        {
            try
            {
                if (client is null) throw new NullReferenceException($"The client was null!");

                await client.CheckOnlineAsync();
                if (client.IsOnline)
                {
                    if (client.ActivePrinter == null)
                        await client.SetPrinterActiveAsync(-1, true);

                    ObservableCollection<IPrint3dJob> jobs = await client.GetJobListAsync();
                    Assert.IsNotNull(jobs);
                }
                else
                    Assert.Fail($"Server {client.FullWebAddress} is offline.");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task PrintModelTest()
        {
            try
            {
                if (client is null) throw new NullReferenceException($"The client was null!");

                await client.CheckOnlineAsync();
                if (client.IsOnline)
                {
                    if (client.ActivePrinter == null)
                        await client.SetPrinterActiveAsync(-1, true);

                    List<IGcode> models = await client.GetModelsAsync();
                    if (models?.Count > 0)
                    {
                        bool printed = await client.CopyModelToPrintQueueAsync(model: models[0], startPrintIfPossible: false);
                        Assert.IsTrue(printed);
                    }
                    else
                    {
                        Assert.Fail($"No models found on server!");
                    }
                }
                else
                    Assert.Fail($"Server {client.FullWebAddress} is offline.");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task DownloadPrintReport()
        {
            try
            {
                if (client is null) throw new NullReferenceException($"The client was null!");

                await client.CheckOnlineAsync();
                if (client.IsOnline)
                {
                    await client.SetPrinterActiveAsync();
                    ObservableCollection<RepetierHistorySummaryItem>? history = await client.GetHistorySummaryItemsAsync("", 2022, true);
                    Assert.IsTrue(history?.Any());

                    ObservableCollection<RepetierHistoryListItem> list = await client.GetHistoryListAsync("", "", 50, 0, 0, true);
                    Assert.IsTrue(list?.Any());

                    RepetierHistoryListItem? historyItem = list?.FirstOrDefault();
                    Assert.IsNotNull(historyItem);

                    byte[]? report = await RepetierClient.Instance.GetHistoryReportAsync(historyItem.Id);
                    Assert.Greater(report.Length, 0);
                    string downloadTarget = @"report.pdf";
                    await File.WriteAllBytesAsync(downloadTarget, report);
                    Assert.IsTrue(File.Exists(downloadTarget));
                    //Process.Start(downloadTarget);
                }
                else
                    Assert.Fail($"Server {client.FullWebAddress} is offline.");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task GetGPIOList()
        {
            try
            {
                if (client is null) throw new NullReferenceException($"The client was null!");

                await client.CheckOnlineAsync();
                if (client.IsOnline)
                {
                    await client.SetPrinterActiveAsync(1);
                    ObservableCollection<RepetierGpioListItem> report = await client.GetGPIOListAsync();
                    Assert.IsNotEmpty(report);
                }
                else
                    Assert.Fail($"Server {client.FullWebAddress} is offline.");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task GetHistoryList()
        {
            try
            {
                if (client is null) throw new NullReferenceException($"The client was null!");

                await client.CheckOnlineAsync();
                if (client.IsOnline)
                {
                    await client.SetPrinterActiveAsync(1);
                    ObservableCollection<RepetierHistoryListItem>? report = await client.GetHistoryListAsync(client?.ActivePrinter?.Slug ?? "");
                    Assert.IsNotEmpty(report);
                }
                else
                    Assert.Fail($"Server {client.FullWebAddress} is offline.");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task GetWebcalls()
        {
            try
            {
                if (client is null) throw new NullReferenceException($"The client was null!");

                await client.CheckOnlineAsync();
                if (client.IsOnline)
                {
                    await client.SetPrinterActiveAsync(1);
                    ObservableCollection<RepetierWebCallAction> report = await client.GetWebCallActionsAsync();
                    Assert.IsNotEmpty(report);
                }
                else
                    Assert.Fail($"Server {client.FullWebAddress} is offline.");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task GetExternalCommands()
        {
            try
            {
                if (client is null) throw new NullReferenceException($"The client was null!");

                await client.CheckOnlineAsync();
                if (client.IsOnline)
                {
                    await client.SetPrinterActiveAsync(1);
                    ObservableCollection<ExternalCommand> commands = await client.GetExternalCommandsAsync();
                    Assert.IsNotEmpty(commands);
                }
                else
                    Assert.Fail($"Server {client.FullWebAddress} is offline.");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        /**/
        [Test]
        public async Task OnlineTest()
        {
            //if (_skipOnlineTests) return;
            try
            {
                if (client is null) throw new NullReferenceException($"The client was null!");

                await client.CheckOnlineAsync(3500);//.ConfigureAwait(false);
                await client.SetPrinterActiveAsync(1);
                // Wait 10 minutes
                CancellationTokenSource cts = new(new TimeSpan(0, 10, 0));
                do
                {
                    await Task.Delay(10000);
                    await client.CheckOnlineAsync();
                    await client.RefreshAllAsync();
                    if (client.IsPrinting)
                    {
                        IPrint3dJobStatus? info = client.ActiveJob;
                        if (info == null)
                            Assert.Fail("Print info was null");
                    }
                } while (client.IsOnline && !cts.IsCancellationRequested);
                Assert.IsTrue(cts.IsCancellationRequested);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task WebcamTest()
        {
            try
            {
                if (client is null) throw new NullReferenceException($"The client was null!");

                await client.CheckOnlineAsync();
                Assert.IsTrue(client.IsOnline);

                await client.SetPrinterActiveAsync();

                RepetierWebcamType type = RepetierWebcamType.Dynamic;
                string webcamUriDynamic = await client.GetWebCamUriAsync(0, type);
                Assert.IsTrue(Uri.TryCreate(webcamUriDynamic, UriKind.RelativeOrAbsolute, out _));

                type = RepetierWebcamType.Static;
                webcamUriDynamic = await client.GetWebCamUriAsync(0, type);
                Assert.IsTrue(Uri.TryCreate(webcamUriDynamic, UriKind.RelativeOrAbsolute, out _));

                type = RepetierWebcamType.Dynamic;
                var webCams = await client.GetWebCamConfigsAsync();
                Assert.Greater(webCams.Count, 0);
                foreach (var cam in webCams)
                {
                    webcamUriDynamic = await client.GetWebCamUriAsync((int)cam.Position, type);
                    Assert.IsTrue(Uri.TryCreate(webcamUriDynamic, UriKind.RelativeOrAbsolute, out _));

                    webcamUriDynamic = client.GetWebCamUri(cam);
                    Assert.IsTrue(Uri.TryCreate(webcamUriDynamic, UriKind.RelativeOrAbsolute, out _));
                }
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task WebsocketTest()
        {
            try
            {
                if (client is null) throw new NullReferenceException($"The client was null!");

                Dictionary<DateTime, string> websocketMessages = [];
                Dictionary<string, string> unkownJsonRespones = [];

                await client.CheckOnlineAsync();
                await client.SetPrinterActiveAsync();
                DateTime start = DateTime.Now;
                await client.StartListeningAsync();

                client.WebSocketDataReceived += (o, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Message))
                    {
                        websocketMessages.Add(DateTime.Now, args.Message);
                        Console.WriteLine($"WebSocket Data: {args.Message} (Total: {websocketMessages.Count})");
                    }
                };

                client.WebSocketMessageReceived += (o, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Message))
                    {
                        websocketMessages.Add(DateTime.Now, args.Message);
                        Debug.WriteLine($"WebSocket Data: {args.Message} (Total: {websocketMessages.Count})");
                    }
                };
                client.WebSocketError += (o, args) =>
                {
                    Assert.Fail($"Websocket closed due to an error: {args}");
                };
                client.IgnoredJsonResultsChanged += (o, args) =>
                {
                    foreach (var keyPair in args.NewIgnoredJsonResults)
                    {
                        if (!unkownJsonRespones.ContainsKey(keyPair.Key))
                            unkownJsonRespones.Add(keyPair.Key, keyPair.Value);
                    }
                };
                // Wait 30 minutes
                CancellationTokenSource cts = new(new TimeSpan(0, 60, 0));
                client.WebSocketDisconnected += (o, args) =>
                {
                    var duraton = DateTime.Now - start;
                    var messages = websocketMessages;
                    if (!cts.IsCancellationRequested)
                        Assert.Fail($"Websocket unexpectly closed: {args}");
                };

                do
                {
                    await Task.Delay(10000);
                    await client.CheckOnlineAsync();
                } while (client.IsOnline && !cts.IsCancellationRequested);
                await client.StopListeningAsync();


                Assert.IsTrue(cts.IsCancellationRequested && websocketMessages?.Count > 50);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        /**/
        [Test]
        public async Task SetHeatedbedTest()
        {
            if (_skipPrinterActionTests) return;
            try
            {
                if (client is null) throw new NullReferenceException($"The client was null!");
                await client.CheckOnlineAsync();
                if (client.IsOnline)
                {
                    if (client.ActivePrinter == null)
                        await client.SetPrinterActiveAsync(1, true);

                    bool result = await client.SetBedTemperatureAsync(0, 25);
                    // Set timeout to 5 minutes
                    var cts = new CancellationTokenSource(new TimeSpan(0, 5, 0));

                    if (result)
                    {
                        double temp = 0;
                        // Wait till temp rises
                        while (temp < 23)
                        {
                            var state = await client.GetStatesAsync();
                            if (state != null && state?.Count > 0)
                            {
                                var beds = state.FirstOrDefault().Value.HeatedBeds;
                                if (beds == null || beds.Count == 0)
                                {
                                    Assert.Fail("No heated bed found");
                                    break;
                                }
                                var bed = beds[0];
                                temp = bed.TempRead ?? 0;
                            }
                        }
                        Assert.GreaterOrEqual(temp, 23);
                        // Turn off bed
                        result = await client.SetBedTemperatureAsync(0, 0);
                        // Set timeout to 5 minutes
                        cts = new CancellationTokenSource(new TimeSpan(0, 5, 0));
                        if (result)
                        {

                            while (temp > 23)
                            {
                                var state = await client.GetStatesAsync();
                                if (state != null && state?.Count > 0)
                                {
                                    var beds = state.FirstOrDefault().Value.HeatedBeds;
                                    if (beds == null || beds.Count == 0)
                                    {
                                        Assert.Fail("No heated bed found");
                                        break;
                                    }
                                    var bed = beds[0];
                                    temp = bed.TempRead ?? 0;
                                }
                            }
                            Assert.LessOrEqual(23, temp);
                        }
                        else
                            Assert.Fail("Command failed to be sent.");
                    }
                    else
                        Assert.Fail("Command failed to be sent.");
                }
                else
                    Assert.Fail($"Server {client.FullWebAddress} is offline.");
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

        [Test]
        public async Task SetExtruderTest()
        {
            if (_skipPrinterActionTests) return;
            try
            {
                if (client is null) throw new NullReferenceException($"The client was null!");
                await client.CheckOnlineAsync();
                if (client.IsOnline)
                {
                    if (client.ActivePrinter == null)
                        await client.SetPrinterActiveAsync(1, true);

                    bool result = await client.SetExtruderTemperatureAsync(extruder: 0, temperature: 30);
                    // Set timeout to 3 minutes
                    var cts = new CancellationTokenSource(new TimeSpan(0, 3, 0));

                    if (result)
                    {
                        double extruderTemp = 0;
                        // Wait till temp rises
                        while (extruderTemp < 28)
                        {
                            var state = await client.GetStatesAsync();
                            if (state != null && state?.Count > 0)
                            {
                                List<RepetierPrinterToolhead> extruders = state.FirstOrDefault().Value.Extruder;
                                if (extruders == null || extruders.Count == 0)
                                {
                                    Assert.Fail("No extrudes available");
                                    break;
                                }
                                RepetierPrinterToolhead extruder = extruders[0];
                                extruderTemp = extruder.TempRead ?? 0;
                            }
                        }
                        Assert.GreaterOrEqual(extruderTemp, 28);
                        // Turn off extruder
                        result = await client.SetExtruderTemperatureAsync(0, 0);
                        // Set timeout to 3 minutes
                        cts = new CancellationTokenSource(new TimeSpan(0, 3, 0));
                        if (result)
                        {

                            while (extruderTemp > 28)
                            {
                                var state = await client.GetStatesAsync();
                                if (state != null && state?.Count > 0)
                                {
                                    var extruders = state.FirstOrDefault().Value.Extruder;
                                    if (extruders == null || extruders.Count == 0)
                                    {
                                        Assert.Fail("No extrudes available");
                                        break;
                                    }
                                    var extruder = extruders[0];
                                    extruderTemp = extruder.TempRead ?? 0;
                                }
                            }
                            Assert.LessOrEqual(extruderTemp, 28);
                        }
                        else
                            Assert.Fail("Command failed to be sent.");
                    }
                    else
                        Assert.Fail("Command failed to be sent.");
                }
                else
                    Assert.Fail($"Server {client.FullWebAddress} is offline.");
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

        [Test]
        public async Task ConnectionBuilderTest()
        {
            string host = $"{(_ssl ? "https://" : "http://")}{_host}:{_port}";
            using RepetierClient client = new RepetierClient.RepetierConnectionBuilder()
                .WithServerAddress(host)
                .WithApiKey(_api)
                .Build();
            await client.CheckOnlineAsync();
            Assert.IsTrue(client?.IsOnline ?? false);
        }

        [Test]
        public async Task ServerQueryTests()
        {
            try
            {
                if (client is null) throw new NullReferenceException($"The client was null!");

                await client.CheckOnlineAsync();
                if (client.IsOnline)
                {
                    if (client.ActivePrinter == null)
                        await client.SetPrinterActiveAsync(0, true);


                    List<IPrinter3d> printers = await client.GetPrintersAsync();
                    Assert.IsNotNull(printers);
                    string json = JsonConvert.SerializeObject(printers, Formatting.Indented);

                    RepetierAvailableUpdateInfo? update = await client.GetAvailableServerUpdateAsync();
                    Assert.IsNotNull(update);
                    json = JsonConvert.SerializeObject(update, Formatting.Indented);

                    RepetierCurrentPrintInfo? printInfo = await client.GetCurrentPrintInfoAsync();
                    Assert.IsNotNull(printInfo);
                    json = JsonConvert.SerializeObject(printInfo, Formatting.Indented);

                    var printInfos = await client.GetCurrentPrintInfosAsync();
                    Assert.IsNotNull(printInfos);
                    json = JsonConvert.SerializeObject(printInfos, Formatting.Indented);

                    var cmds = await client.GetExternalCommandsAsync();
                    Assert.IsNotNull(cmds);
                    json = JsonConvert.SerializeObject(cmds, Formatting.Indented);

                    var gpios = await client.GetGPIOListAsync();
                    Assert.IsNotNull(gpios);
                    json = JsonConvert.SerializeObject(gpios, Formatting.Indented);

                    var history = await client.GetHistoryListAsync(client.ActivePrinter?.Slug);
                    Assert.IsNotNull(history);
                    json = JsonConvert.SerializeObject(history, Formatting.Indented);

                    // Only works if enabled in Settings
                    /*
                    var hid = history?.FirstOrDefault()?.Id;
                    var historyReport = await client.GetHistoryReportAsync(history?.FirstOrDefault()?.Id ?? 0);
                    Assert.IsNotNull(historyReport);
                    json = JsonConvert.SerializeObject(historyReport, Formatting.Indented);
                    */

                    var historySummary = await client.GetHistorySummaryItemsAsync(client.ActivePrinter?.Slug, 2023, true);
                    Assert.IsNotNull(historySummary);
                    json = JsonConvert.SerializeObject(historySummary, Formatting.Indented);

                    var jobList = await client.GetJobListAsync();
                    Assert.IsNotNull(jobList);
                    json = JsonConvert.SerializeObject(jobList, Formatting.Indented);

                    var license = await client.GetLicenseDataAsync();
                    Assert.IsNotNull(license);
                    json = JsonConvert.SerializeObject(license, Formatting.Indented);

                    var messages = await client.GetMessagesAsync();
                    Assert.IsNotNull(messages);
                    json = JsonConvert.SerializeObject(messages, Formatting.Indented);

                    var groups = await client.GetModelGroupsAsync();
                    Assert.IsNotNull(groups);
                    json = JsonConvert.SerializeObject(groups, Formatting.Indented);

                    List<IGcode> files = await client.GetModelsAsync();
                    Assert.IsNotNull(files);
                    json = JsonConvert.SerializeObject(files, Formatting.Indented);

                    var config = await client.GetPrinterConfigAsync();
                    Assert.IsNotNull(config);
                    json = JsonConvert.SerializeObject(config, Formatting.Indented);

                    var servers = await client.GetProjectsListServerAsync();
                    Assert.IsNotNull(servers);
                    json = JsonConvert.SerializeObject(servers, Formatting.Indented);

                    var projects = await client.GetProjectItemsAsync(servers?.Server?.FirstOrDefault()?.Uuid ?? Guid.Empty);
                    Assert.IsNotNull(projects);
                    json = JsonConvert.SerializeObject(projects, Formatting.Indented);

                    var folders = await client.GetProjectsGetFolderAsync(servers?.Server?.FirstOrDefault()?.Uuid ?? Guid.Empty);
                    Assert.IsNotNull(folders);
                    json = JsonConvert.SerializeObject(folders, Formatting.Indented);

                    var state = await client.GetStatesAsync();
                    Assert.IsNotNull(state);
                    json = JsonConvert.SerializeObject(state, Formatting.Indented);

                    //await client.RefreshAllAsync();
                    //Assert.IsTrue(client.InitialDataFetched);
                }
                else
                    Assert.Fail($"Server {client.FullWebAddress} is offline.");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task DownloadGcodeTestAsync()
        {
            try
            {
                if (client is null) throw new NullReferenceException($"The client was null!");

                await client.CheckOnlineAsync();
                if (client.IsOnline)
                {
                    if (client.ActivePrinter == null)
                        await client.SetPrinterActiveAsync(0, true);


                    List<IGcode> files = await client.GetFilesAsync();
                    Assert.IsNotNull(files);

                    byte[]? file = await client.DownloadGcodeAsync(files.FirstOrDefault().Identifier.ToString());
                    Assert.Greater(file.Length, 0);

                    byte[]? file2 = await client.DownloadGcodeAsync(files.FirstOrDefault(), Encoding.Default);
                    Assert.Greater(file2.Length, 0);

                    Assert.AreEqual(file2.Length, file.Length);
                }
                else
                    Assert.Fail($"Server {client.FullWebAddress} is offline.");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }
        #endregion

        #region Cleanup
        [TearDown]
        public void BaseTearDown()
        {
            client?.Dispose();
        }
        #endregion
    }
}
