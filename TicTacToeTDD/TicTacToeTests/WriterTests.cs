using TicTacToe;

namespace TicTacToeTests;

public class WriterTests
{
    private readonly StringWriter _output;
    private readonly Writer _writer;

    public WriterTests()
    {
        _output = new StringWriter();
        _writer = new Writer(_output);
    }
    
    [Fact]
    public void GivenAMessage_WhenPrintMessageCalled_ThenConsoleLogTheMessage()
    {
        const string expected = "Test";
        _writer.PrintMessage(expected);
        
        Assert.Equal(expected, _output.ToString());
    }

    public static IEnumerable<object[]> ExampleInputBoards()
    {
        yield return new object[] { new[,] { { SymbolEnum.Empty, SymbolEnum.Empty, SymbolEnum.Empty }, { SymbolEnum.Empty, SymbolEnum.Empty, SymbolEnum.Empty }, { SymbolEnum.Empty, SymbolEnum.Empty, SymbolEnum.Empty } },
              "   |   |   \n"
            + "-----------\n"
            + "   |   |   \n"
            + "-----------\n"
            + "   |   |   \n"};
        
        yield return new object[] { new[,] { { SymbolEnum.Empty, SymbolEnum.X, SymbolEnum.Empty }, { SymbolEnum.O, SymbolEnum.Empty, SymbolEnum.Empty }, { SymbolEnum.Empty, SymbolEnum.O, SymbolEnum.X } },
              "   | X |   \n"
            + "-----------\n"
            + " O |   |   \n"
            + "-----------\n"
            + "   | O | X \n"};
    }
    
    [Theory]
    [MemberData(nameof(ExampleInputBoards))]
    public void GivenABoard_WhenPrintBoardCalled_ThenConsoleLogTheBoard(SymbolEnum[,] grid, string expected)
    {
        _writer.PrintBoard(grid);
        
        Assert.Equal(expected, _output.ToString());
    }
}