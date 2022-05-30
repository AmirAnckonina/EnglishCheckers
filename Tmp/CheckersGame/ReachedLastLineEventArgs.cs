using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersGame
{
    public class ReachedLastLineEventArgs : EventArgs
    {
        private Square m_LastLineSquare;

        public ReachedLastLineEventArgs(Square i_LastLineSquare)
        {
            m_LastLineSquare = i_LastLineSquare;
        }

        public Square LastLineSquare
        {
            get
            {
                return m_LastLineSquare;
            }
        }
    }
}
