using CalculatorMain;

namespace CalculatorTests;

public class UnitTests
{

    private readonly Calculator _stringCalculator;
    
    public UnitTests()
    {
        _stringCalculator = new Calculator();
    }
    
    [Fact]
    public void GivenEmptyString_WhenAddMethodCalled_ThenReturnZero()
    {
        // Act
        var output = _stringCalculator.Add("");
        
        // Assert
        Assert.Equal(0, output);
    }

    [Theory]
    [InlineData("1", 1)]
    [InlineData("3", 3)]
    public void GivenNumberString_WhenAddMethodCalled_ThenReturnNumberStringAsInteger(string input, int expected)
    {
        // Act
        var output = _stringCalculator.Add(input);

        // Assert 
        Assert.Equal(expected, output);
    }

    [Theory]
    [InlineData("1,2", 3)]
    [InlineData("3,5", 8)]
    public void GivenTwoNumbersAsString_WhenAddMethodCalled_ThenReturnSumOfTwoNumbersAsInteger(string input, int expected)
    {
        // Act
        var output = _stringCalculator.Add(input);

        // Assert 
        Assert.Equal(expected, output);
    }
    
    [Theory]
    [InlineData("1,2,3", 6)]
    [InlineData("3,5,3,9", 20)]
    public void GivenAnyNumbersAsString_WhenAddMethodCalled_ThenReturnSumOfAllNumbersAsInteger(string input, int expected)
    {
        // Act
        var output = _stringCalculator.Add(input);

        // Assert 
        Assert.Equal(expected, output);
    }
    
    [Theory]
    [InlineData("1,2\n3", 6)]
    [InlineData("3\n5\n3,9", 20)]
    public void GivenAnyNumbersAsStringWithNewLines_WhenAddMethodCalled_ThenReturnSumOfAllNumbersAsInteger(string input, int expected)
    {
        // Act
        var output = _stringCalculator.Add(input);

        // Assert 
        Assert.Equal(expected, output);
    }
    
    [Fact]
    public void GivenAnyNumbersAsStringWithDelimiter_WhenAddMethodCalled_ThenReturnSumOfAllNumbersAsInteger()
    {
        // Act
        var output = _stringCalculator.Add("//[;]\n1;2");

        // Assert 
        Assert.Equal(3, output);
    }
    
    [Fact]
    public void GivenNegativeNumbersAsString_WhenAddMethodCalled_ThenThrowException()
    {
        // Act/Assert 
        Assert.Throws<ArgumentException>(() => _stringCalculator.Add("-1,2,-3"));
    }

    [Fact]
    public void GivenNumbersAsString_WhenAddMethodCalled_ThenIgnoreNumbersGreaterThanOrEqualTo1000()
    {
        // Act
        var output = _stringCalculator.Add("1000,1001,2");

        // Assert 
        Assert.Equal(2, output);
    }

    [Fact]
    public void GivenAnyNumbersAsStringWithDelimiterOfAnyLength_WhenAddMethodCalled_ThenReturnSumOfAllNumbersAsInteger()
    {
        // Act
        var output = _stringCalculator.Add("//[***]\n1***2***3");
        
        // Assert
        Assert.Equal(6, output);
    }

    [Fact]
    public void GivenNumbersWithMultipleDelimiters_WhenAddMethodCalled_ThenReturnSumOfAllNumbersAsInteger()
    {
        // Act
        var output = _stringCalculator.Add("//[*][%]\n1*2%3");
        
        // Assert
        Assert.Equal(6, output);
    }

    [Fact]
    public void GivenNumbersWithMultipleDelimitersOfAnyLength_WhenAddMethodCalled_ThenReturnSumOfAllNumbersAsInteger()
    {
        // Act
        var output = _stringCalculator.Add("//[***][#][%]\n1***2#3%4");
        
        // Assert
        Assert.Equal(10, output);
    }
    
    [Fact]
    public void GivenDelimitersWithNumbersInBetween_WhenAddMethodCalled_ThenReturnSumOfAllNumbersAsInteger()
    {
        // Act
        var output = _stringCalculator.Add("//[*1*][%]\n1*1*2%3");
        
        // Assert
        Assert.Equal(6, output);
    }
}