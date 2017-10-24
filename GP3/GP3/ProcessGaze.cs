using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;

namespace GP3
{
    class ProcessGaze
    {
        const int ServerPort = 4242;
        const string ServerAddr = "127.0.0.1";
        TcpClient gp3_client;
        NetworkStream data_feed;
        StreamWriter data_write;
        String incoming_data = "";

        Stopwatch stopwatch = new Stopwatch();

        bool exit_state = true;

        StreamWriter file = null;
        //string FilePath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName + "/Records/" + "GP3 " + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".xml";
        string FilePath = GlobalVars.WatcherFolder + "\\GP3 " + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".xml";
        
        public ProcessGaze()
        {
            Directory.CreateDirectory(GlobalVars.WatcherFolder);
            //Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Records/");
            Console.WriteLine("Ready.\r\nFile Creation Sample Rate: " + GlobalVars.sample_rate.ToString() + " miliseconds.");
                       
            //Process.Start("C:/Program Files (x86)/Gazepoint/Gazepoint/bin64/Gazepoint.exe");
            string proc_path = "C:/Program Files (x86)/Gazepoint/Gazepoint/bin64/";
            string proc_name = "Gazepoint";
            if (!Process.GetProcesses().Any(p => p.ProcessName.Contains(proc_name)))
            {
                Process process = new Process();
                process.StartInfo.FileName = proc_path + proc_name + ".exe";
                //process.StartInfo.Arguments = "-p " + tcpSocketPort.ToString();
                //process.EnableRaisingEvents = true;
                process.Start();
                //process.Exited += (sd, ea) => { };
            }
            //Wait for gazepoint control to start
            while (!Process.GetProcesses().Any(p => p.ProcessName.Contains(proc_name))) { }
            System.Threading.Thread.Sleep(5000);

            // Try to create client object, return if no server found
            try
            {
                gp3_client = new TcpClient(ServerAddr, ServerPort);
                Console.WriteLine("Connected to GazePoint Control");
            }
            catch (Exception error)
            {
                Console.WriteLine("Failed to connect with error: {0}", error);
                return;
            }
        }
        public void Disconnect()
        {
            data_write.Close();
            gp3_client.Close();
            Console.WriteLine("Disconnected");
            KillProc("Gazepoint");
        }
        private void ListenToTcp(CancellationTokenSource ct, IProgress<string> progress)
        {
            do
            {
                if (ct.IsCancellationRequested)
                    return;

                int ch = data_feed.ReadByte();
                if (ch != -1)
                {
                    incoming_data += (char)ch;

                    // find string terminator ("\r\n") 
                    if (incoming_data.IndexOf("\r\n") != -1)
                    {
                        // only process DATA RECORDS, ie <REC .... />
                        //if (incoming_data.IndexOf("<REC") != -1)
                        //{
                        if (progress != null)
                            progress.Report(incoming_data); //Report the tcp-data to form thread
                        incoming_data = string.Empty;
                        //}
                    }
                }
            }
            while (exit_state == false);
            //while (UseTimer == false && exit_state == false || UseTimer == true && stopwatch.ElapsedMilliseconds < time);
        }
        private void WriteFileTask()
        {
            file = new StreamWriter(FilePath, true);
            file.AutoFlush = true;
            file.WriteLine("<root>");
            stopwatch.Start();
            
            do
            {
                int ch = data_feed.ReadByte();
                if (ch != -1)
                {
                    incoming_data += (char)ch;

                    // find string terminator ("\r\n") 
                    if (incoming_data.IndexOf("\r\n") != -1)
                    {
                        // only process DATA RECORDS, ie <REC .... />
                        //if (incoming_data.IndexOf("<REC") != -1)
                        //{
                        if (exit_state == false)
                        {
                            //Console.WriteLine("value: " + value);
                            try
                            {
                                file.Write("  " + incoming_data);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                            if (stopwatch.ElapsedMilliseconds > GlobalVars.sample_rate)
                            {
                                file.WriteLine("</root>");
                                file.Dispose();//?
                                //FilePath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName + "\\Records\\" + "GP3 " + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".xml";
                                FilePath = GlobalVars.WatcherFolder + "\\GP3 " + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".xml";
                                Console.WriteLine("New file: " + FilePath);
                                file = new StreamWriter(FilePath, true);
                                file.AutoFlush = true;
                                file.WriteLine("<root>");
                                stopwatch.Restart();
                            }
                        }
                        incoming_data = string.Empty;
                        //}
                    }
                }
            }
            while (exit_state == false);

        }
        public void Start()
        {
            if (exit_state == true)
            {
                exit_state = false;

                Console.WriteLine("Recording");

                // Load the read and write streams
                data_feed = gp3_client.GetStream();
                data_write = new StreamWriter(data_feed);

                // Setup the data records
                #region data records setup
                data_write.Write("<GET ID=\"TIME_TICK_FREQUENCY\" />\r\n");
                data_write.Write("<SET ID=\"ENABLE_SEND_POG_FIX\" STATE=\"1\" />\r\n");
                data_write.Write("<SET ID=\"ENABLE_SEND_POG_LEFT\" STATE=\"1\" />\r\n");
                data_write.Write("<SET ID=\"ENABLE_SEND_POG_RIGHT\" STATE=\"1\" />\r\n");
                data_write.Write("<SET ID=\"ENABLE_SEND_POG_BEST\" STATE=\"1\" />\r\n");
                data_write.Write("<SET ID=\"ENABLE_SEND_PUPIL_LEFT\" STATE=\"1\" />\r\n");
                data_write.Write("<SET ID=\"ENABLE_SEND_PUPIL_RIGHT\" STATE=\"1\" />\r\n");
                data_write.Write("<SET ID=\"ENABLE_SEND_EYE_LEFT\" STATE=\"1\" />\r\n");
                data_write.Write("<SET ID=\"ENABLE_SEND_EYE_RIGHT\" STATE=\"1\" />\r\n");
                data_write.Write("<SET ID=\"ENABLE_SEND_BLINK\" STATE=\"1\" />\r\n");
                data_write.Write("<SET ID=\"ENABLE_SEND_CURSOR\" STATE=\"1\" />\r\n");
                data_write.Write("<SET ID=\"ENABLE_SEND_COUNTER\" STATE=\"1\" />\r\n");
                data_write.Write("<SET ID=\"ENABLE_SEND_TIME\" STATE=\"1\" />\r\n");
                data_write.Write("<SET ID=\"ENABLE_SEND_TIME_TICK\" STATE=\"1\" />\r\n");
                data_write.Write("<SET ID=\"ENABLE_SEND_USER_DATA\" STATE=\"1\" />\r\n");
                data_write.Write("<SET ID=\"TRACKER_DISPLAY\" STATE=\"0\" />\r\n");
                data_write.Write("<SET ID=\"ENABLE_SEND_DATA\" STATE=\"1\" />\r\n");
                #endregion data records setup

                // Flush the buffer out the socket
                data_write.Flush();

                stopwatch.Start();
                WriteFileTask();
                /*do
                {
                    if(stopwatch.ElapsedMilliseconds > GlobalVars.SampleRate)
                    {
                        ct.Cancel();
                        file.WriteLine("</root>");
                        //ct.Dispose();//?
                        stopwatch.Restart();
                        WriteFileTask();
                    }
                }
                while (exit_state == false);
                */
            }
        }
        public void Stop()
        {
            if (exit_state == false)
            {
                data_write.Write("<SET ID=\"ENABLE_SEND_DATA\" STATE=\"0\" />\r\n");
                //data_write.Close();
                //data_feed.Close();

                exit_state = true;

                Console.WriteLine("Recording Stopped");
                
                file.WriteLine("</root>");
                //file.Close();
            }
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
