using EFCore.Application.Interfaces;
using EFCore.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Application.Services
{
    public class ToDoService : IToDoService
    {
        public TodoItem ChangeDateNow(TodoItem item)
        {
            throw new NotImplementedException();
        }

        public TodoItem ToggleDone(TodoItem item)
        {
            item.Done = !item.Done;
            return item;
        }
    }
}
