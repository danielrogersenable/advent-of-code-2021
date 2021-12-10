using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Solutions.Models.Day05;
using Infrastructure.Services;

namespace AdventOfCode.Solutions.Services
{
    public class Day05 : BaseDayService
    {
        public override long SolvePart1(bool useSample)
        {
            var input = ParseInputToString(useSample);

            var parsedInput = input
                .Select(i => i.Replace(" -> ", ","))
                .Select(i => i.Split(",").Select(j => int.Parse(j)).ToList())
                .Select(i => new Vent
                {
                    StartCoordinate = new Coordinate(i[0], i[1]),
                    EndCoordinate = new Coordinate(i[2], i[3])
                })
                .ToList();

            var overlap = parsedInput
                .SelectMany(p => p.VentRange)
                .GroupBy(p => string.Concat(p.X, ",", p.Y))
                .Where(p => p.Count() > 1)
                .ToList();

            var outputCount = overlap.Count();
            return outputCount;
        }

        public override long SolvePart2(bool useSample)
        {
            var input = ParseInputToString(useSample);

            var parsedInput = input
                .Select(i => i.Replace(" -> ", ","))
                .Select(i => i.Split(",").Select(j => int.Parse(j)).ToList())
                .Select(i => new VentWithDiagonal
                {
                    StartCoordinate = new Coordinate(i[0], i[1]),
                    EndCoordinate = new Coordinate(i[2], i[3])
                })
                .ToList();

            var overlap = parsedInput
                .SelectMany(p => p.VentRange)
                .GroupBy(p => string.Concat(p.X, ",", p.Y))
                .Where(p => p.Count() > 1)
                .ToList();

            var outputCount = overlap.Count();
            return outputCount;
        }
    }
}
