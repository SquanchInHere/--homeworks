using EnterLinq.Models;

namespace EnterLinq.Services;

public class LaptopQueries
{
    public int GetLaptopCount(List<Laptop> laptops)
    {
        var query =
            from laptop in laptops
            select laptop;

        return query.Count();
    }

    public int GetLaptopCountWithPriceMoreThan(List<Laptop> laptops, decimal price)
    {
        var query =
            from laptop in laptops
            where laptop.Price > price
            select laptop;

        return query.Count();
    }

    public int GetLaptopCountWithPriceInRange(List<Laptop> laptops, decimal minPrice, decimal maxPrice)
    {
        var query =
            from laptop in laptops
            where laptop.Price >= minPrice && laptop.Price <= maxPrice
            select laptop;

        return query.Count();
    }

    public int GetLaptopCountByManufacturer(List<Laptop> laptops, string manufacturer)
    {
        var query =
            from laptop in laptops
            where laptop.Manufacturer.Equals(manufacturer, StringComparison.OrdinalIgnoreCase)
            select laptop;

        return query.Count();
    }

    public Laptop? GetLaptopWithMinPrice(List<Laptop> laptops)
    {
        var query =
            from laptop in laptops
            orderby laptop.Price
            select laptop;

        return query.FirstOrDefault();
    }

    public Laptop? GetLaptopWithMaxPrice(List<Laptop> laptops)
    {
        var query =
            from laptop in laptops
            orderby laptop.Price descending
            select laptop;

        return query.FirstOrDefault();
    }

    public Laptop? GetLaptopWithMinCpuFrequency(List<Laptop> laptops)
    {
        var query =
            from laptop in laptops
            orderby laptop.CpuFrequencyGHz
            select laptop;

        return query.FirstOrDefault();
    }

    public Laptop? GetNewestLaptopModel(List<Laptop> laptops)
    {
        var query =
            from laptop in laptops
            orderby laptop.ReleaseYear descending
            select laptop;

        return query.FirstOrDefault();
    }

    public decimal GetAveragePrice(List<Laptop> laptops)
    {
        var query =
            from laptop in laptops
            select laptop.Price;

        return query.Average();
    }

    public IEnumerable<string> GetManufacturerStatistics(List<Laptop> laptops)
    {
        var query =
            from laptop in laptops
            group laptop by laptop.Manufacturer into manufacturerGroup
            orderby manufacturerGroup.Key
            select $"{manufacturerGroup.Key}: {manufacturerGroup.Count()}";

        return query;
    }

    public IEnumerable<string> GetModelStatistics(List<Laptop> laptops)
    {
        var query =
            from laptop in laptops
            group laptop by laptop.Model into modelGroup
            orderby modelGroup.Key
            select $"{modelGroup.Key}: {modelGroup.Count()}";

        return query;
    }

    public IEnumerable<string> GetYearStatistics(List<Laptop> laptops)
    {
        var query =
            from laptop in laptops
            group laptop by laptop.ReleaseYear into yearGroup
            orderby yearGroup.Key
            select $"{yearGroup.Key}: {yearGroup.Count()}";

        return query;
    }
}