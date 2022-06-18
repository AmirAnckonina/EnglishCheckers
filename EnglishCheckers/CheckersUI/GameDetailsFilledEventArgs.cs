using System;

namespace CheckersUI
{
    public class GameDetailsFilledEventArgs : EventArgs
    {
        private readonly string r_Player1Name;
        private readonly string r_Player2Name;
        private readonly int r_BoardSize;
        private readonly bool r_Player2IsHuman;

        public GameDetailsFilledEventArgs(string i_Player1Name, string i_Player2Name, int i_BoardSize, bool i_Player2IsHuman)
        {
            r_Player1Name = i_Player1Name;
            r_Player2Name = i_Player2Name;
            r_BoardSize = i_BoardSize;
            r_Player2IsHuman = i_Player2IsHuman;
        }

        public string Player1Name
        {
            get
            {
                return r_Player1Name;
            }
        }

        public string Player2Name
        {
            get
            {
                return r_Player2Name;
            }
        }

        public int BoardSize
        {
            get
            {
                return r_BoardSize;
            }
        }

        public bool Player2IsHuman
        {
            get
            {
                return r_Player2IsHuman;
            }
        }
    }
}
