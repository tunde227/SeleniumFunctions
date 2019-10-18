using System.Reflection;
using RestSharp;

namespace Backend.Api.Base
{
    public abstract class RequestWithBody<TSource> : RequestWithoutBody<TSource> where TSource : new()
    {
        public override TSource CompleteServiceRequest(string sourceFilePath = "")
        {
            CallerClass = FormatCallerClass(sourceFilePath);
            CallingAssembly = Assembly.GetCallingAssembly();

            RestRequest request = GetRequest();
            AddBody(request);
            IRestResponse response = new RestClient().Execute(request);
            return Deserialize(response);
        }
    }
}