namespace Infrastructure.Services
{
    public class BinaryService
    {
        public long GetBinaryNumberFromString(string binaryString)
        {
            long value = 0;
            foreach (var character in binaryString)
            {
                value = 2 * value;
                if (character == '1')
                {
                    value++;
                }
            }

            return value;
        }
    }
}
