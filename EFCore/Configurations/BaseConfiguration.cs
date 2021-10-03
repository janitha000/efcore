using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.Configurations
{
    public class BaseConfiguration
    {
        public string Type { get; set; }
        public string Url { get; set; }
        public string Path { get; set; }
    }
}
