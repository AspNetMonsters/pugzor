using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using Moq;
using Pugzor.Core;
using Xunit;

namespace Pugzor.Test
{
    [Trait("Category", "PugzorMvcViewOptionsSetup")]
    public class PugzorMvcViewOptionsSetupTests
    {
        [Fact]
        public void PugzorMvcViewOptionsSetup_ConstructorWithNullViewEngine_ThrowsArgumentNullException()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new PugzorMvcViewOptionsSetup(null));
        }

        [Fact]
        public void PugzorMvcViewOptionsSetup_ConfigureWithNullOptions_ThrowsArgumentNullException()
        {
            var viewEngine = new PugzorViewEngine(null, new OptionsWrapper<PugzorViewEngineOptions>(new PugzorViewEngineOptions()));
            var options = new PugzorMvcViewOptionsSetup(viewEngine);
            Assert.Throws(typeof(ArgumentNullException), () => options.Configure(null));
        }
    }
}
