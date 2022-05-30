using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using CheckersGame;


namespace CheckersUI
{
    public static class SquareIndexPointConverter
    {
        public static Point SquareIndexToPoint(SquareIndex i_SqrIdx)
        {
            Point newPoint = new Point(i_SqrIdx.ColumnIdx, i_SqrIdx.RowIdx);

            return newPoint;
        }

        public static SquareIndex PointToSquareIndex(Point i_Point)
        {
            SquareIndex newSqrIdx = new SquareIndex(i_Point.Y, i_Point.X);

            return newSqrIdx;
        }
    }
}
