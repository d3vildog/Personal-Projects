namespace BricksBuster
{
    using System;
    
    public class Weapon
    {
        public int leftX { get; set; }
        public int rightX { get; set; }
        public int Y { get; set; }
        public ConsoleColor Color { get; set; }
        public string Symbol { get; set; }
        
        public Weapon()
        {
        }
        
        public Weapon(int leftX, int rightX, int y, ConsoleColor color, string Symbol)
        {
            this.leftX = leftX;
            this.rightX = rightX;
            this.Y = y;
            this.Color = color;
            this.Symbol = Symbol;
        }
    }
}
