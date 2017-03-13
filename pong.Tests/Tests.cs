using System;
using System.Drawing;
using NUnit.Framework;
using NSubstitute;
using Should;

namespace pong.Tests
{
    [TestFixture]
    public class PaddleTests
    {
        [Test]
        public void PaddleBoundingBoxIsRect()
        {
            Paddle paddle = new Paddle(0, 0);
            paddle.BoundingBox().ShouldBeType(typeof(Rectangle));
        }

    }

    [TestFixture]
    public class BallTests
    {

        private Ball ball;

        [SetUp]
        public void Init()
        {
            ball = new Ball(0, 0);
        }

        [Test]
        public void BoundingBoxIsRect()
        {
            ball.BoundingBox().ShouldBeType(typeof(Rectangle));
        }

        [Test]
        public void BounceOffPaddleNegatesX()
        {
            int xmove = ball.XMove;
            ball.BounceOffPaddle(10);
            ball.XMove.ShouldEqual(-xmove);
        }

        [Test]
        public void BounceOffPaddleAppliesCorrection()
        {
            ball.BounceOffPaddle(10);
            ball.X.ShouldEqual(10);
        }

        [Test]
        public void BounceOffWallNegatesY()
        {
            int ymove = ball.YMove;
            ball.BounceOffWall(10);
            ball.YMove.ShouldEqual(-ymove);
        }

        [Test]
        public void BounceOffWallAppliesCorrection()
        {
            ball.BounceOffWall(10);
            ball.Y.ShouldEqual(10);
        }

        [Test]
        public void UpdatePositionUpdatesXandY()
        {
            int x = ball.X;
            int y = ball.Y;

            ball.UpdatePosition();
            ball.X.ShouldNotEqual(x);
            ball.Y.ShouldNotEqual(y);
        }
    }
}
