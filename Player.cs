using System;
using System.Collections.Generic;
using SplashKitSDK;
public class Player
{
    private Bitmap _PlayerBitmap;
    private Window _gameWindow;
    public double X { get; private set; }
    public double Y { get; private set; }
    public event EventHandler LifeFinished;
    public bool Quit { get; private set; }
    const int SPEED = 10;
    const int GAP = 10;
    protected Vector2D _velocityBullet;
    public List<Bullet> Bullets { get; set; }

    private int _remaningLife { get; set; }
    public int Width
    {
        get
        {
            return _PlayerBitmap.Width;
        }
    }
    public int Height
    {
        get
        {
            return _PlayerBitmap.Height;
        }
    }
    public Window GameWindow { get => this._gameWindow; set => this._gameWindow = value; }

    public Player(Window gamewindow)
    {
        this._gameWindow = gamewindow;
        Bullets = new List<Bullet>();
        Quit = false;
        _PlayerBitmap = new Bitmap("Player1", "Player.png");
        this.X = gamewindow.Width / 2 - Width;
        this.Y = gamewindow.Height / 2 - Height;
        this._remaningLife = 5;

    }

    public void Draw()
    {
        if (_gameWindow != null)
        {
            _gameWindow.DrawBitmap(_PlayerBitmap, x: X, y: Y);
            foreach (var b in Bullets)
            {
                b.Draw();
            }
        }
        else
        {
            //throw exception.
        }

    }
    public void HandleInput()
    {
        SplashKit.ProcessEvents();
        if (SplashKit.MouseClicked(MouseButton.LeftButton) || SplashKit.MouseClicked(MouseButton.MiddleButton) || SplashKit.MouseClicked(MouseButton.RightButton) || SplashKit.MouseClicked(MouseButton.MouseX1Button) || SplashKit.MouseClicked(MouseButton.MouseX2Button))
        {
            //SplashKit.DisplayDialog("button clicked", "clicked ", null, 20);
            shootRobot();
        }
        if (SplashKit.KeyDown(KeyCode.UpKey))
        {
            Move(0, -SPEED);
        }
        if (SplashKit.KeyDown(KeyCode.DownKey))
        {
            Move(0, SPEED);
        }
        if (SplashKit.KeyDown(KeyCode.RightKey))
        {
            Move(SPEED, 0);
        }
        if (SplashKit.KeyDown(KeyCode.LeftKey))
        {
            Move(-SPEED, 0);
        }
        if (SplashKit.KeyTyped(KeyCode.EscapeKey))
        {
            this.Quit = true;
        }
    }
    public void shootRobot()
    {
        Point2D toPt = SplashKit.MousePosition();
        var newBullet = new Bullet(_gameWindow, this.X, this.Y, toPt.X, toPt.Y);
        Bullets.Add(newBullet);
        newBullet.Draw();
    }
    public void StayOnWindow(Window gameWindowSize)
    {
        if (X < GAP)
        {
            X = GAP;
        }
        if (Y < GAP)
        {
            Y = GAP;
        }
        if (X + Width > gameWindowSize.Width - GAP)
        {
            X = gameWindowSize.Width - GAP - Width;
        }
        if (Y + Height > gameWindowSize.Height - GAP)
        {
            Y = gameWindowSize.Height - GAP - Height;
        }
    }
    public void Move(double amountForward, double amountUp)
    {
        Vector2D movement = new Vector2D();
        movement.X += amountForward;
        movement.Y += amountUp;
        X += movement.X;
        Y += movement.Y;
    }
    public bool CollideWithOtherRobot(Robot other)
    {
        bool result = false;
        result = _PlayerBitmap.CircleCollision(X, Y, other.CollisionCircle);
        if (result == true)
        {
            //reduct one life.
            this._remaningLife = this._remaningLife - 1;
            if (this._remaningLife <= 0)
            {
                if (LifeFinished != null)
                {
                    LifeFinished(this, new EventArgs());
                }
            }
        }
        return result;
    }


    public void QuitTheGame()
    {
        this.Quit = true;
    }

    public string GetLifeStatus()
    {
        return this._remaningLife.ToString();
    }



}