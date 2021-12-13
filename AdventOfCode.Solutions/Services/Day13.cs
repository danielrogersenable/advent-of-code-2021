using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Solutions.Models.Day13;

namespace AdventOfCode.Solutions.Services
{
    public class Day13 : BaseDayService
    {
        public override long SolvePart1(bool useSample)
        {
            var input = ParseInputToString(useSample);

            var lineBreak = input.IndexOf(string.Empty);

            var points = input
                .Take(lineBreak)
                .Select(i => i.Split(",").Select(o => int.Parse(o)).ToArray())
                .Select(i => new FoldCoordinate
                {
                    X = i[0],
                    Y = i[1]
                })
                .ToList();

            var folds = input
                .Skip(lineBreak + 1)
                .Select(i => i.Replace("fold along ", string.Empty))
                .Select(i => i.Split("=").ToArray())
                .Select(i => new
                {
                    Direction = i[0],
                    Value = int.Parse(i[1])
                })
                .ToList();

            var firstFold = folds[0];

            points.ForEach(point =>
            {
                if (firstFold.Direction == "y")
                {
                    if (point.Y > firstFold.Value)
                    {
                        var distanceFromFold = point.Y - firstFold.Value;
                        point.Y -= 2 * distanceFromFold;
                    }
                }

                if (firstFold.Direction == "x")
                {
                    if (point.X > firstFold.Value)
                    {
                        var distanceFromFold = point.X - firstFold.Value;
                        point.X -= 2 * distanceFromFold;
                    }
                }
            });

            points = points.Distinct().ToList();

            return points.Count();
        }

        public override long SolvePart2(bool useSample)
        {
            var input = ParseInputToString(useSample);

            var lineBreak = input.IndexOf(string.Empty);

            var points = input
                .Take(lineBreak)
                .Select(i => i.Split(",").Select(o => int.Parse(o)).ToArray())
                .Select(i => new FoldCoordinate
                {
                    X = i[0],
                    Y = i[1]
                })
                .ToList();

            var folds = input
                .Skip(lineBreak + 1)
                .Select(i => i.Replace("fold along ", string.Empty))
                .Select(i => i.Split("=").ToArray())
                .Select(i => new
                {
                    Direction = i[0],
                    Value = int.Parse(i[1])
                })
                .ToList();

            foreach(var fold in folds)
            {
                points.ForEach(point =>
                {
                    if (fold.Direction == "y")
                    {
                        if (point.Y > fold.Value)
                        {
                            var distanceFromFold = point.Y - fold.Value;
                            point.Y -= 2 * distanceFromFold;
                        }
                    }

                    if (fold.Direction == "x")
                    {
                        if (point.X > fold.Value)
                        {
                            var distanceFromFold = point.X - fold.Value;
                            point.X -= 2 * distanceFromFold;
                        }
                    }
                });

                points = points.Distinct().ToList();
            }

            var paperWidth = points.Max(p => p.X);
            var paperHeight = points.Max(p => p.Y);

            Console.WriteLine("Note: for this one, the number returned is irrelevant. Read the console output instead");
            Console.WriteLine();

            for (var y = 0; y <= paperHeight; y++)
            {
                var stringBuilder = new StringBuilder();

                for (var x = 0; x <= paperWidth; x++)
                {
                    if (points.Where(p => p.X == x && p.Y == y).Any())
                    {
                        stringBuilder.Append('#');
                    }
                    else
                    {
                        stringBuilder.Append('.');
                    }
                }

                Console.WriteLine(stringBuilder.ToString());
            }

            return points.Count();
        }
    }
}
