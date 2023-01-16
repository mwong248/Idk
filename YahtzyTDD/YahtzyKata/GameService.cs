using static YahtzyKata.YatzyCategories;
namespace YahtzyKata;

public class GameService
{
    private Player _player;
    private List<Categories> _categoriesToPlay;
    private InputOutput _inputOutput;
    private ScoreCalculator _scoreCalculator;
    
    public GameService(Game game, InputOutput inputOutput, ScoreCalculator scoreCalculator)
    {
        _player = game.Player;
        _categoriesToPlay = game.CategoriesToPlay;
        _inputOutput = inputOutput;
        _scoreCalculator = scoreCalculator;
    }
    
    public void Initialize()
    {
        _inputOutput.WriteLine("\nWelcome To Yatzy!");
        Start();
    }

    public void Start()
    {
        while (!CategoriesIsEmpty())
        {
            for (var turnsLeft = 3; turnsLeft > 0; turnsLeft--)
            {
                _inputOutput.WriteLine("\nYour current dice values are:\n");
                _inputOutput.WriteLine($"{_player.GetDiceValuesAsString()}");
                _inputOutput.Write($"You have {turnsLeft} turn(s) remaining, do you want to roll? Enter either 'Y' or 'N': ");
                
                var decision = _inputOutput.ReadLine().Trim().ToUpper();
                if (decision == "Y")
                {
                    var diceIdStrings = _inputOutput.GetInputDiceIds();
                    HoldDie(diceIdStrings);
                    RollDie();
                    ResetDie();
                }
                else
                {
                    break;
                }
            }
            _inputOutput.WriteLine("\nYour current dice values are:\n");
            _inputOutput.WriteLine($"{_player.GetDiceValuesAsString()}");
            
            var categoryNumber = _inputOutput.GetInputCategory(_categoriesToPlay);
            categoryNumber = _inputOutput.ValidateCategory(categoryNumber, _categoriesToPlay);
            var pointsToAdd = PlayCategory(categoryNumber);
            UpdatePlayer(pointsToAdd);
        }
        _inputOutput.WriteLine("\nGame over! All categories have been played!");
    }

    
    public void HoldDie(string[] diceIdStrings)
    {
        if (diceIdStrings[0] == "ALL")
        {
            return;
        }

        _inputOutput.PrintHeldDie(diceIdStrings);
        
        foreach (var diceId in diceIdStrings)
        {
            _player.HoldDice(diceId);
        }
    }

    public void RollDie()
    {
        _inputOutput.PrintRolledDie(_player.PlayerDie);
        
        foreach (var dice in _player.PlayerDie)
        {
            if (dice.IsHeld) continue;
            dice.Roll();
        }
    }

    public void ResetDie()
    {
        foreach (var dice in _player.PlayerDie)
        {
            dice.IsHeld = false;
        }
    }
    
    public int? PlayCategory(int categoryDecision)
    {
        switch (categoryDecision)
        {
            case 1:
                _categoriesToPlay[_categoriesToPlay.IndexOf(Categories.Chance)] = Categories.Played;
                return _scoreCalculator.GetScoreForChance(_player.GetDiceValues());
            case 2:
                _categoriesToPlay[_categoriesToPlay.IndexOf(Categories.Yatzy)] = Categories.Played;
                return _scoreCalculator.GetScoreForYatzy(_player.GetDiceValues());
            case 3:
                _categoriesToPlay[_categoriesToPlay.IndexOf(Categories.SumOnes)] = Categories.Played;
                return _scoreCalculator.GetScoreForSum(_player.GetDiceValues(), 1);
            case 4:
                _categoriesToPlay[_categoriesToPlay.IndexOf(Categories.SumTwos)] = Categories.Played;
                return _scoreCalculator.GetScoreForSum(_player.GetDiceValues(), 2);
            case 5:
                _categoriesToPlay[_categoriesToPlay.IndexOf(Categories.SumThrees)] = Categories.Played;
                return _scoreCalculator.GetScoreForSum(_player.GetDiceValues(), 3);
            case 6:
                _categoriesToPlay[_categoriesToPlay.IndexOf(Categories.SumFours)] = Categories.Played;
                return _scoreCalculator.GetScoreForSum(_player.GetDiceValues(), 4);
            case 7:
                _categoriesToPlay[_categoriesToPlay.IndexOf(Categories.SumFives)] = Categories.Played;
                return _scoreCalculator.GetScoreForSum(_player.GetDiceValues(), 5);
            case 8:
                _categoriesToPlay[_categoriesToPlay.IndexOf(Categories.SumSixes)] = Categories.Played;
                return _scoreCalculator.GetScoreForSum(_player.GetDiceValues(), 6);
            case 9:
                _categoriesToPlay[_categoriesToPlay.IndexOf(Categories.Pair)] = Categories.Played;
                return _scoreCalculator.GetScoreForSinglePair(_player.GetDiceValues());
            case 10:
                _categoriesToPlay[_categoriesToPlay.IndexOf(Categories.TwoPair)] = Categories.Played;
                return _scoreCalculator.GetScoreForTwoPairs(_player.GetDiceValues());
            case 11:
                _categoriesToPlay[_categoriesToPlay.IndexOf(Categories.ThreeOfAKind)] = Categories.Played;
                return _scoreCalculator.GetScoreForHowManyOfAKind(_player.GetDiceValues(), 3);
            case 12:
                _categoriesToPlay[_categoriesToPlay.IndexOf(Categories.FourOfAKind)] = Categories.Played;
                return _scoreCalculator.GetScoreForHowManyOfAKind(_player.GetDiceValues(), 4);
            case 13:
                _categoriesToPlay[_categoriesToPlay.IndexOf(Categories.SmallStraight)] = Categories.Played;
                return _scoreCalculator.GetScoreForSmallStraight(_player.GetDiceValues());
            case 14:
                _categoriesToPlay[_categoriesToPlay.IndexOf(Categories.LargeStraight)] = Categories.Played;
                return _scoreCalculator.GetScoreForLargeStraight(_player.GetDiceValues());
            case 15:
                _categoriesToPlay[_categoriesToPlay.IndexOf(Categories.FullHouse)] = Categories.Played;
                return _scoreCalculator.GetScoreForFullHouse(_player.GetDiceValues());
            default:
                return null;
        }
    }
    
    public void UpdatePlayer(int? pointsToAdd)
    {
        _player.AddPoints(pointsToAdd ?? 0);
        _inputOutput.PrintPlayerStats(pointsToAdd, _player);
    }

    public bool CategoriesIsEmpty()
    {
        return _categoriesToPlay.All(category => category == Categories.Played);
    }
}