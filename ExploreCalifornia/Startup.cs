using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ExploreCalifornia
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); //zabezpecuje zobrazenie developerskej error page
            }
            else
            {
                app.UseExceptionHandler("/error.html");
            }

            app.Use(async(context, next) =>
            {
                if (context.Request.Path.Value.StartsWith("/invalid"))
                {
                    throw new Exception("Error");
                }

                await next();
            }); 

            app.UseFileServer(); //middleware sa pokusi zadanu URL previest na subor v wwwroot lokacii
            //wwwroot --> specialny folder oddelujuci staticke subory, ktore maju byt k dispozicii pre uzivatelov aplikacie od zvysku aplikacneho kodu


            //app.Use(async (context, next) => //Use + next --> definovania, ze ma byt po vykonani tohto middleware vykonany aj nasledujuci
            //{
            //    if (context.Request.Path.Value.StartsWith("/hello")) //middleware vykonany len pri urcitej URL
            //    {
            //        await context.Response.WriteAsync("Hello ASP.NET Core!");
            //    }
            //    await next();
            //});

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("How are you?");
            //});
        }
    }
}
