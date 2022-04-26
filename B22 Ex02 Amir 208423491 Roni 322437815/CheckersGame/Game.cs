using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersGame
{
    public class Game
    {
        private Board m_Board;
        private Player m_FirstPlayer;
        private Player m_SecondPlayer;
        private Player m_CurrentPlayer;
        private Player m_RivalPlayer;
        MoveManager m_MoveManager;
        private eGameMode m_GameMode;
        private eGameResult m_SingleGameResult;
        private eGameResult m_FinalCheckersSessionResult;
        private bool m_IsRecurringTurn;

        public Game()
        {
            m_FirstPlayer = new Player();
            m_SecondPlayer = new Player();
            m_MoveManager = new MoveManager();
            m_GameMode = eGameMode.SinglePlayerMode;
        }

        public MoveManager MoveManager
        {
            get
            {
                return m_MoveManager;
            }
        }

        public Player CurrentPlayer
        {
            get
            {
                return m_CurrentPlayer;
            }

            set
            {
                m_CurrentPlayer = value;
            }
        }

        public Player RivalPlayer
        {
            get
            {
                return m_RivalPlayer;
            }

            set
            {
                m_RivalPlayer = value;
            }
        }

        public Player FirstPlayer
        {
            get
            {
                return m_FirstPlayer;
            }

            set
            {
                m_FirstPlayer = value;
            }
        }

        public Player SecondPlayer
        {
            get
            {
                return m_SecondPlayer;
            }

            set
            {
                m_SecondPlayer = value;
            }
        }

        public Board Board
        {
            get
            {
                return m_Board;
            }

            set
            {
                m_Board = value;
            }
        }

        public eGameMode GameMode
        {
            get
            {
                return m_GameMode;
            }

            set
            {
                m_GameMode = value;
            }
        }

        public eGameResult SingleGameResult
        {
            get
            {
                return m_SingleGameResult;
            }

            set
            {
                m_SingleGameResult = value;
            }
        }

        public eGameResult FinalCheckersSessionResult
        {
            get
            {
                return m_FinalCheckersSessionResult;
            }

            set
            {
                m_FinalCheckersSessionResult = value;
            }

        }

        public bool IsRecurringTurn
        {
            get
            {
                return m_IsRecurringTurn;
            }

            set
            {
                m_IsRecurringTurn = value;
            }
        }

        public void SetBoard(int i_BoardSize)
        {
            m_Board = new Board(i_BoardSize);
            m_Board.InitializeBoard();
        }

        public void SetGamePlayers(StringBuilder i_FirstPlayerName, StringBuilder i_SecondPlayerName)
        {
            m_FirstPlayer.Name = i_FirstPlayerName;
            m_SecondPlayer.Name = i_SecondPlayerName;
            m_FirstPlayer.PlayerRecognition = ePlayerRecognition.FirstPlayer;
            m_SecondPlayer.PlayerRecognition = ePlayerRecognition.SecondPlayer;
            m_FirstPlayer.DiscType = eDiscType.XDisc;
            m_SecondPlayer.DiscType = eDiscType.ODisc;
            m_FirstPlayer.KingDiscType = eDiscType.XKingDisc;
            m_SecondPlayer.KingDiscType = eDiscType.OKingDisc;
            m_FirstPlayer.MovingDirection = ePlayerMovingDirection.Down;
            m_SecondPlayer.MovingDirection = ePlayerMovingDirection.Up;
            m_FirstPlayer.NumOfDiscs = m_Board.GetDiscOccurences(m_FirstPlayer.DiscType);
            m_SecondPlayer.NumOfDiscs = m_Board.GetDiscOccurences(m_SecondPlayer.DiscType);
            m_FirstPlayer.InitializeCurrentHoldingIndices(m_Board);
            m_SecondPlayer.InitializeCurrentHoldingIndices(m_Board);
            m_FirstPlayer.PlayerType = ePlayerType.Computer;
            if (m_GameMode == eGameMode.TwoPlayersMode)
            {
                m_FirstPlayer.PlayerType = ePlayerType.Human;
            }

            else /// In case it's a Single Player Game Mode.
            {
                m_SecondPlayer.PlayerType = ePlayerType.Computer;
            }
        }

        public void LoadNewPotentialMove(SquareIndex i_SourceIndex, SquareIndex i_DestinationIndex)
        {
            m_MoveManager.SourceIndex = i_SourceIndex;
            m_MoveManager.DestinationIndex = i_DestinationIndex;
        }

        public void GenerateRandomPotentialMove()
        {
            bool isValidEatingMove;
            int generatedIndexFromList;
            var random = new Random();
            SquareIndex currentSquareIndex = new SquareIndex();
            List<SquareIndex> tempHoldingSquareIndices = new List<SquareIndex>(m_CurrentPlayer.CurrentHoldingSquareIndices);

            if (m_IsRecurringTurn)
            {
                /// immediately load the SquareIndex we reached in the last move.
                LoadNewPotentialMove(m_MoveManager.RecurringTurnNewSourceIndex, m_MoveManager.DestinationIndex);
            }

            else if(!CurrentPlayerAnyEatingMovePossibilityCheck())
            { 
                generatedIndexFromList = random.Next(tempHoldingSquareIndices.Count);
                currentSquareIndex = tempHoldingSquareIndices[generatedIndexFromList];
                isValidEatingMove = m_MoveManager.AnyMovePossibilityCheck(currentSquareIndex, m_Board, m_CurrentPlayer);
                while (!isValidEatingMove)
                {
                    tempHoldingSquareIndices.Remove(currentSquareIndex);
                    generatedIndexFromList = random.Next(tempHoldingSquareIndices.Count);
                    currentSquareIndex = tempHoldingSquareIndices[generatedIndexFromList];
                    isValidEatingMove = m_MoveManager.AnyMovePossibilityCheck(currentSquareIndex, m_Board, m_CurrentPlayer);
                }
            }

            else /// In case the player must eat:
            {
                /// the source and destination will be loaded automatically during the eatingMovePossibilitycheck:
                /// CurrentPlayerAnyEatingMovePossibilityCheck()
            }
        }

        public void PostMoveProcedure()
        {
            m_MoveManager.ReachedLastLineValidationAndUpdate(m_Board, m_CurrentPlayer);
            if (m_MoveManager.EatingMoveOccurred())
            {
                m_RivalPlayer.NumOfDiscs--;
                m_RivalPlayer.RemoveIndexFromCurrentHoldingSquareIndices(m_MoveManager.EatedSquareIndex);
            }
            m_CurrentPlayer.UpdateCurrentHoldingSquareIndices(m_MoveManager.SourceIndex, m_MoveManager.DestinationIndex);
        }

        public bool RecurringTurnPossibilityValidation() 
        {
            bool recurringTurnIsPossible;

            if (m_MoveManager.EatingMoveOccurred() && m_MoveManager.RecurringTurnPossibiltyCheck(m_Board, m_CurrentPlayer))
            {
                m_MoveManager.RecurringTurnNewSourceIndex.CopySquareIndices(m_MoveManager.SourceIndex);
                recurringTurnIsPossible = true;
                m_IsRecurringTurn = true;
            }

            else
            {
                recurringTurnIsPossible = false;
                m_IsRecurringTurn = false;
            }

            return recurringTurnIsPossible;
        }

        public void TurnsSetup()
        {
            m_CurrentPlayer = m_SecondPlayer; 
            m_RivalPlayer = m_FirstPlayer;
        }

        public void SwitchTurn()
        {
            if (FirstPlayer == CurrentPlayer) 
            {
                m_CurrentPlayer = m_SecondPlayer;
                m_RivalPlayer = m_FirstPlayer;
            }

            else //Currently, it's the second player turn
            {
                m_CurrentPlayer = m_FirstPlayer;
                m_RivalPlayer = m_SecondPlayer;
            }
        }

        public bool GameOver(bool i_IsQPressed)
        {
            bool isGameOver;

            if (i_IsQPressed) 
            {
                isGameOver = true;
                SaveSingleGameResult(m_RivalPlayer.PlayerRecognition);
            }

            else if (m_RivalPlayer.NumOfDiscs == 0)
            {
                isGameOver = true;
                SaveSingleGameResult(m_CurrentPlayer.PlayerRecognition);
            }

            else if(!PlayerMovementPossibilityCheck(m_RivalPlayer))
            { 
                isGameOver = true;
                if (!PlayerMovementPossibilityCheck(m_CurrentPlayer))
                {
                    SaveSingleGameResult(ePlayerRecognition.None);
                }

                else /// So the current player can keep moving
                {
                    SaveSingleGameResult(m_CurrentPlayer.PlayerRecognition);
                }
            }

            else
            {
                isGameOver = false;
            }

            return isGameOver;
        }

        public void SaveSingleGameResult(ePlayerRecognition i_WinnerPlayerRecognition)
        {
            if (i_WinnerPlayerRecognition == ePlayerRecognition.FirstPlayer)
            {
                m_SingleGameResult = eGameResult.FirstPlayerWon;
            }

            else if (i_WinnerPlayerRecognition == ePlayerRecognition.SecondPlayer)
            {
                m_SingleGameResult = eGameResult.SecondPlayerWon;
            }

            else /// None of the players won.
            {
                m_SingleGameResult = eGameResult.Draw;
            }
        }

        public bool PlayerMovementPossibilityCheck(Player i_Player)
        {
            bool playerAbleToMove; 

            /// Set first to false so if "true" will be returned from a single SquareIndex check, we will go out from the loop via break.
            playerAbleToMove = false; 
            foreach (SquareIndex currSquareIndex in i_Player.CurrentHoldingSquareIndices)
            {
                playerAbleToMove = m_MoveManager.AnyMovePossibilityCheck(currSquareIndex, m_Board, i_Player);
                if (playerAbleToMove)
                {
                    break;
                }
            }

            return playerAbleToMove;
        }

        public bool CurrentPlayerAnyEatingMovePossibilityCheck()
        {
            bool playerCanMakeEatingMove;

            /// Set first to false so if "true" will be returned from a single SquareIndex check, we will go out from the loop via break.
            playerCanMakeEatingMove = false;
            foreach (SquareIndex currSquareIndex in m_CurrentPlayer.CurrentHoldingSquareIndices)
            {
                m_MoveManager.SourceIndex.CopySquareIndices(currSquareIndex);
                playerCanMakeEatingMove = m_MoveManager.AnyEatingMovePossibiltyCheck(m_Board, m_CurrentPlayer);
                if (playerCanMakeEatingMove)
                {
                    m_MoveManager.OnlyEatingIsValid = true;
                    break;
                }
            }

            if (playerCanMakeEatingMove)
            {
                m_MoveManager.OnlyEatingIsValid = true;
            }

            else
            {
                m_MoveManager.OnlyEatingIsValid = false;
            }

            return playerCanMakeEatingMove;
        }

        public void CheckAndUpdateIfCurrentPlayerMustEat() /// Run from moveValidationProcedure -> indicesSave because input already loaded
        {
            bool currentPlayerMustEat;
            SquareIndex sourceIndexSaver;
            SquareIndex destinationIndexSaver;

            /// If playertype isn't computer.
            /// Because while we generate a movement for computer we check if eating option exist and then loading it directly so no duoblr validation needed
            if (m_CurrentPlayer.PlayerType == ePlayerType.Human)
            {
                sourceIndexSaver = new SquareIndex(m_MoveManager.SourceIndex);
                destinationIndexSaver = new SquareIndex(m_MoveManager.DestinationIndex);
                currentPlayerMustEat = CurrentPlayerAnyEatingMovePossibilityCheck();
                m_MoveManager.SourceIndex.CopySquareIndices(sourceIndexSaver);
                m_MoveManager.DestinationIndex.CopySquareIndices(destinationIndexSaver);
            }
        }

        public void ScoreCalculationAndUpdate()
        {
            int singleGameScore;
            int firstPlayerTotalDiscValues;
            int secondPlayerTotalDiscValues;

            firstPlayerTotalDiscValues = m_FirstPlayer.CalculatePlayerDiscValuesAfterSingleGame(m_Board);
            secondPlayerTotalDiscValues = m_SecondPlayer.CalculatePlayerDiscValuesAfterSingleGame(m_Board);
            singleGameScore = Math.Abs(firstPlayerTotalDiscValues - secondPlayerTotalDiscValues);
            if(m_SingleGameResult == eGameResult.FirstPlayerWon)
            {
                m_FirstPlayer.Score += singleGameScore;
            }

            else if(m_SingleGameResult == eGameResult.SecondPlayerWon)
            {
                m_SecondPlayer.Score += singleGameScore;
            }
        }

        public void ResetBetweenSessions()
        {
            FirstPlayer.NumOfDiscs = 0;
            SecondPlayer.NumOfDiscs = 0;
            FirstPlayer.CurrentHoldingSquareIndices.Clear();
            SecondPlayer.CurrentHoldingSquareIndices.Clear();
            m_Board.InitializeBoard();
            m_FirstPlayer.NumOfDiscs = m_Board.GetDiscOccurences(m_FirstPlayer.DiscType);
            m_SecondPlayer.NumOfDiscs = m_Board.GetDiscOccurences(m_SecondPlayer.DiscType);
            m_FirstPlayer.InitializeCurrentHoldingIndices(m_Board);
            m_SecondPlayer.InitializeCurrentHoldingIndices(m_Board);
        }

        public void SaveFinalCheckersGameResult()
        {
            if (m_FirstPlayer.Score > m_SecondPlayer.Score)
            {
                m_FinalCheckersSessionResult = eGameResult.FirstPlayerWon;
            }

            else if (m_SecondPlayer.Score > m_FirstPlayer.Score)
            {
                m_FinalCheckersSessionResult = eGameResult.SecondPlayerWon;
            }

            else 
            {
                m_FinalCheckersSessionResult = eGameResult.Draw;
            }
        }
    }

}

