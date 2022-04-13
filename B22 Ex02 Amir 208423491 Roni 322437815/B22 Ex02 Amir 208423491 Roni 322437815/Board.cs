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
            int i;
            for (i = 0; i < (m_BoardSize / 2) - 1; i++)
            {
                InitializeLine(i,'O');
            }

            for (i+=3; i < (m_BoardSize / 2) - 1; i++)
            {
                InitializeLine(i,'X');
            }
        }

        public void InitializeLine(int i, char i_discType)
        {
            for (int j = 0; j < m_BoardSize; j++)
            {
                if ((i + j) % 2 != 0)
                {
                    m_GameBoard[i, j].CurrDiscType = i_discType;
                    m_GameBoard[i, j].ValidSquare = true;
                }

                else
                {
                    // m_GameBoard[i, j].CurrDiscType = '-1';
                    m_GameBoard[i, j + 1].ValidSquare = false;
                }
            }
        }

        public void PrintBoard(char[,] i_GameBoard)
        {
            int i,j;
            char m_Letter = 'A';
            for (i = 0; i < m_BoardSize; i++)
            {
                Console.Write("{0}",m_Letter);
                m_Letter = (char)(m_Letter + 1);
            }

            Console.WriteLine("");
            m_Letter = 'a';
            for (i = 0; i < m_BoardSize; i++)
            {
                Console.Write("{0}", m_Letter);
                m_Letter = (char)(m_Letter + 1);
                for (j = 0; j < m_BoardSize; j++)
                {
                    Console.Write("| {0} |", i_GameBoard[i, j]);
                }

                Console.WriteLine(" ");
            }
        }
    }
}
