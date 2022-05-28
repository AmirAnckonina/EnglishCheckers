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

        public event EventHandler MoveEntered;

        private PictureBoxSquare[,] m_pictureBoxSquareMatrix;
        private PictureBoxSquare m_SrcPicBox;
        private PictureBoxSquare m_DestPicBox;
        private FormSetup r_FormSetup;
        private GameDetailsFilledEventArgs m_GameDetailsEventArgs;
        private Label m_labelPlayer1NameAndScore;
        private Label m_labelPlayer2NameAndScore;
        private ePicBoxClickStage m_PicBoxClickStage;
        private MovementEventArgs m_MovementEventArgs;

        public event EventHandler GameDetailsFilled;

        public FormGame()
        {
            r_FormSetup = new FormSetup();
            m_MovementEventArgs = new MovementEventArgs();
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
                    currentPictureBoxSquare.PictureBoxSquareClicked += PictureBoxSquare_PictureBoxSquareClicked;
                    ///newPicBoxSqr.SetLocation();

                    this.Controls.Add(currentPictureBoxSquare);
                }
            }
        }

        private void PictureBoxSquare_PictureBoxSquareClicked(object sender, EventArgs e)
        {
            PictureBoxSquare currentPicBoxSqr = sender as PictureBoxSquare;

            if (currentPicBoxSqr != null)
            {
                ///Add condition wheteher the DestClick is the same as SrcClick
                UpdatePicBoxClickStage();
                if (m_PicBoxClickStage == ePicBoxClickStage.TwoPicBoxClicked)
                {
                    if (m_SrcPicBox != m_DestPicBox)
                    {
                        UpdateMovement(currentPicBoxSqr);
                        OnMoveEntered();
                        m_PicBoxClickStage = ePicBoxClickStage.NoneClicked;
                    }

                    else /// Not sure if necessary.
                    {


                    }
                }
            }
            
        }

        protected virtual void OnMoveEntered()
        {
            if (MoveEntered != null)
            {
                MoveEntered.Invoke(this, m_MovementEventArgs);
            }
        }

        private void UpdateMovement(PictureBoxSquare i_CurrPicBoxSqr)
        {
            ///PictureBoxClickedEventArgs picBoxEventArgs = i_PicBoxSqrClickedEventArgs as PictureBoxClickedEventArgs;
            SquareIndex sqrIdx = new SquareIndex(i_CurrPicBoxSqr.PictureBoxSqrIdx.Y, i_CurrPicBoxSqr.PictureBoxSqrIdx.X);
            
            if (m_PicBoxClickStage == ePicBoxClickStage.OnePicBoxClicked)
            {
                /// Update MovementEventArgs with Src Only
                m_MovementEventArgs.Movement.SrcIdx = sqrIdx;
                m_SrcPicBox = i_CurrPicBoxSqr;
            }

            else if (m_PicBoxClickStage == ePicBoxClickStage.TwoPicBoxClicked)
            {
                /// Update MovementEventArgs with Dest Only
                m_MovementEventArgs.Movement.DestIdx = sqrIdx;
                m_DestPicBox = i_CurrPicBoxSqr;
            }
        }

        protected virtual void OnGameDetailsFilled()
        {
            if (GameDetailsFilled != null)
            {
                GameDetailsFilled(this, m_GameDetailsEventArgs);
            }
        }

        private void UpdatePicBoxClickStage()
        {

            if (m_PicBoxClickStage == ePicBoxClickStage.OnePicBoxClicked)
            {
                m_PicBoxClickStage = ePicBoxClickStage.TwoPicBoxClicked;
            }

            else /// (m_MoveStatus == eMoveChoiceStatus.NoneClicked || m_MoveStatus == eMoveChoiceStatus.SrcAndDstClickedClicked)
            {
                m_PicBoxClickStage = ePicBoxClickStage.OnePicBoxClicked;
            }
        }

        public void RunFormGame()
        {
            r_FormSetup.ShowDialog();
            this.ShowDialog();
        }

    }
}
