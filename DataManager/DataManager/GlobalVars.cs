using System.IO;
using System.Reflection;

namespace DataManager
{
    public static class GlobalVars
    {
        public static bool Running { get; set; }
        public static string WsURL { get => wsURL; set => wsURL = value; }
        public static string RestURL { get => restURL; set => restURL = value; }

        private static DirectoryInfo execFolder = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
#if DEBUG
        //private static string restURL = "http://localhost:5904";
        //private static string wsURL   = "ws://localhost:5904/actions";
        private static string restURL = "https://arca.acacia.red";
        private static string wsURL = "ws://arca.acacia.red:5904/actions";

        public static string parentFolder = execFolder.Parent.Parent.Parent.Parent.FullName.ToString();
#else
        private static string restURL = "https://arca.acacia.red";
        private static string wsURL   = "ws://arca.acacia.red:5904/actions";
        
        private static string parentFolder = execFolder.Parent.FullName.ToString();
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
