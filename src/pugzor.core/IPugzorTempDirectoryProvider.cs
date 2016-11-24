using System;
using System.Linq;

namespace pugzor.core
{
    public interface IPugzorTempDirectoryProvider
    {
        string TempDirectory { get; }
    }
}
