using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using AndreasReitberger.API.Repetier;
using AndreasReitberger.API.Repetier.Enum;
using AndreasReitberger.API.Repetier.Models;
using AndreasReitberger.API.Repetier.SourceGeneration;
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
    public partial class RepetierServerSharpApiTest
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

        [GeneratedRegex(@"^[A-Z][A-Za-z0-9]*$")]
        private static partial Regex MyRegex();

        [GeneratedRegex(@"(?<=\"").+?(?=\"")")]
        private static partial Regex MyRegex_Extract();

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
                    //Assert.Fail($"REST-Error: {args?.ToString()}");
                    Debug.WriteLine($"REST-Error: {args?.ToString()}");
                }
            };
        }
        #endregion

        #region Serialize
        [Test]
        public void SerializeJsonTest()
        {
            string dir = @"TestResults\Serialization\";
            Directory.CreateDirectory(dir);
            string serverConfig = Path.Combine(dir, "server.xml");
            if (File.Exists(serverConfig)) File.Delete(serverConfig);
            try
            {
                string host = $"{(_ssl ? "https://" : "http://")}{_host}:{_port}";
                var sClient = new RepetierClient(host)
                {
                    FreeDiskSpace = 1523165212,
                    TotalDiskSpace = 65621361616161,
                };
                sClient.SetProxy(true, "https://testproxy.de", 447, "User", "my_awesome_pwd", true);

                string serializedString = System.Text.Json.JsonSerializer.Serialize(sClient, typeof(RepetierClient), RepetierSourceGenerationContext.Default);
                RepetierClient? serializedObject = (RepetierClient?)System.Text.Json.JsonSerializer.Deserialize(serializedString, typeof(RepetierClient), context: RepetierSourceGenerationContext.Default);
                Assert.That(serializedObject is RepetierClient server && server != null, Is.True);

            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public void SerializeAllTypesWithJsonNewtonsoftTest()
        {
            string dir = @"TestResults\Serialization\";
            Directory.CreateDirectory(dir);
            string serverConfig = Path.Combine(dir, "server.xml");
            if (File.Exists(serverConfig)) File.Delete(serverConfig);
            try
            {
                List<Type> types = [.. AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(t => t.GetTypes())
                       .Where(t => t.IsClass && !t.Name.StartsWith('<') && t.Namespace?.StartsWith("AndreasReitberger.API.Repetier") is true)]
                       ;
                Regex r = MyRegex();
                Regex extract = MyRegex_Extract();
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
                    string? serializedString =
                        JsonConvertHelper.ToSettingsString(obj, settings: RepetierSourceGenerationContext.Default);
                    if (serializedString == "{}") continue;

                    // Get all property infos
                    List<PropertyInfo> p = [.. t
                        .GetProperties()
                        .Where(prop => prop.GetCustomAttribute<JsonPropertyAttribute>(true) is not null)]
                        ;

                    // Get the property names from the json text
                    string[] splitString = serializedString.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    bool skip = false;
                    StringBuilder sb = new();
                    // Cleanup from child nodes, those will be checked individually
                    foreach (string line in splitString)
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
                    string[] splitted = serializedString.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
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
                        Assert.That(valid, Is.True, message: msg);
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

            string dir = @"TestResults\Serialization\";
            Directory.CreateDirectory(dir);
            string serverConfig = Path.Combine(dir, "server.xml");
            if (File.Exists(serverConfig)) File.Delete(serverConfig);
            try
            {
                XmlSerializer xmlSerializer = new(typeof(RepetierClient));
                using (FileStream fileStream = new(serverConfig, FileMode.Create))
                {
                    string host = $"{(_ssl ? "https://" : "http://")}{_host}:{_port}";
                    var sClient = new RepetierClient(host)
                    {
                        ActiveToolheadIndex = 1,
                        FreeDiskSpace = 1523165212,
                        TotalDiskSpace = 65621361616161,
                        IsMultiExtruder = true,
                    };
                    sClient.SetProxy(true, "https://testproxy.de", 447, "User", "my_awesome_pwd", true);

                    xmlSerializer.Serialize(fileStream, sClient);
                    Assert.That(File.Exists(Path.Combine(dir, "server.xml")), Is.True);
                }

                xmlSerializer = new XmlSerializer(typeof(RepetierClient));
                using (FileStream fileStream = new(serverConfig, FileMode.Open))
                {
                    RepetierClient? instance = xmlSerializer.Deserialize(fileStream) as RepetierClient;
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
                    Assert.That(client.InitialDataFetched, Is.True);
                    //Assert.That(client == RepetierClient.Instance);
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
                        Assert.That(args.LoginSucceeded, Is.True);
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
                    Assert.That(succeed, Is.True);
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
                    Assert.That(printers != null && printers.Count > 0, Is.True);
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
                    Assert.That(modelgroups != null && modelgroups.Count > 0, Is.True);

                    await client.RefreshModelGroupsAsync();
                    Assert.That(client.Groups, Is.Not.Empty);
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
                    Assert.That(models, Is.Not.Empty);

                    sw.Stop();
                    Debug.WriteLine($"Time elapsed: {sw.Elapsed} (without images)");

                    sw = Stopwatch.StartNew();
                    models = await client.GetModelsAsync("", AndreasReitberger.API.Print3dServer.Core.Enums.GcodeImageType.Thumbnail, progress);
                    Assert.That(models, Is.Not.Empty);

                    sw.Stop();
                    Debug.WriteLine($"Time elapsed: {sw.Elapsed} (with thumbnails)");

                    sw = Stopwatch.StartNew();
                    models = await client.GetModelsAsync("", AndreasReitberger.API.Print3dServer.Core.Enums.GcodeImageType.Image, progress);
                    Assert.That(models, Is.Not.Empty);

                    sw.Stop();
                    Debug.WriteLine($"Time elapsed: {sw.Elapsed} (with images)");

                    models = await client.GetModelsAsync("", AndreasReitberger.API.Print3dServer.Core.Enums.GcodeImageType.None, progress);
                    Assert.That(models, Is.Not.Empty);

                    List<IGcode> filesCollection = [.. models.Take(25)];
                    Dictionary<long, byte[]>? images = await client.GetModelImagesAsync(filesCollection, imageType: AndreasReitberger.API.Print3dServer.Core.Enums.GcodeImageType.Image, progress);
                    Assert.That(images?.Select(kp => kp.Value).Any(image => image.Length > 0), Is.True);

                    IList<IGcode>? updatedModels = await client.UpdateModelImagesAsync(filesCollection, AndreasReitberger.API.Print3dServer.Core.Enums.GcodeImageType.Image, progress);
                    Assert.That(updatedModels?.Select(model => model.Image).Any(image => image?.Length > 0), Is.True);
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
                    Assert.That(jobs, Is.Not.Null);
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
                        Assert.That(printed, Is.True);
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
                    Assert.That(history?.Any(), Is.True);

                    ObservableCollection<RepetierHistoryListItem> list = await client.GetHistoryListAsync("", "", 50, 0, 0, true);
                    Assert.That(list?.Any(), Is.True);

                    RepetierHistoryListItem? historyItem = list?.FirstOrDefault();
                    Assert.That(historyItem, Is.Not.Null);

                    byte[]? report = await client.GetHistoryReportAsync(historyItem.Id);
                    Assert.That(report, Is.Not.Empty);
                    string downloadTarget = @"report.pdf";
                    await File.WriteAllBytesAsync(downloadTarget, report);
                    Assert.That(File.Exists(downloadTarget), Is.True);
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
                    Assert.That(report, Is.Not.Empty);
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
                    Assert.That(report, Is.Not.Empty);
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
                    Assert.That(report, Is.Not.Empty);
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
                    Assert.That(commands, Is.Not.Empty);
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
                Assert.That(cts.IsCancellationRequested, Is.True);
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
                Assert.That(client.IsOnline, Is.True);

                await client.SetPrinterActiveAsync();

                RepetierWebcamType type = RepetierWebcamType.Dynamic;
                string webcamUriDynamic = await client.GetWebCamUriAsync(0, type);
                Assert.That(Uri.TryCreate(webcamUriDynamic, UriKind.RelativeOrAbsolute, out _), Is.True);

                type = RepetierWebcamType.Static;
                webcamUriDynamic = await client.GetWebCamUriAsync(0, type);
                Assert.That(Uri.TryCreate(webcamUriDynamic, UriKind.RelativeOrAbsolute, out _), Is.True);

                type = RepetierWebcamType.Dynamic;
                List<IWebCamConfig>? webCams = await client.GetWebCamConfigsAsync();
                Assert.That(webCams, Is.Not.Empty);
                foreach (IWebCamConfig cam in webCams)
                {
                    webcamUriDynamic = await client.GetWebCamUriAsync((int)cam.Position, type);
                    Assert.That(Uri.TryCreate(webcamUriDynamic, UriKind.RelativeOrAbsolute, out _), Is.True);

                    webcamUriDynamic = client.GetWebCamUri(cam);
                    Assert.That(Uri.TryCreate(webcamUriDynamic, UriKind.RelativeOrAbsolute, out _), Is.True);
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
                    Debug.WriteLine($"Websocket closed due to an error: {args}");
                    //Assert.Fail($"Websocket closed due to an error: {args}");
                };
                client.IgnoredJsonResultsChanged += (o, args) =>
                {
                    foreach (KeyValuePair<string, string> keyPair in args.NewIgnoredJsonResults)
                    {
                        if (!unkownJsonRespones.ContainsKey(keyPair.Key))
                            unkownJsonRespones.Add(keyPair.Key, keyPair.Value);
                    }
                };
                // Wait 30 minutes
                CancellationTokenSource cts = new(new TimeSpan(0, 30, 0));
                client.WebSocketDisconnected += (o, args) =>
                {
                    TimeSpan duraton = DateTime.Now - start;
                    Dictionary<DateTime, string> messages = websocketMessages;
                    if (!cts.IsCancellationRequested)
                        Assert.Fail($"Websocket unexpectly closed: {args}");
                };

                do
                {
                    await Task.Delay(10000);
                    await client.CheckOnlineAsync();
                } while (client.IsOnline && !cts.IsCancellationRequested);
                await client.StopListeningAsync();


                Assert.That(cts.IsCancellationRequested && websocketMessages?.Count > 50, Is.True);
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
                    CancellationTokenSource cts = new(new TimeSpan(0, 5, 0));

                    if (result)
                    {
                        double temp = 0;
                        // Wait till temp rises
                        while (temp < 23)
                        {
                            Dictionary<string, RepetierPrinterState>? state = await client.GetStatesAsync();
                            if (state != null && state?.Count > 0)
                            {
                                List<RepetierPrinterHeaterComponent> beds = state.FirstOrDefault().Value.HeatedBeds;
                                if (beds == null || beds.Count == 0)
                                {
                                    Assert.Fail("No heated bed found");
                                    break;
                                }
                                RepetierPrinterHeaterComponent bed = beds[0];
                                temp = bed.TempRead ?? 0;
                            }
                        }
                        Assert.That(temp, Is.GreaterThanOrEqualTo(23));
                        // Turn off bed
                        result = await client.SetBedTemperatureAsync(0, 0);
                        // Set timeout to 5 minutes
                        cts = new CancellationTokenSource(new TimeSpan(0, 5, 0));
                        if (result)
                        {
                            while (temp > 23)
                            {
                                Dictionary<string, RepetierPrinterState>? state = await client.GetStatesAsync();
                                if (state != null && state?.Count > 0)
                                {
                                    List<RepetierPrinterHeaterComponent> beds = state.FirstOrDefault().Value.HeatedBeds;
                                    if (beds == null || beds.Count == 0)
                                    {
                                        Assert.Fail("No heated bed found");
                                        break;
                                    }
                                    RepetierPrinterHeaterComponent bed = beds[0];
                                    temp = bed.TempRead ?? 0;
                                }
                            }
                            Assert.That(temp, Is.LessThanOrEqualTo(23));
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
                    CancellationTokenSource cts = new(new TimeSpan(0, 3, 0));

                    if (result)
                    {
                        double extruderTemp = 0;
                        // Wait till temp rises
                        while (extruderTemp < 28)
                        {
                            Dictionary<string, RepetierPrinterState>? state = await client.GetStatesAsync();
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
                        Assert.That(extruderTemp, Is.GreaterThanOrEqualTo(28));
                        // Turn off extruder
                        result = await client.SetExtruderTemperatureAsync(0, 0);
                        // Set timeout to 3 minutes
                        cts = new CancellationTokenSource(new TimeSpan(0, 3, 0));
                        if (result)
                        {

                            while (extruderTemp > 28)
                            {
                                Dictionary<string, RepetierPrinterState>? state = await client.GetStatesAsync();
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
                            Assert.That(extruderTemp, Is.LessThanOrEqualTo(28));
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
            Assert.That(client?.IsOnline ?? false, Is.True);
        }

        [Test]
        public async Task ServerQueryTests()
        {
            try
            {
                if (client is null) throw new NullReferenceException($"The client was null!");
                Stopwatch sw = Stopwatch.StartNew();
                await client.CheckOnlineAsync();
                if (client.IsOnline)
                {
                    if (client.ActivePrinter == null)
                        await client.SetPrinterActiveAsync(0, true);
                    TimeSpan last = new(0);

                    List<IPrinter3d> printers = await client.GetPrintersAsync();
                    Assert.That(printers, Is.Not.Null);
                    string json = JsonConvert.SerializeObject(printers, Formatting.Indented);

                    Debug.WriteLine($"{nameof(client.GetPrintersAsync)}: Took {sw.Elapsed - last}");
                    last = sw.Elapsed;

                    RepetierAvailableUpdateInfo? update = await client.GetAvailableServerUpdateAsync();
                    Assert.That(update, Is.Not.Null);
                    json = JsonConvert.SerializeObject(update, Formatting.Indented);

                    Debug.WriteLine($"{nameof(client.GetAvailableServerUpdateAsync)}: Took {sw.Elapsed - last}");
                    last = sw.Elapsed;

                    RepetierCurrentPrintInfo? printInfo = await client.GetCurrentPrintInfoAsync();
                    Assert.That(printInfo, Is.Not.Null);
                    json = JsonConvert.SerializeObject(printInfo, Formatting.Indented);

                    Debug.WriteLine($"{nameof(client.GetCurrentPrintInfoAsync)}: Took {sw.Elapsed - last}");
                    last = sw.Elapsed;

                    ObservableCollection<RepetierCurrentPrintInfo> printInfos = await client.GetCurrentPrintInfosAsync();
                    Assert.That(printInfos, Is.Not.Null);
                    json = JsonConvert.SerializeObject(printInfos, Formatting.Indented);

                    Debug.WriteLine($"{nameof(client.GetCurrentPrintInfosAsync)}: Took {sw.Elapsed - last}");
                    last = sw.Elapsed;

                    ObservableCollection<ExternalCommand> cmds = await client.GetExternalCommandsAsync();
                    Assert.That(cmds, Is.Not.Null);
                    json = JsonConvert.SerializeObject(cmds, Formatting.Indented);

                    Debug.WriteLine($"{nameof(client.GetExternalCommandsAsync)}: Took {sw.Elapsed - last}");
                    last = sw.Elapsed;

                    ObservableCollection<RepetierGpioListItem> gpios = await client.GetGPIOListAsync();
                    Assert.That(gpios, Is.Not.Null);
                    json = JsonConvert.SerializeObject(gpios, Formatting.Indented);

                    Debug.WriteLine($"{nameof(client.GetGPIOListAsync)}: Took {sw.Elapsed - last}");
                    last = sw.Elapsed;

                    if (client.ActivePrinter?.Slug is not null)
                    {
                        ObservableCollection<RepetierHistoryListItem> history = await client.GetHistoryListAsync(client.ActivePrinter.Slug);
                        Assert.That(history, Is.Not.Null);
                        json = JsonConvert.SerializeObject(history, Formatting.Indented);

                        Debug.WriteLine($"{nameof(client.GetHistoryListAsync)}: Took {sw.Elapsed - last}");
                        last = sw.Elapsed;
                        // Only works if enabled in Settings
                        /*
                        var hid = history?.FirstOrDefault()?.Id;
                        var historyReport = await client.GetHistoryReportAsync(history?.FirstOrDefault()?.Id ?? 0);
                        Assert.IsNotNull(historyReport);
                        json = JsonConvert.SerializeObject(historyReport, Formatting.Indented);
                        */

                        ObservableCollection<RepetierHistorySummaryItem>? historySummary = await client.GetHistorySummaryItemsAsync(client.ActivePrinter.Slug, 2023, true);
                        Assert.That(historySummary, Is.Not.Null);
                        json = JsonConvert.SerializeObject(historySummary, Formatting.Indented);

                        Debug.WriteLine($"{nameof(client.GetHistorySummaryItemsAsync)}: Took {sw.Elapsed - last}");
                        last = sw.Elapsed;
                    }
                    ObservableCollection<IPrint3dJob> jobList = await client.GetJobListAsync();
                    Assert.That(jobList, Is.Not.Null);
                    json = JsonConvert.SerializeObject(jobList, Formatting.Indented);

                    Debug.WriteLine($"{nameof(client.GetJobListAsync)}: Took {sw.Elapsed - last}");
                    last = sw.Elapsed;

                    RepetierLicenseInfo? license = await client.GetLicenseDataAsync();
                    Assert.That(license, Is.Not.Null);
                    json = JsonConvert.SerializeObject(license, Formatting.Indented);

                    Debug.WriteLine($"{nameof(client.GetLicenseDataAsync)}: Took {sw.Elapsed - last}");
                    last = sw.Elapsed;

                    ObservableCollection<RepetierMessage> messages = await client.GetMessagesAsync();
                    Assert.That(messages, Is.Not.Null);
                    json = JsonConvert.SerializeObject(messages, Formatting.Indented);

                    Debug.WriteLine($"{nameof(client.GetMessagesAsync)}: Took {sw.Elapsed - last}");
                    last = sw.Elapsed;

                    List<IGcodeGroup> groups = await client.GetModelGroupsAsync();
                    Assert.That(groups, Is.Not.Null);
                    json = JsonConvert.SerializeObject(groups, Formatting.Indented);

                    Debug.WriteLine($"{nameof(client.GetModelGroupsAsync)}: Took {sw.Elapsed - last}");
                    last = sw.Elapsed;

                    List<IGcode> files = await client.GetModelsAsync();
                    Assert.That(files, Is.Not.Null);
                    json = JsonConvert.SerializeObject(files, Formatting.Indented);

                    Debug.WriteLine($"{nameof(client.GetModelsAsync)}: Took {sw.Elapsed - last}");
                    last = sw.Elapsed;

                    RepetierPrinterConfig? config = await client.GetPrinterConfigAsync();
                    Assert.That(config, Is.Not.Null);
                    json = JsonConvert.SerializeObject(config, Formatting.Indented);

                    Debug.WriteLine($"{nameof(client.GetPrinterConfigAsync)}: Took {sw.Elapsed - last}");
                    last = sw.Elapsed;

                    RepetierProjectsServerListRespone? servers = await client.GetProjectsListServerAsync();
                    Assert.That(servers, Is.Not.Null);
                    json = JsonConvert.SerializeObject(servers, Formatting.Indented);

                    Debug.WriteLine($"{nameof(client.GetProjectsListServerAsync)}: Took {sw.Elapsed - last}");
                    last = sw.Elapsed;

                    ObservableCollection<RepetierProjectItem> projects = await client.GetProjectItemsAsync(servers?.Server?.FirstOrDefault()?.Uuid ?? Guid.Empty);
                    Assert.That(projects, Is.Not.Null);
                    json = JsonConvert.SerializeObject(projects, Formatting.Indented);

                    Debug.WriteLine($"{nameof(client.GetProjectItemsAsync)}: Took {sw.Elapsed - last}");
                    last = sw.Elapsed;

                    RepetierProjectsFolderRespone? folders = await client.GetProjectsGetFolderAsync(servers?.Server?.FirstOrDefault()?.Uuid ?? Guid.Empty);
                    Assert.That(folders, Is.Not.Null);
                    json = JsonConvert.SerializeObject(folders, Formatting.Indented);

                    Debug.WriteLine($"{nameof(client.GetProjectsGetFolderAsync)}: Took {sw.Elapsed - last}");
                    last = sw.Elapsed;

                    Dictionary<string, RepetierPrinterState>? state = await client.GetStatesAsync();
                    Assert.That(state, Is.Not.Null);
                    json = JsonConvert.SerializeObject(state, Formatting.Indented);

                    Debug.WriteLine($"{nameof(client.GetStatesAsync)}: Took {sw.Elapsed - last}");
                    last = sw.Elapsed;

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
                    Assert.That(files, Is.Not.Null);

                    IGcode? f = files.FirstOrDefault();
                    if (f is not null)
                    {
                        byte[]? file = await client.DownloadGcodeAsync(f.Identifier.ToString());
                        Assert.That(file, Is.Not.Empty);

                        byte[]? file2 = await client.DownloadGcodeAsync(f, Encoding.Default);
                        Assert.Multiple(() =>
                        {
                            Assert.That(file2, Is.Not.Empty);
                            Assert.That(file, Has.Length.EqualTo(file2?.Length));
                        });
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
