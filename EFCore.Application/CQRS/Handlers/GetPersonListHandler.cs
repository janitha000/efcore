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
    public class GetPersonListHandler : IRequestHandler<GetPersonListQuery, List<Person>>
    {
        private readonly IPersonContext _context;

        public GetPersonListHandler(IPersonContext context)
        {
            _context = context;
        }

        public async Task<List<Person>> Handle(GetPersonListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Persons.ToListAsync();
        }
    }
}
