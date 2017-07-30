using System.IO;

namespace Pugzor.Core.Helpers
{
    public class TemporaryDirectoryHelper
    {
        private static string _tempDirectory;

        public TemporaryDirectoryHelper()
        {
            _tempDirectory = CreateTemporaryDirectoryPath();
        }

        private static string CreateTemporaryDirectoryPath() => Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

        public static string CreateTemporaryDirectory()
        {
            _tempDirectory = _tempDirectory ?? CreateTemporaryDirectoryPath();

            if (!Directory.Exists(_tempDirectory))
            {
                Directory.CreateDirectory(_tempDirectory);
            }

            return _tempDirectory;
        }
    }
}
