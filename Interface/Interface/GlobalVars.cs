using System.IO;
using System.Reflection;

namespace Interface
{
    public static class GlobalVars
    {
        public static bool Running { get; set; }

        public static DirectoryInfo execFolder = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
#if DEBUG
        //public static string restURL = "http://localhost:5904";
        //public static string wsURL   = "ws://localhost:5904/actions";
        public static string restURL = "https://api.arca.acacia.red";
        public static string wsURL = "wss://api.arca.acacia.red/actions";

        public static string parentFolder = execFolder.Parent.Parent.Parent.Parent.FullName.ToString();
#else
        public static string restURL = "https://arca.acacia.red";
        public static string wsURL   = "wss://arca.acacia.red/actions";
        
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
