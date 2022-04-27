using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CheckersGame;
namespace CheckersUI
{
    class ConsoleUI
    {
        private readonly Game r_Game;
        private readonly ConsoleIOManager r_ConsoleIOManager;
        private GameDetails r_GameDetails;

        public ConsoleUI()
        {
            r_GameDetails = new GameDetails();
            r_Game = new Game();
            r_ConsoleIOManager = new ConsoleIOManager();
        }

        public void Run()
        {
            bool playAnotherRound;

            r_ConsoleIOManager.Welcome();
            GameInitialization();
            do
            {
                RunSingleGameSession();
                r_ConsoleIOManager.RawMoveInputManager.QuitInserted = false;
                r_Game.ResetObjectsBetweenSessions();
                playAnotherRound = r_ConsoleIOManager.AskForAnotherRound();

            } while (playAnotherRound);

            r_Game.SaveFinalCheckersGameResult();
            r_ConsoleIOManager.PrintAllGameSessionsResult(r_Game.FinalCheckersSessionResult, r_Game.FirstPlayer, r_Game.SecondPlayer);
            r_ConsoleIOManager.Goodbye();
        }

        public void GameInitialization()
        {
            r_ConsoleIOManager.GetGameDetailsProcedure(r_GameDetails);
            r_Game.GameMode = r_GameDetails.GameMode;
            r_Game.SetBoard(r_GameDetails.BoardSize);
            r_Game.SetGamePlayers(r_GameDetails.FirstPlayerName, r_GameDetails.SecondPlayerName);
        }

        public void RunSingleGameSession()
        {
            r_Game.TurnsSetup();
            r_ConsoleIOManager.PrintBoard(r_Game.Board, Player.ePlayerType.Human);
            do
            {
                r_Game.SwitchTurn();
                r_ConsoleIOManager.PrintWhoseTurn(r_Game.CurrentPlayer);
                CurrentPlayerTurnProcedure();
            } while (!r_Game.GameOver(r_ConsoleIOManager.RawMoveInputManager.QuitInserted));


            r_Game.ScoreCalculationAndUpdate();
            r_ConsoleIOManager.PrintSingleGameResult(r_Game.SingleGameResult, r_Game.FirstPlayer, r_Game.SecondPlayer);
        }

        public void CurrentPlayerTurnProcedure()
        {
            /// If it's a recurring turn we are called from RecurringTurnProcedure and this check already done.
            if (!r_Game.IsRecurringTurn)
            {
                r_Game.CurrentPlayerAnyEatingMovePossibilityCheck();
            }

            LoadNewPotentialMoveProcedure();
            if (!r_ConsoleIOManager.RawMoveInputManager.QuitInserted)
            {
                MoveValidationProcedure();
                r_Game.MoveManager.ExecuteMove(r_Game.Board, r_Game.CurrentPlayer);
                r_Game.PostMoveProcedure();
                r_ConsoleIOManager.PrintBoard(r_Game.Board, r_Game.CurrentPlayer.PlayerType);
                RecurringTurnProcedure();
            }
        }

        public void LoadNewPotentialMoveProcedure()
        {
            if (r_Game.CurrentPlayer.PlayerType == Player.ePlayerType.Human)
            {
                r_ConsoleIOManager.RequestMoveInput();
                r_Game.LoadSpecificNewPotentialMove(r_ConsoleIOManager.RawMoveInputManager.SourceIndex, r_ConsoleIOManager.RawMoveInputManager.DestinationIndex);
            }

            else /// (i_CurrentPlayer.PlayerType == Player.ePlayerType.Computer)
            {
                r_Game.GenerateAndLoadNewPotentialMove();
            }
        }

        public void MoveValidationProcedure()
        {
            bool validMove;

            if (r_Game.IsRecurringTurn)
            {
                validMove = r_Game.MoveManager.RecurringTurnMoveValidation(r_Game.Board, r_Game.CurrentPlayer);
                while (!validMove)
                {
                    r_ConsoleIOManager.PrintInvalidInputMoveOption(r_Game.CurrentPlayer.PlayerType);
                    LoadNewPotentialMoveProcedure();
                    validMove = r_Game.MoveManager.RecurringTurnMoveValidation(r_Game.Board, r_Game.CurrentPlayer);
                }
            }

            else //Regular turn.
            {
                /// Check If there any eating option
                /// If so , allow only eating option
                /// if not check for simple move
                /// m_Game.CheckAndUpdateIfCurrentPlayerMustEat();
                validMove = r_Game.MoveManager.MoveValidation(r_Game.Board, r_Game.CurrentPlayer);
                while (!validMove)
                {
                    r_ConsoleIOManager.PrintInvalidInputMoveOption(r_Game.CurrentPlayer.PlayerType);
                    LoadNewPotentialMoveProcedure();
                    validMove = r_Game.MoveManager.MoveValidation(r_Game.Board, r_Game.CurrentPlayer);
                }
            }
        }

        public void RecurringTurnProcedure()
        {
            bool recurringTurnIsPossible;

            recurringTurnIsPossible = r_Game.RecurringTurnPossibilityValidation();
            while (recurringTurnIsPossible)
            {
                r_ConsoleIOManager.PrintWhoseTurn(r_Game.CurrentPlayer);
                CurrentPlayerTurnProcedure();
                r_ConsoleIOManager.PrintBoard(r_Game.Board, r_Game.CurrentPlayer.PlayerType);
                recurringTurnIsPossible = r_Game.RecurringTurnPossibilityValidation();
            }
        }
    }
}
