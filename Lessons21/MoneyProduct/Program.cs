using MoneyProduct.Src.Lib;
using MoneyProduct.Src.Services;

try
{
    Money money = new Money(15, 75, "USD");
    Console.WriteLine("Money amount: " + money);

    Product product = new Product("Milk", 25, 50, "UAH");
    Console.WriteLine("Initial product price: " + product);

    product.ReducePrice(5, 25);
    Console.WriteLine("Price after reduction: " + product);
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
