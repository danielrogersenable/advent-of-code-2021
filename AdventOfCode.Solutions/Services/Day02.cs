using System;
using System.Linq;
using AdventOfCode.Solutions.Models.Day02;
using Infrastructure.Services;

namespace AdventOfCode.Solutions.Services
{
    public class Day02 : IDayService
    {
        private readonly InputParserService _inputParserService;

        public Day02(InputParserService inputParserService)
        {
            _inputParserService = inputParserService;
        }

        public long SolvePart1()
        {
            var input = _inputParserService.ParseInputToString("Inputs/day02-1.txt");

            var movements = input
                .Select(i => i.Split(" "))
                .Select(i => new DirectionModel
                {
                    Direction = i[0],
                    Units = int.Parse(i[1])
                });

            var horizontalPosition = 0;
            var verticalPosition = 0;

            foreach (var movement in movements)
            {
                switch (movement.Direction)
                {
                    case "forward":
                        horizontalPosition += movement.Units;
                        break;
                    case "down":
                        verticalPosition += movement.Units;
                        break;
                    case "up":
                        verticalPosition -= movement.Units;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }

            return (horizontalPosition * verticalPosition);
        }

        public long SolvePart2()
        {
            var input = _inputParserService.ParseInputToString("Inputs/day02-1.txt");

            var movements = input
                .Select(i => i.Split(" "))
                .Select(i => new DirectionModel
                {
                    Direction = i[0],
                    Units = int.Parse(i[1])
                });

            var horizontalPosition = 0;
            var verticalPosition = 0;
            var aim = 0;

            foreach (var movement in movements)
            {
                switch (movement.Direction)
                {
                    case "forward":
                        horizontalPosition += movement.Units;
                        verticalPosition += movement.Units * aim;
                        break;
                    case "down":
                        aim += movement.Units;
                        break;
                    case "up":
                        aim -= movement.Units;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }

            return (horizontalPosition * verticalPosition);
        }
    }
}
