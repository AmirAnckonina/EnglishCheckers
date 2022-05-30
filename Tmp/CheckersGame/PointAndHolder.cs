using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CheckersGame
{
    public struct PointAndHolder
    {
        private Point m_PointOnBoard;
        private Square m_Square;

        public PointAndHolder(Square i_Sqr)
        {
            m_PointOnBoard = new Point(i_Sqr.SquareIndex.ColumnIdx, i_Sqr.SquareIndex.RowIdx);
            m_Square = i_Sqr;
        }

        public Point PointOnBoard
        {
            get
            {
                return m_PointOnBoard;
            }

            set
            {
                m_PointOnBoard = value;
            }
        }

        public Square Sqr
        {
            get
            {
                return m_Square;
            }
        }
    }
}
