using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CheckersUI
{
    public class PictureBoxClickedEventArgs : EventArgs
    {
        private Point m_pictureBoxIdx;

        public PictureBoxClickedEventArgs(Point i_Point)
        {
            m_pictureBoxIdx = i_Point;
        }

        public Point PictureBoxIdx
        {
            get
            {
                return m_pictureBoxIdx;
            }

            set
            {
                m_pictureBoxIdx = value;
            }
        }

    }
}
