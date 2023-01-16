using TicTacToe;

namespace TicTacToeTests;

public class BoardCheckerTests
{

    public static IEnumerable<object[]> WonBoards()
    {
        yield return new object[] { new[,] { { SymbolEnum.X, SymbolEnum.X, SymbolEnum.X }, {SymbolEnum.O, SymbolEnum.Empty,SymbolEnum.O }, { SymbolEnum.Empty,SymbolEnum.O, SymbolEnum.Empty } }, SymbolEnum.X};
        yield return new object[] { new[,] { { SymbolEnum.X,SymbolEnum.O, SymbolEnum.Empty }, { SymbolEnum.Empty,SymbolEnum.O, SymbolEnum.X }, { SymbolEnum.X,SymbolEnum.O, SymbolEnum.Empty } },SymbolEnum.O};
        yield return new object[] { new[,] { { SymbolEnum.X,SymbolEnum.O,SymbolEnum.O }, { SymbolEnum.Empty, SymbolEnum.X, SymbolEnum.Empty }, { SymbolEnum.Empty,SymbolEnum.O, SymbolEnum.X } }, SymbolEnum.X};
        yield return new object[] { new[,] { { SymbolEnum.Empty, SymbolEnum.X,SymbolEnum.O }, { SymbolEnum.Empty,SymbolEnum.O, SymbolEnum.Empty }, {SymbolEnum.O, SymbolEnum.X, SymbolEnum.X } },SymbolEnum.O};
    }

    [Theory]
    [MemberData(nameof(WonBoards))]
    public void GivenAWonBoard_WhenIsGameWonCalled_ThenReturnTrue(SymbolEnum[,] wonGrid, string playerSymbol)
    {
        var checker = new BoardChecker();
        var result = checker.IsGameWon(wonGrid, playerSymbol);

        Assert.True(result);
    }
    
    public static IEnumerable<object[]> NotWonBoards()
    {
        yield return new object[] { new[,] { { SymbolEnum.X, SymbolEnum.Empty, SymbolEnum.X }, {SymbolEnum.O, SymbolEnum.Empty,SymbolEnum.O }, { SymbolEnum.Empty,SymbolEnum.O, SymbolEnum.Empty } }, SymbolEnum.X};
        yield return new object[] { new[,] { { SymbolEnum.X,SymbolEnum.O, SymbolEnum.Empty }, { SymbolEnum.Empty,SymbolEnum.O, SymbolEnum.X }, { SymbolEnum.X, SymbolEnum.Empty, SymbolEnum.Empty } },SymbolEnum.O};
        yield return new object[] { new[,] { { SymbolEnum.X,SymbolEnum.O,SymbolEnum.O }, { SymbolEnum.Empty, SymbolEnum.X, SymbolEnum.Empty }, { SymbolEnum.Empty,SymbolEnum.O, SymbolEnum.Empty } }, SymbolEnum.X};
        yield return new object[] { new[,] { { SymbolEnum.Empty, SymbolEnum.X,SymbolEnum.O }, { SymbolEnum.Empty, SymbolEnum.Empty, SymbolEnum.Empty }, {SymbolEnum.O, SymbolEnum.X, SymbolEnum.X } },SymbolEnum.O};
    }
    
    [Theory]
    [MemberData(nameof(NotWonBoards))]
    public void GivenAWonBoard_WhenIsGameWonCalled_ThenReturnFalse(SymbolEnum[,] notWonGrid, string playerSymbol)
    {
        var checker = new BoardChecker();
        var result = checker.IsGameWon(notWonGrid, playerSymbol);

        Assert.False(result);
    }
    
    public static IEnumerable<object[]> DrawnBoards()
    {
        yield return new object[] { new[,] { {SymbolEnum.O,SymbolEnum.O, SymbolEnum.X }, { SymbolEnum.X, SymbolEnum.X,SymbolEnum.O }, {SymbolEnum.O, SymbolEnum.X, SymbolEnum.X } }};
    }

    [Theory]
    [MemberData(nameof(DrawnBoards))]
    public void GivenADrawnBoard_WhenIsGameDrawnCalled_ThenReturnTrue(SymbolEnum[,] drawnGrid)
    {
        var checker = new BoardChecker();
        var result = checker.IsGameDrawn(drawnGrid);
        
        Assert.True(result);
    }
    
    public static IEnumerable<object[]> NotDrawnBoards()
    {
        yield return new object[] { new[,] { { SymbolEnum.X,SymbolEnum.O, SymbolEnum.X }, {SymbolEnum.O, SymbolEnum.X,SymbolEnum.O }, { SymbolEnum.X,SymbolEnum.O,SymbolEnum.O } }};
    }

    [Theory]
    [MemberData(nameof(NotDrawnBoards))]
    public void GivenADrawnBoard_WhenIsGameDrawnCalled_ThenReturnFalse(SymbolEnum[,] drawnGrid)
    {
        var checker = new BoardChecker();
        var result = checker.IsGameDrawn(drawnGrid);
        
        Assert.False(result);
    }
    
    public static IEnumerable<object[]> InputBoardSquareFull()
    {
        yield return new object[] { new[,] { { SymbolEnum.Empty, SymbolEnum.Empty, SymbolEnum.Empty }, { SymbolEnum.Empty, SymbolEnum.Empty,SymbolEnum.O }, { SymbolEnum.Empty, SymbolEnum.Empty, SymbolEnum.Empty } }, new Move(2, 3) };
    }

    [Theory]
    [MemberData(nameof(InputBoardSquareFull))]
    public void GivenAPlayerMoveOnFilledSquare_WhenIsSquareOccupiedCalled_ThenReturnTrue(SymbolEnum[,] grid, Move playerMove)
    {
        var checker = new BoardChecker();
        var result = checker.IsSquareOccupied(grid, playerMove);
        
        Assert.True(result);
    }
    
    public static IEnumerable<object[]> InputBoardSquareEmpty()
    {
        yield return new object[] { new[,] { { SymbolEnum.Empty, SymbolEnum.Empty, SymbolEnum.Empty }, { SymbolEnum.Empty, SymbolEnum.Empty,SymbolEnum.O }, { SymbolEnum.Empty, SymbolEnum.Empty, SymbolEnum.Empty } }, new Move(1, 2) };
    }

    [Theory]
    [MemberData(nameof(InputBoardSquareEmpty))]
    public void GivenAPlayerMoveOnEmptySquare_WhenIsSquareOccupiedCalled_ThenReturnFalse(SymbolEnum[,] grid, Move playerMove)
    {
        var checker = new BoardChecker();
        var result = checker.IsSquareOccupied(grid, playerMove);
        
        Assert.False(result);
    }
}