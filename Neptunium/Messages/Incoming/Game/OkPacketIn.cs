using Neptunium.Messages.Outgoing.Game;

namespace Neptunium.Messages.Incoming.Game
{
    public class OkPacketIn : PacketIn
    {
        public override void Handle()
        {
            Bot.Ses = XmlRead.GetAttribute("ses");

            Connection.SendData(new ChatPacketOut());
        }
    }
}