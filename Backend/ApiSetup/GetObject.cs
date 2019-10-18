using RestSharp;

namespace Backend.ApiSetup
{
    public abstract class GetObject<TSource> : BaseRequest<TSource> where TSource : new()
    {
        protected override Method GetHtmlMethod() => Method.GET;
    }
}