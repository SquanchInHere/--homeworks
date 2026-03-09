using System;
using System.Collections.Generic;
using System.Text;

namespace Dictionary.src
{
    public class DictionaryManager
    {
        private readonly Dictionary<string, List<string>> _dictionary;

        public DictionaryManager(Dictionary<string, List<string>> initialData = null)
        {
            _dictionary = initialData ?? new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
        }

        public void AddWord(string word, string translation)
        {
            if (!TryNormalizeWordAndTranslation(ref word, ref translation))
            {
                Console.WriteLine("Word or translation is empty.");
                return;
            }

            word = word.ToLower();

            List<string> translations;
            if (!_dictionary.TryGetValue(word, out translations))
            {
                translations = new List<string>();
                _dictionary[word] = translations;
            }

            if (TranslationExists(translations, translation))
            {
                Console.WriteLine("This translation already exists.");
                return;
            }

            translations.Add(translation);
            Console.WriteLine("Added successfully.");
        }

        public void RemoveWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                Console.WriteLine("Word is empty.");
                return;
            }

            word = word.Trim().ToLower();

            if (_dictionary.Remove(word))
                Console.WriteLine("Word removed successfully.");
            else
                Console.WriteLine("Word not found.");
        }

        public void AddTranslation(string word, string translation)
        {
            AddWord(word, translation);
        }

        public void ShowTranslations(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                Console.WriteLine("Word is empty.");
                return;
            }

            word = word.Trim();

            List<string> translations;
            if (!_dictionary.TryGetValue(word, out translations))
            {
                Console.WriteLine("Word not found.");
                return;
            }

            Console.WriteLine("\n" + word + ":");
            for (int i = 0; i < translations.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + translations[i]);
            }
        }

        public void ShowWordsByLetter(char letter)
        {
            List<string> words = GetWordsByLetter(letter);

            if (words.Count == 0)
            {
                Console.WriteLine("No words found.");
                return;
            }

            Console.WriteLine("\nWords starting with '" + letter + "':");
            foreach (string word in words)
            {
                Console.WriteLine(word);
            }
        }

        public void ShowDictionaryPaged(int pageSize = 2)
        {
            if (pageSize <= 0)
            {
                Console.WriteLine("Invalid page size.");
                return;
            }

            List<string> keys = GetSortedKeys();

            if (keys.Count == 0)
            {
                Console.WriteLine("Dictionary is empty.");
                return;
            }

            int totalPages = (keys.Count + pageSize - 1) / pageSize;

            PaginationHelper.RunPages(totalPages, currentPage =>
            {
                Console.WriteLine("Dictionary page " + (currentPage + 1) + "/" + totalPages);
                Console.WriteLine();

                int start = currentPage * pageSize;
                int end = start + pageSize;

                if (end > keys.Count)
                    end = keys.Count;

                for (int i = start; i < end; i++)
                {
                    string key = keys[i];
                    PrintWordWithTranslations(key);
                }
            });
        }

        public void ShowDictionaryByLetterPages()
        {
            List<char> letters = GetSortedFirstLetters();

            if (letters.Count == 0)
            {
                Console.WriteLine("Dictionary is empty.");
                return;
            }

            int totalPages = letters.Count;

            PaginationHelper.RunPages(totalPages, currentPage =>
            {
                char currentLetter = letters[currentPage];

                Console.WriteLine("Letter " + currentLetter + " (" + (currentPage + 1) + "/" + totalPages + ")");
                Console.WriteLine();

                List<string> words = GetWordsByLetter(currentLetter);

                foreach (string word in words)
                {
                    PrintWordWithTranslations(word);
                }
            });
        }

        private bool TryNormalizeWordAndTranslation(ref string word, ref string translation)
        {
            if (string.IsNullOrWhiteSpace(word) || string.IsNullOrWhiteSpace(translation))
                return false;

            word = word.Trim();
            translation = translation.Trim();

            return true;
        }

        private bool TranslationExists(List<string> translations, string translation)
        {
            foreach (string item in translations)
            {
                if (item.Equals(translation, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        private List<string> GetSortedKeys()
        {
            List<string> keys = new List<string>();

            foreach (string key in _dictionary.Keys)
            {
                keys.Add(key);
            }

            keys.Sort();
            return keys;
        }

        private List<char> GetSortedFirstLetters()
        {
            List<char> letters = new List<char>();

            foreach (string word in _dictionary.Keys)
            {
                if (string.IsNullOrEmpty(word))
                    continue;

                char firstLetter = char.ToUpper(word[0]);

                if (!letters.Contains(firstLetter))
                {
                    letters.Add(firstLetter);
                }
            }

            letters.Sort();
            return letters;
        }

        private List<string> GetWordsByLetter(char letter)
        {
            List<string> words = new List<string>();
            char targetLetter = char.ToLower(letter);

            foreach (string word in _dictionary.Keys)
            {
                if (string.IsNullOrEmpty(word))
                    continue;

                if (char.ToLower(word[0]) == targetLetter)
                {
                    words.Add(word);
                }
            }

            words.Sort();
            return words;
        }

        private void PrintWordWithTranslations(string word)
        {
            Console.WriteLine(word);

            List<string> translations = _dictionary[word];
            foreach (string translation in translations)
            {
                Console.WriteLine("   - " + translation);
            }

            Console.WriteLine();
        }
    }
}
