using BankAtm.Src.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankAtm.Src.Models
{
    public class ATM
    {
        private static readonly int[] AllowedDenominations = { 100, 200, 500, 1000 };

        private readonly Dictionary<int, int> banknotes;

        public decimal MinWithdrawalAmount { get; }
        public decimal MaxWithdrawalAmount { get; }
        public int MaxBanknotesPerWithdrawal { get; }

        public ATM(decimal minWithdrawalAmount, decimal maxWithdrawalAmount, int maxBanknotesPerWithdrawal)
        {
            if (minWithdrawalAmount <= 0)
                throw new InvalidAmountException("Minimum withdrawal amount must be greater than zero.");

            if (maxWithdrawalAmount < minWithdrawalAmount)
                throw new InvalidAmountException("Maximum withdrawal amount cannot be less than minimum withdrawal amount.");

            if (maxBanknotesPerWithdrawal <= 0)
                throw new AtmLimitExceededException("Maximum number of banknotes per withdrawal must be greater than zero.");

            MinWithdrawalAmount = minWithdrawalAmount;
            MaxWithdrawalAmount = maxWithdrawalAmount;
            MaxBanknotesPerWithdrawal = maxBanknotesPerWithdrawal;

            banknotes = new Dictionary<int, int>
            {
                { 100, 0 },
                { 200, 0 },
                { 500, 0 },
                { 1000, 0 }
            };
        }

        public void Initialize(Dictionary<int, int> initialBanknotes)
        {
            if (initialBanknotes == null)
                throw new AtmException("Initialization data cannot be null.");

            foreach (var denomination in AllowedDenominations)
            {
                banknotes[denomination] = 0;
            }

            foreach (var pair in initialBanknotes)
            {
                ValidateDenomination(pair.Key);

                if (pair.Value < 0)
                    throw new InvalidAmountException($"Banknote count cannot be negative for denomination {pair.Key}.");

                banknotes[pair.Key] = pair.Value;
            }
        }

        public void Deposit(int denomination, int count)
        {
            ValidateDenomination(denomination);

            if (count <= 0)
                throw new InvalidAmountException("Banknote count for deposit must be greater than zero.");

            banknotes[denomination] += count;
        }

        public void Deposit(Dictionary<int, int> depositBanknotes)
        {
            if (depositBanknotes == null)
                throw new AtmException("Deposit data cannot be null.");

            foreach (var pair in depositBanknotes)
            {
                Deposit(pair.Key, pair.Value);
            }
        }

        public Dictionary<int, int> Withdraw(int amount)
        {
            ValidateWithdrawalAmount(amount);

            if (GetTotalAmount() < amount)
                throw new InsufficientFundsException("ATM does not have enough money.");

            var dispensed = BuildDispensePlan(amount);

            foreach (var pair in dispensed)
            {
                banknotes[pair.Key] -= pair.Value;
            }

            return dispensed;
        }

        public int GetTotalAmount()
        {
            return banknotes.Sum(pair => pair.Key * pair.Value);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendLine("ATM state:");
            builder.AppendLine($"  100 UAH: {banknotes[100]}");
            builder.AppendLine($"  200 UAH: {banknotes[200]}");
            builder.AppendLine($"  500 UAH: {banknotes[500]}");
            builder.AppendLine($"  1000 UAH: {banknotes[1000]}");
            builder.AppendLine($"  Total amount: {GetTotalAmount()} UAH");
            builder.AppendLine($"  Min withdrawal: {MinWithdrawalAmount} UAH");
            builder.AppendLine($"  Max withdrawal: {MaxWithdrawalAmount} UAH");
            builder.AppendLine($"  Max banknotes per withdrawal: {MaxBanknotesPerWithdrawal}");

            return builder.ToString();
        }

        private void ValidateDenomination(int denomination)
        {
            if (!AllowedDenominations.Contains(denomination))
                throw new InvalidDenominationException(denomination);
        }

        private void ValidateWithdrawalAmount(int amount)
        {
            if (amount <= 0)
                throw new InvalidAmountException("Withdrawal amount must be greater than zero.");

            if (amount % 100 != 0)
                throw new InvalidAmountException("Withdrawal amount must be a multiple of 100.");

            if (amount < MinWithdrawalAmount)
                throw new AtmLimitExceededException($"Withdrawal amount cannot be less than {MinWithdrawalAmount}.");

            if (amount > MaxWithdrawalAmount)
                throw new AtmLimitExceededException($"Withdrawal amount cannot be greater than {MaxWithdrawalAmount}.");
        }

        private Dictionary<int, int> BuildDispensePlan(int amount)
        {
            var denominations = AllowedDenominations.OrderByDescending(x => x).ToArray();

            Dictionary<int, int>? bestPlan = null;
            int bestBanknoteCount = int.MaxValue;

            void Search(int index, int remainingAmount, Dictionary<int, int> currentPlan, int currentBanknoteCount)
            {
                if (currentBanknoteCount > MaxBanknotesPerWithdrawal)
                    return;

                if (remainingAmount == 0)
                {
                    if (currentBanknoteCount < bestBanknoteCount)
                    {
                        bestBanknoteCount = currentBanknoteCount;
                        bestPlan = new Dictionary<int, int>(currentPlan);
                    }

                    return;
                }

                if (index >= denominations.Length)
                    return;

                int denomination = denominations[index];
                int availableCount = banknotes[denomination];
                int maxNeeded = remainingAmount / denomination;
                int maxUse = Math.Min(availableCount, maxNeeded);

                for (int count = maxUse; count >= 0; count--)
                {
                    currentPlan[denomination] = count;
                    Search(index + 1, remainingAmount - denomination * count, currentPlan, currentBanknoteCount + count);
                }

                currentPlan.Remove(denomination);
            }

            Search(0, amount, new Dictionary<int, int>(), 0);

            if (bestPlan == null)
                throw new CannotDispenseAmountException("The ATM cannot dispense the requested amount with available banknotes and limits.");

            return bestPlan.Where(pair => pair.Value > 0)
                           .ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }
}
