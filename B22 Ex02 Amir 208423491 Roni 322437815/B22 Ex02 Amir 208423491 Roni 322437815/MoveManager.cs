using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B22_Ex02_Amir_208423491_Roni_322437815
{

    public class MoveManager
    {
        private eMoveType m_MoveType;
        private bool m_ReachedLastLine;

        //MoveValidation Params:
        // 1. SquareIndex srcIndex
        // 2. SquareIndex DestIndex
        // 3. ref Board m_Board
        // 4. ref Player m_CurrPlayer

        public bool MoveValidation(Board i_Board, Player i_CurrPlayer, SquareIndex i_SourceIndex, SquareIndex i_DestinationIndex)
        {
            bool moveIsValid;
            bool srcAndDestBasicallyValid;
            bool basicMoveIsValid; //No eating step.
            bool eatingMoveIsValid;

            // 1. IndicesInBoard
            // 2. SrcSquare contains the CurrPlayer DiscType
            // 3. DstSquare is vacant and legal
            // 4. If moveDirection is UP and dstRowInd + 1 = srcRowInd && 

            srcAndDestBasicallyValid = SrcAndDestBasicValidation(i_Board, i_CurrPlayer, i_SourceIndex, i_DestinationIndex);

            if (srcAndDestBasicallyValid)
            {
                basicMoveIsValid = 
            }

            else
            {
                moveIsValid = false;
            }

            return moveIsValid;

        }

        public bool SrcAndDestBasicValidation(Board i_Board, Player i_CurrPlayer, SquareIndex i_SourceIndex, SquareIndex i_DestinationIndex)
        {
            bool srcAndDestBasicallyValid;
            bool sourceIsValid;
            bool destinationIsVacantAndLegal;
            bool indiciesInBoard;

            indiciesInBoard = IndiciesInBoardValidation(i_Board, i_SourceIndex, i_DestinationIndex);
            if (indiciesInBoard)
            {
                sourceIsValid = SourceValidation(i_Board[i_SourceIndex], i_CurrPlayer);
                destinationIsVacantAndLegal = DestinationVacancyAndLegalityValidation(i_Board[i_DestinationIndex]);
                if (sourceIsValid && destinationIsVacantAndLegal)
                {
                    srcAndDestBasicallyValid = true;
                }

                else
                {
                    srcAndDestBasicallyValid = false;
                }
            }

            else
            {
                srcAndDestBasicallyValid = false;
            }

            return srcAndDestBasicallyValid;

        }

        public bool IndiciesInBoardValidation(Board i_Board, SquareIndex i_SourceIndex, SquareIndex i_DestinationIndex)
        {
            bool indiciesInBoard;
            bool sourceIsExist;
            bool destinationIsExist;

            sourceIsExist = i_Board.SquareExistenceValidation(i_SourceIndex);
            destinationIsExist = i_Board.SquareExistenceValidation(i_DestinationIndex);

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

        public bool DestinationVacancyAndLegalityValidation(Square i_DestinationSquare)
        {
            bool destinationIsVacant;
            eDiscType currDestinationDiscType;
            bool indexIsLegal;

            currDestinationDiscType = i_DestinationSquare.DiscType;
            indexIsLegal = i_DestinationSquare.LegalSquare;

            if (currDestinationDiscType == eDiscType.None && indexIsLegal)
            {
                destinationIsVacant = true;
            }

            else
            {
                destinationIsVacant = false;
            }

            return destinationIsVacant;
        }

        public bool SourceValidation(Square i_SourceSquare, Player i_CurrPlayer)
        {
            bool sourceIsValid;

            if (i_CurrPlayer.PlayerRecognition == i_SourceSquare.SquareHolder)
            {
                sourceIsValid = true;
            }

            else
            {
                sourceIsValid = false;
            }

            return sourceIsValid;
        }

        public void ExecuteMove(Board io_Board, Player i_CurrPlayer, SquareIndex i_SourceIndex, SquareIndex i_DestinationIndex)
        {
            // We need:
            // 1. Update the destinationSquare DiscType of the source.
            // 2. Update the sourceSquare DiscType to None.
            // 3. If eating occured -> update also the square we passed above
            // 
            //
            io_Board[i_DestinationIndex].DiscType = io_Board[i_SourceIndex].DiscType; 
        }

       // public bool MoveFromOptionValiidation(Player i_CurrPlayer, Board i_Board)
        /*  {
              return true;
          }*/


    }
}
