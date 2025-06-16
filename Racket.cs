public class Racket
{
    private int _x;
    private int _y;
    private int _length;
    private char _tile;
    private int _initialX;

    public Racket(int x, int y, int length, char tile)
    {
        _x = x;
        _initialX = x;
        _y = y;
        _length = length;
        _tile = tile;
    }

    public void Draw(Field field)
    {
        for (int i = 0; i < _length; i++)
            field.SetChar(_x + i, _y, _tile);
    }

    public void MoveUp(Field field)
    {
        if (field.GetChar(_x - 1, _y) == ' ')
        {
            field.SetChar(_x + _length - 1, _y, ' ');
            _x--;
            field.SetChar(_x, _y, _tile);
        }
    }

    public void MoveDown(Field field)
    {
        if (field.GetChar(_x + _length, _y) == ' ')
        {
            field.SetChar(_x, _y, ' ');
            _x++;
            field.SetChar(_x + _length - 1, _y, _tile);
        }
    }

    public void Reset(Field field)
    {
        for (int i = 0; i < _length; i++)
            field.SetChar(_x + i, _y, ' ');
        _x = _initialX;
        Draw(field);
    }

    public int X => _x;
    public int Y => _y;
    public int Length => _length;
}
