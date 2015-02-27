using System;

namespace Testris
{
    public class OFigure : Figure
    {
        public OFigure(int x, int y, Direction direction)
            : base(x, y, direction)
        {
        }
        
        public void CheckBounderiesLeft(int x, int y)
        {
            if (base.X - 1 < 1)
            {
                base.X++;
            }
        }

        public void CheckBounderiesRight(int x, int y)
        {
            if (base.X + 1 > Start.WindowWidth - Start.ScoreField)
            {
                base.X--;
            }
        }
        
        public void CheckBottomBoundries()
        {
            if (base.y + 3 > Start.WindowHeight - 1)
            {
                Start.NewFigure = true;
                Start.field[base.x, base.y] = true;
                Start.field[base.x - 1, base.y] = true;
                Start.field[base.x - 1, base.y + 1] = true;
                Start.field[base.x, base.y + 1] = true;
            }
        }
        
        public void CheckForEmptyField()
        {
            if (base.y < Console.WindowHeight - 3 && base.x - 1 > 0 && 
                (Start.field[base.x, base.y + 2] == true || Start.field[base.x - 1, base.y + 2] == true))
            {
                Start.NewFigure = true;
                Start.field[base.x, base.y] = true;
                Start.field[base.x - 1, base.y] = true;
                Start.field[base.x - 1, base.y + 1] = true;
                Start.field[base.x, base.y + 1] = true;
            }
        }
        
        public override void Update()
        {
            base.y++;
        }
        
        public override void Draw()
        {
            DrawFigure.DrawSymbolAtPosition(base.x, base.y, "#", ConsoleColor.Blue);
            DrawFigure.DrawSymbolAtPosition(base.x - 1, base.y, "#", ConsoleColor.Blue);
            DrawFigure.DrawSymbolAtPosition(base.x, base.y + 1, "#", ConsoleColor.Blue);
            DrawFigure.DrawSymbolAtPosition(base.x - 1, base.y + 1, "#", ConsoleColor.Blue);
        }
        
        public override void Collisions()
        {
            CheckBottomBoundries();
            CheckForEmptyField();
            CheckBounderiesLeft(base.x, base.y);
            CheckBounderiesRight(base.x, base.y);
        }
    }
}
