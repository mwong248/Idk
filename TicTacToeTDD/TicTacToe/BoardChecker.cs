using TicTacToe.Interfaces;

namespace TicTacToe;

public class BoardChecker : IBoardChecker
{
    public bool IsGameWon(SymbolEnum[,] grid, string playerSymbol)
    {
        return CheckHorizontal(grid, playerSymbol) || CheckVertical(grid, playerSymbol)
                                                   || CheckTopLeftBottomRightDiagonal(grid, playerSymbol)
                                                   || CheckBottomLeftTopRightDiagonal(grid, playerSymbol);
    }

    private bool CheckHorizontal(SymbolEnum[,] grid, string playerSymbol)
    {
        for (var row = 0; row < grid.GetLength(0); row++)
        {
            for (var col = 0; col < 1; col++)
            {
                if (grid[row, col].GetSymbolString() == playerSymbol && grid[row, col + 1].GetSymbolString() == playerSymbol &&
                    grid[row, col + 2].GetSymbolString() == playerSymbol)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool CheckVertical(SymbolEnum[,] grid, string playerSymbol)
    {
        for (var col = 0; col < grid.GetLength(1); col++)
        {
            for (var row = 0; row < 1; row++)
            {
                if (grid[row, col].GetSymbolString() == playerSymbol && grid[row + 1, col].GetSymbolString() == playerSymbol &&
                    grid[row + 2, col].GetSymbolString() == playerSymbol)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool CheckTopLeftBottomRightDiagonal(SymbolEnum[,] grid, string playerSymbol)
    {
        for (var row = 0; row < 1; row++)
        {
            for (var col = 0; col < 1; col++)
            {
                if (grid[row, col].GetSymbolString() == playerSymbol && grid[row + 1, col + 1].GetSymbolString() == playerSymbol &&
                    grid[row + 2, col + 2].GetSymbolString() == playerSymbol)
                {
                    return true;
                }
            } 
        }

        return false;
    }

    private bool CheckBottomLeftTopRightDiagonal(SymbolEnum[,] grid, string playerSymbol)
    {
        for (var row = 2; row > 1; row--)
        {
            for (var col = 0; col < 1; col++)
            {
                if (grid[row, col].GetSymbolString() == playerSymbol && grid[row - 1, col + 1].GetSymbolString() == playerSymbol &&
                    grid[row - 2, col + 2].GetSymbolString() == playerSymbol)
                {
                    return true;
                }
            } 
        }

        return false;
    }

    public bool IsGameDrawn(SymbolEnum[,] grid)
    {
        var full = grid.Cast<SymbolEnum?>().All(square => square != SymbolEnum.Empty);
        var xWon = IsGameWon(grid, "X");
        var oWon = IsGameWon(grid, "O");

        return full && !xWon && !oWon;
    }

    public bool IsSquareOccupied(SymbolEnum[,] grid, Move playerMove)
    {
        return grid[playerMove.Row - 1, playerMove.Col - 1] != SymbolEnum.Empty;
    }
}