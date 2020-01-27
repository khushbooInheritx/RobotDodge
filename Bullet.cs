using SplashKitSDK;

public class Bullet
{

    public double X { get; set; }
    public double Y { get; set; }

    public double TargetX { get; set; }
    public double TargetY { get; set; }

    public double Speed { get; set; }

    private Bitmap _bulletBitMap;
    private Window _gameWindow;

    public bool MissTheTarget { get; set; }
    public Circle CollisionCircle
    {
        get
        {
            return SplashKit.CircleAt(X, Y, 20);
        }
    }

    public bool CollideWithOtherRobot(Robot other)
    {
        return _bulletBitMap.CircleCollision(X, Y, other.CollisionCircle);
    }
    public Bullet(Window gamewindow, double x, double y, double targetX, double targetY, double speed = 10)
    {

        this._gameWindow = gamewindow;
        this.X = x;
        this.Y = y;
        this.TargetX = targetX;
        this.TargetY = targetY;
        this.Speed = speed;
        _bulletBitMap = new Bitmap("b1", "bullet.png");

    }

    public void Draw()
    {

        if (this.X < TargetX)
        {


            if (this.X + Speed <= TargetX)
            {
                this.X = this.X + Speed;
            }
            else
            {
                this.X = TargetX;
            }

        }
        else if (this.X > TargetX)
        {
            if (this.X - Speed >= TargetX)
            {
                this.X = this.X - Speed;
            }
            else
            {
                this.X = TargetX;
            }
        }

        if (this.Y < TargetY)
        {
            if (this.Y + Speed <= TargetY)
            {
                this.Y = this.Y + Speed;
            }
            else
            {
                this.Y = TargetY;
            }
        }
        else if (this.Y > TargetY)
        {
            if (this.Y - Speed >= TargetY)
            {
                this.Y = this.Y - Speed;
            }
            else
            {
                this.Y = TargetY;
            }
        }

        _gameWindow.DrawBitmap(_bulletBitMap, x: X, y: Y);

        if (X == TargetX && Y == TargetY)
            this.MissTheTarget = true;

    }

}