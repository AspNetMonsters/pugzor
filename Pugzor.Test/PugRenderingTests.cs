using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.NodeServices;
using Moq;
using Pugzor.Core;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Pugzor.Core.Abstractions;
using Pugzor.Core.Helpers;
using Xunit;

namespace Pugzor.Test
{
    [Trait("Category", "Rendering")]
    public class PugRenderingTests : IClassFixture<PugRenderingTestsFixture>
    {
        public static IEnumerable<object[]> Names =>
            new List<object[]>
            {
                new []{ "dpaquette" },
                new []{ "stimms" },
                new []{ "MisterJames" }
            };

        private PugRenderingTestsFixture _fixture;

        public PugRenderingTests(PugRenderingTestsFixture fixture)
        {
            _fixture = fixture;
        }

        private async Task<string> RenderView(string view, object model = null, ViewDataDictionary viewData = null, ModelStateDictionary modelState = null) =>
            await _fixture.Renderer.Render(new FileInfo($"TestViews/{view}"), model, viewData, modelState).ConfigureAwait(false);

        [Fact]
        public async Task ViewEngine_View_Renders()
        {
            var renderResult = await RenderView("empty.pug", new { }).ConfigureAwait(false);
            Assert.Equal(string.Empty, renderResult);
        }

        [Fact]
        public async Task ViewEngine_NullModel_Handled()
        {
            var renderResult = await RenderView("empty.pug").ConfigureAwait(false);
            Assert.Equal(string.Empty, renderResult);
        }

        [Theory]
        [MemberData(nameof(Names))]
        public async Task ViewEngine_ModelValue_Embedded(string name)
        {
            var renderResult = await RenderView("simpleName.pug", new { firstName = name }).ConfigureAwait(false);
            Assert.Equal($"<p>{name}</p>", renderResult);
        }

        [Theory]
        [MemberData(nameof(Names))]
        public async Task ViewEngine_ViewDataValue_Embedded(string name)
        {
            var viewData = new ViewDataDictionary(new DefaultModelMetadataProvider(new DefaultCompositeMetadataDetailsProvider(new List<IMetadataDetailsProvider>())), new ModelStateDictionary())
            {
                ["name"] = name
            };

            var renderResult = await RenderView("viewData.pug", viewData: viewData).ConfigureAwait(false);
            Assert.Equal($"<p>{name}</p>", renderResult);
        }

        [Theory]
        [MemberData(nameof(Names))]
        public async Task ViewEngine_ModelStateValue_Embedded(string name)
        {
            var modelState = new ModelStateDictionary();
            var testString = $"{name} is an invalid value";
            modelState.TryAddModelError("testError", $"{testString}");

            var renderResult = await RenderView("modelState.pug", modelState: modelState).ConfigureAwait(false);
            Debug.Write(renderResult);

            Assert.Equal($"<p>{testString}</p>", renderResult);
        }
    }

    public class PugRenderingTestsFixture
    {
        public IPugRendering Renderer;

        public PugRenderingTestsFixture()
        {
            var mockServices = new Mock<IServiceProvider>();
            var nodeServiceOptions = new NodeServicesOptions(mockServices.Object)
            {
                ProjectPath = TemporaryDirectoryHelper.CreateTemporaryDirectory(true)
            };
            var nodeServices = NodeServicesFactory.CreateNodeServices(nodeServiceOptions);
            Renderer = new PugRendering(nodeServices);
        }
    }
}
