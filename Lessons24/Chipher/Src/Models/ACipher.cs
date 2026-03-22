using Cipher.Src.Interfaces;

namespace Cipher.Src.Models
{
    public class ACipher : ICipher, IComparable<ACipher>
    {
        public string OriginalText { get; }
        public int Shift { get; }

        public ACipher(string originalText, int shift = 1)
        {
            OriginalText = originalText ?? string.Empty;
            Shift = shift;
        }

        public string Encode()
        {
            return ShiftText(OriginalText, Shift);
        }

        public string Decode()
        {
            return ShiftText(Encode(), -Shift);
        }

        private string ShiftText(string text, int shift)
        {
            char[] result = new char[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                result[i] = ShiftChar(text[i], shift);
            }

            return new string(result);
        }

        private char ShiftChar(char ch, int shift)
        {
            if (ch >= 'A' && ch <= 'Z')
            {
                return (char)('A' + ((ch - 'A' + shift + 26) % 26));
            }

            if (ch >= 'a' && ch <= 'z')
            {
                return (char)('a' + ((ch - 'a' + shift + 26) % 26));
            }

            return ch;
        }

        public int CompareTo(ACipher other)
        {
            if (other == null)
                return 1;

            return string.Compare(Encode(), other.Encode(), StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return $"ACipher | Encoded: {Encode()}";
        }

        public override bool Equals(object obj)
        {
            if (obj is not ACipher other)
                return false;

            return Encode() == other.Encode();
        }

        public override int GetHashCode()
        {
            return Encode().GetHashCode();
        }
    }
}
