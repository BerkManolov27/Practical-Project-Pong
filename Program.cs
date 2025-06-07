using System;
using System.Threading;

class PongGame
{
    const int fieldWidth = 15;
    const int fieldHeight = 50;
    const int racketLength = fieldWidth / 4;
    const char wallChar = '#';
    const char racketChar = '|';
    const char ballChar = 'O';

    static int leftRacketY = fieldWidth / 2 - racketLength / 2;
    static int rightRacketY = fieldWidth / 2 - racketLength / 2;

    static int ballX = fieldHeight / 2;
    static int ballY = fieldWidth / 2;

    static int ballDirX = 1;
    static int ballDirY = 1;

    static int leftScore = 0;
    static int rightScore = 0;

    static Random random = new Random();

    static bool isAI = true;
    static int aiLevel = 2;
    static bool gameRunning = true;

    static void Main()
    {
        Console.CursorVisible = false;
        Console.Clear();

        ShowMenu();

        Console.Clear();
        DrawField();

        while (gameRunning)
        {
            DrawScore();
            DrawRackets();
            DrawBall();

            Thread.Sleep(60);

            HandlePlayerInput();
            if (!gameRunning) break;

            if (isAI)
                HandleAI();
            else
                HandleSecondPlayerInput();

            MoveBall();
            ClearField();

            if (leftScore == 10 || rightScore == 10)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(leftScore == 10 ? "🎉 Left player wins!" : isAI ? "🎉 Right player (AI) wins!" : "🎉 Right player wins!");
                Console.ResetColor();
                break;
            }
        }

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("\nGame over. Press any key to exit...");
        Console.ReadKey();
    }

    static void ShowMenu()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=== PONG GAME ===");
        Console.WriteLine("Select Game Mode:");
        Console.WriteLine("1. Play vs AI");
        Console.WriteLine("2. 2 Player Mode");
        Console.ResetColor();

        while (true)
        {
            ConsoleKey key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.D1 || key == ConsoleKey.NumPad1)
            {
                isAI = true;
                ChooseAIDifficulty();
                break;
            }
            else if (key == ConsoleKey.D2 || key == ConsoleKey.NumPad2)
            {
                isAI = false;
                break;
            }
        }
    }

    static void ChooseAIDifficulty()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Select AI Difficulty:");
        Console.WriteLine("1. Easy");
        Console.WriteLine("2. Medium");
        Console.WriteLine("3. Hard");
        Console.ResetColor();

        while (true)
        {
            ConsoleKey key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.D1 || key == ConsoleKey.NumPad1) { aiLevel = 1; break; }
            if (key == ConsoleKey.D2 || key == ConsoleKey.NumPad2) { aiLevel = 2; break; }
            if (key == ConsoleKey.D3 || key == ConsoleKey.NumPad3) { aiLevel = 3; break; }
        }
    }

    static void DrawField()
    {
        Console.ForegroundColor = ConsoleColor.White;
        string border = new string(wallChar, fieldHeight);
        Console.SetCursorPosition(0, 0);
        Console.WriteLine(border);
        Console.SetCursorPosition(0, fieldWidth);
        Console.WriteLine(border);
        Console.ResetColor();
    }

    static void DrawScore()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        string score = $"   {leftScore}  |  {rightScore}   (Press ESC to quit)";
        int x = Math.Max(0, (fieldHeight / 2 - score.Length / 2));
        Console.SetCursorPosition(x, fieldWidth + 1);
        Console.Write(score);
        Console.ResetColor();
    }

    static void DrawRackets()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        for (int i = 0; i < racketLength; i++)
        {
            Console.SetCursorPosition(0, leftRacketY + i);
            Console.Write(racketChar);
        }

        Console.ForegroundColor = ConsoleColor.Red;
        for (int i = 0; i < racketLength; i++)
        {
            Console.SetCursorPosition(fieldHeight - 1, rightRacketY + i);
            Console.Write(racketChar);
        }

        Console.ResetColor();
    }

    static void ClearRackets()
    {
        for (int i = 1; i < fieldWidth; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write(' ');
            Console.SetCursorPosition(fieldHeight - 1, i);
            Console.Write(' ');
        }
    }

    static void DrawBall()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.SetCursorPosition(ballX, ballY);
        Console.Write(ballChar);
        Console.ResetColor();
    }

    static void ClearBall()
    {
        Console.SetCursorPosition(ballX, ballY);
        Console.Write(' ');
    }

    static void HandlePlayerInput()
    {
        while (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.Escape)
            {
                gameRunning = false;
                return;
            }

            if (key == ConsoleKey.W && leftRacketY > 1)
                leftRacketY--;
            else if (key == ConsoleKey.S && leftRacketY + racketLength < fieldWidth)
                leftRacketY++;
        }
    }

    static void HandleSecondPlayerInput()
    {
        while (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.Escape)
            {
                gameRunning = false;
                return;
            }

            if (key == ConsoleKey.UpArrow && rightRacketY > 1)
                rightRacketY--;
            else if (key == ConsoleKey.DownArrow && rightRacketY + racketLength < fieldWidth)
                rightRacketY++;
        }
    }

    static void HandleAI()
    {
        int chance = aiLevel switch
        {
            1 => 3,
            2 => 2,
            3 => 1,
            _ => 2
        };

        if (random.Next(chance) == 0)
        {
            if (ballY < rightRacketY + racketLength / 2 && rightRacketY > 1)
                rightRacketY--;
            else if (ballY > rightRacketY + racketLength / 2 && rightRacketY + racketLength < fieldWidth)
                rightRacketY++;
        }
    }

    static void MoveBall()
    {
        ClearBall();
        ballX += ballDirX;
        ballY += ballDirY;

        if (ballY <= 1 || ballY >= fieldWidth - 1)
            ballDirY *= -1;

        if (ballX == 1)
        {
            if (ballY >= leftRacketY && ballY <= leftRacketY + racketLength - 1)
                ballDirX *= -1;
            else
                ScorePoint(playerRight: true);
        }

        if (ballX == fieldHeight - 2)
        {
            if (ballY >= rightRacketY && ballY <= rightRacketY + racketLength - 1)
                ballDirX *= -1;
            else
                ScorePoint(playerRight: false);
        }
    }

    static void ScorePoint(bool playerRight)
    {
        if (playerRight)
            rightScore++;
        else
            leftScore++;
        ResetBall();
    }

    static void ResetBall()
    {
        ClearBall();
        ballX = fieldHeight / 2;
        ballY = fieldWidth / 2;
        ballDirX = random.Next(0, 2) == 0 ? 1 : -1;
        ballDirY = random.Next(0, 2) == 0 ? 1 : -1;
    }

    static void ClearField()
    {
        ClearRackets();
    }
}
