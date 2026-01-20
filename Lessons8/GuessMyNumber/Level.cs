using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessMyNumber
{
    using System;

    public static class Level
    {
        public static bool Play(
            int levelNumber,
            int min,
            int max,
            int rounds,
            int lifePercent,
            int playerMult,
            int cpuMult,
            Score score
        )
        {
            Console.WriteLine();
            Console.WriteLine("=== LEVEL " + levelNumber + " (" + min + "..." + max + "), rounds: " + rounds + " ===");

            bool noLose = true;
            int levelPlayerGain = 0;
            int levelCpuGain = 0;

            int rangeLen = max - min + 1;
            int startLives = CalcLives(rangeLen, lifePercent);

            for (int r = 1; r <= rounds; r++)
            {
                RoundResult rr = Round.Play(
                    min: min,
                    max: max,
                    roundIndex: r,
                    totalRounds: rounds,
                    startLives: startLives,
                    playerMult: playerMult,
                    cpuMult: cpuMult
                );

                if (rr.playerWon)
                {
                    score.Player += rr.playerPoints;
                    levelPlayerGain += rr.playerPoints;

                    Console.WriteLine("WIN! Secret: " + rr.secret);
                    Console.WriteLine("Round points: " + rr.livesLeft + " * " + playerMult + " = " + rr.playerPoints);
                }
                else
                {
                    noLose = false;

                    score.Cpu += rr.cpuPoints;
                    levelCpuGain += rr.cpuPoints;

                    Console.WriteLine("LOSE. Secret was: " + rr.secret);
                    Console.WriteLine("Computer points: " + rr.startLives + " * " + cpuMult + " = " + rr.cpuPoints);
                }

                Console.WriteLine("Current score -> Player: " + score.Player + " | Computer: " + score.Cpu);
            }

            Console.WriteLine();
            Console.WriteLine("=== LEVEL " + levelNumber + " SUMMARY ===");
            Console.WriteLine("Player gained: " + levelPlayerGain);
            Console.WriteLine("Computer gained: " + levelCpuGain);
            Console.WriteLine("Total -> Player: " + score.Player + " | Computer: " + score.Cpu);

            return noLose;
        }

        static int CalcLives(int rangeLen, int percent)
        {
            // ceil(rangeLen * percent / 100)
            int lives = (rangeLen * percent + 99) / 100;
            if (lives < 1) lives = 1;
            return lives;
        }
    }
}
