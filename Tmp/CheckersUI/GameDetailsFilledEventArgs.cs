using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersUI
{
    public class GameDetailsFilledEventArgs : EventArgs
    {
        private string m_Player1Name;
        private string m_Player2Name;
        private int m_BoardSize;
        private readonly bool m_Player2IsHuman;

        public GameDetailsFilledEventArgs(string i_Player1Name, string i_Player2Name, int i_BoardSize, bool i_Player2IsHuman)
        {
            m_Player1Name = i_Player1Name;
            m_Player2Name = i_Player2Name;
            m_BoardSize = i_BoardSize;
            m_Player2IsHuman = i_Player2IsHuman;
        }

        public string Player1Name
        {
            get
            {
                return m_Player1Name;
            }

            set
            {
                m_Player1Name = value;
            }
        }

        public string Player2Name
        {
            get
            {
                return m_Player2Name;
            }

            set
            {
                m_Player2Name = value;
            }
        }

        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }

            set
            {
                m_BoardSize = value;
            }
        }

        public bool Player2IsHuman
        {
            get
            {
                return m_Player2IsHuman;
            }
        }


    }
}
