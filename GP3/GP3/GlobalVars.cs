using System.IO;
using System.Reflection;

namespace GP3
{
    public static class GlobalVars
    {
        public static bool Running { get; set; }
        public static int sample_rate { get; set; }

        private static DirectoryInfo execFolder = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
#if DEBUG
        public static string WatcherFolder = execFolder.Parent.Parent.Parent.Parent.FullName.ToString() + "\\Records";
#else   
        public static string WatcherFolder = execFolder.Parent.FullName.ToString() + "\\Records";
#endif

    }
}
