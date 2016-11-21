using Microsoft.AspNetCore.NodeServices;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace pugzor.core
{
    [HtmlTargetElement("pugzor")]
    public class PugzorTagHelper : TagHelper
    {
        public IPugRendering _pugRendering { get; set; }

        public PugzorTagHelper(IPugRendering pugRendering)
        {
            _pugRendering = pugRendering;
        }

        [HtmlAttributeName("model")]
        public object Model { get; set; }

        [HtmlAttributeName("view")]
        public string View { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var result = await _pugRendering.Render(new FileInfo(View), Model, null, null);
            output.TagName = null;
            output.Content.AppendHtml(result);
        }

    }
}
