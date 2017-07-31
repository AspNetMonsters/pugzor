using System.Collections.Generic;
using Pugzor.Core.Helpers;
using Xunit;

namespace Pugzor.Test
{
    [Trait("Category", "Helper")]
    public class PathHelperTests
    {
        public static IEnumerable<object[]> Paths =
            new List<object[]>
            {
                new object[]{ "/this/is/absolute.pug", true },
                new object[]{ "~/this/is/absolute.pug", true },
                new object[]{ "this/is/relative.pug", false }
            };


        [Theory]
        [MemberData(nameof(Paths))]
        public void PathHelperIsRelativePath_WithPath_ReturnsCorrectValue(string path, bool expectedResult)
        {
            Assert.Equal(!expectedResult, PathHelper.IsRelativePath(path));
        }

        [Theory]
        [MemberData(nameof(Paths))]
        public void PathHelperIsAbsolutePath_WithPath_ReturnsCorrectValue(string path, bool expectedResult)
        {
            Assert.Equal(expectedResult, PathHelper.IsAbsolutePath(path));
        }
    }
}
