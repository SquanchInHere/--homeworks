using System;
using System.Collections.Generic;

namespace Spiral
{
    class Program
    {

        static void Main(string[] args)
        {
            Spiral.Run(Helper.RandomMatrix(Params.row, Params.col, Params.min, Params.max));
        }
    }
}