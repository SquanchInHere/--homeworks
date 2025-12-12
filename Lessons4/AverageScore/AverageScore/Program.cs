using System.Text;

namespace AverageScore
{
    internal class Program
    {
        static double grade = 0;
        static int countGrade = 0;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Введіть 5 оцінок студента щоб розрахувати середній бал:");
            SetGrade();
        }

        static void SetGrade()
        {
            if (countGrade >= 5)
            {
                double average = CalcAverage();
                Console.WriteLine($"Cередній бал студенту {average} {((average >= 4) ? "допущений до іспиту" : "не допущений до іспиту")}");
                return;
            }

            Console.Write($"Введіть оцінку : ");

            if (!double.TryParse(Console.ReadLine(), out double input) || input > 5 || input < 0)
            {
                Console.WriteLine("Некорректно введена оцінка оцінка не може бути більше 5 та менше 0");
                SetGrade();
            }
            else
            {
                countGrade++;
                grade += input;
                SetGrade();
            }
        }

        static double CalcAverage()
        {
            return grade / countGrade;
        }
    }
}
