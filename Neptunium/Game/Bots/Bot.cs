using System.Collections.Generic;
using Neptunium.Messages.Outgoing.Game;
using Neptunium.Messages.Outgoing.Main;
using Neptunium.Network;
using Serilog;

namespace Neptunium.Game.Bots
{
    public class Bot
    {
        private int _serverId;
        public List<string> GameServers { get; } = new List<string>();
        public Connection GameConnection { get; set; }
        public string ChatServer { get; set; }
        public Connection ChatConnection { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Key { get; set; }
        public string Ses { get; set; }

        public void Start()
        {
            Log.Information("Starting {0}", Login);
            ConnectToMain();
        }

        public void ConnectToMain()
        {
            var mainConnection = new Connection(this, "main");
            mainConnection.SendData(new ListPacketOut().AddL(Login));
        }

        public void ConnectToGame()
        {
            if (GameServers.Count == _serverId) return;

            GameConnection = new Connection(this, GameServers[_serverId]);
            _serverId++;
        }

        public void ConnectToChat()
        {
            ChatConnection = new Connection(this, ChatServer);
            ChatConnection.SendData(new Messages.Outgoing.Chat.ChatPacketOut().AddSes(this));
        }

        public void TryLogin()
        {
            GameConnection.SendData(new LoginPacketOut().AddLogin(this));
        }
    }
}