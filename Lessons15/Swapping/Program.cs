namespace Swapping
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] a = Helper.RandomMatrix(Params.row, Params.col, min: 0, max: 20);
            Swap.Run(a);
        }
    }
}
