using System;
class Tetris
{
    public static void Main(){
        IFigure ifigure = new IFigure(0, 0, ConsoleColor.Red);
        Console.ReadLine();
        
        while (true) {
            ifigure.Draw();
            ifigure.Update();
            Console.Clear();
        }
    }
}