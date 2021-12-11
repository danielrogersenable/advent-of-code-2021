namespace AdventOfCode.Solutions.Models.Day11
{
    public class Octopus
    {
        public Octopus(int x, int y, int energyLevel)
        {
            X = x;
            Y = y;
            EnergyLevel = energyLevel;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int EnergyLevel { get; set; }
        public bool HasFlashed { get; set; }
    }
}
