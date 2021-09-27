using EFCore.Application.Interfaces;
using EFCore.Application.Interfaces.SeviceInterfaces;
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
            services.AddScoped<IBitCoinService, BitCoinService>();

            services.AddHttpClient("BitCoinClient", client =>
            {
                client.BaseAddress = new Uri("https://api.coindesk.com/v1/bpi/currentprice.json");
                client.DefaultRequestHeaders.Add("Authorization", "YOUR_ASSEMBLY_AI_TOKEN");
            });
            services.AddHttpClient();
            return services;
        }
    }
}
