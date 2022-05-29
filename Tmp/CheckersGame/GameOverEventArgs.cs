using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersGame
{
    public class GameOverEventArgs : EventArgs
    {

        GameLogic.eGameResult m_GameResult;
        int m_WinnerScors;
        bool m_AnotherGame;

        public GameOverEventArgs(GameLogic.eGameResult i_GameResult,int i_WinnerScors,bool i_AnotherGame)
        {
            m_GameResult = i_GameResult;
            m_WinnerScors = i_WinnerScors;
            m_AnotherGame = i_AnotherGame;
        }

        public GameLogic.eGameResult GameResult
        {
            get
            {
                return m_GameResult;
            }

            set
            {
                m_GameResult = value;
            }
        }

        public int WinnerScors
        {
            get
            {
                return m_WinnerScors;
            }

            set
            {
                m_WinnerScors = value;
            }
        }

        public bool AnotherGame
        {
            get
            {
                return m_AnotherGame;
            }

            set
            {
                m_AnotherGame = value;
            }
        }

    }
}
