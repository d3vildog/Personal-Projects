using System.Linq;
using JustSnake;
using System;
using System.Collections.Generic;

public class Snake
{
    public static Queue<Point> SnakeElements = new Queue<Point>();

    private static void Main()
    {
        SetUpGameField();

        while (true)
        {
            Update();
            Draw();
        }

    }

    private static void Draw()
    {
        foreach (var snakeElement in SnakeElements)
        {
            DrawingTheSnake(snakeElement);
        }
    }

    private static void DrawingTheSnake(Point snakeElement)
    {
        Console.SetCursorPosition(snakeElement.X, snakeElement.Y);
        Console.ForegroundColor = snakeElement.Color;
        Console.Write("#");
    }

    private static void Update()
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            while (Console.KeyAvailable){Console.ReadKey(true);}

            if (key.Key == ConsoleKey.LeftArrow)
            {
                
            }
        }
    }

    private static void SetUpGameField()
    {
        Console.SetWindowSize(60, 40);
        Console.SetBufferSize(60, 40);
        Console.CursorVisible = false;

        for (int i = 0; i < 6; i++)
        {
            SnakeElements.Enqueue(new Point(i, 0, ConsoleColor.Green));
        }
    }
}