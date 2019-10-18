using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
            XmlDeclaration declaration = document.ChildNodes.OfType<XmlDeclaration>().FirstOrDefault();
            return withDeclaration ? declaration?.Value + document.OuterXml : document.OuterXml;
        }

        public static XmlNodeList GetNodeListByXPath(XmlDocument document, string xpath)
        {
            try
            {
                return document.DocumentElement?.SelectNodes(xpath);
            }
            catch (Exception e)
            {
                Logger.Debug($"{e.Message} -> XPath not found: {xpath}");
            }

            return default;
        }

        public static string ByTagName(XmlDocument document, string tagName, int index = 0)
        {
            try
            {
                return document.GetElementsByTagName(tagName).Item(index)?.InnerXml;
            }
            catch (Exception e)
            {
                Logger.Debug($"{e.Message} -> TagName not found: {tagName}");
            }

            return default;
        }

        public static string ByXpath(XmlDocument document, string xpath, int index = 0)
        {
            try
            {
                return GetNodeListByXPath(document, xpath).Item(index)?.InnerText;
            }
            catch (Exception e)
            {
                Logger.Debug(e);
            }

            return default;
        }

        public static List<string> ByXpath(XmlDocument document, string xpath)
        {
            XmlNodeList nodes = GetNodeListByXPath(document, xpath);
            return Enumerable.Range(0, nodes.Count).Select(nodes.Item).Where(e => e != null).Select(e => e.Value)
                .ToList();
        }

        public static T Deserialize<T>(string xml) where T : class
        {
            using (var stringReader = new StringReader(xml))
            {
                using (var textReader = new XmlTextReader(stringReader))
                {
                    return new XmlSerializer(typeof(T)).Deserialize(textReader) as T;
                }
            }
        }

        public static T Deserialize<T>(XmlDocument document) where T : class
        {
            using (XmlReader reader = new XmlNodeReader(document))
            {
                return new XmlSerializer(typeof(T)).Deserialize(reader) as T;
            }
        }

        public static string Serialize(Type type, bool removeNamespaces = false)
        {
            using (var stream = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(stream, removeNamespaces
                    ? new XmlWriterSettings() {OmitXmlDeclaration = true}
                    : null))
                {
                    new XmlSerializer(type).Serialize(writer, Activator.CreateInstance(type),
                        removeNamespaces ? new XmlSerializerNamespaces(new[] {XmlQualifiedName.Empty}) : null);
                    return stream.ToString().Replace(" />", "/>");
                }
            }
        }

        public static string Serialize(object @object, bool removeNamespaces = false) =>
            Serialize(@object.GetType(), removeNamespaces);

        public static List<Dictionary<string, object>> NodeValueToList(Type constant, XmlNodeList nodeList) =>
            Enumerable.Range(0, nodeList.Count).Select(i => NodeValuesToDictionary(constant, nodeList, i)).ToList();

        public static Dictionary<string, object> NodeValuesToDictionary(Type constants, XmlNodeList nodes, int index)
        {
            XmlDocument document = CreateXmlDocumentFromNodeList(nodes, index);
            return new List<PropertyInfo>(constants.GetProperties())
                .Where(field => field.GetValue(null).GetType() == typeof(Dictionary<string, object>))
                .Select(field => field.GetValue(null))
                .Cast<Dictionary<string, object>>()
                .SelectMany(e => e)
                .ToDictionary(e => e.Key, e => ByXpath(document, e.Value as string, 0) as object);
        }
    }
}