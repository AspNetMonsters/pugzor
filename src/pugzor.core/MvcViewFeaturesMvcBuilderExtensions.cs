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
            builder.Services.AddTransient<IConfigureOptions<PugzorViewEngineOptions>, PugzorViewEngineOptionsSetup>();

            if (setupAction != null)
            {
                builder.Services.Configure(setupAction);
            }

            builder.Services.AddTransient<IConfigureOptions<MvcViewOptions>, PugzorMvcViewOptionsSetup>();
            var tempDirectoryProvider = new PugzorTempDirectoryProvider();
            builder.Services.AddSingleton<IPugzorTempDirectoryProvider>(tempDirectoryProvider);
            builder.Services.AddTransient<IPugRendering, PugRendering>();
            builder.Services.AddSingleton<IPugzorViewEngine, PugzorViewEngine>();
            builder.Services.AddNodeServices((options) =>
            {
                options.ProjectPath = tempDirectoryProvider.TempDirectory;
            });
            return builder;

        }
    }
}
