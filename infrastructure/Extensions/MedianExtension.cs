using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class MedianExtension
    {
        public static int Median(this List<int> unorderedInput)
        {
            var input = unorderedInput.OrderBy(i => i).ToList();
            if (input.Count() % 2 == 0)
            {
                return (input[input.Count() / 2] + input[input.Count() / 2 - 1]) / 2;
            }
            else
            {
                return input[(input.Count()) / 2];
            }
        }

        public static long Median(this List<long> unorderedInput)
        {
            var input = unorderedInput.OrderBy(i => i).ToList();
            if (input.Count() % 2 == 0)
            {
                return (input[input.Count() / 2] + input[input.Count() / 2 - 1]) / 2;
            }
            else
            {
                return input[(input.Count()) / 2];
            }
        }
    }
}
