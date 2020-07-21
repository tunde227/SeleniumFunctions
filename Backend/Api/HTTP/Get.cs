using Backend.Api.Base;
using RestSharp;

namespace Backend.Api.HTTP
{
    public abstract class Get<TSource> : RequestWithoutBody<TSource> where TSource : new()
    {
        protected override Method GetHtmlMethod()
        {
            return Method.GET;
        }

        public override TSource CompleteServiceRequest(string sourceFilePath = "")
        {
            CallerClass = FormatCallerClass(sourceFilePath);
            var request = GetRequest();
            var response = new RestClient().Execute(request);
            PrintResponse(response);
            return Deserialize(response);
        }
    }
}