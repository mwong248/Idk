namespace YahtzyKata;

public class Program
{
    static void Main(string[] args)
    {
        var player = new Player();

        var consoleInput = Console.In;
        var consoleOutput = Console.Out;

        var game = new Game(player);
        var inputOutput = new InputOutput(consoleInput, consoleOutput);
        var scoreCalculator = new ScoreCalculator();
        
        var gameService = new GameService(game, inputOutput, scoreCalculator);
        gameService.Initialize();
    }
}