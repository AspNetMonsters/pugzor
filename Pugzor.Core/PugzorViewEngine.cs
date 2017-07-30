using System;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using Pugzor.Core.Abstractions;
using Pugzor.Core.Extensions;
using Pugzor.Core.Helpers;

using static Pugzor.Core.Constants;

namespace Pugzor.Core
{
    public class PugzorViewEngine : IPugzorViewEngine
    {
        private readonly IPugRendering _pugRendering;
        private readonly PugzorViewEngineOptions _options;

        public PugzorViewEngine(IPugRendering pugRendering, IOptions<PugzorViewEngineOptions> optionsAccessor)
        {
            _options = optionsAccessor?.Value ?? throw new ArgumentNullException(nameof(optionsAccessor));
            _pugRendering = pugRendering;
        }

        public ViewEngineResult FindView(ActionContext context, string viewName, bool isMainPage)
        {
            var controllerName = context.GetNormalizedRouteValue(CONTROLLER_KEY);
            var areaName = context.GetNormalizedRouteValue(AREA_KEY);

            var checkedLocations = new List<string>();
            foreach (var location in _options.ViewLocationFormats)
            {
                var view = string.Format(location, viewName, controllerName);
                if (File.Exists(view))
                {
                    return ViewEngineResult.Found("Default", new PugzorView(view, _pugRendering));
                }
                checkedLocations.Add(view);
            }

            return ViewEngineResult.NotFound(viewName, checkedLocations);
        }

        public ViewEngineResult GetView(string executingFilePath, string viewPath, bool isMainPage)
        {
            var applicationRelativePath = PathHelper.GetAbsolutePath(executingFilePath, viewPath);

            if (!(PathHelper.IsAbsolutePath(viewPath) || PathHelper.IsRelativePath(viewPath)))
            {
                // Not a path this method can handle.
                return ViewEngineResult.NotFound(applicationRelativePath, Enumerable.Empty<string>());
            }

            return ViewEngineResult.Found("Default", new PugzorView(applicationRelativePath, _pugRendering));
        }
    }
}
