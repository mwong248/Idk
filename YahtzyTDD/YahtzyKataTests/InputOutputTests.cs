using YahtzyKata;

namespace YahtzyKataTests;

public class InputOutputTests
{

    private readonly StringReader _consoleInput;
    private readonly StringWriter _consoleOutput;
    private readonly InputOutput _inputOutput;
    
    public InputOutputTests()
    {
        _consoleInput = new StringReader("");
        _consoleOutput = new StringWriter();
        _inputOutput = new InputOutput(_consoleInput, _consoleOutput);
    }
    
    [Theory]
    [InlineData("3\r\n", new[] {"3"})]
    [InlineData("3,5,2\r\n", new[] {"3","5","2"})]
    public void GivenDiceIdInputs_WhenGetInputDiceIdsCalled_ThenReturnStringArrayWithDiceIds(string userInput, string[] expected)
    {
        var consoleInput = new StringReader(userInput);
        var inputOutput = new InputOutput(consoleInput, _consoleOutput);
        
        var diceIdStrings = inputOutput.GetInputDiceIds();
        
        Assert.Equal("Which dice do you want to HOLD? Enter the number(s) of the die separated by commas or just press Enter to hold none: ", _consoleOutput.ToString());
        Assert.Equal(expected, diceIdStrings);
    }
    
    [Theory]
    [InlineData("3\r\n", 3)]
    [InlineData("2\r\n", 2)]
    public void GivenACategoryNumberInput_WhenInputCategoryCalled_ThenReturnCategoryNumber(string userInput, int expected)
    {
        var consoleInput = new StringReader(userInput);
        var inputOutput = new InputOutput(consoleInput, _consoleOutput);
        var game = new Game(new Player());

        var categoryNumber = inputOutput.GetInputCategory(game.CategoriesToPlay);
        
        Assert.Equal(expected, categoryNumber);
    }
    
    [Fact]
    public void GivenAValidCategoryInputThatHasBeenPlayed_WhenValidateCategoryCalled_ThenPromptUserToReEnterCategory()
    {
        var consoleInput = new StringReader("3\r\n");
        var inputOutput = new InputOutput(consoleInput, _consoleOutput);
        var game = new Game(new Player());
        var gameService = new GameService(game, inputOutput, new ScoreCalculator());

        gameService.PlayCategory(7);
        inputOutput.ValidateCategory(7, game.CategoriesToPlay);

        Assert.Equal("Category has already been played! Please enter a different category number: ", _consoleOutput.ToString());
    }
    
    [Fact]
    public void GivenAValidCategoryInput_WhenValidateCategoryCalled_ThenReturnCategoryNumber()
    {
        var game = new Game(new Player());
        var categoryNumber = _inputOutput.ValidateCategory(3, game.CategoriesToPlay);

        Assert.Equal(3, categoryNumber);
    }

    [Fact]
    public void GivenDiceIdStrings_WhenPrintHeldDieCalled_ThenPrintHoldMessageWithDiceIds()
    {
        _inputOutput.PrintHeldDie(new[] {"5", "1", "4"});
        
        Assert.Equal("\nHOLDING dice 5\nHOLDING dice 1\nHOLDING dice 4\n", _consoleOutput.ToString());
    }
    
    [Fact]
    public void GivenPlayerDie_WhenPrintRolledDieIsCalled_ThenPrintDieWithIsHeldFalse()
    {
        var player = new Player();
        
        player.PlayerDie[1].IsHeld = true;
        player.PlayerDie[2].IsHeld = true;
        player.PlayerDie[3].IsHeld = true;

        _inputOutput.PrintRolledDie(player.PlayerDie);
        
        Assert.Equal("\nROLLING dice 1\nROLLING dice 5\n", _consoleOutput.ToString());
    }

    [Fact]
    public void GivenPlayerPointsAreAdded_WhenPrintPlayerStatsIsCalled_ThenPrintPlayerScoreMessage()
    {
        var pointsToAdd = 30;
        var player = new Player
        {
            Points = 128 + pointsToAdd
        };
        
        _inputOutput.PrintPlayerStats(pointsToAdd, player);
        Assert.Equal($"\nAdding {pointsToAdd} points...\nYou now have: {player.Points} points!\n", _consoleOutput.ToString());
    }
}