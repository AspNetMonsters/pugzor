using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace pugzor.core
{
    public interface IPugzorTempDirectoryProvider
    {
        string TempDirectory { get; }
    }

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
