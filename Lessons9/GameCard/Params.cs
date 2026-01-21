using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCard
{
    public static class Params
    {
        public const int size = 9;
        public const char space = ' ';
        public const char ULC = '┌';
        public const char URC = '┐';
        public const char DLC = '└';
        public const char DRC = '┘';
        public const char VB = '│';
        public const char HB = '─';
        public static string[] ranks = { "10", "J", "Q", "K", "A" };
        public static char[] suits = { '♠', '♥', '♦', '♣' };
    }
}
