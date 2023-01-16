using YahtzyKata;

namespace YahtzyKataTests;

public class ScoreCalculatorTests
{
    private ScoreCalculator _scoreCalculator;
    
    public ScoreCalculatorTests()
    {
        _scoreCalculator = new ScoreCalculator();
    }
    
    public static IEnumerable<object[]> GetDiceRollsChance()
    {
        yield return new object[] { new[] { 1, 1, 3, 3, 6 }, 14 };
        yield return new object[] { new[] { 4, 5, 5, 6, 1 }, 21 };
    }
    
    [Theory]
    [MemberData(nameof(GetDiceRollsChance))]
    public void GivenPlayerDie_WhenThePlayerSelectsChance_ThenReturnAccumulativeScore(int[] inputDiceValues, int expected)
    {

        var output = _scoreCalculator.GetScoreForChance(inputDiceValues);
        Assert.Equal(expected, output);
    }
    
    
    public static IEnumerable<object[]> GetDiceRollsYatzy()
    {
        yield return new object[] { new[] { 1, 1, 1, 1, 1 }, 50 };
        yield return new object[] { new[] { 1, 1, 1, 2, 1 }, 0 };
    }

    [Theory]
    [MemberData(nameof(GetDiceRollsYatzy))]
    public void GivenPlayerDie_WhenThePlayerSelectsYatzy_ThenReturnYatzyScore(int[] inputDiceValues, int expected)
    {
        var output = _scoreCalculator.GetScoreForYatzy(inputDiceValues);
        Assert.Equal(expected, output);
    }
    
    
    public static IEnumerable<object[]> GetDiceRollsSum()
    {
        yield return new object[] { new[] { 1, 1, 2, 4, 4 }, 4, 8 };
        yield return new object[] { new[] { 2, 3, 2, 5, 1 }, 2, 4 };
        yield return new object[] { new[] { 3, 3, 3, 4, 5 }, 1, 0 };
    }

    [Theory]
    [MemberData(nameof(GetDiceRollsSum))]
    public void GivenPlayerDie_WhenThePlayerSelectsSum_ThenReturnSumScore(int[] inputDiceValues, int numberPlacedOn, int expected)
    {
        var output = _scoreCalculator.GetScoreForSum(inputDiceValues, numberPlacedOn);
        Assert.Equal(expected, output);
    }

    
    public static IEnumerable<object[]> GetDiceRollsPair()
    {
        yield return new object[] { new[] { 3, 3, 3, 4, 4 }, 8 };
        yield return new object[] { new[] { 1, 1, 6, 2, 6 }, 12 };
        yield return new object[] { new[] { 3, 3, 3, 4, 1 }, 6 };
        yield return new object[] { new[] { 3, 3, 3, 3, 1 }, 6 };
    }
    
    [Theory]
    [MemberData(nameof(GetDiceRollsPair))]
    public void GivenPlayerDie_WhenThePlayerSelectsPair_ThenReturnPairScore(int[] inputDiceValues, int expected)
    {
        var output = _scoreCalculator.GetScoreForSinglePair(inputDiceValues);
        Assert.Equal(expected, output);
    }
    
    public static IEnumerable<object[]> GetDiceRollsTwoPair()
    {
        yield return new object[] { new[] { 1, 1, 2, 3, 3 }, 8 };
        yield return new object[] { new[] { 1, 1, 2, 3, 4 }, 0 };
        yield return new object[] { new[] { 1, 1, 2, 2, 2 }, 6 };
    }

    [Theory]
    [MemberData(nameof(GetDiceRollsTwoPair))]
    public void GivenPlayerDie_WhenThePlayerSelectsTwoPair_ThenReturnTwoPairScore(int[] inputDiceValues, int expected)
    {
        var output = _scoreCalculator.GetScoreForTwoPairs(inputDiceValues);
        Assert.Equal(expected, output);
    }
    
    public static IEnumerable<object[]> GetDiceRollsForHowManyOfAKind()
    {
        yield return new object[] { new[] { 3, 3, 3, 4, 5 }, 3, 9 };
        yield return new object[] { new[] { 3, 3, 4, 5, 6 }, 3, 0 };
        yield return new object[] { new[] { 3, 3, 3, 3, 1 }, 3, 9 };
        yield return new object[] { new[] { 2, 2, 2, 2, 5 }, 4, 8 };
        yield return new object[] { new[] { 2, 2, 2, 5, 5 }, 4, 0 };
        yield return new object[] { new[] { 2, 2, 2, 2, 2 }, 4, 8 };
    }
    
    [Theory]
    [MemberData(nameof(GetDiceRollsForHowManyOfAKind))]
    public void GivenPlayerDie_WhenThePlayerSelectsHowManyOfAKind_ThenReturnHowManyOfAKindScore(int[] inputDiceValues, int howMany, int expected)
    {
        var output = _scoreCalculator.GetScoreForHowManyOfAKind(inputDiceValues, howMany);
        Assert.Equal(expected, output);
    }
    
    public static IEnumerable<object[]> GetDiceRollsForSmallStraight()
    {
        yield return new object[] { new[] { 1, 2, 3, 4, 4 }, 30 };
        yield return new object[] { new[] { 2, 2, 3, 4, 5 }, 30 };
        yield return new object[] { new[] { 3, 4, 5, 6, 6 }, 30 };
        yield return new object[] { new[] { 3, 4, 5, 5, 6 }, 0 };
        yield return new object[] { new[] { 4, 5, 6, 3, 1 }, 0 };
        yield return new object[] { new[] { 2, 3, 4, 6, 5 }, 0 };
    }

    [Theory]
    [MemberData(nameof(GetDiceRollsForSmallStraight))]
    public void GivenPlayerDie_WhenThePlayerSelectsSmallStraight_ThenReturnSmallStraightScore(int[] inputDiceValues, int expected)
    {
        var output = _scoreCalculator.GetScoreForSmallStraight(inputDiceValues);
        Assert.Equal(expected, output);
    }
    
    public static IEnumerable<object[]> GetDiceRollsForLargeStraight()
    {
        yield return new object[] { new[] { 1, 2, 3, 4, 5 }, 40 };
        yield return new object[] { new[] { 2, 3, 4, 5, 6 }, 40 };
        yield return new object[] { new[] { 2, 2, 3, 4, 5 }, 0 };
        yield return new object[] { new[] { 1, 4, 5, 5, 6 }, 0 };
        yield return new object[] { new[] { 4, 5, 6, 3, 1 }, 0 };
        yield return new object[] { new[] { 2, 3, 4, 6, 5 }, 0 };
    }
    
    [Theory]
    [MemberData(nameof(GetDiceRollsForLargeStraight))]
    public void GivenPlayerDie_WhenThePlayerSelectsLargeStraight_ThenReturnLargeStraightScore(int[] inputDiceValues, int expected)
    {
        var output = _scoreCalculator.GetScoreForLargeStraight(inputDiceValues);
        Assert.Equal(expected, output);
    }
    
    public static IEnumerable<object[]> GetDiceRollsForFullHouse()
    {
        yield return new object[] { new[] { 1, 1, 2, 2, 2 }, 8 };
        yield return new object[] { new[] { 2, 2, 4, 4, 5 }, 0 };
        yield return new object[] { new[] { 4, 4, 4, 4, 4 }, 0 };
        yield return new object[] { new[] { 3, 5, 5, 3, 3 }, 19 };
        yield return new object[] { new[] { 4, 5, 4, 5, 4 }, 22 };
        yield return new object[] { new[] { 2, 3, 4, 4, 4 }, 0 };
    }
    
    [Theory]
    [MemberData(nameof(GetDiceRollsForFullHouse))]
    public void GivenPlayerDie_WhenThePlayerSelectsFullHouse_ThenReturnFullHouseScore(int[] inputDiceValues, int expected)
    {
        var output = _scoreCalculator.GetScoreForFullHouse(inputDiceValues);
        Assert.Equal(expected, output);
    }
}