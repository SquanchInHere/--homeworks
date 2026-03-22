using BankAtm.Src.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankAtm.Src.Data
{
    public static class DataSeed
    {
        public static Bank CreateBank()
        {
            Bank bank = new Bank("State Bank", 2);

            ATM atm1 = new ATM(200, 10000, 20);
            atm1.Initialize(new Dictionary<int, int>
            {
                { 100, 20 },
                { 200, 20 },
                { 500, 10 },
                { 1000, 5 }
            });

            ATM atm2 = new ATM(100, 5000, 10);
            atm2.Initialize(new Dictionary<int, int>
            {
                { 100, 30 },
                { 200, 10 },
                { 500, 5 },
                { 1000, 2 }
            });

            bank.SetAtm(0, atm1);
            bank.SetAtm(1, atm2);

            return bank;
        }
    }
}
