using System;
using System.Collections.Generic;
using System.Threading;

public class main
{    
    public static bool[,] Field;
    public static bool NewElement = false;
    public static List<TShape> list = new List<TShape>();
    
    public static void Main()
    {
        Console.CursorVisible = false;
        Console.SetWindowSize(30,50);
        Console.SetBufferSize(30,50);
        TShape tshape = new TShape(Console.WindowWidth / 2 - 2, 2, Directions.Up);
        Player player = new Player(tshape);
        Field = new bool[Console.WindowWidth, Console.WindowHeight];
        
        while (true) {
            Console.Clear();
            player.Engine();
            Thread.Sleep(100);
        }
    }
}