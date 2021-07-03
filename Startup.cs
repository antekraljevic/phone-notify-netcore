using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PhoneNotify.Middleware;
using PhoneNotifySoapService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace PhoneNotify
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddNewtonsoftJson();

            services.AddHttpClient();

            services.AddControllers();

            services.AddScoped<PhoneNotifySoap>(provider => {
                var client = new PhoneNotifySoapClient(PhoneNotifySoapClient.EndpointConfiguration.PhoneNotifySoap12);

                client.ClientCredentials.UserName.UserName = Configuration["PhoneNotifySoapService:Username"];
                client.ClientCredentials.UserName.Password = Configuration["PhoneNotifySoapService:Password"];

                return client;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "PhoneNotify", Version = "v2" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "PhoneNotify v2");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
