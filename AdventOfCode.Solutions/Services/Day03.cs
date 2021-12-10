using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Services;

namespace AdventOfCode.Solutions.Services
{
    public class Day03 : IDayService
    {
        private readonly InputParserService _inputParserService;
        private readonly BinaryService _binaryService;

        public Day03(
            InputParserService inputParserService,
            BinaryService binaryService)
        {
            _inputParserService = inputParserService;
            _binaryService = binaryService;
        }

        public long SolvePart1()
        {
            var input = _inputParserService.ParseInputToString("Inputs/day03-1.txt");

            long gamma = 0;
            long epsilon = 0;

            var inputLength = input[0].Length;

            for (var index = 0; index < inputLength; index++)
            {
                var columnEntries = input.Select(i => i[index]);
                
                var oneCount = columnEntries.Where(r => r == '1').Count();
                var zeroCount = columnEntries.Where(r => r == '0').Count();

                if (oneCount > zeroCount)
                {
                    gamma = 2 * gamma + 1;
                    epsilon = 2 * epsilon;
                }
                else
                {
                    gamma = 2 * gamma;
                    epsilon = 2 * epsilon + 1;
                }
            }

            Console.WriteLine("gamma: " + gamma);
            Console.WriteLine("epsilon: " + epsilon);

            return (gamma * epsilon);
        }

        public long SolvePart2()
        {
            var input = _inputParserService.ParseInputToString("Inputs/day03-1.txt");

            var inputLength = input[0].Length;

            var oxygenInput = input.ToList();

            for (var index = 0; index < inputLength; index++)
            {
                var columnEntries = oxygenInput.Select(i => i[index]);

                var oneCount = columnEntries.Where(r => r == '1').Count();
                var zeroCount = columnEntries.Where(r => r == '0').Count();

                if (oneCount >= zeroCount)
                {
                    oxygenInput = oxygenInput
                        .Where(i => i[index] == '1')
                        .ToList();
                }
                else
                {
                    oxygenInput = oxygenInput
                        .Where(i => i[index] == '0')
                        .ToList();
                }
            }

            var scrubberInput = input.ToList();

            for (var index = 0; index < inputLength; index++)
            {
                if (scrubberInput.Count() == 1)
                {
                    continue;
                }

                var columnEntries = scrubberInput.Select(i => i[index]);

                var oneCount = columnEntries.Where(r => r == '1').Count();
                var zeroCount = columnEntries.Where(r => r == '0').Count();

                if (oneCount >= zeroCount)
                {
                    scrubberInput = scrubberInput
                        .Where(i => i[index] == '0')
                        .ToList();
                }
                else
                {
                    scrubberInput = scrubberInput
                        .Where(i => i[index] == '1')
                        .ToList();
                }
            }

            var oxygenValue = _binaryService.GetBinaryNumberFromString(oxygenInput.Single());
            var scrubberValue = _binaryService.GetBinaryNumberFromString(scrubberInput.Single());

            Console.WriteLine("oxygen: " + oxygenValue);
            Console.WriteLine("scrubber: " + scrubberValue);

            return (oxygenValue * scrubberValue);
        }
    }
}