namespace TicTacToe;

public class Board
{
    public SymbolEnum[,] Grid { get; set; }

    public Board()
    {
        Grid = new[,] { { SymbolEnum.Empty, SymbolEnum.Empty, SymbolEnum.Empty },
            { SymbolEnum.Empty, SymbolEnum.Empty, SymbolEnum.Empty },
            { SymbolEnum.Empty, SymbolEnum.Empty, SymbolEnum.Empty } };
    }
}