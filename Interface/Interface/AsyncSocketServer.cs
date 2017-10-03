using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestEase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using WebSocket4Net;
using static Interface.JsonRESTObjects;
using static Interface.JsonSocketObjects;
using static Interface.GlobalVars;

namespace Interface
{
    public partial class AsyncSocketServer
    {
        //The ClientInfo structure holds the required information about every
        //client connected to the server
        struct ClientInfo
        {
            public Socket socket;   //Socket of the client
            public string sensor;  //Name by which the user logged into the chat room
        }

        //The collection of all clients logged into the room (an array of type ClientInfo)
        public static ArrayList clientList;

        //The main socket on which the server listens to the clients
        public static Socket serverSocket;

        public static byte[] byteData = new byte[1024];
        
        public static IAcaciaApi api;
        public static void Start()
        {            
            // Creates an implementation of the Rest interface
            api = RestClient.For<IAcaciaApi>(restURL);

            clientList  = new ArrayList();
            try
            {
                //We are using TCP sockets
                serverSocket = new Socket(AddressFamily.InterNetwork,
                                          SocketType.Stream,
                                          ProtocolType.Tcp);

                //Assign the any IP of the machine and listen on an unassigned port (0)
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Loopback, 11000);
                tcpSocketPort = ipEndPoint.Port;

                //Bind and listen on the given address
                serverSocket.Bind(ipEndPoint);
                serverSocket.Listen(4);

                //Accept the incoming clients
                serverSocket.BeginAccept(new AsyncCallback(OnAccept), null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private static void OnAccept(IAsyncResult ar)
        {
            try
            {
                Socket clientSocket = serverSocket.EndAccept(ar);

                //Start listening for more clients
                serverSocket.BeginAccept(new AsyncCallback(OnAccept), null);

                //Once the client connects then start receiving the commands from her
                clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None,
                    new AsyncCallback(OnReceive), clientSocket);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private static void OnReceive(IAsyncResult ar)
        {
            try
            {
                Socket clientSocket = (Socket)ar.AsyncState;
                clientSocket.EndReceive(ar);

                string msgReceived = Encoding.ASCII.GetString(byteData).TrimEnd('\0');
                byteData = new byte[1024];

                JObject jObject = JObject.Parse(msgReceived);
                if (jObject["Login"] != null)
                {
                    //jObject.SelectTokens("Login").
                    Sensor sensor = jObject["Login"]["Sensor"].ToObject<Sensor>();
                    ClientInfo clientInfo = new ClientInfo()
                    {
                        socket = clientSocket,
                        sensor = sensor.name
                    };

                    clientList.Add(clientInfo);
                    
                    byte[] message = Encoding.ASCII.GetBytes("Connected");
                    clientSocket.BeginSend(message, 0, message.Length, SocketFlags.None,
                            new AsyncCallback(OnSend), clientSocket);
                }
                else if (jObject["Logout"] != null)
                {
                    //When a user wants to log out of the server then we search for her 
                    //in the list of clients and close the corresponding connection

                    int nIndex = 0;
                    foreach (ClientInfo client in clientList)
                    {
                        if (client.socket == clientSocket)
                        {
                            clientList.RemoveAt(nIndex);
                            break;
                        }
                        ++nIndex;
                    }
                    clientSocket.Close();
                }
                else if (jObject["Message"] != null)
                {
                    JObject restMessage = null;
                    string observation_ID = string.Empty;
                    if ( jObject["Message"]["Observation"] != null)
                    {
                        restMessage = (JObject)jObject.SelectToken("Message.Observation");
                        ObservationObject observationObject = restMessage.ToObject<ObservationObject>();
                        observationObject.Scenario = scenario;
                        observationObject.Session = session;
                        observationObject.Student = username;

                        var response = api.PostObservation(observationObject).Result;
                        if (response.ResponseMessage.StatusCode == HttpStatusCode.Created)
                        {
                            var responseObject = response.GetContent();
                            observation_ID = responseObject.Observation_ID;
                        }
                        else //deal with http status error
                        {
                            if(response.ResponseMessage.StatusCode == (HttpStatusCode)422)
                            {
                                Console.WriteLine("I fucked up!");
                            }
                        }
                    }
                    if (jObject["Message"]["Affect"] != null)
                    {
                        restMessage = (JObject)jObject.SelectToken("Message.Affect");
                        AffectObject affectObject = restMessage.ToObject<AffectObject>();
                        affectObject.ObservationID = observation_ID;
                        string message = JsonConvert.SerializeObject(affectObject,
                                                    Newtonsoft.Json.Formatting.None,
                                                    new JsonSerializerSettings
                                                    {
                                                        NullValueHandling = NullValueHandling.Ignore,
                                                        MissingMemberHandling = MissingMemberHandling.Ignore
                                                    });

                        var response = api.PostAffect(message).Result;
                        if (response.StatusCode == HttpStatusCode.Created)
                        {
                            var responseObject = response.Content;
                        }
                        else //deal with http status error
                        {

                        }
                    }
                    else if (jObject["Message"]["Behaviour"] != null)
                    {
                        restMessage = (JObject)jObject.SelectToken("Message.Behaviour");
                        BehaviourObject behaviourObject = restMessage.ToObject<BehaviourObject>();
                        behaviourObject.ObservationID = observation_ID;
                        string message = JsonConvert.SerializeObject(behaviourObject,
                                                    Newtonsoft.Json.Formatting.None,
                                                    new JsonSerializerSettings
                                                    {
                                                        NullValueHandling = NullValueHandling.Ignore,
                                                        MissingMemberHandling = MissingMemberHandling.Ignore
                                                    });

                        var response = api.PostAffect(message).Result;
                        if (response.StatusCode == HttpStatusCode.Created)
                        {
                            var responseObject = response.Content;
                        }
                        else //deal with http status error
                        {

                        }
                    }
                    else if (jObject["Message"]["Emotion"] != null)
                    {
                        restMessage = (JObject)jObject.SelectToken("Message.Emotion");
                        EmotionObject emotionObject = restMessage.ToObject<EmotionObject>();
                        emotionObject.ObservationID = observation_ID;
                        string message = JsonConvert.SerializeObject(emotionObject,
                                                    Newtonsoft.Json.Formatting.None,
                                                    new JsonSerializerSettings
                                                    {
                                                        NullValueHandling = NullValueHandling.Ignore,
                                                        MissingMemberHandling = MissingMemberHandling.Ignore
                                                    });

                        var response = api.PostEmotion(message).Result;
                        if (response.StatusCode == HttpStatusCode.Created)
                        {
                            var responseObject = response.Content;
                        }
                        else //deal with http status error
                        {

                        }
                    }
                }
                //Start listening to the message send by the user
                clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), clientSocket);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public static void OnSend(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndSend(ar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public static void SendCmd(string cmd)
        {
            foreach(ClientInfo clientInfo in clientList.ToArray())
            {
                byte[] byteData = Encoding.ASCII.GetBytes(cmd);
                try
                {
                    clientInfo.socket.BeginSend(byteData, 0, byteData.Length, SocketFlags.None,
                            new AsyncCallback(OnSend), clientInfo.socket);
                }
                catch (Exception e)
                {
                    clientList.Remove(clientInfo);
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }
}
