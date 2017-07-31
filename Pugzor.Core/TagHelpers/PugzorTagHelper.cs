using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Pugzor.Core.Abstractions;

namespace Pugzor.Core.TagHelpers
{
    [HtmlTargetElement("pugzor")]
    public class PugzorTagHelper : TagHelper
    {
        public object Model { get; set; }
        public string View { get; set; }

        private readonly IPugRendering _pugRendering;

        public PugzorTagHelper(IPugRendering pugRendering)
        {
            _pugRendering = pugRendering;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var result = await _pugRendering.Render(new FileInfo(View), Model, null, null).ConfigureAwait(false);
            output.TagName = null;
            output.Content.AppendHtml(result);
        }
    }
}
