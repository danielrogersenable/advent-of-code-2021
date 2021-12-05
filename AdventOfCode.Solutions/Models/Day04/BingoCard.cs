using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Models.Day04
{
    public class BingoCard
    {
        public BingoCard()
        {
            BingoNumbers = new List<BingoNumber>();
        }

        public List<BingoNumber> BingoNumbers { get; set; }

        private int CardWidth
        {
            get
            {
                return BingoNumbers.Select(n => n.XPosition).Max() + 1;
            }
        }

        private int CardHeight 
        {
            get
            {
                return BingoNumbers.Select(n => n.YPosition).Max() + 1;
            }
        }
        public bool IsValid
        {
            get
            {
                return CardWidth == CardHeight && CardWidth * CardHeight == BingoNumbers.Count;
            }
        }

        private bool HasCompleteRow
        {
            get
            {
                return BingoNumbers
                    .Where(n => !n.IsCalled)
                    .Select(n => n.XPosition)
                    .Distinct()
                    .Count() != CardWidth;
            }
        }

        private bool HasCompleteColumn
        {
            get
            {
                return BingoNumbers
                    .Where(n => !n.IsCalled)
                    .Select(n => n.YPosition)
                    .Distinct()
                    .Count() != CardWidth;
            }
        }

        public bool IsComplete
        {
            get
            {
                return HasCompleteColumn || HasCompleteRow;
            }
        }

        public void CallNumber(int value)
        {
            BingoNumbers.ForEach(n => n.IsCalled = n.IsCalled || n.Value == value);
        }

    }
}
