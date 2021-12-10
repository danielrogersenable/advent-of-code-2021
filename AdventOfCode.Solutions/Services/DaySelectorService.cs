using System;

namespace AdventOfCode.Solutions.Services
{
    public class DaySelectorService
    {
        private readonly IServiceProvider _serviceProvider;

        public DaySelectorService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void SelectDay()
        {
            Console.WriteLine("Enter your day and part in the form day-part; e.g. 2-1 for day 2, part 1.");
            Console.WriteLine("Optionally append '-s' to use sample data - e.g. 2-1-s");
            Console.WriteLine("Enter choice here:")
            var daySelection = Console.ReadLine();

            var dayAndPartSelection = daySelection.Split("-");

            if(!int.TryParse(dayAndPartSelection[0], out int dayNumberSelection))
            {
                throw new InvalidCastException("That's not a number!");
            }

            if (!int.TryParse(dayAndPartSelection[1], out int partSelection))
            {
                throw new InvalidCastException("That's not a number!");
            }

            if (dayAndPartSelection.Length == 3 && dayAndPartSelection[2].ToLower() != "s")
            {
                throw new InvalidOperationException("Only 's' can be used as a suffix");
            }

            var dayNumberSelectionPaddedString = string.Concat("0", dayNumberSelection);
            var serviceTypeName = string.Concat("AdventOfCode.Solutions.Services.Day", dayNumberSelectionPaddedString.Substring(dayNumberSelectionPaddedString.Length - 2, 2));
            var serviceType = Type.GetType(serviceTypeName);

            var dayService = (IDayService)_serviceProvider.GetService(serviceType);

            var useSample = dayAndPartSelection.Length == 3;

            long result = -1;

            switch (partSelection)
            {
                case 1:
                    result = dayService.SolvePart1(useSample);
                    break;
                case 2:
                    result = dayService.SolvePart2(useSample);
                    break;
                default:
                    break;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(result);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
