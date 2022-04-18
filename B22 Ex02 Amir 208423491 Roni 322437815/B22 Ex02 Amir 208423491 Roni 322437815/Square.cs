using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B22_Ex02_Amir_208423491_Roni_322437815
{
    public class Square
    {

        private bool m_LegalSquare;
        private eDiscType m_DiscType; 
        private SquareIndex m_SquareIndex;
        private ePlayerRecognition m_SquareHolder;

        public Square(bool i_LegalSquare, eDiscType i_CurrDiscType, int i_RowIndex, int i_ColumnIndex)
        {
            m_LegalSquare = i_LegalSquare;
            m_DiscType = i_CurrDiscType;
            m_SquareIndex = new SquareIndex(i_RowIndex, i_ColumnIndex);

        }

        public bool LegalSquare
        {
            get { return m_LegalSquare; }
            set { m_LegalSquare = value; }
        }

        public eDiscType DiscType
        {
            get { return m_DiscType; }
            set { m_DiscType = value; }
        }
       
        public SquareIndex SqrIndex
        {
            get { return m_SquareIndex; }
            set { m_SquareIndex = value; }
        }

        public ePlayerRecognition SquareHolder
        {
            get { return m_SquareHolder; }
            set { m_SquareHolder = value; }
        }

    }

    
}
