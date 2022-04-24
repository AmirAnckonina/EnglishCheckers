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
        private GameDetails m_GameDetails;
        private readonly Game m_Game;
        private readonly ConsoleUI m_UI;


        public CheckersGameManager()
        {
            m_GameDetails = new GameDetails();
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
            /// m_GameDetails = m_UI.GameDetails; /// set via m_GameDetails /// CHANGE!
            m_Game.GameMode = m_UI.GameDetails.GameMode;
            m_Game.SetBoard(m_UI.GameDetails.BoardSize);
            m_Game.SetGamePlayers(m_UI.GameDetails.FirstPlayerName, m_UI.GameDetails.SecondPlayerName);
        }

        public void RunSingleGameSession()
        {
            m_Game.CurrentPlayer = m_Game.SecondPlayer; /// CHANGE!!!!
            m_Game.RivalPlayer = m_Game.FirstPlayer;
            m_UI.PrintBoard(m_Game.Board, ePlayerType.Computer);

            do
            {
                m_Game.SwitchTurn();
                m_UI.PrintWhoseTurn(m_Game.CurrentPlayer); /// Move to ConsoleUI
                CurrentPlayerTurnProcedure();

            } while (!m_Game.GameOver(m_UI.Input.QuitInserted)); 

            m_Game.UpdateTheGameResult();
            m_Game.ScoreCalculator();

        }

        public void CurrentPlayerTurnProcedure()
        {
            LoadNewPotentialMoveProcedure();
            if (!m_UI.Input.QuitInserted)
            {
                MoveValidationProcedure();
                m_Game.MoveManager.ExecuteMove(m_Game.Board, m_Game.CurrentPlayer);
                m_Game.PostMoveProcedure();
                m_UI.PrintBoard(m_Game.Board, m_Game.CurrentPlayer.PlayerType);
                RecurringTurnProcedure();
            }

        }
       
        public void LoadNewPotentialMoveProcedure()
        {
            if (m_Game.CurrentPlayer.PlayerType == ePlayerType.Human)
            {
                m_UI.RequestMoveInput();
                m_Game.LoadNewPotentialMove(m_UI.Input.SourceIndex, m_UI.Input.DestinationIndex);
            }

            else /// (i_CurrentPlayer.PlayerType == ePlayerType.Computer)
            {
                m_Game.GenerateRandomPotentialMove();
            }
        }

        public void MoveValidationProcedure()
        {
            bool validMove;

            if (m_Game.IsRecurringTurn)
            {
                validMove = m_Game.MoveManager.RecurringTurnMoveValidation(m_Game.Board, m_Game.CurrentPlayer);
                /// We have here Non-Ending loop in case of computer is playing
                while (!validMove)
                {
                    if (m_Game.CurrentPlayer.PlayerType != ePlayerType.Computer) /// If computer Playing Do NOT Print it!
                    {
                        m_UI.PrintInvalidInputMoveOption(); 
                    }
                    LoadNewPotentialMoveProcedure();
                    validMove = m_Game.MoveManager.RecurringTurnMoveValidation(m_Game.Board, m_Game.CurrentPlayer);
                }
            }

            else //Regular turn.
            {
                validMove = m_Game.MoveManager.MoveValidation(m_Game.Board, m_Game.CurrentPlayer);
                while (!validMove)
                {
                    m_UI.PrintInvalidInputMoveOption(); /// If computer Playing Do NOT Print it!
                    LoadNewPotentialMoveProcedure();
                    validMove = m_Game.MoveManager.MoveValidation(m_Game.Board, m_Game.CurrentPlayer);
                }
            }
        }
       
        public void RecurringTurnProcedure()
        {
            bool recurringTurnIsPossible;

            recurringTurnIsPossible = m_Game.RecurringTurnPossibilityValidation();
            while (recurringTurnIsPossible)
            {
                m_UI.PrintWhoseTurn(m_Game.CurrentPlayer);
                CurrentPlayerTurnProcedure();
                m_UI.PrintBoard(m_Game.Board, m_Game.CurrentPlayer.PlayerType);
                recurringTurnIsPossible = m_Game.RecurringTurnPossibilityValidation();
            }
        }


    }
}
