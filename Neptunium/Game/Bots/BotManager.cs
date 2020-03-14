using System.Collections.Generic;
using System.IO;
using System.Linq;
using Serilog;

namespace Neptunium.Game.Bots
{
    public class BotManager : Manager
    {
        public readonly List<Bot> Bots = new List<Bot>();

        protected override void Load()
        {
            if (!File.Exists("bots.txt"))
            {
                Log.Error("NO bots.txt");
                return;
            }

            foreach (var line in File.ReadAllLines("bots.txt"))
            {
                var split = line.Split(':');
                Bots.Add(new Bot {Login = split[0], Password = split[1]});
            }
        }

        protected override void Unload()
        {
            base.Unload();
        }

        public void StartBots()
        {
            foreach (Bot bot in Bots)
            {
                bot.Start();
            }
        }

        public Bot GetBot(string login)
        {
            return Bots.FirstOrDefault(bot => bot.Login == login);
        }
    }
}