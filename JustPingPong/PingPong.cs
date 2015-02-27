using System;
using System.Threading;

class PingPong
{
    // ball variables
    private static int BallXPosition = 0;
    private static int BallYPosition = 0;
    private static bool MovingUp = true;
    private static bool MovingLeft = false;
    private static char BallSymbol = 'O';

    // first player variables
    private const int FirstPlayerStickSize = 5;
    private static int FirstPlayerCurrentPosition = 0;
    private static int FirstPLayerScore = 0;

    // second player variables
    private const int SecondPlayerStickSize = 5;
    private static int SecondPlayerCurrentPosition = 0;
    private static int SecondPlayerScore = 0;

    private static void Main()
    {
        SetGameField();

        while (true)
        {
            MovingObjects();
            Console.Clear();
            DrawingObjects();
            Thread.Sleep(35);
        }
    }

    private static void MovingObjects()
    {
        MoveFirstPlayer();
        MoveSecondPlayer();
        MoveBall();
    }

    private static void DrawingObjects()
    {
        DrawFirstPlayer();
        DrawSecondPlayer();
        DrawBall();
        DrawScore();
    }

    private static void MoveBall()
    {
        if (BallYPosition == 1)
        {
            MovingUp = false;
        }
        if (BallYPosition == Console.WindowHeight - 2)
        {
            MovingUp = true;
        }

        if (BallXPosition == 2 &&  BallYPosition >= FirstPlayerCurrentPosition - 1 && 
            BallYPosition  <= FirstPlayerCurrentPosition + 4)
        {
            MovingLeft = false;
        }

        if (BallXPosition == Console.WindowWidth - 3 && BallYPosition >= SecondPlayerCurrentPosition - 1&&
            BallYPosition <= SecondPlayerCurrentPosition + 4)
        {
            MovingLeft = true;
        }

        if (BallXPosition == 0)
        {
            SecondPlayerScore++;
            BallXPosition = Console.WindowWidth / 2;
            BallYPosition = Console.WindowHeight / 2;
            MovingLeft = false;
            Console.SetCursorPosition(Console.WindowWidth / 2 - 12, Console.WindowHeight / 2);
            Console.Write("Press any key to continue..");
            Console.ReadKey();
        }

        if (BallXPosition == Console.WindowWidth - 2)
        {
            FirstPLayerScore++;
            BallXPosition = Console.WindowWidth / 2;
            BallYPosition = Console.WindowHeight / 2;
            MovingLeft = true;
            Console.SetCursorPosition(Console.WindowWidth / 2 - 12, Console.WindowHeight / 2);
            Console.Write("Press any key to continue..");
            Console.ReadKey();
        }

        if (MovingUp)
        {
            BallYPosition--;
        }
        else
        {
            BallYPosition++;
        }

        if (MovingLeft)
        {
            BallXPosition--;
        }
        else
        {
            BallXPosition++;
        }
    }

    private static void MoveSecondPlayer()
    {
        Random randomGenerator = new Random();
        int randomNumber = randomGenerator.Next(1, 101);

        if (randomNumber <= 70)
        {
            if (SecondPlayerCurrentPosition < BallYPosition && SecondPlayerCurrentPosition + 6 <= Console.WindowHeight - 1)
            {
                SecondPlayerCurrentPosition++;
            }
            else if (SecondPlayerCurrentPosition > BallYPosition && SecondPlayerCurrentPosition >= 1)
            {
                SecondPlayerCurrentPosition--;
            }
        }
    }

    private static void MoveFirstPlayer()
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            while (Console.KeyAvailable){Console.ReadKey(true);}
            if (keyInfo.Key == ConsoleKey.UpArrow && FirstPlayerCurrentPosition > 1)
            {
                FirstPlayerCurrentPosition--;
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow && FirstPlayerCurrentPosition + 5 < Console.WindowHeight - 1)
            {
                FirstPlayerCurrentPosition++;
            }
        }
    }

    private static void DrawScore()
    {
        Console.SetCursorPosition(Console.WindowWidth / 2 - 1, 0);
        Console.Write("{0}-{1}", FirstPLayerScore, SecondPlayerScore);
    }

    private static void DrawBall()
    {
        DrawAtPosition(BallXPosition, BallYPosition, BallSymbol);
    }

    private static void DrawFirstPlayer()
    {
        for (int i = FirstPlayerCurrentPosition; i < FirstPlayerStickSize + FirstPlayerCurrentPosition; i++)
        {
            DrawAtPosition(0, i, '|');
            DrawAtPosition(1, i, '|'); 
        }
    }

    private static void DrawSecondPlayer()
    {
        for (int i = SecondPlayerCurrentPosition; i < SecondPlayerStickSize + SecondPlayerCurrentPosition; i++)
        {
            DrawAtPosition(Console.WindowWidth - 1, i, '|');
            DrawAtPosition(Console.WindowWidth - 2, i, '|');
        }
    }

    private static void DrawAtPosition(int x, int y, char symbol)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(symbol);
    }

    private static void SetGameField() 
    {
        // Game field settings
        Console.CursorVisible = false;
        Console.WindowHeight = 30;
        Console.BufferHeight = Console.WindowHeight;
        Console.WindowWidth = 60;
        Console.BufferWidth = Console.WindowWidth;

        // First player starting position
        FirstPlayerCurrentPosition = Console.WindowHeight / 2 - FirstPlayerStickSize / 2;

        // Second player starting position
        SecondPlayerCurrentPosition = Console.WindowHeight / 2 - SecondPlayerStickSize / 2;

        // Ball stating position
        BallXPosition = Console.WindowWidth / 2;
        BallYPosition = Console.WindowHeight / 2;
    }
}