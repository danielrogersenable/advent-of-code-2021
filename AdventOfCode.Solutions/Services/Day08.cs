using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Services;

namespace AdventOfCode.Solutions.Services
{
    public class Day08 : BaseDayService
    {
        public override long SolvePart1(bool useSample)
        {
            var input = ParseInputToString(useSample)
                .Select(o => o.Split('|'))
                .Select(o => o.Select(i => i.Split(' ')).ToList())
                .Select(o => o[1])
                .SelectMany(o => o)
                .ToList();

            var oneCount = input.Where(o => o.Length == 2).Count();
            var fourCount = input.Where(o => o.Length == 4).Count();
            var sevenCount = input.Where(o => o.Length == 3).Count();
            var eightCount = input.Where(o => o.Length == 7).Count();

            return (oneCount + fourCount + sevenCount + eightCount);
        }

        public override long SolvePart2(bool useSample)
        {
            var input = ParseInputToString(useSample)
                .Select(o => o.Split('|'))
                .Select(o => o
                    .Select(i => i
                        .Split(' ')
                        .Where(i => !string.IsNullOrEmpty(i)))
                    .ToList())
                .ToList();

            var readings = input.Select(o => o[0]).ToList();
            var outputs = input.Select(o => o[1]).ToList();

            var cumulativeTotal = 0;

            for (var index = 0; index < readings.Count; index++)
            {
                var reading = readings[index];
                var output = outputs[index];

                // 1 has length 2
                // 7 has length 3
                // 4 has length 4
                // 2,3,5 has length 5
                // 0,6,9 has length 6
                // 8 has length 7
                // We can evaluate the mappings as follows (using capital letters for the original value):
                // *A* is the letter included in 7 and missing from 1.
                // B and D are the letters included in 4 and missing from 1.
                // A, D and G are the letters common to 2, 3, 5 (the items of length 5).
                // Thus *D* is the intersection of the common letters of items of length 5 and the letters in 4 missing from 1.
                // And so *G* is the remaining letter common to the items of length 5.
                // And *B* is the remaining letter from those included in 4 and missing from 1.
                // E and B are the letters which appear in exactly one of the items of length 5.
                // Hence *E* can be determined from this.
                // The item of length 5 which contains E (and A, D and G) also contains *C".
                // C and F are the letters in 1.
                // So *F* is left.
                var length2 = reading.Where(r => r.Length == 2).Single().ToCharArray();
                var length3 = reading.Where(r => r.Length == 3).Single().ToCharArray();
                var length4 = reading.Where(r => r.Length == 4).Single().ToCharArray();
                var length5 = reading.Where(r => r.Length == 5).Select(r => r.ToCharArray()).ToArray();
                var length6 = reading.Where(r => r.Length == 6).Select(r => r.ToCharArray()).ToArray();

                var A = length3.Where(l => !length2.Contains(l)).Single();
                var BandD = length4.Where(l => !length2.Contains(l)).ToArray();
                var ADandG = length5
                    .SelectMany(l => l)
                    .GroupBy(l => l)
                    .Where(l => l.Count() == 3)
                    .Select(l => l.Key)
                    .ToArray();
                var D = BandD.Where(l => ADandG.Contains(l)).Single();
                var G = ADandG.Where(l => l != D && l != A).Single();
                var B = BandD.Where(l => l != D).Single();
                var EandB = length5
                    .SelectMany(l => l)
                    .GroupBy(l => l)
                    .Where(l => l.Count() == 1)
                    .Select(l => l.Key)
                    .ToArray();
                var E = EandB.Where(l => l != B).Single();
                var C = length5.Where(l => l.Contains(E)).Single().Where(l => !ADandG.Contains(l) && l != E).Single();
                var F = length2.Where(l => l != C).Single();

                var mappedOutput = output
                    .Select(o => o
                        .ToCharArray()
                        .Select(p =>
                            p == A ? 'a' :
                            p == B ? 'b' :
                            p == C ? 'c' :
                            p == D ? 'd' :
                            p == E ? 'e' :
                            p == F ? 'f' :
                            'g')
                        .OrderBy(p => p)
                        .ToArray())
                    .Select(o => new string(o))
                    .Select(o =>
                        o == "abcefg" ? 0 :
                        o == "cf" ? 1 :
                        o == "acdeg" ? 2 :
                        o == "acdfg" ? 3 :
                        o == "bcdf" ? 4 :
                        o == "abdfg" ? 5 :
                        o == "abdefg" ? 6 :
                        o == "acf" ? 7 :
                        o == "abcdefg" ? 8 :
                        o == "abcdfg" ? 9 :
                        throw new NotSupportedException())
                    .ToList();

                var resultingNumber = int.Parse(string.Concat(mappedOutput));

                cumulativeTotal += resultingNumber;
            }

            return cumulativeTotal;
        }
    }
}
