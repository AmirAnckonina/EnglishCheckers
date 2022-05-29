using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersGame
{
    public class MoveExecutedEventArgs : EventArgs
    {
        private SquareIndex m_SrcIdx;
        private SquareIndex m_DestIdx;
        private SquareIndex m_EatedSquareIdx;

        public MoveExecutedEventArgs(SquareIndex i_ScrIdx, SquareIndex i_DestIdx, SquareIndex i_EatedSquareIdx)
        {
            m_SrcIdx = i_ScrIdx;
            m_DestIdx = i_DestIdx;
            m_EatedSquareIdx = i_EatedSquareIdx;
        }

        public SquareIndex SrcIdx
        {
            get
            {
                return m_SrcIdx;
            }

            set
            {
                m_SrcIdx = value;
            }
        }

        public SquareIndex DestIdx
        {
            get
            {
                return m_DestIdx;
            }

            set
            {
                m_DestIdx = value;
            }
        }

        public SquareIndex EatedSquareIdx
        {
            get
            {
                return m_EatedSquareIdx;
            }

            set
            {
                m_EatedSquareIdx = value;
            }
        }
    }
}
