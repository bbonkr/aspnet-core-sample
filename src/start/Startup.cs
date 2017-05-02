using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

public class Startup
{
    public void Configure(IApplicationBuilder app){
        app.Run(ctx=> {
            return ctx.Response.WriteAsync("Hello, World!"); 
        });
    }
}