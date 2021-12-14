using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Solutions.Models.Day14;

namespace AdventOfCode.Solutions.Services
{
    public class Day14 : BaseDayService
    {
        public override long SolvePart1(bool useSample)
        {
            var input = ParseInputToString(useSample);
            var polymer = input[0];

            var templates = input
                .Skip(2)
                .Select(i => i.Split(" -> ").ToArray())
                .Select(i => new PolymerTemplateMap
                {
                    FirstItem = i[0][0],
                    SecondItem = i[0][1],
                    ItemToInsert = i[1][0]
                })
                .ToList();

            var iterationCount = 0;

            while (iterationCount < 10)
            {
                var newPolymer = new StringBuilder();

                for (var i = 0; i < polymer.Length - 1; i++)
                {
                    var firstChar = polymer[i];
                    var secondChar = polymer[i + 1];

                    var charToInsert = templates
                        .Where(i => i.FirstItem == firstChar && i.SecondItem == secondChar)
                        .Select(i => i.ItemToInsert)
                        .Single();

                    newPolymer.Append(firstChar);
                    newPolymer.Append(charToInsert);
                }

                newPolymer.Append(polymer[polymer.Length - 1]);

                polymer = newPolymer.ToString();
                iterationCount++;
            }

            var counts = polymer.ToCharArray()
                .GroupBy(p => p)
                .Select(p => p.Count())
                .ToArray();

            return counts.Max() - counts.Min();
        }

        public override long SolvePart2(bool useSample)
        {
            var input = ParseInputToString(useSample);
            var polymer = input[0];

            var templates = input
                .Skip(2)
                .Select(i => i.Split(" -> ").ToArray())
                .Select(i => new PolymerTemplateMap
                {
                    FirstItem = i[0][0],
                    SecondItem = i[0][1],
                    ItemToInsert = i[1][0]
                })
                .ToList();

            var iterationCount = 0;

            var inputPairs = new HashSet<PolymerPair>();

            for (var i = 0; i < polymer.Length - 1; i++)
            {
                var firstChar = polymer[i];
                var secondChar = polymer[i + 1];

                if (inputPairs.Where(i => i.FirstItem == firstChar && i.SecondItem == secondChar).Any())
                {
                    inputPairs
                        .Where(i => i.FirstItem == firstChar && i.SecondItem == secondChar)
                        .Single()
                        .Count++;
                }
                else
                {
                    inputPairs.Add(new PolymerPair
                    {
                        FirstItem = firstChar,
                        SecondItem = secondChar,
                        Count = 1
                    });
                }
            }

            while (iterationCount < 40)
            {
                var newInputPairs = new HashSet<PolymerPair>();

                foreach(var pair in inputPairs)
                {
                    var template = templates
                        .Where(t => t.FirstItem == pair.FirstItem && t.SecondItem == pair.SecondItem)
                        .Single();

                    if (newInputPairs.Where(p => p.FirstItem == template.FirstItem && p.SecondItem == template.ItemToInsert).Any())
                    {
                        newInputPairs
                            .Where(p => p.FirstItem == template.FirstItem && p.SecondItem == template.ItemToInsert)
                            .Single()
                            .Count += pair.Count;
                    }
                    else
                    {
                        var newPair = new PolymerPair
                        {
                            FirstItem = template.FirstItem,
                            SecondItem = template.ItemToInsert,
                            Count = pair.Count
                        };

                        newInputPairs.Add(newPair);
                    }

                    if (newInputPairs.Where(p => p.FirstItem == template.ItemToInsert && p.SecondItem == template.SecondItem).Any())
                    {
                        newInputPairs
                            .Where(p => p.FirstItem == template.ItemToInsert && p.SecondItem == template.SecondItem)
                            .Single()
                            .Count += pair.Count;
                    }
                    else
                    {
                        var newPair = new PolymerPair
                        {
                            FirstItem = template.ItemToInsert,
                            SecondItem = template.SecondItem,
                            Count = pair.Count
                        };

                        newInputPairs.Add(newPair);
                    }
                }

                var totalCountInternal = (inputPairs.Sum(i => i.Count) + 1);
                inputPairs = newInputPairs;
                iterationCount++;
            }

            var totalCount = (inputPairs.Sum(i => i.Count) - 2) / 2;

            var characters = inputPairs.Select(i => i.FirstItem)
                .Concat(inputPairs.Select(i => i.SecondItem))
                .Distinct()
                .Select(c => new
                {
                    Item = c,
                    Count = (inputPairs.Where(i => i.FirstItem == c).Sum(i => i.Count)
                        + inputPairs.Where(i => i.SecondItem == c).Sum(i => i.Count)
                        + (polymer[0] == c ? 1 : 0)
                        + (polymer[^1] == c ? 1 : 0)) / 2
                })
                .ToList();

            return characters.Max(i => i.Count) - characters.Min(i => i.Count);
        }
    }
}
