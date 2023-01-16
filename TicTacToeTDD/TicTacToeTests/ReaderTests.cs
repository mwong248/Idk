using TicTacToe;

namespace TicTacToeTests;

public class ReaderTests
{
    [Fact]
    public void GivenAUserInputString_WhenGetUserInputCalled_ThenReturnTheUserInputString()
    {
        const string expected = "Test";
        var input = new StringReader(expected);
        var reader = new Reader(input);
        
        var result = reader.GetUserInput();
        
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("1,3", true)]
    [InlineData("2,2", true)]
    [InlineData("2,1", true)]
    [InlineData("3,1", true)]
    public void GivenAValidUserInputString_WhenValidateUserInputCalled_ThenReturnTrue(string userInput, bool expected)
    {
        var input = new StringReader("");
        var reader = new Reader(input);
        
        var result = reader.IsValidInput(userInput);
        
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData("1,,3", false)]
    [InlineData("0,1", false)]
    [InlineData("2,4", false)]
    [InlineData("3,a", false)]
    [InlineData("b,2", false)]
    [InlineData("2.3", false)]
    [InlineData("31,ef.,';", false)]
    public void GivenAnInvalidUserInputString_WhenValidateUserInputCalled_ThenReturnFalse(string userInput, bool expected)
    {
        var input = new StringReader("");
        var reader = new Reader(input);
        
        var result = reader.IsValidInput(userInput);
        
        Assert.Equal(expected, result);
    }
}