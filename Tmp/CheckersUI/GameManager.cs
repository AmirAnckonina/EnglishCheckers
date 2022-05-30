using System;
using System.Text;
using CheckersGame;
using System.Drawing;
using System.Collections.Generic;

namespace CheckersUI
{

    /// <summary>
    ///  Design for windows environment.
    ///  ConsoleUI: Responsible to recieve what he needs from "FormGame" and pass that to GameLogic
    ///  FormGame: Responsible to recieve all events, handle them and pass them whenever it needs
    ///  Example: GetMoveInput. Calling from ConsoleUI, Recieved in FormGame,
    ///  waiting for the event to occur (A Valid button press), then return the value to ConsoleUI to conitunue the logic part.
    /// </summary>
    public class GameManager
    {
        private readonly GameLogic r_GameLogicUnit;
        private FormGame m_FormGame;

        public GameManager()
        {
            r_GameLogicUnit = new GameLogic();
            m_FormGame = new FormGame();
        }

        public void Run()
        {
            try
            {
                RegisterFormEvents();
                RegisterLogicEvents();
                m_FormGame.ShowDialog();
            }
            catch 
            {
                
                /// Show message?? ex.Message();
            }
        }

        private void RegisterFormEvents()
        {
            m_FormGame.GameDetailsFilled += m_FormGame_GameDetailsFilled;
            m_FormGame.PotentialMoveEntered += m_FormGame_PotentialMoveEntered;
            /// AfterSessionMessageBoxAnswered
        }

        private void RegisterLogicEvents()
        {
            r_GameLogicUnit.MoveExecuted += r_GameLogicUnit_MoveExecuted;
            r_GameLogicUnit.SingleGameInitialized += r_GameLogicUnit_SingleGameInitialized;
            r_GameLogicUnit.TurnSwitched += r_GameLogicUnit_TurnSwitched;
            r_GameLogicUnit.SingleGameOver += r_GameLogicUnit_SingleGameOver;
            r_GameLogicUnit.InvalidMoveInserted += r_GameLogicUnit_InvalidMoveInserted;
            r_GameLogicUnit.CurrPlayerReachedLastLine += r_GameLogicUnit_CurrPlayerReachedLastLine;
        }

        private void r_GameLogicUnit_CurrPlayerReachedLastLine(object sender, EventArgs e)
        {
            GameLogic gameLogicUnitObj = sender as GameLogic;

            if (gameLogicUnitObj != null)
            {
                /// Update the relevant Point in board to KingDisc.
            }
        }

        private void r_GameLogicUnit_MoveExecuted(object sender, EventArgs e)
        {
            MoveExecutedEventArgs moveExecutedParams = e as MoveExecutedEventArgs;

            if (moveExecutedParams != null)
            { 
                m_FormGame.PostMoveUpdatePicBoxSqrMatrix(
                    moveExecutedParams.NewOccuipiedPoints,
                    moveExecutedParams.NewEmptyPoints
                    );
            }
        }

        private void r_GameLogicUnit_InvalidMoveInserted(object sender, EventArgs e)
        {
            /// Print adaptable mesaage
        }

        private void r_GameLogicUnit_RecurringTurn(object sender, EventArgs e)
        {
            GameLogic gameLogicUnitObj = sender as GameLogic;

            if (gameLogicUnitObj != null)
            {
                /// CurrentPlayerMoveProcedure();
            }
        }

        private void r_GameLogicUnit_TurnSwitched(object sender, EventArgs e)
        {
            GameLogic gameLogicUnitObj = sender as GameLogic;

            if (gameLogicUnitObj != null)
            {
                m_FormGame.CurrentPlayerRecognition = gameLogicUnitObj.CurrentPlayer.PlayerRecognition;
                if (gameLogicUnitObj.CurrentPlayer.PlayerType == Player.ePlayerType.Computer)
                {
                    ComputerPlayerMoveProcedure();
                    /// System.Threading.Thread.Sleep(2000);
                }
            }
        }

        private void ComputerPlayerMoveProcedure()
        {
            PotentialMove newComputerMove;

            r_GameLogicUnit.CurrentPlayerAnyEatingMovePossibilityCheck();
            r_GameLogicUnit.GenerateAndLoadNewPotentialMove();
            newComputerMove = new PotentialMove(
                r_GameLogicUnit.MoveManager.SrcIdx,
                r_GameLogicUnit.MoveManager.DestIdx
                ); 
            CurrentPlayerMoveProcedure(newComputerMove);
        }

        private void m_FormGame_PotentialMoveEntered(object sender, EventArgs e)
        {
            MovementEventArgs movementParams = e as MovementEventArgs;

            if (movementParams != null)
            {
                /// In this stage should be recurring turn == false
                r_GameLogicUnit.CurrentPlayerAnyEatingMovePossibilityCheck();
                CurrentPlayerMoveProcedure(movementParams.Movement);
            }
        }

        private void m_FormGame_GameDetailsFilled(object sender, EventArgs e)
        {
            GameDetailsFilledEventArgs gameDetails = e as GameDetailsFilledEventArgs;

            r_GameLogicUnit.SetGameObjects(gameDetails.Player1Name, gameDetails.Player2Name, gameDetails.BoardSize);
        }

        private void r_GameLogicUnit_SingleGameInitialized(object sender, EventArgs e)
        {
            GameInitializedEventArgs gameInitializedParams = e as GameInitializedEventArgs;
            GameLogic gameLogicUnitObj = sender as GameLogic;

            if (gameLogicUnitObj != null && gameInitializedParams != null)
            {
                m_FormGame.AddDiscsToPictureBoxSquareMatrix(gameInitializedParams.OcciupiedPoints);
                m_FormGame.CurrentPlayerRecognition = gameLogicUnitObj.CurrentPlayer.PlayerRecognition;
                /// Mark Current Player Label
            }
        }

        private void r_GameLogicUnit_SingleGameOver(object sender, EventArgs e)
        {
            GameOverEventArgs gameOverParams = e as GameOverEventArgs;

            if (gameOverParams != null)
            {

            }
        }

        private void CurrentPlayerMoveProcedure(PotentialMove i_PotentialMove)
        {
            r_GameLogicUnit.LoadSpecificNewPotentialMove(i_PotentialMove);
            if (r_GameLogicUnit.MoveValidationProcedure()) // If and only if is valid
            {
                r_GameLogicUnit.ExecuteMoveProcedure();
                r_GameLogicUnit.PostMoveProcedure();
                r_GameLogicUnit.SwitchTurn();
            }
        }

        /* public void RecurringTurnProcedure()
         {
             bool recurringTurnIsPossible;

             recurringTurnIsPossible = r_GameLogicUnit.RecurringTurnPossibilityValidation();
             while (recurringTurnIsPossible)
             {
                 r_ConsoleIOManager.PrintWhoseTurn(r_GameLogicUnit.CurrentPlayer);
                 CurrentPlayerTurnProcedure();
                 r_ConsoleIOManager.PrintBoard(r_GameLogicUnit.Board, r_GameLogicUnit.CurrentPlayer.PlayerType);
                 recurringTurnIsPossible = r_GameLogicUnit.RecurringTurnPossibilityValidation();
             }
         }*/


        ///private void CurrentPlayerTurnProcedure()
        /// {
        /// PotentialMove newPotentialMove;
        /// If it's a recurring turn we are called from RecurringTurnProcedure and this check already done.
        /*            if (!r_GameLogicUnit.IsRecurringTurn)
                    {
                        /// *** Check CurrPlayer's list of EatingMoves contain the DestIdx as a SrcIdx
                        r_GameLogicUnit.CurrentPlayerAnyEatingMovePossibilityCheck();
                    }

                   *//* LoadNewPotentialMoveProcedure();*//*
                    if (!r_ConsoleIOManager.RawMoveInputManager.QuitInserted)
                    {
                        MoveValidationProcedure();
                        r_GameLogicUnit.MoveManager.ExecuteMove(r_GameLogicUnit.Board, r_GameLogicUnit.CurrentPlayer);
        */
        // r_GameLogicUnit.PostMoveProcedure();
        /// r_ConsoleIOManager.RawMoveInputManager.LoadLastMoveToRawInput(r_GameLogicUnit.MoveManager.SrcIdx, r_GameLogicUnit.MoveManager.DestIdx);
        /// r_ConsoleIOManager.PrintBoard(r_GameLogicUnit.Board, r_GameLogicUnit.CurrentPlayer.PlayerType);
        /// r_ConsoleIOManager.PrintLastMoveByRawInput(r_ConsoleIOManager.RawMoveInputManager.RawInput, r_GameLogicUnit.CurrentPlayer);
        ///RecurringTurnProcedure();
        //// }
        ///}
        /*
                public void MoveValidationProcedure()
                {
                    bool validMove;

                    if (r_GameLogicUnit.IsRecurringTurn)
                    {
                        validMove = r_GameLogicUnit.MoveManager.RecurringTurnMoveValidation(r_GameLogicUnit.Board, r_GameLogicUnit.CurrentPlayer);
                        while (!validMove)
                        {
                            r_ConsoleIOManager.PrintInvalidInputMoveOption(r_GameLogicUnit.CurrentPlayer.PlayerType);
                            LoadNewPotentialMoveProcedure();
                            validMove = r_GameLogicUnit.MoveManager.RecurringTurnMoveValidation(r_GameLogicUnit.Board, r_GameLogicUnit.CurrentPlayer);
                        }
                    }

                    else /// Regular turn.
                    {
                        validMove = r_GameLogicUnit.MoveManager.MoveValidation(r_GameLogicUnit.Board, r_GameLogicUnit.CurrentPlayer);
                        while (!validMove)
                        {
                            r_ConsoleIOManager.PrintInvalidInputMoveOption(r_GameLogicUnit.CurrentPlayer.PlayerType);
                            LoadNewPotentialMoveProcedure();
                            validMove = r_GameLogicUnit.MoveManager.MoveValidation(r_GameLogicUnit.Board, r_GameLogicUnit.CurrentPlayer);
                        }
                    }
                }*/

        /* private void GameInitialization()
         {
             /// 
             r_ConsoleIOManager.GetGameDetailsProcedure(r_GameDetails);
             r_GameLogicUnit.GameMode = r_GameDetails.GameMode;
             /// *** Add Initialize to Both players PotentialMoves Lists
             r_GameLogicUnit.SetGameObjects(r_GameDetails.FirstPlayerName, r_GameDetails.SecondPlayerName, r_GameDetails.BoardSize);
         }*/

        /*

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

              } 
              while (playAnotherRound); // while should be in proceeding line!

              r_GameLogicUnit.SaveFinalCheckersGameResult();
              r_ConsoleIOManager.PrintAllGameSessionsResult(r_GameLogicUnit.FinalCheckersSessionResult, r_GameLogicUnit.FirstPlayer, r_GameLogicUnit.SecondPlayer);
              r_ConsoleIOManager.Goodbye();
          }

          private void ResetBetweenSessionsProcedure()
          {
              r_ConsoleIOManager.RawMoveInputManager.QuitInserted = false;
              r_GameLogicUnit.ResetObjectsBetweenSessions();
          }

          private void GameInitialization()
          {
              r_ConsoleIOManager.GetGameDetailsProcedure(r_GameDetails);
              r_GameLogicUnit.GameMode = r_GameDetails.GameMode;
              /// *** Add Initialize to Both players PotentialMoves Lists
              r_GameLogicUnit.SetGameObjects(r_GameDetails.FirstPlayerName, r_GameDetails.SecondPlayerName, r_GameDetails.BoardSize);
          }

          private void RunSingleGameSession()
          {
              r_GameLogicUnit.TurnsSetup();
              r_ConsoleIOManager.PrintBoard(r_GameLogicUnit.Board, Player.ePlayerType.Human);
              do
              {
                  r_GameLogicUnit.SwitchTurn();
                  r_ConsoleIOManager.PrintWhoseTurn(r_GameLogicUnit.CurrentPlayer);
                  CurrentPlayerTurnProcedure();
              } while (!r_GameLogicUnit.GameOver(r_ConsoleIOManager.RawMoveInputManager.QuitInserted)); // while should be in proceeding line!


              r_GameLogicUnit.ScoreCalculationAndUpdate();
              r_ConsoleIOManager.PrintSingleGameResult(r_GameLogicUnit.SingleGameResult, r_GameLogicUnit.FirstPlayer, r_GameLogicUnit.SecondPlayer);
          }

          private void LoadNewPotentialMoveProcedure()
          {
              PotentialMove newPotentialMove;
              if (r_GameLogicUnit.CurrentPlayer.PlayerType == Player.ePlayerType.Human)
              {
                  /// getANewMoveByInput
                  /// newPotentialMove = r_ConsoleIOManager.GetMoveInput();
                  /// Load to MoveManager the newPotentialMove
                  /// r_Game.MoveManager.LoadNewPotentialMove(newPotentialMove);
                  newPotentialMove = r_ConsoleIOManager.GetMoveInput();
                  /// r_Game.LoadSpecificNewPotentialMove(r_ConsoleIOManager.RawMoveInputManager.SourceIndex, r_ConsoleIOManager.RawMoveInputManager.DestinationIndex);
                  r_GameLogicUnit.LoadSpecificNewPotentialMove(newPotentialMove);
              }
              else /// (i_CurrentPlayer.PlayerType == Player.ePlayerType.Computer)
              {      
                  r_GameLogicUnit.GenerateAndLoadNewPotentialMove();
              }
          }

          private void CurrentPlayerTurnProcedure()
          {
              /// PotentialMove newPotentialMove;
              /// If it's a recurring turn we are called from RecurringTurnProcedure and this check already done.
              if (!r_GameLogicUnit.IsRecurringTurn)
              {
                  /// *** Check CurrPlayer's list of EatingMoves contain the DestIdx as a SrcIdx
                  r_GameLogicUnit.CurrentPlayerAnyEatingMovePossibilityCheck();
              }

             LoadNewPotentialMoveProcedure();
              if (!r_ConsoleIOManager.RawMoveInputManager.QuitInserted)
              {
                  MoveValidationProcedure();
                  r_GameLogicUnit.MoveManager.ExecuteMove(r_GameLogicUnit.Board, r_GameLogicUnit.CurrentPlayer);
                  
                  r_GameLogicUnit.PostMoveProcedure();
                  r_ConsoleIOManager.RawMoveInputManager.LoadLastMoveToRawInput(r_GameLogicUnit.MoveManager.SrcIdx, r_GameLogicUnit.MoveManager.DestIdx);
                  r_ConsoleIOManager.PrintBoard(r_GameLogicUnit.Board, r_GameLogicUnit.CurrentPlayer.PlayerType);
                  r_ConsoleIOManager.PrintLastMoveByRawInput(r_ConsoleIOManager.RawMoveInputManager.RawInput, r_GameLogicUnit.CurrentPlayer);
                  RecurringTurnProcedure();
              }
          }

         

         

        
      }*/
    }
}
