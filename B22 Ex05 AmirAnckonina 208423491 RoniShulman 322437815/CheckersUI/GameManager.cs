using System;
using System.Text;
using CheckersGame;
using System.Drawing;
using System.Collections.Generic;

namespace CheckersUI
{
    public class GameManager
    {
        private readonly GameLogic r_GameLogicUnit;
        private readonly FormGame r_FormGame;

        public GameManager()
        {
            r_GameLogicUnit = new GameLogic();
            r_FormGame = new FormGame();
        }

        public void Run()
        {
            RegisterFormEvents();
            RegisterLogicEvents();
            r_FormGame.ShowDialog();
        }

        private void RegisterFormEvents()
        {
            r_FormGame.GameDetailsFilled += r_FormGame_GameDetailsFilled;
            r_FormGame.PotentialMoveEntered += r_FormGame_PotentialMoveEntered;
            r_FormGame.PlayAnotherGameAnswered += r_FormGame_PlayAnotherGameAnswered;
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

        private void r_FormGame_PlayAnotherGameAnswered(object sender, EventArgs e)
        {
            PlayAnotherGameAnsweredEventArgs playAnotherGameAnsweredParams = e as PlayAnotherGameAnsweredEventArgs;

            if (playAnotherGameAnsweredParams != null)
            {
                if (playAnotherGameAnsweredParams.PlayAnotherGame == true)
                {
                    r_FormGame.ResetPicBoxSqrMatrix();
                    r_GameLogicUnit.ResetObjectsBetweenSessions();
                }

                else
                {
                    r_FormGame.Close();
                }
            }
        }

        private void r_GameLogicUnit_CurrPlayerReachedLastLine(object sender, EventArgs e)
        {
            ReachedLastLineEventArgs reachedLastLineParams = e as ReachedLastLineEventArgs;

            if (reachedLastLineParams != null)
            {
                r_FormGame.UpdateSpecificPicBoxToKingDisc(reachedLastLineParams.LastLineSquare);
            }
        }

        private void r_GameLogicUnit_MoveExecuted(object sender, EventArgs e)
        {
            MoveExecutedEventArgs moveExecutedParams = e as MoveExecutedEventArgs;

            if (moveExecutedParams != null)
            { 
                r_FormGame.PostGameLogicMoveUpdatePicBoxSqrMatrix(
                    moveExecutedParams.NewOccuipiedSquares,
                    moveExecutedParams.NewEmptySquares
                    );
            }
        }

        private void r_GameLogicUnit_InvalidMoveInserted(object sender, EventArgs e)
        {
            r_FormGame.ShowInvalidMoveMessage();
        }

        private void r_GameLogicUnit_TurnSwitched(object sender, EventArgs e)
        {
            GameLogic gameLogicUnitObj = sender as GameLogic;

            if (gameLogicUnitObj != null)
            {
                r_FormGame.CurrentPlayerRecognition = gameLogicUnitObj.CurrentPlayer.PlayerRecognition;
                r_FormGame.MarkCurrentPlayerLabel();
            }

            if (r_GameLogicUnit.CurrentPlayer.PlayerType == Player.ePlayerType.Computer)
            {
                r_GameLogicUnit.MoveProcedure();
            }
        }

        private void r_FormGame_PotentialMoveEntered(object sender, EventArgs e)
        {
            MovementEventArgs movementParams = e as MovementEventArgs;

            if (movementParams != null)
            {
                r_GameLogicUnit.MoveProcedure(movementParams.Movement);
            }
        }

        private void r_FormGame_GameDetailsFilled(object sender, EventArgs e)
        {
            GameDetailsFilledEventArgs gameDetails = e as GameDetailsFilledEventArgs;

            r_GameLogicUnit.SetGameObjects(
                gameDetails.Player1Name,
                gameDetails.Player2Name,
                gameDetails.BoardSize,
                gameDetails.Player2IsHuman
                );
        }

        private void r_GameLogicUnit_SingleGameInitialized(object sender, EventArgs e)
        {
            GameInitializedEventArgs gameInitializedParams = e as GameInitializedEventArgs;
            GameLogic gameLogicUnitObj = sender as GameLogic;

            if (gameLogicUnitObj != null && gameInitializedParams != null)
            {
                r_FormGame.UpdatePlayersLabelScore(gameLogicUnitObj.FirstPlayer.Score, gameLogicUnitObj.SecondPlayer.Score);
                r_FormGame.AddDiscsToPictureBoxSquareMatrix(gameInitializedParams.OcciupiedPoints);
                r_FormGame.CurrentPlayerRecognition = gameLogicUnitObj.CurrentPlayer.PlayerRecognition;
                r_FormGame.MarkCurrentPlayerLabel();
            }
        }

        private void r_GameLogicUnit_SingleGameOver(object sender, EventArgs e)
        {
            GameOverEventArgs gameOverParams = e as GameOverEventArgs;

            if (gameOverParams != null)
            {
                r_FormGame.CreateYesNoMessageBox(gameOverParams.GameResultMessage);
            }
        }
    }
}
