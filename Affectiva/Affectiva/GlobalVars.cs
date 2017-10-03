using System.IO;
using System.Reflection;

namespace Affectiva
{
    internal class GlobalVars
    {
        public static int SampleRate { get; set; }
        public static bool Running { get; set; }

        private static DirectoryInfo execFolder = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
#if DEBUG
        public static string WatcherFolder = execFolder.Parent.Parent.Parent.Parent.FullName.ToString() + "\\Records";
#else   
        public static string WatcherFolder = execFolder.Parent.FullName.ToString() + "\\Records";
#endif

    }
}