namespace NumberGridPrinter
{
    public static class Printer
    {
        public static void Run()
        {
            int n = ReadInt("Enter n (positive integer): ");
            int k = ReadInt("Enter k (0 < k < n): ");

            while (!IsValid(n, k))
            {
                Console.WriteLine("Invalid values. Condition: n > 0 and 0 < k < n.");
                n = ReadInt("Enter n (positive integer): ");
                k = ReadInt("Enter k (0 < k < n): ");
            }

            int rows = PrintRange(n, k);

            Console.WriteLine("Rows: " + rows);
        }

        private static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = Console.ReadLine();

                int v;
                if (s != null && int.TryParse(s.Trim(), out v))
                    return v;

                Console.WriteLine("Please enter an integer number.");
            }
        }

        private static bool IsValid(int n, int k)
        {
            return n > 0 && k > 0 && k < n;
        }

        private static int PrintRange(int n, int k)
        {
            int fullRows = n / k;
            int lastCount = n % k;
            int totalRows = fullRows + (lastCount > 0 ? 1 : 0);

            PrintRowRec(0, fullRows, lastCount, k);

            return totalRows;
        }

        private static void PrintRowRec(int rowIndex, int fullRows, int lastCount, int k)
        {
            int totalRows = fullRows + (lastCount > 0 ? 1 : 0);
            if (rowIndex >= totalRows) return;

            bool isLastPartial = (lastCount > 0) && (rowIndex == fullRows);
            int countInRow = isLastPartial ? lastCount : k;

            int startValue = rowIndex * k + 1;
            PrintColRec(startValue, countInRow, isLastPartial);

            Console.WriteLine();
            PrintRowRec(rowIndex + 1, fullRows, lastCount, k);
        }

        private static void PrintColRec(int value, int left, bool addFive)
        {
            if (left == 0) return;

            int outVal = addFive ? value + Params.LAST_LINE_ADD : value;
            Console.Write(outVal);

            if (left > 1) Console.Write(' ');

            PrintColRec(value + 1, left - 1, addFive);
        }
    }
}
