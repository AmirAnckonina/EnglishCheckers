using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersGame
{
    public class ReachedLastLineEventArgs : EventArgs
    {
        private PointAndHolder m_LastLineIdxAndHolder;

        public ReachedLastLineEventArgs(SquareIndex i_SqrIdx, Player.ePlayerRecognition i_PlayerRecognition)
        {
            m_LastLineIdxAndHolder = new PointAndHolder(i_SqrIdx, i_PlayerRecognition);
        }

        public PointAndHolder LastLineIdxAndHolder
        {
            get
            {
                return m_LastLineIdxAndHolder;
            }
        }
    }
}
