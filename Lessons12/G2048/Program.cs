namespace G2048
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] grid = new int[4, 4]
            {
                { 0, 0, 2, 8 },
                { 2, 0, 2, 4 },
                { 0, 0, 0, 4 },
                { 0, 0, 0, 2 }
            };

            Move.Run(grid, MoveDir.Left);
            Move.Run(grid, MoveDir.Right);
            Move.Run(grid, MoveDir.Up);
            Move.Run(grid, MoveDir.Down);
        }
    }
}
