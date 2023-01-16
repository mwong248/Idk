using TicTacToe;

namespace TicTacToeTests;

public class BoardTests
{
    private readonly Board _board;
    public BoardTests()
    {
        _board = new Board();
    }

    [Fact]
    public void GivenANewGame_WhenBoardIsConstructed_Then2DArrayShouldBeInitialized()
    {
        Assert.Equal(new[,] { {SymbolEnum.Empty, SymbolEnum.Empty, SymbolEnum.Empty},
            {SymbolEnum.Empty, SymbolEnum.Empty, SymbolEnum.Empty},
            {SymbolEnum.Empty, SymbolEnum.Empty, SymbolEnum.Empty} }, _board.Grid);
    }
}