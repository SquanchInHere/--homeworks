using System;
using System.Text;

namespace WorkDayEnd
{
    internal class Program
    {
        const int workDayHours = 8;
        const double secondsInOneHour = 3600;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Введіть скільки пройшло секунд з початку робочого дня :");
            Console.Write("1 година рівна 3600 секунд: ");
            Int32 seccondsPassed = Convert.ToInt32(Console.ReadLine());

            double totalSeccodsPerDay = ConvertHoursToSeconds(workDayHours, secondsInOneHour);
            double remainingSeconds = CalculeteRemainingSeconds(totalSeccodsPerDay, seccondsPassed);

            CalculateTimeLeft(remainingSeconds);
            
        }

        static double ConvertHoursToSeconds(int hours, double seconds)
        {
            return Convert.ToDouble(hours * seconds);
        }

        static double CalculeteRemainingSeconds(double totalSeconds, double passedSeconds)
        {
            return totalSeconds - passedSeconds;
        }

        static int SeccondsToHours(double seconds)
        {
            return Convert.ToInt16(seconds / secondsInOneHour);
        }

        static int SeccondsToMinutes(double seconds)
        {
            return Convert.ToInt16((seconds % secondsInOneHour) / 60);
        }

        static int GetRemainingSeconds(double seconds)
        {
            return Convert.ToInt16((seconds % secondsInOneHour) % 60);
        }

        static void PrintMessage(int[] hourLeft)
        {
            if (hourLeft[0] <= 0 && hourLeft[1] <= 0 && hourLeft[2] <= 0)
            {
                Console.WriteLine("Робочий день вже закінчився!");
                return;
            }

            Console.WriteLine($"Залишок робочого дня: {hourLeft[0]} годин, {hourLeft[1]} хвилин, {hourLeft[2]} секунд.");
        }

        static void CalculateTimeLeft(double totalSecondsLeft)
        {
            int hours = SeccondsToHours(totalSecondsLeft);
            int minutes = SeccondsToMinutes(totalSecondsLeft);
            int seconds = GetRemainingSeconds(totalSecondsLeft);

            PrintMessage(new int[] { hours, minutes, seconds });
        }
    }
}
