using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using WebSocket4Net;
using static DataManager.JsonSocketObjects;

namespace DataManager
{
    // https://docs.microsoft.com/en-us/dotnet/framework/network-programming/asynchronous-server-socket-example
    // State object for reading client data asynchronously  
    public class StateObject
    {
        // Client  socket.  
        public Socket workSocket = null;
        // Size of receive buffer.  
        public const int BufferSize = 1024;
        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];
        // Received data string.  
        public StringBuilder sb = new StringBuilder();
    }
    
    public class AsynchronousSocketListener
    {
        // ManualResetEvent instances signal completion. 
        private static ManualResetEvent allDone = 
            new ManualResetEvent(false);

        //The ClientInfo structure holds the required information about every
        //client connected to the server
        struct ClientInfo
        {
            public Socket socket;   //Socket of the client
            public string sensor;  //Name by which the user logged into the chat room
        }
        //The collection of all clients logged into the room (an array of type ClientInfo)
        public static ArrayList clientList;

        public static WebSocket websocketClient;
        public static void StartListening(WebSocket websocket)
        {
            websocketClient = websocket;
            clientList = new ArrayList();
            // Data buffer for incoming data.  
            byte[] bytes = new Byte[1024];
            /*
            // Establish the local endpoint for the socket. 
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = Array.FindLast(
                Dns.GetHostEntry(string.Empty).AddressList,
                a => a.AddressFamily == AddressFamily.InterNetwork);
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
            */
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Loopback, 11000);
            Console.Out.WriteLine("Assigned port: {0}",localEndPoint.Port);

            // Create the TCP/IP socket.  
            Socket listener = new Socket(localEndPoint.Address.AddressFamily,//AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (GlobalVars.Running)
                {
                    // Set the event to nonsignaled state.  
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.                    
                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // Wait until a connection is made before continuing.  
                    allDone.WaitOne();
                    
                }

                // Release the socket.  
                //listener.Shutdown(SocketShutdown.Both);
                //listener.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            //Console.WriteLine("\nPress ENTER to continue...");
            //Console.Read();            
        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            // Get the socket that handles the client request.  
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Signal the main thread to continue.  
            allDone.Set();

            //Start listening for more clients
            listener.BeginAccept(new AsyncCallback(AcceptCallback), null);

            // Create the state object.  
            StateObject state = new StateObject()
            {
                workSocket = handler
            };
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        public static void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;
            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket.   
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.  
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read   
                // more data.  
                content = state.sb.ToString();
                //if (content.IndexOf("<EOF>") > -1)
                if (state.sb.Length > 1)
                {
                    // All the data has been read from the   
                    // client. Display it on the console. 
                    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                        content.Length, content);

                    JObject jObject = JObject.Parse(content);
                    if (jObject["Ready"] != null)
                    {
                        //jObject.SelectTokens("Ready").
                        Sensor sensor = jObject["Ready"]["Sensor"].ToObject<Sensor>();
                        ClientInfo clientInfo = new ClientInfo()
                        {
                            socket = handler,
                            sensor = sensor.name
                        };
                    }
                    else if (jObject["Message"] != null)
                    {
                        JObject wsmessage = null;
                        if (jObject["Message"]["Affect"] != null)
                        {
                            wsmessage = (JObject)jObject.SelectToken("Message.Affect");
                        }
                        else if (jObject["Message"]["Behaviour"] != null)
                        {
                            wsmessage = (JObject)jObject.SelectToken("Message.Behaviour");
                        }
                        else if (jObject["Message"]["Emotion"] != null)
                        {
                            wsmessage = (JObject)jObject.SelectToken("Message.Emotion");
                        }
                        //websocketClient.Send(wsmessage.ToString());
                    }
                    

                    // Echo the data back to the client.  
                    Send(handler, content);
                }
                else
                {
                    // Not all data received. Get more.  
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }
            }
        }

        private static void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                // Signal that all bytes have been sent.  
                //sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }
}
