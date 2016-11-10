using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;

namespace pugzor.core
{
    public class PugzorViewEngine : IPugzorViewEngine
    {
        private INodeServices _nodeServices;

        public PugzorViewEngine(INodeServices nodeServices)
        {
            _nodeServices = nodeServices;
        }

        public ViewEngineResult FindView(ActionContext context, string viewName, bool isMainPage)
        {            
            return ViewEngineResult.Found("Default", new PugzorView("pug1.pug", _nodeServices));
        }

        public ViewEngineResult GetView(string executingFilePath, string viewPath, bool isMainPage)
        {
            return ViewEngineResult.Found("Default", new PugzorView("pug1.pug", _nodeServices));
        }
    }
}
