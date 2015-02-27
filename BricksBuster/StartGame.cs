namespace BricksBuster
{
    using System;
    using System.Threading;
    using System.Collections.Generic;
    
	class StartGame
	{
		public const int WindowWidthCollumn = 80;
		public const int WindowHeightRow = 55;
		public static int[,] bricksField = new int[WindowHeightRow, WindowWidthCollumn];
		
		public static StickandBall Player = new StickandBall();
		
		public static StickandBall Ball = new StickandBall();
		public static int ballSpeed = 0;
		public static int bonusesSpeed = 0;
		
		public static int playerLives = 3;
		public static int playerScore = 0;
		
		public static List<Bonus> scoreBonuses = new List<Bonus>();
		public static List<Bonus> lifeBonuses = new List<Bonus>();
		public static List<Bonus> shootBonuses = new List<Bonus>();
		
		public static Weapon weapon = new Weapon();
		public static bool weaponActive = false;
		public static List<Shots> shots = new List<Shots>();
		
		public static Random randomNumberGenerator = new Random();
	    public static int timer = 0;
		
		public static void Main()
		{
		    Console.ReadLine();
			SetGameField();
	
			while (true)
			{
			    if (weaponActive)
			    {
                    timer++;
			    }
			    bonusesSpeed++;
			    ballSpeed++;
			    Update();
			    Draw();
			    
			    Thread.Sleep(20);
			}
		}

        static void Draw()
        {
            DrawStick();
            DrawBall();      
            DrawInfo();
            DrawBonuses();
            DrawWeapon();
            DrawShots();
        }
        
        static void DrawShots()
        {
            foreach (var shot in shots) {
                SetCursorPositionAndDraw(shot.leftX, shot.rightX, shot.Y, shot.Symbol, ConsoleColor.Red);
            }
        }
        
        static void DrawWeapon()
        {
            if (weaponActive)
            {
                SetCursorPositionAndDraw(weapon.leftX, weapon.rightX, weapon.Y, weapon.Symbol, ConsoleColor.Red);
            }
        }
        
        static void DrawBonuses()
        {
            foreach (var bonus in scoreBonuses) { 
                SetCursorPositionAndDraw(bonus.Y, bonus.X, "$", bonus.Color);
            }
           
            foreach (var bonus in lifeBonuses) {
                SetCursorPositionAndDraw(bonus.Y, bonus.X, "♥", bonus.Color);
            }
            
            foreach (var bonus in shootBonuses) {
                SetCursorPositionAndDraw(bonus.Y, bonus.X, "W", bonus.Color);
            }
        }
        
        static void DrawInfo()
        {
            SetCursorPositionAndDraw(0, 0, "Lives: " + playerLives + "♥", ConsoleColor.Yellow);
            SetCursorPositionAndDraw(0, WindowWidthCollumn - 12, "Score: " + playerScore+ "$", ConsoleColor.Yellow);
        }
        
        static void DrawBricks()
        {	    
            Console.ForegroundColor = ConsoleColor.Gray;
		    for (int row = 0; row < bricksField.GetLength(0); row++) {
		        for (int col = 0; col < bricksField.GetLength(1); col++) {
                    if (bricksField[row, col] == 1) {
                        SetCursorPositionAndDraw(row, col, "=");
		            }
		        }
		    }
        }
        
        static void DrawBall()
        {
            SetCursorPositionAndDraw(Ball.Y, Ball.X, Ball.Symbol, Ball.Color);
        }
        
        static void DrawStick()
        {
            for (int i = Player.X; i < Player.X + Player.Length; i++) {
                if ((i == Player.X || i == Player.X + Player.Length - 1) && weaponActive)
                {
                    Player.Color = ConsoleColor.Red;
                }
                else
                {
                    Player.Color = ConsoleColor.Green;
                }
                SetCursorPositionAndDraw(Player.Y, i, Player.Symbol, Player.Color);
            }
        }
		
        static void Update()
        {
            BallCollisionWithBricks();
            BallCollisionWithWall();
            BallCollisionWithStick();
            
            if (ballSpeed % 2 == 0) {
                ballSpeed = 0;
                UpdateBall();
            }   
            UpdateStick();
            
            if (bonusesSpeed % 7 == 0) {
                bonusesSpeed = 0;
                UpdateBonuses();
            }

            UpdateWeapon();
            UpdateShots();
        }
        
        static void UpdateShots()
        {
            ShotsCollisionWithBricks();

            for (int i = 0; i < shots.Count; i++) {
                SetCursorPositionAndDraw(shots[i].leftX, shots[i].rightX, shots[i].Y, " ", shots[i].Color);
                shots[i].Y--;
                SetCursorPositionAndDraw(shots[i].leftX, shots[i].rightX, shots[i].Y, shots[i].Symbol, shots[i].Color);
                
                if (shots[i].Y <= 1) {
                    SetCursorPositionAndDraw(shots[i].leftX, shots[i].rightX, shots[i].Y, " ", shots[i].Color);
                    shots.Remove(shots[i]);
                }
            }
        }
        
        static void UpdateWeapon()
        {
            if (timer == 500)
            {
                timer = 0;
                weaponActive = false;
                SetCursorPositionAndDraw(weapon.leftX, weapon.rightX, weapon.Y, " ", ConsoleColor.Red);
            }
            if (weaponActive == true) {
                SetCursorPositionAndDraw(weapon.leftX, weapon.rightX, weapon.Y, " ", ConsoleColor.Red);
                weapon.leftX = Player.X;
                weapon.rightX = Player.X + Player.Length - 1;
            }
        }
        
        static void UpdateBonuses()
        {
            for (int i = 0; i < scoreBonuses.Count; i++) {
                SetCursorPositionAndDraw(scoreBonuses[i].Y, scoreBonuses[i].X, " ");
                scoreBonuses[i].Y++;
                
                if (scoreBonuses[i].X >= Player.X &&
                    scoreBonuses[i].X <= Player.X + Player.Length &&
                    scoreBonuses[i].Y == Player.Y) {
                    playerScore+= 400;
                    scoreBonuses.Remove(scoreBonuses[i]);
                }else if (scoreBonuses[i].Y >= WindowHeightRow - 1) {
                    scoreBonuses.Remove(scoreBonuses[i]);
                }
            }
            
            for (int i = 0; i < lifeBonuses.Count; i++) {
                SetCursorPositionAndDraw(lifeBonuses[i].Y, lifeBonuses[i].X, " ");
                lifeBonuses[i].Y++;
                
                if (lifeBonuses[i].X >= Player.X &&
                    lifeBonuses[i].X <= Player.X + Player.Length &&
                    lifeBonuses[i].Y == Player.Y) {
                    playerLives++;
                    lifeBonuses.Remove(lifeBonuses[i]);
                }else if (lifeBonuses[i].Y >= WindowHeightRow - 1) {
                    lifeBonuses.Remove(lifeBonuses[i]);
                }
            }
            
            for (int i = 0; i < shootBonuses.Count; i++) {
                SetCursorPositionAndDraw(shootBonuses[i].Y, shootBonuses[i].X, " ");
                shootBonuses[i].Y++;
                
                if (shootBonuses[i].X >= Player.X &&
                    shootBonuses[i].X <= Player.X + Player.Length &&
                    shootBonuses[i].Y == Player.Y) {
                    weapon = new Weapon(Player.X, Player.X + Player.Length - 1, Player.Y - 1, ConsoleColor.Red, "^");
                    shootBonuses.Remove(shootBonuses[i]);
                    weaponActive = true;
                }else if (shootBonuses[i].Y >= WindowHeightRow - 1) {
                    shootBonuses.Remove(shootBonuses[i]);
                }
            }
        }
        
        static void BallCollisionWithBricks()
        {
            int rand = randomNumberGenerator.Next(1, 101);

            if (Ball.Y - 1 > 0 && bricksField[Ball.Y - 1, Ball.X] == 1) {
                SetCursorPositionAndDraw(Ball.Y - 1, Ball.X, " ");
                bricksField[Ball.Y - 1, Ball.X] = 0;   
                Ball.DirectionUp = false;
                playerScore += 9;
                
                if (rand < 10) {
                    lifeBonuses.Add(new Bonus(Ball.X, Ball.Y + 1, ConsoleColor.Red));
                    DrawBricks();
                }
                else if (rand < 30) {
                    scoreBonuses.Add(new Bonus(Ball.X, Ball.Y + 1, ConsoleColor.Blue));
                    DrawBricks();
                }
                else if (rand < 50) {
                    shootBonuses.Add(new Bonus(Ball.X, Ball.Y + 1, ConsoleColor.Blue));
                    DrawBricks();
                }
            }
            
            if (Ball.X - 1 > 0 && bricksField[Ball.Y, Ball.X - 1] == 1) {
                SetCursorPositionAndDraw(Ball.Y, Ball.X - 1, " ");
                bricksField[Ball.Y, Ball.X - 1] = 0;   
                Ball.DirectionLeft = false;
                playerScore += 9;
                 
                if (rand < 10) {
                    lifeBonuses.Add(new Bonus(Ball.X, Ball.Y + 1, ConsoleColor.Red));
                    DrawBricks();
                }
                else if (rand < 30) {
                    scoreBonuses.Add(new Bonus(Ball.X, Ball.Y + 1, ConsoleColor.Blue));
                    DrawBricks();
                }
                else if (rand < 50) {
                    shootBonuses.Add(new Bonus(Ball.X, Ball.Y + 1, ConsoleColor.Blue));
                    DrawBricks();
                }
            }
            
            if (Ball.Y + 1 < WindowHeightRow - 1 && bricksField[Ball.Y + 1, Ball.X] == 1) {
                SetCursorPositionAndDraw(Ball.Y + 1, Ball.X, " ");
                bricksField[Ball.Y + 1, Ball.X] = 0;
                Ball.DirectionUp = true;
                playerScore += 9;
               
                if (rand < 10) {
                    lifeBonuses.Add(new Bonus(Ball.X, Ball.Y + 1, ConsoleColor.Red));
                    DrawBricks();
                }
                else if (rand < 30) {
                    scoreBonuses.Add(new Bonus(Ball.X, Ball.Y + 1, ConsoleColor.Blue));
                    DrawBricks();
                }
                else if (rand < 50) {
                    shootBonuses.Add(new Bonus(Ball.X, Ball.Y + 1, ConsoleColor.Blue));
                    DrawBricks();
                }
            }
            
            if (Ball.X + 1 < WindowWidthCollumn - 1 && bricksField[Ball.Y, Ball.X + 1] == 1) {
                SetCursorPositionAndDraw(Ball.Y, Ball.X + 1, " ");
                bricksField[Ball.Y, Ball.X + 1] = 0;   
                Ball.DirectionLeft = true;
                playerScore += 9;
                
                if (rand < 10) {
                    lifeBonuses.Add(new Bonus(Ball.X, Ball.Y + 1, ConsoleColor.Red));
                    DrawBricks();
                }
                else if (rand < 30) {
                    scoreBonuses.Add(new Bonus(Ball.X, Ball.Y + 1, ConsoleColor.Blue));
                    DrawBricks();
                }
                else if (rand < 50) {
                    shootBonuses.Add(new Bonus(Ball.X, Ball.Y + 1, ConsoleColor.Blue));
                    DrawBricks();
                }
            }
        }
        
        static void BallCollisionWithStick()
        {
            if (Ball.X >= Player.X &&
                Ball.X <= Player.X + Player.Length &&                 
               Ball.Y + 1 == Player.Y) {
                Ball.DirectionUp = true;
            }
        }
        
        static void UpdateBall()
        {
            if (Ball.DirectionUp) {
                SetCursorPositionAndDraw(Ball.Y, Ball.X, " ");
                Ball.Y--; 
            }else{
                SetCursorPositionAndDraw(Ball.Y, Ball.X, " ");
                Ball.Y++;
            }
            
            if (Ball.DirectionLeft) {
                SetCursorPositionAndDraw(Ball.Y, Ball.X, " ");
                Ball.X--;
            }else {
                SetCursorPositionAndDraw(Ball.Y, Ball.X, " ");
                Ball.X++;
            }
        }

        private static void ShotsCollisionWithBricks()
        {
            for (int i = 0; i < shots.Count; i++)
            {
                if (bricksField[shots[i].Y - 1, shots[i].leftX] == 1)
                {
                    bricksField[shots[i].Y - 1, shots[i].leftX] = 0;
                    SetCursorPositionAndDraw(shots[i].leftX, shots[i].rightX, shots[i].Y, " ", ConsoleColor.White);
                    SetCursorPositionAndDraw(shots[i].leftX, shots[i].rightX, shots[i].Y - 1, " ", ConsoleColor.White);
                }

                if (bricksField[shots[i].Y - 1, shots[i].rightX ] == 1)
                {
                    bricksField[shots[i].Y - 1, shots[i].rightX] = 0;
                    SetCursorPositionAndDraw(shots[i].leftX, shots[i].rightX, shots[i].Y - 1, " ", ConsoleColor.White);
                    SetCursorPositionAndDraw(shots[i].leftX, shots[i].rightX, shots[i].Y, " ", ConsoleColor.White);
                    shots.Remove(shots[i]);
                }
            }
        }
        
        static void BallCollisionWithWall()
        {
            if (Ball.Y < 3) {
                Ball.DirectionUp = false;;
            }
            
            if (Ball.Y > WindowHeightRow - 2) {
                playerLives--;
                lifeBonuses.Clear();
                shootBonuses.Clear();
                scoreBonuses.Clear();
                if (playerLives < 0)
                {
                    SetCursorPositionAndDraw(Console.WindowWidth / 2 - 3, Console.WindowHeight / 2, "GAME OVER!");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                InitialiseBallStartPoint();
                Console.Clear();
                Draw();
                DrawBricks();                
                Console.ReadLine();
            }
            
            if (Ball.X < 1) {
                Ball.DirectionLeft = false;
            }
            
            if (Ball.X > WindowWidthCollumn - 3) {
                Ball.DirectionLeft = true;
            }
        }
        
        static void UpdateStick()
        {
            
            if (Console.KeyAvailable) 
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                while(Console.KeyAvailable) Console.ReadKey(true);
                
                if (key.Key == ConsoleKey.Spacebar && weaponActive == true) {
                    shots.Add(new Shots(weapon.leftX, weapon.rightX, weapon.Y - 1, ConsoleColor.Red, "|"));
                }
                
                if (key.Key == ConsoleKey.LeftArrow && Player.X - 3 > 0 ) {
                    SetCursorPositionAndDraw(Player.Y, Player.X + Player.Length - 1, " ");
                    SetCursorPositionAndDraw(Player.Y, Player.X + Player.Length - 2, " ");
                    SetCursorPositionAndDraw(Player.Y, Player.X + Player.Length - 3, " ");
                    Player.X-=3;
                }else if (key.Key == ConsoleKey.LeftArrow && Player.X - 2 > 0 ) {
                    SetCursorPositionAndDraw(Player.Y, Player.X + Player.Length - 1, " ");
                    SetCursorPositionAndDraw(Player.Y, Player.X + Player.Length - 2, " ");
                    Player.X-=2;
                }else if (key.Key == ConsoleKey.LeftArrow && Player.X - 1 > 0 ) {
                    SetCursorPositionAndDraw(Player.Y, Player.X + Player.Length - 1, " "); 
                    Player.X-=1;
                }
                
                if (key.Key == ConsoleKey.RightArrow && Player.X + Player.Length + 3 < WindowWidthCollumn) {
                    SetCursorPositionAndDraw(Player.Y, Player.X, " ");
                    SetCursorPositionAndDraw(Player.Y, Player.X + 1, " ");
                    SetCursorPositionAndDraw(Player.Y, Player.X + 2, " ");
                    if (Player.X > 1) {
                        SetCursorPositionAndDraw(Player.Y, Player.X + 3, " ");
                    }
                    Player.X+=3;
                }else if (key.Key == ConsoleKey.RightArrow && Player.X + Player.Length + 2 < WindowWidthCollumn) {
                    SetCursorPositionAndDraw(Player.Y, Player.X, " ");
                    SetCursorPositionAndDraw(Player.Y, Player.X + 1, " ");
                    SetCursorPositionAndDraw(Player.Y, Player.X + 2, " ");
                    Player.X+=2;
                }else if (key.Key == ConsoleKey.RightArrow && Player.X + Player.Length + 1 < WindowWidthCollumn) {
                    SetCursorPositionAndDraw(Player.Y, Player.X, " ");
                    SetCursorPositionAndDraw(Player.Y, Player.X + 1, " ");
                    SetCursorPositionAndDraw(Player.Y, Player.X + 2, " ");
                    SetCursorPositionAndDraw(Player.Y, Player.X + 3, " ");
                    Player.X+=1;
                }
            }
        }
		
		static void SetGameField()
		{
		    // Player initial point
		    Player.Length = 11;
            Player.X = WindowWidthCollumn / 2 - Player.Length / 2;
            Player.Y = WindowHeightRow - 2;
            Player.Color = ConsoleColor.Green;
            Player.Symbol ="#";
            
            // Console settings 
			Console.WindowWidth = WindowWidthCollumn;
			Console.WindowHeight = WindowHeightRow;
			Console.BufferWidth = WindowWidthCollumn;
			Console.BufferHeight = WindowHeightRow;
			Console.CursorVisible = false;
			Console.Title = "Brick Buster";		

            // Initialise the field of bricks
            InitialiseTheFieldOfBricks();
            InitialiseBallStartPoint();
            DrawBricks();
		}
		
		static void InitialiseBallStartPoint()
		{
		    // Ball initial point
		    int ballUp = randomNumberGenerator.Next(0,2);
		    int ballDown = randomNumberGenerator.Next(0,2);		    
            Ball.X = WindowWidthCollumn / 2;
            Ball.Y = WindowHeightRow / 2;
            Ball.Length = 1;
            Ball.Color = ConsoleColor.Yellow;
            Ball.Symbol = "@";            
            Ball.DirectionUp = ballUp == 1 ? true : false; 
            Ball.DirectionLeft = ballDown == 1 ? true : false; 
		}
		
		public static void InitialiseTheFieldOfBricks()
		{		    
		    for (int row = 0; row < bricksField.GetLength(0); row++) {
		        for (int col = 0; col < bricksField.GetLength(1); col++) {
		            bricksField[row, col] = 0;
		        }
		    }
		    
		    for (int row = 5; row < 20; row++) {
		        for (int col = 10; col < bricksField.GetLength(1) - 10; col++) {
		            bricksField[row, col] = 1;
		        }
		    }
		}
		
		public static void SetCursorPositionAndDraw(int leftX,int rightX, int y, string str, ConsoleColor color)
		{
		    Console.SetCursorPosition(leftX, y);
		    Console.ForegroundColor = color;
		    Console.Write(str);
		    
		    Console.SetCursorPosition(rightX, y);
		    Console.ForegroundColor = color;
		    Console.Write(str);
		}
		
		public static void SetCursorPositionAndDraw(int row, int col, string str, ConsoleColor color)
		{
		    Console.SetCursorPosition(col, row);
		    Console.ForegroundColor = color;
		    Console.Write(str);
		}
		
		public static void SetCursorPositionAndDraw(int row, int col, string str)
		{
		    Console.SetCursorPosition(col, row);
		    Console.Write(str);
		}
	}
}