using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Five_In_a_Row
{
    public partial class GameForm : Form
    {
        private PieceColor nowColor = PieceColor.black;

        private Board board = new Board();
        public GameForm()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Application.Exit();
        }

        // Add the Piece
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            // cannot be placed
            if (!this.board.CanBePlaced(e.X, e.Y)) return;
            // Add Piece
            AddPiece(e.X, e.Y);
            // check the win status
            CheckWinStatus();
            // change next color
            ChangeNextColor();
        }

        private void AddPiece(int x, int y)
        {
            // add piece
            Point position = this.board.FindTheClosedNode(x, y);
            this.Controls.Add(new Piece(position, nowColor));
            this.board.Place(position, nowColor);
        }

        private void ChangeNextColor()
        {
            if (nowColor == PieceColor.white) nowColor = PieceColor.black;
            else if (nowColor == PieceColor.black) nowColor = PieceColor.white;
        }

        // TODO
        private void CheckWinStatus()
        {
            WinStatus status = this.board.CheckWin();
            if (status.Equals(WinStatus.BLACK_WIN))
            {
                MessageBox.Show("黑子赢", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                NewGame();
            }
            else if (status.Equals(WinStatus.WHITE_WIN))
            {
                MessageBox.Show("白子赢", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                NewGame();
            }
        }

        public void NewGame()
        {
            new GameForm().Show();
            Dispose();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if(board.CanBePlaced(e.X, e.Y))
            {
                this.Cursor = Cursors.Hand;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void btnGameOver_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
