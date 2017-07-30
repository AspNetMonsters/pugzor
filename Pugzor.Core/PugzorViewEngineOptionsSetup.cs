using Microsoft.Extensions.Options;

using static Pugzor.Core.Constants;

namespace Pugzor.Core
{
    public class PugzorViewEngineOptionsSetup : ConfigureOptions<PugzorViewEngineOptions>
    {
        public PugzorViewEngineOptionsSetup() : base(Configure) { }

        private new static void Configure(PugzorViewEngineOptions options)
        {
            options.ViewLocationFormats.Add("Views/{1}/{0}" + VIEW_EXTENSION);
            options.ViewLocationFormats.Add("Views/Shared/{0}" + VIEW_EXTENSION);
        }
    }
}
