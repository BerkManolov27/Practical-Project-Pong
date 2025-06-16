public class Field
{
    private char[,] _field;
    private char _tile;

    public int Width { get; }
    public int Height { get; }

    public Field(int width, int height, char tile)
    {
        Width = width;
        Height = height;
        _tile = tile;
        _field = new char[height, width];

        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                _field[y, x] = ' ';

        for (int x = 0; x < width; x++)
        {
            _field[0, x] = tile;
            _field[height - 1, x] = tile;
        }
    }

    public char GetChar(int y, int x) => _field[y, x];
    public void SetChar(int y, int x, char c) => _field[y, x] = c;

    public void Draw()
    {
        Console.SetCursorPosition(0, 0);
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
                Console.Write(_field[y, x]);
            Console.WriteLine();
        }
    }
}
