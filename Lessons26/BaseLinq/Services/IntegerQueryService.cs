namespace BaseLinq.Services;

public class IntegerQueryService
{
    public event Action<string>? OperationPerformed;

    public List<int> GenerateNumbers(int start, int end)
    {
        List<int> numbers = Enumerable.Range(start, end - start + 1).ToList();
        OnOperationPerformed("Generated collection of integers");
        return numbers;
    }

    public IEnumerable<int> GetAll(IEnumerable<int> numbers)
    {
        IEnumerable<int> result = numbers;
        OnOperationPerformed("Returned all integers");
        return result;
    }

    public IEnumerable<int> GetEven(IEnumerable<int> numbers)
    {
        IEnumerable<int> result = numbers.Where(number => number % 2 == 0);
        OnOperationPerformed("Returned even integers");
        return result;
    }

    public IEnumerable<int> GetOdd(IEnumerable<int> numbers)
    {
        IEnumerable<int> result = numbers.Where(number => number % 2 != 0);
        OnOperationPerformed("Returned odd integers");
        return result;
    }

    public IEnumerable<int> GetInRange(IEnumerable<int> numbers, int min, int max)
    {
        IEnumerable<int> result = numbers.Where(number => number >= min && number <= max);
        OnOperationPerformed("Returned numbers in a given range");
        return result;
    }

    public IEnumerable<int> GetDivisibleBySevenSorted(IEnumerable<int> numbers)
    {
        IEnumerable<int> result = numbers
            .Where(number => number % 7 == 0)
            .OrderBy(number => number);

        OnOperationPerformed("Returned numbers divisible by seven and sorted them");
        return result;
    }

    public IEnumerable<int> GetDivisibleByEight(IEnumerable<int> numbers)
    {
        IEnumerable<int> result = numbers.Where(number => number % 8 == 0);
        OnOperationPerformed("Returned numbers divisible by eight");
        return result;
    }

    public IEnumerable<int> GetDescending(IEnumerable<int> numbers)
    {
        IEnumerable<int> result = numbers.OrderByDescending(number => number);
        OnOperationPerformed("Sorted integers in descending order");
        return result;
    }

    private void OnOperationPerformed(string message)
    {
        OperationPerformed?.Invoke(message);
    }
}