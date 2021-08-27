using EFCore.Application.Common;
using System;

namespace EFCore.Application.Models
{
    public class TodoItem : AuditableEntity
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public bool Done { get; set; }

        public DateTime? Completed { get; set; }  

        public int TodoCategoryId { get; set; }
        public TodoCategory TodoCategory { get; set; }

        public override string ToString()
        {
            if (!Done)
            {
                return $"Id: {Id}, Title: {Title}, Done: Not Complete";
            }

            return $"Id: {Id}, Title: {Title}, Done: {Completed.Value.ToShortDateString()}";
        }
    }
}
