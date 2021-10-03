using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Application.Models.Bricks
{
    public class Tag
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Title { get; set; } = string.Empty;
        public List<Brick> Bricks { get; set; } = new();
    }
}
