using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CheckersGame
{
    public class MoveExecutedEventArgs : EventArgs
    {
        private List<PointAndHolder> m_NewOccuipiedPoints;
        private List<PointAndHolder> m_NewEmptyPoints;

        public MoveExecutedEventArgs()
        {
            m_NewOccuipiedPoints = new List<PointAndHolder>();
            m_NewEmptyPoints = new List<PointAndHolder>();
        }

        public List<PointAndHolder> NewOccuipiedPoints
        {
            get
            {
                return m_NewOccuipiedPoints;
            }

            set
            {
                m_NewOccuipiedPoints = value;
            }
        }

        public List<PointAndHolder> NewEmptyPoints
        {
            get
            {
                return m_NewEmptyPoints;
            }

            set
            {
                m_NewEmptyPoints = value;
            }
        }

        public void AddNewEmptyPoint(SquareIndex i_SqrIdx)
        {
            PointAndHolder newEmptyPoint = new PointAndHolder(i_SqrIdx, Player.ePlayerRecognition.None);

            m_NewEmptyPoints.Add(newEmptyPoint);
        }

        public void AddNewOccuipiedPoint(SquareIndex i_SqrIdx, Player.ePlayerRecognition i_PlayerRecognition)
        {
            PointAndHolder newOccuipiedPoint = new PointAndHolder(i_SqrIdx, i_PlayerRecognition);

            m_NewOccuipiedPoints.Add(newOccuipiedPoint);
        }
    
    }
}
