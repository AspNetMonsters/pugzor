using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Pugzor.Core.Abstractions;
using Pugzor.Core.Helpers;

namespace Pugzor.Core
{
    public class PugRendering : IPugRendering
    {
        private readonly INodeServices _nodeServices;

        public PugRendering(INodeServices nodeServices)
        {
            _nodeServices = nodeServices;
            var tempDirectory = TemporaryDirectoryHelper.CreateTemporaryDirectory();
            EmbeddedFileHelper.ExpandEmbeddedFiles(tempDirectory);
        }

        public async Task<string> Render(FileInfo pugFile, object model, ViewDataDictionary viewData, ModelStateDictionary modelState) =>
            await _nodeServices.InvokeAsync<string>("pugcompile", pugFile.FullName, model, viewData, modelState).ConfigureAwait(false);
    }
}
