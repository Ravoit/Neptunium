using Serilog;

namespace Neptunium.Messages.Incoming.CHAT
{
    public class RadioListPacketIn : PacketIn
    {
        public override void Handle()
        {
            var s = XmlRead.GetAttribute("s");
            Log.Information("{0}: {1} - {2}", Bot?.Login, "RadioList", s);
        }
    }
}