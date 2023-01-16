namespace TicTacToe;

public class Game
{
    public readonly Board Board;
    public readonly Player Player1;
    public readonly Player Player2;
    public bool Xturn { get; set; }
    
    public Game(Board board, Player player1, Player player2)
    {
        Player1 = player1;
        Player2 = player2;
        Board = board;
        Xturn = true;
    }
}