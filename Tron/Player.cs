using System;

namespace Tron
{
    public class Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        public ConsoleColor Color { get; set; }
        public int Direction { get; set; }
        public int Score { get; set; }

        public Player()
        {
        }

        public Player(int x, int y, ConsoleColor color, int direction, int score)
        {
            this.X = x;
            this.Y = y;
            this.Color = color;
            this.Direction = direction;
            this.Score = score;
        }
    }
}
