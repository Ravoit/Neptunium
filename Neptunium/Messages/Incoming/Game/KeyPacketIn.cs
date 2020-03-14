namespace Neptunium.Messages.Incoming.Game
{
    public class KeyPacketIn : PacketIn
    {
        public override void Handle()
        {
            Bot.Key =  XmlRead.GetAttribute("key");

            Bot.TryLogin();
        }
    }
}