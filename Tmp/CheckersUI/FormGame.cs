using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckersUI
{
    public partial class FormGame : Form
    {
        public enum eMoveChoiceStatus
        {
            NoneClicked,
            SrcClicked,
            SrcAndDstClicked
        }

        public event EventHandler PictureBoxSquareClicked;

        private PictureBoxSquare[,] m_pictureBoxSquareMatrix;
        private FormSetup r_FormSetup;
        private GameDetailsFilledEventArgs m_GameDetailsEventArgs;
        private Label m_labelPlayer1NameAndScore;
        private Label m_labelPlayer2NameAndScore;
        private eMoveChoiceStatus m_MoveStatus;

        public event EventHandler GameDetailsFilled;

        public FormGame()
        {
            r_FormSetup = new FormSetup();
            m_MoveStatus = eMoveChoiceStatus.NoneClicked;
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            r_FormSetup.FormClosed += r_FormSetup_FormClosed;
            r_FormSetup.ShowDialog();
            /// from here, the GameForm will be loaded...
        }

        private void r_FormSetup_FormClosed(object sender, FormClosedEventArgs e)
        {
            /// Retrieve / Set Game Details.
            /// 
            if (string.IsNullOrEmpty(r_FormSetup.Player1Name))
            {
                r_FormSetup.Player1Name = "Player 1";
            }

            /// <----------------------------------------------------------->
           
            if (!r_FormSetup.Player2IsHuman)
            {
                r_FormSetup.Player2Name = "Computer";
            }

            else if (string.IsNullOrEmpty(r_FormSetup.Player2Name))
            {
                r_FormSetup.Player2Name = "Player 2";
            }

            /// <----------------------------------------------------------->
            

            m_GameDetailsEventArgs = new GameDetailsFilledEventArgs(
                r_FormSetup.Player1Name,
                r_FormSetup.Player2Name,
                r_FormSetup.BoardSize,
                r_FormSetup.Player2IsHuman
                );

            
            m_pictureBoxSquareMatrix = new PictureBoxSquare[r_FormSetup.BoardSize, r_FormSetup.BoardSize];
            SetAdaptableFormGame();        
            InitPictureBoxSquareMatrix();
            OnGameDetailsFilled();
        }

        private void SetAdaptableFormGame()
        {
            SetFormGameDimensions();
            SetPlayersLabels();
        }

        private void SetPlayersLabels()
        {
            Point player1Position = new Point(this.Bottom - 20, this.Left + 20);
            Point player2Position = new Point(this.Bottom - 20, this.Right - 20);

            m_labelPlayer1NameAndScore.Location = player1Position;
            m_labelPlayer2NameAndScore.Location = player2Position;
            m_labelPlayer1NameAndScore.Text = r_FormSetup.Player1Name;
            m_labelPlayer2NameAndScore.Text = r_FormSetup.Player2Name;

        }

        private void SetFormGameDimensions()
        {
            this.Height = (r_FormSetup.BoardSize * FormGameSpecs.k_PictureBoxHeight) + FormGameSpecs.k_HeightExtention;
            this.Width = (r_FormSetup.BoardSize * FormGameSpecs.k_PictureBoxWidth) + FormGameSpecs.k_WidthExtention;
        }

        private void InitPictureBoxSquareMatrix()
        {
            PictureBoxSquare newPicBoxSqr;

            for (int rowIdx = 0; rowIdx < r_FormSetup.BoardSize; rowIdx++)
            {
                for (int colIdx = 0; colIdx < r_FormSetup.BoardSize; colIdx++)
                {
                    newPicBoxSqr = new PictureBoxSquare(rowIdx, colIdx);
                    newPicBoxSqr.SetSquare();
                    ///newPicBoxSqr.SetLocation();

                    this.Controls.Add(newPicBoxSqr);
                }
            }
        }

        protected virtual void OnGameDetailsFilled()
        {
            if (GameDetailsFilled != null)
            {
                GameDetailsFilled(this, m_GameDetailsEventArgs);
            }
        }

        public void PictureBoxSquare_Clicked(object sender, EventArgs e)
        {
            /// Classify according to MoveStatus
            UpdateMoveArgsAfterPictureBoxSquareClick();
            if (m_MoveStatus == eMoveChoiceStatus.SrcAndDstClicked)
            {
                /// Check Move...
            }
        }

        private void UpdateMoveArgsAfterPictureBoxSquareClick()
        {
            if (m_MoveStatus == eMoveChoiceStatus.NoneClicked)
            {
                m_MoveStatus = eMoveChoiceStatus.SrcClicked;
            }

            else if (m_MoveStatus == eMoveChoiceStatus.SrcClicked)
            {
                m_MoveStatus = eMoveChoiceStatus.SrcAndDstClicked;
            }
        }

        public void RunFormGame()
        {
            r_FormSetup.ShowDialog();
            this.ShowDialog();
        }

    }
}
