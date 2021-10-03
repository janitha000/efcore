using EFCore.Application.Interfaces;
using EFCore.Application.Models;
using EFCore.Application.Models.Bricks;
using EFCore.Infrastructure;
using EFCore.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore
{
    public static class DbInitialiser
    {
        public static void Initialize(ApplicationDbContext context, PersonContext pcontext)
        {
            context.Database.EnsureCreated();

            if (context.TodoItems.Any() && pcontext.Persons.Any())
            {
                return;
            }

            Console.WriteLine("Seeding data in to the database .......");

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

            var persons = new Person[]
            {
                new Person{Id = 1, FirstName ="Janitha", LastName = "Tennakoon"},
                new Person{Id = 2, FirstName ="Vindya", LastName = "Hettige"},
                new Person{Id = 3, FirstName ="Kavinda", LastName = "Hearath"},
                new Person{Id = 4, FirstName ="Sameera", LastName = "Kumarage"},
            };

            foreach(Person p in persons)
            {
                pcontext.Persons.Add(p);
            }

            pcontext.SaveChanges();

        }

        public static void BrickDataInitialise (BrickContext brickContext)
        {
            brickContext.Database.EnsureCreated();
            if (brickContext.Bricks.Any())
            {
                return;
            }

            Vendor brickKing, bunteSteine, heldDerSteine, brickHeaven;
            var Vendors = new Vendor[]
            {
                brickKing = new Vendor { Vendorname = "Brick King" },
                bunteSteine = new Vendor { Vendorname = "Bunte Steine" },
                heldDerSteine = new Vendor { Vendorname = "Held der Steine" },
                brickHeaven = new Vendor { Vendorname = "Brick Heaven" },
            };

            brickContext.AddRange(Vendors);

            Tag rare, ninjago, minecraft;
            var Tags = new Tag[]
            {
                rare = new Tag { Title = "Rare" },
                ninjago = new Tag { Title = "Ninjago" },
                minecraft = new Tag { Title = "Mincraft" },
            };

            brickContext.AddRange(Tags);

            var BasePlate = new BasePlate
            {
                Title = "Baseplate 16 x 16 with Island on Blue Water Pattern",
                Color = Colors.Green,
                Tags = new() { rare, minecraft },
                Length = 16,
                Width = 16,
                Availability = new List<BrickAvailability>
                {
                   new() { Vendor = bunteSteine, AvailableAmount = 5, Price = 6.6m },
                   new() { Vendor = heldDerSteine, AvailableAmount = 10, Price = 5.9m },
                }
            };

            brickContext.Add(BasePlate);

            brickContext.Add(new Brick
            {
                Title = "Brick 1 x 2 x 1",
                Color = Colors.Black
            });
            brickContext.AddAsync(new MiniHead
            {
                Title = "Minifigure, Head Dual Sided Black Eyebrows, Wide Open Mouth / Lopsided Grin",
                Color = Colors.Yellow
            });


            brickContext.SaveChanges();
        }
    }
}

