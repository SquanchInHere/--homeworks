namespace Cards.Src
{
    public static class Menu
    {
        public static void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Bank Card Validator");
                Console.WriteLine("1. Check card number");
                Console.WriteLine("0. Exit");
                Console.Write("Choose option: ");

                string choice = Console.ReadLine()!;

                switch (choice)
                {
                    case "1":
                        CheckCard();
                        Pause();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Invalid option.");
                        Pause();
                        break;
                }
            }
        }

        private static void CheckCard()
        {
            Console.Write("Enter 16-digit card number: ");
            string cardNumber = Console.ReadLine()!;

            if (!CardValidator.IsValidFormat(cardNumber))
            {
                Console.WriteLine("Error: card number must contain exactly 16 digits.");
                return;
            }

            bool isValid = CardValidator.IsValidByLuhn(cardNumber);

            if (!isValid)
            {
                Console.WriteLine("Card number failed the Luhn check.");
                return;
            }

            Console.WriteLine("Card number is valid.");

            string paymentSystem = PaymentSystemDetector.Detect(cardNumber);

            Console.WriteLine("Payment system: " + paymentSystem);
        }

        private static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
