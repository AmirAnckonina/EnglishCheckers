using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersGame
{

    public class MoveManager
    {
        private static readonly int InvalidIndicesDifferences = -1; // Check how to set it.
        private SquareIndex m_SourceIndex;
        private SquareIndex m_DestinationIndex;
        private SquareIndex m_NewSourceIndexPostEating;
        private eMoveType m_MoveType;
        private bool m_ReachedLastLine;
       // private bool m_EatingMove;
        
        public MoveManager()
        {
            m_SourceIndex = new SquareIndex();
            m_DestinationIndex = new SquareIndex();
            m_MoveType = eMoveType.None;
        }

        /*public bool EatingMove
        {
            get
            {
                return m_EatingMove;
            }

            set
            {
                m_EatingMove = value;
            }
        }*/

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

        public bool MoveValidation(Board i_Board, Player i_CurrPlayer)
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

            srcAndDestBasicallyValid = SrcAndDestBasicValidation(i_Board, i_CurrPlayer);
            indicesDifferencesAreValid = IndicesDifferencesValidationAndSetup(out indicesDifferences);
            if (srcAndDestBasicallyValid && indicesDifferencesAreValid)
            {
                switch (indicesDifferences) // Think how to use the boolean result above
                {
                    case 1:
                        simpleMoveIsValid = SimpleMoveValidation(i_Board, i_CurrPlayer);
                        moveIsValid = simpleMoveIsValid;
                        break;

                    case 2:
                        eatingMoveIsValid = EatingMoveValidation(i_Board, i_CurrPlayer);
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

        public bool IndicesDifferencesValidationAndSetup(out int o_IndicesDifference)
        {
            bool indicesDifferencesAreValid;
            int rowIndicesDifference;
            int columnIndicesDifference;

            rowIndicesDifference = CalculateRowIndicesDifference();
            columnIndicesDifference = CalculateColumnIndicesDifference();

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

        public int CalculateRowIndicesDifference()
        {
            int rowIndicesDifferenceResult;

            rowIndicesDifferenceResult = Math.Abs(m_SourceIndex.RowIndex - m_DestinationIndex.RowIndex);

            return rowIndicesDifferenceResult;
        }

        public int CalculateColumnIndicesDifference()
        {
            int columnIndicesDifferenceResult;

            columnIndicesDifferenceResult = Math.Abs(m_SourceIndex.ColumnIndex - m_DestinationIndex.ColumnIndex);

            return columnIndicesDifferenceResult;
        }

        public bool SimpleMoveValidation(Board i_Board, Player i_CurrPlayer)
        {
            bool simpleMoveIsValid;
            bool simpleMoveForwardIsValid;
            bool simpleMoveBackwardsIsValid;

            simpleMoveForwardIsValid = SimpleMoveForwardValidation(i_CurrPlayer);
            if (simpleMoveForwardIsValid)
            {
                simpleMoveIsValid = true;
            }

            else if (i_Board[m_SourceIndex].DiscType == i_CurrPlayer.KingDiscType) //The sourceIndex contain a KingDiscType.
            {
                simpleMoveBackwardsIsValid = SimpleMoveBackwardsValidation(i_CurrPlayer);
                simpleMoveIsValid = simpleMoveBackwardsIsValid;
            }

            else
            {
                simpleMoveIsValid = false;
            }

            return simpleMoveIsValid;

        }

        public bool SimpleMoveForwardValidation(Player i_CurrPlayer)
        {
            bool simpleMoveForwardIsValid;

            if (i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Up)
            {
                simpleMoveForwardIsValid = SimpleNorthEastMoveValidation() || SimpleNorthWestMoveValidation();
            }

            else //(i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Down)
            {
                simpleMoveForwardIsValid = SimpleSouthEastMoveValidation() || SimpleSouthWestMoveValidation();
            }

            return simpleMoveForwardIsValid;

        }

        public bool SimpleMoveBackwardsValidation(Player i_CurrPlayer)
        {
            bool simpleMoveBackwardsIsValid;

            if (i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Up)
            {
                simpleMoveBackwardsIsValid = SimpleSouthEastMoveValidation() || SimpleSouthWestMoveValidation();
            }

            else //(i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Down)
            {
                simpleMoveBackwardsIsValid = SimpleNorthEastMoveValidation() || SimpleNorthWestMoveValidation();
            }

            return simpleMoveBackwardsIsValid;

        }

        public bool SimpleNorthEastMoveValidation()
        {
            bool simpleNorthEastMoveIsValid;

            if (m_SourceIndex.RowIndex - 1 == m_DestinationIndex.RowIndex && m_SourceIndex.ColumnIndex + 1 == m_DestinationIndex.ColumnIndex)
            {
                simpleNorthEastMoveIsValid = true;
                m_MoveType = eMoveType.SimpleNorthEastMove;
            }

            else
            {
                simpleNorthEastMoveIsValid = false;
            }

            return simpleNorthEastMoveIsValid;

            //SquareIndex potentialNewSquareIndex; new SquareIndex(m_SourceIndex.RowIndex -1, m_SourceIndex.ColumnIndex + 1);
            //How to Use operator????
            /* if (potentialNewSquareIndex. == m_DestinationIndex)
             {

             }*/
        }

        public bool SimpleNorthWestMoveValidation()
        {
            bool simpleNorthWestMoveIsValid;

            if (m_SourceIndex.RowIndex - 1 == m_DestinationIndex.RowIndex && m_SourceIndex.ColumnIndex - 1 == m_DestinationIndex.ColumnIndex)
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

        public bool SimpleSouthEastMoveValidation()
        {
            bool simpleSouthEastMoveIsValid;

            if (m_SourceIndex.RowIndex + 1 == m_DestinationIndex.RowIndex && m_SourceIndex.ColumnIndex + 1 == m_DestinationIndex.ColumnIndex)
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

        public bool SimpleSouthWestMoveValidation()
        {
            bool simpleSouthWestMoveIsValid;

            if (m_SourceIndex.RowIndex + 1 == m_DestinationIndex.RowIndex && m_SourceIndex.ColumnIndex - 1 == m_DestinationIndex.ColumnIndex)
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

        public bool EatingMoveValidation(Board i_Board, Player i_CurrPlayer)
        {
            bool eatingMoveIsValid;
            bool eatingMoveForwardIsValid;
            bool eatingMoveBackwardsIsValid;

            eatingMoveForwardIsValid = EatingMoveForwardValidation(i_Board, i_CurrPlayer);
            if (eatingMoveForwardIsValid)
            {
                eatingMoveIsValid = true;
                //m_EatingMove = true;
            }

            else if (i_Board[m_SourceIndex].DiscType == i_CurrPlayer.KingDiscType) //The sourceIndex contain a KingDiscType.
            {
                eatingMoveBackwardsIsValid = EatingMoveBackwardsValidation(i_Board, i_CurrPlayer);
                eatingMoveIsValid = eatingMoveBackwardsIsValid;
            }

            else
            {
                eatingMoveIsValid = false;
            }

            return eatingMoveIsValid;

        }

        public bool EatingMoveForwardValidation(Board i_Board, Player i_CurrPlayer)
        {
            bool eatingMoveForwardIsValid;

            if (i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Up)
            {
                eatingMoveForwardIsValid = EatingNorthEastMoveValidation(i_Board, i_CurrPlayer) || EatingNorthWestMoveValidation(i_Board, i_CurrPlayer);
            }

            else //(i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Down)
            {
                eatingMoveForwardIsValid = EatingSouthEastMoveValidation(i_Board, i_CurrPlayer) || EatingSouthWestMoveValidation(i_Board, i_CurrPlayer);
            }

            return eatingMoveForwardIsValid;

        }

        public bool EatingMoveBackwardsValidation(Board i_Board, Player i_CurrPlayer)
        {
            bool eatingMoveBackwardsIsValid;

            if (i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Up)
            {
                eatingMoveBackwardsIsValid = EatingSouthEastMoveValidation(i_Board, i_CurrPlayer) || EatingSouthWestMoveValidation(i_Board, i_CurrPlayer);
            }

            else //(i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Down)
            {
                eatingMoveBackwardsIsValid = EatingNorthEastMoveValidation(i_Board, i_CurrPlayer) || EatingNorthWestMoveValidation(i_Board, i_CurrPlayer);
            }

            return eatingMoveBackwardsIsValid;

        }

        public bool EatingNorthEastMoveValidation(Board i_Board, Player i_CurrPlayer)
        {
            bool eatingNorthEastMoveIsValid;
            SquareIndex northEastSquareIndex = new SquareIndex(m_SourceIndex.RowIndex - 2, m_SourceIndex.ColumnIndex + 2);
            /// Two Validation. 1. Indices are matching 2. Rival is in between
            /// RONI -> Create in SquareIndex class IsEqualFunction
            /// Take the northEastSquareIndex and use IsEqual Function -> m_SourceIndex.IsEqual(northEastSquareIndex);
            if (m_SourceIndex.RowIndex - 2 == m_DestinationIndex.RowIndex && m_SourceIndex.ColumnIndex + 2 == m_DestinationIndex.ColumnIndex)
            {
                if (i_Board[m_SourceIndex.RowIndex - 1, m_SourceIndex.ColumnIndex + 1].RivalInSquareValidation(i_CurrPlayer))
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

        public bool EatingNorthWestMoveValidation(Board i_Board, Player i_CurrPlayer)
        {
            bool eatingNorthWestMoveIsValid;
            //Two Validation. 1. Indices are matching 2. Rival is in between
            if (m_SourceIndex.RowIndex - 2 == m_DestinationIndex.RowIndex && m_SourceIndex.ColumnIndex - 2 == m_DestinationIndex.ColumnIndex)
            {
                if (i_Board[m_SourceIndex.RowIndex - 1, m_SourceIndex.ColumnIndex - 1].RivalInSquareValidation(i_CurrPlayer))
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

        public bool EatingSouthEastMoveValidation(Board i_Board, Player i_CurrPlayer)
        {
            bool eatingSouthEastMoveIsValid;
            //Two Validation. 1. Indices are matching 2. Rival is in between
            if (m_SourceIndex.RowIndex + 2 == m_DestinationIndex.RowIndex && m_SourceIndex.ColumnIndex + 2 == m_DestinationIndex.ColumnIndex)
            {
                if (i_Board[m_SourceIndex.RowIndex + 1, m_SourceIndex.ColumnIndex + 1].RivalInSquareValidation(i_CurrPlayer))
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

        public bool EatingSouthWestMoveValidation(Board i_Board, Player i_CurrPlayer)
        {
            bool eatingSouthWestMoveIsValid;
            //Two Validation. 1. Indices are matching 2. Rival is in between
            if (m_SourceIndex.RowIndex + 2 == m_DestinationIndex.RowIndex && m_SourceIndex.ColumnIndex - 2 == m_DestinationIndex.ColumnIndex)
            {
                if (i_Board[m_SourceIndex.RowIndex + 1, m_SourceIndex.ColumnIndex - 1].RivalInSquareValidation(i_CurrPlayer))
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

        public bool SrcAndDestBasicValidation(Board i_Board, Player i_CurrPlayer)
        {
            bool srcAndDestBasicallyValid;
            bool sourceIsValid;
            bool destinationIsVacantAndLegal;
            bool indiciesInBoard;

            indiciesInBoard = IndiciesInBoardValidation(i_Board);
            if (indiciesInBoard)
            {
                sourceIsValid = SourceValidation(i_Board[m_SourceIndex], i_CurrPlayer);
                destinationIsVacantAndLegal = DestinationVacancyAndLegalityValidation(i_Board[m_DestinationIndex]);
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

        public bool IndiciesInBoardValidation(Board i_Board)
        {
            bool indiciesInBoard;
            bool sourceIsExist;
            bool destinationIsExist;

            sourceIsExist = i_Board.SquareExistenceValidation(m_SourceIndex);
            destinationIsExist = i_Board.SquareExistenceValidation(m_DestinationIndex);

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

        public void ExecuteMove(Board io_Board, Player i_CurrPlayer)
        {
            /// We need:
            /// 1. Update the destinationSquare DiscType of the source.
            /// 2. Update the sourceSquare DiscType to None.
            /// 3. If eating occured -> update also the square we passed above

            BasicMoveProcedure(io_Board, i_CurrPlayer);
            if (EatingMoveOccurred())
            {
                EatingMoveProcedure(io_Board);
            }  
        }

        public bool EatingMoveOccurred()
        {
            bool eatingMoveOccurred;

            switch (m_MoveType)
            {
                case eMoveType.EatingNorthEastMove:       
                case eMoveType.EatingNorthWestMove:
                case eMoveType.EatingSouthEastMove:
                case eMoveType.EatingSouthWestMove:
                    eatingMoveOccurred = true;
                    break;

                default:
                    eatingMoveOccurred = false;
                    break;
            }

            return eatingMoveOccurred;
        }

        public void BasicMoveProcedure(Board io_Board, Player i_CurrPlayer)
        {
            /// option1: update field by field in square:
            io_Board[m_DestinationIndex].DiscType = io_Board[m_SourceIndex].DiscType;
            io_Board[m_DestinationIndex].SquareHolder = i_CurrPlayer.PlayerRecognition;
            io_Board[m_SourceIndex].DiscType = eDiscType.None;
            io_Board[m_SourceIndex].SquareHolder = ePlayerRecognition.None;

            /// option2: set the detinationSquare ref to sourceIndex?
            /// 
        }

        public void EatingMoveProcedure(Board io_Board)
        {
            switch (m_MoveType)
            {
                case eMoveType.EatingNorthEastMove:
                    io_Board[m_SourceIndex.RowIndex - 1, m_SourceIndex.ColumnIndex + 1].DiscType = eDiscType.None;
                    io_Board[m_SourceIndex.RowIndex - 1, m_SourceIndex.ColumnIndex + 1].SquareHolder = ePlayerRecognition.None;
                    break;

                case eMoveType.EatingNorthWestMove:
                    io_Board[m_SourceIndex.RowIndex - 1, m_SourceIndex.ColumnIndex - 1].DiscType = eDiscType.None;
                    io_Board[m_SourceIndex.RowIndex - 1, m_SourceIndex.ColumnIndex - 1].SquareHolder = ePlayerRecognition.None;
                    break;

                case eMoveType.EatingSouthEastMove:
                    io_Board[m_SourceIndex.RowIndex + 1 , m_SourceIndex.ColumnIndex + 1].DiscType = eDiscType.None;
                    io_Board[m_SourceIndex.RowIndex + 1, m_SourceIndex.ColumnIndex + 1].SquareHolder = ePlayerRecognition.None;
                    break;

                case eMoveType.EatingSouthWestMove:
                    io_Board[m_SourceIndex.RowIndex + 1, m_SourceIndex.ColumnIndex - 1].DiscType = eDiscType.None;
                    io_Board[m_SourceIndex.RowIndex + 1, m_SourceIndex.ColumnIndex - 1].SquareHolder = ePlayerRecognition.None;
                    break;

                default:
                    break;
            }
        }

        public bool RecurringTurnEatingMovePossibiltyValidation(Board i_Board, Player i_CurrPlayer)
        {
            bool anotherEatingIsPossible = true;
            /// Should be placed outside this method. -> m_NewSourceIndexPostEating = m_DestinationIndex;
            /// 
            m_SourceIndex = m_DestinationIndex;

            /// Should check for newSourceIndex if there any possibilty for another eating.
            /// CAREFUL!!! handle the data memebrs with extra caution.
            /// 
            /// Will know to ask only for switch case 2 because we sending only + 2,2 Indicies.
            /// Or send directly to Basic Validation and EatingMoveValidation directions

            /// NorthEast
            /// Use SetSquareIndices -> m_DesitinationIndex.SetSqaureIndices(m_SourceIndex.RowIndex - 2,  m_SourceIndex.ColumnIndex + 2)
            m_DestinationIndex.RowIndex = m_SourceIndex.RowIndex - 2;
            m_DestinationIndex.ColumnIndex = m_SourceIndex.ColumnIndex + 2;
            /// Check SrcAndDestBasicValidation + EatingMoveNotrhEastValidation + Player Direction + PlayerDiscType - Important!!!!!
            
            /// NorthWest
            m_DestinationIndex.RowIndex = m_SourceIndex.RowIndex - 2;
            m_DestinationIndex.ColumnIndex = m_SourceIndex.ColumnIndex - 2;

            /// SouthEast
            m_DestinationIndex.RowIndex = m_SourceIndex.RowIndex + 2;
            m_DestinationIndex.ColumnIndex = m_SourceIndex.ColumnIndex + 2;

            /// SouthWest
            m_DestinationIndex.RowIndex = m_SourceIndex.RowIndex - 2;
            m_DestinationIndex.ColumnIndex = m_SourceIndex.ColumnIndex - 2;

            return anotherEatingIsPossible;
        }

        // public bool MoveFromOptionValiidation(Player i_CurrPlayer, Board i_Board)
        /*  {
              return true;
          }*/
        /*
                public void UpdateIfReachedToLastLine(Player i_CurrPlayer, SquareIndex m_SourceIndex, int i_BoardSize)
                {
                    if (i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Up)
                    {
                        if (m_SourceIndex.RowIndex == 0)
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
                        if (m_SourceIndex.RowIndex == i_BoardSize)
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

                public bool DoubleTurnValidation(Board i_Board, Player i_CurrPlayer, SquareIndex m_SourceIndex, SquareIndex m_DestinationIndex)
                {
                    bool isDoubleTurnValid;
                    SquareIndex newSourceIndex = m_DestinationIndex; ;
                    SquareIndex newDestinationIndex = new SquareIndex();

                    newDestinationIndex.RowIndex = SetNewDestinationRow(i_CurrPlayer, m_DestinationIndex);
                    newDestinationIndex.RowIndex = SetNewDestinationColumn(i_CurrPlayer, m_DestinationIndex);

                    if (DoubleTurnWithSpecificDst(i_Board, i_CurrPlayer, newSourceIndex, newDestinationIndex))
                    {
                        isDoubleTurnValid = true;
                    }

                    else
                    {
                        newDestinationIndex.ColumnIndex = m_DestinationIndex.ColumnIndex - 2;
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

                public bool DoubleTurnWithSpecificDst(Board i_Board, Player i_CurrPlayer, SquareIndex m_SourceIndex, SquareIndex m_DestinationIndex)
                {
                    bool isDoubleTurnValid;
                    bool indicesDifferencesAreValid;
                    bool srcAndDestBasicallyValid;
                    bool isEatenValid;

                    srcAndDestBasicallyValid = SrcAndDestBasicValidation(i_Board, i_CurrPlayer, m_SourceIndex, m_DestinationIndex);
                    indicesDifferencesAreValid = IndicesDifferencesValidationAndSetup(m_SourceIndex, m_DestinationIndex, out int o_IndicesDifference);
                    isEatenValid = EatingMoveValidation(i_Board, i_CurrPlayer, m_SourceIndex, m_DestinationIndex);

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

                public int SetNewDestinationRow(Player i_CurrPlayer, SquareIndex m_DestinationIndex)
                {
                    int newRowResult = -1;

                    if (i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Up) //Forward
                    {
                        newRowResult = (m_DestinationIndex.RowIndex) - 2;
                    }

                    else if (i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Down)
                    {
                        newRowResult = (m_DestinationIndex.RowIndex) + 2;
                    }

                    return newRowResult;
                }

                public int SetNewDestinationColumn(Player i_CurrPlayer, SquareIndex m_DestinationIndex)
                {
                    return m_DestinationIndex.RowIndex + 2;
                }

                public bool AnyPosabbilityToMove(Board i_Board, Player i_CurrPlayer, SquareIndex m_SourceIndex, SquareIndex i_destinationIndex)
                {
                    bool anyPosabbilityToMove = false;

                    foreach (SquareIndex currSquare in i_CurrPlayer.CurrentHoldingSquareIndices)
                    {
                        if (SimpleMoveBackwardsValidation(i_CurrPlayer, m_SourceIndex, i_destinationIndex) || EatingMoveForwardValidation(i_Board, i_CurrPlayer, m_SourceIndex, i_destinationIndex))
                        {
                            anyPosabbilityToMove = true;
                        }
                    }

                    return anyPosabbilityToMove;
                }


            }*/
    }
}




