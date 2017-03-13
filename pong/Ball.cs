using System;
using System.Drawing;
using System.IO;
using System.Reflection;

public class Ball : GameObject
{

    public int X, Y;
    public int XMove, YMove;
    private int _speed;
    private Bitmap _sprite;

    public Ball(int startX, int startY)
    {
        X = startX;
        Y = startY;
        _speed = 10;
        Random rnd = new Random();
        XMove = 1;
        YMove = rnd.Next(1, 2);
        try
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream("pong.Resources.ball.png");
            _sprite = new Bitmap(stream);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void BounceOffPaddle(int correction)
    {
        X += correction;
        XMove = -XMove;
    }

    public void BounceOffWall(int correction)
    {
        Y += correction;
        YMove = -YMove;
    }

    public void UpdatePosition()
    {
        X += XMove * _speed;
        Y += YMove * _speed;
    }

    public void Draw(Graphics g)
    {
        g.DrawImage(_sprite, X, Y);
    }

    public Rectangle BoundingBox()
    {
        Rectangle rect = new Rectangle(X, Y, _sprite.Width, _sprite.Height);
        return rect;
    }
}
