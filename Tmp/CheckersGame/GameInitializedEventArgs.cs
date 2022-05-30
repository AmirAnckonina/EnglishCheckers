using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CheckersGame
{
    public class GameInitializedEventArgs : EventArgs
    {
        private readonly List<Square> m_AllOccuipiedSquares;

        public GameInitializedEventArgs()
        {
            m_AllOccuipiedSquares = new List<Square>();
        }

        public void AddOccuipiedPointToList(Square i_Sqr)
        {
            m_AllOccuipiedSquares.Add(i_Sqr);
        }

        public List<Square> OcciupiedPoints
        {
            get
            {
                return m_AllOccuipiedSquares;
            }
        }
       
    } 
}
