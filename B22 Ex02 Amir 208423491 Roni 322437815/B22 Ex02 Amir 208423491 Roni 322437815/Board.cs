﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace B22_Ex02_Amir_208423491_Roni_322437815
{
    public class Board
    {
        private Square[,] m_GameBoard;
        private int m_BoardSize;
        public static char NoDisc = 'N';

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
            int m_Row, m_Index;

            for (m_Row = 0; m_Row < (m_BoardSize / 2) - 1; m_Row++)
            {
                InitializeLine(m_Row,'O');
            }

            for (m_Index = 0; m_Index < 2; m_Index++)  
            {
                InitializeLine(m_Row + m_Index, 'N');
            }

            for (m_Row += 2; m_Row < m_BoardSize; m_Row++) 
            {
                InitializeLine(m_Row,'X');
            }
        }

        public void InitializeLine(int m_Row, char i_DiscType)
        {
            int m_IsEven;

            for (int m_Column = 0; m_Column < m_BoardSize; m_Column++)
            {
                m_IsEven = (m_Row + m_Column) % 2;
                if (m_IsEven != 0)  
                {          
                    m_GameBoard[m_Row, m_Column] = new Square(true, i_DiscType);
                }

                else
                {
                    m_GameBoard[m_Row, m_Column] = new Square(false, 'N');
                }
            }
        }

        public void PrintBoard()
        {
            int m_Row, m_Column, m_Index;
            char m_Letter = 'A', m_CurrDisc;

            for (m_Row = 0; m_Row < m_BoardSize; m_Row++)
            {
                Console.Write("   {0} ",m_Letter);
                m_Letter = (char)(m_Letter + 1);
            }

            Console.WriteLine("");
            m_Letter = 'a';
            for (m_Row = 0; m_Row < m_BoardSize; m_Row++)
            {
                Console.Write("{0}|", m_Letter);
                m_Letter = (char)(m_Letter + 1);
                for (m_Column = 0; m_Column < m_BoardSize; m_Column++)
                {
                    m_CurrDisc = m_GameBoard[m_Row, m_Column].CurrDiscType;
                    if (m_CurrDisc != 'N')
                    {
                        Console.Write(" {0} | ", m_CurrDisc);
                    }

                    else
                    {
                        Console.Write("   | ");
                    }
                }

                Console.WriteLine(" ");
                for (m_Index = 0; m_Index < m_BoardSize; m_Index++)
                {
                    Console.Write("=====");
                }

                Console.WriteLine(" ");
            }
        }

        public int GetDiscOccurences(char i_DiscType)
        {
            int m_Row, m_Column, m_Counter = 0;

            for (m_Row = 0; m_Row < BoardSize; m_Row++)
            {
                for (m_Column = 0; m_Column < BoardSize; m_Column++)
                {
                    if(m_GameBoard[m_Row,m_Column].CurrDiscType == i_DiscType)
                    {
                        m_Counter++;
                    }
                }
            }

            return m_Counter;
        }

        public ref Square GetSquare(int i_Row, int i_Column)
        {
            return ref m_GameBoard[i_Row, i_Column];
        }
    }
}
