using EFCore.Application.Interfaces;
using EFCore.Application.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration )
        {
            services.AddMediatR(typeof(MediatREntryPoint).Assembly);
            services.AddScoped<IToDoService, ToDoService>();

            return services;
        }
    }
}
