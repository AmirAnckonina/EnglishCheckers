using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersGame
{
    public class MoveExecutedEventArgs : EventArgs
    {
        private MoveManager m_LastMove;

        public MoveExecutedEventArgs(MoveManager i_LastMove)
        {
            m_LastMove = i_LastMove;
        }

        public MoveManager LastMove
        {
            get
            {
                return m_LastMove;
            }
        }
    }
}
