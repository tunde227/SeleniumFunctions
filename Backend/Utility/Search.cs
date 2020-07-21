using System.Linq;
using System.Xml;
using HtmlAgilityPack;
using log4net;

namespace Backend.Utility
{
    public sealed class Search
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Search));

        private Search(object document)
        {
            Doc = document;
        }

        private object Doc { get; }

        public static Search Document(XmlDocument document)
        {
            return new Search(document);
        }

        public string ByXpath(string xpath, int index = 0)
        {
            return Doc switch
            {
                XmlDocument xml => xml.DocumentElement?.SelectNodes(xpath)?.Item(index)?.InnerText,
                HtmlDocument html => html.DocumentNode.SelectSingleNode(xpath).InnerHtml,
                _ => null
            };
        }

        public string ByTagName(string tagName, int index = 0)
        {
            return Doc switch
            {
                XmlDocument xml => xml.GetElementsByTagName(tagName).Item(index)?.InnerXml,
                HtmlDocument html => html.DocumentNode.Descendants().Where(node => node.Name.Equals(tagName))
                    .Select(e => e.InnerHtml).ElementAt(index),
                _ => null
            };
        }
    }
}