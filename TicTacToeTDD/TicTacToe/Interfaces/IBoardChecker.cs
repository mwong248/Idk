namespace TicTacToe.Interfaces;

public interface IBoardChecker
{
    bool IsGameWon(SymbolEnum[,] grid, string playerSymbol);
    bool IsGameDrawn(SymbolEnum[,] grid);
    bool IsSquareOccupied(SymbolEnum[,] grid, Move playerMove);
}