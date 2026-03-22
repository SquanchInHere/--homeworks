using Cipher.Src.Models;

namespace Cipher.Src.Services
{
    public class CipherService
    {
        public List<ACipher> GetSortedACiphers(IEnumerable<string> sourceStrings, int shift = 1)
        {
            HashSet<ACipher> aCiphers = new HashSet<ACipher>();

            foreach (string text in sourceStrings)
            {
                aCiphers.Add(new ACipher(text, shift));
            }

            List<ACipher> sortedACiphers = aCiphers.ToList();
            sortedACiphers.Sort();

            return sortedACiphers;
        }

        public List<BCipher> GetSortedBCiphers(IEnumerable<string> sourceStrings)
        {
            HashSet<BCipher> bCiphers = new HashSet<BCipher>();

            foreach (string text in sourceStrings)
            {
                bCiphers.Add(new BCipher(text));
            }

            List<BCipher> sortedBCiphers = bCiphers.ToList();
            sortedBCiphers.Sort();

            return sortedBCiphers;
        }
    }
}
