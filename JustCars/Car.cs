using System;

class Car
{
    public int X;
    public int Y;
    public ConsoleColor Color;
    public char Symbol;

    public Car()
    {
        
    }

    public Car(int x, int y, ConsoleColor color, char symbol)
    {
        this.X = x;
        this.Y = y;
        this.Color = color;
        this.Symbol = symbol;
    }
}