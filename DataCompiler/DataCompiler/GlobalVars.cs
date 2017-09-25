using System.Net.Sockets;

namespace DataCompiler
{
    public static class GlobalVars
    {
        public static bool Running { get; set; }
        // Client socket.  
        public static Socket client;
    }
}
