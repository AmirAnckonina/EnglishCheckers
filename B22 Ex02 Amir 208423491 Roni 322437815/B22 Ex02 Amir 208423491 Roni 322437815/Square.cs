using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B22_Ex02_Amir_208423491_Roni_322437815
{
    public class Square
    {

        private bool m_LegalSquare;
        private eDiscType m_CurrDiscType;  //X,O,K,Q,N
        private SquareIndex m_SquareIndex;

        // private int[] m_SqrIndex;
        /*private int m_RowIndex;
        private int m_ColumnIndex;*/

        public Square(bool i_LegalSquare, eDiscType i_CurrDiscType, int i_RowIndex, int i_ColumnIndex)
        {
            m_LegalSquare = i_LegalSquare;
            m_CurrDiscType = i_CurrDiscType;
            m_SquareIndex = new SquareIndex(i_RowIndex, i_ColumnIndex);

           // m_SqrIndex = new int[2] { i_RowIndex, i_ColumnIndex };
            /*m_RowIndex = i_RowIndex;
            m_ColumnIndex = i_ColumnIndex;*/
        }

        public bool LegalSquare
        {
            get { return m_LegalSquare; }
            set { m_LegalSquare = value; }
        }

        public eDiscType CurrDiscType
        {
            get { return m_CurrDiscType; }
            set { m_CurrDiscType = value; }
        }
       
        public SquareIndex SqrIndex
        {
            get { return m_SquareIndex; }
            set { m_SquareIndex = value; }
        }

    }

    
}
