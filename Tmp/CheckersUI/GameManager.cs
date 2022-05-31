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
        private FormGame m_FormGame;

        public GameManager()
        {
            r_GameLogicUnit = new GameLogic();
            m_FormGame = new FormGame();
        }

        public void Run()
        {
            RegisterFormEvents();
            RegisterLogicEvents();
            m_FormGame.ShowDialog();
        }

        private void RegisterFormEvents()
        {
            m_FormGame.GameDetailsFilled += m_FormGame_GameDetailsFilled;
            m_FormGame.PotentialMoveEntered += m_FormGame_PotentialMoveEntered;
            m_FormGame.PlayAnotherGameAnswered += m_FormGame_PlayAnotherGameAnswered;
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

        private void m_FormGame_PlayAnotherGameAnswered(object sender, EventArgs e)
        {
            PlayAnotherGameAnsweredEventArgs playAnotherGameAnsweredParams = e as PlayAnotherGameAnsweredEventArgs;

            if (playAnotherGameAnsweredParams != null)
            {
                if (playAnotherGameAnsweredParams.PlayAnotherGame == true)
                {
                    m_FormGame.ResetPicBoxSqrMatrix();
                    r_GameLogicUnit.ResetObjectsBetweenSessions();
                }
            }
        }

        private void r_GameLogicUnit_CurrPlayerReachedLastLine(object sender, EventArgs e)
        {
            ReachedLastLineEventArgs reachedLastLineParams = e as ReachedLastLineEventArgs;

            if (reachedLastLineParams != null)
            {
                m_FormGame.UpdateSpecificPicBoxToKingDisc(reachedLastLineParams.LastLineSquare);
            }
        }

        private void r_GameLogicUnit_MoveExecuted(object sender, EventArgs e)
        {
            MoveExecutedEventArgs moveExecutedParams = e as MoveExecutedEventArgs;

            if (moveExecutedParams != null)
            { 
                m_FormGame.PostGameLogicMoveUpdatePicBoxSqrMatrix(
                    moveExecutedParams.NewOccuipiedSquares,
                    moveExecutedParams.NewEmptySquares
                    );
            }
        }

        private void r_GameLogicUnit_InvalidMoveInserted(object sender, EventArgs e)
        {
            m_FormGame.ShowInvalidMoveMessage();
        }

        private void r_GameLogicUnit_TurnSwitched(object sender, EventArgs e)
        {
            GameLogic gameLogicUnitObj = sender as GameLogic;

            if (gameLogicUnitObj != null)
            {
                m_FormGame.CurrentPlayerRecognition = gameLogicUnitObj.CurrentPlayer.PlayerRecognition;
                m_FormGame.MarkCurrentPlayerLabel();
            }
        }

        private void m_FormGame_PotentialMoveEntered(object sender, EventArgs e)
        {
            MovementEventArgs movementParams = e as MovementEventArgs;

            if (movementParams != null)
            {
                r_GameLogicUnit.MoveProcedure(movementParams.Movement);
            }
        }

        private void m_FormGame_GameDetailsFilled(object sender, EventArgs e)
        {
            GameDetailsFilledEventArgs gameDetails = e as GameDetailsFilledEventArgs;

            r_GameLogicUnit.SetGameObjects(gameDetails.Player1Name, gameDetails.Player2Name, gameDetails.BoardSize, gameDetails.Player2IsHuman);
        }

        private void r_GameLogicUnit_SingleGameInitialized(object sender, EventArgs e)
        {
            GameInitializedEventArgs gameInitializedParams = e as GameInitializedEventArgs;
            GameLogic gameLogicUnitObj = sender as GameLogic;

            if (gameLogicUnitObj != null && gameInitializedParams != null)
            {
                m_FormGame.UpdatePlayersLabelScore(gameLogicUnitObj.FirstPlayer.Score, gameLogicUnitObj.SecondPlayer.Score);
                m_FormGame.AddDiscsToPictureBoxSquareMatrix(gameInitializedParams.OcciupiedPoints);
                m_FormGame.CurrentPlayerRecognition = gameLogicUnitObj.CurrentPlayer.PlayerRecognition;
                m_FormGame.MarkCurrentPlayerLabel();
            }
        }

        private void r_GameLogicUnit_SingleGameOver(object sender, EventArgs e)
        {
            GameOverEventArgs gameOverParams = e as GameOverEventArgs;

            if (gameOverParams != null)
            {
                m_FormGame.CreateYesNoMessageBox(gameOverParams.GameResultMessage);
                /// Update m_FormGame Lables
            }
        }
    }
}
