using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B22_Ex02_Amir_208423491_Roni_322437815
{
    public class Player
    {
        public static char NoDisc = 'N';
        private StringBuilder m_Name;
        private int m_NumOfDiscs;
        private char m_DiscType;
        private ePlayerType m_PlayerType;

        public Player()
        {
            m_Name = new StringBuilder(0, 20);
            m_NumOfDiscs = 0;
            m_PlayerType = ePlayerType.Human;
            m_DiscType = NoDisc;
        }

        /* public Player(string i_Name, int i_NumOfDiscs, char i_DiscType, ePlayerType i_PlayerType)
        {
            m_Name = new StringBuilder(i_Name, 20);
            m_NumOfDiscs = i_NumOfDiscs;
            m_PlayerType = i_PlayerType;
            m_DiscType = i_DiscType;
        }*/

        public StringBuilder Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public int NumOfDiscs
        {
            get { return m_NumOfDiscs; }
            set { m_NumOfDiscs = value; }
        }

        public ePlayerType PlayerType
        {
            get { return m_PlayerType; }
            set { m_PlayerType = value; }
        }

        public char DiscType
        {
            get { return m_DiscType; }
            set { m_DiscType = value; }
        }

        //public void SingleMove(ref Board io_Board, )
        //{

        //}

    }
}
