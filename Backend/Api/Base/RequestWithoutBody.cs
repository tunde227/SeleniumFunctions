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
            return response?.ContentType.ToLower(CultureInfo.CurrentCulture) switch
            {
                "xml"        => ParseResponse(NodeUtils.CreateXmlDocumentFromText(response.Content)),
                "json"       => ParseResponse(JObject.Parse(response.Content)),
                "text/plain" => ParseResponse(response.Content),
                "text/html"  => ParseResponse(NodeUtils.CreateHtmlDocumentFromText(response.Content)),
                _ => throw new NotImplementedException(
                    $"{CallerClass} response type could not be determined.")
            };
        }

        public T Deserialize<T>(T response) => response;
    }
}