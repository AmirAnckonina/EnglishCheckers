using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B22_Ex02_Amir_208423491_Roni_322437815
{
    public class Player
    {
        private string m_Name; //Max size of 20
        private int m_NumOfDiscs;
        private readonly char m_DiscType;
        private ePlayerType m_PlayerType;

        public Player(string i_Name, int i_NumOfDiscs, char i_DiscType, ePlayerType i_PlayerType)
        {
            m_Name = i_Name;
            m_NumOfDiscs = i_NumOfDiscs;
            m_PlayerType = i_PlayerType;
            m_DiscType = i_DiscType;
        }

        public string Name
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

        public char DiscType()
        {
            return m_DiscType;
        }

    }
}
