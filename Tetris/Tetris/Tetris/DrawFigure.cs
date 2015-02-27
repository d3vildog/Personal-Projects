using System;

namespace Testris
{
    public static class DrawFigure
    {
        public static void DrawSymbolAtPosition(int x, int y, string symbol, ConsoleColor color)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(symbol);
        }
    }
}