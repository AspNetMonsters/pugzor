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
                new ZipArchive(stream, ZipArchiveMode.Read, false).ExtractToDirectory(tempDirectory);
            }
        }
    }
}
