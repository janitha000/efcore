using EFCore.Application;
using EFCore.Application.Interfaces;
using EFCore.Application.Profiles;
using EFCore.Configurations;
using EFCore.Infrastructure;
using EFCore.Infrastructure.Contexts;
using EFCore.Middlewares;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using System.Text;

namespace EFCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
           
        }

        public IConfiguration Configuration { get; }
        public IOptions<object> Options { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure(Configuration);
            services.AddApplication(Configuration);

            services.AddTransient<RequestConsoleLoggingMiddleware>();

            services.AddControllers()
                    .AddFluentValidation(f => f.RegisterValidatorsFromAssemblyContaining<Startup>())
                    .AddNewtonsoftJson(options =>
                        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EFCore", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                { new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                    },
                 new string[] {}
                }
            });
            });

            services.Configure<BaseConfiguration>(Configuration.GetSection("BaseUrl"));
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

            services.AddAutoMapper(Assembly.GetAssembly(typeof(BrickProfile)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EFCore v1"));
            }

            if(env.IsProduction())
            {
                Console.WriteLine("Running on Producttion");

            }

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                var pcontext = serviceScope.ServiceProvider.GetService<PersonContext>();
                var bcontext = serviceScope.ServiceProvider.GetService<BrickContext>();
                DbInitialiser.Initialize(context, pcontext);
                DbInitialiser.BrickDataInitialise(bcontext);
            }

            //app.UseMiddleware<RequestLoggingMiddleware>();

            app.UseMiddleware<RequestConsoleLoggingMiddleware>();
            app.UseRequestLogging();


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
