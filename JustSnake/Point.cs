using System;

namespace JustSnake
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public ConsoleColor Color { get; set; }

        public Point(int x, int y, ConsoleColor color)
        {
            this.X = x;
            this.Y = y;
            this.Color = color;
        }
    }
}
