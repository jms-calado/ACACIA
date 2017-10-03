using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static DataCompiler.FileAnaliser;
using static DataCompiler.GlobalVars;

namespace DataCompiler
{    
    public class AsynchronousClient2
    {
        static ManualResetEvent resetEvent = new ManualResetEvent(false);
        // Client socket.  
        //public static Socket client;

        private static byte[] byteData = new byte[1024];
        
        public static string Sensor_Name = "Data_Compiler";
        
        // The response from the remote device.  
        private static String response = String.Empty;

        public static void StartClient(int port)
        {
            // Connect to a remote device.  
            try
            {
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Loopback, port);

                // Create a TCP/IP socket.  
                client = new Socket(remoteEP.Address.AddressFamily,//AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.  
                client.BeginConnect(remoteEP, new AsyncCallback(OnConnect), client);

                //Register sensor
                //Send(client, "{\"Ready\":{\"Sensor\":{\"name\":\"" + Sensor_Name + "\",\"status\":\"On\", }}}");
                //Send(client, "{\"Message\":{\"Affect\":{\"\":\"\", }}}");
                
                //FileAnaliser fileAnaliser = new FileAnaliser();
                //fileAnaliser.client = client;
                //fileAnaliser.FileWatcher();
                
                // Convert the string data to byte data using ASCII encoding.  
                //byteData = Encoding.ASCII.GetBytes(data);
                //client.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnSend), null);

                byteData = new byte[1024];
                //Start listening to the data asynchronously
                client.BeginReceive(byteData,
                                           0,
                                           byteData.Length,
                                           SocketFlags.None,
                                           new AsyncCallback(OnReceive),
                                           null);

                resetEvent.WaitOne();
                //while (GlobalVars.Running) { }
                /*{
                    // Send test data to the remote device.  
                    Send(client, "This is a test<EOF>");
                    sendDone.WaitOne();

                    // Receive the response from the remote device.  
                    Receive(client);
                    receiveDone.WaitOne();

                    // Write the response to the console.  
                    Console.WriteLine("Response received : {0}", response);
                }*/
                // Release the socket.  
                //client.Shutdown(SocketShutdown.Both);
                //client.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void OnConnect(IAsyncResult ar)
        {
            try
            {
                client.EndConnect(ar);
                
                //byte[] b = msgToSend.ToByte ();
                string message = "{\"Login\":{\"Sensor\":{\"name\":\"DataCompiler\",\"status\":\"On\", }}}";
                byte[] b = Encoding.ASCII.GetBytes(message);

                //Send the message to the server
                client.BeginSend(b, 0, b.Length, SocketFlags.None, new AsyncCallback(OnSend), null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private static void OnSend(IAsyncResult ar)
        {
            try
            {
                client.EndSend(ar);
            }
            catch (ObjectDisposedException)
            { }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private static void OnReceive(IAsyncResult ar)
        {
            try
            {
                client.EndReceive(ar);

                string msgReceived = Encoding.ASCII.GetString(byteData).TrimEnd('\0');
                if (msgReceived.Equals("Connected"))
                {
                }
                else if (msgReceived.Contains("Start"))
                {
                    FileAnaliser fileAnaliser = new FileAnaliser();
                    //fileAnaliser.client = client;
                    fileAnaliser.FileWatcher();
                }
                else if (msgReceived.Equals("Stop"))
                {
                    System.Windows.Forms.Application.Exit();
                }
                else if (msgReceived.Equals("Off"))
                {
                    //GlobalVars.Running = true;
                    resetEvent.Set();
                    System.Environment.Exit(1);
                }

                byteData = new byte[1024];

                client.BeginReceive(byteData,
                                          0,
                                          byteData.Length,
                                          SocketFlags.None,
                                          new AsyncCallback(OnReceive),
                                          null);

            }
            catch (ObjectDisposedException)
            { }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public static void Send(Socket client, string data)
        {
            byteData = new byte[1024];
            // Convert the string data to byte data using ASCII encoding.  
            byteData = Encoding.ASCII.GetBytes(data);
            client.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnSend), null);
        }
    }
}
