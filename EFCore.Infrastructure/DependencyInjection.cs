using System;
using EFCore.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Distributed;
using EFCore.Infrastructure.Contexts;

namespace EFCore.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            //{

            //}

            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase(databaseName: "ToDo"));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddDbContext<PersonContext>(options => options.UseInMemoryDatabase(databaseName: "Person"));
            services.AddScoped<IPersonContext>(provider => provider.GetService<PersonContext>());

            services.AddDbContext<BrickContext>(options => options.UseSqlServer(connectionString: configuration.GetConnectionString("BricksServer")));

            services.AddScoped<ITodoRepository, TodoRepository>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
                options.InstanceName = "todo_";
            });

            return services;
        }
    }
}
