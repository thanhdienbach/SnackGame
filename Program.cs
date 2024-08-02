using System.Dynamic;
using System.Security.Cryptography.X509Certificates;

namespace Snack;

class Point
{
    public int X { get; set; }
    public int Y { get; set; }
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}

class Program
{
    static string _direction = "RIGHT";
    static int _speed = 500;
    static int rows = 20;
    static int cols = 41;
    static string[,] _screen = new string[rows, cols];
    // static int[] _head = new int[2] { 4, 5 };
    static Point _head = new Point(4, 5);
    static Point _food = new Point(-1, -1);
    static bool foodExist = false;
    static void Main(string[] args)
    {
        Thread _game = new Thread(LisenKey);
        _game.Start();
        do
        {
            Console.Clear();
            SetupScreen();
            DrawScreen();
            MoveHead();
            CreateFood();
            Task.Delay(_speed).Wait();
        }
        while (true);

    }
    static void SetupScreen()
    {
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (r == 0 || r == rows - 1 || c == 0 || c == cols - 1)
                {
                    _screen[r, c] = "#";
                }
                else if (r == _head.X && c == _head.Y)
                {
                    _screen[r, c] = "*";
                }
                else if (r == _food.X && c == _food.Y)
                {
                    _screen[r, c] = "@";
                }
                else
                {
                    _screen[r, c] = " ";
                }
            }
        }

    }
    static void DrawScreen()
    {
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                Console.Write(_screen[r, c]);
            }
            Console.WriteLine();
        }
    }
    static void MoveHead()
    {
        switch (_direction)
        {
            case "LEFT":
                _head.Y -= 1;
                if (_head.Y == 1)
                {
                    _head.Y = cols - 1;
                }
                break;
            case "RIGHT":
                _head.Y += 1;
                if (_head.Y == cols - 1)
                {
                    _head.Y = 1;
                }
                break;
            case "UP":
                _head.X -= 1;
                if (_head.X == 1)
                {
                    _head.X = rows - 1;
                }
                break;
            case "DOWN":
                _head.X += 1;
                if (_head.X == rows - 1)
                {
                    _head.X = 1;
                }
                break;
        }
        EatFood();
    }
    static void LisenKey()
    {
        while (true)
        {
            ConsoleKeyInfo _keyInfo = Console.ReadKey();
            switch (_keyInfo.Key)
            {
                case ConsoleKey.RightArrow:
                    if (_direction != "LEFT")
                    {
                        _direction = "RIGHT";
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (_direction != "RIGHT")
                    {
                        _direction = "LEFT";
                    }
                    break;
                case ConsoleKey.UpArrow:
                    if (_direction != "DOWN")
                    {
                        _direction = "UP";
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (_direction != "UP")
                    {
                        _direction = "DOWN";
                    }
                    break;
            }
            Console.WriteLine(_direction);
        }
    }
    static void CreateFood()
    {
        if (foodExist == false)
        {
            Random random = new Random();
            int foodX = random.Next(1, rows - 1);
            int foodY = random.Next(1, cols - 1);
            _food = new Point(foodX, foodY);
            foodExist = true;
        }
    }
    static void EatFood()
    {
        if(_head.X == _food.X && _head.Y == _food.Y)
        {
            foodExist = false;
        }
    }
}
