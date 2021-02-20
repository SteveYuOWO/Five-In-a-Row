using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Five_In_a_Row
{
    enum PieceColor
    {
        black, white
    }
    class Piece: PictureBox
    {
        private static readonly int IMAGE_WIDTH = 50, IMAGE_HEIGHT = 50;
        public Piece(int x, int y, PieceColor color)
        {
            Initialize(new Point(x, y), color);
        }

        public Piece(Point p, PieceColor color)
        {
            Initialize(p, color);
        }

        // Initialize function
        public void Initialize(Point p, PieceColor color)
        {
            // offset to center
            p.X -= IMAGE_WIDTH / 2;
            p.Y -= IMAGE_HEIGHT / 2;
            // change other propties
            this.BackColor = Color.Transparent;
            this.Size = new Size(IMAGE_WIDTH, IMAGE_HEIGHT);
            this.Location = p;
            if (color == PieceColor.white) this.Image = Resources.white;
            else if (color == PieceColor.black) this.Image = Resources.black;
        }
    }
}
