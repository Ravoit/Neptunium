using Serilog;

namespace Neptunium.Messages.Incoming.CHAT
{
    public class APacketIn : PacketIn
    {
        public override void Handle()
        {
            var t = XmlRead.GetAttribute("t");
            Log.Information("{0}: {1} - {2}", Bot?.Login, "USER ENTERED ROOM", t);
        }
    }
}