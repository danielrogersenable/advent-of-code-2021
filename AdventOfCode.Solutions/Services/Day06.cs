using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Solutions.Models.Day06;
using Infrastructure.Services;

namespace AdventOfCode.Solutions.Services
{
    public class Day06 : BaseDayService
    {
        public override long SolvePart1(bool useSample)
        {
            var input = ParseInputToString(useSample);

            var lanternfishAges = input[0]
                .Split(',')
                .Select(i => int.Parse(i))
                .ToList();

            var dayCount = 0;
            var maxDayCount = 80;

            while (dayCount < maxDayCount)
            {
                var procreateCount = lanternfishAges.Where(l => l == 0).Count();

                while (procreateCount > 0)
                {
                    lanternfishAges.Add(-1);
                    procreateCount--;
                }

                lanternfishAges = lanternfishAges
                    .Select(l => 
                    {
                        if (l == -1)
                        {
                            return 8;
                        }
                        else if (l == 0)
                        {
                            return 6;
                        }
                        else
                        {
                            return l-1;
                        }
                    }).ToList();

                dayCount++;
            }

            return lanternfishAges.Count();
        }

        public override long SolvePart2(bool useSample)
        {
            var input = ParseInputToString(useSample);

            var lanternfishAges = input[0]
                .Split(',')
                .Select(i => int.Parse(i))
                .ToList();

            var lanternfish = lanternfishAges
                .GroupBy(l => l)
                .Select(k => new LanternfishModel
                {
                    Age = k.Key,
                    Count = k.Count()
                })
                .OrderByDescending(k => k.Age)
                .ToList();

            var dayCount = 0;
            var maxDayCount = 256;

            while (dayCount < maxDayCount)
            {
                var procreateCount = lanternfish.Where(l => l.Age == 0).Select(l => l.Count).SingleOrDefault();

                lanternfish.ForEach(l => l.UpdateAge());

                lanternfish.Add(new LanternfishModel
                {
                    Age = 8,
                    Count = procreateCount
                });

                lanternfish = lanternfish
                    .GroupBy(l => l.Age)
                    .Select(l => new LanternfishModel
                    {
                        Age = l.Key,
                        Count = l.Sum(f => f.Count)
                    })
                    .ToList();

                dayCount++;
            }

            return lanternfish.Sum(l => l.Count);
        }
    }
}
