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
        private eGameMode m_GameMode; // Consider if necessary ??
        private eGameResult m_GameResult;
        private bool m_IsRecurringTurn;
        /// private bool m_FirstPlayerTurn;

        public Game()
        {
            //m_Board = new Board();
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

        public eGameResult GameResult
        {
            get
            {
                return m_GameResult;
            }

            set
            {
                m_GameResult = value;
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
            m_FirstPlayer.PlayerType = ePlayerType.Human; //Should be Human, Justfor testing  Computer 
            if (m_GameMode == eGameMode.TwoPlayersMode)
            {
                m_FirstPlayer.PlayerType = ePlayerType.Human;
            }

            else // (m_GameMode == eGameMode.SinglePlayerMode)
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
            if (m_IsRecurringTurn)
            {
                /// We should add HERE a condition If it's a recurring turn.
            }

            else
            {
                bool isValidMove;
                int generatedIndexFromList;
                var random = new Random();
                SquareIndex currentSquareIndex = new SquareIndex();
                List<SquareIndex> tempHoldingSquareIndices = new List<SquareIndex>(m_CurrentPlayer.CurrentHoldingSquareIndices);

                generatedIndexFromList = random.Next(tempHoldingSquareIndices.Count);
                currentSquareIndex = tempHoldingSquareIndices[generatedIndexFromList];
                isValidMove = m_MoveManager.AnyMovePossibilityCheck(currentSquareIndex, m_Board, m_CurrentPlayer);
                while (!isValidMove)
                { 
                    tempHoldingSquareIndices.Remove(currentSquareIndex);
                    generatedIndexFromList = random.Next(tempHoldingSquareIndices.Count);
                    currentSquareIndex = tempHoldingSquareIndices[generatedIndexFromList];
                    isValidMove = m_MoveManager.AnyMovePossibilityCheck(currentSquareIndex, m_Board, m_CurrentPlayer);
                }
            }
        } 

       /* public void SingleRandomGenerationAndValidation(List<SquareIndex> i_TempHoldingSquareIndices, SquareIndex i_CurrentSquareIndex, int generatedIndexFromList,Random random, bool i_IsValidMove)
        { 
            generatedIndexFromList = random.Next(i_TempHoldingSquareIndices.Count);
            i_CurrentSquareIndex = i_TempHoldingSquareIndices[generatedIndexFromList];
            /// i_IsValidMove = MoveIsValid(randomSquareIndex);
        }*/

        public void PostMoveProcedure()
        {
            /// Has recachedLastLine
            m_MoveManager.ReachedLastLineValidationAndUpdate(m_Board, m_CurrentPlayer);
            /// Reduce numOfdiscs of rival
            if (m_MoveManager.EatingMoveOccurred())
            {
                m_RivalPlayer.NumOfDiscs--;
                m_RivalPlayer.RemoveIndexFromCurrentHoldingSquareIndices(m_MoveManager.EatedSquareIndex);
            }
            /// Update the new DestinationIndex to be on CurrentHoldingIndices + Remove the SourceIndex from the CurrentHoldingIndices.
            m_CurrentPlayer.UpdateCurrentHoldingSquareIndices(m_MoveManager.SourceIndex, m_MoveManager.DestinationIndex);
        }

        public bool RecurringTurnPossibilityValidation() //organize this function. using local booleans.
        {
            bool recurringTurnIsPossible;

            if (m_MoveManager.EatingMoveOccurred() && m_MoveManager.RecurringTurnPossibiltyCheck(m_Board, m_CurrentPlayer))
            {
                /// Update newSourceIndex, So later when we check the input it has to be equal.
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

        public void SwitchTurn()
        {
            //if (m_CurrentPlayer.Equals(FirstPlayer)) /// ReferenceEquals(FirstPlayer, CurrentPlayer)
            if (FirstPlayer == CurrentPlayer) // CHECK!!
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

            if (i_IsQPressed || (m_RivalPlayer.NumOfDiscs == 0)) 
            {
                isGameOver = true;
            }

            else if(!PlayerMovementPossibilityCheck(m_RivalPlayer))
            { 
                isGameOver = true;
                if(!PlayerMovementPossibilityCheck(m_CurrentPlayer))
                {
                    m_GameResult = eGameResult.Draw;
                }

               
            }

            else
            {
                isGameOver = false;
            }

            return isGameOver;
        }

        public void UpdateTheGameResult()
        {
            if (m_CurrentPlayer.PlayerRecognition == ePlayerRecognition.FirstPlayer)
            {
                m_GameResult = eGameResult.FirstPlayerWon;
            }

            else
            {
                m_GameResult = eGameResult.SecondPlayerWon;
            }
        }

        public bool PlayerMovementPossibilityCheck(Player i_Player)
        {
            bool rivalPlayerAbleToMove; 
            /// SquareIndex currSquareIndex;
            /// int indexInList;
            /// int indicesListSize = m_RivalPlayer.GetCurrentHoldingSqaureIndicesListLength();
            /// int indicesListSize = (m_RivalPlayer.CurrentHoldingSquareIndices).Count;

            /// Set first to false so if "true" will be returned from a single SquareIndex check,
            ///we will go out from the loop
            rivalPlayerAbleToMove = false; 
            /*for (indexInList = 0; indexInList < indicesListSize && !rivalPlayerAbleToMove ; indexInList++)
            {
                currSquareIndex = m_RivalPlayer.CurrentHoldingSquareIndices[indexInList];
                rivalPlayerAbleToMove = m_MoveManager.AnyMovePossibilityCheck(currSquareIndex, m_Board, m_RivalPlayer);
            }*/


            foreach (SquareIndex currSquareIndex in m_RivalPlayer.CurrentHoldingSquareIndices)
            {
                rivalPlayerAbleToMove = m_MoveManager.AnyMovePossibilityCheck(currSquareIndex, m_Board, m_RivalPlayer);
                if (rivalPlayerAbleToMove)
                {
                    break;
                }
            }

            return rivalPlayerAbleToMove;
        }

        public int ScoreCalculator()
        {
            int scoreCalculate;
            
            if(m_GameResult == eGameResult.FirstPlayerWon)
            {
                scoreCalculate = FirstPlayer.NumOfDiscs - SecondPlayer.NumOfDiscs;
            }

            else if(m_GameResult == eGameResult.SecondPlayerWon)
            {
                scoreCalculate = SecondPlayer.NumOfDiscs - FirstPlayer.NumOfDiscs;
            }

            else ///the game result is : Draw
            {
                scoreCalculate = 0;
            }

            return scoreCalculate;
        }
    }

}

