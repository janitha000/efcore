using EFCore.Application.Interfaces;
using EFCore.Application.Models;
using EFCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore
{
    public static class DbInitialiser
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.TodoItems.Any())
            {
                return;
            }



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
}

