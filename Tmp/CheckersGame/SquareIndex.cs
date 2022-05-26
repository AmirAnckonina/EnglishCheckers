using System;
using System.Text;

namespace CheckersGame
{
    public struct SquareIndex
    {
        private int m_RowIdx;  
        private int m_ColumnIdx;

        public SquareIndex(int i_RowIdx, int i_ColumnIdx)
        {
            m_RowIdx = i_RowIdx;
            m_ColumnIdx = i_ColumnIdx;
        }

        public SquareIndex(SquareIndex i_OtherSquareIndex)
        {
            m_RowIdx = i_OtherSquareIndex.RowIdx;
            m_ColumnIdx = i_OtherSquareIndex.ColumnIdx;
        }

        public int RowIdx
        {
            get
            {
                return m_RowIdx;
            }

            set 
            {
                m_RowIdx = value; 
            }
        }

        public int ColumnIdx
        {
            get 
            {
                return m_ColumnIdx;
            }

            set 
            {
                m_ColumnIdx = value; 
            }
        }

        public void SetSquareIndices(int i_NewRowIndex, int i_NewColumnIndex)
        {
            m_RowIdx = i_NewRowIndex;
            m_ColumnIdx = i_NewColumnIndex;
        }

        public void CopySquareIndices(SquareIndex i_OtherSquareIndex)
        {
            m_RowIdx = i_OtherSquareIndex.RowIdx;
            m_ColumnIdx = i_OtherSquareIndex.ColumnIdx;
        }
    }
}
