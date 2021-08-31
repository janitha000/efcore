using EFCore.Application.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Application.Interfaces
{
    public interface IPersonContext : IDisposable
    {
        public DbSet<Person> Persons { get; set; }
        public Task<int> SaveChangesAsync();
        void MarkAsModified(Person  person);
    }
}
