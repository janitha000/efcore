using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFCore.Application.Models;
using EFCore.Application.Interfaces;
using MediatR;
using EFCore.Application.CQRS.Queries;
using EFCore.Application.CQRS.Commands;
using AutoMapper;
using EFCore.Application.Dtos;

namespace EFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IMediator _mediatR;
        private readonly IMapper _mapper;

        public PeopleController(IMediator mediatR, IMapper mapper)
        {
            _mediatR = mediatR;
            _mapper = mapper;
        }

        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
            return await _mediatR.Send(new GetPersonListQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _mediatR.Send(new GetPersonByIdQuery(id));

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        [HttpGet("automapper/{id}")]
        public async Task<ActionResult<PersonDto>> GetPersonMapper(int id)
        {
            Person person = new()
            {
                Id = 1,
                FirstName = "Janitha",
                LastName = "Tennakoon"
            };

            var personDto = _mapper.Map<PersonDto>(person);
            return Ok(personDto);
            
        }

        //// PUT: api/People/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            try
            {
                var updatedPerson = await _mediatR.Send(new UpdatePersonCommand(id, person));
                return Ok(person);

            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return NoContent();
            }

        }

        // POST: api/People
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(string firstName, string lastName)
        {
            var person = await _mediatR.Send(new InsertPersonCommand(firstName, lastName));

            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        //// DELETE: api/People/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePerson(int id)
        //{
        //    var person = await _context.Persons.FindAsync(id);
        //    if (person == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Persons.Remove(person);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool PersonExists(int id)
        //{
        //    return _context.Persons.Any(e => e.Id == id);
        //}
    }
}
