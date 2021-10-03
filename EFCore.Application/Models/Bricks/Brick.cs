using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Application.Models.Bricks
{
    public class Brick
    {
        public int Id { get; set; }
        public Colors? Color { get; set; }
        [MaxLength(250)]
        public string Title { get; set; } = string.Empty;

        public List<Tag> Tags { get; set; } = new();
        public List<BrickAvailability> Availability { get; set; } = new();
    }

    public enum Colors
    {
        Red,
        Green,
        Black,
        Yellow
    }

    public class BasePlate : Brick
    {
        public int Length { get; set; }
        public int Width { get; set; }
    }

    public class MiniHead : Brick
    {
        public bool IsDualSided { get; set; }
    }
}
