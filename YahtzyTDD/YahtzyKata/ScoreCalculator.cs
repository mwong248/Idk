namespace YahtzyKata;

public class ScoreCalculator
{
    public int GetScoreForChance(int[] playerDie)
    {
        return playerDie.Sum(diceValue => diceValue);
    }

    public int GetScoreForYatzy(int[] playerDie)
    {
        var identicalDiceValue = playerDie[0];
        return playerDie.Any(diceValue => diceValue != identicalDiceValue) ? 0 : 50;
    }

    public int GetScoreForSum(int[] playerDie, int numberPlacedOn)
    {
        return playerDie.Sum(diceValue => diceValue == numberPlacedOn ? diceValue : 0);
    }

    public int GetScoreForSinglePair(int[] playerDie)
    {
        var duplicates = FindDuplicates(playerDie);
        return duplicates.Count > 0 ? duplicates.Max() * 2 : 0;
    }

    public int GetScoreForTwoPairs(int[] playerDie)
    {
        var duplicates = FindDuplicates(playerDie);
        return duplicates.Count == 2 ? duplicates.Sum(duplicate => duplicate * 2) : 0;
    }

    private HashSet<int> FindDuplicates(int[] playerDie)
    {
        var seenBefore = new HashSet<int>();
        var duplicates = new HashSet<int>();
        
        foreach (var diceValue in playerDie)
        {
            if (!seenBefore.Contains(diceValue))
            {
                seenBefore.Add(diceValue);
            }
            else
            {
                duplicates.Add(diceValue);
            }
        }

        return duplicates;
    }

    public int GetScoreForHowManyOfAKind(int[] playerDie, int howMany)
    {
        var numRepeatedXTimes = playerDie.GroupBy(num => num).Where(num => num.Count() >= howMany)
            .Select(num => num.Key).ToList();
        
        return numRepeatedXTimes.Count > 0 ? numRepeatedXTimes[0] * howMany : 0;
    }

    public int GetScoreForSmallStraight(int[] playerDie)
    {
        var smallStraightFromIndexZero = playerDie[..4];
        var smallStraightFromIndexOne = playerDie[1..];

        if (smallStraightFromIndexOne.Zip(smallStraightFromIndexOne.Skip(1), (a, b) => (a + 1) == b).All(x => x))
        {
            return 30;
        }

        if (smallStraightFromIndexZero.Zip(smallStraightFromIndexZero.Skip(1), (a, b) => (a + 1) == b).All(x => x))
        {
            return 30;
        }

        return 0;
    }

    public int GetScoreForLargeStraight(int[] playerDie)
    {
        return playerDie.Zip(playerDie.Skip(1), (a, b) => (a + 1) == b).All(x => x) ? 40 : 0;
    }

    public int GetScoreForFullHouse(int[] inputDiceValues)
    {
        Array.Sort(inputDiceValues);

        if (inputDiceValues.All(x => x == inputDiceValues[0]))
        {
            return 0;
        }

        if (inputDiceValues[0] == inputDiceValues[1] && inputDiceValues[2] == inputDiceValues[3] &&
            inputDiceValues[3] == inputDiceValues[4])
        {
            return GetScoreForChance(inputDiceValues);
        } 
        
        if (inputDiceValues[0] == inputDiceValues[1] && inputDiceValues[1] == inputDiceValues[2] &&
            inputDiceValues[3] == inputDiceValues[4])
        {
            return GetScoreForChance(inputDiceValues);
        }

        return 0;
    }
}