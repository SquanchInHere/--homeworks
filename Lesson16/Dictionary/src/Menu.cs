namespace Dictionary.src
{
    public static class Menu
    {
        public static void Run(DictionaryManager dict)
        {
            while (true)
            {
                Console.Clear();
                ShowMenu();

                string choice = ReadString("Choose: ");
                bool needPause = true;

                switch (choice)
                {
                    case "1":
                        AddWord(dict);
                        break;

                    case "2":
                        RemoveWord(dict);
                        break;

                    case "3":
                        AddTranslation(dict);
                        break;

                    case "4":
                        ShowTranslations(dict);
                        break;

                    case "5":
                        ShowWordsByLetter(dict);
                        break;

                    case "6":
                        dict.ShowDictionaryPaged();
                        needPause = false;
                        break;

                    case "7":
                        dict.ShowDictionaryByLetterPages();
                        needPause = false;
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Wrong choice.");
                        break;
                }

                if (needPause)
                {
                    Pause();
                }
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("1 Add word");
            Console.WriteLine("2 Remove word");
            Console.WriteLine("3 Add translation");
            Console.WriteLine("4 Show translations");
            Console.WriteLine("5 Words by letter");
            Console.WriteLine("6 Show dictionary by pages");
            Console.WriteLine("7 Show dictionary by letter pages");
            Console.WriteLine("0 Exit");
            Console.WriteLine();
        }

        private static void AddWord(DictionaryManager dict)
        {
            string word = ReadString("Word: ");
            string translation = ReadString("Translation: ");

            dict.AddWord(word, translation);
        }

        private static void RemoveWord(DictionaryManager dict)
        {
            string word = ReadString("Word: ");
            dict.RemoveWord(word);
        }

        private static void AddTranslation(DictionaryManager dict)
        {
            string word = ReadString("Word: ");
            string translation = ReadString("Translation: ");

            dict.AddTranslation(word, translation);
        }

        private static void ShowTranslations(DictionaryManager dict)
        {
            string word = ReadString("Word: ");
            dict.ShowTranslations(word);
        }

        private static void ShowWordsByLetter(DictionaryManager dict)
        {
            string input = ReadString("Letter: ");

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Letter is empty.");
                return;
            }

            dict.ShowWordsByLetter(input[0]);
        }

        private static string ReadString(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        private static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
