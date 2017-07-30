using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Pugzor.Core.Abstractions;

namespace Pugzor.Core
{
    public class PugzorView : IView
    {
        private readonly string _path;
        private readonly IPugRendering _pugRendering;

        public PugzorView(string path, IPugRendering pugRendering)
        {
            _path = path;
            _pugRendering = pugRendering;
        }

        public string Path => _path;

        public async Task RenderAsync(ViewContext context)
        {
            var result = await _pugRendering.Render(new FileInfo(Path), context.ViewData.Model, context.ViewData, context.ModelState).ConfigureAwait(false);
            context.Writer.Write(result);
        }
    }
}
