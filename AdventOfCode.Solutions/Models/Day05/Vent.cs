using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Models.Day05
{
    public class Vent
    {
        public Coordinate StartCoordinate { get; set; }
        public Coordinate EndCoordinate { get; set; }

        public virtual List<Coordinate> VentRange
        {
            get
            {
                var ventRange = new List<Coordinate>();
                if (StartCoordinate.X == EndCoordinate.X)
                {
                    var minCoordinate = Math.Min(StartCoordinate.Y, EndCoordinate.Y);
                    var maxCoordinate = Math.Max(StartCoordinate.Y, EndCoordinate.Y);
                    for (var range = minCoordinate; range <= maxCoordinate; range++)
                    {
                        ventRange.Add(new Coordinate(StartCoordinate.X, range));
                    }
                }
                else if (StartCoordinate.Y == EndCoordinate.Y)
                {
                    var minCoordinate = Math.Min(StartCoordinate.X, EndCoordinate.X);
                    var maxCoordinate = Math.Max(StartCoordinate.X, EndCoordinate.X);
                    for (var range = minCoordinate; range <= maxCoordinate; range++)
                    {
                        ventRange.Add(new Coordinate(range, StartCoordinate.Y));
                    }
                }

                return ventRange;
            }
        }
    }
}
