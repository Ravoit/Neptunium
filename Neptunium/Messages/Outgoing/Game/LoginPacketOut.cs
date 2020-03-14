using Neptunium.Game.Bots;

namespace Neptunium.Messages.Outgoing.Game
{
    internal class LoginPacketOut : PacketOut
    {
        public LoginPacketOut() : base("LOGIN")
        {
        }

        public LoginPacketOut AddLogin(Bot bot)
        {
            AddAttribute("v3", "127.0.0.1");
            AddAttribute("lang", "ru");
            AddAttribute("v2", "7.0.1 (7.1.2.6)");
            AddAttribute("v", "108");
            AddAttribute("open_pssw", 1);
            AddAttribute("p", bot.Password);
            AddAttribute("l", bot.Login);

            return this;
        }
    }
}