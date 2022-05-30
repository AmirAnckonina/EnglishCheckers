using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersGame
{
    public class GameOverEventArgs : EventArgs
    {

        GameLogic.eGameResult m_GameResult;
        int m_WinnerPlayerScore;

        public GameOverEventArgs(GameLogic.eGameResult i_GameResult, int i_WinnerPlayerScore)
        {
            m_GameResult = i_GameResult;
            m_WinnerPlayerScore = i_WinnerPlayerScore;
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

        public int WinnerPlayerScore
        {
            get
            {
                return m_WinnerPlayerScore;
            }

            set
            {
                m_WinnerPlayerScore = value;
            }
        }
    }
}
