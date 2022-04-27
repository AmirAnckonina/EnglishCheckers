using System;
using System.Text;

namespace CheckersGame
{
    public class SquareIndex
    {
        private int m_RowIndex;  
        private int m_ColumnIndex;

        public SquareIndex()
        {
            /// Empty constructor.
        }

        public SquareIndex(int i_RowIndex, int i_ColumnIndex)
        {
            m_RowIndex = i_RowIndex;
            m_ColumnIndex = i_ColumnIndex;
        }

        public SquareIndex(SquareIndex i_OtherSquareIndex)
        {
            m_RowIndex = i_OtherSquareIndex.RowIndex;
            m_ColumnIndex = i_OtherSquareIndex.ColumnIndex;
        }

        public int RowIndex
        {
            get
            {
                return m_RowIndex;
            }

            set 
            {
                m_RowIndex = value; 
            }
        }

        public int ColumnIndex
        {
            get 
            {
                return m_ColumnIndex;
            }

            set 
            {
                m_ColumnIndex = value; 
            }
        }

        public bool IsEqual(SquareIndex i_SquareIndex)
        {
            bool isEqual;

            if (this.m_RowIndex == i_SquareIndex.RowIndex && this.ColumnIndex == i_SquareIndex.ColumnIndex)
            {
                isEqual = true;
            }

            else
            {
                isEqual = false;
            }

            return isEqual;
        }

        public override bool Equals(object obj)
        {
            return this.IsEqual((SquareIndex)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public void SetSquareIndices(int i_NewRowIndex, int i_NewColumnIndex)
        {
            m_RowIndex = i_NewRowIndex;
            m_ColumnIndex = i_NewColumnIndex;
        }

        public void CopySquareIndices(SquareIndex i_OtherSquareIndex)
        {
            m_RowIndex = i_OtherSquareIndex.RowIndex;
            m_ColumnIndex = i_OtherSquareIndex.ColumnIndex;
        }
    }
}
