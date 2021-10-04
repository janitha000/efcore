using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Application.Models.Easy2You
{
    public class SalesOrder
    {
        public int Id { get; set; }
        public List<SalesOrderAudit> SalesOrderAudits { get; set; }
        public void AddAuditItem()
        {
            var audit = new SalesOrderAudit()
            {
                UpdatedAt = DateTime.UtcNow
            };

            SalesOrderAudits.Add(audit);
            

        }
    }
}
