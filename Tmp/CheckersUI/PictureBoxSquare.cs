using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using CheckersGame;

namespace CheckersUI
{
    public class PictureBoxSquare : PictureBox
    {
        private Point m_PictureBoxSqrIdx;
        private Player.ePlayerRecognition m_SquareHolder;

        public PictureBoxSquare(int i_RowIdx, int i_ColIdx)
        {
            InitializeComponent();
            m_PictureBoxSqrIdx = new Point(i_ColIdx, i_RowIdx);
            m_SquareHolder = Player.ePlayerRecognition.None;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public bool IsOccuipied
        {
            get
            {
                return this.Image != null;
            }
        }

        public Player.ePlayerRecognition SquareHolder
        {
            get
            {
                return m_SquareHolder;
            }
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

        public void ResetPicBoxSquare()
        {
            this.Image = null;
            m_SquareHolder = Player.ePlayerRecognition.None;
        }

        private void SetLocation()
        {
            Point locationToSet = new Point();

            locationToSet.X = m_PictureBoxSqrIdx.X * FormGameSpecs.k_PictureBoxHeight;
            locationToSet.Y = m_PictureBoxSqrIdx.Y * FormGameSpecs.k_PictureBoxWidth;
            this.Location = locationToSet;
        }

        public void UpdatePicBoxSquare(Square i_Square)
        {
            if (i_Square.SquareHolder == Player.ePlayerRecognition.FirstPlayer)
            {
                if (i_Square.DiscType == GameLogic.eDiscType.ODisc)
                {
                    this.Image = Properties.Resources.BlackPiece;
                }

                else /// So it's a king
                {
                    this.Image = Properties.Resources.BlackKing_cropped;
                } 
            }

            else if (i_Square.SquareHolder == Player.ePlayerRecognition.SecondPlayer)
            {
                if (i_Square.DiscType == GameLogic.eDiscType.XDisc)
                {
                    this.Image = Properties.Resources.RedPiece;
                }

                else /// So it's a king
                {
                    this.Image = Properties.Resources.RedKing_cropped;
                }
                
            }

            else /// == None
            {
                this.Image = null;
            }

            m_SquareHolder = i_Square.SquareHolder;
            /// this.SizeMode = PictureBoxSizeMode.StretchImage; 
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
            this.Size = new System.Drawing.Size(FormGameSpecs.k_PictureBoxWidth, FormGameSpecs.k_PictureBoxHeight);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
