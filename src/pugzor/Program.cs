using EdgeJs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pugzor
{
    class Program
    {
        public static async Task Start()
        {
            var func = Edge.Func(@"
            let pug = require('pug');
            return function (data, callback) {
                let pugCompiledFunction = pug.compileFile('pug1.pug');
                callback(null, pugCompiledFunction(data));
            }
        ");

            Console.WriteLine(await func(new { name = "Simon" }));
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            Start().Wait();
        }
    }
}
