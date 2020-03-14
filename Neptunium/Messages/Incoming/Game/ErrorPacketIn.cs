namespace Neptunium.Messages.Incoming.Game
{
    public class ErrorPacketIn : PacketIn
    {
        public override void Handle()
        {
            var code = XmlRead.GetAttribute("code");

            if (code == "1")
            {
                Bot.ConnectToGame(); // next game server
            }
        }
    }
}