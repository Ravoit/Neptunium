using Serilog;

namespace Neptunium.Messages.Incoming.CHAT
{
    public class RPacketIn : PacketIn
    {
        public override void Handle()
        {
            var t = XmlRead.GetAttribute("t");
            Log.Information("{0}: {1} - {2}", Bot?.Login, "ROOM INFO", t);
        }
    }
}