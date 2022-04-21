using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersGame
{
    public class Game
    {
        private Board m_Board;
        private Player[] m_Players;
        MoveManager m_Move;
        private eGameMode m_GameMode; // Consider if necessary ??
        private bool m_FirstPlayerTurn;

        public Game()
        {
            m_GameMode = eGameMode.SinglePlayerMode;
            m_FirstPlayerTurn = true;

        }

        public Player FirstPlayer
        {
            get
            {
                return m_Players[0];
            }
            set
            {
                m_Players[0] = value;
            }
        }

        public Player SecondPlayer
        {
            get
            {
                return m_Players[1];
            }
            set
            {
                m_Players[1] = value;
            }
        }

       /* public void InitalizeGameObjects(CheckersUI.GameDetails i_GameDetails)
        {
            m_GameMode = i_GameDetails.GameMode;
            m_Board.BoardSize = i_GameDetails.BoardSize;
            m_Board.InitializeBoard();
            m_Players[0].Name = i_GameDetails.NameOfFirstPlayer;
            m_Players[1].Name = i_GameDetails.NameOfSecondPlayer;
            m_Players[0].PlayerRecognition = ePlayerRecognition.FirstPlayer;
            m_Players[1].PlayerRecognition = ePlayerRecognition.SecondPlayer;
            m_Players[0].DiscType = eDiscType.ODisc;
            m_Players[1].DiscType = eDiscType.XDisc;
            m_Players[0].KingDiscType = eDiscType.OKingDisc;
            m_Players[1].KingDiscType = eDiscType.XKingDisc;
            m_Players[0].MovingDirection = ePlayerMovingDirection.Down;
            m_Players[1].MovingDirection = ePlayerMovingDirection.Up;
            m_Players[0].NumOfDiscs = m_Board.GetDiscOccurences(m_Players[0].DiscType);
            m_Players[1].NumOfDiscs = m_Board.GetDiscOccurences(m_Players[1].DiscType);
            m_Players[0].PlayerType = ePlayerType.Human;
            if (m_GameMode == eGameMode.TwoPlayersMode)
            {
                m_Players[1].PlayerType = ePlayerType.Human;
            }

            else // (m_GameMode == eGameMode.SinglePlayerMode)
            {
                m_Players[1].PlayerType = ePlayerType.Computer;
            }

        }*/

        /// <------------------------------------------------------>
        /// Old design code below!
        /// <------------------------------------------------------>

        public void MoveProcedure(Player i_CurrPlayer)
        {
           /* bool validMove;

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

        public void SwitchTurn(Player io_CurrPlayer)
        {
            if (m_FirstPlayerTurn)
            {
                m_FirstPlayerTurn = false;
                io_CurrPlayer = m_Players[1];
            }

            else //Currently, it's the second player turn
            {
                m_FirstPlayerTurn = true;
                io_CurrPlayer = m_Players[0];
            }

        }

        public bool GameOver()
        {
            bool gameOver = true;

            return gameOver;
        }

    }

}

