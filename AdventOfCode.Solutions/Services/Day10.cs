using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Extensions;
using Infrastructure.Services;

namespace AdventOfCode.Solutions.Services
{
    public class Day10 : BaseDayService
    {
        public override long SolvePart1(bool useSample)
        {
            var input = ParseInputToString(useSample);

            var illegalScore = 0;

            foreach(var row in input)
            {
                var index = -1;
                var pendingSymbols = new List<char>();

                var isCorrupted = false;

                foreach(var character in row)
                {
                    index++;
                    if (isCorrupted)
                    {
                        continue;
                    }

                    char lastSymbol;

                    switch (character)
                    {
                        case '(':
                        case '[':
                        case '{':
                        case '<':
                            pendingSymbols.Add(character);
                            break;
                        case ')':
                            lastSymbol = pendingSymbols.Last();
                            if (lastSymbol != '(')
                            {
                                isCorrupted = true;
                                illegalScore += 3;
                            }
                            else
                            {
                                pendingSymbols.RemoveAt(pendingSymbols.Count() - 1);
                            }
                            break;
                        case ']':
                            lastSymbol = pendingSymbols.Last();
                            if (lastSymbol != '[')
                            {
                                isCorrupted = true;
                                illegalScore += 57;
                            }
                            else
                            {
                                pendingSymbols.RemoveAt(pendingSymbols.Count() - 1);
                            }
                            break;
                        case '}':
                            lastSymbol = pendingSymbols.Last();
                            if (lastSymbol != '{')
                            {
                                isCorrupted = true;
                                illegalScore += 1197;
                            }
                            else
                            {
                                pendingSymbols.RemoveAt(pendingSymbols.Count() - 1);
                            }
                            break;
                        case '>':
                            lastSymbol = pendingSymbols.Last();
                            if (lastSymbol != '<')
                            {
                                isCorrupted = true;
                                illegalScore += 25137;
                            }
                            else
                            {
                                pendingSymbols.RemoveAt(pendingSymbols.Count() - 1);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            return illegalScore;
        }

        public override long SolvePart2(bool useSample)
        {
            var input = ParseInputToString(useSample);
            var totalScores = new List<long>();

            foreach (var row in input)
            {
                var index = -1;
                long totalScore = 0;
                var pendingSymbols = new List<char>();

                var isCorrupted = false;

                foreach (var character in row)
                {
                    index++;
                    if (isCorrupted)
                    {
                        continue;
                    }

                    char lastSymbol;

                    switch (character)
                    {
                        case '(':
                        case '[':
                        case '{':
                        case '<':
                            pendingSymbols.Add(character);
                            break;
                        case ')':
                            lastSymbol = pendingSymbols.Last();
                            if (lastSymbol != '(')
                            {
                                isCorrupted = true;
                            }
                            else
                            {
                                pendingSymbols.RemoveAt(pendingSymbols.Count() - 1);
                            }
                            break;
                        case ']':
                            lastSymbol = pendingSymbols.Last();
                            if (lastSymbol != '[')
                            {
                                isCorrupted = true;
                            }
                            else
                            {
                                pendingSymbols.RemoveAt(pendingSymbols.Count() - 1);
                            }
                            break;
                        case '}':
                            lastSymbol = pendingSymbols.Last();
                            if (lastSymbol != '{')
                            {
                                isCorrupted = true;
                            }
                            else
                            {
                                pendingSymbols.RemoveAt(pendingSymbols.Count() - 1);
                            }
                            break;
                        case '>':
                            lastSymbol = pendingSymbols.Last();
                            if (lastSymbol != '<')
                            {
                                isCorrupted = true;
                            }
                            else
                            {
                                pendingSymbols.RemoveAt(pendingSymbols.Count() - 1);
                            }
                            break;
                        default:
                            break;
                    }
                }

                if (!isCorrupted)
                {
                    pendingSymbols.Reverse();
                    foreach (var symbol in pendingSymbols)
                    {
                        totalScore *= 5;
                        switch (symbol)
                        {
                            case '(':
                                totalScore++;
                                break;
                            case '[':
                                totalScore += 2;
                                break;
                            case '{':
                                totalScore += 3;
                                break;
                            case '<':
                                totalScore += 4;
                                break;
                            default:
                                break;
                        }
                    }

                    totalScores.Add(totalScore);
                }
            }

            return totalScores.Median();
        }
    }
}
