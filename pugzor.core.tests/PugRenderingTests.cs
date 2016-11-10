using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Moq;


namespace pugzor.core.tests
{
    public class PugRenderingTests
    {
        [Fact]
        public void Can_find_files()
        {
            var file = new System.IO.FileInfo("PugFiles/simple.pug");
            Assert.True(file.Exists);
        }

        [Fact]
        public async Task Should_render_simple_view()
        {
            var mockServices = new Mock<IServiceProvider>();
            var pugRendering = new PugRendering(Microsoft.AspNetCore.NodeServices.NodeServicesFactory.CreateNodeServices(new Microsoft.AspNetCore.NodeServices.NodeServicesOptions(mockServices.Object)), new PugzorTempDirectoryProvider());
            var file = new System.IO.FileInfo("PugFiles/simple.pug");
            Assert.Equal("Hello", await pugRendering.Render(file, new { }));
        }
    }
}
