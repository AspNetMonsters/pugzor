using pugzor.core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Pugzor.UnitTests
{
    public class PugzorTempDirectoryProviderTests
    {
        [Fact]
        void Directory_is_created()
        {
            var sut = new PugzorTempDirectoryProvider();
            Assert.True(Directory.Exists(sut.TempDirectory));
        }

        [Fact]
        void Directory_is_empty()
        {
            var sut = new PugzorTempDirectoryProvider();
            Assert.False(Directory.GetFiles(sut.TempDirectory).Any());
        }
    }
}
