namespace BricksBuster
{
    using System;
    
    public class StickandBall
    {
        public int X { get; set; }
        public int Y { get; set; }
        public ConsoleColor Color { get; set; }
        public int Length { get; set; }
        public string Symbol { get; set; }
        public bool DirectionLeft { get; set; }
        public bool DirectionUp { get; set; }
        
        public StickandBall()
        {
            
        }
        
        public StickandBall(int x,int y, ConsoleColor color, int length, string symbol)
        {
            this.X = x;
            this.Y = y;
            this.Color = color;
            this.Length = length;        
            this.Symbol = symbol;
        }
        
        public StickandBall(int x,int y, ConsoleColor color, int length, string symbol, bool DirectionLeft, bool DirectionUp)
        {
            this.X = x;
            this.Y = y;
            this.Color = color;
            this.Length = length;        
            this.Symbol = symbol;
            this.DirectionLeft = DirectionLeft;
            this.DirectionUp = DirectionUp;
        }
    }
}
