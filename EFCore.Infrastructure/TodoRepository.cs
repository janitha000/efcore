using EFCore.Application.Dtos;
using EFCore.Application.Interfaces;
using EFCore.Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;

namespace EFCore.Infrastructure
{
    public class TodoRepository : ITodoRepository
    {
        private readonly IApplicationDbContext _context;
        private readonly IDistributedCache _cache;
        private readonly ILogger<TodoRepository> _logger;

        public TodoRepository(IApplicationDbContext context,  IDistributedCache cache, ILogger<TodoRepository> logger)
        {
            _context = context;
            _cache = cache;
            _logger = logger;
        }
        public async Task<IEnumerable<TodoItem>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem> GetTodoItem(long id)
        {
            string recordId = id.ToString();
            var todoItem = await _cache.GetRecordAsync<TodoItem>(recordId);
            if(todoItem is null)
            {
                _logger.LogInformation($"todoItem with key ${recordId} is not availble in the cache");
                todoItem = await _context.TodoItems.FindAsync(id);
                await _cache.SetRecordAsync(recordId, todoItem, TimeSpan.FromMinutes(60));
            }
            else
            {
                _logger.LogInformation($"todoItem with key ${recordId} is not taken from cache");
            }

            return todoItem;
        }


        public async Task<IEnumerable<TodoItemDto>> GetToDoItemsByStatus(bool done)
        {
            var todoItems = await _context.TodoItems
                                        .Include(i => i.TodoCategory)
                                        .Where(i => i.Done == done)
                                        .AsNoTracking()
                                        .Select(item => new TodoItemDto()
                                        {
                                            Title = item.Title,
                                            Completed = item.Completed,
                                            Done = item.Done,
                                            description = item.ToString(),
                                            TodoItemCategoryDto = new TodoItemCategoryDto()
                                            {
                                                Name = item.TodoCategory.Name
                                            }

                                        }).ToListAsync();

            return todoItems;
        }

        public async Task<TodoItem> PostTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
        }

        public async Task PutTodoItem(long id, TodoItem todoItem)
        {

            _context.MarkAsModified(todoItem);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    throw new Exception("Item not found");
                }
                else
                {
                    throw;
                }
            }
        }
        public async Task<bool> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if(todoItem is null)
            {
                return false;
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return true;
        }

        public bool TodoItemExists(long id)
        {
            throw new NotImplementedException();
        }
    }
}
