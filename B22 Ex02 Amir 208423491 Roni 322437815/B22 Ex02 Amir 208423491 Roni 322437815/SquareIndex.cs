using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B22_Ex02_Amir_208423491_Roni_322437815
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
            get { return m_RowIndex; }
            set { m_RowIndex = value; }
        }

        public int ColumnIndex
        {
            get { return m_ColumnIndex; }
            set { m_ColumnIndex = value; }
        }

        public void SetSquareIndex(int i_RowIndex, int i_ColumnIndex)
        {
            m_RowIndex = i_RowIndex;
            m_ColumnIndex = i_ColumnIndex;
        }

    }
}
