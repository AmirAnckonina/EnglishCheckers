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
    public partial class FormStart : Form
    {
        private eFormCloseReason m_FormStartCloseReason;

        public FormStart()
        {
            InitializeComponent();
            m_FormStartCloseReason = eFormCloseReason.Xpressed;
        }

        public eFormCloseReason FormStartCloseReason
        {
            get
            {
                return m_FormStartCloseReason;
            }
        }

        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            Button buttonStartGame = sender as Button;

            if (buttonStartGame != null)
            {
                /// Report Game Details Filled
                m_FormStartCloseReason = eFormCloseReason.UserProcceed;
                this.Close();
            }
        }
    }
}
