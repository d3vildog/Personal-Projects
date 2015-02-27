using System;
public class IFigure : Figure
{
    public IFigure()
    {
    }
    
    public IFigure(int x, int y, ConsoleColor color)
        : base(x, y, color)
    {
    }
    
    public override void Draw()
    {
        DrawAtPosition(this.X, this.Y, "#", ConsoleColor.Red);
        DrawAtPosition(this.X + 1, this.Y, "#", ConsoleColor.Red);
        DrawAtPosition(this.X + 2, this.Y, "#", ConsoleColor.Red);
        DrawAtPosition(this.X + 3, this.Y, "#", ConsoleColor.Red);
    }
    
    public override void Update(){
        this.Y++;
    }
}