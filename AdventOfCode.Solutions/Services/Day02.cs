using System;
using System.Linq;
using AdventOfCode.Solutions.Models.Day02;
using Infrastructure.Services;

namespace AdventOfCode.Solutions.Services
{
    public class Day02 : BaseDayService
    {
        public override long SolvePart1(bool useSample)
        {
            var input = ParseInputToString(useSample);

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

        public override long SolvePart2(bool useSample)
        {
            var input = ParseInputToString(useSample);

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
