using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.IO;
using System.Threading.Tasks;

namespace pugzor.core
{
    public interface IPugRendering
    {
        Task<string> Render(FileInfo pugFile, object model, ViewDataDictionary viewData, ModelStateDictionary modelState);
    }
}