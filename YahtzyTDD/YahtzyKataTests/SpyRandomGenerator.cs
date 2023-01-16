
using YahtzyKata;

namespace YahtzyKataTests;

public class SpyRandomGenerator : IRandomGenerator
{
    public bool RandomCalled { get; set; }

    public SpyRandomGenerator()
    {
        RandomCalled = false;
    }

    public int GenerateNumberBetween(int low, int high)
    {
        RandomCalled = true;
        return 7;
    }
}
