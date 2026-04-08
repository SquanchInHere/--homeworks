namespace BaseLinq.Services;

public class CityQueryService
{
    public event Action<string>? OperationPerformed;

    public IEnumerable<string> GetAllCities(IEnumerable<string> cities)
    {
        IEnumerable<string> result = cities;
        OnOperationPerformed("Returned all cities");
        return result;
    }

    public IEnumerable<string> GetCitiesByLength(IEnumerable<string> cities, int length)
    {
        IEnumerable<string> result = cities.Where(city => city.Length == length);
        OnOperationPerformed("Returned cities with the specified name length");
        return result;
    }

    public IEnumerable<string> GetCitiesStartingWithA(IEnumerable<string> cities)
    {
        IEnumerable<string> result = cities.Where(city =>
            city.StartsWith("A", StringComparison.OrdinalIgnoreCase));

        OnOperationPerformed("Returned cities starting with A");
        return result;
    }

    public IEnumerable<string> GetCitiesEndingWithM(IEnumerable<string> cities)
    {
        IEnumerable<string> result = cities.Where(city =>
            city.EndsWith("M", StringComparison.OrdinalIgnoreCase));

        OnOperationPerformed("Returned cities ending with M");
        return result;
    }

    public IEnumerable<string> GetCitiesStartingWithNAndEndingWithK(IEnumerable<string> cities)
    {
        IEnumerable<string> result = cities.Where(city =>
            city.StartsWith("N", StringComparison.OrdinalIgnoreCase) &&
            city.EndsWith("K", StringComparison.OrdinalIgnoreCase));

        OnOperationPerformed("Returned cities starting with N and ending with K");
        return result;
    }

    private void OnOperationPerformed(string message)
    {
        OperationPerformed?.Invoke(message);
    }
}