using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersGame
{
    public class Player
    {
        /*private class MoveOption
        {
            SquareIndex m_SourceIndex;
            SquareIndex m_DestinationIndex;
        } */

        private StringBuilder m_Name;
        private int m_NumOfDiscs;
        private int m_Score;
        private eDiscType m_DiscType; //X, O
        private eDiscType m_KingDiscType; //K, U
        private ePlayerType m_PlayerType;
        private ePlayerMovingDirection m_MovingDirection;
        private ePlayerRecognition m_PlayerRecognition;
        private List<SquareIndex> m_CurrentHoldingSquareIndices;

        //private List<MoveOption> m_PlayerPossibleMoves;

        public Player()
        {
            m_Name = new StringBuilder(0, 20);
            m_NumOfDiscs = 0;
            m_PlayerType = ePlayerType.Human;
            m_DiscType = eDiscType.None;
            m_CurrentHoldingSquareIndices = new List<SquareIndex>();
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

        public ePlayerType PlayerType
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
            /// m_CurrentHoldingSquareIndices.Remove(i_MovedFromSquareIndex); /// not working!!!
            foreach (SquareIndex sqrInd in m_CurrentHoldingSquareIndices)
            {
                if (sqrInd.IsEqual(i_SquareIndexToRemove))
                {
                    m_CurrentHoldingSquareIndices.Remove(sqrInd);
                    break;
                }
            }
        }

        /* public bool AnyMovePossibilyValidation(ref Board i_Board, ref MoveManager i_MoveManager)
         {
             foreach (SquareIndex sqrIndex in m_CurrentHoldingIndices)
             {
                 i_MoveManager.MoveFromOptionValiidation(m_DiscType, m_KingDiscType, i_Board[sqrIndex]); //How to pass *this* object by ref?
             }
         }*/

    }
}
