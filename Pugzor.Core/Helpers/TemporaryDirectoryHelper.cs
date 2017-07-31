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

        public static string CreateTemporaryDirectory(bool forceNew = false)
        {
            _tempDirectory = _tempDirectory == null || forceNew ? CreateTemporaryDirectoryPath() : _tempDirectory;

            if (!Directory.Exists(_tempDirectory))
            {
                Directory.CreateDirectory(_tempDirectory);
            }

            return _tempDirectory;
        }
    }
}
