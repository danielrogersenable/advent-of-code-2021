using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Models.Day15
{
    public class GraphVertex
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Risk { get; set; }
        public int? ProposedMinimumDistance { get; set; }
        public bool Confirmed { get; set; }
    }
}
