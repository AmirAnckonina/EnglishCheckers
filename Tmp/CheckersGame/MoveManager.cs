using System;
using System.Text;

namespace CheckersGame
{
    public class MoveManager
    {
        public enum eMoveType
        {
            SimpleNorthEastMove,
            SimpleNorthWestMove,
            SimpleSouthEastMove,
            SimpleSouthWestMove,
            EatingNorthEastMove,
            EatingNorthWestMove,
            EatingSouthEastMove,
            EatingSouthWestMove,
            None
        }

        private const int k_InvalidIndicesDifferences = -1;
        private SquareIndex m_SrcIdx;
        private SquareIndex m_DestIdx;
        private SquareIndex m_EatedSquareIdx;
        private SquareIndex m_RecurringTurnNewSrcIdx;
        private eMoveType m_MoveType;
        private bool m_OnlyEatingMoveIsValid;

        public event EventHandler MoveExecuted;

        public MoveManager()
        {
            m_SrcIdx = new SquareIndex();
            m_DestIdx = new SquareIndex();
            m_RecurringTurnNewSrcIdx = new SquareIndex();
            m_EatedSquareIdx = new SquareIndex();
            m_MoveType = eMoveType.None;
        }

        public SquareIndex SrcIdx
        {
            get
            {
                return m_SrcIdx;
            }

            set
            {
                m_SrcIdx = value;
            }
        }

        public SquareIndex DestIdx
        {
            get
            {
                return m_DestIdx;
            }
            set
            {
                m_DestIdx = value;
            }
        }

        public SquareIndex RecurringTurnNewSrcIdx
        {
            get
            {
                return m_RecurringTurnNewSrcIdx;
            }

            set
            {
                m_RecurringTurnNewSrcIdx = value;
            }
        }

        public SquareIndex EatedSquareIdx
        {
            get
            {
                return m_EatedSquareIdx;
            }

            set
            {
                m_EatedSquareIdx = value;
            }
        }

        public bool OnlyEatingIsValid
        {
            get
            {
                return m_OnlyEatingMoveIsValid;
            }

            set
            {
                m_OnlyEatingMoveIsValid = value;
            }
        }

        public void LoadNewPotentialMove(PotentialMove i_PotentialMove)
        {
            m_SrcIdx = i_PotentialMove.SrcIdx;
            m_DestIdx = i_PotentialMove.DestIdx;
        }

        /// In the following method we validate if the player can move. 
        /// The inidces already loaded to the object when the function called
        /// The validations goes from the basic ones to the severe.
        public bool MoveValidation(Board i_Board, Player i_CurrPlayer)
        {
            bool moveIsValid;
            bool srcAndDestBasicallyValid;
            bool indicesDifferencesAreValid;
            int indicesDifferences;

            srcAndDestBasicallyValid = SourceAndDestinationBasicValidation(i_Board, i_CurrPlayer);
            indicesDifferencesAreValid = IndicesDifferencesValidationAndSetup(out indicesDifferences);
            if (srcAndDestBasicallyValid && indicesDifferencesAreValid)
            {
                if (m_OnlyEatingMoveIsValid)
                {
                    if (indicesDifferences == 2 && EatingMoveValidation(i_Board, i_CurrPlayer))
                    {
                        moveIsValid = true;
                    }

                    else
                    {
                        moveIsValid = false;
                    }
                }

                else if (indicesDifferences == 1 && SimpleMoveValidation(i_Board, i_CurrPlayer))
                {
                    moveIsValid = true;
                }

                else
                {
                    moveIsValid = false;
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
                o_IndicesDifference = k_InvalidIndicesDifferences;
            }

            return indicesDifferencesAreValid;
        }

        public int CalculateRowIndicesDifference()
        {
            int rowIndicesDifferenceResult;

            rowIndicesDifferenceResult = Math.Abs(m_SrcIdx.RowIdx - m_DestIdx.RowIdx);

            return rowIndicesDifferenceResult;
        }

        public int CalculateColumnIndicesDifference()
        {
            int columnIndicesDifferenceResult;

            columnIndicesDifferenceResult = Math.Abs(m_SrcIdx.ColumnIdx - m_DestIdx.ColumnIdx);

            return columnIndicesDifferenceResult;
        }

        public bool SimpleMoveValidation(Board i_Board, Player i_CurrPlayer)
        {
            bool simpleMoveIsValid;
            bool simpleMoveForwardIsValid;

            simpleMoveForwardIsValid = SimpleForwardMoveValidation(i_CurrPlayer);
            if (simpleMoveForwardIsValid)
            {
                simpleMoveIsValid = true;
            }

            /// If the sourceIndex contain a KingDiscType.
            else if (i_Board[m_SrcIdx].DiscType == i_CurrPlayer.KingDiscType)
            {
                simpleMoveIsValid = SimpleBackwardsMoveValidation(i_CurrPlayer);
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

            if (i_CurrPlayer.MovingDirection == Player.ePlayerMovingDirection.Up)
            {
                simpleMoveForwardIsValid = SimpleNorthEastMoveValidation() || SimpleNorthWestMoveValidation();
            }

            else /// (i_CurrPlayer.MovingDirection == Player.ePlayerMovingDirection.Down)
            {
                simpleMoveForwardIsValid = SimpleSouthEastMoveValidation() || SimpleSouthWestMoveValidation();
            }

            return simpleMoveForwardIsValid;
        }

        public bool SimpleBackwardsMoveValidation(Player i_CurrPlayer)
        {
            bool simpleMoveBackwardsIsValid;

            if (i_CurrPlayer.MovingDirection == Player.ePlayerMovingDirection.Up)
            {
                simpleMoveBackwardsIsValid = SimpleSouthEastMoveValidation() || SimpleSouthWestMoveValidation();
            }

            else /// (i_CurrPlayer.MovingDirection == Player.ePlayerMovingDirection.Down)
            {
                simpleMoveBackwardsIsValid = SimpleNorthEastMoveValidation() || SimpleNorthWestMoveValidation();
            }

            return simpleMoveBackwardsIsValid;
        }

        public bool SimpleNorthEastMoveValidation()
        {
            bool simpleNorthEastMoveIsValid;
            SquareIndex northEastSquareIndex = new SquareIndex(m_SrcIdx.RowIdx - 1, m_SrcIdx.ColumnIdx + 1);

            if (m_DestIdx.Equals(northEastSquareIndex))
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
        }

        public bool SimpleNorthWestMoveValidation()
        {
            bool simpleNorthWestMoveIsValid;
            SquareIndex northWestSquareIndex = new SquareIndex(m_SrcIdx.RowIdx - 1, m_SrcIdx.ColumnIdx - 1);

            if (m_DestIdx.Equals(northWestSquareIndex))
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
            SquareIndex southEastSquareIndex = new SquareIndex(m_SrcIdx.RowIdx + 1, m_SrcIdx.ColumnIdx + 1);

            if (m_DestIdx.Equals(southEastSquareIndex))
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
            SquareIndex southWestSquareIndex = new SquareIndex(m_SrcIdx.RowIdx + 1, m_SrcIdx.ColumnIdx - 1);

            if (m_DestIdx.Equals(southWestSquareIndex))
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

            eatingMoveForwardIsValid = EatingForwardMoveValidation(i_Board, i_CurrPlayer);
            if (eatingMoveForwardIsValid)
            {
                eatingMoveIsValid = true;
            }

            else if (i_Board[m_SrcIdx].DiscType == i_CurrPlayer.KingDiscType) //The sourceIndex contain a KingDiscType.
            {
                eatingMoveIsValid = EatingBackwardsMoveValidation(i_Board, i_CurrPlayer);
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

            if (i_CurrPlayer.MovingDirection == Player.ePlayerMovingDirection.Up)
            {
                eatingMoveForwardIsValid = EatingNorthEastMoveValidation(i_Board, i_CurrPlayer) || EatingNorthWestMoveValidation(i_Board, i_CurrPlayer);
            }

            else //(i_CurrPlayer.MovingDirection == Player.ePlayerMovingDirection.Down)
            {
                eatingMoveForwardIsValid = EatingSouthEastMoveValidation(i_Board, i_CurrPlayer) || EatingSouthWestMoveValidation(i_Board, i_CurrPlayer);
            }

            return eatingMoveForwardIsValid;
        }

        public bool EatingBackwardsMoveValidation(Board i_Board, Player i_CurrPlayer)
        {
            bool eatingMoveBackwardsIsValid;

            if (i_CurrPlayer.MovingDirection == Player.ePlayerMovingDirection.Up)
            {
                eatingMoveBackwardsIsValid = EatingSouthEastMoveValidation(i_Board, i_CurrPlayer) || EatingSouthWestMoveValidation(i_Board, i_CurrPlayer);
            }

            else //(i_CurrPlayer.MovingDirection == Player.ePlayerMovingDirection.Down)
            {
                eatingMoveBackwardsIsValid = EatingNorthEastMoveValidation(i_Board, i_CurrPlayer) || EatingNorthWestMoveValidation(i_Board, i_CurrPlayer);
            }

            return eatingMoveBackwardsIsValid;
        }

        public bool EatingNorthEastMoveValidation(Board i_Board, Player i_CurrPlayer)
        {
            bool eatingNorthEastMoveIsValid;
            SquareIndex northEastSquareIndex = new SquareIndex(m_SrcIdx.RowIdx - 2, m_SrcIdx.ColumnIdx + 2);
            SquareIndex northEastPotentialEatedSquareIndex;

            if (m_DestIdx.Equals(northEastSquareIndex))
            {
                northEastPotentialEatedSquareIndex = new SquareIndex(m_SrcIdx.RowIdx - 1, m_SrcIdx.ColumnIdx + 1);
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
            SquareIndex northWestSquareIndex = new SquareIndex(m_SrcIdx.RowIdx - 2, m_SrcIdx.ColumnIdx - 2);
            SquareIndex northWestPotentialEatedSquareIndex;

            /// Two Validation: 1. Indices are matching 2. Rival is in between
            if (m_DestIdx.Equals(northWestSquareIndex))
            {
                northWestPotentialEatedSquareIndex = new SquareIndex(m_SrcIdx.RowIdx - 1, m_SrcIdx.ColumnIdx - 1);
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
            SquareIndex southEastSquareIndex = new SquareIndex(m_SrcIdx.RowIdx + 2, m_SrcIdx.ColumnIdx + 2);
            SquareIndex southEastPotentialEatedSquareIndex;

            if (m_DestIdx.Equals(southEastSquareIndex))
            {
                southEastPotentialEatedSquareIndex = new SquareIndex(m_SrcIdx.RowIdx + 1, m_SrcIdx.ColumnIdx + 1);
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
            SquareIndex southWestSquareIndex = new SquareIndex(m_SrcIdx.RowIdx + 2, m_SrcIdx.ColumnIdx - 2);
            SquareIndex southWestPotentialEatedSquareIndex;

            if (m_DestIdx.Equals(southWestSquareIndex))
            {
                southWestPotentialEatedSquareIndex = new SquareIndex(m_SrcIdx.RowIdx + 1, m_SrcIdx.ColumnIdx - 1);
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
                sourceIsValid = SourceValidation(i_Board[m_SrcIdx], i_CurrPlayer);
                destinationIsVacantAndLegal = DestinationVacancyAndLegalityValidation(i_Board[m_DestIdx]);
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

            sourceIsExist = i_Board.SquareExistenceValidation(m_SrcIdx);
            destinationIsExist = i_Board.SquareExistenceValidation(m_DestIdx);
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
            GameLogic.eDiscType currDestinationDiscType;
            bool indexIsLegal;

            currDestinationDiscType = i_DestinationSquare.DiscType;
            indexIsLegal = i_DestinationSquare.LegalSquare;
            if (currDestinationDiscType == GameLogic.eDiscType.None && indexIsLegal)
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
            BasicMoveProcedure(io_Board, i_CurrPlayer);
            if (EatingMoveOccurred())
            {
                EatingMoveProcedure(io_Board);
            }

            /// MoveExecutedEventArgs XXX = 
            /// OnMoveExecuted(XXX)
            /// Report BoardUpdated.
        }

      /*  protected virtual void OnMoveExecuted(MoveExecutedEventArgs i_MUEventArgs)
        {
            if (!=null)
            {
                MoveExecuted.Invoke(this, i_MUEventArgs);
            }
        }*/

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

        // $G$ CSS-013 (-3) Input parameters names should start with i_. why using here io_ ?
        public void BasicMoveProcedure(Board io_Board, Player i_CurrPlayer)
        {
            io_Board[m_DestIdx].DiscType = io_Board[m_SrcIdx].DiscType;
            io_Board[m_DestIdx].SquareHolder = i_CurrPlayer.PlayerRecognition;
            io_Board[m_SrcIdx].DiscType = GameLogic.eDiscType.None;
            io_Board[m_SrcIdx].SquareHolder = Player.ePlayerRecognition.None;
        }
        
        // $G$ CSS-013 (-3) Input parameters names should start with i_. why using here io_ ?
        public void EatingMoveProcedure(Board io_Board)
        {
            switch (m_MoveType)
            {
                case eMoveType.EatingNorthEastMove:
                    io_Board[m_SrcIdx.RowIdx - 1, m_SrcIdx.ColumnIdx + 1].DiscType = GameLogic.eDiscType.None;
                    io_Board[m_SrcIdx.RowIdx - 1, m_SrcIdx.ColumnIdx + 1].SquareHolder = Player.ePlayerRecognition.None;
                    m_EatedSquareIdx.SetSquareIndices(m_SrcIdx.RowIdx - 1, m_SrcIdx.ColumnIdx + 1);
                    break;

                case eMoveType.EatingNorthWestMove:
                    io_Board[m_SrcIdx.RowIdx - 1, m_SrcIdx.ColumnIdx - 1].DiscType = GameLogic.eDiscType.None;
                    io_Board[m_SrcIdx.RowIdx - 1, m_SrcIdx.ColumnIdx - 1].SquareHolder = Player.ePlayerRecognition.None;
                    m_EatedSquareIdx.SetSquareIndices(m_SrcIdx.RowIdx - 1, m_SrcIdx.ColumnIdx - 1);
                    break;

                case eMoveType.EatingSouthEastMove:
                    io_Board[m_SrcIdx.RowIdx + 1, m_SrcIdx.ColumnIdx + 1].DiscType = GameLogic.eDiscType.None;
                    io_Board[m_SrcIdx.RowIdx + 1, m_SrcIdx.ColumnIdx + 1].SquareHolder = Player.ePlayerRecognition.None;
                    m_EatedSquareIdx.SetSquareIndices(m_SrcIdx.RowIdx + 1, m_SrcIdx.ColumnIdx + 1);
                    break;

                case eMoveType.EatingSouthWestMove:
                    io_Board[m_SrcIdx.RowIdx + 1, m_SrcIdx.ColumnIdx - 1].DiscType = GameLogic.eDiscType.None;
                    io_Board[m_SrcIdx.RowIdx + 1, m_SrcIdx.ColumnIdx - 1].SquareHolder = Player.ePlayerRecognition.None;
                    m_EatedSquareIdx.SetSquareIndices(m_SrcIdx.RowIdx + 1, m_SrcIdx.ColumnIdx - 1);
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
                io_Board[m_DestIdx].DiscType = i_CurrPlayer.KingDiscType;
            }
        }

        public bool FirstPlayerReachedLastLineValidation(Player i_CurrPlayer, Board i_Board)
        {
            bool firstPlayerReachedLastLine;

            if (i_CurrPlayer.MovingDirection == Player.ePlayerMovingDirection.Down && m_DestIdx.RowIdx == i_Board.BoardSize - 1)
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

            if (i_CurrPlayer.MovingDirection == Player.ePlayerMovingDirection.Up && m_DestIdx.RowIdx == 0)
            {
                secondPlayerReachedLastLine = true;
            }

            else
            {
                secondPlayerReachedLastLine = false;
            }

            return secondPlayerReachedLastLine;
        }

        public bool AnySimpleMovePossibiltyCheck(SquareIndex i_SquareIndex, Board i_Board, Player i_CurrPlayer)
        {
            bool simpleMoveIsPossible;
            bool simpleMoveForwardIsPossible;

            m_SrcIdx.CopySquareIndices(i_SquareIndex);
            simpleMoveForwardIsPossible = AnySimpleForwardMovePossibilityCheck(i_Board, i_CurrPlayer);
            if (simpleMoveForwardIsPossible)
            {
                simpleMoveIsPossible = true;
            }

            else if (i_Board[m_SrcIdx].DiscType == i_CurrPlayer.KingDiscType)
            {
                simpleMoveIsPossible = AnySimpleBackwardsMovePossibilityCheck(i_Board, i_CurrPlayer);
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

            if (i_CurrPlayer.MovingDirection == Player.ePlayerMovingDirection.Up)
            {
                simpleMoveForwardIsPossible = SimpleNorthEastMovePossibilityCheck(i_Board, i_CurrPlayer) || SimpleNorthWestMovePossibilityCheck(i_Board, i_CurrPlayer);
            }

            else //(i_CurrPlayer.MovingDirection == Player.ePlayerMovingDirection.Down)
            {
                simpleMoveForwardIsPossible = SimpleSouthEastMovePossibilityCheck(i_Board, i_CurrPlayer) || SimpleSouthWestMovePossibilityCheck(i_Board, i_CurrPlayer);
            }

            return simpleMoveForwardIsPossible;
        }

        public bool AnySimpleBackwardsMovePossibilityCheck(Board i_Board, Player i_CurrPlayer)
        {
            bool simpleMoveBackwardsIsPossible;

            if (i_CurrPlayer.MovingDirection == Player.ePlayerMovingDirection.Up)
            {
                simpleMoveBackwardsIsPossible = SimpleSouthEastMovePossibilityCheck(i_Board, i_CurrPlayer) || SimpleSouthWestMovePossibilityCheck(i_Board, i_CurrPlayer);
            }

            else //(i_CurrPlayer.MovingDirection == Player.ePlayerMovingDirection.Down)
            {
                simpleMoveBackwardsIsPossible = SimpleNorthEastMovePossibilityCheck(i_Board, i_CurrPlayer) || SimpleNorthWestMovePossibilityCheck(i_Board, i_CurrPlayer);
            }

            return simpleMoveBackwardsIsPossible;
        }

        public bool SimpleNorthEastMovePossibilityCheck(Board i_Board, Player i_CurrPlayer)
        {
            bool simpleNorthEastMoveIsPossible;
            bool srcAndDestAreBasicallyValid;

            m_DestIdx.SetSquareIndices(m_SrcIdx.RowIdx - 1, m_SrcIdx.ColumnIdx + 1);
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

            m_DestIdx.SetSquareIndices(m_SrcIdx.RowIdx - 1, m_SrcIdx.ColumnIdx - 1);
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

            m_DestIdx.SetSquareIndices(m_SrcIdx.RowIdx + 1, m_SrcIdx.ColumnIdx + 1);
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

            m_DestIdx.SetSquareIndices(m_SrcIdx.RowIdx + 1, m_SrcIdx.ColumnIdx - 1);
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

        public bool AnyEatingMovePossibiltyCheckByIndex(SquareIndex i_PotentialSourceSquareIndex, Board i_Board, Player i_CurrPlayer)
        {
            bool eatingMoveIsPossible;
            bool eatingMoveForwardIsPossible;

            m_SrcIdx.CopySquareIndices(i_PotentialSourceSquareIndex);
            eatingMoveForwardIsPossible = AnyEatingForwardMovePossibilityCheck(i_Board, i_CurrPlayer);
            if (eatingMoveForwardIsPossible)
            {
                eatingMoveIsPossible = true;
            }

            else if (i_Board[m_SrcIdx].DiscType == i_CurrPlayer.KingDiscType)
            {
                eatingMoveIsPossible = AnyEatingBackwardsMovePossibilityCheck(i_Board, i_CurrPlayer);
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

            if (i_CurrPlayer.MovingDirection == Player.ePlayerMovingDirection.Up)
            {
                eatingMoveForwardIsPossible = EatingNorthEastMovePossibiltyCheck(i_Board, i_CurrPlayer) || EatingNorthWestMovePossibiltyCheck(i_Board, i_CurrPlayer);
            }

            else //(i_CurrPlayer.MovingDirection == Player.ePlayerMovingDirection.Down)
            {
                eatingMoveForwardIsPossible = EatingSouthEastMovePossibiltyCheck(i_Board, i_CurrPlayer) || EatingSouthWestMovePossibiltyCheck(i_Board, i_CurrPlayer);
            }

            return eatingMoveForwardIsPossible;
        }

        public bool AnyEatingBackwardsMovePossibilityCheck(Board i_Board, Player i_CurrPlayer)
        {
            bool eatingMoveBackwardsIsPossible;
            if (i_CurrPlayer.MovingDirection == Player.ePlayerMovingDirection.Up)
            {
                eatingMoveBackwardsIsPossible = EatingSouthEastMovePossibiltyCheck(i_Board, i_CurrPlayer) || EatingSouthWestMovePossibiltyCheck(i_Board, i_CurrPlayer);
            }

            else //(i_CurrPlayer.MovingDirection == Player.ePlayerMovingDirection.Down)
            {
                eatingMoveBackwardsIsPossible = EatingNorthEastMovePossibiltyCheck(i_Board, i_CurrPlayer) || EatingNorthWestMovePossibiltyCheck(i_Board, i_CurrPlayer);
            }

            return eatingMoveBackwardsIsPossible;
        }

        public bool EatingNorthEastMovePossibiltyCheck(Board i_Board, Player i_CurrPlayer)
        {
            bool eatingNorthEastIsPossible;
            bool srcAndDestAreBasicallyValid;

            m_DestIdx.SetSquareIndices(m_SrcIdx.RowIdx - 2, m_SrcIdx.ColumnIdx + 2);
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

            m_DestIdx.SetSquareIndices(m_SrcIdx.RowIdx - 2, m_SrcIdx.ColumnIdx - 2);
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

            m_DestIdx.SetSquareIndices(m_SrcIdx.RowIdx + 2, m_SrcIdx.ColumnIdx + 2);
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

            m_DestIdx.SetSquareIndices(m_SrcIdx.RowIdx + 2, m_SrcIdx.ColumnIdx - 2);
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
            int indicesDifferences;

            if (m_SrcIdx.Equals(m_RecurringTurnNewSrcIdx))
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

        public bool AnyMovePossibilityCheckByIndex(SquareIndex i_PotentialSourceSquareIndex, Board i_Board, Player i_CurrPlayer)
        {
            bool thereOptionToMoveFromIndex;

            thereOptionToMoveFromIndex = AnySimpleMovePossibiltyCheck(i_PotentialSourceSquareIndex, i_Board, i_CurrPlayer) || AnyEatingMovePossibiltyCheckByIndex(i_PotentialSourceSquareIndex, i_Board, i_CurrPlayer);

            return thereOptionToMoveFromIndex;
        }

        public bool RecurringTurnPossibiltyCheck(SquareIndex i_NewSourceIndexAfterEating, Board i_Board, Player i_CurrPlayer)
        {
            bool recurringTurnIsPossible;

            recurringTurnIsPossible = AnyEatingMovePossibiltyCheckByIndex(i_NewSourceIndexAfterEating, i_Board, i_CurrPlayer);

            return recurringTurnIsPossible;
        }
    }
}




