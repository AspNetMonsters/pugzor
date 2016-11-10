using Microsoft.AspNetCore.NodeServices;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pugzor.core
{
    [HtmlTargetElement("pugzor")]
    public class PugzorTagHelper : TagHelper
    {
        private INodeServices _nodeServices;

        public PugzorTagHelper(INodeServices nodeServices)
        {
            _nodeServices = nodeServices;
        }

        [HtmlAttributeName("model")]
        public object Model { get; set; }

        [HtmlAttributeName("view")]
        public string View { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var result = await _nodeServices.InvokeAsync<string>("./pugcompile", View, Model);
            output.TagName = null;
            output.Content.AppendHtml(result);
        }

    }
}
