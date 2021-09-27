using EFCore.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Application.Interfaces
{
    public interface IToDoService
    {
        TodoItem ToggleDone(TodoItem item);
        TodoItem ChangeDateNow(TodoItem item);
    }
}
