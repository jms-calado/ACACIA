using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocket4Net;
using static Interface.JsonSocketObjects;
using static Interface.GlobalVars;
using System.Diagnostics;
using System.Threading;

namespace Interface
{
    class WebsocketNetClient
    {
        static ManualResetEvent resetEvent = new ManualResetEvent(false);
        static ManualResetEvent resumeEvent = new ManualResetEvent(false);
        private WebSocket websocketClient;
        private string url;
        private string protocol;
        private WebSocketVersion version;
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
            resumeEvent.Set();
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

            JObject jObject = JObject.Parse(e.Message);
            if ("toggleOnOff".Equals(jObject.SelectToken("action").ToString()))
            {
                if ("On".Equals(jObject.SelectToken("statusOnOff").ToString()))
                {
                    session = jObject.SelectToken("session").ToString();
                    scenario = jObject.SelectToken("scenario").ToString();
                    sample_rate = jObject.SelectToken("sample_rate").ToString();
                    sensory_components = jObject.SelectToken("sensory_components").ToObject<string[]>();
                    
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
            else if ("toggleStartStop".Equals(jObject.SelectToken("action").ToString()))
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
    }
}
