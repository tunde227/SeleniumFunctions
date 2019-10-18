using RestSharp;

namespace Backend.ApiSetup
{
    public abstract class Get<TSource> : RequestWithoutBody<TSource> where TSource : new()
    {
        protected override Method GetHtmlMethod() => Method.GET;

        public override TSource CompleteServiceRequest(string sourceFilePath = "")
        {
            CallerClass = FormatCallerClass(sourceFilePath);
            RestRequest request = GetRequest();
            IRestResponse response = new RestClient().Execute(request);
            PrintResponse(response);
            return Deserialize(response);
        }
    }
}