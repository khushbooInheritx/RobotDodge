using SplashKitSDK;
using System;
using System.Collections.Generic;
public class RobotDodge
{
    private Player _player;
    private Window _gameWindow;
    private Timer _timeCounter = new Timer("Player's Time");
    private List<Robot> robots = new List<Robot>();
    public Window GameWindow { get => this._gameWindow; set => this._gameWindow = value; }

    public bool Quit
    {
        get
        {
            return _player.Quit;
        }
    }
    public RobotDodge(Window gameWindow)
    {
        this._gameWindow = gameWindow;
        _player = new Player(_gameWindow);
        AddRobot();
        _player.LifeFinished += LifeEnded;
        _timeCounter.Start();
    }

    public void LifeEnded(object sender, EventArgs e)
    {
        SplashKit.DisplayDialog("Game Over!!", "Your game is over", null, 20);
        _player.QuitTheGame();
    }


    public void AddRobot()
    {
        for (int i = 0; i < 8; i++)
        {
            if (i < 2)
            {
                Roundy _testRobot = new Roundy(_gameWindow, _player);
                robots.Add(_testRobot);
            }
            else
            {
                Boxy _testBox = new Boxy(_gameWindow, _player);
                robots.Add(_testBox);
            }
        }
    }
    public void HandleInput()
    {
        _player.HandleInput();
        _player.StayOnWindow(_gameWindow);
    }
    public void Draw()
    {
        _gameWindow.Clear(Color.White);
        foreach (Robot r in robots)
        {
            r.Draw();
            r.Update();
            uint score = _timeCounter.Ticks / 1000;
            _gameWindow.DrawText("Your Score is: " + score.ToString(), Color.Blue, 10, 10);
            _gameWindow.DrawText("Player's Life ❤️ " + _player.GetLifeStatus(), Color.Green, 650, 10);
        }
        _player.Draw();

        _gameWindow.Refresh(60);
    }
    public void RandomRobot()
    {
        foreach (Robot r in robots)
        {
            r.Draw();
            r.Update();
        }
    }
    public void Update()
    {
        CheckCollisions();
        RandomRobot();
    }
    private void CheckCollisions()
    {
        List<Robot> removeRobots = new List<Robot>();
        List<Bullet> removeBullets = new List<Bullet>();
        foreach (Robot robot in robots)
        {
            if (_player.CollideWithOtherRobot(robot) || robot.IsOffScreen(_gameWindow) == true)
            {
                removeRobots.Add(robot);
                //Console.WriteLine("added in remove robots ");
            }

            //Check for bullets;
            foreach (var b in _player.Bullets)
            {

                if (b.CollideWithOtherRobot(robot))
                {
                    removeBullets.Add(b);
                    removeRobots.Add(robot);
                }

                if (b.MissTheTarget)
                {
                    removeBullets.Add(b);
                }
            }
        }
        foreach (Robot robot in removeRobots)
        {
            robots.Remove(robot);
            if (robot is Boxy)
            {
                Boxy _testRobot = new Boxy(_gameWindow, _player);
                robots.Add(_testRobot);
            }
            else if (robot is Roundy)
            {
                Roundy _testRobot = new Roundy(_gameWindow, _player);
                robots.Add(_testRobot);
            }
        }

        foreach (var Bullet in removeBullets)
        {
            _player.Bullets.Remove(Bullet);
        }
    }
}