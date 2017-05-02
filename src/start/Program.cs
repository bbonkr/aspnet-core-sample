using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace start
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            app.Run();                
        }
    }
}
