using System;
using System.Text;

namespace ConvertNumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            string result = "";

            for (int i = 0; i < 6; i++)
            {
                Console.Write("Ввудіть число: ");
                string input = Convert.ToString(Console.ReadLine());

                result = result + input;
            }

            Console.WriteLine(result);
        }
    }
}
