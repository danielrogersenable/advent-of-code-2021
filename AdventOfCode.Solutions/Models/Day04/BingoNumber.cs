using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Models.Day04
{
    public class BingoNumber
    {
        public int Value { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public bool IsCalled { get; set; }
    }
}
