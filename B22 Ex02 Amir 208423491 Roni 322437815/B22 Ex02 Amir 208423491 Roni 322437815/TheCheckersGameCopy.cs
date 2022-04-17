using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B22_Ex02_Amir_208423491_Roni_322437815
{
    public class TheCheckersGameCopy
    {
        private Board m_Board;
        private GameMenu m_GameMenu;
        private Player[] m_Players;
        InputHandler m_Input;
        MoveHandler m_Move;
        private eGameMode m_GameMode;
        private eWinnerPlayer m_WinnerPlayer;
        private bool m_FirstPlayerTurn = true;

        public void RunSession()
        {
            InitSingleGame();
            RunSingleGame();
        }

        public void InitSingleGame()
        {
            int gameModePick = 0;

            //GameMenu -> run Welcome function
            //GameMenu -> ask for first player's name
            m_Players[0].Name.Append("name");
            //GameMenu -> ask for boardDimensions
            // m_Board.BoardSize = returnedValue from Menu
            m_Board.InitializeBoard();
            //gameModePick = //GameMenu -> ask whether to play against computer or another player
            SetGameMode(gameModePick);
            SetSecondPlayerProcedure(gameModePick);

            m_Players[0].DiscType = eDiscType.XDisc;
            m_Players[1].DiscType = eDiscType.ODisc;
            m_Players[0].NumOfDiscs = m_Board.GetDiscOccurences(m_Players[0].DiscType);
            m_Players[1].NumOfDiscs = m_Board.GetDiscOccurences(m_Players[1].DiscType);

        }

        public void SetGameMode(int i_GameModePick)
        {
            if (i_GameModePick == 1)
            {
                m_GameMode = eGameMode.SinglePlayerMode;
            }

            else //i_GameModePick == 2 
            {
                m_GameMode = eGameMode.TwoPlayersMode;
            }
        }

        public void SetSecondPlayerProcedure(int i_GameModePick)
        {
            if (i_GameModePick == 1)
            {
                m_Players[1].PlayerType = ePlayerType.Computer;
            }

            else //numOfPlayers == 2 // -> if so, ask for the second player name
            {
                m_Players[1].PlayerType = ePlayerType.Human;
                //GameMenu -> ask for the second player name
                m_Players[1].Name.Append("name2");
            }
        }

        public void RunSingleGame()
        {
            StringBuilder rawInput = new StringBuilder();

            if (m_GameMode == eGameMode.SinglePlayerMode)
            {
                //input.GetFromColIndex();

                //m_Board.
                //
            }

            else //if (m_GameMode == eGameMode.TwoPlayersMode)
            {
                RunTwoPlayersMode();
            }

        }

        public void RunTwoPlayersMode()
        {
            StringBuilder rawInput = new StringBuilder();
            bool validMove;

            while (!GameOver())
            {

                RawInputProcedure(ref rawInput); //Finished with a valid format certainly.
                validMove = CheckCurrentPlayerMove(ref m_Input.GetSourceIndex(), ref m_Input.GetDestinationIndex());
                while (!validMove)
                {
                    RawInputProcedure(ref rawInput);
                    validMove = CheckCurrentPlayerMove(ref m_Input.GetSourceIndex(), ref m_Input.GetDestinationIndex());
                }

                ExecuteCurrentPlayerMove();

                SwitchTurn();

            }
        }

        public bool CheckCurrentPlayerMove(ref int[] i_SourceIndex, ref int[] i_DestinationIndex)
        {
            bool validMove;

            if (m_FirstPlayerTurn)
            {
                validMove = MoveValidation(ref m_Players[0], ref i_SourceIndex, ref i_DestinationIndex);
            }

            else
            {
                validMove = MoveValidation(ref m_Players[1], ref i_SourceIndex, ref i_DestinationIndex);
            }

            return validMove;

        }

        public bool MoveValidation(ref Player i_CurrPlayer, ref int[] i_SourceIndex, ref int[] i_DestinationIndex)
        {
            bool moveIsValid;
            bool srcAndDestBasicallyValid;
            bool simpleStepIsValid; //No eating step.
            bool eatOpponentStepIsValid;

            // 1. SrcSquare contains the CurrPlayer DiscType && DstSquare is vacant and legal
            // 2. Case 1: If moveDirection is UP and dstRowInd + 1 = srcRowInd && 

            srcAndDestBasicallyValid = m_Move.SrcAndDestBasicValidation(ref m_Board, i_CurrPlayer.DiscType, ref i_SourceIndex, ref i_DestinationIndex);



            if (srcAndDestBasicallyValid)
            {

            }

            else
            {
                moveIsValid = false;
            }

            return moveIsValid;

        }

        public void RawInputProcedure(ref StringBuilder io_RawInput)
        {
            io_RawInput.Clear();
            io_RawInput.Append(Console.ReadLine());
            m_Input.LoadNewInput(io_RawInput);

            while (!m_Input.InputStructureIsValid())
            {
                Console.WriteLine("Sorry, your input structure isn't valid.");
                io_RawInput.Clear();
                io_RawInput.Append(Console.ReadLine());
                m_Input.LoadNewInput(io_RawInput);
            }
        }

        public void SwitchTurn()
        {
            if (m_FirstPlayerTurn)
            {
                m_FirstPlayerTurn = false;
            }

            else //Currently, it's the second player turn
            {
                m_FirstPlayerTurn = true;
            }

        }

        public bool GameOver()
        {
            bool isGameOver;

            if (m_FirstPlayerTurn && (m_Board.GetDiscOccurences(eDiscType.XDisc) == 0 || !isThereAnyValidMove(m_Players[0])))
            {
                isGameOver = true;
                m_WinnerPlayer = eWinnerPlayer.SecondPlayer;
            }

            else if (!m_FirstPlayerTurn && (m_Board.GetDiscOccurences(eDiscType.ODisc) == 0 || !isThereAnyValidMove(m_Players[1])))
            {
                isGameOver = true;
                m_WinnerPlayer = eWinnerPlayer.SecondPlayer;
            }

            else if (m_Board.GetDiscOccurences(eDiscType.XDisc) == m_Board.GetDiscOccurences(eDiscType.ODisc) || (!isThereAnyValidMove(m_Players[0]) && !isThereAnyValidMove(m_Players[1]))) 
            {
                isGameOver = true;
                m_WinnerPlayer = eWinnerPlayer.Draw;
            }

            // else if (someone quite the game ('Q'))

            else
            {
                isGameOver = false;
            }

            return isGameOver;
        }

        public bool isThereAnyValidMove(Player i_CurrPlayer)
        {
            bool isThereValidMove = true;



            return isThereValidMove;
        }

    }

}
