using EFCore.Application.Interfaces;
using EFCore.Application.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Infrastructure.Contexts
{
    public class PersonContext : DbContext, IPersonContext
    {
        public PersonContext(DbContextOptions<PersonContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }

        public void MarkAsModified(Person person)
        {
            Entry(person).State = EntityState.Modified;
        }

        public async Task<int> SaveChangesAsync()
        {
            var result = await base.SaveChangesAsync();
            return result;
        }
    }
}
