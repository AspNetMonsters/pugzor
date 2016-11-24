using Microsoft.AspNetCore.NodeServices;
using Moq;
using pugzor.core;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Pugzor.UnitTests
{
    public class PugRenderingTests
    {
        INodeServices _nodeServices;
        PugzorTempDirectoryProvider _tempDirectoryProvider;
        public PugRenderingTests()
        {
            _tempDirectoryProvider = new PugzorTempDirectoryProvider();
            _nodeServices = NodeServicesFactory.CreateNodeServices(
                new NodeServicesOptions(new Mock<IServiceProvider>().Object)
                {
                    ProjectPath = _tempDirectoryProvider.TempDirectory
                });
        }

        [Fact]
        public async Task A_view_should_be_rendered()
        {
            var renderer = new PugRendering(_nodeServices, _tempDirectoryProvider);
            var result = await renderer.Render(new FileInfo("emptyTest.pug"), new { }, null, null);
            Assert.Equal(String.Empty, result);
        }

        [Fact]
        public async Task A_null_model_should_be_handled()
        {
            var renderer = new PugRendering(_nodeServices, _tempDirectoryProvider);
            var result = await renderer.Render(new FileInfo("emptyTest.pug"), null, null, null);
            Assert.Equal(String.Empty, result);
        }

        [Fact]
        public async Task A_value_should_be_embedded()
        {
            var renderer = new PugRendering(_nodeServices, _tempDirectoryProvider);
            var result = await renderer.Render(new FileInfo("simpleName.pug"), new { firstName = "bob" }, null, null);
            Assert.Equal("<p>bob</p>", result);
        }
    }
}
