using RestSharp;

namespace Backend.ApiSetup
{
    public abstract class RequestSerialization<TSource> : BaseRequest<TSource> where TSource : new()
    {
        public abstract string GetPayload();

        protected override string GeneratePayload() => GetPayload();

        public override TSource CompleteServiceRequest(string sourceFilePath = "")
        {
            CallerClass = FormatCallerClass(sourceFilePath);

            RestRequest request = GetRequest();
            AddBody(request);

            IRestResponse<TSource> response = new RestClient().Execute<TSource>(request);
            PrintResponse(response);
            return response.Data;
        }
    }
}