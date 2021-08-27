using EFCore.Application.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EFCore.Application.Interfaces
{
    public interface IApplicationDbContext : IDisposable
    {
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<TodoCategory> TodoCategories { get; set; }
        public Task<int> SaveChangesAsync();
        void MarkAsModified(TodoItem item);
    }
}
