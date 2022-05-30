using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CheckersGame
{
    public class MoveExecutedEventArgs : EventArgs
    {
        private List<Square> m_NewOccuipiedSquares;
        private List<Square> m_NewEmptySquares;

        public MoveExecutedEventArgs()
        {
            m_NewOccuipiedSquares = new List<Square>();
            m_NewEmptySquares = new List<Square>();
        }

        public List<Square> NewOccuipiedSquares
        {
            get
            {
                return m_NewOccuipiedSquares;
            }

            set
            {
                m_NewOccuipiedSquares = value;
            }
        }

        public List<Square> NewEmptySquares
        {
            get
            {
                return m_NewEmptySquares;
            }

            set
            {
                m_NewEmptySquares = value;
            }
        }

        public void AddNewEmptyPoint(Square i_Sqr)
        {
            m_NewEmptySquares.Add(i_Sqr);
        }

        public void AddNewOccuipiedPoint(Square i_Sqr)
        {
            m_NewOccuipiedSquares.Add(i_Sqr);
        }
    
    }
}
