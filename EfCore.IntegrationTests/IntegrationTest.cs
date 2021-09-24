using EFCore;
using EFCore.Application.Models;
using EFCore.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EfCore.IntegrationTests
{
    public class IntegrationTest : IDisposable
    {
        protected readonly IServiceProvider serviceProvider;
        protected readonly HttpClient client;

        public IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
               .WithWebHostBuilder(builder =>
               {
                   builder.ConfigureServices(services =>
                   {
                       services.RemoveAll(typeof(ApplicationDbContext));
                       services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase(databaseName: "ToDoTest"));
                   });
               });
            serviceProvider = appFactory.Services;
            client = appFactory.CreateClient();
        }

        public void Dispose()
        {
            using var serviceScope = serviceProvider.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.EnsureDeleted();
        }

        protected async Task<TodoItem> CreateTodoAsync(TodoItem item)
        {
            var response = await client.PostAsJsonAsync("https://localhost:5001/api/TodoItems", item);
            return await response.Content.ReadAsAsync<TodoItem>();
        }
    }
}
