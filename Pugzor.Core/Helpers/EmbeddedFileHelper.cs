using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;

namespace Pugzor.Core.Helpers
{
    public static class EmbeddedFileHelper
    {
        public static void ExpandEmbeddedFiles(string tempDirectory)
        {
            var assembly = Assembly.Load(new AssemblyName("Pugzor.Core"));
            var embeddedResourceName = assembly.GetManifestResourceNames().First(resource => resource.Contains("embeddedNodeResources"));

            using (var stream = assembly.GetManifestResourceStream(embeddedResourceName))
            {
                var archive = new ZipArchive(stream, ZipArchiveMode.Read, false);
                var tempDir = new DirectoryInfo(tempDirectory);
                foreach (var entry in archive.Entries)
                {
                    var filePath = $"{tempDir.FullName}\\{entry.FullName}";
                    if (File.Exists(filePath))
                    {
                        continue;
                    }

                    Directory.CreateDirectory(new FileInfo(filePath).DirectoryName);
                    entry.ExtractToFile(filePath, true);
                }
            }
        }
    }
}
