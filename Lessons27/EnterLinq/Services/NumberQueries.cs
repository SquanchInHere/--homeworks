namespace EnterLinq.Services;

public class NumberQueries
{
    public IEnumerable<int> SortByDigitSumAscending(int[] numbers)
    {
        var query =
            from number in numbers
            orderby GetDigitSum(number), number
            select number;

        return query;
    }

    public IEnumerable<int> SortByDigitSumDescending(int[] numbers)
    {
        var query =
            from number in numbers
            orderby GetDigitSum(number) descending, number descending
            select number;

        return query;
    }

    private int GetDigitSum(int number)
    {
        int value = Math.Abs(number);
        int sum = 0;

        while (value > 0)
        {
            sum += value % 10;
            value /= 10;
        }

        return sum;
    }
}
