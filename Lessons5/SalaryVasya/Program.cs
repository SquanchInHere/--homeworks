using System.Text;

namespace SalaryVasya
{
    class Program
    {
        const decimal PAY_PER_100_LINES = 150;
        const int LINES_BLOCK = 100;
        const decimal EVERY_THIRD_LATES = 20;
        const int NUMBER_DELAY = 3;

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                Menu();

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Некорректно введені данні!");
                }

                if (choice == 0) break;

                switch (choice)
                {
                    case 1:
                        Task1();
                        break;
                    case 2:
                        Task2();
                        break;
                    case 3:
                        Task3();
                        break;
                    default:
                        Console.WriteLine("Нема такого пункту.");
                        break;
                }
            }
        }

        static void Menu()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n==== MENU ====");
            Console.WriteLine("1) Дохід + запізнення -> скільки рядків треба написати");
            Console.WriteLine("2) Рядки + бажана зарплата -> скільки разів можна запізнитись");
            Console.WriteLine("3) Рядки + запізнення -> скільки заплатять і чи заплатять взагалі");
            Console.WriteLine("0) Вихід");
            Console.ResetColor();
            Console.Write("Ваш вибір: ");
        }

        static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int v) && v >= 0) return v;
                Console.WriteLine("Введіть ціле число >= 0.");
            }
        }

        static decimal ReadDecimal(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out decimal v)) return v;
                Console.WriteLine("Введіть число (наприклад 250 або 250.5).");
            }
        }

        static void Task1()
        {
            decimal desiredIncome = ReadDecimal("Введіть бажаний дохід ($): ");
            int late = ReadInt("Введіть кількість запізнень: ");

            decimal fines = (late / NUMBER_DELAY) * EVERY_THIRD_LATES;
            decimal needGross = desiredIncome + fines;
            if (needGross < 0) needGross = 0;

            decimal moneyPerLine = PAY_PER_100_LINES / LINES_BLOCK;
            decimal x = needGross / moneyPerLine;
            int linesNeeded = (int)decimal.Truncate(x) + (x == decimal.Truncate(x) ? 0 : 1);

            Console.WriteLine($"Штрафи: {fines}$");
            Console.WriteLine($"Потрібно написати рядків: {linesNeeded}");
        }

        static void Task2()
        {
            int lines = ReadInt("Введіть кількість написаних рядків: ");
            decimal desiredSalary = ReadDecimal("Введіть бажану зарплату ($): ");

            decimal baseSalary = lines * (PAY_PER_100_LINES / LINES_BLOCK);
            decimal diff = baseSalary - desiredSalary;

            if (diff < 0)
            {
                Console.WriteLine($"Навіть без запізнень зарплата буде {baseSalary}$, це менше за бажану.");
                Console.WriteLine("Максимум запізнень: 0");
                return;
            }

            int maxPenaltyGroups = (int)(diff / 20);
            int maxLate = maxPenaltyGroups * NUMBER_DELAY + 2;

            Console.WriteLine($"Базова зарплата без штрафів: {baseSalary}$");
            Console.WriteLine($"Максимум запізнень: {maxLate}");
        }

        static void Task3()
        {
            int lines = ReadInt("Введіть кількість рядків: ");
            int late = ReadInt("Введіть кількість запізнень: ");

            decimal baseSalary = lines * (PAY_PER_100_LINES / LINES_BLOCK);
            decimal fines = (late / NUMBER_DELAY) * EVERY_THIRD_LATES;
            decimal finalSalary = baseSalary - fines;

            Console.WriteLine($"Базова зарплата: {baseSalary}$");
            Console.WriteLine($"Штрафи: {fines}$");

            if (finalSalary <= 0)
            {
                Console.WriteLine("В результаті нічого не заплатять.");
            }
            else
            {
                Console.WriteLine($"Заплатять: {finalSalary}$");
            }
        }
    }
}
