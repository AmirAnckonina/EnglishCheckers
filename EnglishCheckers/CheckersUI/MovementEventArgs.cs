using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using CheckersGame;

namespace CheckersUI
{
    public class MovementEventArgs : EventArgs
    {
        private PotentialMove m_Movement;

        public MovementEventArgs()
        {
            m_Movement = new PotentialMove();
        }

        public MovementEventArgs(Point i_SrcPoint, Point i_DestPoint)
        {
            SquareIndex srcSqrIdx = SquareIndexPointConverter.PointToSquareIndex(i_SrcPoint);
            SquareIndex destSqrIdx = SquareIndexPointConverter.PointToSquareIndex(i_DestPoint);
            m_Movement = new PotentialMove(srcSqrIdx, destSqrIdx);
        }

        public PotentialMove Movement
        {
            get
            {
                return m_Movement;
            }

            set
            {
                m_Movement = value;
            }
        }
    }
}
