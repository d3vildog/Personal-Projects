namespace BricksBuster
{
    using System;

    public class Bonus
    {
        public int X { get; set; }
        public int Y { get; set; }
        public ConsoleColor Color { get; set; }
        
        public Bonus(int x, int y, ConsoleColor color)
        {
            this.X = x;
            this.Y = y;
            this.Color = color;
        }
    }
}
