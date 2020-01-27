using System;
using SplashKitSDK;
public abstract class Robot
{
    protected Window _gameWindow;
    protected double _x { get; set; }
    protected double _y { get; set; }

    protected Color _mainColour;
    protected Vector2D _velocity;

    public int Width
    {
        get
        {
            return 50;
        }
    }
    public int Height
    {
        get
        {
            return 50;
        }
    }

    public Circle CollisionCircle
    {
        get
        {
            return SplashKit.CircleAt(_x, _y, 20);
        }
    }

    public double GetMyPositionX()
    {
        return this._x;
    }

    public double GetMyPositionY()
    {
        return this._y;
    }

    /*-----Methods------*/

    public Robot(Window gameWindow, Player player)
    {
        _x = 0;
        _y = 0;
        _gameWindow = gameWindow;

        if (SplashKit.Rnd() < 0.5)
        {
            _x = SplashKit.Rnd(gameWindow.Width);

            if (SplashKit.Rnd() < 0.5)
            {
                _y = -Height;
            }
            else
            {
                _y = gameWindow.Height;
            }
        }
        else
        {
            _y = SplashKit.Rnd(gameWindow.Height);

            if (SplashKit.Rnd() < 0.5)
            {
                _x = -Width;
            }
            else
            {
                _x = gameWindow.Width;
            }
        }
        _mainColour = Color.RandomRGB(200);

        const int SPEED = 1;
        // Get a Point for the Robot
        Point2D fromPt = new Point2D()
        {
            X = _x,
            Y = _y
        };
        // Get a Point for the Player
        Point2D toPt = new Point2D()
        {
            X = player.X,
            Y = player.Y
        };
        // Calculate the direction to head.
        Vector2D dir;
        dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, toPt));
        // Set the speed and assign to the Velocity
        _velocity = SplashKit.VectorMultiply(dir, SPEED);
    }

    public abstract void Draw();

    public void Update()
    {
        _x += _velocity.X;
        _y += _velocity.Y;
    }
    public bool IsOffScreen(Window screen)
    {
        if (_x < -Width || _x > screen.Width || _y < -Height || _y > screen.Height)
        {
            return true;
        }
        return false;
    }
}

public class Boxy : Robot
{
    // {
        Window _gameWindow;
        private double _x { get; set; }
        private double _y { get; set; }

        private Color _mainColour;
        private Vector2D _velocity;

    public Boxy(Window gameWindow, Player player) : base(gameWindow, player)
    {


    }
    public override void Draw()
    {
        double leftX, rightX;
        double eyeY, mouthY;
        leftX = _x + 12;
        rightX = _x + 27;
        eyeY = _y + 10;
        mouthY = _y + 30;

        _gameWindow.FillRectangle(Color.Gray, _x, _y, 50, 50);
        _gameWindow.FillRectangle(_mainColour, leftX, eyeY, 10, 10);
        _gameWindow.FillRectangle(_mainColour, rightX, eyeY, 10, 10);
        _gameWindow.FillRectangle(_mainColour, leftX, mouthY, 25, 10);
        _gameWindow.FillRectangle(_mainColour, leftX + 2, mouthY + 2, 21, 6);
    }
}
public class Roundy : Robot
{
    // Window _gameWindow;
    public Roundy(Window gameWindow, Player player) : base(gameWindow, player)
    {

    }
    public override void Draw()
    {
        double leftX, midX, rightX;
        double midY, eyeY, mouthY;
        leftX = _x + 17;
        midX = _x + 25;
        rightX = _x + 33;
        midY = _y + 25;
        eyeY = _y + 20;
        mouthY = _y + 35;
        SplashKit.FillCircle(Color.White, midX, midY, 25);
        SplashKit.DrawCircle(Color.Gray, midX, midY, 25);
        SplashKit.FillCircle(_mainColour, leftX, eyeY, 5);
        SplashKit.FillCircle(_mainColour, rightX, eyeY, 5);
        SplashKit.FillEllipse(Color.Gray, _x, eyeY, 50, 30);
        SplashKit.DrawLine(Color.Black, _x, mouthY, _x + 50, _y + 35);
    }
}
