using System.IO;
using System.Net.Sockets;
using System.Reflection;

namespace DataCompiler
{
    public static class GlobalVars
    {
        //public static bool Running { get; set; }
        // Client socket.  
        public static Socket client;

        private static DirectoryInfo execFolder = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));

#if DEBUG
        //public static StreamWriter report = new StreamWriter(Directory.GetParent(Directory.GetCurrentDirectory()).FullName + "\\Reports\\Report.txt", true);
        public static string filepath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName + "\\Reports\\Report.txt";

        public static string WatcherFolder = execFolder.Parent.Parent.Parent.Parent.FullName.ToString() + "\\Records";
        //public static string WatcherFolder = Directory.GetParent(Directory.GetCurrentDirectory()).FullName + "\\Records";
        //public static string WatcherFolder = @"C:\Users\Admin\Dropbox\ano3s1\TESE\Aplicação GIT\ACACIA\Records";
        //public static string selected_file = string.Empty;
#else   
        public static string WatcherFolder = execFolder.Parent.FullName.ToString() + "\\Records";
#endif
    }
}
