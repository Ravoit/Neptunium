namespace Neptunium.Messages.Incoming.Game
{
    public class ChatPacketIn : PacketIn
    {
        public override void Handle()
        {
            Bot.ChatServer = XmlRead.GetAttribute("server");
            Bot.ConnectToChat();
        }
    }
}