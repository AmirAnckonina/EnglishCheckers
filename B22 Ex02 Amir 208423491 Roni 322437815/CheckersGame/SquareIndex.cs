using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersGame
{
    public class SquareIndex
    {
        private int m_RowIndex;  //consider internal access modifier ??
        private int m_ColumnIndex;

        public SquareIndex()
        {
            //Empty constructor.
        }

        public SquareIndex(int i_RowIndex, int i_ColumnIndex)
        {
            m_RowIndex = i_RowIndex;
            m_ColumnIndex = i_ColumnIndex;
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

        public void SetSquareIndex(int i_RowIndex, int i_ColumnIndex)
        {
            m_RowIndex = i_RowIndex;
            m_ColumnIndex = i_ColumnIndex;  
        }

        public static bool operator == (SquareIndex i_SquareIndex1, SquareIndex i_SquareIndex2)
        {
            bool isEqual;

            if(i_SquareIndex1.m_RowIndex == i_SquareIndex2.RowIndex && i_SquareIndex1.ColumnIndex == i_SquareIndex2.ColumnIndex)
            {
                isEqual = true;
            }

            else
            {
                isEqual = false;
            }

            return isEqual;
           
        }

        public static bool operator !=(SquareIndex i_SquareIndex1, SquareIndex i_SquareIndex2)
        {
            bool isNotEqual;

            if (i_SquareIndex1.m_RowIndex != i_SquareIndex2.RowIndex && i_SquareIndex1.ColumnIndex != i_SquareIndex2.ColumnIndex)
            {
                isNotEqual = true;
            }

            else
            {
                isNotEqual = false;
            }

            return isNotEqual;
        }


    }
}
