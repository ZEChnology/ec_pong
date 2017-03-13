using System.Drawing;

interface GameObject
{
    void UpdatePosition();
    void Draw(Graphics g);
    Rectangle BoundingBox();
}
