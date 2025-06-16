public class Ball
{
    private int _x, _y;
    private int _initialX, _initialY;
    private char _tile;
    private bool _isGoingDown = true;
    private bool _isGoingRight = true;

    public Ball(int x, int y, char tile)
    {
        _x = _initialX = x;
        _y = _initialY = y;
        _tile = tile;
    }

    public void Draw(Field field)
    {
        field.SetChar(_x, _y, _tile);
    }

    public void CalculateTrajectory(Field field, Racket left, Racket right)
    {
        field.SetChar(_x, _y, ' ');


        if (_isGoingDown && field.GetChar(_x + 1, _y) != ' ')
            _isGoingDown = false;
        else if (!_isGoingDown && field.GetChar(_x - 1, _y) != ' ')
            _isGoingDown = true;


        if (_y == left.Y + 1 && _x >= left.X && _x < left.X + left.Length)
            _isGoingRight = true;
        else if (_y == right.Y - 1 && _x >= right.X && _x < right.X + right.Length)
            _isGoingRight = false;

        _x += _isGoingDown ? 1 : -1;
        _y += _isGoingRight ? 1 : -1;

        field.SetChar(_x, _y, _tile);
    }

    public int CheckForGoal(int width)
    {
        if (_y <= 0) return 1;
        if (_y >= width - 1) return 0;
        return -1;
    }

    public void Reset(Field field)
    {
        field.SetChar(_x, _y, ' ');
        _x = _initialX;
        _y = _initialY;
        Draw(field);
    }
}
