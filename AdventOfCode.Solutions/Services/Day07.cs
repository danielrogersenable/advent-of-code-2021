using System;
using System.Linq;
using Infrastructure.Services;

namespace AdventOfCode.Solutions.Services
{
    public class Day07 : BaseDayService
    {
        public override long SolvePart1(bool useSample)
        {
            var input = ParseSingleRowInputToNumberList(useSample)
                .OrderBy(o => o)
                .ToList();

            int medianValue;

            if (input.Count() % 2 == 0)
            {
                medianValue = (input[input.Count() / 2] + input[input.Count() / 2 - 1]) / 2;
            }
            else
            {
                medianValue = input[(input.Count()) / 2];
            }

            return input.Select(i => Math.Abs(i - medianValue)).Sum();
        }

        public override long SolvePart2(bool useSample)
        {
            var input = ParseSingleRowInputToNumberList(useSample)
                .OrderBy(o => o)
                .ToList();

            int bestResult = 0;
            int location = 1;
            var shouldBreak = false;

            while (!shouldBreak)
            {
                var result = input
                    .Select(i => Math.Abs(i - location))
                    .Sum(i => (int)(0.5 * i * (i + 1)));

                if ((bestResult == 0) || (result < bestResult))
                {
                    bestResult = result;
                    location++;
                }
                else
                {
                    shouldBreak = true;
                }
            }

            return bestResult;
        }
    }
}
