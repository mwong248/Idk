using TicTacToe.Interfaces;

namespace TicTacToe;

public class Player
{
    public readonly string PlayerSymbol;
    
    public Player(string symbol)
    {
        PlayerSymbol = symbol;
    }
}