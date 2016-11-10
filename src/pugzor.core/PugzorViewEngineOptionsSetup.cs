using Microsoft.Extensions.Options;

namespace pugzor.core
{
    public class PugzorViewEngineOptionsSetup : ConfigureOptions<PugzorViewEngineOptions>
    {
        public PugzorViewEngineOptionsSetup()
            : base(options => Configure(options))
        {
        }

        private static new void Configure(PugzorViewEngineOptions options)
        {            
            options.ViewLocationFormats.Add("Views/{1}/{0}" + PugzorViewEngine.ViewExtension);
            options.ViewLocationFormats.Add("Views/Shared/{0}" + PugzorViewEngine.ViewExtension);
        }
    }    
}
