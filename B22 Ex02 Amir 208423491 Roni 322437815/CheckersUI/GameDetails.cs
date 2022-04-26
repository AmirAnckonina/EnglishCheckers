using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersUI
{
    public class GameDetails
    {
        /// private const int k_MaxNameLen = 20;
        private StringBuilder m_FirstPlayerName;
        private StringBuilder m_SecondPlayerName;
        private CheckersGame.Game.eGameMode m_GameMode;
        private int m_BoardSize;

        public GameDetails()
        {
            m_FirstPlayerName = new StringBuilder();
            m_SecondPlayerName = new StringBuilder();
        }

        public StringBuilder FirstPlayerName
        {
            get
            {
                return m_FirstPlayerName;
            }

            set
            {
                m_FirstPlayerName = value;
            }
        }

        public StringBuilder SecondPlayerName
        {
            get
            {
                return m_SecondPlayerName;
            }

            set
            {
                m_SecondPlayerName = value;
            }
        }

        public CheckersGame.Game.eGameMode GameMode
        {
            get
            {
                return m_GameMode;
            }

            set
            {
                m_GameMode = value;
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

    }
}
