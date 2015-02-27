using System;

namespace Testris
{
    public class Player
    {
        private int points;
        private Figure figure;
        private static Random randomNumber = new Random();

        public Player(int x, int y, Direction direction)
        {
            int randomfigure = (randomNumber.Next(0,2));
            
            if (randomfigure == 0) {
                figure = new TFigure(x, y, direction);                
            }else if (randomfigure == 1) {
                figure = new OFigure(x, y, direction);
            }
        }
        
        public int Points
        {
            get { return points; }
            set { points = value; }
        }

        public void Draw()
        {
            figure.Draw();
        }

        public void UserEvent()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                while (Console.KeyAvailable) { Console.ReadKey(true); }

                if (key.Key == ConsoleKey.Z)
                {
                    ++figure.Direction;
                    if ((int)figure.Direction == 4)
                    {
                        figure.Direction = Direction.Down;
                    }
                }

                if (key.Key == ConsoleKey.LeftArrow)
                {
                    figure.X--;
                }

                if (key.Key == ConsoleKey.RightArrow)
                {
                    figure.X++;
                }
            }
        }
        
        public void Update(){
            figure.Update();
        }
        
        public void Collisions(){
            figure.Collisions();
        }
    }
}
