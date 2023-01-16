using YahtzyKata;

namespace YahtzyKataTests;

public class DiceTests
{
    private readonly Dice _dice;

    public DiceTests()
    {
        _dice = new Dice(new RandomGenerator(), 1);
    }
    
    [Fact]
    public void GivenADice_WhenDiceRolled_ThenReturnNumberBetweenOneAndSix()
    {
        var output = _dice.Roll();
        
        Assert.InRange(output, 1, 6);
    }

    [Fact]
    public void GivenADice_WhenDiceCreated_ThenReturnNumberBetweenOneAndSix()
    {
        Assert.InRange(_dice.Value, 1, 6);
    }

    [Fact]
    public void GivenADice_WhenTheRollCalled_ThenRandomNextMethodCalled()
    {
        var spyRandomGenerator = new SpyRandomGenerator();
        var dice = new Dice(spyRandomGenerator, 1);
        var output = dice.Roll();
        
        Assert.True(spyRandomGenerator.RandomCalled);
    }
}