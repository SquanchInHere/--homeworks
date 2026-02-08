namespace BullsAndCows
{
    public static class BullsAndCows
    {
        public static void Run()
        {
            Console.WriteLine("=== Bulls and Cows (4-digit) ===");
            Console.WriteLine("Rules:");
            Console.WriteLine("- Secret is a 4-digit number with UNIQUE digits.");
            Console.WriteLine("- First digit is NOT zero.");
            Console.WriteLine("- Bulls = how many digits you guessed (any position).");
            Console.WriteLine("- Cows  = how many digits are guessed AND in the correct position.");
            Console.WriteLine();

            string secret = GenerateSecretNumber();
            int attempts = 0;
            int score = 0;

            bool useRecursion = false;

            if (useRecursion)
            {
                PlayGameRecursive(secret, 0, 0);
                return;
            }

            while (true)
            {
                string guess = ReadFourDigitNumber("Enter your guess: ");
                attempts++;

                int cows = CountCows(secret, guess);
                int bulls = CountBulls(secret, guess);

                int roundScore = cows * Params.PointsPerCow + (bulls - cows) * Params.PointsPerBull;

                if (bulls == 0 && cows == 0)
                {
                    roundScore -= Params.PenaltyEmptyRound;
                    Console.WriteLine("No matches this round. Don't worry — next one will be better!");
                }

                score += roundScore;

                Console.WriteLine("Bulls (correct digits, any position): " + bulls);
                Console.WriteLine("Cows  (correct digits in correct place): " + cows);
                Console.WriteLine("Round score: " + roundScore + " | Total score: " + score);
                Console.WriteLine($"Secret: {secret}");

                PrintEncouragement(cows, bulls);

                if (cows == 4)
                {
                    Console.WriteLine();
                    Console.WriteLine("You guessed it! The secret number was: " + secret);
                    Console.WriteLine("Attempts: " + attempts);
                    Console.WriteLine("Final score: " + score);
                    break;
                }

                Console.WriteLine();
            }
        }

        private static string ReadFourDigitNumber(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (input == null)
                {
                    Console.WriteLine("Invalid input. Try again.");
                    continue;
                }

                input = input.Trim();

                if (input.Length != 4)
                {
                    Console.WriteLine("Please enter exactly 4 digits.");
                    continue;
                }

                int number;
                if (!int.TryParse(input, out number))
                {
                    Console.WriteLine("Digits only. Try again.");
                    continue;
                }

                if (input[0] == '0')
                {
                    Console.WriteLine("First digit cannot be zero.");
                    continue;
                }

                if (!HasUniqueDigits(input))
                {
                    Console.WriteLine("Digits must be unique (no repeats).");
                    continue;
                }

                return input;
            }
        }

        private static int CountBulls(string secret, string guess)
        {
            int bulls = 0;

            for (int i = 0; i < 4; i++)
            {
                char g = guess[i];
                if (ContainsChar(secret, g))
                {
                    bulls++;
                }
            }

            return bulls;
        }

        private static int CountCows(string secret, string guess)
        {
            int cows = 0;

            for (int i = 0; i < 4; i++)
            {
                if (secret[i] == guess[i])
                {
                    cows++;
                }
            }

            return cows;
        }

        private static void PrintEncouragement(int cows, int bulls)
        {
            if (cows == 4)
            {
                Console.WriteLine("Perfect! You nailed it!");
                return;
            }

            if (cows == 3)
            {
                Console.WriteLine("So close! One more position to fix!");
                return;
            }

            if (cows == 2)
            {
                Console.WriteLine("Nice! You're halfway locked in!");
                return;
            }

            if (bulls >= 2)
            {
                Console.WriteLine("Good progress — you have the right digits, now fix positions!");
                return;
            }

            if (bulls == 1)
            {
                Console.WriteLine("You found a digit! Keep going!");
                return;
            }

            Console.WriteLine("Tough round. Shake it off and try again!");
        }

        private static string GenerateSecretNumber()
        {
            Random rnd = new Random();
            char[] digits = new char[4];

            digits[0] = (char)('0' + rnd.Next(1, 10));

            for (int i = 1; i < 4; i++)
            {
                while (true)
                {
                    char c = (char)('0' + rnd.Next(0, 10));
                    if (!ContainsCharArray(digits, i, c))
                    {
                        digits[i] = c;
                        break;
                    }
                }
            }

            return new string(digits);
        }

        private static void PlayGameRecursive(string secret, int attempts, int score)
        {
            string guess = ReadFourDigitNumber("Enter your guess: ");
            attempts++;

            int cows = CountCows(secret, guess);
            int bulls = CountBulls(secret, guess);

            int roundScore = cows * Params.PointsPerCow + (bulls - cows) * Params.PointsPerBull;

            if (bulls == 0 && cows == 0)
            {
                roundScore -= Params.PenaltyEmptyRound;
                Console.WriteLine("No matches this round. Don't worry — next one will be better!");
            }

            score += roundScore;

            Console.WriteLine("Bulls (correct digits, any position): " + bulls);
            Console.WriteLine("Cows  (correct digits in correct place): " + cows);
            Console.WriteLine("Round score: " + roundScore + " | Total score: " + score);

            PrintEncouragement(cows, bulls);

            if (cows == 4)
            {
                Console.WriteLine();
                Console.WriteLine("You guessed it! The secret number was: " + secret);
                Console.WriteLine("Attempts: " + attempts);
                Console.WriteLine("Final score: " + score);
                return;
            }

            Console.WriteLine();
            PlayGameRecursive(secret, attempts, score);
        }

        private static bool HasUniqueDigits(string s)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = i + 1; j < 4; j++)
                {
                    if (s[i] == s[j]) return false;
                }
            }
            return true;
        }

        private static bool ContainsChar(string s, char c)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == c) return true;
            }
            return false;
        }

        private static bool ContainsCharArray(char[] arr, int lengthToCheck, char c)
        {
            for (int i = 0; i < lengthToCheck; i++)
            {
                if (arr[i] == c) return true;
            }
            return false;
        }
    }
}
