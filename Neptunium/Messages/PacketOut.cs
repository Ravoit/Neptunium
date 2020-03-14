using Neptunium.Util.XmlUtils;

namespace Neptunium.Messages
{
    public class PacketOut : XmlWrite
    {
        protected PacketOut(string name) : base(name)
        {
        }
    }
}