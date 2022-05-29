using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace CheckersUI
{
    public class PictureBoxSquare : PictureBox
    {
        private Point m_PictureBoxSqrIdx;

        public PictureBoxSquare(int i_RowIdx, int i_ColIdx)
        {
            InitializeComponent();
            m_PictureBoxSqrIdx = new Point(i_RowIdx, i_ColIdx);
        }

        public Point PictureBoxSqrIdx
        {
            get
            {
                return m_PictureBoxSqrIdx;
            }
        }

        public void SetSquare()
        {
            this.BorderStyle = BorderStyle.None;
            SetEnabledAndBackgroundImage();
            SetLocation();
        }

        private void SetEnabledAndBackgroundImage()
        {
            int squareIndicesParityCalculationResult;

            squareIndicesParityCalculationResult = (m_PictureBoxSqrIdx.Y + m_PictureBoxSqrIdx.X) % 2;
            if (squareIndicesParityCalculationResult == 1)
            {
                this.Enabled = true;
                this.BackgroundImage = Properties.Resources.EmptyValidSquare;
            }

            else /// == 0
            {
                this.Enabled = false;
                this.BackgroundImage = Properties.Resources.EmptyInvalidSquare;
            }
        }

        private void SetLocation()
        {
            Point locationToSet = new Point();

            locationToSet.X = m_PictureBoxSqrIdx.X * FormGameSpecs.k_PictureBoxHeight;
            locationToSet.Y = m_PictureBoxSqrIdx.Y * FormGameSpecs.k_PictureBoxWidth;
            this.Location = locationToSet;
        }

        /*protected override void OnClick(EventArgs e)
        {
            ///PictureBoxClickedEventArgs picBoxEventArgs = new PictureBoxClickedEventArgs(m_PictureBoxSqrIdx);
            /// Add effect of pressed square
            /// 
            //this.BorderStyle = BorderStyle.Fixed3D;
            if (PictureBoxSquareClicked != null)
            {
                PictureBoxSquareClicked.Invoke(this, e);
            *//*}
        }*/

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBoxSquare
            // 
            this.BackgroundImage = global::CheckersUI.Properties.Resources.EmptyInvalidSquare;
            this.InitialImage = null;
            this.Size = new System.Drawing.Size(50, 50);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
