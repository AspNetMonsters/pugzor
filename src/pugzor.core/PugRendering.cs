using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.IO.Compression;
using Microsoft.AspNetCore.NodeServices;

namespace pugzor.core
{
    public class PugRendering : IPugRendering
    {
        public INodeServices _nodeServices { get; set; }
        public IPugzorTempDirectoryProvider _tempDirectoryProvider;
        public PugRendering(INodeServices nodeServices, IPugzorTempDirectoryProvider tempDirectoryProvider)
        {
            _nodeServices = nodeServices;
            _tempDirectoryProvider = tempDirectoryProvider;
            ExpandEmbeddedFiles();
        }

        private string ExpandEmbeddedFiles()
        {
            var asm = this.GetType().GetTypeInfo().Assembly;
            var embeddedResourceName = asm.GetName().Name + ".embeddedNodeResources.zip";

            using (var stream = asm.GetManifestResourceStream(embeddedResourceName))
            {
                var tempDirectory = _tempDirectoryProvider.TempDirectory;
                new ZipArchive(stream).ExtractToDirectory(tempDirectory);
                return tempDirectory;
            }

        }

        public async Task<string> Render(FileInfo pugFile, object model)
        {
            var result = await _nodeServices.InvokeAsync<string>("pugcompile", pugFile.FullName, model);
            return result;
        }
    }
}
