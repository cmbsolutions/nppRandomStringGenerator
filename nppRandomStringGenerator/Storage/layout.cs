using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nppRandomStringGenerator.Storage
{
    internal class layout
    {
        public string Name { get; set; }
        public string MaxLength { get; set; }
        public string Mask { get; set; }
        public bool Required { get; set; }
    }
}
