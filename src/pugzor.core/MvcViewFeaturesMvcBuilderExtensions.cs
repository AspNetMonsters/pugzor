using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace pugzor.core
{
    public static class MvcViewFeaturesMvcBuilderExtensions
    {
        public static IMvcBuilder AddPugzor(this IMvcBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            var tempDirectoryProvider = new PugzorTempDirectoryProvider();
            builder.Services.AddSingleton<IPugzorTempDirectoryProvider>(tempDirectoryProvider);
            builder.Services.AddTransient<IConfigureOptions<MvcViewOptions>, PugzorMvcViewOptionsSetup>();
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
