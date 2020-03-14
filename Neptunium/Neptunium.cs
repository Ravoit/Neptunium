using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Neptunium.Game.Bots;
using Neptunium.Messages;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Telegram;

namespace Neptunium
{
    public static class Neptunium
    {
        private const string Build = "Neptunium (Build 001)";
        private static DateTime _serverStarted;
        private static bool _working;

        public static PacketManager PacketManager;
        public static BotManager BotManager;

        public static void Start()
        {
            _serverStarted = DateTime.Now;

            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File("logs/.txt", rollingInterval: RollingInterval.Day);

            if (File.Exists("telegram.txt"))
            {
                var lines = File.ReadAllLines("telegram.txt");
                loggerConfiguration.WriteTo.Telegram(lines[0], lines[1],
                    restrictedToMinimumLevel: LogEventLevel.Information);
            }

            Log.Logger = loggerConfiguration.CreateLogger();

            PacketManager = new PacketManager();
            BotManager = new BotManager();

            _working = true;

            new Task(StatusUpdate).Start();

            Log.Information("[{0}] Loaded in {1}", "SERVER", DateTime.Now - _serverStarted);

            Console.Beep();

            BotManager.StartBots();
        }


        public static void Shutdown()
        {
            if (!_working) return;

            Log.Warning("Shutting down");

            _working = false;

            BotManager.Shutdown();
            PacketManager.Shutdown();

            Log.Information("Uninitialized successfully. Closing.");

            Environment.Exit(0);
        }

        private static void StatusUpdate()
        {
            static string PrettifyByte(long allocatedMemory)
            {
                string[] sizes = {"B", "KB", "MB", "GB", "TB"};

                var order = 0;
                while (allocatedMemory >= 1024 && order < sizes.Length - 1)
                {
                    order++;
                    allocatedMemory /= 1024;
                }

                return $"{allocatedMemory:0.##} {sizes[order]}";
            }

            const int period = 1;
            var currentProcess = Process.GetCurrentProcess();

            while (_working)
            {
                var startTime = DateTime.UtcNow;
                var startCpuUsage = currentProcess.TotalProcessorTime;

                Thread.Sleep(period * 1000);

                var endTime = DateTime.UtcNow;
                var endCpuUsage = currentProcess.TotalProcessorTime;

                var cpuUsageTotal = (endCpuUsage - startCpuUsage).TotalMilliseconds /
                    ((endTime - startTime).TotalMilliseconds * Environment.ProcessorCount) * 100;

                var uptime = DateTime.Now - _serverStarted;

                Console.Title =
                    $"{Build} | RAM: {PrettifyByte(currentProcess.PrivateMemorySize64)} | CPU: {cpuUsageTotal:0.##}% | Bots: {BotManager.Bots.Count} | Uptime: {uptime.Days}d {uptime.Hours}h {uptime.Minutes}m {uptime.Seconds}s";
            }
        }
    }
}