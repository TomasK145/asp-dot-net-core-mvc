using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ExploreCalifornia
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        //IConfiguration --> ma pristup k Environment variables z property projektu, appsettings.json suboru, ...
        public Startup(IConfiguration configuration) //pridanim tohto parametra do konstruktoru zabezpeci ASP.NET Core populate s internym config objektom, je vo forme Dictionary
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddTransient --> kazde vyuzitie dependency ma vlastnu instanciu
            //services.AddScoped --> vyuzitelna dependency vramci jedneho web requestu (umoznuje sharovanie vramci daneho web requestu)
            //services.AddSingleton --> jedna instancia dependency pre cely lifetime aplikacie, pr. pre narocne instancie, pre instancie ktore niesu zavisle na konkretnych uzivatelov/requestoch, ...

            services.AddTransient<FeatureToggles>(config =>  //umoznuje definovat ako bude object inicializovany pri injektovani
            {
                return new FeatureToggles
                {
                    DeveloperExceptions = _configuration.GetValue<bool>("FeatureToggles:EnableDeveloperExceptions")
                };
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, FeatureToggles features)
        {
            loggerFactory.AddConsole();

            //if (env.IsDevelopment())
            //if (_configuration["EnableDeveloperExceptions"] == "True")
            //if (_configuration.GetValue<bool>("FeatureToggles:EnableDeveloperExceptions")) //config hodnota prevedena na pozadovany datovy typ
            if (features.DeveloperExceptions) //citanie konfiguracie z custom triedy
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

            app.UseMvc(routes =>
            {
                routes.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
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
