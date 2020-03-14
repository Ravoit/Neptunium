namespace Neptunium.Messages.Incoming.Main
{
    public class ListPacketIn : PacketIn
    {
        public override void Handle()
        {
            foreach (var xmlRead in XmlRead.Children)
            {
                var host = xmlRead.GetAttribute("host");

                Bot.GameServers.Add(host);
            }

            Bot.ConnectToGame();
        }
    }
}