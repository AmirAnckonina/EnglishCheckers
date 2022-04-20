using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersUI
{
    public class GameDetails
    {
        private StringBuilder m_FirstPlayerName;
        private StringBuilder m_SecondtPlayerName;
        private CheckersGame.eGameMode m_GameMode;
        private int m_BoardSize;

        public StringBuilder NameOfFirstPlayer
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

        public StringBuilder NameOfSecondPlayer
        {
            get
            {
                return NameOfSecondPlayer;
            }

            set
            {
                NameOfSecondPlayer = value;
            }
        }

        public CheckersGame.eGameMode GameMode
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
