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
        private SquareIndex m_RecurringTurnNewSourceIndex;
        private SquareIndex m_EatedSquareIndex;
        private eMoveType m_MoveType;
        /// private bool m_ReachedLastLine;
       
        
        public MoveManager()
        {
            m_SourceIndex = new SquareIndex();
            m_DestinationIndex = new SquareIndex();
            m_RecurringTurnNewSourceIndex = new SquareIndex(); 
            m_EatedSquareIndex = new SquareIndex();
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

        public SquareIndex RecurringTurnNewSourceIndex
        {
            get
            {
                return m_RecurringTurnNewSourceIndex;
            }

            set
            {
                m_RecurringTurnNewSourceIndex = value;
            }
        }

        public SquareIndex EatedSquareIndex
        {
            get
            {
                return m_EatedSquareIndex;
            }

            set
            {
                m_EatedSquareIndex = value;
            }
        }


        /*public bool ReachedLastLine
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
*/

        public void clearPreviousValues()
        {
            /// ?
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

            srcAndDestBasicallyValid = SourceAndDestinationBasicValidation(i_Board, i_CurrPlayer);
            indicesDifferencesAreValid = IndicesDifferencesValidationAndSetup(out indicesDifferences);
            /// Consider Handle the moveValidation 
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
                        m_MoveType = eMoveType.None;
                        break;
                }
            }

            else
            {
                moveIsValid = false;
                m_MoveType = eMoveType.None;
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

            simpleMoveForwardIsValid = SimpleForwardMoveValidation(i_CurrPlayer);
            if (simpleMoveForwardIsValid)
            {
                simpleMoveIsValid = true;
            }

            else if (i_Board[m_SourceIndex].DiscType == i_CurrPlayer.KingDiscType) //The sourceIndex contain a KingDiscType.
            {
                simpleMoveIsValid  = simpleMoveBackwardsIsValid = SimpleBackwardsMoveValidation(i_CurrPlayer);
                /// simpleMoveIsValid = simpleMoveBackwardsIsValid;
            }

            else
            {
                simpleMoveIsValid = false;
                m_MoveType = eMoveType.None;
            }

            return simpleMoveIsValid;

        }

        public bool SimpleForwardMoveValidation(Player i_CurrPlayer)
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

        public bool SimpleBackwardsMoveValidation(Player i_CurrPlayer)
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
            SquareIndex northEastSquareIndex = new SquareIndex(m_SourceIndex.RowIndex - 1, m_SourceIndex.ColumnIndex + 1);

            if (m_DestinationIndex.IsEqual(northEastSquareIndex)) 
            {
                simpleNorthEastMoveIsValid = true;
                m_MoveType = eMoveType.SimpleNorthEastMove;
            }

            else
            {
                simpleNorthEastMoveIsValid = false;
                m_MoveType = eMoveType.None;
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
            SquareIndex northWestSquareIndex = new SquareIndex(m_SourceIndex.RowIndex - 1, m_SourceIndex.ColumnIndex - 1);

            if (m_DestinationIndex.IsEqual(northWestSquareIndex))
            {
                simpleNorthWestMoveIsValid = true;
                m_MoveType = eMoveType.SimpleNorthWestMove;
            }

            else
            {
                simpleNorthWestMoveIsValid = false;
                m_MoveType = eMoveType.None;
            }

            return simpleNorthWestMoveIsValid;

        }

        public bool SimpleSouthEastMoveValidation()
        {
            bool simpleSouthEastMoveIsValid;
            SquareIndex southEastSquareIndex = new SquareIndex(m_SourceIndex.RowIndex + 1, m_SourceIndex.ColumnIndex + 1);

            if (m_DestinationIndex.IsEqual(southEastSquareIndex))
            {
                simpleSouthEastMoveIsValid = true;
                m_MoveType = eMoveType.SimpleSouthEastMove;
            }

            else
            {
                simpleSouthEastMoveIsValid = false;
                m_MoveType = eMoveType.None;
            }

            return simpleSouthEastMoveIsValid;

        }

        public bool SimpleSouthWestMoveValidation()
        {
            bool simpleSouthWestMoveIsValid;
            SquareIndex southWestSquareIndex = new SquareIndex(m_SourceIndex.RowIndex + 1, m_SourceIndex.ColumnIndex - 1);

            if (m_DestinationIndex.IsEqual(southWestSquareIndex))
            {
                simpleSouthWestMoveIsValid = true;
                m_MoveType = eMoveType.SimpleSouthWestMove;
            }

            else
            {
                simpleSouthWestMoveIsValid = false;
                m_MoveType = eMoveType.None;
            }

            return simpleSouthWestMoveIsValid;

        }

        public bool EatingMoveValidation(Board i_Board, Player i_CurrPlayer)
        {
            bool eatingMoveIsValid;
            bool eatingMoveForwardIsValid;
            bool eatingMoveBackwardsIsValid;

            eatingMoveForwardIsValid = EatingForwardMoveValidation(i_Board, i_CurrPlayer);
            if (eatingMoveForwardIsValid)
            {
                eatingMoveIsValid = true;
            }

            else if (i_Board[m_SourceIndex].DiscType == i_CurrPlayer.KingDiscType) //The sourceIndex contain a KingDiscType.
            {
                eatingMoveIsValid = eatingMoveBackwardsIsValid = EatingBackwardsMoveValidation(i_Board, i_CurrPlayer);
                /// eatingMoveIsValid = eatingMoveBackwardsIsValid;
            }

            else
            {
                eatingMoveIsValid = false;
                m_MoveType = eMoveType.None;
            }

            return eatingMoveIsValid;

        }

        public bool EatingForwardMoveValidation(Board i_Board, Player i_CurrPlayer)
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

        public bool EatingBackwardsMoveValidation(Board i_Board, Player i_CurrPlayer)
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
            SquareIndex northEastPotentialEatedSquareIndex;

            if (m_DestinationIndex.IsEqual(northEastSquareIndex))
            {
                northEastPotentialEatedSquareIndex = new SquareIndex(m_SourceIndex.RowIndex - 1, m_SourceIndex.ColumnIndex + 1);
                if (i_Board[northEastPotentialEatedSquareIndex].RivalInSquareValidation(i_CurrPlayer))
                {
                    eatingNorthEastMoveIsValid = true;
                    m_MoveType = eMoveType.EatingNorthEastMove;
                }

                else
                {
                    eatingNorthEastMoveIsValid = false;
                    m_MoveType = eMoveType.None;
                }
            }

            else
            {
                eatingNorthEastMoveIsValid = false;
                m_MoveType = eMoveType.None;
            }


            return eatingNorthEastMoveIsValid;
        }

        public bool EatingNorthWestMoveValidation(Board i_Board, Player i_CurrPlayer)
        {
            bool eatingNorthWestMoveIsValid;
            SquareIndex northWestSquareIndex = new SquareIndex(m_SourceIndex.RowIndex - 2, m_SourceIndex.ColumnIndex - 2);
            SquareIndex northWestPotentialEatedSquareIndex;

            //Two Validation. 1. Indices are matching 2. Rival is in between

            if (m_DestinationIndex.IsEqual(northWestSquareIndex))
            {
                northWestPotentialEatedSquareIndex = new SquareIndex(m_SourceIndex.RowIndex - 1, m_SourceIndex.ColumnIndex - 1);
                if (i_Board[northWestPotentialEatedSquareIndex].RivalInSquareValidation(i_CurrPlayer))
                {
                    eatingNorthWestMoveIsValid = true;
                    m_MoveType = eMoveType.EatingNorthWestMove;
                }

                else
                {
                    eatingNorthWestMoveIsValid = false;
                    m_MoveType = eMoveType.None;
                }
            }

            else
            {
                eatingNorthWestMoveIsValid = false;
                m_MoveType = eMoveType.None;
            }


            return eatingNorthWestMoveIsValid;
        }

        public bool EatingSouthEastMoveValidation(Board i_Board, Player i_CurrPlayer)
        {
            bool eatingSouthEastMoveIsValid;
            SquareIndex southEastSquareIndex = new SquareIndex(m_SourceIndex.RowIndex + 2, m_SourceIndex.ColumnIndex + 2);
            SquareIndex southEastPotentialEatedSquareIndex;

            if (m_DestinationIndex.IsEqual(southEastSquareIndex))
            {
                southEastPotentialEatedSquareIndex = new SquareIndex(m_SourceIndex.RowIndex + 1, m_SourceIndex.ColumnIndex + 1);
                if (i_Board[southEastPotentialEatedSquareIndex].RivalInSquareValidation(i_CurrPlayer))
                {
                    eatingSouthEastMoveIsValid = true;
                    m_MoveType = eMoveType.EatingSouthEastMove;
                }

                else
                {
                    eatingSouthEastMoveIsValid = false;
                    m_MoveType = eMoveType.None;
                }
            }

            else
            {
                eatingSouthEastMoveIsValid = false;
                m_MoveType = eMoveType.None;
            }


            return eatingSouthEastMoveIsValid;
        }

        public bool EatingSouthWestMoveValidation(Board i_Board, Player i_CurrPlayer)
        {
            bool eatingSouthWestMoveIsValid;
            SquareIndex southWestSquareIndex = new SquareIndex(m_SourceIndex.RowIndex + 2, m_SourceIndex.ColumnIndex - 2);
            SquareIndex southWestPotentialEatedSquareIndex;

            if (m_DestinationIndex.IsEqual(southWestSquareIndex))
            {
                southWestPotentialEatedSquareIndex = new SquareIndex(m_SourceIndex.RowIndex + 1, m_SourceIndex.ColumnIndex - 1);
                if (i_Board[southWestPotentialEatedSquareIndex].RivalInSquareValidation(i_CurrPlayer))
                {
                    eatingSouthWestMoveIsValid = true;
                    m_MoveType = eMoveType.EatingSouthWestMove;
                }

                else
                {
                    eatingSouthWestMoveIsValid = false;
                    m_MoveType = eMoveType.None;
                }
            }

            else
            {
                eatingSouthWestMoveIsValid = false;
                m_MoveType = eMoveType.None;
            }


            return eatingSouthWestMoveIsValid;
        }

        public bool SourceAndDestinationBasicValidation(Board i_Board, Player i_CurrPlayer)
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
                    m_MoveType = eMoveType.None;
                }
            }

            else
            {
                srcAndDestBasicallyValid = false;
                m_MoveType = eMoveType.None;
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
                m_MoveType = eMoveType.None;
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

        public void EatingMoveProcedure(Board io_Board) /// Organize this function -> Using new SquareIndex?
        {
            switch (m_MoveType)
            {
                case eMoveType.EatingNorthEastMove:
                    io_Board[m_SourceIndex.RowIndex - 1, m_SourceIndex.ColumnIndex + 1].DiscType = eDiscType.None;
                    io_Board[m_SourceIndex.RowIndex - 1, m_SourceIndex.ColumnIndex + 1].SquareHolder = ePlayerRecognition.None;
                    m_EatedSquareIndex.SetSquareIndices(m_SourceIndex.RowIndex - 1, m_SourceIndex.ColumnIndex + 1);
                    break;

                case eMoveType.EatingNorthWestMove:
                    io_Board[m_SourceIndex.RowIndex - 1, m_SourceIndex.ColumnIndex - 1].DiscType = eDiscType.None;
                    io_Board[m_SourceIndex.RowIndex - 1, m_SourceIndex.ColumnIndex - 1].SquareHolder = ePlayerRecognition.None;
                    m_EatedSquareIndex.SetSquareIndices(m_SourceIndex.RowIndex - 1, m_SourceIndex.ColumnIndex - 1);
                    break;

                case eMoveType.EatingSouthEastMove:
                    io_Board[m_SourceIndex.RowIndex + 1 , m_SourceIndex.ColumnIndex + 1].DiscType = eDiscType.None;
                    io_Board[m_SourceIndex.RowIndex + 1, m_SourceIndex.ColumnIndex + 1].SquareHolder = ePlayerRecognition.None;
                    m_EatedSquareIndex.SetSquareIndices(m_SourceIndex.RowIndex + 1, m_SourceIndex.ColumnIndex + 1);
                    break;

                case eMoveType.EatingSouthWestMove:
                    io_Board[m_SourceIndex.RowIndex + 1, m_SourceIndex.ColumnIndex - 1].DiscType = eDiscType.None;
                    io_Board[m_SourceIndex.RowIndex + 1, m_SourceIndex.ColumnIndex - 1].SquareHolder = ePlayerRecognition.None;
                    m_EatedSquareIndex.SetSquareIndices(m_SourceIndex.RowIndex + 1, m_SourceIndex.ColumnIndex - 1);
                    break;

                default:
                    break;
            }
        }

        public void ReachedLastLineValidationAndUpdate(Board io_Board, Player i_CurrPlayer)
        {
            bool firstPlayerReachedLastLine;
            bool secondPlayerReachedLastLine;

            firstPlayerReachedLastLine = FirstPlayerReachedLastLineValidation(i_CurrPlayer, io_Board);
            secondPlayerReachedLastLine = SecondPlayerReachedLastLineValidation(i_CurrPlayer);
            if (firstPlayerReachedLastLine || secondPlayerReachedLastLine)
            {
                io_Board[m_DestinationIndex].DiscType = i_CurrPlayer.KingDiscType;
            }
        }

        public bool FirstPlayerReachedLastLineValidation(Player i_CurrPlayer, Board i_Board)
        {
            bool firstPlayerReachedLastLine;

            if (i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Down && m_DestinationIndex.RowIndex == i_Board.BoardSize - 1)
            {
                firstPlayerReachedLastLine = true;
            }
            
            else
            {
                firstPlayerReachedLastLine = false;
            }

            return firstPlayerReachedLastLine; 
        }

        public bool SecondPlayerReachedLastLineValidation(Player i_CurrPlayer)
        {
            bool secondPlayerReachedLastLine;
            
            if (i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Up && m_DestinationIndex.RowIndex == 0)
            {
                secondPlayerReachedLastLine = true;
            }

            else
            {
                secondPlayerReachedLastLine = false;
            }
            
            return secondPlayerReachedLastLine;
        }

        public bool AnySimpleMovePossibiltyCheck(Board i_Board, Player i_CurrPlayer)
        {
            bool simpleMoveIsPossible;
            bool simpleMoveForwardIsPossible;
            bool simpleMoveBackwardsIsPossible;

            simpleMoveForwardIsPossible = AnySimpleForwardMovePossibilityCheck(i_Board, i_CurrPlayer);
            if (simpleMoveForwardIsPossible)
            {
                simpleMoveIsPossible = true;
            }

            else if (i_Board[m_SourceIndex].DiscType == i_CurrPlayer.KingDiscType)
            {
                simpleMoveIsPossible = simpleMoveBackwardsIsPossible = AnySimpleBackwardsMovePossibilityCheck(i_Board, i_CurrPlayer);
            }

            else
            {
                simpleMoveIsPossible = false;
            }

            return simpleMoveIsPossible;
        }

        public bool AnySimpleForwardMovePossibilityCheck(Board i_Board, Player i_CurrPlayer)
        {
            bool simpleMoveForwardIsPossible;

            if (i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Up)
            {
                simpleMoveForwardIsPossible = SimpleNorthEastMovePossibilityCheck(i_Board, i_CurrPlayer) || SimpleNorthWestMovePossibilityCheck(i_Board, i_CurrPlayer);
            }

            else //(i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Down)
            {
                simpleMoveForwardIsPossible = SimpleSouthEastMovePossibilityCheck(i_Board, i_CurrPlayer) || SimpleSouthWestMovePossibilityCheck(i_Board, i_CurrPlayer);
            }

            return simpleMoveForwardIsPossible;
        }        
        
        public bool AnySimpleBackwardsMovePossibilityCheck(Board i_Board, Player i_CurrPlayer)
        {
            bool simpleMoveBackwardsIsPossible;

            if (i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Up)
            {
                simpleMoveBackwardsIsPossible = SimpleSouthEastMovePossibilityCheck(i_Board, i_CurrPlayer) || SimpleSouthWestMovePossibilityCheck(i_Board, i_CurrPlayer);
            }

            else //(i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Down)
            {
                simpleMoveBackwardsIsPossible = SimpleNorthEastMovePossibilityCheck(i_Board, i_CurrPlayer) || SimpleNorthWestMovePossibilityCheck(i_Board, i_CurrPlayer);
            }

            return simpleMoveBackwardsIsPossible;
        }

        public bool SimpleNorthEastMovePossibilityCheck(Board i_Board, Player i_CurrPlayer)
        {
            bool simpleNorthEastMoveIsPossible;
            bool srcAndDestAreBasicallyValid;

            m_DestinationIndex.SetSquareIndices(m_SourceIndex.RowIndex - 1, m_SourceIndex.ColumnIndex + 1);
            srcAndDestAreBasicallyValid = SourceAndDestinationBasicValidation(i_Board, i_CurrPlayer);
            if (srcAndDestAreBasicallyValid)
            {
                simpleNorthEastMoveIsPossible = SimpleNorthEastMoveValidation();
            }

            else
            {
                simpleNorthEastMoveIsPossible = false;
            }

            return simpleNorthEastMoveIsPossible;
        }

        public bool SimpleNorthWestMovePossibilityCheck(Board i_Board, Player i_CurrPlayer)
        {
            bool simpleNorthWestMoveIsPossible;
            bool srcAndDestAreBasicallyValid;

            m_DestinationIndex.SetSquareIndices(m_SourceIndex.RowIndex - 1, m_SourceIndex.ColumnIndex - 1);
            srcAndDestAreBasicallyValid = SourceAndDestinationBasicValidation(i_Board, i_CurrPlayer);
            if (srcAndDestAreBasicallyValid)
            {
                simpleNorthWestMoveIsPossible = SimpleNorthWestMoveValidation();
            }

            else
            {
                simpleNorthWestMoveIsPossible = false;
            }

            return simpleNorthWestMoveIsPossible;
        }

        public bool SimpleSouthEastMovePossibilityCheck(Board i_Board, Player i_CurrPlayer)
        {
            bool simpleSouthEastMoveIsPossible;
            bool srcAndDestAreBasicallyValid;

            m_DestinationIndex.SetSquareIndices(m_SourceIndex.RowIndex + 1, m_SourceIndex.ColumnIndex + 1);
            srcAndDestAreBasicallyValid = SourceAndDestinationBasicValidation(i_Board, i_CurrPlayer);
            if (srcAndDestAreBasicallyValid)
            {
                simpleSouthEastMoveIsPossible = SimpleSouthEastMoveValidation();
            }

            else
            {
                simpleSouthEastMoveIsPossible = false;
            }

            return simpleSouthEastMoveIsPossible;
        }

        public bool SimpleSouthWestMovePossibilityCheck(Board i_Board, Player i_CurrPlayer)
        {
            bool simpleSouthWestMoveIsPossible;
            bool srcAndDestAreBasicallyValid;

            m_DestinationIndex.SetSquareIndices(m_SourceIndex.RowIndex + 1, m_SourceIndex.ColumnIndex - 1);
            srcAndDestAreBasicallyValid = SourceAndDestinationBasicValidation(i_Board, i_CurrPlayer);
            if (srcAndDestAreBasicallyValid)
            {
                simpleSouthWestMoveIsPossible = SimpleSouthWestMoveValidation();
            }

            else
            {
                simpleSouthWestMoveIsPossible = false;
            }

            return simpleSouthWestMoveIsPossible;
        }

        public bool AnyEatingMovePossibiltyCheck(Board i_Board, Player i_CurrPlayer)
        {
            bool eatingMoveIsPossible;
            bool eatingMoveForwardIsPossible;
            bool eatingMoveBackwardsIsPossible;

            eatingMoveForwardIsPossible = AnyEatingForwardMovePossibilityCheck(i_Board, i_CurrPlayer);
            if (eatingMoveForwardIsPossible)
            {
                eatingMoveIsPossible = true;
            }

            else if (i_Board[m_SourceIndex].DiscType == i_CurrPlayer.KingDiscType)
            {
                eatingMoveIsPossible = eatingMoveBackwardsIsPossible = AnyEatingBackwardsMovePossibilityCheck(i_Board, i_CurrPlayer);
            }

            else
            {
                eatingMoveIsPossible = false;
            }

            return eatingMoveIsPossible;

        }

        public bool AnyEatingForwardMovePossibilityCheck(Board i_Board, Player i_CurrPlayer)
        {
            bool eatingMoveForwardIsPossible;

            if (i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Up)
            {
                eatingMoveForwardIsPossible = EatingNorthEastMovePossibiltyCheck(i_Board, i_CurrPlayer) || EatingNorthWestMovePossibiltyCheck(i_Board, i_CurrPlayer);
            }

            else //(i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Down)
            {
                eatingMoveForwardIsPossible = EatingSouthEastMovePossibiltyCheck(i_Board, i_CurrPlayer) || EatingSouthWestMovePossibiltyCheck(i_Board, i_CurrPlayer);
            }

            return eatingMoveForwardIsPossible;
        }

        public bool AnyEatingBackwardsMovePossibilityCheck(Board i_Board, Player i_CurrPlayer)
        {
            bool eatingMoveBackwardsIsPossible;

            if (i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Up)
            {
                eatingMoveBackwardsIsPossible = EatingSouthEastMovePossibiltyCheck(i_Board, i_CurrPlayer) || EatingSouthWestMovePossibiltyCheck(i_Board, i_CurrPlayer);
            }

            else //(i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Down)
            {
                eatingMoveBackwardsIsPossible = EatingNorthEastMovePossibiltyCheck(i_Board, i_CurrPlayer) || EatingNorthWestMovePossibiltyCheck(i_Board, i_CurrPlayer);
            }

            return eatingMoveBackwardsIsPossible;
        }

        public bool EatingNorthEastMovePossibiltyCheck(Board i_Board, Player i_CurrPlayer)
        {
            bool eatingNorthEastIsPossible;
            bool srcAndDestAreBasicallyValid;

            m_DestinationIndex.SetSquareIndices(m_SourceIndex.RowIndex - 2, m_SourceIndex.ColumnIndex + 2);
            srcAndDestAreBasicallyValid = SourceAndDestinationBasicValidation(i_Board, i_CurrPlayer);
            if (srcAndDestAreBasicallyValid)
            {
                eatingNorthEastIsPossible = EatingNorthEastMoveValidation(i_Board, i_CurrPlayer);
            }

            else
            {
                eatingNorthEastIsPossible = false;
            }

            return eatingNorthEastIsPossible;
        }
       
        public bool EatingNorthWestMovePossibiltyCheck(Board i_Board, Player i_CurrPlayer)
        {
            bool eatingNorthWestIsPossible;
            bool srcAndDestAreBasicallyValid;

            m_DestinationIndex.SetSquareIndices(m_SourceIndex.RowIndex - 2, m_SourceIndex.ColumnIndex - 2);
            srcAndDestAreBasicallyValid = SourceAndDestinationBasicValidation(i_Board, i_CurrPlayer);
            if (srcAndDestAreBasicallyValid)
            {
                eatingNorthWestIsPossible = EatingNorthWestMoveValidation(i_Board, i_CurrPlayer);

            }

            else
            {
                eatingNorthWestIsPossible = false;
            }

            return eatingNorthWestIsPossible;
        }

        public bool EatingSouthEastMovePossibiltyCheck(Board i_Board, Player i_CurrPlayer)
        {
            bool eatingSouthEastIsPossible;
            bool srcAndDestAreBasicallyValid;

            m_DestinationIndex.SetSquareIndices(m_SourceIndex.RowIndex + 2, m_SourceIndex.ColumnIndex + 2);
            srcAndDestAreBasicallyValid = SourceAndDestinationBasicValidation(i_Board, i_CurrPlayer);
            if (srcAndDestAreBasicallyValid)
            {
                eatingSouthEastIsPossible = EatingSouthEastMoveValidation(i_Board, i_CurrPlayer);
            }

            else
            {
                eatingSouthEastIsPossible = false;
            }

            return eatingSouthEastIsPossible;
        }

        public bool EatingSouthWestMovePossibiltyCheck(Board i_Board, Player i_CurrPlayer)
        {
            bool eatingSouthWestIsPossible;
            bool srcAndDestAreBasicallyValid;

            m_DestinationIndex.SetSquareIndices(m_SourceIndex.RowIndex + 2, m_SourceIndex.ColumnIndex - 2);
            srcAndDestAreBasicallyValid = SourceAndDestinationBasicValidation(i_Board, i_CurrPlayer);
            if (srcAndDestAreBasicallyValid)
            {
                eatingSouthWestIsPossible = EatingSouthWestMoveValidation(i_Board, i_CurrPlayer);
            }

            else
            {
                eatingSouthWestIsPossible = false;
            }

            return eatingSouthWestIsPossible;

        }

        public bool RecurringTurnMoveValidation(Board i_Board, Player i_CurrPlayer)
        {
            bool recurringTurnMoveIsValid;
            bool srcAndDestBasicallyValid;
            bool indicesDifferencesAreValid;
            int  indicesDifferences;


            if (m_SourceIndex.IsEqual(m_RecurringTurnNewSourceIndex))
            {
                srcAndDestBasicallyValid = SourceAndDestinationBasicValidation(i_Board, i_CurrPlayer);
                indicesDifferencesAreValid = IndicesDifferencesValidationAndSetup(out indicesDifferences);
                if (srcAndDestBasicallyValid && indicesDifferencesAreValid && indicesDifferences == 2)
                {
                    recurringTurnMoveIsValid = EatingMoveValidation(i_Board, i_CurrPlayer);
                }

                else
                {
                    recurringTurnMoveIsValid = false;
                    m_MoveType = eMoveType.None;
                }
            }

            else
            {
                recurringTurnMoveIsValid = false;
                m_MoveType = eMoveType.None;
            }

            return recurringTurnMoveIsValid;
        }

        public bool AnyMovePossibilityCheck(SquareIndex i_SquareIndex, Board i_Board, Player i_CurrPlayer)
        {
            bool thereOptionToMoveFromIndex;

            /// The recieved index already validated (in Player's HoldingSquareIndices.)
            /// We should check all 8 potential options to move from the received index.
            /// For every option

            m_SourceIndex.CopySquareIndices(i_SquareIndex); /// regular = ? afraid something will be wrong.

            thereOptionToMoveFromIndex = AnySimpleMovePossibiltyCheck(i_Board, i_CurrPlayer) || AnyEatingMovePossibiltyCheck(i_Board, i_CurrPlayer);

            return thereOptionToMoveFromIndex;
        }

        public bool RecurringTurnPossibiltyCheck(Board i_Board, Player i_CurrPlayer)
        {
            bool recurringTurnIsPossible;

            m_SourceIndex.CopySquareIndices(m_DestinationIndex); /// A ref = might be enough but be careful.
            recurringTurnIsPossible = AnyEatingMovePossibiltyCheck(i_Board, i_CurrPlayer);

            return recurringTurnIsPossible;
        }




        /*          bool eatingNorthEastIsPossible;
                    bool eatingNorthWestIsPossible;
                    bool eatingSouthEastIsPossible;
                    bool eatingSouthWestIsPossible;*/

        /// Should be placed outside this method. -> m_NewSourceIndexPostEating = m_DestinationIndex;
        /// Should check for newSourceIndex if there any possibilty for another eating.
        /// CAREFUL!!! handle the data memebrs with extra caution.
        /// 
        /// Will know to ask only for switch case 2 because we sending only + 2,2 Indicies.
        /// Or send directly to Basic Validation and EatingMoveValidation directions
        /// Check SrcAndDestBasicValidation + EatingMoveNotrhEastValidation + Player Direction + PlayerDiscType - Important!!!!!


        /*
                    if (i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Up)
                    {
                        eatingNorthEastIsPossible = EatingNorthEastPossibiltyCheck(i_Board, i_CurrPlayer);
                        eatingNorthWestIsPossible = EatingNorthWestPossibiltyCheck(i_Board, i_CurrPlayer);
                        if (eatingNorthEastIsPossible || eatingNorthWestIsPossible)
                        {
                            eatingMoveIsPossible = true;
                            /// The m_SourceIndex is the one we checked with the possibily, so if so, save it for later.
                        }

                        else if (i_Board[m_SourceIndex].DiscType == i_CurrPlayer.KingDiscType) /// Check If KingDiscType
                        {
                            eatingSouthEastIsPossible = EatingSouthEastPossibiltyCheck(i_Board, i_CurrPlayer);
                            eatingSouthWestIsPossible = EatingSouthWestPossibiltyCheck(i_Board, i_CurrPlayer);
                            eatingMoveIsPossible = eatingSouthEastIsPossible || eatingSouthWestIsPossible;
                        }

                        else
                        {
                            eatingMoveIsPossible = false;
                        }

                    }

                    else //(i_CurrPlayer.MovingDirection == ePlayerMovingDirection.Down)
                    {
                        eatingSouthEastIsPossible = EatingSouthEastPossibiltyCheck(i_Board, i_CurrPlayer);
                        eatingSouthWestIsPossible = EatingSouthWestPossibiltyCheck(i_Board, i_CurrPlayer);
                        if (eatingSouthEastIsPossible || eatingSouthWestIsPossible)
                        {
                            eatingMoveIsPossible = true;
                        }

                        else if (i_Board[m_SourceIndex].DiscType == i_CurrPlayer.KingDiscType)
                        {
                            eatingNorthEastIsPossible = EatingNorthEastPossibiltyCheck(i_Board, i_CurrPlayer);
                            eatingNorthWestIsPossible = EatingNorthWestPossibiltyCheck(i_Board, i_CurrPlayer);
                            eatingMoveIsPossible = eatingNorthEastIsPossible || eatingNorthWestIsPossible;
                        }

                        else
                        {
                            eatingMoveIsPossible = false;
                        }
                    }

                    if (eatingMoveIsPossible)
                    {
                        m_RecurringTurnNewSourceIndex.CopySquareIndices(m_SourceIndex);
                    }
        */

    }
}




