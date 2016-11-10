using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pugzor.core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddNodeServices(options => {
                // Set any properties that you want on 'options' here
                options.ProjectPath = @"C:\dev\monsters\pugzor\src\pugzor.core";
            });

            var serviceProvider = services.BuildServiceProvider();
            var nodeServices = serviceProvider.GetRequiredService<INodeServices>();
            Start(nodeServices).Wait();
        }

        public static async Task Start(INodeServices nodeServices)
        {
            var result = await nodeServices.InvokeAsync<string>("./pugcompile", new { name = "Dave" });            
            Console.WriteLine(result);
            Console.ReadLine();
        }

    }
}
