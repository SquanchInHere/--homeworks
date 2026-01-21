using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcSalary
{
    internal class Calculate
    {
        public static void Run(double hours = 45)
        {
            CalcSalary(hours, out double gross, out double tax, out double net);

            Console.WriteLine("Hours: " + hours);
            Console.WriteLine("Gross: " + gross);
            Console.WriteLine("Tax:   " + tax);
            Console.WriteLine("Net:   " + net);
        }

        private static void CalcSalary(double hours, out double gross, out double tax, out double net)
        {
            double basePay;
            double overtimePay;

            if (hours <= Params.BASE_HOURS)
            {
                basePay = hours * Params.BASE_RATE;
                overtimePay = 0.0;
            }
            else
            {
                basePay = Params.BASE_HOURS * Params.BASE_RATE;
                overtimePay = (hours - Params.BASE_HOURS) * Params.OVERTIME_RATE;
            }

            gross = basePay + overtimePay;
            tax = CalcTax(gross);
            net = gross - tax;
        }

        private static double CalcTax(double gross)
        {
            double tax = 0.0;
            double part1 = Math.Min(gross, Params.FIRST_BRACKET_LIMIT);
            tax += Percent(part1, Params.FIRST_TAX_PERCENT);
            double rest = gross - Params.FIRST_BRACKET_LIMIT;

            if (rest > 0)
            {
                double part2 = Math.Min(rest, Params.SECOND_BRACKET_LIMIT);
                tax += Percent(part2, Params.SECOND_TAX_PERCENT);
                double part3 = rest - Params.SECOND_BRACKET_LIMIT;

                if (part3 > 0) tax += part3 * 0.05;
            }

            return tax;
        }

        private static double Percent(double value, int percent)
        {
            return value * percent / 100.0;
        }
    }
}
