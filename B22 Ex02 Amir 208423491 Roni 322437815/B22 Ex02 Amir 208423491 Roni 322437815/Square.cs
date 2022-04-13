using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B22_Ex02_Amir_208423491_Roni_322437815
{
    public class Square
    {
        private bool m_ValidSquare;
        private char m_CurrDiscType;  //X,O,K,Q

        public Square(bool i_ValidSquare, char i_CurrDiscType)
        {
            m_ValidSquare = i_ValidSquare;
            m_CurrDiscType = i_CurrDiscType;
        }

        public bool ValidSquare
        {
            get { return m_ValidSquare; }
            set { m_ValidSquare = value; }
        }

        public char CurrDiscType
        {
            get { return m_CurrDiscType; }
            set { m_CurrDiscType = value; }
        }
    }

    
}
