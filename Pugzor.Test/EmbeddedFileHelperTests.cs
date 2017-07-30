using System.IO;
using Pugzor.Core.Helpers;
using Xunit;

namespace Pugzor.Test
{
    [Trait("Category", "Helper")]
    public class EmbeddedFileHelperTests
    {
        [Fact]
        public void EmbeddedFileHelper_Extract_DirectoryContainsFiles()
        {
            // Assuming temporary directory helper is working as should
            var tempDir = TemporaryDirectoryHelper.CreateTemporaryDirectory();
            EmbeddedFileHelper.ExpandEmbeddedFiles(tempDir);
            var directory = new DirectoryInfo(tempDir);

            Assert.NotEmpty(directory.EnumerateDirectories());
            Assert.NotEmpty(directory.EnumerateFiles());
        }
    }
}
