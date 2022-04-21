using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CheckersUI;
using CheckersGame;

namespace GameManagement
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
            m_Game.GameMode = m_UI.GameDetails.GameMode;
            m_Game.Board.SetBoard(m_UI.GameDetails.BoardSize);
            m_Game.SetGamePlayers(m_UI.GameDetails.NameOfFirstPlayer, m_UI.GameDetails.NameOfSecondPlayer);
        }


        public void RunSingleGameSession()
        {
            m_Game.CurrentPlayer = m_Game.FirstPlayer;
            while (!m_Game.GameOver())
            {
                m_UI.PrintWhoseTurn(m_Game.CurrentPlayer);
                NewPotentialMoveProcedure();
                m_Game.MoveProcedure();
                ///
                /// 
                m_Game.SwitchTurn();
                //m_UI.PrintBoard(m_Game.Board);

            }
        }

        public void NewPotentialMoveProcedure()
        {
            if (m_Game.CurrentPlayer.PlayerType == ePlayerType.Human)
            {
                RawInputProcedure();
                m_Game.LoadNewPotentialMove(m_UI.Input.SourceIndex, m_UI.Input.DestinationIndex);
                //m_Game.MoveManager.
            }

            else /// (i_CurrentPlayer.PlayerType == ePlayerType.Computer)
            {
                m_Game.GenerateRandomPotentialMove();
            }
        }

        public void RawInputProcedure()
        {
            m_UI.Input.LoadNewInput();

            while (!m_UI.Input.InputStructureIsValid)
            {
                m_UI.PrintInvalidInputStructure();
                m_UI.Input.LoadNewInput();
            }

        }

        public void MoveProcedure()
        {
           /* bool validMove;

            validMove = m_Game.MoveManager.MoveValidation();
            while (!validMove)
            {
                // m_UI.PrintInvalidInputMoveOption();
                validMove = m_Move.MoveValidation(m_Board, i_CurrPlayer, m_UI.Input.SrcIndex, m_UI.Input.DestIndex);
            }

            //Update movement on board.
            m_Move.ExecuteMove(m_Board, i_CurrPlayer, m_UI.Input.SrcIndex, m_UI.Input.DestIndex);*/
        }

    }
}
