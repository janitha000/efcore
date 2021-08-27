using EFCore.Application.Dtos;
using EFCore.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Application.Interfaces
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> GetTodoItems();
        Task<TodoItem> GetTodoItem(long id);
        Task<IEnumerable<TodoItemDto>> GetToDoItemsByStatus(bool done);
        Task PutTodoItem(long id, TodoItem todoItem);
        Task<TodoItem> PostTodoItem(TodoItem todoItem);
        Task<bool> DeleteTodoItem(long id);
        bool TodoItemExists(long id);

    }
}
