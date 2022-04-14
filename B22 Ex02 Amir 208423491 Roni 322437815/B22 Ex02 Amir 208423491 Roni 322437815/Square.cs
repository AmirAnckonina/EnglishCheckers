using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B22_Ex02_Amir_208423491_Roni_322437815
{
    public class Square
    {
        private bool m_ValidSquare;
        private eDiscType m_CurrDiscType;  //X,O,K,Q,N
        private int m_RowIndex;
        private int m_ColumnIndex;

        public Square(bool i_ValidSquare, eDiscType i_CurrDiscType, int i_RowIndex, int i_ColumtIndex)
        {
            m_ValidSquare = i_ValidSquare;
            m_CurrDiscType = i_CurrDiscType;
            m_RowIndex = i_RowIndex;
            m_ColumnIndex = i_ColumtIndex;
        }

        public bool ValidSquare
        {
            get { return m_ValidSquare; }
            set { m_ValidSquare = value; }
        }

        public eDiscType CurrDiscType
        {
            get { return m_CurrDiscType; }
            set { m_CurrDiscType = value; }
        }
    }

    
}
