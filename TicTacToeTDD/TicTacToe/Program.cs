// See https://aka.ms/new-console-template for more information

namespace TicTacToe;

public static class Program
{
    static void Main(string[] args)
    {
        var board = new Board();
        var player1 = new Player(SymbolEnum.X.GetSymbolString());
        var player2 = new Player(SymbolEnum.O.GetSymbolString());
        var game = new Game(board, player1, player2);
        var input = Console.In;
        var output = Console.Out;
        var reader = new Reader(input);
        var writer = new Writer(output);
        var boardChecker = new BoardChecker();
        var gameService = new GameService(game, reader, writer, boardChecker);
        
        gameService.Initialize();
    }
}