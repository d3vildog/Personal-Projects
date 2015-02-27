using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testris
{
    public abstract class Figure
    {
        protected int x;
        protected int y;
        protected Direction direction;
        protected ConsoleColor color = ConsoleColor.Blue;

        public Figure(int x, int y, Direction direction)
        {
            this.X = x;
            this.Y = y;
            this.direction = direction;
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public Direction Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public abstract void Draw();
        
        public abstract void Update();
        
        public abstract void Collisions();
    }
}
