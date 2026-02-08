namespace FilterArray
{
    public static class Filter
    {
        public static void Run()
        {
            int[] original = { 1, 2, 6, -1, 88, 7, 6 };
            int[] filter = { 6, 88, 7 };

            int[] result = FilterArray(original, filter);

            PrintArr(result);
        }

        private static void PrintArr(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (i > 0) Console.Write(" ");
                Console.Write(arr[i]);
            }
            Console.WriteLine();
        }

        private static int[] FilterArray(int[] original, int[] filter)
        {
            if (original == null) return new int[0];
            if (filter == null || filter.Length == 0) return CopyArray(original);

            int count = 0;
            for (int i = 0; i < original.Length; i++)
            {
                if (!Contains(filter, original[i]))
                    count++;
            }

            int[] result = new int[count];

            int k = 0;
            for (int i = 0; i < original.Length; i++)
            {
                if (!Contains(filter, original[i]))
                {
                    result[k] = original[i];
                    k++;
                }
            }

            return result;
        }

        private static bool Contains(int[] arr, int value)
        {
            for (int i = 0; i < arr.Length; i++)
                if (arr[i] == value)
                    return true;
            return false;
        }

        private static int[] CopyArray(int[] src)
        {
            int[] dst = new int[src.Length];
            for (int i = 0; i < src.Length; i++)
                dst[i] = src[i];
            return dst;
        }
    }
}
