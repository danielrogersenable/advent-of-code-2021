using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Models.Day05
{
    public class VentWithDiagonal : Vent
    {
        public override List<Coordinate> VentRange
        {
            get
            {
                var baseRange = base.VentRange;

                if (StartCoordinate.X != EndCoordinate.X && StartCoordinate.Y != EndCoordinate.Y)
                {
                    var maxX = Math.Max(StartCoordinate.X, EndCoordinate.X);
                    var minX = Math.Min(StartCoordinate.X, EndCoordinate.X);
                    var maxY = Math.Max(StartCoordinate.Y, EndCoordinate.Y);
                    var minY = Math.Min(StartCoordinate.Y, EndCoordinate.Y);

                    if (maxX - minX == maxY - minY)
                    {
                        var hasPositiveSkew =
                            (maxX == StartCoordinate.X && maxY == StartCoordinate.Y)
                            || (minX == StartCoordinate.X && minY == StartCoordinate.Y);

                        var diff = maxX - minX;

                        for (var i = 0; i <= diff; i++)
                        {
                            if (hasPositiveSkew)
                            {
                                baseRange.Add(new Coordinate(minX + i, minY + i));
                            }
                            else
                            {
                                baseRange.Add(new Coordinate(maxX - i, minY + i));
                            }
                        }
                    }
                }

                return baseRange;
            }
        }
    }
}
