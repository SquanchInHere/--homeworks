using BankAtm.Src.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankAtm.Src.Models
{
    public class Bank
    {
        private readonly ATM[] atms;

        public string Name { get; }

        public Bank(string name, int atmCount)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new AtmException("Bank name cannot be empty.");

            if (atmCount <= 0)
                throw new AtmException("ATM count must be greater than zero.");

            Name = name.Trim();
            atms = new ATM[atmCount];
        }

        public void SetAtm(int index, ATM atm)
        {
            ValidateAtmIndex(index);

            if (atm == null)
                throw new AtmException("ATM cannot be null.");

            atms[index] = atm;
        }

        public ATM GetAtm(int index)
        {
            ValidateAtmIndex(index);

            if (atms[index] == null)
                throw new AtmException("ATM is not initialized.");

            return atms[index];
        }

        public int GetTotalMoney()
        {
            int total = 0;

            foreach (var atm in atms)
            {
                if (atm != null)
                    total += atm.GetTotalAmount();
            }

            return total;
        }

        public void ShowAllAtms()
        {
            Console.WriteLine($"\nBank: {Name}");
            for (int i = 0; i < atms.Length; i++)
            {
                Console.WriteLine($"--- ATM #{i} ---");
                if (atms[i] == null)
                    Console.WriteLine("Not initialized.");
                else
                    Console.WriteLine(atms[i]);
            }
        }

        private void ValidateAtmIndex(int index)
        {
            if (index < 0 || index >= atms.Length)
                throw new AtmException("Invalid ATM index.");
        }
    }
}
