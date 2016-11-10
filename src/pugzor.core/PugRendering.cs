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
        private string _tempDirectory;
        public INodeServices _nodeServices { get; set; }
        public PugRendering(INodeServices nodeServices)
        {
            _nodeServices = nodeServices;
            _tempDirectory = ExpandEmbeddedFiles();
        }

        private string ExpandEmbeddedFiles()
        {
            var asm = this.GetType().GetTypeInfo().Assembly;
            var embeddedResourceName = asm.GetName().Name + ".embeddedNodeResources.zip";

            using (var stream = asm.GetManifestResourceStream(embeddedResourceName))
            {
                var tempDirectory = GetTemporaryDirectory();
                new ZipArchive(stream).ExtractToDirectory(tempDirectory);
                return tempDirectory;
            }

        }

        public string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }

        public async Task<string> Render(FileInfo pugFile, object model)
        {
            var result = await _nodeServices.InvokeAsync<string>(Path.Combine(_tempDirectory, "pugcompile.js"), pugFile.FullName, model);
            return result;
        }
    }
}
