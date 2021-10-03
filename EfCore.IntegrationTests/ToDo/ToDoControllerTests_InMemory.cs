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
    public class ToDoControllerTests_InMemory
    {
        private DbContextOptions<ApplicationDbContext> options;
        private ITodoRepository repository;
        private IToDoService service;

        public ToDoControllerTests_InMemory()
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
            using (var context = new ApplicationDbContext(options))
            {
                var TodoControleller = new TodoItemsController(repository, service);
                var todoItems = await TodoControleller.GetTodoItems();
                var result = (todoItems.Result as OkObjectResult).Value as TodoItem[];

                result.Should().NotBeNull();
                //result.Should().HaveCount(1);

            }
        }
    }
}
