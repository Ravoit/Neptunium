using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading;
using Serilog;

namespace Neptunium
{
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");

                Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location));

                Neptunium.Start();

                AppDomain.CurrentDomain.ProcessExit += (s, e) => Neptunium.Shutdown();
                AssemblyLoadContext.Default.Unloading += obj => Neptunium.Shutdown();
                Console.CancelKeyPress += (s, ev) => Neptunium.Shutdown();

                while (true)
                {
                    var input = Console.ReadLine();
                    switch (input)
                    {
                        default:
                            Log.Information("UNKNOWN COMMAND");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                {
                    throw;
                }

                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
        }
    }
}