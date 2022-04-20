using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CheckersGameManagement;
using CheckersGame;

namespace CheckersGameManagement
{
    public class CheckersGameManager
    {
        private readonly CheckersGame.Game m_Game;
        private readonly CheckersUI.ConsoleUI m_UI;

        public void RunFullGameSessions()
        {
            //Integrate between UI and GameLogic.
            // via ConsoleUI, get all the relevant details for building a game.
            // Update Game (under CheckersGame project)
            // Split the whole old "TheCheckersGame", using m_Game.

            bool fullGameSessionsFinished = false;


            m_UI.Welcome();
            GameInitialization();

            do
            {
                /// ResetBetweenSessions
                /// PlaySingleSession();
                /// PlayAnotherTurn();

            } while (!fullGameSessionsFinished);


        }

        public void GameInitialization()
        {
            m_UI.RequestGameDetails();
            m_Game.InitalizeGameObjects();
        }
    

        public void RunSingleGameSession()
        {
            Player currPlayer;

            currPlayer = m_Game.FirstPlayer;
            while (!m_Game.GameOver())
            {
                m_UI.PrintWhoseTurn(currPlayer);
                /// m_UI.Input.LoadNewInput - ? do it under RawInputProcedure?
                ///
                /// 
                m_Game.SwitchTurn(currPlayer);

            }
        }
    }
}
