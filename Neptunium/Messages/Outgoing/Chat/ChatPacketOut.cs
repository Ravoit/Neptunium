using System.Reflection.Metadata;
using Neptunium.Game.Bots;

namespace Neptunium.Messages.Outgoing.Chat
{
    internal class ChatPacketOut : PacketOut
    {
        public ChatPacketOut() : base("CHAT")
        {
        }

        public ChatPacketOut AddSes(Bot bot)
        {
            AddAttribute("l", bot.Login);
            AddAttribute("ses", bot.Ses);

            return this;
        }
    }
}