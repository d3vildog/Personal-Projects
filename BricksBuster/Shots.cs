namespace BricksBuster
{
    using System;
    
    public class Shots
    {
        public int leftX { get; set; }
        public int rightX { get; set; }
        public int Y { get; set; }
        public ConsoleColor Color { get; set; }
        public string Symbol { get; set; }
        
        public Shots()
        {
        }
        
        public Shots(int leftX, int rightX, int y, ConsoleColor color, string Symbol)
        {
            this.leftX = leftX;
            this.rightX = rightX;
            this.Y = y;
            this.Color = color;
            this.Symbol = Symbol;
        }
    }
}
