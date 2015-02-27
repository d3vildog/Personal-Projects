using System;

namespace Testris
{
    public class TFigure : Figure
    {
        public TFigure(int x, int y, Direction direction)
            : base(x, y, direction)
        {
        }

        public void CheckBounderiesLeft(int x, int y)
        {
            if (base.Direction == Direction.Down || base.Direction == Direction.Left || base.Direction == Direction.Up)
            {
                if (base.X - 1 < 1)
                {
                    base.X++;
                }
            }

            if (base.Direction == Direction.Right)
            {
                if (base.X < 1)
                {
                    base.X++;
                }
            }
        }

        public void CheckBounderiesRight(int x, int y)
        {
            if (base.Direction == Direction.Down || base.Direction == Direction.Up)
            {
                if (base.X + 1 > Start.WindowWidth - Start.ScoreField)
                {
                    base.X--;
                }
            }

            if (base.direction == Direction.Left)
            {
                if (base.X + 1 > Start.WindowWidth - Start.ScoreField + 1)
                {
                    base.X--;
                }
            }

            if (base.Direction == Direction.Right)
            {
                if (base.X > Start.WindowWidth - 2 - Start.ScoreField + 1)
                {
                    base.X--;
                }
            }
        }

        public void CheckBottomBoundries()
        {
            if (base.direction == Direction.Down)
            {
                if (base.y + 2 > Start.WindowHeight - 1)
                {
                    Start.NewFigure = true;
                    Start.field[base.x - 1, base.y - 1] = true;
                    Start.field[base.x, base.y - 1] = true;
                    Start.field[base.x + 1, base.y - 1] = true;
                    Start.field[base.x, base.y] = true;
                }
            }

            if (base.direction == Direction.Right)
            {
                if (base.y + 2 > Start.WindowHeight - 1)
                {
                    Start.NewFigure = true;
                    Start.field[base.x, base.y - 2] = true;
                    Start.field[base.x, base.y - 1] = true;
                    Start.field[base.x, base.y] = true;
                    Start.field[base.x + 1, base.y - 1] = true;
                }
            }

            if (base.direction == Direction.Up)
            {
                if (base.y + 2 > Start.WindowHeight - 1)
                {
                    Start.NewFigure = true;
                    Start.field[base.x - 1, base.y] = true;
                    Start.field[base.x, base.y] = true;
                    Start.field[base.x + 1, base.y] = true;
                    Start.field[base.x, base.y - 1] = true;
                }
            }

            if (base.direction == Direction.Left)
            {
                if (base.y + 2 > Start.WindowHeight - 1)
                {
                    Start.NewFigure = true;
                    Start.field[base.x, base.y - 2] = true;
                    Start.field[base.x, base.y - 1] = true;
                    Start.field[base.x, base.y] = true;
                    Start.field[base.x - 1, base.y - 1] = true;
                }
            }
        }

        public void CheckForEmptyField()
        {
            if (base.Direction == Direction.Up)
            {
                if (base.y < Console.WindowHeight - 2 && base.x - 1 > 0 && base.x + 1 < Start.WindowWidth - 1 && (Start.field[base.x - 1, base.y + 1] == true ||
                    Start.field[base.x, base.y + 1] == true || Start.field[base.x + 1, base.y + 1] == true))
                {
                    Start.NewFigure = true;
                    Start.field[base.x - 1, base.y] = true;
                    Start.field[base.x, base.y] = true;
                    Start.field[base.x + 1, base.y] = true;
                    Start.field[base.x, base.y - 1] = true;
                }
            }

            if (base.Direction == Direction.Right)
            {
                if (base.y < Console.WindowHeight - 2 && base.x > 0 && base.x + 1 < Start.WindowWidth - 1 && (Start.field[base.x + 1, base.y] == true || Start.field[base.x, base.y + 1] == true))
                {
                    Start.NewFigure = true;
                    Start.field[base.x, base.y - 2] = true;
                    Start.field[base.x, base.y - 1] = true;
                    Start.field[base.x, base.y] = true;
                    Start.field[base.x + 1, base.y - 1] = true;
                }
            }

            if (base.Direction == Direction.Left)
            {
                if (base.y < Console.WindowHeight - 3 &&  base.x - 1 > 0 && base.x + 1 < Start.WindowWidth - 1 && (Start.field[base.x, base.y + 1] == true || Start.field[base.x - 1, base.y] == true))
                {
                    Start.NewFigure = true;
                    Start.field[base.x, base.y - 2] = true;
                    Start.field[base.x, base.y - 1] = true;
                    Start.field[base.x, base.y] = true;
                    Start.field[base.x - 1, base.y - 1] = true;
                }
            }

            if (base.Direction == Direction.Down)
            {
                if (base.y < Console.WindowHeight - 2 && base.x - 1 > 0 && base.x + 1 < Start.WindowWidth - 1 &&  (Start.field[base.x - 1, base.y] == true || Start.field[base.x, base.y + 1] == true || Start.field[base.x + 1, base.y] == true))
                {
                    Start.NewFigure = true;
                    Start.field[base.x - 1, base.y - 1] = true;
                    Start.field[base.x, base.y - 1] = true;
                    Start.field[base.x + 1, base.y - 1] = true;
                    Start.field[base.x, base.y] = true;
                }
            }
        }
        
        public override void Collisions(){
            CheckBottomBoundries();
            CheckForEmptyField();
            CheckBounderiesLeft(base.x, base.y);
            CheckBounderiesRight(base.x, base.y);
        }

        public override void Draw()
        {
            if (base.Direction == Direction.Down)
            {
                DrawFigure.DrawSymbolAtPosition(x - 1, y, "#", color);
                DrawFigure.DrawSymbolAtPosition(x, y, "#", color);
                DrawFigure.DrawSymbolAtPosition(x + 1, y, "#", color);
                DrawFigure.DrawSymbolAtPosition(x, y + 1, "#", color);
            }

            if (base.Direction == Direction.Right)
            {
                DrawFigure.DrawSymbolAtPosition(x, y - 1, "#", color);
                DrawFigure.DrawSymbolAtPosition(x, y, "#", color);
                DrawFigure.DrawSymbolAtPosition(x, y + 1, "#", color);
                DrawFigure.DrawSymbolAtPosition(x + 1, y, "#", color);
            }

            if (base.Direction == Direction.Up)
            {
                DrawFigure.DrawSymbolAtPosition(x - 1, y, "#", color);
                DrawFigure.DrawSymbolAtPosition(x, y, "#", color);
                DrawFigure.DrawSymbolAtPosition(x + 1, y, "#", color);
                DrawFigure.DrawSymbolAtPosition(x, y - 1, "#", color);
            }

            if (base.Direction == Direction.Left)
            {
                DrawFigure.DrawSymbolAtPosition(x, y - 1, "#", color);
                DrawFigure.DrawSymbolAtPosition(x, y, "#", color);
                DrawFigure.DrawSymbolAtPosition(x, y + 1, "#", color);
                DrawFigure.DrawSymbolAtPosition(x - 1, y, "#", color);
            }
        }
        
        public override void Update()
        {
            base.y++;
        }
    }
}