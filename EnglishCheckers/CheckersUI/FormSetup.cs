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
    public partial class FormSetup : Form
    {
        private const int k_MaxNameLength = 30;
        private eFormCloseReason m_FormSetupCloseReason;

        public FormSetup()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            m_FormSetupCloseReason = eFormCloseReason.Xpressed;
        }

        /// Properties 

        public eFormCloseReason FormSetupCloseReason
        {
            get
            {
                return m_FormSetupCloseReason;
            }
        }

        public string Player1Name
        {
            get
            {
                return textBoxPlayer1Name.Text;
            }

            set
            {
                textBoxPlayer1Name.Text = value;
            }
        }

        public string Player2Name
        {
            get
            {
                return textBoxPlayer2Name.Text;
            }

            set
            {
                textBoxPlayer2Name.Text = value;
            }
        }

        public bool Player2IsHuman
        {
            get
            {
                return checkBoxPlayer2.Checked;
            }
        }

        public int BoardSize
        {
            get
            {
                if (radioButton6X6.Checked)
                {
                    return 6;
                }

                else if (radioButton8X8.Checked)
                {
                    return 8;
                }

                else /// radioButton10X10.Checked
                {
                    return 10;
                }
            }
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            if(textBoxPlayer2Name.Enabled == true)
            {
                textBoxPlayer2Name.Enabled = false;
            }

            else /// Currently disabled
            {
                textBoxPlayer2Name.Enabled = true;
            }

            if (checkBoxPlayer2.Checked == true)
            {
                textBoxPlayer2Name.BackColor = Color.White;
                textBoxPlayer2Name.Text = null;
            }

            else /// Unchecked
            {
                textBoxPlayer2Name.BackColor = System.Drawing.SystemColors.MenuBar;
                textBoxPlayer2Name.Text = "[Computer]";
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            Button buttonDone = sender as Button;
            string invalidNameMessage = null;

            if (buttonDone != null)
            {
                if (!PlayersNameValidation(textBoxPlayer1Name.Text, ref invalidNameMessage) || !PlayersNameValidation(textBoxPlayer2Name.Text, ref invalidNameMessage))
                {
                    MessageBox.Show(invalidNameMessage);
                }

                else
                {
                    m_FormSetupCloseReason = eFormCloseReason.UserProcceed;
                    this.Close();
                }
            }
        }

        private bool PlayersNameValidation(string i_PlayerName, ref string o_ErrorPlayerNameMessage)
        {
            bool validName = true;

            if (string.IsNullOrEmpty(i_PlayerName))
            {
                validName = false;
                o_ErrorPlayerNameMessage = "Player's name is empty! Please enter a valid name.";
            }

            else if (i_PlayerName.Length > k_MaxNameLength)
            {
                validName = false;
                o_ErrorPlayerNameMessage = "Player's name is too long! The maximum name length is 30.";
            }

            return validName;
        }
    }
}
