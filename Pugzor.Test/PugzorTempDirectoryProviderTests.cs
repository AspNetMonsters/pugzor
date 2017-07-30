using System.IO;
using Pugzor.Core.Helpers;
using Xunit;

namespace Pugzor.UnitTests
{
    [Trait("Category", "TempDirectory")]
    public class PugzorTempDirectoryProviderTests
    {
        [Fact]
        void Directory_Created_Exists()
        {
            var path = TemporaryDirectoryHelper.CreateTemporaryDirectory();
            var result = Directory.Exists(path);
            Assert.True(result);
        }
    }
}
