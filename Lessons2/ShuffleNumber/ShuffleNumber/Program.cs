using System;
using System.Text;

namespace ShuffleNumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Write("Введіть число: ");
            string number = Console.ReadLine();
            Console.WriteLine(number);
            var list = number.ToString().ToCharArray();

            if (list.Length > 6 || list.Length < 6)
            {
                Console.WriteLine("Помилка: число повинно містити не більше 6 цифр та не меньше 6 цифр.");
                return;
            }

            Console.Write("Введіть номер першого розряду (від 1 до 6): ");
            int d1 = int.Parse(Console.ReadLine()) - 1;

            Console.Write("Введіть номер другого розряду (від 1 до 6): ");
            int d2 = int.Parse(Console.ReadLine()) - 1;

            if (list[d1] == null && list[d2] == null)
            {
                Console.WriteLine("Помилка: введені розряди виходять за межі числа.");
                return;
            }


            var tmp = list[d1];
            list[d1] = list[d2];
            list[d2] = tmp;
            Console.WriteLine(list);
            return;
        }
    }
}
