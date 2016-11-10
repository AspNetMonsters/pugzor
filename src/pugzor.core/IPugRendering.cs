using System.IO;
using System.Threading.Tasks;

namespace pugzor.core
{
    public interface IPugRendering
    {
        Task<string> Render(FileInfo pugFile, object model);
    }
}