using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFCore.Application.Models;
using EFCore.Application.Dtos;
using EFCore.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using AutoMapper;

namespace EFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IToDoService _todoService;
        private readonly IMapper _mapper;

        public TodoItemsController(ITodoRepository todoRepository, IToDoService todoService, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _todoService = todoService;
            _mapper = mapper;

        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            var todoItems = await _todoRepository.GetTodoItems();
            var mappedTodoItems = _mapper.Map<TodoItemDto>(todoItems);
            return Ok(mappedTodoItems);
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _todoRepository.GetTodoItem(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpGet("/done/{done}")]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetToDoItemsByStatus(bool done)
        {
            var todoItems = await _todoRepository.GetToDoItemsByStatus(done);

            return Ok(todoItems);
        }

        [HttpGet("/toggle/{id}")]
        public async Task<ActionResult<TodoItem>> ToggleDone(int id)
        {
            var todoItem = await _todoRepository.GetTodoItem(id);
            if(todoItem is null)
            {
                return NotFound();
            }

            var item = _todoService.ToggleDone(todoItem);

            return Ok(item);
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {
            try
            {
                await _todoRepository.PutTodoItem(id, todoItem);
                return NoContent();
            }
            catch(Exception ex)
            {
                if(ex.Message == "Item not found")
                {
                    return NotFound();
                }
                return NoContent();
            }
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            try
            {
                await _todoRepository.PostTodoItem(todoItem);
                return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error occured {e.Message}");
                return NoContent();
            }
            
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var isDeleted = await _todoRepository.DeleteTodoItem(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        
    }
}
