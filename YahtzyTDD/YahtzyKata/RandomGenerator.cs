namespace YahtzyKata;

public class RandomGenerator : IRandomGenerator
{
    public int GenerateNumberBetween(int low, int high)
    {
        return new Random().Next(low, high);
    }
}