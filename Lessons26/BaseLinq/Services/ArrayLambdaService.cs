using System.Text.RegularExpressions;

namespace BaseLinq.Services;

public class ArrayLambdaService
{
    public event Action<string>? OperationPerformed;

    public int CountDivisibleBySeven(int[] numbers)
    {
        Func<int[], int> countFunc = array => array.Count(number => number % 7 == 0);

        int result = countFunc(numbers);
        OnOperationPerformed("Counted numbers divisible by seven");
        return result;
    }

    public int CountPositiveNumbers(int[] numbers)
    {
        Func<int[], int> countFunc = array => array.Count(number => number > 0);

        int result = countFunc(numbers);
        OnOperationPerformed("Counted positive numbers");
        return result;
    }

    public IEnumerable<int> GetUniqueNegativeNumbers(int[] numbers)
    {
        Func<int[], IEnumerable<int>> getFunc = array =>
            array.Where(number => number < 0).Distinct();

        IEnumerable<int> result = getFunc(numbers);
        OnOperationPerformed("Displayed unique negative numbers");
        return result;
    }

    public bool ContainsWord(string text, string word)
    {
        Func<string, string, bool> containsFunc = (sourceText, searchWord) =>
            Regex.IsMatch(sourceText, $@"\b{Regex.Escape(searchWord)}\b", RegexOptions.IgnoreCase);

        bool result = containsFunc(text, word);
        OnOperationPerformed("Checked text for a given word");
        return result;
    }

    private void OnOperationPerformed(string message)
    {
        OperationPerformed?.Invoke(message);
    }
}