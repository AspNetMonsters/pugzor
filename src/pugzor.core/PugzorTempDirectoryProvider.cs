using System;
using System.IO;
using System.Linq;

namespace pugzor.core
{
    public class PugzorTempDirectoryProvider : IPugzorTempDirectoryProvider
    {
        public string TempDirectory { get; private set; }
        public PugzorTempDirectoryProvider()
        {
            TempDirectory = GetTemporaryDirectory();
        }

        private string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }
    }
}
