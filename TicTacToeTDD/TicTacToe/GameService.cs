using TicTacToe.Interfaces;

namespace TicTacToe;

public class GameService : IService
{
    private Game _game;
    private readonly Board _board;
    private Player _player1;
    private Player _player2;
    private Reader _reader;
    private readonly Writer _writer;
    private BoardChecker _boardChecker;

    public GameService(Game game, Reader reader, Writer writer, BoardChecker boardChecker)
    {
        _game = game;
        _board = game.Board;
        _player1 = game.Player1;
        _player2 = game.Player2;
        _reader = reader;
        _writer = writer;
        _boardChecker = boardChecker;
    }

    public void Initialize()
    {
        _writer.PrintMessage("Welcome to Tic-Tac-Toe!\n");
        Play();
    }

    public void Play()
    {
        _writer.PrintBoard(_board.Grid);
        
        while (true)
        {
            if (_game.Xturn)
            {
                var xInput = PlayUserTurn(SymbolEnum.X.GetSymbolString());
                FillSquare(xInput, SymbolEnum.X.GetSymbolString());
            }
            else
            {
                var oInput = PlayUserTurn(SymbolEnum.O.GetSymbolString());
                FillSquare(oInput, SymbolEnum.O.GetSymbolString());
            }
            _writer.PrintMessage("\nCurrent board: \n");
            _writer.PrintBoard(_board.Grid);
            _writer.PrintMessage("\n");

            if (CheckBoardForEndState())
            {
                break;
            }   
        }
        
        _writer.PrintMessage("Thanks for playing!\n");
    }

    public Move PlayUserTurn(string playerSymbol)
    {
        _writer.PrintMessage($"It is player {playerSymbol}'s turn! Enter the coordinate you want to place a symbol on, (e.g, 2,1): ");
        var input = _reader.GetUserInput();

        while (true)
        {
            if (!_reader.IsValidInput(input))
            {
                _writer.PrintMessage("Invalid input, please try again: ");
                input = _reader.GetUserInput();
                continue;
            }
            
            var inputAsArray = input.Split(",");
            var move = new Move(int.Parse(inputAsArray[0]), int.Parse(inputAsArray[1]));

            if (_boardChecker.IsSquareOccupied(_board.Grid, move))
            {
                _writer.PrintMessage("Invalid move - square is already full, please try again: ");
                input = _reader.GetUserInput();
            }
            else
            {
                _game.Xturn = !_game.Xturn;
                return move;
            }
        }
    }

    public bool CheckBoardForEndState()
    {
        if (_boardChecker.IsGameWon(_board.Grid, _player1.PlayerSymbol))
        {
            _writer.PrintMessage($"{_player1.PlayerSymbol} won!\n");
            return true;
        }

        if (_boardChecker.IsGameWon(_board.Grid, _player2.PlayerSymbol))
        {
            _writer.PrintMessage($"{_player2.PlayerSymbol} won!\n");
            return true;
        }

        if (_boardChecker.IsGameDrawn(_board.Grid))
        {
            _writer.PrintMessage("It's a draw!\n");
            return true;
        }

        return false;
    }

    public void FillSquare(Move playerMove, string playerSymbol)
    {
        _board.Grid[playerMove.Row - 1, playerMove.Col - 1] = TileSymbols.GetSymbolEnum(playerSymbol);
    }
}