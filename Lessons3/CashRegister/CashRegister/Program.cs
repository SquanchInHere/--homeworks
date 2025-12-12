using System;
using System.Text;

namespace CashRegister
{
    internal class Program
    {
        static int q1 = 10, q2 = 8, q3 = 15, q4 = 20;

        static decimal p1 = 100, p2 = 150, p3 = 200, p4 = 50;

        static decimal totalIncome = 0;

        static int productSet = 0;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            ServeClient();
        }

        static void ServeClient()
        {
            ProductList();
            SetProduct();
        }

        static void ProductList()
        {
            Console.WriteLine("--- Вибір товару ---");
            Console.WriteLine($"1 — Гірлянда ({p1} грн) | Залишок: {q1}");
            Console.WriteLine($"2 — Іграшка ({p2} грн)  | Залишок: {q2}");
            Console.WriteLine($"3 — Зірка ({p3} грн)    | Залишок: {q3}");
            Console.WriteLine($"4 — Мішура ({p4} грн)   | Залишок: {q4}");
            Console.WriteLine("0 — Завершити покупку");
        }

        static void SetProduct()
        {
            Console.Write("Введіть цифру від 0 до 4: ");

            if (!int.TryParse(Console.ReadLine(), out productSet))
            {
                Console.WriteLine("Некоректний ввод!");
                ProductList();
                SetProduct();
                return;
            }

            if (productSet == 0)
            {
                EndWork();
                return;
            }

            ushort amount = SetAmount();

            switch (productSet)
            {
                case 1:
                    q1 = ChangeRemainingGoods(q1, ref amount);
                    totalIncome += CalcPrice(p1, amount);
                    break;

                case 2:
                    q2 = ChangeRemainingGoods(q2, ref amount);
                    totalIncome += CalcPrice(p2, amount);
                    break;

                case 3:
                    q3 = ChangeRemainingGoods(q3, ref amount);
                    totalIncome += CalcPrice(p3, amount);
                    break;

                case 4:
                    q4 = ChangeRemainingGoods(q4, ref amount);
                    totalIncome += CalcPrice(p4, amount);
                    break;

                default:
                    Console.WriteLine("Такого товару не існує!");
                    ProductList();
                    SetProduct();
                    return;
            }

            Console.Write("Додати ще товар? (y/n): ");
            string? c = Console.ReadLine()?.Trim().ToLower();

            if (c == "y")
            {
                ProductList();
                SetProduct();
                return;
            }

            EndWork();
        }

        static ushort SetAmount()
        {
            Console.Write("Введіть кількість товару: ");

            if (!ushort.TryParse(Console.ReadLine(), out ushort amount))
            {
                Console.WriteLine("Помилка! Введіть число!");
                return SetAmount();
            }

            return amount;
        }

        static int ChangeRemainingGoods(int qty, ref ushort amount)
        {
            if (amount > qty)
            {
                Console.WriteLine($"Недостатньо товару. Залишок: {qty}");
                Console.Write("Змінити кількість? (y/n): ");

                string? c = Console.ReadLine()?.Trim().ToLower();

                if (c == "y")
                {
                    amount = SetAmount();
                    return ChangeRemainingGoods(qty, ref amount);
                }
                else
                {
                    EndWork();
                }
            }

            return qty - amount;
        }

        static decimal CalcPrice(decimal price, int amount)
        {
            return price * amount;
        }

        static void EndWork()
        {
            Console.WriteLine($"\nЗагальна виручка: {totalIncome} грн");

            Console.Write("Обслуговувати наступного клієнта? (y/n): ");
            string? c = Console.ReadLine()?.Trim().ToLower();

            if (c == "y")
            {
                ServeClient();
                return;
            }

            Console.WriteLine("Магазин закривається!");
        }
    }
}
