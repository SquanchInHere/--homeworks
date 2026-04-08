namespace EnterLinq.Models;

public class Laptop
{
    public string Model { get; set; }
    public string Manufacturer { get; set; }
    public double CpuFrequencyGHz { get; set; }
    public int Cores { get; set; }
    public decimal Price { get; set; }
    public int ReleaseYear { get; set; }

    public Laptop(
        string model,
        string manufacturer,
        double cpuFrequencyGHz,
        int cores,
        decimal price,
        int releaseYear)
    {
        Model = model;
        Manufacturer = manufacturer;
        CpuFrequencyGHz = cpuFrequencyGHz;
        Cores = cores;
        Price = price;
        ReleaseYear = releaseYear;
    }

    public override string ToString()
    {
        return $"{Manufacturer} {Model} | CPU: {CpuFrequencyGHz} GHz | Cores: {Cores} | Price: {Price} | Year: {ReleaseYear}";
    }
}
