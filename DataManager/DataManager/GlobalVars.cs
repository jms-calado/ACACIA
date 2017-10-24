using System.IO;
using System.Reflection;

namespace DataManager
{
    public static class GlobalVars
    {
        private static DirectoryInfo execFolder = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
#if DEBUG
        //public static string RestURL = "http://localhost:5904";
        //public static string WsURL   = "ws://localhost:5904/actions";
        public static string RestURL = "https://api.arca.acacia.red";
        public static string WsURL = "wss://api.arca.acacia.red/actions";

        public static string parentFolder = execFolder.Parent.Parent.Parent.Parent.FullName.ToString();
#else
        public static string RestURL = "https://api.arca.acacia.red";
        public static string WsURL   = "wss://api.arca.acacia.red/actions";
        
        public static string parentFolder = execFolder.Parent.FullName.ToString();
#endif

        public static int tcpSocketPort { get; set; }

        public static string username { get; set; }
        public static string password { get; set; }

        public static string session { get; set; }
        public static string scenario { get; set; }
        public static string sample_rate { get; set; }
        public static string[] sensory_components { get; set; }
    }
}
