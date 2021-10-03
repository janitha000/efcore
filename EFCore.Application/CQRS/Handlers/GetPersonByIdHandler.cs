using EFCore.Application.CQRS.Queries;
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
    public class GetPersonByIdHandler : IRequestHandler<GetPersonByIdQuery, Person>
    {
        private readonly IPersonContext _context;

        public GetPersonByIdHandler(IPersonContext context)
        {
            _context = context;
        }

        public async Task<Person> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            var person = await _context.Persons.FindAsync(request.Id);
            return person;
        }
    }
}
