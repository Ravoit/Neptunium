using Neptunium.Game.Bots;
using Neptunium.Network;
using Neptunium.Util.XmlUtils;

namespace Neptunium.Messages
{
    public class PacketIn
    {
        protected Connection Connection { get; private set; }
        protected XmlRead XmlRead { get; private set; }
        protected Bot Bot => Connection.Bot;

        public void Read(Connection connection, XmlRead xmlRead)
        {
            Connection = connection;
            XmlRead = xmlRead;
        }

        public virtual void Handle()
        {
        }
    }
}