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
        
        public PictureBoxSquare()
        {
            m_PictureBoxSqrIdx = new Point();
        }

        public PictureBoxSquare(int i_RowIdx, int i_ColIdx)
        {
            InitializeComponent();
            m_PictureBoxSqrIdx = new Point(i_RowIdx, i_ColIdx);
            /// this.BackgroundImage = Properties.Resources.EmptyInvalidSquare;
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
            this.Click += new System.EventHandler(this.PictureBoxSquare_Click);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        private void PictureBoxSquare_Click(object sender, EventArgs e)
        {

        }
    }
}
