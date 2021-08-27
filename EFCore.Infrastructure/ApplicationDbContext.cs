using EFCore.Application.Interfaces;
using EFCore.Application.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.Infrastructure
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<TodoCategory> TodoCategories { get; set; }

        public void MarkAsModified(TodoItem item)
        {
            Entry(item).State = EntityState.Modified;
        }

        public async Task<int> SaveChangesAsync()
        {
            var result = await base.SaveChangesAsync();
            return result;
        }
    }
}
