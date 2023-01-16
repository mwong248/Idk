namespace YahtzyKata;

public class Dice
{
    public int Value { get; set; }
    public int IdNumber { get; set; }
    private readonly IRandomGenerator _randomGenerator;
    public bool IsHeld { get; set; }

    public Dice(IRandomGenerator rand, int id)
    {
        _randomGenerator = rand;
        IdNumber = id;
        IsHeld = false;
        Roll();
    }

    public int Roll()
    {
        Value = _randomGenerator.GenerateNumberBetween(1, 7);

        return Value;
    }
}