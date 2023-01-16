using TicTacToe.Interfaces;

namespace TicTacToe;

public class Reader : IReader
{
    private readonly TextReader _textReader;
    
    public Reader(TextReader textReader)
    {
        _textReader = textReader;
    }
    
    public string GetUserInput()
    {
        return _textReader.ReadLine();
    }

    public bool IsValidInput(string userInput)
    {
        if (userInput.Split(",").Length != 2)
        {
            return false;
        }

        if (!userInput.Split(",").All(s => s.All(char.IsDigit)))
        {
            return false;
        }

        if (int.Parse(userInput.Split(",")[0]) < 1 || int.Parse(userInput.Split(",")[0]) > 3)
        {
            return false;
        }
        
        if (int.Parse(userInput.Split(",")[1]) < 1 || int.Parse(userInput.Split(",")[1]) > 3)
        {
            return false;
        }

        return true;
    }
}