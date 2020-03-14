using System;
using Serilog;

namespace Neptunium.Game
{
    public class Manager
    {
        private string ManagerName { get; }

        protected Manager()
        {
            var taskStarted = DateTime.Now;

            ManagerName = GetType().Name;

            Load();

            Log.Information("[{0}] Loaded in {1}", ManagerName, DateTime.Now - taskStarted);
        }

        public void Shutdown()
        {
            var taskStarted = DateTime.Now;
            Unload();
            Log.Information("[{0}] Shut down in {1}", ManagerName, DateTime.Now - taskStarted);
        }

        protected virtual void Load()
        {
        }

        protected virtual void Unload()
        {
        }
    }
}