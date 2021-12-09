using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Solutions.Models.Day09;
using Infrastructure.Services;

namespace AdventOfCode.Solutions.Services
{
    public class Day09 : IDayService
    {
        private readonly InputParserService _inputParserService;

        public Day09(InputParserService inputParserService)
        {
            _inputParserService = inputParserService;
        }

        public string SolvePart1()
        {
            var input = _inputParserService.ParseInputToString("Inputs/day09-1.txt")
                .Select(i => i.ToCharArray().Select(i => int.Parse(i.ToString())).ToArray())
                .ToArray();

            var lavaFloorPoints = new List<LavaFloorPoint>();

            for (var x = 0; x < input.Length; x++)
            {
                for (var y = 0; y < input[x].Length; y++)
                {
                    lavaFloorPoints.Add(new LavaFloorPoint(x, y, input[x][y]));
                }
            }

            var lowPoints = lavaFloorPoints
                .Where(l => !lavaFloorPoints
                    .Where(p => Math.Abs(p.X - l.X) + Math.Abs(p.Y - l.Y) == 1 && p.Height <= l.Height)
                    .Any())
                .ToList();

            return (lowPoints.Sum(l => l.Height) + lowPoints.Count()).ToString();
        }

        public string SolvePart2()
        {
            var input = _inputParserService.ParseInputToString("Inputs/day09-1.txt")
                .Select(i => i.ToCharArray().Select(i => int.Parse(i.ToString())).ToArray())
                .ToArray();

            var lavaFloorPoints = new List<LavaFloorPoint>();

            for (var x = 0; x < input.Length; x++)
            {
                for (var y = 0; y < input[x].Length; y++)
                {
                    lavaFloorPoints.Add(new LavaFloorPoint(x, y, input[x][y]));
                }
            }

            lavaFloorPoints
                .ForEach(l =>
                {
                    if (l.Height == 9)
                    {
                        l.DirectionToBasin = Direction.None;
                        return;
                    }

                    var adjacentPoints = lavaFloorPoints
                        .Where(p => Math.Abs(p.X - l.X) + Math.Abs(p.Y - l.Y) == 1);

                    var minHeightPoint = adjacentPoints
                        .Where(p => p.Height == adjacentPoints.Min(i => i.Height))
                        .First();

                    if (minHeightPoint.Height > l.Height)
                    {
                        l.DirectionToBasin = Direction.Basin;
                    }
                    else
                    {
                        if (minHeightPoint.X == l.X)
                        {
                            if (minHeightPoint.Y == l.Y + 1)
                            {
                                l.DirectionToBasin = Direction.Down;
                            }
                            else
                            {
                                l.DirectionToBasin = Direction.Up;
                            }
                        }
                        else
                        {
                            if (minHeightPoint.X == l.X + 1)
                            {
                                l.DirectionToBasin = Direction.Right;
                            }
                            else
                            {
                                l.DirectionToBasin = Direction.Left;
                            }
                        }
                    }
                });

            var basinCentres = lavaFloorPoints
                .Where(l => l.DirectionToBasin == Direction.Basin)
                .ToList();

            for (var i = 0; i < basinCentres.Count(); i++)
            {
                basinCentres[i].BasinId = i;
            }

            foreach (var lavaPoint in lavaFloorPoints)
            {
                if (lavaPoint.BasinId.HasValue || lavaPoint.Height == 9)
                {
                    continue;
                }

                var visitedPoints = new List<LavaFloorPoint>() {};
                var notAtBasin = true;

                var currentPoint = lavaPoint;

                while (notAtBasin)
                {
                    visitedPoints.Add(currentPoint);
                    switch (currentPoint.DirectionToBasin)
                    {
                        case Direction.Basin:
                            visitedPoints.ForEach(l => l.BasinId = currentPoint.BasinId);
                            notAtBasin = false;
                            break;
                        case Direction.Up:
                            currentPoint = lavaFloorPoints
                                .Where(l => l.X == currentPoint.X && l.Y == currentPoint.Y - 1)
                                .Single();
                            break;
                        case Direction.Down:
                            currentPoint = lavaFloorPoints
                                .Where(l => l.X == currentPoint.X && l.Y == currentPoint.Y + 1)
                                .Single();
                            break;
                        case Direction.Left:
                            currentPoint = lavaFloorPoints
                                .Where(l => l.X == currentPoint.X - 1 && l.Y == currentPoint.Y)
                                .Single();
                            break;
                        case Direction.Right:
                            currentPoint = lavaFloorPoints
                                  .Where(l => l.X == currentPoint.X + 1 && l.Y == currentPoint.Y)
                                  .Single();
                            break;
                        default:
                            throw new Exception("Something has gone wrong here");
                    }
                }
            }

            var results = lavaFloorPoints
                .Where(l => l.BasinId.HasValue)
                .GroupBy(l => l.BasinId.Value)
                .Select(l => new
                {
                    l.Key,
                    Count = l.Count()
                })
                .OrderByDescending(l => l.Count)
                .ToList();

            return (results[0].Count * results[1].Count * results[2].Count).ToString();
        }
    }
}
