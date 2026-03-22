using BankAtm.Src.Exceptions;
using BankAtm.Src.Services;

namespace BankAtm.Src
{
    public class Menu
    {
        private readonly BankService bankService;

        public Menu(BankService bankService)
        {
            this.bankService = bankService;
        }

        public void Run()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\n=== BANK ATM MENU ===");
                Console.WriteLine("1. Show all ATMs");
                Console.WriteLine("2. Show total money in ATM network");
                Console.WriteLine("3. Deposit money into ATM");
                Console.WriteLine("4. Withdraw money from ATM");
                Console.WriteLine("5. Show ATM state");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            bankService.ShowAllAtms();
                            break;

                        case "2":
                            bankService.ShowTotalMoney();
                            break;

                        case "3":
                            DepositMenu();
                            break;

                        case "4":
                            WithdrawMenu();
                            break;

                        case "5":
                            ShowAtmStateMenu();
                            break;

                        case "0":
                            isRunning = false;
                            Console.WriteLine("Exiting program...");
                            break;

                        default:
                            Console.WriteLine("Invalid option. Try again.");
                            break;
                    }
                }
                catch (AtmException ex)
                {
                    Console.WriteLine($"ATM error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }
            }
        }

        private void DepositMenu()
        {
            int atmIndex = ReadInt("Enter ATM index: ");

            Dictionary<int, int> banknotes = new Dictionary<int, int>();

            Console.WriteLine("Enter banknotes to deposit.");
            banknotes[100] = ReadInt("100 UAH count: ");
            banknotes[200] = ReadInt("200 UAH count: ");
            banknotes[500] = ReadInt("500 UAH count: ");
            banknotes[1000] = ReadInt("1000 UAH count: ");

            bankService.DepositToAtm(atmIndex, banknotes);
        }

        private void WithdrawMenu()
        {
            int atmIndex = ReadInt("Enter ATM index: ");
            int amount = ReadInt("Enter withdrawal amount: ");

            bankService.WithdrawFromAtm(atmIndex, amount);
        }

        private void ShowAtmStateMenu()
        {
            int atmIndex = ReadInt("Enter ATM index: ");
            bankService.ShowAtmState(atmIndex);
        }

        private int ReadInt(string message)
        {
            int value;

            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out value) && value >= 0)
                    return value;

                Console.WriteLine("Invalid number. Try again.");
            }
        }
    }
}
