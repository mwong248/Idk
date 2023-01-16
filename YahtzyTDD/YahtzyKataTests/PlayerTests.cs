using YahtzyKata;

namespace YahtzyKataTests;

public class PlayerTests
{
    private readonly Player _player;

    public PlayerTests()
    {
        _player = new Player();
    }

    [Fact]
    public void GivenAPlayer_WhenTheyStartANewGame_ThenPointsShouldBeZero()
    {
        Assert.Equal(0, _player.Points);
    }

    [Fact]
    public void GivenAPlayer_WhenTheySelectADice_ThenReturnThatDice()
    {
        var dice = _player.SelectDice("5");

        Assert.Equal(5, dice!.IdNumber);
    }

    [Fact]
    public void GivenAPlayer_WhenTheyScore_ThenAddPoints()
    {
        _player.AddPoints(30);
        Assert.Equal(30, _player.Points);
    }

    [Fact]
    public void GivenAPlayer_WhenTheyChooseToHoldADice_ThenDiceIsHeldShouldBeTrue()
    {
        Assert.False(_player.PlayerDie[4].IsHeld);
        _player.HoldDice("5");
        Assert.True(_player.PlayerDie[4].IsHeld);
    }
}