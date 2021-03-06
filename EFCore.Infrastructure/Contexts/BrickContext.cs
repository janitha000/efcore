using EFCore.Application.Models.Bricks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Infrastructure.Contexts
{
    public class BrickContext : IdentityDbContext
    {
        public BrickContext(DbContextOptions<BrickContext> options) : base(options) { }

        public DbSet<Brick> Bricks { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BrickAvailability> BrickAvailabilities { get; set; }
        public DbSet<Vendor> Vendors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BasePlate>().HasBaseType<Brick>();
            modelBuilder.Entity<MiniHead>().HasBaseType<Brick>();
            base.OnModelCreating(modelBuilder);
        }
    }
}


//migration :  dotnet ef migrations add Initial -p .\ -s ..\EFCore -c BrickContext
//database upodate : dotnet ef database update -p .\ -s ..\EFCore -c BrickContext