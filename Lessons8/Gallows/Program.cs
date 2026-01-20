using System.Text;

namespace Gallows
{
    internal class Program
    {
        static readonly Random rnd = new Random();

        // Canvas for gallow
        const int rows = 7, cols = 9;
        const char LCorner = '┌';
        const char RCorner = '┐';
        const int postCol = 2;
        const int beamRow = 0;
        const int beamLen = 4;

        static readonly char[,] columns = new char[rows, cols];

        /**
         * Words by level
         * 
         * 1 easy
         * 2 medium
         * 3 hard
         */
        static readonly string[][] LevelWords = {
            new[] { "apple", "house", "water" },
            new[] { "doctor", "member", "orange" },
            new[] { "biscuit", "journey", "monster" }
        };

        /**
         * game state
         */
        static int wrongAttempts = 0;
        static int attemptsUsed = 0;
        static int maxTries = 0;

        /**
         * alphabet array
         */
        static readonly bool[] used = new bool[26];
        static readonly bool[] correct = new bool[26];

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            int level = ReadMenuChoice1to4();
            if (level == 4) return;

            string word = GetRandomWord(level).ToLower();

            Array.Clear(used, 0, used.Length);
            Array.Clear(correct, 0, correct.Length);

            wrongAttempts = 0;
            attemptsUsed = 0;
            maxTries = word.Length + 2;

            long sw = Environment.TickCount64;
            bool win = false;
            bool exit = false;

            while (true)
            {
                Console.Clear();
                DrawGallow();
                Console.WriteLine();

                string masked = BuildMaskedWord(word);
                Console.WriteLine($"Word: {masked}");
                Console.WriteLine($"Tries: {attemptsUsed}/{maxTries}   Wrong: {wrongAttempts}/6");
                Console.WriteLine("Letters: " + GetUsedLettersString());
                Console.WriteLine();
                Console.WriteLine("Input: ONE letter (a-z) or WHOLE word. Type '4' or 'exit' to quit.");
                Console.WriteLine();

                if (!masked.Contains('_'))
                {
                    win = true;
                    break;
                }

                if (wrongAttempts >= 6 || attemptsUsed >= maxTries)
                    break;

                string input = ReadGuessInput("Enter: ");

                if (input == "4" || input == "exit")
                {
                    exit = true;
                    break;
                }

                // Guess whole word
                if (input.Length > 1)
                {
                    attemptsUsed++;

                    if (input == word)
                    {
                        win = true;
                        break;
                    }

                    wrongAttempts++;
                    continue;
                }

                // Guess single letter
                char letter = input[0];
                int idx = letter - 'a';
                if (used[idx])
                {
                    Console.WriteLine("You already entered that letter. Press any key...");
                    Console.ReadKey();
                    continue;
                }

                used[idx] = true;
                attemptsUsed++;

                if (word.Contains(letter))
                    correct[idx] = true;
                else
                    wrongAttempts++;
            }

            long ms = Environment.TickCount64 - sw;

            if (exit)
            {
                Console.WriteLine("\nExit. Press any key...");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            DrawGallow();

            TimeSpan duration = TimeSpan.FromMilliseconds(ms);

            PrintFinalStats(win, duration, word);
        }

        /**
         * Menu/Input
         * 
         * 
         * @return void
         */
        static int ReadMenuChoice1to4()
        {
            while (true)
            {
                Console.Write("Select difficulty (1.Easy 2.Medium 3.Hard 4.Exit): ");
                string? s = Console.ReadLine();

                if (int.TryParse(s, out int n) && n >= 1 && n <= 4)
                    return n;

                Console.WriteLine("Enter 1..4.");
            }
        }

        /**
         * Get random word from array
         * 
         * @return string
         */
        static string GetRandomWord(int level)
        {
            level = Math.Clamp(level, 1, 3);
            var list = LevelWords[level - 1];
            return list[rnd.Next(list.Length)];
        }

        /**
         * Try read input
         * 
         * @param string pompt
         * 
         * @return string
         */
        static string ReadGuessInput(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? s = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(s))
                {
                    Console.WriteLine("Empty input.");
                    continue;
                }

                s = s.Trim().ToLower();

                if (s == "4" || s == "exit")
                    return s;

                bool ok = IsAllEnglishLetters(s);
                if (!ok)
                {
                    Console.WriteLine("Use only English letters a..z (or type 'exit' / 4).");
                    continue;
                }

                return s;
            }
        }

        /**
         * Check all english leters
         * 
         * @param string s
         * 
         * @return bool
         */
        static bool IsAllEnglishLetters(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                char ch = s[i];
                bool lower = ch >= 'a' && ch <= 'z';
                if (!lower) return false;
            }
            return true;
        }

        /**
         * Word masking 
         * @param string word
         * 
         * @return string
         */
        static string BuildMaskedWord(string word)
        {
            var sb = new StringBuilder(word.Length * 2);
            foreach (char ch in word)
            {
                if (ch >= 'a' && ch <= 'z' && correct[ch - 'a'])
                    sb.Append(ch);
                else
                    sb.Append('_');

                sb.Append(' ');
            }
            return sb.ToString().TrimEnd();
        }

        /**
         * Get used leters
         * 
         * @return string
         */
        static string GetUsedLettersString()
        {
            var list = new List<char>();
            for (int i = 0; i < 26; i++)
                if (used[i]) list.Add((char)('a' + i));

            return list.Count == 0 ? "-" : string.Join(", ", list);
        }

        /**
         * Print stats
         * 
         * @param bool $win
         * @param TimeSpan $time
         * @param string $word
         * 
         * @return void
         */
        static void PrintFinalStats(bool win, TimeSpan time, string word)
        {
            Console.WriteLine();
            Console.WriteLine(win ? "You WIN!" : "You LOSE!");
            Console.WriteLine($"Time: {time:mm\\:ss}");
            Console.WriteLine($"Attempts used: {attemptsUsed}/{maxTries}");
            Console.WriteLine($"Wrong attempts: {wrongAttempts}/6");
            Console.WriteLine($"Word: {word}");
            Console.WriteLine("Letters: " + GetUsedLettersString());
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        /**
         * Drawing gallow
         * 
         * @return void
         */
        static void DrawGallow()
        {
            int ropeCol = postCol + beamLen;

            // fill spaces
            for (int r = 0; r < rows; r++)
                for (int col = 0; col < cols; col++)
                    columns[r, col] = ' ';

            // top corners
            columns[beamRow, postCol] = LCorner;
            columns[beamRow, ropeCol] = RCorner;

            // beam
            for (int x = postCol + 1; x < ropeCol; x++)
                columns[beamRow, x] = '-';

            // post
            for (int r = beamRow + 1; r < rows - 1; r++)
                columns[r, postCol] = '|';

            // rope
            columns[beamRow + 1, ropeCol] = '|';

            // ground
            for (int x = 0; x < cols; x++)
                columns[rows - 1, x] = '=';

            // supports: /|\
            int baseRow = rows - 1;
            if (postCol - 1 >= 0) columns[baseRow - 1, postCol - 1] = '/';
            columns[baseRow - 1, postCol] = '|';
            if (postCol + 1 < cols) columns[baseRow - 1, postCol + 1] = '\\';

            DrawBody(wrongAttempts, ropeCol);

            // print
            for (int r = 0; r < rows; r++)
            {
                for (int col = 0; col < cols; col++)
                    Console.Write(columns[r, col]);
                Console.WriteLine();
            }
        }

        /**
         * Draw body
         * 
         * @param int wrong
         * @param int ropeCol
         * 
         * @return void
         */
        static void DrawBody(int wrong, int ropeCol)
        {
            wrong = Math.Clamp(wrong, 0, 6);

            for (int i = 1; i <= wrong; i++)
            {
                int r, c;
                char ch;

                switch (i)
                {
                    case 1: r = 2; c = ropeCol; ch = 'O'; break;
                    case 2: r = 3; c = ropeCol; ch = '|'; break;
                    case 3: r = 3; c = ropeCol - 1; ch = '/'; break;
                    case 4: r = 3; c = ropeCol + 1; ch = '\\'; break;
                    case 5: r = 4; c = ropeCol - 1; ch = '/'; break;
                    default: r = 4; c = ropeCol + 1; ch = '\\'; break;
                }

                if (r >= 0 && r < rows && c >= 0 && c < cols)
                    columns[r, c] = ch;
            }
        }
    }
}
