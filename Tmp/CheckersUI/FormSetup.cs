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
        public event EventHandler GameDetailsFilled;

        public FormSetup()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        /// Properties 
        
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

        private void buttonDone_Click(object sender, EventArgs e)
        {
            Button buttonDone = sender as Button;

            if (buttonDone != null)
            {
                /// Report Game Details Filled
                /// Add condition whether the Names are valid
                this.Close();
            }
        }

        private void radioButton6X6_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton6X6 = sender as RadioButton;

            if (radioButton6X6 != null)
            {
                if (radioButton6X6.Checked)
                {
                    /// Update game details to 6X6 Board
                }
            }
        }

        private void radioButton8X8_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton8X8 = sender as RadioButton;

            if (radioButton8X8 != null)
            {
                if (radioButton8X8.Checked)
                {
                    /// Update game details to 6X6 Board
                }
            }
        }

        private void radioButton10X10_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton10X10 = sender as RadioButton;

            if (radioButton10X10 != null)
            {
                if (radioButton10X10.Checked)
                {
                    /// Update game details to 6X6 Board
                }
            }
        }

        private void textBoxPlayer1Name_TextChanged(object sender, EventArgs e)
        {

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
                textBoxPlayer2Name.Text = null;
            }
        }

        private void textBoxPlayer2Name_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
