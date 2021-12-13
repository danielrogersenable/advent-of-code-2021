using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Models.Day13
{
    public class FoldCoordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public override int GetHashCode()
        {
            return X ^ Y;
        }

        public override bool Equals(object obj)
        {
            var castObject = (FoldCoordinate)obj;
            return X == castObject.X && Y == castObject.Y;
        }
    }
}
