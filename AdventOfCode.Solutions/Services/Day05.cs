using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Solutions.Models.Day05;
using Infrastructure.Services;

namespace AdventOfCode.Solutions.Services
{
    public class Day05 : IDayService
    {
        private readonly InputParserService _inputParserService;

        public Day05(InputParserService inputParserService)
        {
            _inputParserService = inputParserService;
        }

        public long SolvePart1()
        {
            var input = _inputParserService.ParseInputToString("Inputs/day05-1.txt");

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

        public long SolvePart2()
        {
            var input = _inputParserService.ParseInputToString("Inputs/day05-1.txt");

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
