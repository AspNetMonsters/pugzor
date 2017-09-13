using System.Collections.Generic;

namespace Pugzor.Core
{
    public class PugzorViewEngineOptions
    {
        public IList<string> ViewLocationFormats { get; } = new List<string>();
        public string BaseDir { get; set; }
        public bool Pretty { get; set; }
    }
}
