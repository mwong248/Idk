namespace TicTacToe.Interfaces;

public interface IWriter
{
    void PrintBoard(SymbolEnum[,] grid);
    void PrintMessage(string message);
}