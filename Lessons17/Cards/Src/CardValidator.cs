namespace Cards.Src
{
    public static class CardValidator
    {
        public static bool IsValidFormat(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                return false;

            if (cardNumber.Length != 16)
                return false;

            foreach (char c in cardNumber)
            {
                if (!char.IsDigit(c))
                    return false;
            }

            return true;
        }

        public static bool IsValidByLuhn(string cardNumber)
        {
            int sum = 0;
            bool doubleDigit = false;

            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int digit = cardNumber[i] - '0';

                if (doubleDigit)
                {
                    digit *= 2;

                    if (digit > 9)
                    {
                        digit -= 9;
                    }
                }

                sum += digit;
                doubleDigit = !doubleDigit;
            }

            return sum % 10 == 0;
        }
    }
}
