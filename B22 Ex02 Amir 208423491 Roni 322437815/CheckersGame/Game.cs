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
        MoveManager m_MoveManager;
        private eGameMode m_GameMode; // Consider if necessary ??
        private bool m_FirstPlayerTurn;

        public Game()
        {
            m_GameMode = eGameMode.SinglePlayerMode;
            m_FirstPlayerTurn = true;

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

        public MoveManager MoveManager
        {
            get
            {
                return m_MoveManager;
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

        public void SetBoard(int i_BoardSize)
        {
            m_Board.BoardSize = i_BoardSize;
            m_Board.InitializeBoard();
        }
        public void SetGamePlayers(StringBuilder i_FirstPlayerName, StringBuilder i_SecondPlayerName)
        {
            m_FirstPlayer.Name = i_FirstPlayerName;
            m_SecondPlayer.Name = i_SecondPlayerName;
            m_FirstPlayer.PlayerRecognition = ePlayerRecognition.FirstPlayer;
            m_SecondPlayer.PlayerRecognition = ePlayerRecognition.SecondPlayer;
            m_FirstPlayer.DiscType = eDiscType.ODisc;
            m_SecondPlayer.DiscType = eDiscType.XDisc;
            m_FirstPlayer.KingDiscType = eDiscType.OKingDisc;
            m_SecondPlayer.KingDiscType = eDiscType.XKingDisc;
            m_FirstPlayer.MovingDirection = ePlayerMovingDirection.Down;
            m_SecondPlayer.MovingDirection = ePlayerMovingDirection.Up;
            m_FirstPlayer.NumOfDiscs = m_Board.GetDiscOccurences(m_FirstPlayer.DiscType);
            m_SecondPlayer.NumOfDiscs = m_Board.GetDiscOccurences(m_SecondPlayer.DiscType);
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

        /// <------------------------------------------------------>
        /// Old design code below!
        /// <------------------------------------------------------>


        public void LoadNewPotentialMove(SquareIndex i_SourceIndex, SquareIndex i_DestinationIndex)
        {
            m_MoveManager.SourceIndex = i_SourceIndex;
            m_MoveManager.DestinationIndex = i_DestinationIndex;
        }

        public void GenerateRandomPotentialMove()
        {
            /// generate for computer player a valid source index for potential move.
            /// 
        }
        public void MoveProcedure()
        {
          /*  bool validMove;

            validMove = m_Move.MoveValidation(m_Board, i_CurrPlayer, m_UI.Input.SrcIndex, m_UI.Input.DestIndex);
            while (!validMove)
            {
                // m_UI.PrintInvalidInputMoveOption();
                validMove = m_Move.MoveValidation(m_Board, i_CurrPlayer, m_UI.Input.SrcIndex, m_UI.Input.DestIndex);
            }

            //Update movement on board.
            m_Move.ExecuteMove(m_Board, i_CurrPlayer, m_UI.Input.SrcIndex, m_UI.Input.DestIndex);*/
        }

        public void RawInputProcedure()
        {
            /*m_UI.Input.LoadNewInput();

            while (!m_UI.Input.InputStructureIsValid)
            {
                m_UI.PrintInvalidInputStructure();
                m_UI.Input.LoadNewInput();
            }*/
        }

        public void SwitchTurn()
        {
            if (m_CurrentPlayer == FirstPlayer)
            {
                m_FirstPlayerTurn = false;
                m_CurrentPlayer = m_SecondPlayer;
            }

            else //Currently, it's the second player turn
            {
                m_FirstPlayerTurn = true;
                m_CurrentPlayer = m_FirstPlayer;
            }

        }

        public bool GameOver()
        {
            bool gameOver = true;

            return gameOver;
        }

    }

}

