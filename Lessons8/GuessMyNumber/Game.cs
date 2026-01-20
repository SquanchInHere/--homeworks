using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessMyNumber
{
    public static class Game
    {
        public static void Run()
        {
            Score score = new Score();

            Console.WriteLine("=== GUESS MY NUMBER ===");

            bool level1Perfect = Level.Play(
                levelNumber: 1,
                min: Params.L1_MIN,
                max: Params.L1_MAX,
                rounds: Params.L1_ROUNDS,
                lifePercent: Params.L1_LIFE_PERCENT,
                playerMult: Params.L1_PLAYER_MULT,
                cpuMult: Params.L1_CPU_MULT,
                score: score
            );

            if (!level1Perfect)
            {
                PrintLose(score);
                return;
            }

            if (Input.YesNo("\nLevel 1 completed without losing. Continue to Level 2? (y/n): "))
            {
                Level.Play(
                    levelNumber: 2,
                    min: Params.L2_MIN,
                    max: Params.L2_MAX,
                    rounds: Params.L2_ROUNDS,
                    lifePercent: Params.L2_LIFE_PERCENT,
                    playerMult: Params.L2_PLAYER_MULT,
                    cpuMult: Params.L2_CPU_MULT,
                    score: score
                );
            }

            PrintEnd(score);
        }

        static void PrintLose(Score score)
        {
            Console.WriteLine("\n=== YOU LOST ===");
            Console.WriteLine("Player score: " + score.Player);
            Console.WriteLine("Computer score: " + score.Cpu);
        }

        static void PrintEnd(Score score)
        {
            Console.WriteLine("\n=== GAME OVER ===");
            Console.WriteLine("Player score: " + score.Player);
            Console.WriteLine("Computer score: " + score.Cpu);
        }
    }
}
