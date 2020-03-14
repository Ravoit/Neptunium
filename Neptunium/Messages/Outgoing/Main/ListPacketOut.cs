namespace Neptunium.Messages.Outgoing.Main
{
    internal class ListPacketOut : PacketOut
    {
        public ListPacketOut() : base("LIST")
        {
        }

        public ListPacketOut AddL(string login)
        {
            AddAttribute("login", login);

            return this;
        }
    }
}