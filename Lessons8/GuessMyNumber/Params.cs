namespace GuessMyNumber
{

    public static class Params
    {
        public static Random rnd = new Random();
         
        public const int L1_MIN = 1;
        public const int L1_MAX = 10;
        public const int L1_ROUNDS = 3;
        public const int L1_LIFE_PERCENT = 50;
        public const int L1_PLAYER_MULT = 5;
        public const int L1_CPU_MULT = 5;

        public const int L2_MIN = 10;
        public const int L2_MAX = 100;
        public const int L2_ROUNDS = 2;
        public const int L2_LIFE_PERCENT = 25;
        public const int L2_PLAYER_MULT = 10;
        public const int L2_CPU_MULT = 10;
    }
}
