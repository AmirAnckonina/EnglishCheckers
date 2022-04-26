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

        private readonly StringBuilder r_Name;
        private readonly eDiscType r_DiscType; 
        private readonly eDiscType r_KingDiscType; 
        private readonly Player.ePlayerType r_PlayerType;
        private readonly ePlayerMovingDirection r_MovingDirection;
        private readonly ePlayerRecognition r_PlayerRecognition;
        private int m_NumOfDiscs;
        private int m_Score;
        private List<SquareIndex> m_CurrentHoldingSquareIndices;
        
        public Player(StringBuilder i_Name, eDiscType i_DiscType, eDiscType i_KingDiscType, ePlayerType i_PlayerType,
                        ePlayerMovingDirection i_PlayerMovingDirection, ePlayerRecognition i_PlayerRecognition)
        {
            r_Name = i_Name;
            r_DiscType = i_DiscType;
            r_KingDiscType = i_KingDiscType;
            r_PlayerType = i_PlayerType;
            r_MovingDirection = i_PlayerMovingDirection;
            r_PlayerRecognition = i_PlayerRecognition;
            m_NumOfDiscs = 0;
            m_Score = 0;
            m_CurrentHoldingSquareIndices = new List<SquareIndex>();
        }

        /*public Player()
        {
            r_Name = new StringBuilder(0, 20);
            r_PlayerType = Player.ePlayerType.Human;
            r_DiscType = eDiscType.None;
            m_NumOfDiscs = 0;
            m_Score = 0;
            m_CurrentHoldingSquareIndices = new List<SquareIndex>();
        }*/

        public StringBuilder Name
        {
            get 
            {
                return r_Name;
            }

/*            set
            {
                m_Name = value; 
            }*/
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
                return r_PlayerType;
            }

/*            set
            {
                r_PlayerType = value; 
            }*/
        }

        public eDiscType DiscType
        {
            get
            {
                return r_DiscType; 
            }

/*            set 
            {
                r_DiscType = value; 
            }*/
        }

        public eDiscType KingDiscType
        {
            get 
            { 
                return r_KingDiscType; 
            }

/*            set
            {
                r_KingDiscType = value;
            }*/
        }

        public ePlayerMovingDirection MovingDirection
        {
            get
            {
                return r_MovingDirection;
            }

/*            set 
            {
                r_MovingDirection = value; 
            }*/
        }

        public ePlayerRecognition PlayerRecognition
        {
            get 
            {
                return r_PlayerRecognition; 
            }

 /*           set
            {
                r_PlayerRecognition = value; 
            }*/
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
                if (currSquare.SquareHolder == r_PlayerRecognition)
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
                if (i_Board[currSquareIndex].DiscType == r_KingDiscType)
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
