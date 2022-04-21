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
        private readonly Game m_Game;
        private readonly ConsoleUI m_UI;


        public CheckersGameManager()
        {
            m_Game = new Game();
            m_UI = new ConsoleUI();
        }

        public void Run()
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
                RunSingleGameSession();
                /// PlayAnotherTurn();

            } while (!fullGameSessionsFinished);


        }

        public void GameInitialization()
        {
            m_UI.RequestGameDetails();
            m_Game.GameMode = m_UI.GameDetails.GameMode;
            m_Game.SetBoard(m_UI.GameDetails.BoardSize);
            m_Game.SetGamePlayers(m_UI.GameDetails.FirstPlayerName, m_UI.GameDetails.SecondPlayerName);
        }

        public void RunSingleGameSession()
        {
            m_Game.CurrentPlayer = m_Game.FirstPlayer;
            m_UI.PrintBoard(m_Game.Board);
            while (!m_Game.GameOver())
            {
                m_UI.PrintWhoseTurn(m_Game.CurrentPlayer);
                MoveProcedure();
                m_Game.SwitchTurn();
                m_UI.PrintBoard(m_Game.Board);
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
            NewPotentialMoveProcedure();
            MoveValidationProcedure();
            m_Game.MoveManager.ExecuteMove(m_Game.Board, m_Game.CurrentPlayer);
            m_UI.PrintBoard(m_Game.Board);
        }

        public void MoveValidationProcedure()
        {
            bool validMove;

            if (m_Game.IsRecurringTurn)
            {
                /// validMove = m_Game.MoveManager.RecurringTurnMoveValidation();
               /* while (!validMove)
                {
                    m_UI.PrintInvalidInputMoveOption();
                    NewPotentialMoveProcedure();
                    /// validMove = m_Game.MoveManager.RecurringTurnMoveValidation();
                }*/
            }

            else //Regular turn.
            {
                validMove = m_Game.MoveManager.MoveValidation(m_Game.Board, m_Game.CurrentPlayer);
                while (!validMove)
                {
                    m_UI.PrintInvalidInputMoveOption();
                    NewPotentialMoveProcedure();
                    validMove = m_Game.MoveManager.MoveValidation(m_Game.Board, m_Game.CurrentPlayer);
                }
            }
        }

    }
}
