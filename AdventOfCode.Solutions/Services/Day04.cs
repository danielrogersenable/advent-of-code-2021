using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Solutions.Models.Day04;
using Infrastructure.Services;

namespace AdventOfCode.Solutions.Services
{
    public class Day04 : BaseDayService
    {
        public override long SolvePart1(bool useSample)
        {
            var input = ParseInputToString(useSample);

            var numberOrder = input[0]
                .Split(',')
                .Select(n => int.Parse(n))
                .ToList();

            var bingoCards = new List<BingoCard>();
            var currentBingoCard = new BingoCard();
            var bingoRow = 0;

            for (var index = 2; index < input.Count; index++)
            {
                var inputRow = input[index];

                if (string.IsNullOrWhiteSpace(inputRow))
                {
                    bingoCards.Add(currentBingoCard);
                    currentBingoCard = new BingoCard();
                    bingoRow = 0;
                }
                else
                {
                    var parsedRow = inputRow
                        .Split(' ')
                        .Where(n => !string.IsNullOrWhiteSpace(n))
                        .Select(n => int.Parse(n))
                        .ToList();

                    for (var columnIndex = 0; columnIndex < parsedRow.Count(); columnIndex++)
                    {
                        currentBingoCard.BingoNumbers.Add(new BingoNumber
                        {
                            XPosition = columnIndex,
                            YPosition = bingoRow,
                            Value = parsedRow[columnIndex],
                            IsCalled = false
                        });
                    }

                    bingoRow++;
                }
            }

            if (currentBingoCard.BingoNumbers.Any())
            {
                bingoCards.Add(currentBingoCard);
            }

            if (bingoCards.Where(b => !b.IsValid).Any())
            {
                var invalidCards = bingoCards.Where(b => !b.IsValid).ToList();
                throw new InvalidOperationException("Something has gone wrong with the cards");
            }

            var cardComplete = false;
            var numberCalledIndex = 0;
            long result = 0;

            while (!cardComplete)
            {
                var numberCalled = numberOrder[numberCalledIndex];
                Console.WriteLine("Called number " + numberCalled);

                bingoCards.ForEach(b => b.CallNumber(numberCalled));

                if (bingoCards.Where(b => b.IsComplete).Any())
                {
                    var completeCard = bingoCards.Where(b => b.IsComplete).Single();
                    var unmarkedNumbers = completeCard.BingoNumbers
                        .Where(n => !n.IsCalled)
                        .Select(n => n.Value)
                        .Sum();

                    result = unmarkedNumbers * numberCalled;
                    cardComplete = true;
                }

                if (!cardComplete)
                {
                    numberCalledIndex++;
                }
            }

            return result;
        }

        public override long SolvePart2(bool useSample)
        {
            var input = ParseInputToString(useSample);

            var numberOrder = input[0]
                .Split(',')
                .Select(n => int.Parse(n))
                .ToList();

            var bingoCards = new List<BingoCard>();
            var currentBingoCard = new BingoCard();
            var bingoRow = 0;

            for (var index = 2; index < input.Count; index++)
            {
                var inputRow = input[index];

                if (string.IsNullOrWhiteSpace(inputRow))
                {
                    bingoCards.Add(currentBingoCard);
                    currentBingoCard = new BingoCard();
                    bingoRow = 0;
                }
                else
                {
                    var parsedRow = inputRow
                        .Split(' ')
                        .Where(n => !string.IsNullOrWhiteSpace(n))
                        .Select(n => int.Parse(n))
                        .ToList();

                    for (var columnIndex = 0; columnIndex < parsedRow.Count(); columnIndex++)
                    {
                        currentBingoCard.BingoNumbers.Add(new BingoNumber
                        {
                            XPosition = columnIndex,
                            YPosition = bingoRow,
                            Value = parsedRow[columnIndex],
                            IsCalled = false
                        });
                    }

                    bingoRow++;
                }
            }

            if (currentBingoCard.BingoNumbers.Any())
            {
                bingoCards.Add(currentBingoCard);
            }

            if (bingoCards.Where(b => !b.IsValid).Any())
            {
                var invalidCards = bingoCards.Where(b => !b.IsValid).ToList();
                throw new InvalidOperationException("Something has gone wrong with the cards");
            }

            var cardComplete = false;
            var numberCalledIndex = 0;
            long result = 0;

            while (!cardComplete)
            {
                var numberCalled = numberOrder[numberCalledIndex];
                Console.WriteLine("Called number " + numberCalled);

                bingoCards.ForEach(b => b.CallNumber(numberCalled));

                if (bingoCards.Count == 1)
                {
                    var finalCard = bingoCards.Single();

                    if (finalCard.IsComplete)
                    {
                        var unmarkedNumbers = finalCard.BingoNumbers
                            .Where(n => !n.IsCalled)
                            .Select(n => n.Value)
                            .Sum();

                        result = unmarkedNumbers * numberCalled;
                        cardComplete = true;
                    }
                }

                if (bingoCards.Where(b => b.IsComplete).Any())
                {
                    var completeCards = bingoCards.Where(b => b.IsComplete).ToList();
                    foreach (var completeCard in completeCards)
                    {
                        bingoCards.Remove(completeCard);
                    }
                }

                if (!cardComplete)
                {
                    numberCalledIndex++;
                }
            }

            return result;
        }
    }
}
