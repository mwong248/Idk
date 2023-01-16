using static YahtzyKata.YatzyCategories;

namespace YahtzyKata;

public class InputOutput
{
    private TextReader _consoleInput;
    private TextWriter _consoleOutput;
    
    public InputOutput(TextReader consoleInput, TextWriter consoleOutput)
    {
        _consoleInput = consoleInput;
        _consoleOutput = consoleOutput;
    }
    
    public string[] GetInputDiceIds()
    {
        _consoleOutput.Write("Which dice do you want to HOLD? Enter the number(s) of the die separated by commas or just press Enter to hold none: ");
        var userInput = _consoleInput.ReadLine().Trim();
        return userInput == "" ? new[] { "ALL" } : userInput.Split(",");
    }

    public int GetInputCategory(List<Categories> categoriesToPlay)
    {
        for (var i = 0; i < categoriesToPlay.Count ; i++)
        {
            if (categoriesToPlay[i] != Categories.Played)
            {
                _consoleOutput.WriteLine($"{i + 1}. {categoriesToPlay[i]}");
            }
        }
        _consoleOutput.Write("\nWhich category do you want to play? Enter the number of the category: ");
        return int.Parse(_consoleInput.ReadLine());
    }
    
    public int ValidateCategory(int categoryNumber, List<Categories> categoriesToPlay)
    {
        while (true)
        {
            if (categoryNumber is < 1 or > 15)
            {
                Write("Invalid number. Please enter a number between 1 to 15 and try again: ");
                categoryNumber = int.Parse(ReadLine().Trim());
            }
            else if (categoriesToPlay[categoryNumber - 1] == Categories.Played)
            {
                Write("Category has already been played! Please enter a different category number: ");
                categoryNumber = int.Parse(ReadLine().Trim());
            }
            else
            {
                return categoryNumber;   
            }
        }
    }
    
    public void PrintHeldDie(string[] diceIdStrings)
    {
        WriteLine("");
        
        foreach (var diceId in diceIdStrings)
        {
            WriteLine($"HOLDING dice {diceId}");
        }
    }

    public void PrintRolledDie(Dice[] playerDie)
    {
        WriteLine("");
        
        foreach (var dice in playerDie)
        {
            if (dice.IsHeld) continue;
            WriteLine($"ROLLING dice {dice.IdNumber}");
        }
    }
    
    public void PrintPlayerStats(int? pointsToAdd, Player player)
    {
        WriteLine($"\nAdding {pointsToAdd} points...");
        WriteLine($"You now have: {player.Points} points!");
    }

    public void WriteLine(string messageToPrint)
    {
        _consoleOutput.WriteLine(messageToPrint);
    }

    public void Write(string messageToPrint)
    {
        _consoleOutput.Write(messageToPrint);
    }

    public string ReadLine()
    {
        return _consoleInput.ReadLine();
    }
}