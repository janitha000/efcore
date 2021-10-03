using EFCore.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Application.CQRS.Queries
{
    public record GetPersonListQuery() : IRequest<List<Person>>;
}
