using TicTacToe;

namespace TicTacToeTests;

public class GameServiceTests
{
    private Board _board;
    private Player _player1;
    private Player _player2;
    private Game _game;
    private StringReader _input;
    private StringWriter _output;
    private Reader _reader;
    private Writer _writer;
    private BoardChecker _boardChecker;
    private GameService _gameService;
    
    public GameServiceTests()
    {
        _board = new Board();
        _player1 = new Player(SymbolEnum.X.GetSymbolString());
        _player2 = new Player(SymbolEnum.O.GetSymbolString());
        _game = new Game(_board, _player1, _player2);
        _input = new StringReader("");
        _output = new StringWriter();
        _reader = new Reader(_input);
        _writer = new Writer(_output);
        _boardChecker = new BoardChecker();
        _gameService = new GameService(_game, _reader, _writer, _boardChecker);
    }
    
    [Fact]
    public void GivenAValidUserInput_WhenFillSquareIsCalled_ThenGridSquareShouldBeFilled()
    {
        var symbol = SymbolEnum.X.GetSymbolString();
        var playerMove = new Move(1, 3);

        _gameService.FillSquare(playerMove, symbol);
        
        Assert.Equal(symbol, _board.Grid[0, 2].GetSymbolString());
    }

    [Theory]
    [InlineData("2,1", 2, 1)]
    [InlineData("1,2", 1, 2)]
    [InlineData("2,3", 2, 3)]
    [InlineData("3,3", 3, 3)]
    [InlineData("1,1", 1, 1)]
    public void GivenAValidUserInput_WhenPlayUserTurnCalled_ThenAMoveStructureShouldBeReturnedWithCorrectXYValues(string userInput, int userInputX, int userInputY)
    {
        var input = new StringReader(userInput);
        var reader = new Reader(input);
        _gameService = new GameService(_game, reader, _writer, _boardChecker);

        var moveReturned = _gameService.PlayUserTurn(SymbolEnum.X.GetSymbolString());
        
        Assert.Equal(new Move(userInputX, userInputY), moveReturned);
    }
    
    [Theory]
    [InlineData("1,,1\r\n2,1")]
    [InlineData("0,a\r\n1,2")]
    [InlineData("b,1\r\n2,3")]
    [InlineData("4,3\r\n3,3")]
    [InlineData(";2,2das\r\n1,1")]
    public void GivenAnInvalidUserInput_WhenPlayUserTurnCalled_ThenInvalidInputErrorMessageShouldPrint(string userInput)
    { ;
        var input = new StringReader(userInput);
        var reader = new Reader(input);
        _gameService = new GameService(_game, reader, _writer, _boardChecker);

        _gameService.PlayUserTurn(SymbolEnum.X.GetSymbolString());

        Assert.Contains("Invalid input, please try again: ", _output.ToString());
    }
    
    [Fact]
    public void GivenValidUserInputOnFilledSquare_WhenPlayUserTurnCalled_ThenInvalidMoveMessageShouldPrint()
    {
        var move = new Move(3, 3);
        var input = new StringReader("3,3\r\n1,2\r\n");
        var reader = new Reader(input);
        _gameService = new GameService(_game, reader, _writer, _boardChecker);
        
        _gameService.FillSquare(move, SymbolEnum.X.GetSymbolString());
        _gameService.PlayUserTurn(SymbolEnum.O.GetSymbolString());
        
        
        Assert.Contains("Invalid move - square is already full, please try again: ", _output.ToString());
    }
    
    [Fact]
    public void GivenInvalidUserInputFollowingValidUserInputOnFilledSquare_WhenPlayUserTurnCalled_ThenBothInvalidInputErrorAndInvalidMoveMessagesShouldPrint()
    {
        var move = new Move(3, 3);
        var input = new StringReader("3,3\r\n1,,2\r\n1,2\r\n");
        var reader = new Reader(input);
        _gameService = new GameService(_game, reader, _writer, _boardChecker);
        
        _gameService.FillSquare(move, SymbolEnum.X.GetSymbolString());
        _gameService.PlayUserTurn(SymbolEnum.O.GetSymbolString());

        Assert.Contains("Invalid move - square is already full, please try again: ", _output.ToString());
        Assert.Contains("Invalid input, please try again: ", _output.ToString());
    }
    
    [Theory]
    [InlineData("2,1")]
    [InlineData("1,2")]
    [InlineData("2,3")]
    [InlineData("3,3")]
    [InlineData("1,1")]
    public void GivenAValidInput_WhenPlayUserTurnCalled_ThenXTurnBooleanShouldFlip(string userInput)
    {
        var input = new StringReader(userInput);
        var reader = new Reader(input);
        var xTurnBefore = _game.Xturn;
        _gameService = new GameService(_game, reader, _writer, _boardChecker);

        _gameService.PlayUserTurn(SymbolEnum.X.GetSymbolString());
        var xTurnAfter = _game.Xturn;
        
        Assert.NotEqual(xTurnBefore, xTurnAfter);
    }

    [Fact]
    public void GivenADrawnGame_WhenCheckBoardForEndStateCalled_ThenReturnTrueAndPrintDrawMessage()
    {
        _gameService = new GameService(_game, _reader, _writer, _boardChecker);
        _board.Grid = new[,] { { SymbolEnum.O, SymbolEnum.O, SymbolEnum.X }, { SymbolEnum.X, SymbolEnum.X, SymbolEnum.O }, { SymbolEnum.O, SymbolEnum.X, SymbolEnum.X } };
        
        var isEnd = _gameService.CheckBoardForEndState();

        Assert.Contains("It's a draw!\n", _output.ToString());
        Assert.True(isEnd);
    }
    
    [Fact]
    public void GivenAWonGame_WhenXWinsAndCheckBoardForEndStateCalled_ThenReturnTrueAndPrintXWon()
    {
        _gameService = new GameService(_game, _reader, _writer, _boardChecker);
        _board.Grid =  new[,] { { SymbolEnum.X, SymbolEnum.O, SymbolEnum.O }, { SymbolEnum.Empty, SymbolEnum.X, SymbolEnum.Empty }, { SymbolEnum.Empty, SymbolEnum.O, SymbolEnum.X } };
        
        var isEnd = _gameService.CheckBoardForEndState();

        Assert.Contains("X won!\n", _output.ToString());
        Assert.True(isEnd);
    }
    
    [Fact]
    public void GivenAWonGame_WhenOWinsAndCheckBoardForEndStateCalled_ThenReturnTrueAndPrintOWon()
    {
        _gameService = new GameService(_game, _reader, _writer, _boardChecker);
        _board.Grid =  new[,] { { SymbolEnum.Empty, SymbolEnum.X, SymbolEnum.O }, { SymbolEnum.Empty, SymbolEnum.O, SymbolEnum.Empty }, { SymbolEnum.O, SymbolEnum.X, SymbolEnum.X } };
        
        var isEnd = _gameService.CheckBoardForEndState();

        Assert.Contains("O won!\n", _output.ToString());
        Assert.True(isEnd);
    }
    
    [Theory]
    [InlineData("1,2\r\n1,1\r\n2,1\r\n1,3\r\n2,3\r\n2,2\r\n3,1\r\n3,2\r\n3,3\r\n", " O | X | O \n" 
                                                                                    + "-----------\n" 
                                                                                    + " X | O | X \n"
                                                                                    + "-----------\n"
                                                                                    + " X | O | X \n")]
    public void GivenValidInputsForDrawnGame_WhenPlayCalled_ThenPrintDrawnBoardAndDrawnMessageAndExitMessage(string userInput, string expectedBoardPrint)
    {
        var input = new StringReader(userInput);
        var reader = new Reader(input);

        _gameService = new GameService(_game, reader, _writer, _boardChecker);

        _gameService.Play();

        Assert.Contains(expectedBoardPrint, _output.ToString());
        Assert.Contains("It's a draw!\n", _output.ToString());
        Assert.Contains("Thanks for playing!\n", _output.ToString());
    }
    
    [Theory]
    [InlineData("1,3\r\n1,1\r\n2,2\r\n1,2\r\n2,3\r\n2,1\r\n3,1\r\n",    " O | O | X \n" 
                                                                      + "-----------\n" 
                                                                      + " O | X | X \n"
                                                                      + "-----------\n"
                                                                      + " X |   |   \n")]
    public void GivenValidInputsForWonGame_WhenPlayCalledAndXWins_ThenPrintWonBoardAndXWonMessageAndExitMessage(string userInput, string expectedBoardPrint)
    {
        var input = new StringReader(userInput);
        var reader = new Reader(input);

        _gameService = new GameService(_game, reader, _writer, _boardChecker);

        _gameService.Play();

        Assert.Contains(expectedBoardPrint, _output.ToString());
        Assert.Contains("X won!\n", _output.ToString());
        Assert.Contains("Thanks for playing!\n", _output.ToString());
    }
    
    [Theory]
    [InlineData("1,2\r\n1,1\r\n3,1\r\n2,2\r\n2,3\r\n3,3\r\n", " O | X |   \n" 
                                                            + "-----------\n" 
                                                            + "   | O | X \n"
                                                            + "-----------\n"
                                                            + " X |   | O \n")]
    public void GivenValidInputsForWonGame_WhenPlayCalledAndOWins_ThenPrintWonBoardAndOWonMessageAndExitMessage(string userInput, string expectedBoardPrint)
    {
        var input = new StringReader(userInput);
        var reader = new Reader(input);

        _gameService = new GameService(_game, reader, _writer, _boardChecker);

        _gameService.Play();

        Assert.Contains(expectedBoardPrint, _output.ToString());
        Assert.Contains("O won!\n", _output.ToString());
        Assert.Contains("Thanks for playing!\n", _output.ToString());
    }
    
    [Theory]
    [InlineData("1,3\r\n1,1\r\n2,2\r\n2,2\r\n1,,2\r\n1,2\r\n2,3\r\n2,1\r\n3,1\r\n",    " O | O | X \n" 
                                                                                     + "-----------\n" 
                                                                                     + " O | X | X \n"
                                                                                     + "-----------\n"
                                                                                     + " X |   |   \n")]
    public void GivenWonGameWithSomeInvalidInputs_WhenPlayCalledAndXWins_ThenPrintErrorMessagesAndWonBoardAndXWonMessageAndExitMessage(string userInput, string expectedBoardPrint)
    {
        var input = new StringReader(userInput);
        var reader = new Reader(input);

        _gameService = new GameService(_game, reader, _writer, _boardChecker);

        _gameService.Play();

        Assert.Contains(expectedBoardPrint, _output.ToString());
        Assert.Contains("Invalid move - square is already full, please try again: ", _output.ToString());
        Assert.Contains("Invalid input, please try again: ", _output.ToString());
        Assert.Contains("X won!\n", _output.ToString());
        Assert.Contains("Thanks for playing!\n", _output.ToString());
    }
}