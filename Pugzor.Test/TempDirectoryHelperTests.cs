using System.IO;
using Pugzor.Core.Helpers;
using Xunit;

namespace Pugzor.Test
{
    [Trait("Category", "Helper")]
    public class TempDirectoryHelperTests
    {
        [Fact]
        void Directory_Created_Exists()
        {
            var path = TemporaryDirectoryHelper.CreateTemporaryDirectory(true);
            var result = Directory.Exists(path);
            Assert.True(result);
        }
    }
}
