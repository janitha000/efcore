using EFCore.Application.Interfaces;
using EFCore.Application.Models;
using EFCore.Controllers;
using EFCore.Infrastructure;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.IntegrationTests.ToDo
{
    public class ToDoControllerTests_Mock
    {
        private IToDoService service;
        //Following tests are not integration tests, but rather unit tests
        [Test]
        public async Task GetTodoItems_ShouldReturnToDoItems_Mock()
        {
            var repositoryStub = new Mock<ITodoRepository>();
            var mockToDoItems = new TodoItem[]
            {
                new TodoItem() {Id=1, Title="Test 1", Done=false},
                new TodoItem() {Id=2, Title="Test 2", Done=true},
                new TodoItem() {Id=3, Title="Test 3", Done=false},
            };

            repositoryStub.Setup(repo => repo.GetTodoItems()).ReturnsAsync(mockToDoItems);
            var controller = new TodoItemsController(repositoryStub.Object, service);

            var result = await controller.GetTodoItems();

            result.Result.Should().BeOfType<OkObjectResult>();
            //result.Value.Should().NotBeNull();
            //result.Value.Should().HaveCount(3);
        }

        [Test]
        public async Task GetTodoItem_ShouldReturnNull_NoItem()
        {
            var repositoryStub = new Mock<ITodoRepository>();
            repositoryStub.Setup(repo => repo.GetTodoItem(It.IsAny<long>())).ReturnsAsync((TodoItem)null);
            var controller = new TodoItemsController(repositoryStub.Object, service);

            var result = await controller.GetTodoItem(111);

            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public async Task GetTodoItem_ShouldReturnItem()
        {
            var repositoryStub = new Mock<ITodoRepository>();
            var expectedItem = new TodoItem() { Id = 1, Title = "Test 1", Done = false };

            repositoryStub.Setup(repo => repo.GetTodoItem(It.IsAny<long>())).ReturnsAsync(expectedItem);
            var controller = new TodoItemsController(repositoryStub.Object, service);

            var result = await controller.GetTodoItem(111);

            result.Value.Should().BeEquivalentTo(expectedItem, options => options.ComparingByMembers<TodoItem>());

        }

        [Test]
        public async Task PostTodoItem_ShouldCreateItem()
        {
            var repositoryStub = new Mock<ITodoRepository>();
            var inputItem = new TodoItem() {Title = "Test 1", Done = false };

            var controller = new TodoItemsController(repositoryStub.Object, service);

            var result = await controller.PostTodoItem(inputItem);

            var createdItem = (result.Result as CreatedAtActionResult).Value as TodoItem;

            createdItem.Should().BeEquivalentTo(inputItem, options => options.ComparingByMembers<TodoItem>().ExcludingMissingMembers());
        }
    }
}
