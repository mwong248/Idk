namespace YahtzyKata;

public class Player
{
    public Dice[] PlayerDie { get; } = new Dice[5];
    public int? Points { get; set; }

    public Player()
    {
        for (var i = 0; i < PlayerDie.Length; i++)
        {
            PlayerDie[i] = new Dice(new RandomGenerator(), i + 1);   
        }

        Points = 0;
    }

    public Dice? SelectDice(string diceId)
    {
        return PlayerDie.FirstOrDefault(dice => dice.IdNumber == int.Parse(diceId));
    }

    public void AddPoints(int? pointsToAdd)
    {
        Points += pointsToAdd;
    }

    public void HoldDice(string diceId)
    {
        var diceToHold = SelectDice(diceId);

        diceToHold!.IsHeld = true;
    }

    public int[] GetDiceValues()
    {
        var diceValueArr = new int[5];

        for (var i = 0; i < PlayerDie.Length; i++)
        {
            diceValueArr[i] = PlayerDie[i].Value;
        }

        return diceValueArr;
    }

    public string GetDiceValuesAsString()
    {
        var diceValues = GetDiceValues();
        
        var diceValueString = "";
        
        for (var i = 0; i < diceValues.Length; i++)
        {
            diceValueString += $"Dice {i + 1}: {diceValues[i]}\n";
        }
        return diceValueString;
    }
}