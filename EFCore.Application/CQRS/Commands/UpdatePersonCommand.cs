using EFCore.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Application.CQRS.Commands
{
    public record UpdatePersonCommand(int id, Person person) : IRequest<Person>;
    
}
