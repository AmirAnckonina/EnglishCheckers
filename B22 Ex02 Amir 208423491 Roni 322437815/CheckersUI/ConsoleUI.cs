using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CheckersGame;
namespace CheckersUI
{
    class ConsoleUI
    {
        private readonly Game m_Game;
        private readonly ConsoleIOManager m_ConsoleIOManager;
        private GameDetails m_GameDetails;

        public ConsoleUI()
        {
            m_GameDetails = new GameDetails();
            m_Game = new Game();
            m_ConsoleIOManager = new ConsoleIOManager();
        }

        public void Run()
        {
            bool playAnotherRound;

            m_ConsoleIOManager.Welcome();
            GameInitialization();
            do
            {
                RunSingleGameSession();
                m_Game.ResetBetweenSessions();
                playAnotherRound = m_ConsoleIOManager.AskForAnotherRound();

            } while (playAnotherRound);

            m_Game.SaveFinalCheckersGameResult();
            m_ConsoleIOManager.PrintAllGameSessionsResult(m_Game.FinalCheckersSessionResult, m_Game.FirstPlayer, m_Game.SecondPlayer);
            m_ConsoleIOManager.Goodbye();
        }

        public void GameInitialization()
        {
            m_ConsoleIOManager.GetGameDetailsProcedure(m_GameDetails);
            m_Game.GameMode = m_GameDetails.GameMode;
            m_Game.SetBoard(m_GameDetails.BoardSize);
            m_Game.SetGamePlayers(m_GameDetails.FirstPlayerName, m_GameDetails.SecondPlayerName);
        }

        public void RunSingleGameSession()
        {
            m_Game.TurnsSetup();
            m_ConsoleIOManager.PrintBoard(m_Game.Board, Player.ePlayerType.Human);
            do
            {
                m_Game.SwitchTurn();
                m_ConsoleIOManager.PrintWhoseTurn(m_Game.CurrentPlayer);
                CurrentPlayerTurnProcedure();

            } while (!m_Game.GameOver(m_ConsoleIOManager.RawMoveInputManager.QuitInserted));

            m_Game.ScoreCalculationAndUpdate();
            m_ConsoleIOManager.PrintSingleGameResult(m_Game.SingleGameResult, m_Game.FirstPlayer, m_Game.SecondPlayer);
        }

        public void CurrentPlayerTurnProcedure()
        {
            LoadNewPotentialMoveProcedure();
            if (!m_ConsoleIOManager.RawMoveInputManager.QuitInserted)
            {
                MoveValidationProcedure();
                m_Game.MoveManager.ExecuteMove(m_Game.Board, m_Game.CurrentPlayer);
                m_Game.PostMoveProcedure();
                m_ConsoleIOManager.PrintBoard(m_Game.Board, m_Game.CurrentPlayer.PlayerType);
                RecurringTurnProcedure();
            }
        }

        public void LoadNewPotentialMoveProcedure()
        {
            if (m_Game.CurrentPlayer.PlayerType == Player.ePlayerType.Human)
            {
                m_ConsoleIOManager.RequestMoveInput();
                m_Game.LoadNewPotentialMove(m_ConsoleIOManager.RawMoveInputManager.SourceIndex, m_ConsoleIOManager.RawMoveInputManager.DestinationIndex);
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
                    m_ConsoleIOManager.PrintInvalidInputMoveOption(m_Game.CurrentPlayer.PlayerType);
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
                    m_ConsoleIOManager.PrintInvalidInputMoveOption(m_Game.CurrentPlayer.PlayerType);
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
                m_ConsoleIOManager.PrintWhoseTurn(m_Game.CurrentPlayer);
                CurrentPlayerTurnProcedure();
                m_ConsoleIOManager.PrintBoard(m_Game.Board, m_Game.CurrentPlayer.PlayerType);
                recurringTurnIsPossible = m_Game.RecurringTurnPossibilityValidation();
            }
        }
    }
}
