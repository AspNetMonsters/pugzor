using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace pugzor.core
{
    public class PugzorViewEngine : IPugzorViewEngine
    {
        private INodeServices _nodeServices;
        public static readonly string ViewExtension = ".pug";
        private const string ControllerKey = "controller";
        private const string AreaKey = "area";
        private PugzorViewEngineOptions _options;

        public PugzorViewEngine(INodeServices nodeServices, 
            IOptions<PugzorViewEngineOptions> optionsAccessor)
        {            
            _options = optionsAccessor.Value;
            _nodeServices = nodeServices;
        }

        public ViewEngineResult FindView(ActionContext context, string viewName, bool isMainPage)
        {
            return LocatePageFromViewLocations(context, viewName, isMainPage);            
        }

        private ViewEngineResult LocatePageFromViewLocations(
            ActionContext actionContext,
            string viewName,
            bool isMainPage)
        {
            var controllerName = GetNormalizedRouteValue(actionContext, ControllerKey);
            var areaName = GetNormalizedRouteValue(actionContext, AreaKey);

            var view = string.Format(_options.ViewLocationFormats[0], viewName, controllerName);
                        
            return ViewEngineResult.Found("Default", new PugzorView(view, _nodeServices));
        }

        public ViewEngineResult GetView(string executingFilePath, string viewPath, bool isMainPage)
        {
            var applicationRelativePath = GetAbsolutePath(executingFilePath, viewPath);

            if (!(IsApplicationRelativePath(viewPath) || IsRelativePath(viewPath)))
            {
                // Not a path this method can handle.
                return ViewEngineResult.NotFound(viewPath, Enumerable.Empty<string>());
            }

            return ViewEngineResult.Found("Default", new PugzorView("pug1.pug", _nodeServices));
        }

        public string GetAbsolutePath(string executingFilePath, string pagePath)
        {
            if (string.IsNullOrEmpty(pagePath))
            {
                // Path is not valid; no change required.
                return pagePath;
            }

            if (IsApplicationRelativePath(pagePath))
            {
                // An absolute path already; no change required.
                return pagePath;
            }

            if (!IsRelativePath(pagePath))
            {
                // A page name; no change required.
                return pagePath;
            }

            // Given a relative path i.e. not yet application-relative (starting with "~/" or "/"), interpret
            // path relative to currently-executing view, if any.
            if (string.IsNullOrEmpty(executingFilePath))
            {
                // Not yet executing a view. Start in app root.
                return "/" + pagePath;
            }

            // Get directory name (including final slash) but do not use Path.GetDirectoryName() to preserve path
            // normalization.
            var index = executingFilePath.LastIndexOf('/');
            Debug.Assert(index >= 0);
            return executingFilePath.Substring(0, index + 1) + pagePath;
        }


        private static bool IsApplicationRelativePath(string name)
        {
            Debug.Assert(!string.IsNullOrEmpty(name));
            return name[0] == '~' || name[0] == '/';
        }

        private static bool IsRelativePath(string name)
        {
            Debug.Assert(!string.IsNullOrEmpty(name));

            // Though ./ViewName looks like a relative path, framework searches for that view using view locations.
            return name.EndsWith(ViewExtension, StringComparison.OrdinalIgnoreCase);
        }

        public static string GetNormalizedRouteValue(ActionContext context, string key)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            object routeValue;
            if (!context.RouteData.Values.TryGetValue(key, out routeValue))
            {
                return null;
            }

            var actionDescriptor = context.ActionDescriptor;
            string normalizedValue = null;

            string value;
            if (actionDescriptor.RouteValues.TryGetValue(key, out value) &&
                !string.IsNullOrEmpty(value))
            {
                normalizedValue = value;
            }

            var stringRouteValue = routeValue?.ToString();
            if (string.Equals(normalizedValue, stringRouteValue, StringComparison.OrdinalIgnoreCase))
            {
                return normalizedValue;
            }

            return stringRouteValue;
        }


    }
}
