namespace Recipe.Interfaces;

public interface ISeeder<T>
{
    List<T> Seed();
}
