var pug = require('pug');

module.exports = function (callback, viewPath, model, viewDictionary, modelState) {
	// Invoke some external transpiler (e.g., an NPM module) then:
    var pugCompiledFunction = pug.compileFile(viewPath);
    model = model || {};
    model.ViewData = viewDictionary;
    model.ModelState = modelState;
	callback(null, pugCompiledFunction(model));	
};