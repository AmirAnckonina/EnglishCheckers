using System;
using System.Text;
using CheckersGame;

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
        private readonly GameLogic r_GameUnit;
        private FormGame m_FormGame;

        public GameManager()
        {
            r_GameUnit = new GameLogic();
            m_FormGame = new FormGame();
        }

        public void Run()
        {
            RegisterLogicEvents();
            RegisterFormEvents();
            m_FormGame.ShowDialog();
        }

        private void RegisterFormEvents()
        {
            m_FormGame.GameDetailsFilled += m_FormGame_GameDetailsFilled;
            /// PotentialMoveEntered
            /// AfterSessionMessageBoxAnswered
        }

        private void m_FormGame_GameDetailsFilled(object sender, EventArgs e)
        {
            /// Send here the relevant params to SetGameObjects under GameLogic
            GameDetailsFilledEventArgs gameDetails = sender as GameDetailsFilledEventArgs;

            if (gameDetails != null)
            {
               /// r_GameUnit.SetGameObjects() 
            }
        }

        private void RegisterLogicEvents()
        {
            /// BoardUpdated / MoveExecuted
            r_GameUnit.MoveManager.MoveExecuted += MoveManager_MoveExecuted;
        }

        private void MoveManager_MoveExecuted(object sender, EventArgs e)
        {
            /// Tell the FormGame MoveExecuted So the Board diaplay should change
            /// SquareIndex to Remove 
            /// SquareIndex to Add
            /// PictureBox -> remove
            
        }

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

          /// private void GetAndLoadNewPotentialMove()
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
          }

          public void RecurringTurnProcedure()
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
          }
      }*/
    }
}
