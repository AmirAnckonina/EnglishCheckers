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
            int m_Row;
            for (m_Row = 0; m_Row < (m_BoardSize / 2) - 1; m_Row++)
            {
                InitializeLine(m_Row,'O',false);
            }

            for (m_Row += 0; m_Row < 3; m_Row++)
            {
                InitializeLine(m_Row, ' ',true);
            }

            for (m_Row+=3; m_Row < (m_BoardSize / 2) - 1; m_Row++)
            {
                InitializeLine(m_Row,'X',false);
            }
        }

        public void InitializeLine(int m_Row, char i_DiscType, bool i_IsEmptySquare)
        {
            int m_IsEven;

            for (int m_Column = 0; m_Column < m_BoardSize; m_Column++)
            {
                m_IsEven = (m_Row + m_Column) % 2;
                if (m_IsEven != 0)  
                {
                    if(!i_IsEmptySquare)
                    {
                        m_GameBoard[m_Row, m_Column].CurrDiscType = i_DiscType;
                    }
                    m_GameBoard[m_Row, m_Column].ValidSquare = true;
                }

                else
                {
                    if (!i_IsEmptySquare)
                    {
                        // m_GameBoard[i, j].CurrDiscType = '-1';
                    }

                    m_GameBoard[m_Row, m_Column + 1].ValidSquare = false;
                }
            }
        }

        public void PrintBoard(char[,] i_GameBoard)
        {
            int m_Row,m_Column;
            char m_Letter = 'A';
            for (m_Row = 0; m_Row < m_BoardSize; m_Row++)
            {
                Console.Write("{0}",m_Letter);
                m_Letter = (char)(m_Letter + 1);
            }

            Console.WriteLine("");
            m_Letter = 'a';
            for (m_Row = 0; m_Row < m_BoardSize; m_Row++)
            {
                Console.Write("{0}", m_Letter);
                m_Letter = (char)(m_Letter + 1);
                for (m_Column = 0; m_Column < m_BoardSize; m_Column++)
                {
                    Console.Write("| {0} |", i_GameBoard[m_Row, m_Column]);
                }

                Console.WriteLine(" ");
            }
        }
    }
}
