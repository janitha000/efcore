using EFCore.Application.Models.Bricks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Application.Dtos
{
    public class BrickAvailabilityDto
    {
        public Vendor Vendor { get; set; }
        public int VendorId { get; set; }
        public Brick Brick { get; set; }
        public int BrickId { get; set; }
        public int AvailableAmount { get; set; }
        public decimal Price { get; set; }
    }
}
