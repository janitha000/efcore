using System;

namespace EFCore.Application.Models.Easy2You
{
    public class SalesOrderAudit
    {
        public int Id { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}