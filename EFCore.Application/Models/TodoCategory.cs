using EFCore.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.Application.Models
{
    public class TodoCategory : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TodoItem> Items { get; set; }
    }
}
