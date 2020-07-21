using RestSharp;

namespace Backend.Api.Base
{
    public abstract class RequestSerialization<TSource> : ServiceRequest<TSource> where TSource : new()
    {
        public abstract string GetPayload();

        protected override string GeneratePayload()
        {
            return GetPayload();
        }

        public override TSource CompleteServiceRequest(string sourceFilePath = "")
        {
            CallerClass = FormatCallerClass(sourceFilePath);

            var request = GetRequest();
            AddBody(request);

            var response = new RestClient().Execute<TSource>(request);
            PrintResponse(response);
            return response.Data;
        }
    }
}