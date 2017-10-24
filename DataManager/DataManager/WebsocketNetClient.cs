using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocket4Net;
using static DataManager.JsonSocketObjects;
using static DataManager.GlobalVars;
using System.Diagnostics;
using System.Threading;

namespace DataManager
{
    class WebsocketNetClient
    {
        static ManualResetEvent resetEvent = new ManualResetEvent(false);
        static ManualResetEvent resumeEvent = new ManualResetEvent(false);
        private WebSocket websocketClient;
        private string url;
        private string protocol;
        private WebSocketVersion version;
        public class WSstate
        {
            public static bool connected = false;
        }
        public void Setup(string url, string protocol, WebSocketVersion version)
        {
            this.url = url;
            this.protocol = protocol;
            this.version = version;

            //websocketClient = new WebSocket(this.url, this.protocol, this.version);
            websocketClient = new WebSocket(this.url);

            websocketClient.Error           += new EventHandler<SuperSocket.ClientEngine.ErrorEventArgs>(WebsocketClient_Error);
            websocketClient.Opened          += new EventHandler(WebsocketClient_Opened);
            websocketClient.Closed          += new EventHandler(WebsocketClient_Closed);
            websocketClient.MessageReceived += new EventHandler<MessageReceivedEventArgs>(WebsocketClient_MessageReceived);
            Start();
        }
        public void Start()
        {
            websocketClient.Open();
            //wait for ws connection established
            resumeEvent.WaitOne();
            //while (WSstate.connected == false) { }
            //Start TCP socket
            Task.Run(() => AsyncSocketServer.Start());

            //wait for program termination
            resetEvent.WaitOne();
            //while (WSstate.connected == true) { }
        }
        public void Stop()
        {
            websocketClient.Close();
            websocketClient.Dispose();
            Console.WriteLine("Client disconnected!");
            resetEvent.Set(); // Allow the program to exit
        }
        public void Send(string message)
        {
            websocketClient.Send(message);
        }
        private void WebsocketClient_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("Client successfully connected.");         
            new LoginForm().ShowDialog();
            Sensor sensor = new Sensor()
            {
                action = "add",
                name = username,
                type = "Student",
                sensors = new string[] { "GP3", "Affectiva" },
                statusOnOff = "Off",
                statusStartStop = "Stop"
            };
            string message = JsonConvert.SerializeObject(sensor,
                                        Newtonsoft.Json.Formatting.None,
                                        new JsonSerializerSettings
                                        {
                                            NullValueHandling = NullValueHandling.Ignore,
                                            MissingMemberHandling = MissingMemberHandling.Ignore
                                        });
            websocketClient.Send(message);
            Console.WriteLine("Message Sent: " + message);
            resumeEvent.Set();
            //WSstate.connected = true;   
        }
        private void WebsocketClient_Closed(object sender, EventArgs e)
        {
            Console.WriteLine("Connection closed by the server.");
            try
            {
                //websocketClient.Close();
            }
            catch(Exception er)
            {
                Console.WriteLine(er.ToString());
            }
        }
        private void WebsocketClient_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Console.WriteLine("Message Received: " + e.Message);
            JObject jObject = new JObject();
            try
            {
                jObject = JObject.Parse(e.Message);
            }
            catch (JsonReaderException err)
            {
                Console.WriteLine("Failed parsing websocket json: {0}", err);
            }
            if ("onoff".Equals(jObject.SelectToken("action").ToString()))
            {
                if ("On".Equals(jObject.SelectToken("statusOnOff").ToString()))
                {
                    session = jObject.SelectToken("session").ToString();
                    scenario = jObject.SelectToken("scenario").ToString();
                    sample_rate = jObject.SelectToken("sample_rate").ToString();
                    sensory_components = jObject.SelectToken("sensory_components").ToObject<string[]>();

                    /*
                    foreach(string proc_name in sensory_components)
                    {
                        StartProc(proc_name);

                    }*/
                    //Process process = new Process();
                    //process.StartInfo.FileName = parentFolder + "\\2.bat";
                    //Console.WriteLine(parentFolder + "\\2.bat");
                    //process.StartInfo.WorkingDirectory
                    //process.Start();

                    Sensor sensor = new Sensor()
                    {
                        action = "toggleOnOff",
                        id = (int)jObject.SelectToken("id")
                    };
                    string message = JsonConvert.SerializeObject(sensor,
                                                Newtonsoft.Json.Formatting.None,
                                                new JsonSerializerSettings
                                                {
                                                    NullValueHandling = NullValueHandling.Ignore,
                                                    MissingMemberHandling = MissingMemberHandling.Ignore
                                                });
                    websocketClient.Send(message);
                    Console.WriteLine("Message Sent: " + message);
                }
                else if ("Off".Equals(jObject.SelectToken("statusOnOff").ToString()))
                {
                    AsyncSocketServer.SendCmd("Off");
                    
                    /*
                    foreach(string proc_name in sensory_components)
                    {
                        KillProc(proc_name);

                    }*/

                    Sensor sensor = new Sensor()
                    {
                        action = "toggleOnOff",
                        id = (int)jObject.SelectToken("id")
                    };
                    string message = JsonConvert.SerializeObject(sensor,
                                                Newtonsoft.Json.Formatting.None,
                                                new JsonSerializerSettings
                                                {
                                                    NullValueHandling = NullValueHandling.Ignore,
                                                    MissingMemberHandling = MissingMemberHandling.Ignore
                                                });
                    websocketClient.Send(message);
                    Console.WriteLine("Message Sent: " + message);
                }
            }
            else if ("startstop".Equals(jObject.SelectToken("action").ToString()))
            {
                if ("Start".Equals(jObject.SelectToken("statusStartStop").ToString()))
                {
                    AsyncSocketServer.SendCmd("Start:" + sample_rate);

                    Sensor sensor = new Sensor()
                    {
                        action = "toggleStartStop",
                        id = (int)jObject.SelectToken("id")
                    };
                    string message = JsonConvert.SerializeObject(sensor,
                                                Newtonsoft.Json.Formatting.None,
                                                new JsonSerializerSettings
                                                {
                                                    NullValueHandling = NullValueHandling.Ignore,
                                                    MissingMemberHandling = MissingMemberHandling.Ignore
                                                });
                    websocketClient.Send(message);
                    Console.WriteLine("Message Sent: " + message);
                }
                else if ("Stop".Equals(jObject.SelectToken("statusStartStop").ToString()))
                {
                    AsyncSocketServer.SendCmd("Stop");

                    Sensor sensor = new Sensor()
                    {
                        action = "toggleStartStop",
                        id = (int)jObject.SelectToken("id")
                    };
                    string message = JsonConvert.SerializeObject(sensor,
                                                Newtonsoft.Json.Formatting.None,
                                                new JsonSerializerSettings
                                                {
                                                    NullValueHandling = NullValueHandling.Ignore,
                                                    MissingMemberHandling = MissingMemberHandling.Ignore
                                                });
                    websocketClient.Send(message);
                    Console.WriteLine("Message Sent: " + message);
                }
            }
        }
        private void WebsocketClient_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            Console.WriteLine(e.Exception.GetType() + ": " + e.Exception.Message + Environment.NewLine + e.Exception.StackTrace);
            if (e.Exception.InnerException != null)
            {
                Console.WriteLine(e.Exception.InnerException.GetType());
            }
            return;
        }
        private bool StartProc(string proc_name)
        {
            if (!Process.GetProcesses().Any(p => p.ProcessName.Contains(proc_name)))
            {
                Process process = new Process();
                process.StartInfo.FileName = proc_name + ".exe";
                process.StartInfo.Arguments = "-p " + tcpSocketPort.ToString();
                process.EnableRaisingEvents = true;
                process.Start();
                process.Exited += (sd, ea) => { };
                return true;
            }
            else return false;
        }
        private bool KillProc(string proc_name)
        {
            if (Process.GetProcesses().Any(p => p.ProcessName.Contains(proc_name)))
            {
                try
                {
                    foreach (Process proc in Process.GetProcessesByName(proc_name))
                    {
                        proc.Kill();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                return false;
            }
            else return false;
        }
    }
}
