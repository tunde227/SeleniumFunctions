using Backend.Utility;
using log4net;

namespace Backend.ApiSetup
{
    public abstract class Payload<TSource> : RequestWithoutBody<TSource> where TSource : new()
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Payload<TSource>));

        public abstract string GetTemplateDirectory();
        public abstract string GetFileName();
        public abstract string ReplaceElement(string request);

        protected override string GeneratePayload()
        {
            string payload = ReplaceElement(FileUtils.ReadFromFile(CallingAssembly,
                $"{GetTemplateDirectory()}{GetFileName()}"));
            Logger.Debug($"{CallerClass} -> Payload: {payload}");
            return payload;
        }
    }
}