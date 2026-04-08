using EnterLinq.Models;

namespace EnterLinq.Data;

public class LaptopSeeder
{
    public List<Laptop> GetLaptops()
    {
        return new List<Laptop>
        {
            new Laptop("XPS 13", "Dell", 3.4, 8, 1500, 2023),
            new Laptop("Inspiron 15", "Dell", 2.8, 6, 900, 2022),
            new Laptop("MacBook Air M3", "Apple", 4.0, 8, 1700, 2024),
            new Laptop("MacBook Pro M4", "Apple", 4.3, 12, 2600, 2025),
            new Laptop("Pavilion 14", "HP", 2.6, 4, 700, 2021),
            new Laptop("Omen 16", "HP", 3.8, 8, 1800, 2024),
            new Laptop("ThinkPad X1", "Lenovo", 3.2, 8, 1400, 2023),
            new Laptop("Legion 5", "Lenovo", 3.9, 8, 1600, 2024)
        };
    }
}