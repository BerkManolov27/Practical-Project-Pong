using System;
using System.Net.Sockets;
using System.Threading;

class Program
{
    static void Main()
    {
        const int width = 51;
        const int height = 15;
        Console.CursorVisible = false;
        Console.SetWindowSize(width, height + 2);

        Field field = new Field(width, height, '-');
        Racket left = new Racket(6, 2, 3, '|');
        Racket right = new Racket(6, width - 2, 3, '|');
        Ball ball = new Ball(height / 2, width / 2, 'O');

        int leftScore = 0, rightScore = 0;
        int frame = 0;

        while (true)
        {
            ReadInput(field, left, right);

            if (frame % 2 == 0)
                ball.CalculateTrajectory(field, left, right);

            int goal = ball.CheckForGoal(width);
            if (goal != -1)
            {
                if (goal == 0) leftScore++;
                else rightScore++;

                ball.Reset(field);
                left.Reset(field);
                right.Reset(field);
                Thread.Sleep(1000);
            }

            left.Draw(field);
            right.Draw(field);
            ball.Draw(field);

            field.Draw();
            DrawScore(leftScore, rightScore, height);

            Thread.Sleep(10);
            frame++;
        }
    }

    static void ReadInput(Field field, Racket left, Racket right)
    {
        if (!Console.KeyAvailable) return;

        var key = Console.ReadKey(true).Key;
        switch (key)
        {
            case ConsoleKey.W:
                left.MoveUp(field);
                break;
            case ConsoleKey.S:
                left.MoveDown(field);
                break;
            case ConsoleKey.UpArrow:
                right.MoveUp(field);
                break;
            case ConsoleKey.DownArrow:
                right.MoveDown(field);
                break;
        }
    }

    static void DrawScore(int left, int right, int height)
    {
        Console.SetCursorPosition(0, height);
        Console.WriteLine($"Left: {left}  |  Right: {right}");
    }
}
