using Backend.Api.Base;
using RestSharp;

namespace Backend.Api.HTTP
{
    public abstract class PostObject<TSource> : RequestSerialization<TSource> where TSource : new()
    {
        protected override Method GetHtmlMethod()
        {
            return Method.POST;
        }
    }
}