using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B22_Ex02_Amir_208423491_Roni_322437815
{
    public class MoveManager
    {
        //MoveValidation Params:
        // 1. SquareIndex srcIndex
        // 2. SquareIndex DestIndex
        // 3. ref Board m_Board
        // 4. ref Player m_CurrPlayer

        public bool MoveValidation(ref Board i_Board, ref Player i_Player, SquareIndex i_SourceIndex, SquareIndex i_DestinationIndex)
        {
            bool moveIsValid;
            bool srcAndDestBasicallyValid;
            bool simpleStepIsValid; //No eating step.
            bool eatOpponentStepIsValid;

            // 1. SrcSquare contains the CurrPlayer DiscType && DstSquare is vacant and legal
            // 2. Case 1: If moveDirection is UP and dstRowInd + 1 = srcRowInd && 

            srcAndDestBasicallyValid = SrcAndDestBasicValidation(ref i_Board,i_Player.m_PlayerSerial ref i_SourceIndex, ref i_DestinationIndex);

            if (srcAndDestBasicallyValid)
            {

            }

            else
            {
                moveIsValid = false;
            }

            return moveIsValid;

        }

        public bool SrcAndDestBasicValidation(ref Board i_Board, ePlayer i_SquareHolder, SquareIndex i_srcIndex, SquareIndex i_destIndex)
        {
            bool srcAndDestBasicallyValid;
            bool sourceIsValid;
            bool destinationIsVacant;
            bool indiciesInBoard;

            sourceIsValid = SourceValidation(i_Board[i_srcIndex], i_SquareHolder);
            destinationIsVacant = DestinationVacancyAndLegalityValidation(ref i_Board, ref i_DestinationIndex);
            indiciesInBoard = IndiciesInBoardValidation(ref i_Board, ref i_SourceIndex, ref i_DestinationIndex);

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

        public bool IndiciesInBoardValidation(ref Board i_Board, ref int[] i_SourceIndex, ref int[] i_DestinationIndex)
        {
            bool indiciesInBoard;
            bool sourceIsExist;
            bool destinationIsExist;

            sourceIsExist = i_Board.SquareExistenceValidation(ref i_SourceIndex);
            destinationIsExist = i_Board.SquareExistenceValidation(ref i_DestinationIndex);

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

        public bool DestinationVacancyAndLegalityValidation(ref Board i_Board, ref int[] i_DestinationIndex)
        {
            bool destinationIsVacant;
            eDiscType CurrDestinationDiscType;
            bool indexIsLegal;

            CurrDestinationDiscType = i_Board.GetSquare(i_DestinationIndex[0], i_DestinationIndex[1]).DiscType;
            indexIsLegal = i_Board.GetSquare(i_DestinationIndex[0], i_DestinationIndex[1]).LegalSquare;

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

        public bool SourceValidation(Square i_CurrSqeuare, ePlayer i_SquareHolder)
        {
            bool sourceIsValid;

            if (i_SquareHolder == i_CurrSqeuare.m_SquareHolder)
            {
                sourceIsValid = true;
            }

            else
            {
                sourceIsValid = false;
            }

            return sourceIsValid;
        }

        public bool MoveFromOptionValiidation(ref Player i_CurrPlayer, ref Board i_Board)
        {
            return true;
        }


    }
}
