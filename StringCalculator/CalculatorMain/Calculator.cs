namespace CalculatorMain;

public class Calculator
{
    
    private readonly string _delimiterPrefix = "//[";
    private readonly string _delimiterSuffix = "]\n";
    private readonly List<string> _defaultDelimiters = new() {",", "\n"};
    
    static void Main(string[] args)
    {
        
    }
    
    public int Add(string input)
    {
        int sum = 0;
        string[]? numbersToAdd;

        if (string.IsNullOrEmpty(input))
        {
            return sum;
        }

        // sum string without custom delimiters
        if (char.IsDigit(input[0]) || input[0] == '-')
        {
            numbersToAdd = input.Split(_defaultDelimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);
            sum = Sum(numbersToAdd);
        }
        
        if (input.StartsWith(_delimiterPrefix))
        {
            var customDelimiters = GetDelimiters(input);
            customDelimiters.AddRange(_defaultDelimiters);
            
            var numSequenceStartIndex = input.IndexOf(_delimiterSuffix) + _delimiterSuffix.Length;
            var numberSequence = input.Substring(numSequenceStartIndex, input.Length - numSequenceStartIndex);
            
            numbersToAdd = numberSequence.Split(customDelimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);
            sum = Sum(numbersToAdd);
        }

        return sum;
    }

    private int Sum(string[] numbersToAdd)
    {
        var numbersToAddInts = Array.ConvertAll(numbersToAdd, int.Parse);

        if (numbersToAddInts.Min() < 0)
        {
            throw new ArgumentException("Input cannot contain negative numbers");
        }
        
        var sum = numbersToAddInts.Sum(number => number is > 0 and < 1000 ? number : 0);
        return sum;
    }

    private List<string> GetDelimiters(string input)
    {
        var startIndex = input.IndexOf("[");
        var endIndex = input.IndexOf("\n");

        var delimiterString = input.Substring(startIndex, endIndex - startIndex);
        var delimitersArray = delimiterString.Split("]");

        return (from delimiter in delimitersArray where !string.IsNullOrEmpty(delimiter) select delimiter.Substring(1)).ToList();
    }
}