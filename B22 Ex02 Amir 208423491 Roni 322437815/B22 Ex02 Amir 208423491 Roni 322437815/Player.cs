using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B22_Ex02_Amir_208423491_Roni_322437815
{
    public class Player
    {
        private class MoveOption
        {
            Square m_SourceIndex;
            Square m_DestinationIndex;
        }

        private StringBuilder m_Name;
        private int m_NumOfDiscs;
        private eDiscType m_DiscType; //X, O
        private eDiscType m_KingDiscType; //K, U
        private ePlayerType m_PlayerType;
        private ePlayerMovingDirection m_MovingDirection;
        private List<MoveOption> m_PlayerPossibleMoves;

        public Player()
        {
            m_Name = new StringBuilder(0, 20);
            m_NumOfDiscs = 0;
            m_PlayerType = ePlayerType.Human;
            m_DiscType = eDiscType.None;
            
        }

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

        public eDiscType DiscType
        {
            get { return m_DiscType; }
            set { m_DiscType = value; }
        }
        
        public eDiscType KingDiscType
        {
            get { return m_KingDiscType; }
            set { m_KingDiscType = value; }
        }

        public void SingleMove(ref Board io_Board) //???
        {

        }

    }
}
