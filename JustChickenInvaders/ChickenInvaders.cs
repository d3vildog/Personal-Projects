using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class ChickenInvaders
{
    public static Obj player = new Obj(0, 0, ConsoleColor.Blue, '#');
    public static List<Obj> chickens = new List<Obj>();
    public static List<Obj> shots = new List<Obj>(); 
    public static Obj rocket = new Obj(0, 0, ConsoleColor.Red, 'O');
    public static bool rocketLaunch = false;
    public static int bombsCount = 3;
    public static int chickensKilled = 0;
    public static int playerLives = 3;

    public static Random randomNumberGenerator = new Random();
    static void Main()
    {
        SetGameField();

        while (true)
        { 
            Update();
            Console.Clear();
            Draw();
        }
    }

    private static void Draw()
    {
        DrawPlayer();
        DrawChickens();
        DrawShots();
        DrawUserInfo();
        Thread.Sleep(150);
    }

    private static void DrawUserInfo()
    {
        DrawStringAtPosition(27, 3, ConsoleColor.White, "Lives: " + playerLives);
        DrawStringAtPosition(27, 5, ConsoleColor.White, "Frags: " + chickensKilled);
        DrawStringAtPosition(27, 7, ConsoleColor.White, "Bombs: " + bombsCount);
        DrawStringAtPosition(27, 12, ConsoleColor.White, " Buttons");
        DrawStringAtPosition(28, 14, ConsoleColor.White, "  ↑");
        DrawStringAtPosition(28, 15, ConsoleColor.White, "←   →");
        DrawStringAtPosition(30, 16, ConsoleColor.White, "↓");
        DrawStringAtPosition(27, 18, ConsoleColor.White, "B - Bomb");
        DrawStringAtPosition(27, 20, ConsoleColor.White, "Spacebar ");
        DrawStringAtPosition(27, 21, ConsoleColor.White, " shooot");

        for (int row = 0; row < Console.WindowHeight; row++)
        {
            for (int col = 25; col < 26; col++)
            {
                Console.SetCursorPosition(col, row);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write('|');
            }
        }
    }

    private static void DrawStringAtPosition(int x, int y, ConsoleColor consoleColor, string str)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = consoleColor;
        Console.Write(str);
    }

    private static void DrawShots()
    {
        for (int i = 0; i < shots.Count; i++)
        {
            DrawObjAtPosition(shots[i]);
        }

        if (rocketLaunch)
        {
            DrawObjAtPosition(rocket);
        }
    }

    private static void DrawChickens()
    {
        int randomchickenposition = randomNumberGenerator.Next(0, Console.WindowWidth - 12);

        Obj newchicken = new Obj(randomchickenposition, 0, ConsoleColor.White, 'v');
        chickens.Add(newchicken);

        for (int i = 0; i < chickens.Count; i++)
        {
            DrawObjAtPosition(chickens[i]);
        }
    }

    private static void DrawPlayer()
    {
        for (int i = player.X; i < player.X + 4; i++)
        {
            DrawAtPosition(i, player.Y, '#', ConsoleColor.Blue);
        }
        DrawAtPosition(player.X, player.Y - 1, 'I', ConsoleColor.Gray);
        DrawAtPosition(player.X + 3, player.Y - 1, 'I', ConsoleColor.Gray);
    }

    private static void DrawObjAtPosition(Obj player)
    {
        Console.SetCursorPosition(player.X, player.Y);
        Console.ForegroundColor = player.Color;
        Console.Write(player.Symbol);
    }
    private static void DrawObj(Obj player, int numb)
    {
        Console.SetCursorPosition(player.X, player.Y);
        Console.ForegroundColor = player.Color;
        Console.Write(numb);
    }

    private static void DrawAtPosition(int x, int y, char symbol, ConsoleColor color)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(symbol);
    }

    private static void Update()
    {
        UpdatePlayer();
        UpdateChickens();
        UpdateShots();
    }

    private static void UpdateShots()
     {
        //List<Obj> ListToRemove = new List<Obj>();

        foreach (var shot in shots)
        {
            shot.Y--;
        }

        if (rocketLaunch == true && rocket.Y == Console.WindowHeight / 2)
        {
            rocketLaunch = false;
            rocket.X = 0;
            rocket.Y = 0;
            chickensKilled += chickens.Count;
            chickens.Clear();
        }
        else if (rocket.X < Console.WindowWidth / 2 - 4)
        {
            rocket.X++;
            rocket.Y--;
        }
        else if (rocket.X > Console.WindowWidth / 2 - 4)
        {
            rocket.X--;
            rocket.Y--;
        }
        else
        {
            rocket.Y--;
        }

        shots.RemoveAll(x => x.Y == 0);

        // TODO
        for (int i = 0; i < shots.Count; i++)
        {
            //if (shots.ElementAtOrDefault(i) != null) shots.Remove(shots.Find(x => x.X.Equals(chickens[i].X) && x.Y.Equals(chickens[i].Y)));
            if (chickens.ElementAtOrDefault(i) != null)
            {
                if (chickens.Remove(chickens.Find(x => x.X.Equals(shots[i].X) && x.Y.Equals(shots[i].Y))) == true)
                {
                    shots.Remove(shots[i]);
                    chickensKilled++;
                }
            }
        }
    }

    private static void UpdateChickens()
    {
        List<Obj> chickenstoremove = new List<Obj>();

        for (int i = 0; i < chickens.Count; i++)
        {
            chickens[i].Y++;

            if (chickens[i].Y >= Console.WindowHeight)
            {
                chickenstoremove.Add(chickens[i]);
            }
            //(y-1)        (y-1)
            //(x)(x+1)(x+2)(x+3)
            if ((chickens[i].Y == player.Y - 1 && chickens[i].X == player.X ||
                (chickens[i].Y == player.Y - 1 && chickens[i].X == player.X + 3) ||
                (chickens[i].Y == player.Y && chickens[i].X == player.X + 1) ||
                (chickens[i].Y == player.Y && chickens[i].X == player.X + 2)))
            {
                playerLives--;
                chickenstoremove.Add(chickens[i]);
            }
        }

        for (int i = 0; i < chickenstoremove.Count; i++)
        {
            chickens.Remove(chickenstoremove[i]);
        }
        chickenstoremove.Clear();

        // todo
        for (int i = 0; i < shots.Count; i++)
        {
            //if (shots.elementatordefault(i) != null) shots.remove(shots.find(x => x.x.equals(chickens[i].x) && x.y.equals(chickens[i].y)));
            if (chickens.ElementAtOrDefault(i) != null)
            {
                if (chickens.Remove(chickens.Find(x => x.X.Equals(shots[i].X) && x.Y.Equals(shots[i].Y))) == true)
                {
                    shots.Remove(shots[i]);
                    chickensKilled++;
                }
            }
        }
    }
    
    private static void UpdatePlayer()
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            while (Console.KeyAvailable) Console.ReadKey(true);
            
            if (key.Key == ConsoleKey.Spacebar)
            {
                Obj firstShot = new Obj(player.X, player.Y - 1, ConsoleColor.Red, '|');
                Obj secondShot = new Obj(player.X + 3, player.Y - 1, ConsoleColor.Red, '|');
                shots.Add(firstShot);
                shots.Add(secondShot);
            }
            else if (key.Key == ConsoleKey.B && rocketLaunch != true && bombsCount > 0)
            {
                rocket = new Obj(player.X + 1, player.Y - 1, ConsoleColor.Red, 'O');
                rocketLaunch = true;
                bombsCount--;
            }
            else if (key.Key == ConsoleKey.LeftArrow && player.X > 0)
            {
                player.X--;
            }
            else if (key.Key == ConsoleKey.RightArrow && player.X < Console.WindowWidth - 16)
            {
                player.X++;
            }
        }

        if (playerLives < 0)
        {
            DrawStringAtPosition(27, 24, ConsoleColor.Red, "Game over!");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }

    private static void SetGameField()
    {
        // Console settigs
        Console.WindowHeight = 30;
        Console.WindowWidth = 37;
        Console.BufferHeight = 30;
        Console.BufferWidth = 37;
        Console.CursorVisible = false;
        Console.Title = "Space Frags";

        // Setting up player starting point
        player.X = Console.WindowWidth/2;
        player.Y = Console.WindowHeight - 1;
    }
}

