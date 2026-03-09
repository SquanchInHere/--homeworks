namespace Cards.Src
{
    public static class PaymentSystemDetector
    {
        private static readonly Dictionary<string, int[]> PrefixRules = new Dictionary<string, int[]>
        {
            { "UnionPay", new[] { 62 } },
            { "Maestro", new[] { 50 } },
            { "Maestro, Discover, UnionPay", new[] { 6 } },
            { "VISA", new[] { 4 } },
            { "MasterCard", new[] { 5 } },
            { "Diners Club", new[] { 30, 36, 38 } },
            { "American Express", new[] { 34, 37 } },
            { "JCB", new[] { 35 } },
            { "Prostir", new[] { 9 } }
        };

        private static readonly Dictionary<string, int[]> RangeRules = new Dictionary<string, int[]>
        {
            { "Maestro", new[] { 56, 69 } }
        };

        public static string Detect(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber))
                return "Unknown";

            foreach (KeyValuePair<string, int[]> rule in PrefixRules)
            {
                foreach (int prefix in rule.Value)
                {
                    if (cardNumber.StartsWith(prefix.ToString()))
                        return rule.Key;
                }
            }

            foreach (KeyValuePair<string, int[]> rule in RangeRules)
            {
                int[] range = rule.Value;

                if (StartsWithRange(cardNumber, range[0], range[1]))
                    return rule.Key;
            }

            return "Unknown";
        }

        private static bool StartsWithRange(string cardNumber, int start, int end)
        {
            if (cardNumber.Length < 2)
                return false;

            string firstTwoDigits = cardNumber.Substring(0, 2);

            int number;
            if (!int.TryParse(firstTwoDigits, out number))
                return false;

            return number >= start && number <= end;
        }
    }
}
