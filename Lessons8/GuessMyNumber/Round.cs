using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessMyNumber
{
    public static class Round
    {
        public static RoundResult Play(
            int min,
            int max,
            int roundIndex,
            int totalRounds,
            int startLives,
            int playerMult,
            int cpuMult
        )
        {
            int lives = startLives;
            int secret = Params.rnd.Next(min, max + 1);

            Console.WriteLine();
            Console.WriteLine("-- Round " + roundIndex + "/" + totalRounds + " --");
            Console.WriteLine("I guessed a number. Your lives: " + lives);

            while (lives > 0)
            {
                int guess = Input.ReadInt("Enter your guess: ");

                if (guess == secret)
                {
                    RoundResult win = new RoundResult();
                    win.playerWon = true;
                    win.secret = secret;
                    win.startLives = startLives;
                    win.livesLeft = lives;
                    win.playerPoints = lives * playerMult;
                    win.cpuPoints = 0;
                    return win;
                }

                lives--;
                Console.WriteLine("Wrong. -1 life. Lives left: " + lives);
                if (lives <= 0) break;

                if (Input.YesNo("Want a hint for 1 life? (y/n): "))
                {
                    lives--;
                    if (lives < 0) lives = 0;

                    if (secret > guess) Console.WriteLine("Hint: secret number is GREATER than your guess.");
                    else Console.WriteLine("Hint: secret number is LESS than your guess.");

                    Console.WriteLine("Hint cost: -1 life. Lives left: " + lives);
                    if (lives <= 0) break;
                }
            }

            RoundResult lose = new RoundResult();
            lose.playerWon = false;
            lose.secret = secret;
            lose.startLives = startLives;
            lose.livesLeft = 0;
            lose.playerPoints = 0;
            lose.cpuPoints = startLives * cpuMult;
            return lose;
        }
    }
}
