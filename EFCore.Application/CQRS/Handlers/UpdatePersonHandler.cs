using EFCore.Application.CQRS.Commands;
using EFCore.Application.CQRS.Queries;
using EFCore.Application.Interfaces;
using EFCore.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EFCore.Application.CQRS.Handlers
{
    public class UpdatePersonHandler : IRequestHandler<UpdatePersonCommand, Person>
    {
        private readonly IPersonContext _context;
        private readonly IMediator _mediator;

        public UpdatePersonHandler(IPersonContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Person> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _mediator.Send(new GetPersonByIdQuery(request.id));
            if(person is null)
            {
                throw new Exception("Person not found");
            }

            person.FirstName = request.person.FirstName;
            person.LastName = request.person.LastName;

            _context.MarkAsModified(person);
            await _context.SaveChangesAsync();

            return person;

        }
    }
}
