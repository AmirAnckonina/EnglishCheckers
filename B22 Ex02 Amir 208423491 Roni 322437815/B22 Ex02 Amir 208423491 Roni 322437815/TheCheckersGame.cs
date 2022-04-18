using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B22_Ex02_Amir_208423491_Roni_322437815
{
    public class TheCheckersGame
    {
        private Board m_Board;
        private ConsoleUI m_GameMenu;
        private Player[] m_Players;
        InputManager m_Input;
        MoveManager m_Move;
        private eGameMode m_GameMode;
        private bool m_FirstPlayerTurn;

        /*public TheCheckersGame()
        {
            m_GameMode = eGameMode.SinglePlayerMode;
            m_FirstPlayerTurn = true;
        }*/

        public void RunSession()
        {
            InitSingleGame();
            RunSingleGame();
        }

        public void InitSingleGame()
        {
            m_GameMenu.Welcome();
            m_Players[0].Name = m_GameMenu.GetPlayerName();
            SetGameMode();
            SetBoard();
            SetPlayers();
        }

        public void SetBoard()
        {
            m_Board.BoardSize = m_GameMenu.GetSizeOfBoard();
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
                m_Players[1].Name = m_GameMenu.GetPlayerName();
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

            gameModeChoice = m_GameMenu.GetGameMode();
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
                m_GameMenu.PrintWhoseTurn(currPlayer);



                SwitchTurn(currPlayer);
            }
        }

       /* public void RunTwoPlayersMode()
        {
           
            bool validMove;

            while (!GameOver())
            { 

                RawInputProcedure(); //Finished with a valid format certainly.
                validMove = CheckCurrentPlayerMove(); //Get SquareIndex
                while (!validMove)
                {
                    RawInputProcedure(ref rawInput);
                    validMove = CheckCurrentPlayerMove(ref m_Input.GetSourceIndex(), ref m_Input.GetDestinationIndex());
                }

                ExecuteCurrentPlayerMove();

                SwitchTurn();

            }
        }
*/
        public void RawInputProcedure()
        {
            m_Input.LoadNewInput();

            while (!m_Input.InputStructureIsValid)
            {
                Console.WriteLine("Sorry, your input structure isn't valid.");
                m_Input.LoadNewInput();
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

       /* public bool CheckCurrentPlayerMove(ref int[] i_SourceIndex, ref int[] i_DestinationIndex)
        {
            bool validMove;

            if (m_FirstPlayerTurn)
            {
                validMove = m_Move.MoveValidation(ref m_Players[0], ref i_SourceIndex, ref i_DestinationIndex);
            }

            else
            {
                validMove = MoveValidation(ref m_Players[1], ref i_SourceIndex, ref i_DestinationIndex);
            }

            return validMove;

        }
        //MoveValidation Params:
        // 1. SquareIndex srcIndex
        // 2. SquareIndex DestIndex
        // 3. ref Board m_Board
        // 4. ref Player m_CurrPlayer



        *//*  public bool MoveValidation(ref Player i_CurrPlayer, SquareIndex i_SourceIndex, ref int[] i_DestinationIndex)
          {
              bool moveIsValid;
              bool srcAndDestBasicallyValid;
              bool simpleStepIsValid; //No eating step.
              bool eatOpponentStepIsValid;

              // 1. SrcSquare contains the CurrPlayer DiscType && DstSquare is vacant and legal
              // 2. Case 1: If moveDirection is UP and dstRowInd + 1 = srcRowInd && 

              srcAndDestBasicallyValid = SrcAndDestBasicValidation(i_CurrPlayer.DiscType, ref i_SourceIndex, ref i_DestinationIndex);

              if (srcAndDestBasicallyValid)
              {

              }

              else
              {
                  moveIsValid = false;
              }

              return moveIsValid;

          }*//*

        public bool SrcAndDestBasicValidation(eDiscType i_CurrPlayerDiscType, ref int[] i_SourceIndex, ref int[] i_DestinationIndex)
        {
            bool srcAndDestBasicallyValid;
            bool sourceIsValid;
            bool destinationIsVacant;
            bool indiciesInBoard;

            sourceIsValid = SourceValidation(i_CurrPlayerDiscType, ref i_SourceIndex);
            destinationIsVacant = DestinationVacancyAndLegalityValidation(ref i_DestinationIndex);
            indiciesInBoard = IndiciesInBoardValidation(ref i_SourceIndex, ref i_DestinationIndex);

            if (indiciesInBoard && sourceIsValid && destinationIsVacant)
            {
                srcAndDestBasicallyValid = true;
            }

            else
            {
                srcAndDestBasicallyValid = false;
            }

            return srcAndDestBasicallyValid;

        }

        public bool IndiciesInBoardValidation(ref int[] i_SourceIndex, ref int[] i_DestinationIndex)
        {
            bool indiciesInBoard;
            bool sourceIsExist;
            bool destinationIsExist;

            sourceIsExist = m_Board.SquareExistenceValidation(i_SourceIndex[0], i_SourceIndex[1]);
            destinationIsExist = m_Board.SquareExistenceValidation(i_DestinationIndex[0], i_DestinationIndex[1]);

            if (sourceIsExist && destinationIsExist)
            {
                indiciesInBoard = true;
            }

            else
            {
                indiciesInBoard = false;
            }

            return indiciesInBoard;
        }

        public bool DestinationVacancyAndLegalityValidation(ref int[] i_DestinationIndex)
        {
            bool destinationIsVacant;
            eDiscType CurrDestinationDiscType;
            bool indexIsLegal;

            CurrDestinationDiscType = m_Board.GetSquare(i_DestinationIndex[0], i_DestinationIndex[1]).CurrDiscType;
            indexIsLegal = m_Board.GetSquare(i_DestinationIndex[0], i_DestinationIndex[1]).LegalSquare;

            if (CurrDestinationDiscType == eDiscType.None && indexIsLegal)
            {
                destinationIsVacant = true;
            }

            else
            {
                destinationIsVacant = false;
            }

            return destinationIsVacant;
        }

        public bool SourceValidation(eDiscType i_CurrPlayerDiscType, ref int[] i_SourceIndex)
        {
            bool sourceIsValid;

            // if(i_CurrPlayerDiscType == m_Board.GetSquare(i_SourceIndex[0], i_SourceIndex[1]).CurrDiscType())
            if (i_CurrPlayerDiscType == m_Board[5, 5].CurrDiscType
            {
                sourceIsValid = true;
            }

            else
            {
                sourceIsValid = false;
            }

            return sourceIsValid;
        }*/

    }

}
