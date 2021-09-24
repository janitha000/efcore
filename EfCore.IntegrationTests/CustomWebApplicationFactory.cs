using EFCore.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;

namespace EfCore.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder()
                .UseStartup<TStartup>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                         d => d.ServiceType ==
                             typeof(DbContextOptions<ApplicationDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase(databaseName: "ToDoTest"));

                var sp = services.BuildServiceProvider();

                //using (var scope = sp.CreateScope())
                //{
                //    var scopedServices = scope.ServiceProvider;
                //    var db = scopedServices.GetRequiredService<ApplicationDbContext>();
           

                   

                //    db.Database.EnsureCreated();

                //    try
                //    {
                //        Utilities.InitializeDbForTests(db);
                //    }
                //    catch (Exception ex)
                //    {
                //        logger.LogError(ex, "An error occurred seeding the " +
                //            "database with test messages. Error: {Message}", ex.Message);
                //    }
                //}
            });
        }
    }
}
