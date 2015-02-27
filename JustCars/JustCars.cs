using System;
using System.Collections.Generic;
using System.Threading;

class JustCars
{
    private static  Car playerCar = new Car(0, 0, ConsoleColor.Blue, '#');
    private static int playerScore = 0;
    private static int playerLives = 3;
    private static bool enemyHitted = false;
    private static bool bonusLiveTaken = false;
    private static bool bonusScoreTaken = false;
    private static int count = 0;
    private static bool jam = false;
    private static int divider = 5;
    private static int timerForLives = 0;
    private static int timerForScore = 0;

    private static List<Car> enemyCars = new List<Car>();
    private static List<Bonus> lifeBonuses = new List<Bonus>();
    private static List<Bonus> scoreBonuses = new List<Bonus>(); 

    private static Random randomNumberGenerator = new Random();
    private const int GameFieldWidth = 7;

    static void Main()
    {
        SetGameField();
        startScreen();

        while (true)
        {
            playerScore += 2;
            count++;
            MovingUserCar();
            CreatingAndMoveObjects();
            Console.Clear();
            ReDrawUserCar();
            ReDrawEnemyCarsAndBonuses();
            ReDrawInfo();
            Thread.Sleep(100);
        }
    }

    private static void startScreen()
    {
        String c = "\t******" +
                      "\n\t**" +
                      "\n\t*" +
                      "\n\t**" +
                      "\n\t******\n";
        String a = "  \t   **" +
                   "\n\t **  **" +
                   "\n\t********" +
                   "\n\t**   ***" +
                   "\n\t**   ***\n";

        String r = "\t*******" +
                   "\n\t**   **" +
                   "\n\t*******" +
                   "\n\t****" +
                   "\n\t**  **" +
                   "\n\t**    *\n";

        String s = "\t  ****" +
                   "\n\t**" +
                   "\n\t** " +
                   "\n\t  ****" +
                   "\n\t      **" +
                   "\n\t      **" +
                   "\n\t  ****";

        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.Write("\n\n" + c + "\n" + a + "\n" + r + "\n" + s);
        Console.ReadLine();
    }

    private static void ReDrawEnemyCarsAndBonuses()
    {
        foreach (var enemyCar in enemyCars)
        {
            DrawCarAtPosition(enemyCar);
        }

        foreach (var bonus in lifeBonuses)
        {
            DrawBonusAtPosition(bonus);
        }

        foreach (var bonus in scoreBonuses)
        {
            DrawBonusAtPosition(bonus);
        }
    }

    private static void CreatingAndMoveObjects()
    {
        int randomNumber = randomNumberGenerator.Next(0, GameFieldWidth);
        int randomNumberForObjects = randomNumberGenerator.Next(1, 2000);
        if (randomNumberForObjects < 5)
        {
            Bonus newLiveBonus = new Bonus(randomNumber, 0, ConsoleColor.Green, 'B');
            lifeBonuses.Add(newLiveBonus);
        }
        else if (randomNumberForObjects < 50)
        {
            Bonus newScoreBonus = new Bonus(randomNumber, 0, ConsoleColor.Yellow, '$');
            scoreBonuses.Add(newScoreBonus);
        }
        else if (count % divider == 0)
        {
            Car newCar = new Car(randomNumber, 0, ConsoleColor.Gray, 'V');
            enemyCars.Add(newCar);

            if (count > 400)
            {
                jam = true;
                divider = 1;
                if (count == 500)
                {
                    divider = 5;
                    count = 0;
                    jam = false;
                }
            }
        }

        for (int i = 0; i < enemyCars.Count; i++)
        {
            enemyCars[i].Y++;
            int enemyCarY = enemyCars[i].Y;
            Car currCar = enemyCars[i];

            if (enemyCars[i].X == playerCar.X && enemyCars[i].Y == playerCar.Y)
            {
                enemyCars.Clear();
                lifeBonuses.Clear();
                scoreBonuses.Clear();
                playerLives--;
                enemyHitted = true;
                Console.Beep(4502, 100);

                if (playerLives < 0)
                {
                    DrawAtPosition(GameFieldWidth + 3, 9, "Game over!", ConsoleColor.Red);
                    Console.ReadLine();
                    Environment.Exit(0);
                }
            }

            if (enemyCarY == Console.WindowHeight)
            {
                enemyCars.Remove(currCar);
            }
        }

        for (int i = 0; i < lifeBonuses.Count; i++)
        {
            lifeBonuses[i].Y++;
            int bonusY = lifeBonuses[i].Y;
            Bonus currBonus = lifeBonuses[i];

            if (lifeBonuses[i].X == playerCar.X && lifeBonuses[i].Y == playerCar.Y)
            {
                lifeBonuses.Clear();
                bonusLiveTaken = true;
                playerLives++;
            }

            if (bonusY == Console.WindowHeight)
            {
                lifeBonuses.Remove(currBonus);
            }
        }

        for (int i = 0; i < scoreBonuses.Count; i++)
        {
            scoreBonuses[i].Y++;
            int bonusY = scoreBonuses[i].Y;
            Bonus currBonus = scoreBonuses[i];

            if (scoreBonuses[i].X == playerCar.X && scoreBonuses[i].Y == playerCar.Y)
            {
                scoreBonuses.Remove(currBonus);
                bonusScoreTaken = true;
                playerScore+=200;
            }

            if (bonusY == Console.WindowHeight)
            {
                scoreBonuses.Remove(currBonus);
            }
        }

        if (bonusLiveTaken)
        {
            timerForLives++;
        }

        if (bonusScoreTaken)
        {
            timerForScore++;
        }
    }

    private static void MovingUserCar()
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            while (Console.KeyAvailable) Console.ReadKey(true);
            if (key.Key == ConsoleKey.LeftArrow && playerCar.X > 0)
            {
                playerCar.X--;
            }
            else if (key.Key == ConsoleKey.RightArrow && playerCar.X < GameFieldWidth - 1)
            {
                playerCar.X++;
            }
        }
        if (count == 500)
        {
            playerCar.Y--;
        }
    }

    private static void ReDrawInfo()
    {
        SpecialDraw(GameFieldWidth + 3, 5, "Lives: ", playerLives.ToString() + '♥', ConsoleColor.Green);
        SpecialDraw(GameFieldWidth + 3, 7, "Score: ", playerScore.ToString() + "$", ConsoleColor.Yellow);

        for (int col = 0; col < Console.WindowHeight; col++)
        {
            DrawAtPosition(GameFieldWidth, col, "|", ConsoleColor.DarkGray);
        }

        if (jam)
        {
            DrawAtPosition(10, 9, "JAM OF CARS!", ConsoleColor.Red);
        }
    }

    private static void ReDrawUserCar()
    {
        if (bonusLiveTaken)
        {
            DrawAtPosition(GameFieldWidth + 12, 5, "+1", ConsoleColor.Green);

            if (timerForLives == 20)
            {
                bonusLiveTaken = false;
                timerForLives = 0;
            }
        }

        if (bonusScoreTaken)
        {
            DrawAtPosition(GameFieldWidth + 10, 6, "200$", ConsoleColor.Yellow);

            if (timerForScore == 20)
            {
                bonusScoreTaken = false;
                timerForScore = 0;
            }
        }

        if (enemyHitted)
        {
            DrawAtPosition(playerCar.X, playerCar.Y, "X", ConsoleColor.Red);
            Thread.Sleep(250);
            enemyHitted = false;
        }
        else
        {
            DrawCarAtPosition(playerCar);
        }
    }

    private static void DrawCarAtPosition(Car playerCar)
    {
        Console.SetCursorPosition(playerCar.X, playerCar.Y);
        Console.ForegroundColor = playerCar.Color;
        Console.Write(playerCar.Symbol);
    }

    private static void DrawBonusAtPosition(Bonus bonus)
    {
        Console.SetCursorPosition(bonus.X, bonus.Y);
        Console.ForegroundColor = bonus.Color;
        Console.Write(bonus.Symbol);
    }

    private static void DrawAtPosition(int x, int y, String txt, ConsoleColor color)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(txt);
    }

    private static void SpecialDraw(int x, int y, String firstTxt, String secondTxt, ConsoleColor color)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = ConsoleColor.White; 
        Console.Write(firstTxt);

        Console.ForegroundColor = color;
        Console.Write(secondTxt);
    }

    private static void SetGameField()
    {
        // Field settings
        Console.CursorVisible = false;
        Console.WindowHeight = 30;
        Console.WindowWidth = 23;
        Console.BufferHeight = Console.WindowHeight;
        Console.BufferWidth = Console.WindowWidth;
        Console.Title = "Just Cars";

        // Setting up user starting point 
        playerCar.X = GameFieldWidth/2;
        playerCar.Y = Console.WindowHeight - 2;
    }
}
