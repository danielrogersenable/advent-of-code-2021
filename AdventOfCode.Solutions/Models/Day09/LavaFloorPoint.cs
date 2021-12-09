using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Models.Day09
{
    public class LavaFloorPoint
    {
        public LavaFloorPoint(int x, int y, int height)
        {
            X = x;
            Y = y;
            Height = height;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public Direction DirectionToBasin { get; set; }
        public int? BasinId { get; set; }
    }
}
