using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;

namespace pugzor.core
{
    public class PugzorViewEngine : IViewEngine
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
