using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CheckersGame
{
    public class GameInitializedEventArgs : EventArgs
    {
        private List<PointAndHolder> m_AllOccuipiedPoints;

        public GameInitializedEventArgs()
        {
            m_AllOccuipiedPoints = new List<PointAndHolder>();
        }

        public void AddOccuipiedPointToList(SquareIndex i_SqrIdx, Player.ePlayerRecognition i_PlayerRecognition)
        {
            PointAndHolder newOccuipiedPoint = new PointAndHolder(i_SqrIdx, i_PlayerRecognition);

            m_AllOccuipiedPoints.Add(newOccuipiedPoint);
        }

        public List<PointAndHolder> OcciupiedPoints
        {
            get
            {
                return m_AllOccuipiedPoints;
            }
        }
       
    } 
}
