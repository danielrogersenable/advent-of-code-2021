using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Services;

namespace AdventOfCode.Solutions.Services
{
    public class Day01 : BaseDayService
    {
        public override long SolvePart1(bool useSample)
        {
            var input = ParseInputToNumber(useSample);

            var nextInputLargerCount = 0;

            for (var i = 0; i < input.Count-1; i++)
            {
                if (input[i+1] > input[i])
                {
                    nextInputLargerCount++;
                }
            }

            return nextInputLargerCount;
        }

        public override long SolvePart2(bool useSample)
        {
            var input = ParseInputToNumber(useSample);

            var nextInputLargerCount = 0;

            for (var i = 0; i < input.Count - 3; i++)
            {
                if (input[i + 3]  > input[i])
                {
                    nextInputLargerCount++;
                }
            }

            return nextInputLargerCount;
        }
    }
}
