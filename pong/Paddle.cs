using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

public class Paddle : GameObject
{

    public int X, Y;
    private Bitmap _sprite;

    public Paddle(int startX, int startY)
    {
        X = startX;
        Y = startY;

        try
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream("pong.Resources.paddle.png");
            _sprite = new Bitmap(stream);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void UpdatePosition()
    {
        Y = Cursor.Position.Y;
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
