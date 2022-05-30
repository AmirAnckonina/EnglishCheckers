using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersGame
{
    public class GameOverEventArgs : EventArgs
    {
        private string m_GameResultMessage;

        public GameOverEventArgs(
            GameLogic.eGameResult i_GameResult,
            int i_WinnerPlayerScore,
            string i_FirstPlayerName,
            string i_SecondPlayerName)
        {
            SetGameResultMessage(i_GameResult, i_WinnerPlayerScore, i_FirstPlayerName, i_SecondPlayerName);
        }

        public string GameResultMessage
        {
            get
            {
                return m_GameResultMessage;
            }
        }

        public void SetGameResultMessage(
            GameLogic.eGameResult i_GameResult,
            int i_WinnerPlayerScore,
            string i_FirstPlayerName,
            string i_SecondPlayerName)
        {
            string winnerPlayerName;

            if (i_GameResult != GameLogic.eGameResult.Draw)
            {
                if (i_GameResult == GameLogic.eGameResult.FirstPlayerWon)
                {
                    winnerPlayerName = i_FirstPlayerName;
                }

                else /// (i_GameResult == GameLogic.eGameResult.SecondPlayerWon)
                {
                    winnerPlayerName = i_SecondPlayerName;       
                }

                m_GameResultMessage = string.Format(
                        "{0} is the winner! \n{0}'s total score: {1}", winnerPlayerName, i_WinnerPlayerScore
                        );
            }

            else /// Draw
            {
                m_GameResultMessage = string.Format("It's a Draw!");
            }
        }
    }
}
