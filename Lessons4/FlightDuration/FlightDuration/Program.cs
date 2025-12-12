using System.Text;

namespace FlightDuration
{
    internal class Program
    {
        static int liters;
        static double AB = 0, BC = 0;
        const double GAS_TANK = 300;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.Write("Введіть вагу вантажу (кг): ");
            SetGasoline();

            if (liters == 0) return;

            Console.Write("Введіть відстань A → B (км): ");
            SetDistance();

            Console.Write("Введіть відстань B → C (км): ");
            SetDistance();

            double fuelAB = CalcConsumption(AB);
            double fuelBC = CalcConsumption(BC);

            if (fuelAB > GAS_TANK || fuelBC > GAS_TANK)
            {
                string distanceMessage = ((fuelAB > GAS_TANK) ? $"A -> B {fuelAB}л. на {AB}км." : $"B -> C {fuelBC}л. на {BC}км.");
                Console.WriteLine($"Політ за даним маршрутом неможливий. Бак об'ємом {GAS_TANK}л. а затрати з точки {distanceMessage}");
                return;
            }

            double fuelLeftInB = GAS_TANK - fuelAB;
            double refuelInB = fuelBC - fuelLeftInB;

            if (refuelInB < 0)
                refuelInB = 0;

            Console.WriteLine($"Мінімальна дозаправка в пункті B: {refuelInB} л");
        }

        static void SetGasoline()
        {
            if (!double.TryParse(Console.ReadLine(), out double weight))
            {
                Console.WriteLine("Некорректно введені данні!");
                SetGasoline();
            }

            switch (weight)
            {
                case <= 500:
                    liters = 1;
                    break;
                case <= 1000:
                    liters = 4;
                    break;
                case <= 1500:
                    liters = 7;
                    break;
                case <= 2000:
                    liters = 9;
                    break;
                default:
                    Console.WriteLine("Літачку тяжко він нікуди не полетить!");
                    break;
            }

            return;
        }

        static void SetDistance()
        {
            if (!double.TryParse(Console.ReadLine(), out double distance) || distance <= 0)
            {
                Console.WriteLine("Некорректно введені данні");
                SetDistance();
                return;
            }

            if (AB == 0)
                AB = distance;
            else 
                BC = distance;
        }

        static double CalcConsumption(double distance)
        {
            return distance * liters;
        }
    }
}
