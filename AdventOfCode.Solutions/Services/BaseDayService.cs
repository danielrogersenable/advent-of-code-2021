using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Services;

namespace AdventOfCode.Solutions.Services
{
    public abstract class BaseDayService : IDayService
    {
        private readonly InputParserService _inputParserService;

        public BaseDayService() : this (new InputParserService())
        {
        }

        public BaseDayService(InputParserService inputParserService)
        {
            _inputParserService = inputParserService;
        }

        public string GetInputName(bool useSample)
        {
            var className = GetType().Name;

            var fileName = className.ToLower() + "-1";

            if (useSample)
            {
                fileName += "-sample";
            }

            return fileName + ".txt";
        }

        public IList<long> ParseInputToNumber(bool useSample)
        {
            var inputName = GetInputName(useSample);

            return _inputParserService.ParseInputToNumber("Inputs/" + inputName);
        }

        public IList<string> ParseInputToString(bool useSample)
        {
            var inputName = GetInputName(useSample);

            return _inputParserService.ParseInputToString("Inputs/" + inputName);
        }

        public IList<int> ParseSingleRowInputToNumberList(bool useSample)
        {
            var inputName = GetInputName(useSample);

            return _inputParserService.ParseSingleRowInputToNumberList("Inputs/" + inputName);
        }

        public string ParseSingleRowInputToString(bool useSample)
        {
            var inputName = GetInputName(useSample);

            return _inputParserService.ParseSingleRowInputToString("Inputs/" + inputName);
        }

        public abstract long SolvePart1(bool useSample);

        public abstract long SolvePart2(bool useSample);
    }
}
