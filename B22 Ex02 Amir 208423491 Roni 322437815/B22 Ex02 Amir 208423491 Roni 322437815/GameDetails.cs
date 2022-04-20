using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B22_Ex02_Amir_208423491_Roni_322437815
{
    public class GameDetails
    {
        private StringBuilder m_FirstPlayerName;
        private StringBuilder m_SecondtPlayerName;
        private int m_EGameMode;
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

        public int GameMode
        {
            get
            {
                return m_EGameMode;
            }

            set
            {
                m_EGameMode = value;
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
