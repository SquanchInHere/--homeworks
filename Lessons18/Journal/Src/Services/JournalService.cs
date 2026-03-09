using Journal.Src.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Journal.Src.Services
{
    public class JournalService
    {
        public void InputData(JournalModel journal)
        {
            Console.Write("Enter journal name: ");
            journal.Name = Console.ReadLine()!;

            Console.Write("Enter foundation year: ");
            journal.Year = int.Parse(Console.ReadLine()!);

            Console.Write("Enter journal description: ");
            journal.Description = Console.ReadLine()!;

            Console.Write("Enter contact phone: ");
            journal.Phone = Console.ReadLine()!;

            Console.Write("Enter email: ");
            journal.Email = Console.ReadLine()!;
        }

        public void ShowData(JournalModel journal)
        {
            Console.WriteLine("\n--- Journal Information ---");
            Console.WriteLine("Name: " + journal.Name);
            Console.WriteLine("Foundation year: " + journal.Year);
            Console.WriteLine("Description: " + journal.Description);
            Console.WriteLine("Phone: " + journal.Phone);
            Console.WriteLine("Email: " + journal.Email);
        }
    }
}
