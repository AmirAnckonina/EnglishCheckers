using System;
using System.Text;

namespace CheckersGame
{
    public class Square
    {
        private bool m_LegalSquare;
        private GameLogic.eDiscType m_DiscType;
        private Player.ePlayerRecognition m_SquareHolder;
        private SquareIndex m_SquareIndex;

        public Square()
        {
            m_SquareIndex = new SquareIndex();
        }

        public SquareIndex SquareIndex
        {
            get
            {
                return m_SquareIndex;
            }

            set
            {
                m_SquareIndex = value;
            }
        }

        public bool LegalSquare
        {
            get
            {
                return m_LegalSquare;
            }

            set
            {
                m_LegalSquare = value;
            }
        }

        public GameLogic.eDiscType DiscType
        {
            get
            {
                return m_DiscType;
            }

            set
            {
                m_DiscType = value;
            }
        }

        public Player.ePlayerRecognition SquareHolder
        {
            get
            {
                return m_SquareHolder;
            }
            set
            {
                m_SquareHolder = value;
            }
        }

        public void SetSquare(bool i_LegalSquare, GameLogic.eDiscType i_DiscType, Player.ePlayerRecognition i_SquareHolder, int i_RowIndex, int i_ColumnIndex)
        {
            m_LegalSquare = i_LegalSquare;
            m_DiscType = i_DiscType;
            m_SquareHolder = i_SquareHolder;
            m_SquareIndex.RowIdx = i_RowIndex;
            m_SquareIndex.ColumnIdx = i_ColumnIndex;
        }

        public void CopySquareContent(Square i_CopyFromSquare)
        {
            m_LegalSquare = i_CopyFromSquare.LegalSquare;
            m_DiscType = i_CopyFromSquare.DiscType;
            m_SquareHolder = i_CopyFromSquare.SquareHolder;
        }

        public bool RivalInSquareValidation(Player i_CurrPlayer)
        {
            bool rivalInSqaure;

            /// If the current sqaure holder isn't the currPlayer and isn't vacant so the rival is there.
            if (m_SquareHolder != i_CurrPlayer.PlayerRecognition && m_SquareHolder != Player.ePlayerRecognition.None)
            {
                rivalInSqaure = true;
            }

            else
            {
                rivalInSqaure = false;
            }

            return rivalInSqaure;
        }
    }
}
