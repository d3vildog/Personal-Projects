using System;

public class Obj : IEquatable<Obj>
{
    public int X;
    public int Y;
    public ConsoleColor Color;
    public char Symbol;

    public Obj(int x, int y, ConsoleColor color, char symbol)
    {
        this.X = x;
        this.Y = y;
        this.Color = color;
        this.Symbol = symbol;
    }

    public bool Equals(Obj other)
    {
        if (other == null)
        {
            return false;
        }

        if (this.X == other.X && this.Y == other.Y)
        {
            return true;
        }

        return false;
    }
}