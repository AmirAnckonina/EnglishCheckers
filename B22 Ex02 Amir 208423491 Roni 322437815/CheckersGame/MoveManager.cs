using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersGame
{

    public class MoveManager
    {
        private static readonly int InvalidIndicesDifferences = -1; // Check how to set it.
        private eMoveType m_MoveType;
        private bool m_ReachedLastLine;

        //MoveValidation Params:
        // 1. SquareIndex srcIndex
        // 2. SquareIndex DestIndex
        // 3. ref Board m_Board
        // 4. ref Player m_CurrPlayer

        public bool ReachedLastLine
        {
            get
            {
               return m_ReachedLastLine;
            }

            set
            {
                m_ReachedLastLine = value;
            }
        }

        public bool MoveValidation(Board i_Board, Player i_CurrPlayer, SquareIndex i_SourceIndex, SquareIndex i_DestinationIndex)
        {
            bool moveIsValid;
            bool srcAndDestBasicallyValid;
            bool simpleMoveIsValid; //No eating step.
            bool eatingMoveIsValid;
            bool indicesDifferencesAreValid;
            int indicesDifferences;

            // SrcAndDestValidation: 
            // 1. IndicesInBoard
            // 2. SrcSquare contains the CurrPlayer DiscType
            // 3. DstSquare is vacant and legal 
            // 4. indicesDiffernceAreValid

            //Then according to the indicesDifferences, check:
            // 5. SimpleMoveIsValid
            // 6. EatingMoveIsValid

            srcAndDestBasicallyValid = SrcAndDestBasicValidation(i_Board, i_CurrPlayer, i_SourceIndex, i_DestinationIndex);
            indicesDifferencesAreValid = IndicesDifferencesValidationAndSetup(i_SourceIndex, i_DestinationIndex, out indicesDifferences);
            if (srcAndDestBasicallyValid && indicesDifferencesAreValid)
            {
                switch (indicesDifferences) // Think how to use the boolean result above
                {
                    case 1:
                        simpleMoveIsValid = SimpleMoveValidation(i_Board, i_CurrPlayer, i_SourceIndex, i_DestinationIndex);
                        moveIsValid = simpleMoveIsValid;
                        break;

                    case 2:
                        eatingMoveIsValid = EatingMoveValidation(i_Board, i_CurrPlayer, i_SourceIndex, i_DestinationIndex);
                        moveIsValid = eatingMoveIsValid;
                        break;

                    //In case abs(rowIndicesDifference) isn't 1 or 2 , so the move isn't valid. 
                    default:
                        moveIsValid = false;
                        break;
                }
            }

            else
            {
                moveIsValid = false;
            }

            return moveIsValid;

        }

        public bool IndicesDifferencesValidationAndSetup(SquareIndex i_SourceIndex, SquareIndex i_DestinationIndex, out int o_IndicesDifference)
        {
            bool indicesDifferencesAreValid;
            int rowIndicesDifference;
            int columnIndicesDifference;

            rowIndicesDifference = CalculateRowIndicesDifference(i_SourceIndex, i_DestinationIndex);
            columnIndicesDifference = CalculateColumnIndicesDifference(i_SourceIndex, i_DestinationIndex);

            if ((rowIndicesDifference == columnIndicesDifference) && (rowIndicesDifference == 1 || rowIndicesDifference == 2))
            {
                indicesDifferencesAreValid = true;
                o_IndicesDifference = rowIndicesDifference;
            }

            else
            {
                indicesDifferencesAreValid = false;
                o_IndicesDifference = InvalidIndicesDifferences;
            }

            return indicesDifferencesAreValid;

        }

        public int CalculateRowIndicesDifference(SquareIndex i_SourceIndex, SquareIndex i_DestinationIndex)
        {
            int rowIndicesDifferenceResult;

            rowIndicesDifferenceResult = Math.Abs(i_SourceIndex.RowIndex - i_DestinationIndex.RowIndex);

            return rowIndicesDifferenceResult;
        }

        public int CalculateColumnIndicesDifference(SquareIndex i_SourceIndex, SquareIndex i_DestinationIndex)
        {
            int columnIndicesDifferenceResult;

            columnIndicesDifferenceResult = Math.Abs(i_SourceIndex.ColumnIndex - i_DestinationIndex.ColumnIndex);

            return columnIndicesDifferenceResult;
        }

        public bool SimpleMoveValidation(Board i_Board, Player i_CurrPlayer, SquareIndex i_SourceIndex, SquareIndex i_DestinationIndex)
        {
            bool simpleMoveIsValid;
            bool simpleMoveForwardIsValid;
            bool simpleMoveBackwardsIsValid;

            simpleMoveForwardIsValid = SimpleMoveForwardValidation(i_CurrPlayer, i_SourceIndex, i_DestinationIndex);
            if (simpleMoveForwardIsValid)
            {
                simpleMoveIsValid = true;
            }

            else if (i_Board[i_SourceIndex].DiscType == i_CurrPlayer.KingDiscType) //The sourceIndex contain a KingDiscType.
            {
                simpleMoveBackwardsIsValid = SimpleMoveBackwardsValidation(i_CurrPlayer, i_SourceIndex, i_DestinationIndex);
                simpleMoveIsValid = simpleMoveBackwardsIsValid;
            }

            else
            {
                simpleMoveIsValid = false;
            }

            return simpleMoveIsValid;

        }

        public bool SimpleMoveForwardValidation(Player i_CurrPlayer, SquareIndex i_SourceIndex, SquareIndex i_DestinationIndex)
        {
            bool simpleMoveForwardIsValid;

            if (i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Up)
            {
                simpleMoveForwardIsValid = SimpleNorthEastMoveValidation(i_SourceIndex, i_DestinationIndex) || SimpleNorthWestMoveValidation(i_SourceIndex, i_DestinationIndex);
            }

            else //(i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Down)
            {
                simpleMoveForwardIsValid = SimpleSouthEastMoveValidation(i_SourceIndex, i_DestinationIndex) || SimpleSouthWestMoveValidation(i_SourceIndex, i_DestinationIndex);
            }

            return simpleMoveForwardIsValid;

        }

        public bool SimpleMoveBackwardsValidation(Player i_CurrPlayer, SquareIndex i_SourceIndex, SquareIndex i_DestinationIndex)
        {
            bool simpleMoveBackwardsIsValid;

            if (i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Up)
            {
                simpleMoveBackwardsIsValid = SimpleSouthEastMoveValidation(i_SourceIndex, i_DestinationIndex) || SimpleSouthWestMoveValidation(i_SourceIndex, i_DestinationIndex);
            }

            else //(i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Down)
            {
                simpleMoveBackwardsIsValid = SimpleNorthEastMoveValidation(i_SourceIndex, i_DestinationIndex) || SimpleNorthWestMoveValidation(i_SourceIndex, i_DestinationIndex);
            }

            return simpleMoveBackwardsIsValid;

        }

        public bool SimpleNorthEastMoveValidation(SquareIndex i_SourceIndex, SquareIndex i_DestinationIndex)
        {
            bool simpleNorthEastMoveIsValid;

            if (i_SourceIndex.RowIndex - 1 == i_DestinationIndex.RowIndex && i_SourceIndex.ColumnIndex + 1 == i_DestinationIndex.ColumnIndex)
            {
                simpleNorthEastMoveIsValid = true;
            }

            else
            {
                simpleNorthEastMoveIsValid = false;
            }

            return simpleNorthEastMoveIsValid;

            //SquareIndex potentialNewSquareIndex; new SquareIndex(i_SourceIndex.RowIndex -1, i_SourceIndex.ColumnIndex + 1);
            //How to Use operator????
            /* if (potentialNewSquareIndex. == i_DestinationIndex)
             {

             }*/
        }

        public bool SimpleNorthWestMoveValidation(SquareIndex i_SourceIndex, SquareIndex i_DestinationIndex)
        {
            bool simpleNorthWestMoveIsValid;

            if (i_SourceIndex.RowIndex - 1 == i_DestinationIndex.RowIndex && i_SourceIndex.ColumnIndex - 1 == i_DestinationIndex.ColumnIndex)
            {
                simpleNorthWestMoveIsValid = true;
            }

            else
            {
                simpleNorthWestMoveIsValid = false;
            }

            return simpleNorthWestMoveIsValid;

        }

        public bool SimpleSouthEastMoveValidation(SquareIndex i_SourceIndex, SquareIndex i_DestinationIndex)
        {
            bool simpleSouthEastMoveIsValid;

            if (i_SourceIndex.RowIndex + 1 == i_DestinationIndex.RowIndex && i_SourceIndex.ColumnIndex + 1 == i_DestinationIndex.ColumnIndex)
            {
                simpleSouthEastMoveIsValid = true;
            }

            else
            {
                simpleSouthEastMoveIsValid = false;
            }

            return simpleSouthEastMoveIsValid;

        }

        public bool SimpleSouthWestMoveValidation(SquareIndex i_SourceIndex, SquareIndex i_DestinationIndex)
        {
            bool simpleSouthWestMoveIsValid;

            if (i_SourceIndex.RowIndex + 1 == i_DestinationIndex.RowIndex && i_SourceIndex.ColumnIndex - 1 == i_DestinationIndex.ColumnIndex)
            {
                simpleSouthWestMoveIsValid = true;
            }

            else
            {
                simpleSouthWestMoveIsValid = false;
            }

            return simpleSouthWestMoveIsValid;

        }

        public bool EatingMoveValidation(Board i_Board, Player i_CurrPlayer, SquareIndex i_SourceIndex, SquareIndex i_DestinationIndex)
        {
            bool eatingMoveIsValid;
            bool eatingMoveForwardIsValid;
            bool eatingMoveBackwardsIsValid;

            eatingMoveForwardIsValid = EatingMoveForwardValidation(i_Board, i_CurrPlayer, i_SourceIndex, i_DestinationIndex);
            if (eatingMoveForwardIsValid)
            {
                eatingMoveIsValid = true;
            }

            else if (i_Board[i_SourceIndex].DiscType == i_CurrPlayer.KingDiscType) //The sourceIndex contain a KingDiscType.
            {
                eatingMoveBackwardsIsValid = EatingMoveBackwardsValidation(i_Board, i_CurrPlayer, i_SourceIndex, i_DestinationIndex);
                eatingMoveIsValid = eatingMoveBackwardsIsValid;
            }

            else
            {
                eatingMoveIsValid = false;
            }

            return eatingMoveIsValid;

        }

        public bool EatingMoveForwardValidationBoard(Board i_Board, Player i_CurrPlayer, SquareIndex i_SourceIndex, SquareIndex i_DestinationIndex)
        {
            bool eatingMoveForwardIsValid;

            if (i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Up)
            {
                //eatingMoveForwardIsValid = 
            }

            else //(i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Down)
            {
                //eatingMoveForwardIsValid = 
            }

            return eatingMoveForwardIsValid;

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

            io_Board[i_DestinationIndex].DiscType = io_Board[i_SourceIndex].DiscType;
            io_Board[i_SourceIndex].DiscType = eDiscType.None;


        }

        // public bool MoveFromOptionValiidation(Player i_CurrPlayer, Board i_Board)
        /*  {
              return true;
          }*/

        public void updateIfReachedToLastLine(Player i_CurrPlayer, SquareIndex i_SourceIndex, int i_BoardSize)
        {
            if (i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Up)
            {
                if (i_SourceIndex.RowIndex == 0)
                {
                    m_ReachedLastLine = true;
                }

                else
                {
                    m_ReachedLastLine = false;
                }
            }

            else if (i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Down)
            {
                if (i_SourceIndex.RowIndex == i_BoardSize)
                {
                    m_ReachedLastLine = true;
                }

                else
                {
                    m_ReachedLastLine = false;
                }
            }

            //else if (i_CurrPlayer.MovingDirection == ePlayerMovingDirection.None)
            //??
        }


    }


}




