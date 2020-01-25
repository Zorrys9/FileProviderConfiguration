using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FileProviderConfiguration
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration config)
        {
            AppConfiguration = config;

            AppConfiguration = new ConfigurationBuilder()
                .AddXmlFile("configXML.xml")
                .AddJsonFile("configJSON.json")
                .AddIniFile("configINI.ini")
                .Build();
        }
        public IConfiguration AppConfiguration { get; set; }
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async context=> {
                context.Response.ContentType = "text/html;charset=utf-8";
                await context.Response.WriteAsync($"<p style='color:{AppConfiguration["colorJSON"]}'>{AppConfiguration["json:textJSON"]}</p>" +
                    $"<p style='color:{AppConfiguration["colorXML"]}'>{AppConfiguration["xml:textXML"]}</p>" +
                    $"<p style='color:{AppConfiguration["colorINI"]}'>{AppConfiguration["textINI"]}</p>lastfile: {AppConfiguration["file"]}");
            });
        }
    }
}
