namespace TicTacToe.Interfaces;

public interface IService
{
    void Initialize();
    void FillSquare(Move playerMove, string playerSymbol);
}