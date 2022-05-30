using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
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

        private PictureBoxSquare[,] m_PicBoxSqrMatrix;
        private PictureBoxSquare m_SrcPicBox;
        private PictureBoxSquare m_DestPicBox;
        private FormSetup r_FormSetup;
        private FormStart r_FormStart;
        private Label m_LabelPlayer1NameAndScore;
        private Label m_LabelPlayer2NameAndScore;
        private ePicBoxClickStage m_PicBoxClickStage;
        private Player.ePlayerRecognition m_CurrentPlayerRecognition;
        /// private MovementEventArgs m_MovementEventArgs;

        public event EventHandler GameDetailsFilled;
        public event EventHandler PotentialMoveEntered;

        public FormGame()
        {
            r_FormSetup = new FormSetup();
            r_FormStart = new FormStart();
            InitializeComponent();
            m_LabelPlayer1NameAndScore = new Label();
            m_LabelPlayer2NameAndScore = new Label();
            m_CurrentPlayerRecognition = Player.ePlayerRecognition.None;
            m_PicBoxClickStage = ePicBoxClickStage.NoneClicked;
            this.StartPosition = FormStartPosition.CenterScreen;
            r_FormSetup.FormClosed += r_FormSetup_FormClosed;
            r_FormStart.FormClosed += r_FormStart_FormClosed;
        }


        public Player.ePlayerRecognition CurrentPlayerRecognition
        {
            get
            {
                return m_CurrentPlayerRecognition;
            }

            set
            {
                m_CurrentPlayerRecognition = value;
            }
        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            r_FormStart.ShowDialog();
            /// from here, the GameForm will be loaded...
        }

        private void r_FormStart_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (r_FormStart.FormStartCloseReason == eFormCloseReason.UserProcceed)
            {
                r_FormSetup.ShowDialog();
            }

            else
            {
                this.Close();
            }
        }

        private void r_FormSetup_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (r_FormSetup.FormSetupCloseReason == eFormCloseReason.UserProcceed)
            {
                    GameDetailsFilledEventArgs gameDetailsParams;

                    HandleEmptyNames();
                    gameDetailsParams = new GameDetailsFilledEventArgs(
                        r_FormSetup.Player1Name,
                        r_FormSetup.Player2Name,
                        r_FormSetup.BoardSize,
                        r_FormSetup.Player2IsHuman
                        );

                    m_PicBoxSqrMatrix = new PictureBoxSquare[r_FormSetup.BoardSize, r_FormSetup.BoardSize];
                    SetAdaptableFormGame();
                    SetPlayersLabels();
                    InitPictureBoxSquareMatrix();
                    OnGameDetailsFilled(gameDetailsParams);
            }

            else
            {
                this.Close();
            }
        }

        private void HandleEmptyNames()
        {
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
        }

        private void SetAdaptableFormGame()
        {
            SetFormGameDimensions();
            SetPlayersLabels();
        }

        private void SetPlayersLabels()
        {
            Point player1Position = new Point(this.Left + 20, this.Bottom - 20);
            Point player2Position = new Point(this.Right - 20, this.Bottom - 20);

            m_LabelPlayer1NameAndScore.Location = player1Position;
            m_LabelPlayer2NameAndScore.Location = player2Position;
            m_LabelPlayer1NameAndScore.Text = r_FormSetup.Player1Name;
            m_LabelPlayer2NameAndScore.Text = r_FormSetup.Player2Name;

            this.Controls.Add(m_LabelPlayer1NameAndScore);
            this.Controls.Add(m_LabelPlayer2NameAndScore);
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

                    m_PicBoxSqrMatrix[rowIdx, colIdx] = currentPictureBoxSquare;
                    this.Controls.Add(currentPictureBoxSquare);
                }
            }
        }

        private void CurrentPictureBoxSquare_Click(object sender, EventArgs e)
        {
            PictureBoxSquare currentPicBoxSqr = sender as PictureBoxSquare;

            if (currentPicBoxSqr != null) /// Valid and OccuipiedObject
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

                else if (m_PicBoxClickStage == ePicBoxClickStage.OnePicBoxClicked && currentPicBoxSqr.SquareHolder == m_CurrentPlayerRecognition)
                {
                    m_SrcPicBox.BorderStyle = BorderStyle.Fixed3D;
                }
            }
        }

        protected virtual void OnPotentialMoveEntered(MovementEventArgs i_MovementParams)
        {
            if (PotentialMoveEntered != null)
            {
                PotentialMoveEntered.Invoke(this, i_MovementParams);
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
            OnPotentialMoveEntered(movementParams);
        }

        private void PreMovementReportPicBoxParamsUpdate(PictureBoxSquare i_CurrPicBoxSqr)
        {
            if (m_PicBoxClickStage == ePicBoxClickStage.NoneClicked && i_CurrPicBoxSqr.SquareHolder == m_CurrentPlayerRecognition)
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

        protected virtual void OnGameDetailsFilled(GameDetailsFilledEventArgs i_GDEventArgs)
        {
            if (GameDetailsFilled != null)
            {
                GameDetailsFilled(this, i_GDEventArgs);
            }
        }

        public void AddDiscsToPictureBoxSquareMatrix(List<PointAndHolder> i_PointsToAddDiscs)
        {
            foreach(PointAndHolder currPoint in i_PointsToAddDiscs)
            {
                m_PicBoxSqrMatrix[currPoint.PointOnBoard.Y, currPoint.PointOnBoard.X].UpdatePicBoxSquare(currPoint);
            }
        }
        
        public void PostMoveUpdatePicBoxSqrMatrix(List<PointAndHolder> i_NewOccuipiedPoints, List<PointAndHolder> i_NewEmptyPoints)
        {
            foreach (PointAndHolder newOccuipied in i_NewOccuipiedPoints)
            {
                m_PicBoxSqrMatrix[newOccuipied.PointOnBoard.Y, newOccuipied.PointOnBoard.X].UpdatePicBoxSquare(newOccuipied);
            }

            foreach (PointAndHolder newEmpty in i_NewEmptyPoints)
            {
                m_PicBoxSqrMatrix[newEmpty.PointOnBoard.Y, newEmpty.PointOnBoard.X].UpdatePicBoxSquare(newEmpty);
            }

/*            if (m_CurrentPlayerRecognition == Player.ePlayerRecognition.SecondPlayer && !r_FormSetup.Player2IsHuman)
            {
                System.Threading.Thread.Sleep(2000);
            }*/
        }

        public void RunFormGame()
        {
            r_FormSetup.ShowDialog();
            this.ShowDialog();
        }

    }
}
