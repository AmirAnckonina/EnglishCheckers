using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
