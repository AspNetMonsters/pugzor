using System;
using Microsoft.AspNetCore.Mvc;

namespace Pugzor.Core.Extensions
{
    public static class ActionContextExtensions
    {
        public static string GetNormalizedRouteValue(this ActionContext context, string key)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (!context.RouteData.Values.TryGetValue(key, out object routeValue))
            {
                return null;
            }

            var actionDescriptor = context.ActionDescriptor;
            string normalizedValue = null;

            if (actionDescriptor.RouteValues.TryGetValue(key, out string value) && !string.IsNullOrEmpty(value))
            {
                normalizedValue = value;
            }

            var stringRouteValue = routeValue?.ToString();
            return string.Equals(normalizedValue, stringRouteValue, StringComparison.OrdinalIgnoreCase) ? normalizedValue : stringRouteValue;
        }
    }
}
