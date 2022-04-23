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
        private bool m_FirstPlayerTurn;
        private bool m_IsRecurringTurn;

        public Game()
        {
            //m_Board = new Board();
            m_FirstPlayer = new Player();
            m_SecondPlayer = new Player();
            m_MoveManager = new MoveManager();

            m_GameMode = eGameMode.SinglePlayerMode;
            m_FirstPlayerTurn = true;

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
            m_FirstPlayer.PlayerType = ePlayerType.Human;
            if (m_GameMode == eGameMode.TwoPlayersMode)
            {
                m_FirstPlayer.PlayerType = ePlayerType.Human;
            }

            else // (m_GameMode == eGameMode.SinglePlayerMode)
            {
                m_SecondPlayer.PlayerType = ePlayerType.Computer;
            }
        }

        public void LoadNewPotentialMove(SquareIndex i_SourceIndex = null, SquareIndex i_DestinationIndex = null)
        {
            if (m_CurrentPlayer.PlayerType == ePlayerType.Human)
            {
                m_MoveManager.SourceIndex = i_SourceIndex;
                m_MoveManager.DestinationIndex = i_DestinationIndex;
            }

            else //ComputerType
            {
                GenerateRandomPotentialMove();
            }
        }

        public void GenerateRandomPotentialMove()
        {
            /// generate for computer player a valid source index for potential move.
            /// If RecurringTurn -> Take the prev Destination and set it as a new source
            /// Note to separate if it's recurring turn or not. 

        }

        public void PostMoveProcedure()
        {
            /// Has recachedLastLine
            m_MoveManager.ReachedLastLineValidationAndUpdate(m_Board, m_CurrentPlayer);
            /// Reduce numOfdiscs of rival
            if (m_MoveManager.EatingMoveOccurred())
            {
                m_RivalPlayer.NumOfDiscs--;
            }
            /// Update the new DestinationIndex to be on CurrentHoldingIndices
            /// Remove the SourceIndex from the CurrentHoldingIndices.
            m_CurrentPlayer.UpdateCurrentHoldingSquareIndices(m_MoveManager.SourceIndex, m_MoveManager.DestinationIndex);
        }

        public bool RecurringTurnPossibilityValidation()
        {
            bool recurringTurnIsPossible;

            if (m_MoveManager.EatingMoveOccurred() && m_MoveManager.RecurringTurnEatingMovePossibilty(m_Board, m_CurrentPlayer))
            {
                /// IMPORTANT! -> Update here the m_NewSourceIndexPostEating variable under MoveManager.
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
                m_FirstPlayerTurn = false;
                m_CurrentPlayer = m_SecondPlayer;
                m_RivalPlayer = m_FirstPlayer;
            }

            else //Currently, it's the second player turn
            {
                m_FirstPlayerTurn = true;
                m_CurrentPlayer = m_FirstPlayer;
                m_RivalPlayer = m_SecondPlayer;
            }
        }

        public bool GameOver()
        {
            bool gameOver = false;

            return gameOver;
        }

    }

}

