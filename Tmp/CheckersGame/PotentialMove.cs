using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersGame
{
    public struct PotentialMove
    {
        private SquareIndex m_SrcIdx;
        private SquareIndex m_DestIdx;

        public PotentialMove(SquareIndex i_SrcIdx, SquareIndex i_DestIdx)
        {
            m_SrcIdx = i_SrcIdx;
            m_DestIdx = i_DestIdx;
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
    }
}
