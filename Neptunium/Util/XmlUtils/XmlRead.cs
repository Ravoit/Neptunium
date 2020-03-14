using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Neptunium.Util.XmlUtils
{
    public class XmlRead
    {
        public string XmlName { get; private set; }
        private string Content { get; set; }
        public Dictionary<string, string> Attributes { get; private set; }
        public List<XmlRead> Children { get; private set; }

        public XmlRead(string xml)
        {
            using XmlReader xmlReader = XmlReader.Create(new StringReader(xml));
            Read(xmlReader);
        }

        private XmlRead(XmlReader xmlReader)
        {
            Read(xmlReader);
        }

        private void Read(XmlReader xmlReader)
        {
            Attributes = new Dictionary<string, string>();
            Children = new List<XmlRead>();

            xmlReader.MoveToContent();

            XmlName = xmlReader.Name;

            while (xmlReader.MoveToNextAttribute())
            {
                Attributes.Add(xmlReader.Name, xmlReader.Value);
            }

            xmlReader.MoveToElement();

            var currentDepth = xmlReader.Depth;

            while (xmlReader.Read())
            {
                switch (xmlReader.NodeType)
                {
                    case XmlNodeType.Element:
                    {
                        KeepReading:
                        if (xmlReader.Depth > currentDepth)
                        {
                            Children.Add(new XmlRead(xmlReader));

                            goto KeepReading;
                        }

                        return;
                    }
                    case XmlNodeType.Text:
                        Content += xmlReader.Value;
                        break;
                }
            }
        }

        public string GetAttribute(string key)
        {
            Attributes.TryGetValue(key, out var value);
            return value;
        }

    }
}