using TicTacToe;

namespace TicTacToeTests;

public class PlayerTests
{
    [Fact]
    public void GivenANewPlayer_WhenConstructed_ThenPlayerSymbolShouldBeInitialized()
    {
        var symbol = "X";
        var player = new Player(symbol);
        
        Assert.Equal(symbol, player.PlayerSymbol);
    }
}