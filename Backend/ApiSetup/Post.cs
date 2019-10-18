using RestSharp;

namespace Backend.ApiSetup
{
    public abstract class Post<TSource> : Payload<TSource> where TSource : new()
    {
        protected override Method GetHtmlMethod() => Method.POST;
    }
}