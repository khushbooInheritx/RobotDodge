using System;
using SplashKitSDK;
using System.Collections.Generic;

public class Program
{
    //  static Player _player;
    static RobotDodge _robotDodge;
    static Window _gameWindow;
    // static Player _player;
    public static void Main()
    {
        _gameWindow = new Window("RodgeWindow", 800, 800);
        _gameWindow.Clear(Color.White);

        _robotDodge = new RobotDodge(_gameWindow);

        while (!_gameWindow.CloseRequested)
        {
            SplashKit.ProcessEvents();
            _robotDodge.Draw();
            _robotDodge.HandleInput();
            _robotDodge.Update();
            if (_robotDodge.Quit == true)
            {
                break;
            }
        }
        _gameWindow.Close();
        _gameWindow = null;
    }
}
