﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersGame
{
    public class Square
    {
        private bool m_LegalSquare;
        private eDiscType m_DiscType;
        private ePlayerRecognition m_SquareHolder;
        private SquareIndex m_SquareIndex;

        public Square()
        {
            m_SquareIndex = new SquareIndex();
        }

        public Square(bool i_LegalSquare, eDiscType i_CurrDiscType, int i_RowIndex, int i_ColumnIndex)
        {
            m_LegalSquare = i_LegalSquare;
            m_DiscType = i_CurrDiscType;
            m_SquareIndex = new SquareIndex(i_RowIndex, i_ColumnIndex);

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

        public eDiscType DiscType
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

        public SquareIndex SqrIndex
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

        public ePlayerRecognition SquareHolder
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

        public void InitializeSquare(bool i_LegalSquare, eDiscType i_DiscType, ePlayerRecognition i_SquareHolder, int i_RowIndex, int i_ColumnIndex)
        {
            m_LegalSquare = i_LegalSquare;
            m_DiscType = i_DiscType;
            m_SquareHolder = i_SquareHolder;
            m_SquareIndex.RowIndex = i_RowIndex;
            m_SquareIndex.ColumnIndex = i_ColumnIndex;
        }

        public bool RivalInSquareValidation(Player i_CurrPlayer)
        {
            bool rivalInSqaure;

            //IF the current swaure holder isn't the currPlayer and isn't vacant so the rival is there.
            if (m_SquareHolder != i_CurrPlayer.PlayerRecognition && m_SquareHolder != ePlayerRecognition.None)
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
