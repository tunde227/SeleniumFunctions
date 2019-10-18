using System;
using System.Globalization;
using System.Xml;
using Backend.Utility;
using HtmlAgilityPack;
using log4net;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Backend.Api.Base
{
    public abstract class RequestWithoutBody<TSource> : ServiceRequest<TSource> where TSource : new()
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(RequestWithoutBody<TSource>));

        public abstract TSource ParseResponse(object serviceResponse);

        protected internal TSource Deserialize(IRestResponse response)
        {
            string contentType = response.ContentType.ToLower(CultureInfo.CurrentCulture);
            string body = response.Content;

            switch (contentType)
            {
                case "xml":
                    return ParseResponse(NodeUtils.CreateXmlDocumentFromText(body));
                case "json":
                    return ParseResponse(JObject.Parse(body));
                case "text/plain":
                    return ParseResponse(body);
                case "text/html":
                    return ParseResponse(NodeUtils.CreateHtmlDocumentFromText(body));
                default:
                    throw new NotImplementedException($"{CallerClass} response type could not be determined.");
            }
        }

        public T GetResponse<T>(object response)
        {
            if (response is XmlDocument || response is JObject || response is string || response is HtmlDocument)
            {
                return (T) response;
            }

            throw new NotImplementedException($"{CallerClass} -> Return type of {typeof(T).Name} is not implemented.");
        }
    }
}