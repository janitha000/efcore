using EFCore.Application.CQRS.Commands;
using EFCore.Application.Interfaces;
using EFCore.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EFCore.Application.CQRS.Handlers
{
    public class InsertPersonHandler : IRequestHandler<InsertPersonCommand, Person>
    {
        private readonly IPersonContext _context;

        public InsertPersonHandler(IPersonContext context)
        {
            _context = context;
        }

        public async Task<Person> Handle(InsertPersonCommand request, CancellationToken cancellationToken)
        {
            var person = new Person() { FirstName = request.FirstName, LastName = request.LastName };
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return person;
        }
    }
}
