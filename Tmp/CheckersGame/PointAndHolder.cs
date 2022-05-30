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
        private Player.ePlayerRecognition m_PlayerRecognition;

        public PointAndHolder(SquareIndex i_SqrIdx, Player.ePlayerRecognition i_PlayerRecognition)
        {
            m_PointOnBoard = new Point(i_SqrIdx.ColumnIdx, i_SqrIdx.RowIdx);
            m_PlayerRecognition = i_PlayerRecognition;
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

        public Player.ePlayerRecognition PlayerRecognition
        {
            get
            {
                return m_PlayerRecognition;
            }

            set
            {
                m_PlayerRecognition = value;
            }
        }
    }
}
