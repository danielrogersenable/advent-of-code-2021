namespace AdventOfCode.Solutions.Models.Day06
{
    public class LanternfishModel
    {
        public int Age { get; set; }
        public long Count { get; set; }

        public void UpdateAge()
        {
            if (Age == 0)
            {
                Age = 6;
            }
            else
            {
                Age--;
            }
        }
    }
}
