using Swapping;

namespace Magic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Helper.RandomMatrix(Params.row, Params.col, 1, 9);
            int[,] a = {
                { 2, 7, 6 },
                { 9, 5, 1 },
                { 4, 3, 8 }
            };
            IsMagic.Run(a);
        }
    }
}
