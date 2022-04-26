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
            bool playAnotherRound;

            m_UI.Welcome();
            GameInitialization();
            do
            {
                RunSingleGameSession();
                m_Game.ResetBetweenSessions();
                playAnotherRound =  m_UI.AskForAnotherRound();

            } while (playAnotherRound);

            m_Game.SaveFinalCheckersGameResult();
            m_UI.PrintAllGameSessionsResult(m_Game.FinalCheckersSessionResult, m_Game.FirstPlayer, m_Game.SecondPlayer);
            m_UI.Goodbye();
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
            m_Game.TurnsSetup();
            m_UI.PrintBoard(m_Game.Board, Player.ePlayerType.Human);
            do
            {
                m_Game.SwitchTurn();
                m_UI.PrintWhoseTurn(m_Game.CurrentPlayer); /// Move to ConsoleUI
                CurrentPlayerTurnProcedure();

            } while (!m_Game.GameOver(m_UI.Input.QuitInserted)); 

            m_Game.ScoreCalculationAndUpdate();
            m_UI.PrintSingleGameResult(m_Game.SingleGameResult, m_Game.FirstPlayer, m_Game.SecondPlayer);
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
            if (m_Game.CurrentPlayer.PlayerType == Player.ePlayerType.Human)
            {
                m_UI.RequestMoveInput();
                m_Game.LoadNewPotentialMove(m_UI.Input.SourceIndex, m_UI.Input.DestinationIndex);
            }

            else /// (i_CurrentPlayer.PlayerType == Player.ePlayerType.Computer)
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
                while (!validMove)
                {
                    m_UI.PrintInvalidInputMoveOption(m_Game.CurrentPlayer.PlayerType);
                    LoadNewPotentialMoveProcedure();
                    validMove = m_Game.MoveManager.RecurringTurnMoveValidation(m_Game.Board, m_Game.CurrentPlayer);
                }
            }

            else //Regular turn.
            {
                /// Check If there any eating option
                /// If so , allow only eating option
                /// if not check for simple move
                m_Game.CheckAndUpdateIfCurrentPlayerMustEat();
                validMove = m_Game.MoveManager.MoveValidation(m_Game.Board, m_Game.CurrentPlayer);
                while (!validMove)
                {
                    m_UI.PrintInvalidInputMoveOption(m_Game.CurrentPlayer.PlayerType); 
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
