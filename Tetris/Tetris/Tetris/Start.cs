using System;
using System.Threading;
using Testris;

class Start
{
    public static bool[,] field;
    public static Player player;
    public static bool NewFigure = true;
    
    public static int WindowWidth = 35;
    public static int WindowHeight = 30;
    public static int ScoreField = (int)(0.40*WindowWidth);

    private static void Main()
    {
        Inicialization();

        while (true)
        {
            if (NewFigure == true)
            {
                InicializeNewFigure();
            }
            
            Update();
            Draw();
            Thread.Sleep(100);
            Console.Clear();
        }
    }
    
    static void Update()
    {
        CheckForFilledRow();
        player.UserEvent();
        player.Update();
        player.Collisions();
    }
    
    static void Draw()
    {
        player.Draw();
        DrawField();
    }

    private static void DrawField()
    {
        for (int row = 0; row < field.GetLength(0); row++)
        {
            for (int col = 0; col < field.GetLength(1); col++)
            {
                if (field[row, col] == true)
                {
                    DrawFigure.DrawSymbolAtPosition(row, col, "#", ConsoleColor.Red);
                }
            }
        }
    }

    private static void InicializeNewFigure()
    {
        player = new Player(WindowWidth / 2, 2, Direction.Down);
        NewFigure = false;
    }

    private static void Inicialization()
    {
        Console.WindowWidth = WindowWidth;
        Console.WindowHeight = WindowHeight;
        Console.BufferWidth = WindowWidth;
        Console.BufferHeight = WindowHeight;

        player = new Player(WindowWidth / 2, 2, Direction.Down);
        field = new bool[WindowWidth - 1, WindowHeight - 1];
    }
    
    private static void CheckForFilledRow(){
        bool filledRow = false;
        for (int row = 0; row < field.GetLength(1); row++) {
            for (int col = 1; col < field.GetLength(0) - ScoreField + 1; col++) {
                if (field[col, row] == true) {
                    filledRow = true;
                }
                else {
                    filledRow = false;
                    break;
                }
            }
            
            if (filledRow == true) {      
                for (int col = 1; col < field.GetLength(0) - ScoreField + 1; col++) {
                    field[col, row] = false;
                }
                
                filledRow = false;        
                
                for (int innRow = row; innRow >= 5; innRow--) {
                    for (int col = 1; col < field.GetLength(0) - ScoreField + 1; col++) {
                        field[col, innRow] = field[col, innRow - 1];
                        field[col, innRow - 1] = false;
                    }
                }
            }
        }
    }
}