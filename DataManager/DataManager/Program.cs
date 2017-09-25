using RestEase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocket4Net;
using static DataManager.JsonRESTObjects;

namespace DataManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            GlobalVars.Running = true;
            //*
            WebsocketNetClient wsclient = new WebsocketNetClient();
            wsclient.Setup(GlobalVars.WsURL, null, WebSocketVersion.Rfc6455);
            //*/
            //Task.Run(() => AsyncSocketServer.Start());
                        
            /*Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());*/
        }
    }
}
