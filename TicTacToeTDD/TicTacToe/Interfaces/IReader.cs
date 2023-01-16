namespace TicTacToe.Interfaces;

public interface IReader
{
    string GetUserInput();
    bool IsValidInput(string userInput);
}