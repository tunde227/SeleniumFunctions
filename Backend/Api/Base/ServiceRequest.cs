using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using log4net;
using RestSharp;

namespace Backend.Api.Base
{
    public abstract class ServiceRequest<TSource> where TSource : new()
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ServiceRequest<TSource>));

        public Assembly CallingAssembly { get; set; }
        protected internal string CallerClass { get; set; }
        public abstract string Url { get; }
        protected abstract Method GetHtmlMethod();
        public abstract List<(string, string)> GetHeaders();

        protected virtual string GeneratePayload()
        {
            return string.Empty;
        }

        protected internal string PrepareUrl()
        {
            var url = Url;
            Logger.Debug($"{CallerClass} -> URL: {url}");
            return string.IsNullOrEmpty(url)
                ? throw new NotSupportedException($"Please provide a URL for {CallerClass} API.")
                : url;
        }

        protected internal RestRequest GetRequest()
        {
            var request = new RestRequest(PrepareUrl(), GetHtmlMethod());
            GetHeaders().ForEach(header => request.AddHeader(header.Item1, header.Item2));
            return request;
        }

        protected internal void AddBody(RestRequest request)
        {
            request.AddParameter("", GeneratePayload(), ParameterType.RequestBody);
        }

        public virtual TSource CompleteServiceRequest([CallerFilePath] string sourceFilePath = "")
        {
            CallerClass = FormatCallerClass(sourceFilePath);
            var response = new RestClient().Execute<TSource>(GetRequest());
            PrintResponse(response);
            return response.Data;
        }

        protected static string FormatCallerClass(string sourceFilePath)
        {
            return !string.IsNullOrEmpty(sourceFilePath)
                ? sourceFilePath.Substring(sourceFilePath.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                    .Replace(".cs", "")
                : "";
        }

        protected internal static void PrintResponse(IRestResponse response)
        {
            Logger.Debug($"Status Code: {response.StatusCode} - Status Description: {response.StatusDescription} - " +
                         $"HTTP Status: {response.ResponseStatus} - Content-Type: {response.ContentType} - " +
                         $"\n Response Content: {response.Content} - ");
        }
    }
}