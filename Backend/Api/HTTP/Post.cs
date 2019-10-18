using Backend.Api.Base;
using RestSharp;

namespace Backend.Api.HTTP
{
    public abstract class Post<TSource> : RequestPayload<TSource> where TSource : new()
    {
        protected override Method GetHtmlMethod() => Method.POST;
    }
}