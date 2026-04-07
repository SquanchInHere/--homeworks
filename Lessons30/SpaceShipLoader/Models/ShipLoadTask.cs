namespace SpaceShipLoader.Models;

public class ShipLoadTask
{
    public string Name { get; set; }
    public double MaxValue { get; set; }
    public double Step { get; set; }

    public ShipLoadTask(string name, double maxValue, double step)
    {
        Name = name;
        MaxValue = maxValue;
        Step = step;
    }
}
