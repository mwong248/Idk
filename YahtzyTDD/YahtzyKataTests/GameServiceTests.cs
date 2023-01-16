using YahtzyKata;
using static YahtzyKata.YatzyCategories;

namespace YahtzyKataTests;

public class GameServiceTests
{
    private Player _player;
    private InputOutput _inputOutput;
    private Game _game;
    private GameService _gameService;
    
    public GameServiceTests()
    {
        _inputOutput = new InputOutput(new StringReader(""), new StringWriter());
        _player = new Player();
        _game = new Game(_player);
        _gameService = new GameService(_game, _inputOutput, new ScoreCalculator());
    }
    
    [Fact]
    public void GivenStringArrayWithDiceIdStrings_WhenHoldDieIsCalled_ThenDiceWithThoseIdsShouldHaveIsHeldTrue()
    {
        _gameService.HoldDie(new[] {"5", "1", "4"});
        
        Assert.True(_player.PlayerDie[4].IsHeld);
        Assert.True(_player.PlayerDie[0].IsHeld);
        Assert.True(_player.PlayerDie[3].IsHeld);
    }

    [Theory]
    [InlineData(7)]
    [InlineData(8)]
    public void GivenACategoryNumberInput_WhenPlayCategoryCalled_ThenPointsReturnedShouldNotBeNull(int userInput)
    {
        var points = _gameService.PlayCategory(userInput);
        
        Assert.NotNull(points);
    }
    
    [Theory]
    [InlineData(16)]
    public void GivenInvalidCategoryNumberInput_WhenPlayCategoryCalled_ThenNullShouldBeReturned(int userInput)
    {
        var points = _gameService.PlayCategory(userInput);
        
        Assert.Null(points);
    }
    
    [Fact]
    public void GivenCategoryNumberInputs_WhenPlayCategoryCalled_ThenCorrespondingCategoryIsRemoved()
    {
        _gameService.PlayCategory(13);

        Assert.DoesNotContain(Categories.SmallStraight, _game.CategoriesToPlay);
    }
    
    [Fact]
    public void GivenPointsToAdd_WhenUpdatePlayerCalled_ThenUpdatePlayerPoints()
    {
        _player.Points = 128;
        var pointsToAdd = 30;
        
        _gameService.UpdatePlayer(pointsToAdd);
        
        Assert.Equal(128 + 30, _player.Points);
    }
    
    [Theory]
    [InlineData(0, 2)]
    [InlineData(2, 3)]
    [InlineData(0, 4)]
    public void GivenHeldPlayerDie_WhenResetDieCalled_ThenAllDieIsHeldShouldBeSetToFalse(int start, int end)
    {
        for (var i = start; i <= end; i++)
        {
            _player.PlayerDie[i].IsHeld = true;
        }
        
        _gameService.ResetDie();
        
        for (var i = start; i <= end; i++)
        {
            Assert.False(_player.PlayerDie[i].IsHeld);
        }
    }
    
    [Fact]
    public void GivenANewGame_WhenCategoriesIsEmptyCalled_ThenReturnFalse()
    {
        Assert.False(_gameService.CategoriesIsEmpty());
    }
    
    [Fact]
    public void GivenAFinishedGame_WhenCategoriesIsEmptyCalled_ThenReturnTrue()
    {
        for (var i = 1; i <= 15; i++)
        {
            _gameService.PlayCategory(i);
        }

        Assert.True(_gameService.CategoriesIsEmpty());
    }
}