using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersGame
{

    public class MoveManager
    {
        private SquareIndex m_SourceIndex;
        private SquareIndex m_DestinationIndex;
        private static readonly int InvalidIndicesDifferences = -1; // Check how to set it.
        private eMoveType m_MoveType;
        private bool m_ReachedLastLine;

        //MoveValidation Params:
        // 1. SquareIndex srcIndex
        // 2. SquareIndex DestIndex
        // 3. ref Board m_Board
        // 4. ref Player m_CurrPlayer

        public SquareIndex SourceIndex
        {
            get
            {
                return m_SourceIndex;
            }
            set
            {
                m_SourceIndex = value;
            }
        } 
        
        public SquareIndex DestinationIndex
        {
            get
            {
                return m_DestinationIndex;
            }
            set
            {
                m_DestinationIndex = value;
            }
        }

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
                m_MoveType = eMoveType.SimpleNorthEastMove;
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
                m_MoveType = eMoveType.SimpleNorthWestMove;
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
                m_MoveType = eMoveType.SimpleSouthEastMove;
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
                m_MoveType = eMoveType.SimpleSouthWestMove;
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

        public bool EatingMoveForwardValidation(Board i_Board, Player i_CurrPlayer, SquareIndex i_SourceIndex, SquareIndex i_DestinationIndex)
        {
            bool eatingMoveForwardIsValid;

            if (i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Up)
            {
                eatingMoveForwardIsValid = EatingNorthEastMoveValidation(i_Board, i_CurrPlayer, i_SourceIndex, i_DestinationIndex) || EatingNorthWestMoveValidation(i_Board, i_CurrPlayer, i_SourceIndex, i_DestinationIndex);
            }

            else //(i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Down)
            {
                eatingMoveForwardIsValid = EatingSouthEastMoveValidation(i_Board, i_CurrPlayer, i_SourceIndex, i_DestinationIndex) || EatingSouthWestMoveValidation(i_Board, i_CurrPlayer, i_SourceIndex, i_DestinationIndex);
            }

            return eatingMoveForwardIsValid;

        }

        public bool EatingMoveBackwardsValidation(Board i_Board, Player i_CurrPlayer, SquareIndex i_SourceIndex, SquareIndex i_DestinationIndex)
        {
            bool eatingMoveBackwardsIsValid;

            if (i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Up)
            {
                eatingMoveBackwardsIsValid = EatingSouthEastMoveValidation(i_Board, i_CurrPlayer, i_SourceIndex, i_DestinationIndex) || EatingSouthWestMoveValidation(i_Board, i_CurrPlayer, i_SourceIndex, i_DestinationIndex);
            }

            else //(i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Down)
            {
                eatingMoveBackwardsIsValid = EatingNorthEastMoveValidation(i_Board, i_CurrPlayer, i_SourceIndex, i_DestinationIndex) || EatingNorthWestMoveValidation(i_Board, i_CurrPlayer, i_SourceIndex, i_DestinationIndex);
            }

            return eatingMoveBackwardsIsValid;

        }

        public bool EatingNorthEastMoveValidation(Board i_Board, Player i_CurrPlayer, SquareIndex i_SourceIndex, SquareIndex i_DestinationIndex)
        {
            bool eatingNorthEastMoveIsValid;
            //Two Validation. 1. Indices are matching 2. Rival is in between
            if (i_SourceIndex.RowIndex - 2 == i_DestinationIndex.RowIndex && i_SourceIndex.ColumnIndex + 2 == i_DestinationIndex.ColumnIndex)
            {
                if (i_Board[i_SourceIndex.RowIndex - 1, i_SourceIndex.ColumnIndex + 1].RivalInSquareValidation(i_CurrPlayer))
                {
                    eatingNorthEastMoveIsValid = true;
                    m_MoveType = eMoveType.EatingNorthEastMove;
                }
                
                else
                {
                    eatingNorthEastMoveIsValid = false;
                }
            }

            else
            {
                eatingNorthEastMoveIsValid = false;
            }


            return eatingNorthEastMoveIsValid;

        }

        public bool EatingNorthWestMoveValidation(Board i_Board, Player i_CurrPlayer, SquareIndex i_SourceIndex, SquareIndex i_DestinationIndex)
        {
            bool eatingNorthWestMoveIsValid;
            //Two Validation. 1. Indices are matching 2. Rival is in between
            if (i_SourceIndex.RowIndex - 2 == i_DestinationIndex.RowIndex && i_SourceIndex.ColumnIndex - 2 == i_DestinationIndex.ColumnIndex)
            {
                if (i_Board[i_SourceIndex.RowIndex - 1, i_SourceIndex.ColumnIndex - 1].RivalInSquareValidation(i_CurrPlayer))
                {
                    eatingNorthWestMoveIsValid = true;
                    m_MoveType = eMoveType.EatingNorthWestMove;
                }

                else
                {
                    eatingNorthWestMoveIsValid = false;
                }
            }

            else
            {
                eatingNorthWestMoveIsValid = false;
            }


            return eatingNorthWestMoveIsValid;
        }

        public bool EatingSouthEastMoveValidation(Board i_Board, Player i_CurrPlayer, SquareIndex i_SourceIndex, SquareIndex i_DestinationIndex)
        {
            bool eatingSouthEastMoveIsValid;
            //Two Validation. 1. Indices are matching 2. Rival is in between
            if (i_SourceIndex.RowIndex + 2 == i_DestinationIndex.RowIndex && i_SourceIndex.ColumnIndex + 2 == i_DestinationIndex.ColumnIndex)
            {
                if (i_Board[i_SourceIndex.RowIndex + 1, i_SourceIndex.ColumnIndex + 1].RivalInSquareValidation(i_CurrPlayer))
                {
                    eatingSouthEastMoveIsValid = true;
                    m_MoveType = eMoveType.EatingSouthEastMove;
                }

                else
                {
                    eatingSouthEastMoveIsValid = false;
                }
            }

            else
            {
                eatingSouthEastMoveIsValid = false;
            }


            return eatingSouthEastMoveIsValid;
        }

        public bool EatingSouthWestMoveValidation(Board i_Board, Player i_CurrPlayer, SquareIndex i_SourceIndex, SquareIndex i_DestinationIndex)
        {
            bool eatingSouthWestMoveIsValid;
            //Two Validation. 1. Indices are matching 2. Rival is in between
            if (i_SourceIndex.RowIndex + 2 == i_DestinationIndex.RowIndex && i_SourceIndex.ColumnIndex - 2 == i_DestinationIndex.ColumnIndex)
            {
                if (i_Board[i_SourceIndex.RowIndex + 1, i_SourceIndex.ColumnIndex - 1].RivalInSquareValidation(i_CurrPlayer))
                {
                    eatingSouthWestMoveIsValid = true;
                    m_MoveType = eMoveType.EatingSouthWestMove;
                }

                else
                {
                    eatingSouthWestMoveIsValid = false;
                }
            }

            else
            {
                eatingSouthWestMoveIsValid = false;
            }


            return eatingSouthWestMoveIsValid;
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

        public void UpdateIfReachedToLastLine(Player i_CurrPlayer, SquareIndex i_SourceIndex, int i_BoardSize)
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

        public bool DoubleTurnValidation(Board i_Board, Player i_CurrPlayer, SquareIndex i_SourceIndex, SquareIndex i_DestinationIndex)
        {
            bool isDoubleTurnValid;
            SquareIndex newSourceIndex = i_DestinationIndex; ;
            SquareIndex newDestinationIndex = new SquareIndex();

            newDestinationIndex.RowIndex = SetNewDestinationRow(i_CurrPlayer, i_DestinationIndex);
            newDestinationIndex.RowIndex = SetNewDestinationColumn(i_CurrPlayer, i_DestinationIndex);

            if (DoubleTurnWithSpecificDst(i_Board, i_CurrPlayer, newSourceIndex, newDestinationIndex))
            {
                isDoubleTurnValid = true;
            }

            else
            {
                newDestinationIndex.ColumnIndex = i_DestinationIndex.ColumnIndex - 2;
                if (DoubleTurnWithSpecificDst(i_Board, i_CurrPlayer, newSourceIndex, newDestinationIndex))
                {
                    isDoubleTurnValid = true;
                }

                else
                {
                    isDoubleTurnValid = false;
                }
            }

            return isDoubleTurnValid;
        }

        public bool DoubleTurnWithSpecificDst(Board i_Board, Player i_CurrPlayer, SquareIndex i_SourceIndex, SquareIndex i_DestinationIndex)
        {
            bool isDoubleTurnValid;
            bool indicesDifferencesAreValid;
            bool srcAndDestBasicallyValid;
            bool isEatenValid;

            srcAndDestBasicallyValid = SrcAndDestBasicValidation(i_Board, i_CurrPlayer, i_SourceIndex, i_DestinationIndex);
            indicesDifferencesAreValid = IndicesDifferencesValidationAndSetup(i_SourceIndex, i_DestinationIndex, out int o_IndicesDifference);
            isEatenValid = EatingMoveValidation(i_Board, i_CurrPlayer, i_SourceIndex, i_DestinationIndex);

            if (srcAndDestBasicallyValid && indicesDifferencesAreValid && isEatenValid)
            {
                isDoubleTurnValid = true;
            }

            else
            {
                isDoubleTurnValid = false;
            }

            return isDoubleTurnValid;

        }

        public int SetNewDestinationRow(Player i_CurrPlayer, SquareIndex i_DestinationIndex)
        {
            int newRowResult = -1;

            if (i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Up) //Forward
            {
                newRowResult = (i_DestinationIndex.RowIndex) - 2;
            }

            else if (i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Down)
            {
                newRowResult = (i_DestinationIndex.RowIndex) + 2;
            }

            return newRowResult;
        }

        public int SetNewDestinationColumn(Player i_CurrPlayer, SquareIndex i_DestinationIndex)
        {
            return i_DestinationIndex.RowIndex + 2;
        }

        public bool AnyPosabbilityToMove(Board i_Board, Player i_CurrPlayer, SquareIndex i_SourceIndex, SquareIndex i_destinationIndex)
        {
            bool anyPosabbilityToMove = false; 

            foreach (SquareIndex currSquare in i_CurrPlayer.CurrentHoldingSquareIndices)
            {
                if(SimpleMoveBackwardsValidation(i_CurrPlayer,i_SourceIndex,i_destinationIndex) || EatingMoveForwardValidation(i_Board,i_CurrPlayer,i_SourceIndex,i_destinationIndex))
                {
                    anyPosabbilityToMove = true;
                }
            }

            return anyPosabbilityToMove;
        }
  

    }

}




