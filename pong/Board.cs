using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

public class Board : UserControl
{

    public int BORDER_WIDTH;
    public int TITLEBAR_HEIGHT;

    private const int WIDTH = 1200;
    private const int HEIGHT = 750;

    private Timer _timer;
    private IContainer _components;

    private Paddle _rightPaddle;
    private Paddle _leftPaddle;
    private Ball _ball;

    public Board()
    {
        BackColor = Color.Blue;
        DoubleBuffered = true;
        this.ClientSize = new Size(WIDTH, HEIGHT);
        _components = new Container();
        _rightPaddle = new Paddle(1100, 450);
        _leftPaddle = new Paddle(100, 450);
        _ball = new Ball(600, 450);

        InitGame();
    }

    private void InitGame()
    {
        _timer = new Timer(this._components);
        _timer.Enabled = true;
        _timer.Interval = 10;
        _timer.Tick += new System.EventHandler(this.OnTick);

        Paint += new PaintEventHandler(this.OnPaint);
    }

    public void OnTick(object sender, EventArgs e)
    {
        _rightPaddle.UpdatePosition();
        _leftPaddle.UpdatePosition();
        DetectCollisions();
        _ball.UpdatePosition();
        this.Refresh();
    }

    private void OnPaint(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        _rightPaddle.Draw(g);
        _leftPaddle.Draw(g);
        _ball.Draw(g);
    }

    public void DetectCollisions()
    {
        Rectangle ball_box = _ball.BoundingBox();
        Rectangle left_paddle_box = _leftPaddle.BoundingBox();
        Rectangle right_paddle_box = _rightPaddle.BoundingBox();
        int paddle_width = right_paddle_box.Width;
        bool left_intersect = ball_box.IntersectsWith(left_paddle_box);
        bool right_intersect = ball_box.IntersectsWith(right_paddle_box);

        if (left_intersect || right_intersect)
        {
            int correction = Convert.ToInt32(paddle_width + 10);
            if (right_intersect)
            {
                correction = -paddle_width;
            }
            _ball.BounceOffPaddle(correction);
        }
        else if (ball_box.Y <= 0)
        {
            _ball.BounceOffWall(10);
        }
        else if (ball_box.Y + ball_box.Height + TITLEBAR_HEIGHT >= HEIGHT)
        {
            _ball.BounceOffWall(-10);
        }
    }
}
