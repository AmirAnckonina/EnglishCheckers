using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CheckersGame; 

namespace CheckersUI
{
    public partial class FormGame : Form
    {
        public enum ePicBoxClickStage
        {
            NoneClicked,
            OnePicBoxClicked,
            TwoPicBoxClicked
        }


        private PictureBoxSquare[,] m_pictureBoxSquareMatrix;
        private PictureBoxSquare m_SrcPicBox;
        private PictureBoxSquare m_DestPicBox;
        private FormSetup r_FormSetup;
        private GameDetailsFilledEventArgs m_GameDetailsEventArgs;
        private Label m_labelPlayer1NameAndScore;
        private Label m_labelPlayer2NameAndScore;
        private ePicBoxClickStage m_PicBoxClickStage;
        /// private MovementEventArgs m_MovementEventArgs;

        public event EventHandler GameDetailsFilled;
        public event EventHandler MoveEntered;

        public FormGame()
        {
            r_FormSetup = new FormSetup();    
            m_labelPlayer1NameAndScore = new Label();
            m_labelPlayer2NameAndScore = new Label();
            m_PicBoxClickStage = ePicBoxClickStage.NoneClicked;
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
            PictureBoxSquare currentPictureBoxSquare;

            for (int rowIdx = 0; rowIdx < r_FormSetup.BoardSize; rowIdx++)
            {
                for (int colIdx = 0; colIdx < r_FormSetup.BoardSize; colIdx++)
                {
                    currentPictureBoxSquare = new PictureBoxSquare(rowIdx, colIdx);
                    currentPictureBoxSquare.SetSquare();
                    currentPictureBoxSquare.Click += CurrentPictureBoxSquare_Click;
                    ///newPicBoxSqr.SetLocation();

                    this.Controls.Add(currentPictureBoxSquare);
                }
            }
        }

        private void CurrentPictureBoxSquare_Click(object sender, EventArgs e)
        {
            PictureBoxSquare currentPicBoxSqr = sender as PictureBoxSquare;

            if (currentPicBoxSqr != null) /// Valid Object
            {
                PreMovementReportPicBoxParamsUpdate(currentPicBoxSqr); /// Update Src and Dst PicBox, as well as Movement status
                if (m_PicBoxClickStage == ePicBoxClickStage.TwoPicBoxClicked) /// A second clicked received
                {
                    if (m_SrcPicBox != m_DestPicBox) /// Not the same square picked.
                    {
                        ReportNewPotentialMovement();
                        m_PicBoxClickStage = ePicBoxClickStage.NoneClicked;

                    }

                    else /// The sourceIdx clicked twice , Not sure if necessary.
                    {
                        m_SrcPicBox.BorderStyle = BorderStyle.None;
                    }

                    PostMovementReportPicBoxParamsUpdate();
                }

                else /// Only onePicBox Clicked
                {
                    m_SrcPicBox.BorderStyle = BorderStyle.Fixed3D;
                }
            }
        }

        protected virtual void OnMoveEntered(MovementEventArgs i_MovementParams)
        {
            if (MoveEntered != null)
            {
                MoveEntered.Invoke(this, i_MovementParams);
            }
        }

        private void ReportNewPotentialMovement()
        {
            MovementEventArgs movementParams = new MovementEventArgs();
            SquareIndex potentialSrcIdx = new SquareIndex(
                m_SrcPicBox.PictureBoxSqrIdx.Y,
                m_SrcPicBox.PictureBoxSqrIdx.X
                );
            SquareIndex potentialDestIdx = new SquareIndex(
                m_DestPicBox.PictureBoxSqrIdx.Y,
                m_DestPicBox.PictureBoxSqrIdx.X
                );

            movementParams.Movement.SrcIdx = potentialSrcIdx;
            movementParams.Movement.DestIdx = potentialDestIdx;
            OnMoveEntered(movementParams);
        }

        private void PreMovementReportPicBoxParamsUpdate(PictureBoxSquare i_CurrPicBoxSqr)
        {

            if (m_PicBoxClickStage == ePicBoxClickStage.NoneClicked)
            {
                m_PicBoxClickStage = ePicBoxClickStage.OnePicBoxClicked;
                m_SrcPicBox = i_CurrPicBoxSqr;
            }

            else if (m_PicBoxClickStage == ePicBoxClickStage.OnePicBoxClicked)
            {
                m_PicBoxClickStage = ePicBoxClickStage.TwoPicBoxClicked;
                m_DestPicBox = i_CurrPicBoxSqr;
            }
        }

        private void PostMovementReportPicBoxParamsUpdate()
        {
            /// TwoClicks handles, clear the Src and Dest
            m_SrcPicBox.BorderStyle = BorderStyle.None;
            m_PicBoxClickStage = ePicBoxClickStage.NoneClicked;
            m_SrcPicBox = m_DestPicBox = null;
        }

        protected virtual void OnGameDetailsFilled()
        {
            if (GameDetailsFilled != null)
            {
                GameDetailsFilled(this, m_GameDetailsEventArgs);
            }
        }

        public void RunFormGame()
        {
            r_FormSetup.ShowDialog();
            this.ShowDialog();
        }

    }
}
