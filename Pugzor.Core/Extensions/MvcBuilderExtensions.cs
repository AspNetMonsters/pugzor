using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Pugzor.Core.Abstractions;
using Pugzor.Core.Helpers;

namespace Pugzor.Core.Extensions
{
    public static class MvcBuilderExtensions
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

            builder.Services
                .AddTransient<IConfigureOptions<MvcViewOptions>, PugzorMvcViewOptionsSetup>()
                .AddSingleton<IPugRendering, PugRendering>()
                .AddSingleton<IPugzorViewEngine, PugzorViewEngine>()
                .AddNodeServices(options =>
                {
                    options.ProjectPath = TemporaryDirectoryHelper.CreateTemporaryDirectory();
                });

            return builder;

        }
    }
}
