using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B22_Ex02_Amir_208423491_Roni_322437815
{
    public class TheCheckersGame
    {
        private Board m_Board;
        private ConsoleUI m_UI;
        private Player[] m_Players;
        //InputManager m_Input;
        MoveManager m_Move;
        private eGameMode m_GameMode;
        private bool m_FirstPlayerTurn;

        public TheCheckersGame()
        {
            m_GameMode = eGameMode.SinglePlayerMode;
            m_FirstPlayerTurn = true;

        }

        public void RunSession()
        {
            InitSingleGame();
            RunSingleGame();
        }

        public void InitSingleGame()
        {
            m_UI.Welcome();
            m_Players[0].Name = m_UI.GetPlayerName();
            SetGameMode();
            SetBoard();
            SetPlayers();
        }

        public void SetBoard()
        {
            m_Board.BoardSize = m_UI.GetSizeOfBoard();
            m_Board.InitializeBoard();
        }

        public void SetPlayers()
        {
            SetSecondPlayersName();
            SetPlayersType();
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
        }

        public void SetSecondPlayersName()
        {
            if (m_GameMode == eGameMode.TwoPlayersMode)
            {
                m_Players[1].Name = m_UI.GetPlayerName();
            }

            else // (m_GameMode == eGameMode.SinglePlayerMode)
            {
                //Validate the constructor to Player knows to leavr the name dMember as ""
            }
        } 

        public void SetPlayersType()
        {
            m_Players[0].PlayerType = ePlayerType.Human;
            if (m_GameMode == eGameMode.TwoPlayersMode)
            {
                m_Players[1].PlayerType = ePlayerType.Human;
            }

            else // (m_GameMode == eGameMode.SinglePlayerMode)
            {
                m_Players[1].PlayerType = ePlayerType.Computer;
            }
        }

        public void SetGameMode()
        {
            int gameModeChoice;

            gameModeChoice = m_UI.GetGameMode();
            if (gameModeChoice == 1)
            {
                m_GameMode = eGameMode.SinglePlayerMode;
            }

            else //gameModePick == 2 
            {
                m_GameMode = eGameMode.TwoPlayersMode;
            }
        }

        public void RunSingleGame()
        {
            Player currPlayer;

            currPlayer = m_Players[0];
            while (!GameOver())
            {
                m_UI.PrintWhoseTurn(currPlayer);
                RawInputProcedure();
                MoveProcedure(currPlayer);
                //
                //
                SwitchTurn(currPlayer);
            }
        }

        public void MoveProcedure(Player i_CurrPlayer)
        {
            bool validMove;

            validMove = m_Move.MoveValidation(m_Board, i_CurrPlayer, m_UI.Input.SrcIndex, m_UI.Input.DestIndex);
            while (!validMove)
            {
                m_UI.PrintInvalidInputMoveOption();
                validMove = m_Move.MoveValidation(m_Board, i_CurrPlayer, m_UI.Input.SrcIndex, m_UI.Input.DestIndex);
            }

            //Update movement on board.
            m_Move.ExecuteMove(m_Board, i_CurrPlayer, m_UI.Input.SrcIndex, m_UI.Input.DestIndex);
        }

        public void RawInputProcedure()
        {
            m_UI.Input.LoadNewInput();

            while (!m_UI.Input.InputStructureIsValid)
            {
                m_UI.PrintInvalidInputStructure();
                m_UI.Input.LoadNewInput();
            }
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

        public static bool GameOver()
        {
            bool gameOver = true;

            return gameOver;
        }

    }

}
