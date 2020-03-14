using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Neptunium.Util.XmlUtils
{
    public class XmlWrite
    {
        public string Name { get; }
        private string Content { get; set; }
        public Dictionary<string, object> Attributes { get; } = new Dictionary<string, object>();
        public List<XmlWrite> Children { get; } = new List<XmlWrite>();

        protected XmlWrite(string name)
        {
            Name = name;
        }

        public string GetXml()
        {
            using var stringWriter = new StringWriter();
            using var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                ConformanceLevel = ConformanceLevel.Fragment,
                Indent = false,
                NewLineHandling = NewLineHandling.None
            });
            Write(xmlWriter);
            xmlWriter.Close();
            return stringWriter.ToString();
        }

        private void Write(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement(Name);

            foreach (var (key, value) in Attributes)
            {
                xmlWriter.WriteAttributeString(key, Convert.ToString(value));
            }

            if (Content != null)
                xmlWriter.WriteString(Content);

            foreach (var child in Children)
            {
                child.Write(xmlWriter);
            }

            xmlWriter.WriteEndElement();
        }

        public object GetAttribute(string key)
        {
            Attributes.TryGetValue(key, out var value);
            return value;
        }

        public XmlWrite AddAttribute(string key, object value)
        {
            Attributes.Add(key, value);
            return this;
        }

        public XmlWrite RemoveAttribute(string key)
        {
            Attributes.Remove(key);
            return this;
        }

        public XmlWrite AddChild(XmlWrite child)
        {
            Children.Add(child);
            return this;
        }

        public XmlWrite AddContent(string content)
        {
            Content = content;
            return this;
        }
    }
}