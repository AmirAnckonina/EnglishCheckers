using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace B22_Ex02_Amir_208423491_Roni_322437815
{
    public class Board
    {
        private Square[,] m_GameBoard;
        private int m_BoardSize;
        private eDiscType eDiscType;

        public Board(int i_BoardSize)
        {
            m_BoardSize = i_BoardSize;
            m_GameBoard = new Square[m_BoardSize, m_BoardSize];
        }
        
        public Square[,] GameBoard
        {
            get { return m_GameBoard; }
            set { m_GameBoard = value; }
        }

        public int BoardSize
        {
            get { return m_BoardSize; }
            set { m_BoardSize = value; }
        }

        public void InitializeBoard()
        {
            int row, index;

            for (row = 0; row < (m_BoardSize / 2) - 1; row++)
            {
                InitializeLine(row, eDiscType.ODisc);
            }

            for (index = 0; index < 2; index++)  
            {
                InitializeLine(row + index, eDiscType.None);
            }

            for (row += 2; row < m_BoardSize; row++) 
            {
                InitializeLine(row,eDiscType.XDisc);
            }
        }

        public void InitializeLine(int m_Row, eDiscType i_DiscType)
        {
            int isEven;

            for (int m_Column = 0; m_Column < m_BoardSize; m_Column++)
            {
                isEven = (m_Row + m_Column) % 2;
                if (isEven != 0)  
                {
                    m_GameBoard[m_Row, m_Column] = new Square(true, i_DiscType, m_Row, m_Column);
                }

                else
                {
                    m_GameBoard[m_Row, m_Column] = new Square(false, eDiscType.None, m_Row, m_Column);
                }
            }
        }

        public void PrintBoard()
        {
            int row, column, index;
            char m_Letter = 'A', m_CurrDiscChar;
            eDiscType m_CurrDisc;

            for (row = 0; row < m_BoardSize; row++)
            {
                Console.Write("   {0} ",m_Letter);
                m_Letter = (char)(m_Letter + 1);
            }

            Console.WriteLine("");
            m_Letter = 'a';
            for (row = 0; row < m_BoardSize; row++)
            {
                Console.Write("{0}|", m_Letter);
                m_Letter = (char)(m_Letter + 1);
                for (column = 0; column < m_BoardSize; column++)
                {
                    m_CurrDiscChar = GetCharByDiscType(m_GameBoard[row, column].CurrDiscType);
                    m_CurrDisc = m_GameBoard[row, column].CurrDiscType;
                    if (m_CurrDisc != eDiscType.None)
                    {
                        Console.Write(" {0} | ", m_CurrDiscChar);
                    }

                    else
                    {
                        Console.Write("   | ");
                    }
                }

                Console.WriteLine(" ");
                for (index = 0; index < m_BoardSize; index++)
                {
                    Console.Write("=====");
                }

                Console.WriteLine(" ");
            }
        }

        public int GetDiscOccurences(eDiscType i_DiscType)
        {
            int row, column, counter = 0;

            for (row = 0; row < BoardSize; row++)
            {
                for (column = 0; column < BoardSize; column++)
                {
                    if(m_GameBoard[row,column].CurrDiscType == i_DiscType)
                    {
                        counter++;
                    }
                }
            }

            return counter;
        }

        public ref Square GetSquare(int i_Row, int i_Column)
        {
            return ref m_GameBoard[i_Row, i_Column];
        }

        public char GetCharByDiscType(eDiscType i_DiscTypeNum)
        {
            char discTypeChar;

            if(i_DiscTypeNum == eDiscType.XDisc)
            {
                discTypeChar = 'X';
            }

            else if(i_DiscTypeNum == eDiscType.ODisc)
            {
                discTypeChar = 'O';
            }

           else  if(i_DiscTypeNum == eDiscType.K_XKing)
            {
                discTypeChar = 'K';
            }

            else if(i_DiscTypeNum == eDiscType.U_OKing)
            {
                discTypeChar = 'U';
            }   
            
            else
            {
                discTypeChar = 'N';
            }

            return discTypeChar;
        }

        public bool IsIndexValid(int i_Row, int i_Column)
        {
            bool isIndexValid = true;

            if (i_Row > m_BoardSize || i_Column > m_BoardSize)
            {
                isIndexValid = false;
            }

            return isIndexValid;
        }
    }
}
