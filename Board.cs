using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Five_In_a_Row
{
    enum WinStatus
    {
        NO_ONE_WIN, BLACK_WIN, WHITE_WIN
    }

    class Board
    {
        private static readonly int OFFSET = 75, NODE_RADIUS = 10, NODE_DISTANCE = 75, CAPACITY = 9;
        private static readonly Point NULL_POINT = new Point(-1, -1);

        public bool CanBePlaced(int x, int y)
        {
            if(FindTheClosedNode(x, y) != NULL_POINT)
            {
                Point index = GetIndexFromPoint(new Point(x, y));
                if (!blackPiece[index.X, index.Y] && !whitePiece[index.X, index.Y])
                        return true;
            }
            return false;
        }

        public Point FindTheClosedNode(int x, int y)
        {
            // check the point in piece radius
            int nodeX = FindTheClosedNode(x);
            int nodeY = FindTheClosedNode(y);
            if (nodeX < 0 || nodeY < 0) return NULL_POINT;
            return new Point(nodeX, nodeY);
        }

        private int FindTheClosedNode(int pos)
        {
            if (pos < OFFSET - NODE_RADIUS) return -1;

            pos -= OFFSET;
            int quotient = pos / NODE_DISTANCE;
            int reminder = pos % NODE_DISTANCE;

            if (reminder <= NODE_RADIUS) 
                return quotient * NODE_DISTANCE + OFFSET;
            else if (reminder >= NODE_DISTANCE - NODE_RADIUS) 
                return (quotient + 1) * NODE_DISTANCE + OFFSET;
            else return -1;
        }

        private bool[,] blackPiece = new bool[CAPACITY, CAPACITY];
        private bool[,] whitePiece = new bool[CAPACITY, CAPACITY];
        public void Place(Point pos, PieceColor color)
        {
            Point index = GetIndexFromPoint(pos);
            if (color == PieceColor.black) blackPiece[index.X, index.Y] = true;
            else whitePiece[index.X, index.Y] = true;
        }

        private Point GetIndexFromPoint(Point pos)
        {
            pos = FindTheClosedNode(pos.X, pos.Y);
            int indexX = (pos.X - OFFSET) / NODE_DISTANCE;
            int indexY = (pos.Y - OFFSET) / NODE_DISTANCE;
            return new Point(indexX, indexY);
        }

        public WinStatus CheckWin()
        {
            if (CheckFiveContinues(blackPiece)) return WinStatus.BLACK_WIN;
            if (CheckFiveContinues(whitePiece)) return WinStatus.WHITE_WIN;
            return WinStatus.NO_ONE_WIN;
        } 

        private bool CheckFiveContinues(bool[,] pieces)
        {
            for(int i = 0; i < CAPACITY; i++)
            {
                for(int j = 0; j < CAPACITY; j++)
                {
                    if(pieces[i, j])
                    {
                        if (CheckLine(pieces, i, j)) return true;
                    }
                }
            }
            return false;
        }

        private bool CheckLine(bool[,] pieces, int i, int j)
        {
            return CheckR(pieces, i, j) || CheckD(pieces, i, j) || CheckRD(pieces, i, j) || CheckLD(pieces, i, j);
        }

        private bool CheckR(bool[,] pieces, int i, int j)
        {
            if (i + 4 >= CAPACITY) return false;
            for(int k = 0; k < 5; k++)
                if (pieces[i + k, j] == false) return false;
            return true;
        }

        private bool CheckD(bool[,] pieces, int i, int j)
        {
            if (j + 4 >= CAPACITY) return false;
            for (int k = 0; k < 5; k++)
                if (pieces[i, j + k] == false) return false;
            return true;
        }

        private bool CheckLD(bool[,] pieces, int i, int j)
        {
            if (i - 4 < 0 || j + 4 >= CAPACITY) return false;
            for (int k = 0; k < 5; k++)
                if (pieces[i - k, j + k] == false) return false;
            return true;
        }

        private bool CheckRD(bool[,] pieces, int i, int j)
        {
            if (i + 4 >= CAPACITY || j + 4 < 0) return false;
            for (int k = 0; k < 5; k++)
                if (pieces[i + k, j + k] == false) return false;
            return true;
        }

    }
}
