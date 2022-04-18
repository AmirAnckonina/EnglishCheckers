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

        //Use of Indexers!!! could be implemented!
        //Check id already returned a ref? returning a class return a ref? or a copy of the value?
        public ref Square GetSquare(int i_Row, int i_Column) 
        {
            return ref m_GameBoard[i_Row, i_Column];
        }

        //Use of Indexers!!! ToCheck!
        public ref Square this[SquareIndex i_SquareIndex]
        {
            get { return ref m_GameBoard[i_SquareIndex.RowIndex, i_SquareIndex.ColumnIndex]; }
            // set { m_GameBoard[i_SquareIndex.RowIndex, i_SquareIndex.ColumnIndex] = value; }
        }
        
        public int BoardSize
        {
            get { return m_BoardSize; }
            set { m_BoardSize = value; }
        }

        public void InitializeBoard()
        {
            int rowIndex, emptyLinesIndex; //What did you mean? index is column?

            for (rowIndex = 0; rowIndex < (m_BoardSize / 2) - 1; rowIndex++)
            {
                InitializeLine(rowIndex, eDiscType.ODisc);
            }

            for (emptyLinesIndex = 0; emptyLinesIndex < 2; emptyLinesIndex++)  
            {
                InitializeLine(rowIndex + emptyLinesIndex, eDiscType.None);
            }

            for (rowIndex += 2; rowIndex < m_BoardSize; rowIndex++) 
            {
                InitializeLine(rowIndex,eDiscType.XDisc);
            }
        }

        public void InitializeLine(int i_RowIndex, eDiscType i_DiscType)
        {
            int isEven;

            for (int i_ColumnIndex = 0; i_ColumnIndex < m_BoardSize; i_ColumnIndex++)
            {
                isEven = (i_RowIndex + i_ColumnIndex) % 2;
                if (isEven != 0)  
                {
                    m_GameBoard[i_RowIndex, i_ColumnIndex] = new Square(true, i_DiscType, i_RowIndex, i_ColumnIndex);
                }

                else
                {
                    m_GameBoard[i_RowIndex, i_ColumnIndex] = new Square(false, eDiscType.None, i_RowIndex, i_ColumnIndex);
                }
            }
        }

        public void PrintBoard()
        {
            int row, column, index;
            char letter = 'A', currDiscChar;
            eDiscType currDiscType;

            for (row = 0; row < m_BoardSize; row++)
            {
                Console.Write("   {0} ", letter);
                letter = (char)(letter + 1);
            }

            Console.WriteLine("");
            letter = 'a';
            for (row = 0; row < m_BoardSize; row++)
            {
                Console.Write("{0}|", letter);
                letter = (char)(letter + 1);
                for (column = 0; column < m_BoardSize; column++)
                {
                    currDiscChar = GetCharByDiscType(m_GameBoard[row, column].CurrDiscType);
                    currDiscType = m_GameBoard[row, column].CurrDiscType;
                    if (currDiscType != eDiscType.None)
                    {
                        Console.Write(" {0} | ", currDiscChar);
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
                    if (m_GameBoard[row, column].CurrDiscType == i_DiscType) 
                    {
                        counter++;
                    }
                }
            }

            return counter;
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

           else  if(i_DiscTypeNum == eDiscType.XKing)
            {
                discTypeChar = 'K';
            }

            else if(i_DiscTypeNum == eDiscType.OKing)
            {
                discTypeChar = 'U';
            }   
            
            else
            {
                discTypeChar = 'N';
            }

            return discTypeChar;
        }

        // RONI. The function isn't hermetic. What about negative values?
        // Changed it a little bit, including the name -> because of a name pattern I've already used
        // Will explain to you when we meet.
        /*public bool SquareExistenceValidation(int i_RowIndex, int i_ColumnIndex) 
        {
            bool isSquareExist;
            bool isRowIndexInBoard;
            bool isColumnIndexInBoard;

            isRowIndexInBoard = RowIndexExistenceValidation(i_RowIndex);
            isColumnIndexInBoard = ColumnIndexExistenceValidation(i_ColumnIndex);
            if (isRowIndexInBoard && isColumnIndexInBoard)
            {
                isSquareExist = true;
            }

            else
            {
                isSquareExist = false;
            }

            return isSquareExist;

        }*/

        public bool SquareExistenceValidation(ref int[] i_SquareIndex)
        {
            bool isSquareExist;
            bool isRowIndexInBoard;
            bool isColumnIndexInBoard;

            isRowIndexInBoard = RowIndexExistenceValidation(i_SquareIndex[0]); 
            isColumnIndexInBoard = ColumnIndexExistenceValidation(i_SquareIndex[1]);
            if (isRowIndexInBoard && isColumnIndexInBoard)
            {
                isSquareExist = true;
            }

            else
            {
                isSquareExist = false;
            }

            return isSquareExist;

        }

        public bool RowIndexExistenceValidation(int i_RowIndex)
        {
            bool isRowIndexExist;

            if (i_RowIndex >= 0 && i_RowIndex <= BoardSize)
            {
                isRowIndexExist = true;
            }

            else
            {
                isRowIndexExist = false;
            }

            return isRowIndexExist;

        }

        public bool ColumnIndexExistenceValidation(int i_ColumnIndex)
        {
            bool isColumnIndexExist;

            if (i_ColumnIndex >= 0 && i_ColumnIndex <= BoardSize)
            {
                isColumnIndexExist = true;
            }

            else
            {
                isColumnIndexExist = false;
            }

            return isColumnIndexExist;

        }
    }
}
