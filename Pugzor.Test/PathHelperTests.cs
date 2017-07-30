using Pugzor.Core.Helpers;
using Xunit;

namespace Pugzor.Test
{
    [Trait("Category", "Helper")]
    public class PathHelperTests
    {
        private const string RELATIVE_PATH = "this/is/relative.pug";
        private const string ABSOLUTE_PATH_1 = "/this/is/absolue.pug";
        private const string ABSOLUTE_PATH_2 = "~/this/is/absolue.pug";


        [Fact]
        public void PathHelperIsRelativePath_WithRelativePath_ReturnsTrue()
        {
            Assert.True(PathHelper.IsRelativePath(RELATIVE_PATH));
        }

        [Theory]
        [InlineData(ABSOLUTE_PATH_1)]
        [InlineData(ABSOLUTE_PATH_2)]
        public void PathHelperIsRelativePath_WithNonRelativePath_ReturnsFalse(string path)
        {
            Assert.False(PathHelper.IsRelativePath(path));
            Assert.False(PathHelper.IsRelativePath(path));
        }

        [Theory]
        [InlineData(ABSOLUTE_PATH_1)]
        [InlineData(ABSOLUTE_PATH_2)]
        public void PathHelperIsAbsolutePath_WithAbsolutePath_ReturnsTrue(string path)
        {
            Assert.True(PathHelper.IsAbsolutePath(path));
        }

        [Fact]
        public void PathHelperIsAbsolutePath_WithNonAbsolutePath_ReturnsTrue()
        {
            Assert.False(PathHelper.IsAbsolutePath(RELATIVE_PATH));
        }
    }
}
