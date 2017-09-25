using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataCompiler
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            int port = 11000;
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Ignoring arguments (no arguments/incorrect arguments)");
            }
            else
            {
                if (args[0] == "-port" || args[0] == "-p")
                {
                    if (args[1] != String.Empty)
                    {
                        try
                        {
                            port = Convert.ToInt32(args[1]);
                            Console.WriteLine("Program Loaded with arguments: " + args[0] + " " + args[1]);
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine("Input string is not a sequence of digits: {0}", e);
                        }
                        catch (OverflowException e)
                        {
                            Console.WriteLine("The number cannot fit in an Int32: {0}", e);
                        }
                    }
                    else
                    {
                        Console.WriteLine("2nd argument is missing");
                    }
                }
            }

            GlobalVars.Running = true;
            //Task.Run(() => AsynchronousClient2.StartClient(port));
            AsynchronousClient2.StartClient(port);
            /*Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());*/
        }
    }
}
