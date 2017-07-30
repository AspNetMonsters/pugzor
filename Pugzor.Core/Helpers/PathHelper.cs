using System;
using System.Diagnostics;
using static Pugzor.Core.Constants;

namespace Pugzor.Core.Helpers
{
    public static class PathHelper
    {
        public static string GetAbsolutePath(string executingFilePath, string pagePath)
        {
            if (string.IsNullOrEmpty(pagePath))
            {
                // Path is not valid; no change required.
                return pagePath;
            }

            if (IsApplicationRelativePath(pagePath))
            {
                // An absolute path already; no change required.
                return pagePath.Replace("~/", "");
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
            return executingFilePath.Substring(0, index + 1) + pagePath;
        }

        public static bool IsApplicationRelativePath(string name)
        {
            Debug.Assert(!string.IsNullOrEmpty(name));
            return name[0] == '~' || name[0] == '/';
        }

        public static bool IsRelativePath(string name)
        {
            Debug.Assert(!string.IsNullOrEmpty(name));

            // Though ./ViewName looks like a relative path, framework searches for that view using view locations.
            return name.EndsWith(VIEW_EXTENSION, StringComparison.OrdinalIgnoreCase);
        }
    }
}
