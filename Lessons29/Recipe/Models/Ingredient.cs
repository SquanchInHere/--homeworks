namespace Recipe.Models;

public class Ingredient
{
    public string Name { get; set; } = string.Empty;
    public int Calories { get; set; }

    public override string ToString()
    {
        return $"{Name} ({Calories} cal)";
    }
}
