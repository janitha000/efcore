using AutoMapper;
using EFCore.Application.Dtos;
using EFCore.Application.Models.Bricks;
using EFCore.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BricksController : ControllerBase
    {
        private BrickContext _context;
        private IMapper _mapper;

        public BricksController(BrickContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("BrickAvailability")]
        public async Task<ActionResult<IEnumerable<BrickAvailability>>> GetBrickAvailability()
        {
            try
            {
                var availability = await _context.BrickAvailabilities
                                        .Include(a => a.Brick)
                                        .Include(a => a.Vendor)
                                        .ToArrayAsync();

                //var brickAvailability = new[] {

                //    new BrickAvailability()
                //    {
                //        BrickId = 1,
                //        Price = 100,
                //        Id = 2,
                //        Vendor = new Vendor() { Vendorname ="Janitha", Id = 1}

                //    }
                //};

                var dto = _mapper.Map<BrickAvailability[], IEnumerable<BrickAvailabilityDto>>(availability);

                return Ok(dto);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return NotFound();
            }
                                               
        }


        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BricksController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BricksController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BricksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BricksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
