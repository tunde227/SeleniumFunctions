using RestSharp;

namespace Backend.ApiSetup
{
    public abstract class PostObject<TSource> : RequestSerialization<TSource> where TSource : new()
    {
        protected override Method GetHtmlMethod() => Method.POST;
    }
}