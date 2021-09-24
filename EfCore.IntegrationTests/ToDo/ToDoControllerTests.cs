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
    public class ToDoControllerTests
    {
        private DbContextOptions<ApplicationDbContext> options;
        private ITodoRepository repository;
        private IToDoService service;

        public ToDoControllerTests()
        {
            options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "TodoTest").Options;
            repository = new Mock<ITodoRepository>().Object;
            service = new Mock<IToDoService>().Object;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var todoCategories = new TodoCategory[]
                {
                    new TodoCategory{Id = 1, Name = "Home"},
                    new TodoCategory{Id = 2, Name = "Hotel"},
                    new TodoCategory{Id = 3, Name = "Road"},
                    new TodoCategory{Id = 4, Name = "Health"},
                    new TodoCategory{Id = 5, Name = "Entertaintment"},

                };
                foreach (TodoCategory c in todoCategories)
                {
                    context.TodoCategories.Add(c);
                }
                context.SaveChanges();

                var items = new TodoItem[]
                {
                    new TodoItem{Id = 1, Title = "test 1", Completed = DateTime.Parse("2002-09-01"), Done = false , TodoCategoryId = todoCategories.Single(c => c.Name == "Home").Id },
                    new TodoItem{Id = 2, Title = "test 2", Completed = DateTime.Parse("2002-09-01"), Done = true  ,TodoCategoryId = todoCategories.Single(c => c.Name == "Home").Id},
                    new TodoItem{Id = 3, Title = "test 3", Completed = DateTime.Parse("2002-09-01"), Done = false ,TodoCategoryId = todoCategories.Single(c => c.Name == "Hotel").Id },
                    new TodoItem{Id = 4, Title = "test 4", Completed = DateTime.Parse("2002-09-01"), Done = true  ,TodoCategoryId = todoCategories.Single(c => c.Name == "Home").Id},
                    new TodoItem{Id = 5, Title = "test 5", Completed = DateTime.Parse("2002-09-01"), Done = true ,TodoCategoryId = todoCategories.Single(c => c.Name == "Entertaintment").Id},
                    new TodoItem{Id = 6, Title = "test 6", Completed = DateTime.Parse("2002-09-01"), Done = false ,TodoCategoryId = todoCategories.Single(c => c.Name == "Health").Id },

                };

                foreach (TodoItem s in items)
                {
                    context.TodoItems.Add(s);
                }

                context.SaveChanges();
            }
        }

        [Test]
        public async Task ShouldReturnAllTodoItems_InMemoryDB()
        {
            using(var context = new ApplicationDbContext(options))
            {
                var TodoControleller = new TodoItemsController(repository, service);
                var todoItems = await TodoControleller.GetTodoItems();
                var result = (todoItems.Result as OkObjectResult).Value as TodoItem[];

                result.Should().NotBeNull();
                //result.Should().HaveCount(1);

            }
        }

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
            result.Value.Should().NotBeNull();
            result.Value.Should().HaveCount(3);
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
