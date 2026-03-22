using BankAtm.Src.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankAtm.Src.Services
{
    public class BankService
    {
        private readonly Bank bank;

        public BankService(Bank bank)
        {
            this.bank = bank;
        }

        public void ShowAllAtms()
        {
            bank.ShowAllAtms();
        }

        public void ShowTotalMoney()
        {
            Console.WriteLine($"Total money in bank ATM network: {bank.GetTotalMoney()} UAH");
        }

        public void DepositToAtm(int atmIndex, Dictionary<int, int> banknotes)
        {
            ATM atm = bank.GetAtm(atmIndex);
            atm.Deposit(banknotes);

            Console.WriteLine($"Money deposited into ATM #{atmIndex}.");
        }

        public void WithdrawFromAtm(int atmIndex, int amount)
        {
            ATM atm = bank.GetAtm(atmIndex);
            Dictionary<int, int> withdrawn = atm.Withdraw(amount);

            Console.WriteLine("Dispensed banknotes:");
            foreach (var pair in withdrawn)
            {
                Console.WriteLine($"{pair.Key} UAH x {pair.Value}");
            }
        }

        public void ShowAtmState(int atmIndex)
        {
            ATM atm = bank.GetAtm(atmIndex);
            Console.WriteLine(atm);
        }
    }
}
