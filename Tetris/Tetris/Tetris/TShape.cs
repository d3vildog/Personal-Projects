using System;
public class TShape
{
    public int x;
    public int y;
    public Directions direction;
    public ConsoleColor color = ConsoleColor.Blue;

    public TShape(int x, int y, Directions direction)
    {
        this.x = x;
        this.y = y;
        this.direction = direction;
    }
    
    public void Draw(){
        if (direction == Directions.Up) {  
            DrawAtPosition(x, y - 1, "o", color);
                
            DrawAtPosition(x - 1, y, "o", color); 
            
            DrawAtPosition(x, y, "o", color);             
             
            DrawAtPosition(x + 1, y, "o", color);    
        }
        
        if (direction == Directions.Down) {
            DrawAtPosition(x - 1, y, "o", color);    
            
            DrawAtPosition(x, y, "o", color);                
            
            DrawAtPosition(x + 1, y, "o", color);
            
            DrawAtPosition(x, y + 1, "o", color);    
        }
        
        if (direction == Directions.Left) {
            DrawAtPosition(x, y - 1, "o", color);     
   
            DrawAtPosition(x - 1, y, "o", color);    
            
            DrawAtPosition(x, y, "o", color);
            
            DrawAtPosition(x, y + 1, "o", color);               
        }
        
        if (direction == Directions.Right) {
            DrawAtPosition(x, y - 1, "o", color);   
            
            DrawAtPosition(x, y, "o", color);
            
            DrawAtPosition(x + 1, y, "o", color);     
             
            DrawAtPosition(x, y + 1, "o", color);     
        }
    }
    
    public void Update(){
        this.y++;    
       if (direction == Directions.Up && this.y >= Console.WindowHeight - 1) {
            this.y = Console.WindowHeight - 1;
            FillTheField();
            main.NewElement = true;
        }
        else if(this.y >= Console.WindowHeight - 2){
            this.y = Console.WindowHeight - 2;
            FillTheField();
            main.NewElement = true;
        }    
    }
        
    public int Length { get; private set;}
    
    public static void DrawAtPosition(int x, int y, string element, ConsoleColor color){
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(element);
    }
    
   public void FillTheField(){
        if (direction == Directions.Up) {
            main.Field[x, y - 1] = true;
            main.Field[x - 1, y] = true;
            main.Field[x, y] = true;
            main.Field[x + 1, y] = true;
        }        
        else if (direction == Directions.Right) {main.Field[x, y - 1] = true;
            main.Field[x, y - 1] = true;
            main.Field[x, y] = true;
            main.Field[x + 1, y] = true;   
            main.Field[x, y + 1] = true;            
        }
        else if (direction == Directions.Down) {
            main.Field[x - 1, y] = true;
            main.Field[x, y] = true;
            main.Field[x + 1, y] = true;   
            main.Field[x, y + 1] = true; 
        }
        else if (direction == Directions.Left) {
            main.Field[x, y - 1] = true;
            main.Field[x - 1, y] = true;
            main.Field[x, y] = true;   
            main.Field[x, y + 1] = true; 
        }
    }
}