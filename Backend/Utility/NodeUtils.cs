using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using HtmlAgilityPack;
using log4net;

namespace Backend.Utility
{
    public static class NodeUtils
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(NodeUtils));

        public static XmlDocument CreateXmlDocumentFromText(string xml)
        {
            var document = new XmlDocument();
            document.LoadXml(xml);
            return document;
        }

        public static HtmlDocument CreateHtmlDocumentFromText(string html)
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);
            return document;
        }

        public static XmlDocument CreateXmlDocumentFromNodeList(XmlNodeList nodeList, int index = 0)
        {
            var document = new XmlDocument();
            document.LoadXml(nodeList?.Item(index)?.OuterXml ?? throw new InvalidOperationException());
            return document;
        }

        public static string ToString(XmlDocument document, bool withDeclaration = false)
        {
            var declaration = document.ChildNodes.OfType<XmlDeclaration>().FirstOrDefault();
            return withDeclaration ? declaration?.Value + document.OuterXml : document.OuterXml;
        }

        public static T Deserialize<T>(string xml) where T : class
        {
            using var stringReader = new StringReader(xml);
            using var textReader = new XmlTextReader(stringReader);
            return new XmlSerializer(typeof(T)).Deserialize(textReader) as T;
        }

        public static T Deserialize<T>(XmlDocument document) where T : class
        {
            using XmlReader reader = new XmlNodeReader(document);
            return new XmlSerializer(typeof(T)).Deserialize(reader) as T;
        }

        public static string Serialize(Type type, bool removeNamespaces = false)
        {
            using var stream = new StringWriter();
            using var writer = XmlWriter.Create(stream, removeNamespaces
                ? new XmlWriterSettings {OmitXmlDeclaration = true}
                : null);
            new XmlSerializer(type).Serialize(writer, Activator.CreateInstance(type),
                removeNamespaces ? new XmlSerializerNamespaces(new[] {XmlQualifiedName.Empty}) : null);
            return stream.ToString().Replace(" />", "/>");
        }

        public static string Serialize(object @object, bool removeNamespaces = false)
        {
            return Serialize(@object.GetType(), removeNamespaces);
        }
    }
}