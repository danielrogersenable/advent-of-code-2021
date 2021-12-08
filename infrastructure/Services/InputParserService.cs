using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Infrastructure.Services
{
    public class InputParserService
    {
        public IList<string> ParseInputToString(string fileLocation)
        {
            var inputList = new List<string>();

            foreach (string line in File.ReadLines(fileLocation))
            {
                inputList.Add(line);
            }

            return inputList;
        }

        public string ParseSingleRowInputToString(string fileLocation)
        {
            var lines = File.ReadLines(fileLocation).ToList();

            return lines.First();
        }

        public List<int> ParseSingleRowInputToNumberList(string fileLocation)
        {
            var lines = File.ReadLines(fileLocation).ToList();

            return lines.First().Split(',').Select(o => int.Parse(o)).ToList();
        }

        public IList<long> ParseInputToNumber(string fileLocation)
        {
            var inputList = new List<long>();

            foreach (string line in File.ReadLines(fileLocation))
            {
                if (long.TryParse(line, out var parsedLine))
                {
                    inputList.Add(parsedLine);
                }
                else
                {
                    throw new InvalidDataException("Unable to parse to number: " + line);
                }
            }

            return inputList;
        }
    }
}
