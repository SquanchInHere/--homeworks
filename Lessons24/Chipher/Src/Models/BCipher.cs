using Cipher.Src.Interfaces;

namespace Cipher.Src.Models
{
    public class BCipher : ICipher, IComparable<BCipher>
    {
        public string OriginalText { get; }

        public BCipher(string originalText)
        {
            OriginalText = originalText ?? string.Empty;
        }

        public string Encode()
        {
            return TransformText(OriginalText);
        }

        public string Decode()
        {
            return TransformText(Encode());
        }

        private string TransformText(string text)
        {
            char[] result = new char[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                result[i] = MirrorChar(text[i]);
            }

            return new string(result);
        }

        private char MirrorChar(char ch)
        {
            if (ch >= 'A' && ch <= 'Z')
            {
                return (char)('Z' - (ch - 'A'));
            }

            if (ch >= 'a' && ch <= 'z')
            {
                return (char)('z' - (ch - 'a'));
            }

            return ch;
        }

        public int CompareTo(BCipher? other)
        {
            if (other == null)
                return 1;

            return string.Compare(Encode(), other.Encode(), StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return $"BCipher | Encoded: {Encode()}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is not BCipher other)
                return false;

            return Encode() == other.Encode();
        }

        public override int GetHashCode()
        {
            return Encode().GetHashCode();
        }
    }
}
