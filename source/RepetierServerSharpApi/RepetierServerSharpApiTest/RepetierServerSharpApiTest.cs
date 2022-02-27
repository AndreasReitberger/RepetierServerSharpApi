using AndreasReitberger;
using AndreasReitberger.Models;
using AndreasReitberger.Core.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.Linq;
using AndreasReitberger.Enum;

namespace RepetierServerSharpApiTest
{
    [TestClass]
    public class RepetierServerSharpApiTest
    {

        private readonly string _host = "192.168.10.112";
        private readonly int _port = 3344;
        private readonly string _api = "1437e240-0314-4bfe-a7ed-f4f58c341ff1";
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

                RepetierServerPro.Instance = new RepetierServerPro(_host, _api, _port, _ssl)
                {
                    FreeDiskSpace = 1523165212,
                    TotalDiskSpace = 65621361616161,
                };
                RepetierServerPro.Instance.SetProxy(true, "https://testproxy.de", 447, "User", SecureStringHelper.ConvertToSecureString("my_awesome_pwd"), true);

                var serializedString = JsonSerializer.Serialize(RepetierServerPro.Instance);
                var serializedObject = JsonSerializer.Deserialize<RepetierServerPro>(serializedString);
                Assert.IsTrue(serializedObject is RepetierServerPro server && server != null);

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
                var xmlSerializer = new XmlSerializer(typeof(RepetierServerPro));
                using (var fileStream = new FileStream(serverConfig, FileMode.Create))
                {
                    RepetierServerPro.Instance = new RepetierServerPro(_host, _api, _port, _ssl)
                    {
                        ActiveExtruder = 1,
                        AvailableDiskSpace = 1523152132,
                        FreeDiskSpace = 1523165212,
                        TotalDiskSpace = 65621361616161,
                        IsDualExtruder = true,
                    };
                    RepetierServerPro.Instance.SetProxy(true, "https://testproxy.de", 447, "User", SecureStringHelper.ConvertToSecureString("my_awesome_pwd"), true);

                    xmlSerializer.Serialize(fileStream, RepetierServerPro.Instance);
                    Assert.IsTrue(File.Exists(Path.Combine(dir, "server.xml")));
                }

                xmlSerializer = new XmlSerializer(typeof(RepetierServerPro));
                using (var fileStream = new FileStream(serverConfig, FileMode.Open))
                {
                    var instance = (RepetierServerPro)xmlSerializer.Deserialize(fileStream);
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
                        new() { DynamicUrl = new("https://some.url.de/"), Pos = 0, Orientation = 90 },
                        new() { DynamicUrl = new("https://some.url.de/"), Pos = 1, Orientation = 180 },
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
                RepetierServerPro _server = new(_host, _api, _port, _ssl);
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
                RepetierServerPro _server = new(_host, _port, _ssl);
                await _server.CheckOnlineAsync();
                if (_server.IsOnline)
                {
                    bool succeed = false;
                    // Wait 5 minutes
                    CancellationTokenSource cts = new(new TimeSpan(0, 5, 0));
                    _server.LoginResultReceived += ((sender, args) =>
                    {
                        Assert.IsTrue(args.LoginSucceeded);
                        succeed = true;
                        cts.Cancel();
                    });
                    _server.StartListening();
                    // Wait till session is esstablished
                    while (_server.Session == null && !cts.IsCancellationRequested)
                    {
                        await Task.Delay(250);
                    }
                    if (_server.ActivePrinter == null)
                        await _server.SetPrinterActiveAsync(0, true);
                    _server.Login("testuser", SecureStringHelper.ConvertToSecureString("testpassword"), _server.SessionId);
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
                RepetierServerPro _server = new(_host, _api, _port, _ssl);
                _server.Error += (o, e) =>
                {
                    Assert.Fail(e.ToString());
                };
                await _server.CheckOnlineAsync();
                if (_server.IsOnline)
                {
                    if (_server.ActivePrinter == null)
                        await _server.SetPrinterActiveAsync(0, true);

                    ObservableCollection<RepetierPrinter> printers = await _server.GetPrintersAsync();
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
                RepetierServerPro _server = new(_host, _api, _port, _ssl);
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

                    ObservableCollection<string> modelgroups = await _server.GetModelGroupsAsync();
                    Assert.IsTrue(modelgroups != null && modelgroups.Count > 0);

                    await _server.RefreshModelGroupsAsync();
                    Assert.IsTrue(_server.ModelGroups?.Count > 0);
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
                RepetierServerPro _server = new(_host, _api, _port, _ssl);
                _server.Error += (o, e) =>
                {
                    Assert.Fail(e.ToString());
                };
                await _server.CheckOnlineAsync();
                if (_server.IsOnline)
                {
                    if (_server.ActivePrinter == null)
                        await _server.SetPrinterActiveAsync(0, true);

                    ObservableCollection<RepetierModel> models = await _server.GetModelsAsync();
                    Assert.IsTrue(models?.Count > 0);

                    // Try to fetch models from a second printer, which is not set active at the moment
                    string secondPrinter = "Prusa_i3_MK3S1";
                    ObservableCollection<RepetierModel> modelsSecondPrinter = await _server.GetModelsAsync(secondPrinter);
                    Assert.IsTrue(modelsSecondPrinter?.Count > 0 && models.Count != modelsSecondPrinter.Count);
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
                RepetierServerPro _server = new(_host, _api, _port, _ssl);
                _server.Error += (sender, e) =>
                {
                    Assert.Fail(e.ToString());
                };
                await _server.CheckOnlineAsync();
                if (_server.IsOnline)
                {
                    if (_server.ActivePrinter == null)
                        await _server.SetPrinterActiveAsync(-1, true);

                    ObservableCollection<RepetierJobListItem> jobs = await _server.GetJobListAsync();
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
        public async Task PrintModelest()
        {
            try
            {
                RepetierServerPro _server = new(_host, _api, _port, _ssl);
                await _server.CheckOnlineAsync();
                if (_server.IsOnline)
                {
                    if (_server.ActivePrinter == null)
                        await _server.SetPrinterActiveAsync(-1, true);

                    ObservableCollection<RepetierModel> models = await _server.GetModelsAsync();
                    if (models?.Count > 0)
                    {
                        bool printed = await _server.CopyModelToPrintQueueAsync(model: models[0], startPrintIfPossible: true);
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
                RepetierServerPro _server = new(_host, _api, _port, _ssl);
                _server.Error += (sender, e) =>
                {
                    Assert.Fail(e.ToString());
                };
                await _server.CheckOnlineAsync();
                if (_server.IsOnline)
                {
                    await _server.SetPrinterActiveAsync();
                    ObservableCollection<RepetierHistorySummaryItem> history = await _server.GetHistorySummaryItemsAsync("", 2021, true);
                    Assert.IsTrue(history?.Any());

                    ObservableCollection<RepetierHistoryListItem> list = await _server.GetHistoryListAsync("", "", 50, 0, 0, true);
                    Assert.IsTrue(list?.Any());

                    RepetierHistoryListItem historyItem = list.FirstOrDefault();
                    Assert.IsNotNull(historyItem);

                    byte[] report = await RepetierServerPro.Instance.GetHistoryReportAsync(historyItem.Id);
                    Assert.IsTrue(report.Length > 0);
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
                RepetierServerPro _server = new(_host, _api, _port, _ssl);
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
                RepetierServerPro _server = new(_host, _api, _port, _ssl);
                _server.Error += (o, e) =>
                {
                    Assert.Fail(e.ToString());
                };
                await _server.CheckOnlineAsync();
                if (_server.IsOnline)
                {
                    await _server.SetPrinterActiveAsync(1);
                    ObservableCollection<RepetierHistoryListItem> report = await _server.GetHistoryListAsync(_server.ActivePrinter.Slug);
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
                RepetierServerPro _server = new(_host, _api, _port, _ssl);
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
                RepetierServerPro _server = new(_host, _api, _port, _ssl);
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
                var host = "192.168.10.112";
                RepetierServerPro _server = new(host, _api, _port, _ssl);
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
                        var info = _server.ActivePrintInfo;
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
                string host = "192.168.10.113";
                string api = "671a482d-2879-4a11-a68d-170883c1ba25";

                RepetierServerPro _server = new(host, api, _port, _ssl);
                await _server.CheckOnlineAsync();
                Assert.IsTrue(_server.IsOnline);

                await _server.SetPrinterActiveAsync();

                _server.Error += (o, args) =>
                {
                    Assert.Fail(args.ToString());
                };

                RepetierWebcamType type = RepetierWebcamType.Dynamic;
                string webcamUriDynamic = await _server.GetWebCamUriAsync(0, type);
                
                type = RepetierWebcamType.Static;              
                webcamUriDynamic = await _server.GetWebCamUriAsync(0, type);

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
                Dictionary<DateTime, string> websocketMessages = new();
                RepetierServerPro _server = new(_host, _api, _port, _ssl);
                await _server.SetPrinterActiveAsync(1);
                _server.StartListening();

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
                        Debug.WriteLine($"WebSocket Data: {args.Message} (Total: {websocketMessages.Count})");
                    }
                };

                _server.WebSocketError += (o, args) =>
                {
                    Assert.Fail($"Websocket closed due to an error: {args}");
                };

                // Wait 10 minutes
                CancellationTokenSource cts = new(new TimeSpan(0, 10, 0));
                _server.WebSocketDisconnected += (o, args) =>
                {
                    if (!cts.IsCancellationRequested)
                        Assert.Fail($"Websocket unexpectly closed: {args}");
                };

                do
                {
                    await Task.Delay(10000);
                    await _server.CheckOnlineAsync();
                } while (_server.IsOnline && !cts.IsCancellationRequested);
                _server.StopListening();


                Assert.IsTrue(cts.IsCancellationRequested);
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
                RepetierServerPro _server = new(_host, _api, _port, _ssl);
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
                        double temp = 0;
                        // Wait till temp rises
                        while (temp < 23)
                        {
                            var state = await _server.GetStateObjectAsync();
                            if (state != null && state.Printer != null)
                            {
                                var beds = state.Printer.HeatedBeds;
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
                                var state = await _server.GetStateObjectAsync();
                                if (state != null && state.Printer != null)
                                {
                                    var beds = state.Printer.HeatedBeds;
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
                RepetierServerPro _server = new(_host, _api, _port, _ssl);
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
                        double extruderTemp = 0;
                        // Wait till temp rises
                        while (extruderTemp < 28)
                        {
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
                                var state = await _server.GetStateObjectAsync();
                                if (state != null && state.Printer != null)
                                {
                                    var extruders = state.Printer.Extruder;
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

    }
}
