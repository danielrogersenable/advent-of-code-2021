using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Services;

namespace AdventOfCode.Solutions.Services
{
    public class Day01 : IDayService
    {
        private readonly InputParserService _inputParserService;

        public Day01(InputParserService inputParserService)
        {
            _inputParserService = inputParserService;
        }

        public long SolvePart1()
        {
            var input = _inputParserService.ParseInputToNumber("Inputs/day01-1.txt");

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

        public long SolvePart2()
        {
            var input = _inputParserService.ParseInputToNumber("Inputs/day01-1.txt");

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
