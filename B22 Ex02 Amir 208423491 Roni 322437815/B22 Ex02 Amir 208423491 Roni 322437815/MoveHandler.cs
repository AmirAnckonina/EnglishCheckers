using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B22_Ex02_Amir_208423491_Roni_322437815
{
    public class MoveHandler
    {
        public bool SrcAndDestBasicValidation(ref Board i_Board, eDiscType i_CurrPlayerDiscType, ref int[] i_SourceIndex, ref int[] i_DestinationIndex)
        {
            bool srcAndDestBasicallyValid;
            bool sourceIsValid;
            bool destinationIsVacant;
            bool indiciesInBoard;

            sourceIsValid = SourceValidation(ref i_Board, i_CurrPlayerDiscType, ref i_SourceIndex);
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

            CurrDestinationDiscType = i_Board.GetSquare(i_DestinationIndex[0], i_DestinationIndex[1]).CurrDiscType;
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

        public bool SourceValidation(ref Board i_Board, eDiscType i_CurrPlayerDiscType, ref int[] i_SourceIndex)
        {
            bool sourceIsValid;

            if (i_CurrPlayerDiscType == i_Board.GetSquare(i_SourceIndex[0], i_SourceIndex[1]).CurrDiscType())
            {
                sourceIsValid = true;
            }

            else
            {
                sourceIsValid = false;
            }

            return sourceIsValid;
        }

        public bool 
    }
}
