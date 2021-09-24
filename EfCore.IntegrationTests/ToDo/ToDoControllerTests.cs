using EFCore;
using EFCore.Application.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace EfCore.IntegrationTests.ToDo
{
    public class ToDoControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> appFactory;
        private readonly HttpClient client;

        public ToDoControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            appFactory = factory;
            client = appFactory.CreateClient();
        }
       

        [Fact]
        public async Task GetItems_ShouldReturnItem()
        {
            //await CreateTodoAsync(new TodoItem() { Id = 1, Title = "test" , CreatedBy="Janitha"});
            var response = await client.GetAsync("https://localhost:5001/api/TodoItems");
            var returnedItems = await response.Content.ReadAsAsync<TodoItem[]>();
            returnedItems.Should().HaveCount(6);
        }

        //[Test]
        //public async Task GetItem_ShouldReturnItem()
        //{
        //    await CreateTodoAsync(new TodoItem() { Id = 1, Title = "test" });
        //    var response = await client.GetAsync("https://localhost:5001/api/TodoItems/1");
        //    var returnedItem = await response.Content.ReadAsAsync<TodoItem>();
        //    returnedItem.Id.Should().Be(1);
        //}
    }
}
