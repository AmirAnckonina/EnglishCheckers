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
        private readonly FormSetup r_FormSetup;
        private readonly FormStart r_FormStart;
        private readonly Label m_LabelPlayer1NameAndScore;
        private readonly Label m_LabelPlayer2NameAndScore;
        private ePicBoxClickStage m_PicBoxClickStage;
        private Player.ePlayerRecognition m_CurrentPlayerRecognition;

        public event EventHandler GameDetailsFilled;
        public event EventHandler PotentialMoveEntered;
        public event EventHandler PlayAnotherGameAnswered;

        public FormGame()
        {
            r_FormSetup = new FormSetup();
            r_FormStart = new FormStart();
            InitializeComponent();
            m_LabelPlayer1NameAndScore = new Label();
            m_LabelPlayer2NameAndScore = new Label();
            m_CurrentPlayerRecognition = Player.ePlayerRecognition.FirstPlayer;
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
                    InitPictureBoxSquareMatrix();
                    SetPlayersLabels();
                    OnGameDetailsFilled(gameDetailsParams);
            }

            else
            {
                this.Close();
            }
        }

        public void UpdateSpecificPicBoxToKingDisc(Square i_Square)
        {
            Point pointToUpdate = SquareIndexPointConverter.SquareIndexToPoint(i_Square.SquareIndex);

            m_PicBoxSqrMatrix[pointToUpdate.Y, pointToUpdate.X].UpdatePicBoxSquare(i_Square);
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
        }

        private void SetPlayersLabels()
        {
            SetPlayersLabelLocation();
            SetPlayersLabelFontAndSize();
            UpdatePlayersLabelScore(0, 0);
            
            this.Controls.Add(m_LabelPlayer1NameAndScore);
            this.Controls.Add(m_LabelPlayer2NameAndScore);
        }

        public void UpdatePlayersLabelScore(int i_FirstPlayerScore, int i_SecondPlayerScore)
        {
            m_LabelPlayer1NameAndScore.Text = r_FormSetup.Player1Name + string.Format(": {0}", i_FirstPlayerScore);
            m_LabelPlayer2NameAndScore.Text = r_FormSetup.Player2Name + string.Format(": {0}", i_SecondPlayerScore);
        }

        private void SetPlayersLabelFontAndSize()
        {
            m_LabelPlayer1NameAndScore.Font = new System.Drawing.Font(
                "Microsoft Sans Serif",
                13F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((byte)(0)));
            m_LabelPlayer2NameAndScore.Font = new System.Drawing.Font(
                "Microsoft Sans Serif",
                13F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                ((byte)(0)));
        }

        private void SetPlayersLabelLocation()
        {
            Point player1Position = new Point();
            Point player2Position = new Point();
            PictureBoxSquare lastLineMiddlePicBox;

            lastLineMiddlePicBox = m_PicBoxSqrMatrix[r_FormSetup.BoardSize - 1, r_FormSetup.BoardSize / 2];
            player1Position = lastLineMiddlePicBox.Location;
            player2Position = lastLineMiddlePicBox.Location;
            player1Position.Offset(-2 * FormGameSpecs.k_PictureBoxWidth, (int)(1.25f * FormGameSpecs.k_PictureBoxHeight));
            player2Position.Offset(FormGameSpecs.k_PictureBoxWidth, (int)(1.25f * FormGameSpecs.k_PictureBoxHeight));

            m_LabelPlayer1NameAndScore.Location = player1Position;
            m_LabelPlayer2NameAndScore.Location = player2Position;
            m_LabelPlayer1NameAndScore.AutoSize = true;
            m_LabelPlayer2NameAndScore.AutoSize = true;
        }

        private void SetFormGameDimensions()
        {
            this.Height = (r_FormSetup.BoardSize * FormGameSpecs.k_PictureBoxHeight) + FormGameSpecs.k_HeightExtention;
            this.Width = (r_FormSetup.BoardSize * FormGameSpecs.k_PictureBoxWidth);
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
            MovementEventArgs movementParams = new MovementEventArgs(
                m_SrcPicBox.PictureBoxSqrIdx,
                m_DestPicBox.PictureBoxSqrIdx
                );
 
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

        public void AddDiscsToPictureBoxSquareMatrix(List<Square> i_PointsToAddDiscs)
        {
            Point pointToUpdate;

            foreach(Square currSqr in i_PointsToAddDiscs)
            {
                pointToUpdate = SquareIndexPointConverter.SquareIndexToPoint(currSqr.SquareIndex);
                m_PicBoxSqrMatrix[pointToUpdate.Y, pointToUpdate.X].UpdatePicBoxSquare(currSqr);
            }
        }
        
        public void PostMoveUpdatePicBoxSqrMatrix(List<Square> i_NewOccuipiedPoints, List<Square> i_NewEmptyPoints)
        {
            Point pointToUpdate;

            foreach (Square newOccuipiedSqr in i_NewOccuipiedPoints)
            {
                pointToUpdate = SquareIndexPointConverter.SquareIndexToPoint(newOccuipiedSqr.SquareIndex);
                m_PicBoxSqrMatrix[pointToUpdate.Y, pointToUpdate.X].UpdatePicBoxSquare(newOccuipiedSqr);
            }

            foreach (Square newEmptySqr in i_NewEmptyPoints)
            {
                pointToUpdate = SquareIndexPointConverter.SquareIndexToPoint(newEmptySqr.SquareIndex);
                m_PicBoxSqrMatrix[pointToUpdate.Y, pointToUpdate.X].UpdatePicBoxSquare(newEmptySqr);
            }
        }

        public void CreateYesNoMessageBox(string i_GameResultMessage)
        {
            DialogResult dialogResult;
            PlayAnotherGameAnsweredEventArgs playAnotherGameAnsweredParams;
            string messageBoxContent = i_GameResultMessage + "\nGo for another round?";
            
            dialogResult = MessageBox.Show(messageBoxContent, "English Checkers", MessageBoxButtons.YesNo);
            playAnotherGameAnsweredParams = new PlayAnotherGameAnsweredEventArgs(dialogResult);
            
            OnPlayAnotherGameAnswered(playAnotherGameAnsweredParams);
        }

        public void ShowInvalidMoveMessage()
        {
            MessageBox.Show("Invalid Move Choice!\nPlease Try again.");
        }

        public void MarkCurrentPlayerLabel()
        {
            if (m_CurrentPlayerRecognition == Player.ePlayerRecognition.FirstPlayer)
            {
                m_LabelPlayer1NameAndScore.ForeColor = Color.Green;
                m_LabelPlayer2NameAndScore.ForeColor = Color.Black;
            }
            else /// SecondPlayer
            {
                m_LabelPlayer2NameAndScore.ForeColor = Color.Green;
                m_LabelPlayer1NameAndScore.ForeColor = Color.Black;
            }
        }

        private void OnPlayAnotherGameAnswered(PlayAnotherGameAnsweredEventArgs i_PlayAnotherGameAnsweredParams)
        {
            if (PlayAnotherGameAnswered != null)
            {
                PlayAnotherGameAnswered(this, i_PlayAnotherGameAnsweredParams);
            }
        }

        public void ResetPicBoxSqrMatrix()
        {
            foreach (PictureBoxSquare picBoxSqr in m_PicBoxSqrMatrix)
            {
                picBoxSqr.ResetPicBoxSquare();
            }
        }

        public void RunFormGame()
        {
            r_FormSetup.ShowDialog();
            this.ShowDialog();
        }
    }
}
