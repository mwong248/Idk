using TicTacToe.Interfaces;

namespace TicTacToe;

public class Writer : IWriter
{
    private readonly TextWriter _textWriter;
    
    public Writer(TextWriter textWriter)
    {
        _textWriter = textWriter;
    }
    
    public void PrintBoard(SymbolEnum[,] boardGrid)
    {
        for (var row = 0; row < boardGrid.GetLength(0); row++)
        {
            for (var col = 0; col < boardGrid.GetLength(1); col++)
            {
                _textWriter.Write(col == 2 ? $" {boardGrid[row, col].GetSymbolString()} \n" : $" {boardGrid[row, col].GetSymbolString()} |");
            }
            if (row < 2)
            {
                _textWriter.Write("-----------\n");  
            }
        }
    }

    public void PrintMessage(string message)
    {
        _textWriter.Write(message);
    }
}