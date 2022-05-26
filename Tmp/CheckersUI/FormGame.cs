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
        private PictureBox[,] m_pictureBoxBoard;
        private FormSetup r_FormSetup;
        private GameDetailsFilledEventArgs m_GameDetailsEventArgs;

        public event EventHandler GameDetailsFilled;

        public FormGame()
        {
            r_FormSetup = new FormSetup();
            InitializeComponent();   
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

            /// Set pictureBoxBoard.
            m_pictureBoxBoard = new PictureBox[r_FormSetup.BoardSize, r_FormSetup.BoardSize];
            InitPictureBoxBoard();
            OnGameDetailsFilled();
        }
        
        private void InitPictureBoxBoard()
        {
            for (int rowIdx = 0; rowIdx < r_FormSetup.BoardSize; rowIdx++)
            {
                for (int colIdx = 0; colIdx < r_FormSetup.BoardSize; colIdx++)
                {
                    /// Init single PictureBox
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

        public void RunFormGame()
        {
            r_FormSetup.ShowDialog();
            this.ShowDialog();
        }
    }
}
