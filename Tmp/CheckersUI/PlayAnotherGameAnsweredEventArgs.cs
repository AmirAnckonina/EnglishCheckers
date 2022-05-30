using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckersUI
{
    public class PlayAnotherGameAnsweredEventArgs : EventArgs
    {
        bool m_PlayAnotherGame;

        public PlayAnotherGameAnsweredEventArgs(DialogResult i_DialogResult)
        {
            m_PlayAnotherGame = false;
            SetPlayAnotherGame(i_DialogResult);
        }

        public bool PlayAnotherGame
        {
            get
            {
                return m_PlayAnotherGame;
            }
        }

        private void SetPlayAnotherGame(DialogResult i_DialogResult)
        {
            if (i_DialogResult == DialogResult.Yes)
            {
                m_PlayAnotherGame = true;
            }

            else
            {
                m_PlayAnotherGame = false;
            }
        }

    }
}
