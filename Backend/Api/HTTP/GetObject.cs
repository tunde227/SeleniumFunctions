using Backend.Api.Base;
using RestSharp;

namespace Backend.Api.HTTP
{
    public abstract class GetObject<TSource> : ServiceRequest<TSource> where TSource : new()
    {
        protected override Method GetHtmlMethod() => Method.GET;
    }
}