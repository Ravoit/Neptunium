using Serilog;

namespace Neptunium.Messages.Incoming.CHAT
{
    public class DPacketIn : PacketIn
    {
        public override void Handle()
        {
            var t = XmlRead.GetAttribute("t");
            Log.Information("{0}: {1} - {2}", Bot?.Login, "USER LEFT ROOM", t);
        }
    }
}