using System;

public class Bonus
{
    public int X;
    public int Y;
    public ConsoleColor Color;
    public char Symbol;

    public Bonus(int x, int y, ConsoleColor color, char symbol)
    {
        this.X = x;
        this.Y = y;
        this.Color = color;
        this.Symbol = symbol;
    }
}