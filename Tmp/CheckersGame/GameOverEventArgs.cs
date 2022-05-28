using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersGame
{
    public class GameOverEventArgs : EventArgs
    {
        private string m_Message;

        public GameOverEventArgs(string i_Message)
        {
            m_Message = i_Message;
        }

        public string Message
        {
            get
            {
                return m_Message;
            }
        }
    }
}
