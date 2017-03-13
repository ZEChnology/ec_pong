using System;
using System.Drawing;
using System.Windows.Forms;

class Pong: Form {

    public Pong() {

        Text = "Pong";
        DoubleBuffered = true;
        FormBorderStyle = FormBorderStyle.FixedSingle;

        int borderWidth = (this.Width - this.ClientSize.Width) / 2;
        int titleBarHeight = this.Height - this.ClientSize.Height - borderWidth;

        Board board = new Board();
        board.BORDER_WIDTH = borderWidth;
        board.TITLEBAR_HEIGHT = titleBarHeight;

        Size = board.ClientSize;
        Controls.Add(board);
        CenterToScreen();
    }
}


class MApplication {
    public static void Main() {
        Application.Run(new Pong());
    }
}
