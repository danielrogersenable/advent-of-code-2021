using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Solutions.Models.Day11;

namespace AdventOfCode.Solutions.Services
{
    public class Day11 : BaseDayService
    {
        public override long SolvePart1(bool useSample)
        {
            var input = ParseInputToString(useSample)
                .Select(i => i.ToCharArray().Select(i => int.Parse(i.ToString())).ToArray())
                .ToArray();

            var octopi = new List<Octopus>();

            for (var x = 0; x < input.Length; x++)
            {
                for (var y = 0; y < input[x].Length; y++)
                {
                    octopi.Add(new Octopus(x, y, input[x][y]));
                }
            }

            var timeStep = 0;
            var flashCount = 0;

            while (timeStep < 100)
            {
                timeStep++;

                // Increase energy of each octopus by 1
                octopi.ForEach(o => o.EnergyLevel++);

                // Handle flashes
                while (octopi.Where(o => o.EnergyLevel > 9 && !o.HasFlashed).Any())
                {
                    var flashingOctopus = octopi.Where(o => o.EnergyLevel > 9 && !o.HasFlashed).First();
                    flashingOctopus.HasFlashed = true;
                    flashCount++;

                    octopi.ForEach(o =>
                    {
                        if (Math.Abs(o.X - flashingOctopus.X) <= 1 && Math.Abs(o.Y - flashingOctopus.Y) <= 1)
                        {
                            o.EnergyLevel++;
                        }
                    });
                }

                // Reduce energy of flashers
                octopi.ForEach(o =>
                {
                    if (o.HasFlashed)
                    {
                        o.EnergyLevel = 0;
                        o.HasFlashed = false;
                    }
                });
            }

            return flashCount;
        }

        public override long SolvePart2(bool useSample)
        {
            var input = ParseInputToString(useSample)
                .Select(i => i.ToCharArray().Select(i => int.Parse(i.ToString())).ToArray())
                .ToArray();

            var octopi = new List<Octopus>();

            for (var x = 0; x < input.Length; x++)
            {
                for (var y = 0; y < input[x].Length; y++)
                {
                    octopi.Add(new Octopus(x, y, input[x][y]));
                }
            }

            var timeStep = 0;
            var haveAllFlashedSimultaneously = false;

            while (!haveAllFlashedSimultaneously)
            {
                timeStep++;

                // Increase energy of each octopus by 1
                octopi.ForEach(o => o.EnergyLevel++);

                // Handle flashes
                while (octopi.Where(o => o.EnergyLevel > 9 && !o.HasFlashed).Any())
                {
                    var flashingOctopus = octopi.Where(o => o.EnergyLevel > 9 && !o.HasFlashed).First();
                    flashingOctopus.HasFlashed = true;

                    octopi.ForEach(o =>
                    {
                        if (Math.Abs(o.X - flashingOctopus.X) <= 1 && Math.Abs(o.Y - flashingOctopus.Y) <= 1)
                        {
                            o.EnergyLevel++;
                        }
                    });
                }

                if (!octopi.Where(o => !o.HasFlashed).Any())
                {
                    haveAllFlashedSimultaneously = true;
                }

                // Reduce energy of flashers
                octopi.ForEach(o =>
                {
                    if (o.HasFlashed)
                    {
                        o.EnergyLevel = 0;
                        o.HasFlashed = false;
                    }
                });
            }

            return timeStep;
        }
    }
}
