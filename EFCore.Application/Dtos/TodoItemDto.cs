using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.Application.Dtos
{
    public class TodoItemDto
    {

        public string Title { get; set; }

        public bool Done { get; set; }

        public DateTime? Completed { get; set; }

        public string description { get; set; }

        public TodoItemCategoryDto TodoItemCategoryDto { get; set; }
    }
}
