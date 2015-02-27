using System.Threading;

namespace Tron
{
    using System;

    class Tron
    {
        public static Player LeftPlayer;
        public static Player RightPlayer;
        public static bool[,] IsUsed;

        static void Main()
        {
            SetUpGameField();

            while (true)
            {
                Update();
                Draw();

                Thread.Sleep(100);
            }
        }

        private static void Draw()
        {
            DrawPlayer();
            DrawScore();
        }

        private static void DrawScore()
        {
            Console.SetCursorPosition(40, 0);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("{0}-{1}", LeftPlayer.Score, RightPlayer.Score);
        }

        private static void DrawPlayer()
        {
            Console.SetCursorPosition(RightPlayer.X, RightPlayer.Y);
            Console.ForegroundColor = RightPlayer.Color;
            Console.Write("*");

            Console.SetCursorPosition(LeftPlayer.X, LeftPlayer.Y);
            Console.ForegroundColor = LeftPlayer.Color;
            Console.Write("@");
        }

        private static void Update()
        {
            IsUsed[LeftPlayer.X, LeftPlayer.Y] = true;
            IsUsed[RightPlayer.X, RightPlayer.Y] = true;

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                while (Console.KeyAvailable) { Console.ReadKey(true); }

                // RIGHT PLAYER
                if (key.Key == ConsoleKey.LeftArrow && RightPlayer.Direction != 1)
                {
                    RightPlayer.Direction = 3;
                }

                if (key.Key == ConsoleKey.RightArrow && RightPlayer.Direction != 3)
                {
                    RightPlayer.Direction = 1;
                }

                if (key.Key == ConsoleKey.UpArrow && RightPlayer.Direction != 2)
                {
                    RightPlayer.Direction = 0;
                }

                if (key.Key == ConsoleKey.DownArrow && RightPlayer.Direction != 0)
                {
                    RightPlayer.Direction = 2;
                }


                // LEFT PLAYER
                if (key.Key == ConsoleKey.A && LeftPlayer.Direction != 1)
                {
                    LeftPlayer.Direction = 3;
                }

                if (key.Key == ConsoleKey.D && LeftPlayer.Direction != 3)
                {
                    LeftPlayer.Direction = 1;
                }

                if (key.Key == ConsoleKey.W && LeftPlayer.Direction != 2)
                {
                    LeftPlayer.Direction = 0;
                }

                if (key.Key == ConsoleKey.S && LeftPlayer.Direction != 0)
                {
                    LeftPlayer.Direction = 2;
                }
            }

            MovePlayers(LeftPlayer);
            MovePlayers(RightPlayer);

            if (IsUsed[LeftPlayer.X, LeftPlayer.Y] == true)
            {
                RightPlayer.Score++;
                Console.Clear();

                LeftPlayer.X = 0;
                LeftPlayer.Y = Console.WindowHeight / 2;
                LeftPlayer.Direction = 1;

                RightPlayer.X = Console.WindowWidth - 1;
                RightPlayer.Y = Console.WindowHeight / 2;
                RightPlayer.Direction = 3;

                IsUsed = new bool[Console.WindowWidth, Console.LargestWindowHeight];
            }
            if (IsUsed[RightPlayer.X, RightPlayer.Y] == true)
            {
                LeftPlayer.Score++;
                Console.Clear();

                LeftPlayer.X = 0;
                LeftPlayer.Y = Console.WindowHeight / 2;
                LeftPlayer.Direction = 1;

                RightPlayer.X = Console.WindowWidth - 1;
                RightPlayer.Y = Console.WindowHeight / 2;
                RightPlayer.Direction = 3;

                IsUsed = new bool[Console.WindowWidth, Console.LargestWindowHeight];
            }
        }

        private static void MovePlayers(Player player)
        {
            if (player.Direction == 0)
            {
                player.Y--;
            }
            if (player.Direction == 1)
            {
                player.X++;
            }
            if (player.Direction == 2)
            {
                player.Y++;
            }
            if (player.Direction == 3)
            {
                player.X--;
            }
        }

        private static void SetUpGameField()
        {
            Console.WindowHeight = 50;
            Console.WindowWidth = 80;
            Console.BufferHeight = 50;
            Console.BufferWidth = 80;

            LeftPlayer = new Player(0, Console.WindowHeight / 2, ConsoleColor.Red, 1, 0);
            RightPlayer = new Player(Console.WindowWidth - 1, Console.WindowHeight / 2, ConsoleColor.Green, 3, 0);

            IsUsed = new bool[Console.WindowWidth, Console.LargestWindowHeight];
        }
    }
}
