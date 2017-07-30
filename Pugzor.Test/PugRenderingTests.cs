using System;
using Microsoft.AspNetCore.NodeServices;
using Moq;
using Pugzor.Core;
using System.IO;
using System.Threading.Tasks;
using Pugzor.Core.Abstractions;
using Pugzor.Core.Helpers;
using Xunit;

namespace Pugzor.Test
{
    [Trait("Category", "Rendering")]
    public class PugRenderingTests : IClassFixture<PugRenderingTestsFixture>
    {
        private PugRenderingTestsFixture _fixture;

        public PugRenderingTests(PugRenderingTestsFixture fixture)
        {
            _fixture = fixture;
        }

        private async Task<string> RenderView(object model, string view) =>
            await _fixture.Renderer.Render(new FileInfo($"TestViews/{view}"), model, null, null).ConfigureAwait(false);

        [Fact]
        public async Task ViewEngine_View_Renders()
        {
            var renderResult = await RenderView(new { }, "emptyTest.pug").ConfigureAwait(false);
            Assert.Equal(string.Empty, renderResult);
        }

        [Fact]
        public async Task ViewEngine_NullModel_Handled()
        {
            var renderResult = await RenderView(null, "emptyTest.pug").ConfigureAwait(false);
            Assert.Equal(string.Empty, renderResult);
        }

        [Theory]
        [InlineData("Bob")]
        [InlineData("Jack")]
        [InlineData("Nick")]
        [InlineData("John")]
        public async Task ViewEngine_ModelValue_Embedded(string name)
        {
            var model = new {firstName = name};
            var renderResult = await RenderView(model, "simpleName.pug").ConfigureAwait(false);
            Assert.Equal($"<p>{name}</p>", renderResult);
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
