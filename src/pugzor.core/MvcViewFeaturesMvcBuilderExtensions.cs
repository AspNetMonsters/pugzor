using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace pugzor.core
{
    public static class MvcViewFeaturesMvcBuilderExtensions
    {
        public static IMvcBuilder AddPugzor(this IMvcBuilder builder, Action<PugzorViewEngineOptions> setupAction = null)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Services.AddOptions();
            ServiceDescriptor.Transient<IConfigureOptions<PugzorViewEngineOptions>, PugzorViewEngineOptionsSetup>();

            if (setupAction != null)
            {
                builder.Services.Configure(setupAction);
            }


            builder.Services.AddTransient<IConfigureOptions<MvcViewOptions>, PugzorMvcViewOptionsSetup>();
            builder.Services.AddSingleton<IPugzorViewEngine, PugzorViewEngine>();
            return builder;

        }
    }
}
