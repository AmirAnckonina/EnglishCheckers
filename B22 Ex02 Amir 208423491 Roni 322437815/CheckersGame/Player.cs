using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersGame
{
    public class Player
    {
        public enum ePlayerRecognition
        {
            FirstPlayer = 1,
            SecondPlayer = 2,
            None
        }

        public enum ePlayerMovingDirection
        {
            Up,
            Down,
            None
        }

        public enum ePlayerType
        {
            Computer,
            Human
        }

        private StringBuilder m_Name;
        private int m_NumOfDiscs;
        private int m_Score;
        private eDiscType m_DiscType; 
        private eDiscType m_KingDiscType; 
        private Player.ePlayerType m_PlayerType;
        private ePlayerMovingDirection m_MovingDirection;
        private ePlayerRecognition m_PlayerRecognition;
        private List<SquareIndex> m_CurrentHoldingSquareIndices;

        public Player()
        {
            m_Name = new StringBuilder(0, 20);
            m_CurrentHoldingSquareIndices = new List<SquareIndex>();
            m_PlayerType = Player.ePlayerType.Human;
            m_DiscType = eDiscType.None;
            m_NumOfDiscs = 0;
            m_Score = 0;
        }

        public StringBuilder Name
        {
            get 
            {
                return m_Name;
            }

            set
            {
                m_Name = value; 
            }
        }

        public int NumOfDiscs
        {
            get 
            {
                return m_NumOfDiscs;
            }

            set
            { 
                m_NumOfDiscs = value; 
            }
        }

        public int Score
        {
            get
            {
                return m_Score;
            }

            set
            {
                m_Score = value;
            }
        }

        public Player.ePlayerType PlayerType
        {
            get
            { 
                return m_PlayerType;
            }

            set
            {
                m_PlayerType = value; 
            }
        }

        public eDiscType DiscType
        {
            get
            {
                return m_DiscType; 
            }

            set 
            {
                m_DiscType = value; 
            }
        }

        public eDiscType KingDiscType
        {
            get 
            { 
                return m_KingDiscType; 
            }

            set
            {
                m_KingDiscType = value;
            }
        }

        public ePlayerMovingDirection MovingDirection
        {
            get
            {
                return m_MovingDirection;
            }

            set 
            {
                m_MovingDirection = value; 
            }
        }

        public ePlayerRecognition PlayerRecognition
        {
            get 
            {
                return m_PlayerRecognition; 
            }

            set
            {
                m_PlayerRecognition = value; 
            }
        }

        public List<SquareIndex> CurrentHoldingSquareIndices
        {
            get
            {
                return m_CurrentHoldingSquareIndices;
            }

            set
            {
                m_CurrentHoldingSquareIndices = value;
            }
        }

        public int GetCurrentHoldingSqaureIndicesListLength()
        {
            return m_CurrentHoldingSquareIndices.Count;
        }

        public void InitializeCurrentHoldingIndices(Board i_Board)
        {
            SquareIndex newSquareIndex;

            foreach (Square currSquare in i_Board.GameBoard)
            {
                if (currSquare.SquareHolder == m_PlayerRecognition)
                {
                    newSquareIndex = new SquareIndex(currSquare.SquareIndex);
                    m_CurrentHoldingSquareIndices.Add(newSquareIndex);
                }
            }
        }

        public void UpdateCurrentHoldingSquareIndices(SquareIndex i_MovedFromSquareIndex, SquareIndex i_NewHoldingSquareIndex)
        {
            RemoveIndexFromCurrentHoldingSquareIndices(i_MovedFromSquareIndex);
            AddIndexToCurrentHoldingSquareIndices(i_NewHoldingSquareIndex);
        }

        public void AddIndexToCurrentHoldingSquareIndices(SquareIndex i_SquareIndexToAdd)
        {
            SquareIndex newSquareIndex = new SquareIndex(i_SquareIndexToAdd);
            m_CurrentHoldingSquareIndices.Add(newSquareIndex);
        }

        public void RemoveIndexFromCurrentHoldingSquareIndices(SquareIndex i_SquareIndexToRemove)
        {
            foreach (SquareIndex sqrInd in m_CurrentHoldingSquareIndices)
            {
                if (sqrInd.IsEqual(i_SquareIndexToRemove))
                {
                    m_CurrentHoldingSquareIndices.Remove(sqrInd);
                    break;
                }
            }
        }

        public int CalculatePlayerDiscValuesAfterSingleGame(Board i_Board)
        {
            int totalDiscValues = 0;

            foreach (SquareIndex currSquareIndex in m_CurrentHoldingSquareIndices)
            {
                if (i_Board[currSquareIndex].DiscType == m_KingDiscType)
                {
                    totalDiscValues += 4;
                }

                else 
                {
                    totalDiscValues++;
                }
            }

            return totalDiscValues;
        }
    }
}
