using System;
using System.Text;
using CheckersGame;

namespace CheckersUI
{
    public class ConsoleUI
    {
        private readonly Game r_Game;
        private readonly ConsoleIOManager r_ConsoleIOManager;
        private readonly GameDetails r_GameDetails;

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
                ResetBetweenSessionsProcedure();
                playAnotherRound = r_ConsoleIOManager.AskForAnotherRound();

            } while (playAnotherRound); // while should be in proceeding line!

            r_Game.SaveFinalCheckersGameResult();
            r_ConsoleIOManager.PrintAllGameSessionsResult(r_Game.FinalCheckersSessionResult, r_Game.FirstPlayer, r_Game.SecondPlayer);
            r_ConsoleIOManager.Goodbye();
        }

        private void ResetBetweenSessionsProcedure()
        {
            r_ConsoleIOManager.RawMoveInputManager.QuitInserted = false;
            r_Game.ResetObjectsBetweenSessions();
        }
       
        private void GameInitialization()
        {
            r_ConsoleIOManager.GetGameDetailsProcedure(r_GameDetails);
            r_Game.GameMode = r_GameDetails.GameMode;
            /// *** Add Initialize to Both players PotentialMoves Lists
            r_Game.SetGameObjects(r_GameDetails.FirstPlayerName, r_GameDetails.SecondPlayerName, r_GameDetails.BoardSize);
        }

        private void RunSingleGameSession()
        {
            r_Game.TurnsSetup();
            r_ConsoleIOManager.PrintBoard(r_Game.Board, Player.ePlayerType.Human);
            do
            {
                r_Game.SwitchTurn();
                r_ConsoleIOManager.PrintWhoseTurn(r_Game.CurrentPlayer);
                CurrentPlayerTurnProcedure();
            } while (!r_Game.GameOver(r_ConsoleIOManager.RawMoveInputManager.QuitInserted)); // while should be in proceeding line!


            r_Game.ScoreCalculationAndUpdate();
            r_ConsoleIOManager.PrintSingleGameResult(r_Game.SingleGameResult, r_Game.FirstPlayer, r_Game.SecondPlayer);
        }

        private void CurrentPlayerTurnProcedure()
        {
            /// PotentialMove newPotentialMove;
            /// If it's a recurring turn we are called from RecurringTurnProcedure and this check already done.
            if (!r_Game.IsRecurringTurn)
            {
                /// *** Check CurrPlayer's list of EatingMoves contain the DestIdx as a SrcIdx
                r_Game.CurrentPlayerAnyEatingMovePossibilityCheck();
            }

           LoadNewPotentialMoveProcedure();
            if (!r_ConsoleIOManager.RawMoveInputManager.QuitInserted)
            {
                MoveValidationProcedure();
                r_Game.MoveManager.ExecuteMove(r_Game.Board, r_Game.CurrentPlayer);
                r_Game.PostMoveProcedure();
                r_ConsoleIOManager.RawMoveInputManager.LoadLastMoveToRawInput(r_Game.MoveManager.SrcIdx, r_Game.MoveManager.DestIdx);
                r_ConsoleIOManager.PrintBoard(r_Game.Board, r_Game.CurrentPlayer.PlayerType);
                r_ConsoleIOManager.PrintLastMoveByRawInput(r_ConsoleIOManager.RawMoveInputManager.RawInput, r_Game.CurrentPlayer);
                RecurringTurnProcedure();
            }
        }

        /// private void GetAndLoadNewPotentialMove()
        private void LoadNewPotentialMoveProcedure()
        {
            PotentialMove newPotentialMove;

            if (r_Game.CurrentPlayer.PlayerType == Player.ePlayerType.Human)
            {
                /// getANewMoveByInput
                /// newPotentialMove = r_ConsoleIOManager.GetMoveInput();
                /// Load to MoveManager the newPotentialMove
                /// r_Game.MoveManager.LoadNewPotentialMove(newPotentialMove);
                newPotentialMove = r_ConsoleIOManager.GetMoveInput();

                /// r_Game.LoadSpecificNewPotentialMove(r_ConsoleIOManager.RawMoveInputManager.SourceIndex, r_ConsoleIOManager.RawMoveInputManager.DestinationIndex);
                r_Game.LoadSpecificNewPotentialMove(newPotentialMove);
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

            else /// Regular turn.
            {
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
