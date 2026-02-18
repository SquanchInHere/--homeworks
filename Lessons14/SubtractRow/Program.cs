namespace SubtractRow
{
    internal class Program
    {
        static void Main()
        {
            int[,] a = Matrix.CreateRandomMatrix(minValue: 0, maxValue: 10);
            Matrix.Run(a);
        }
    }
}
