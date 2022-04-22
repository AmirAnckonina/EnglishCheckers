using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CheckersGame
{
    public class Board
    {
        private Square[,] m_GameBoard;
        private int m_BoardSize;
        private eDiscType eDiscType;

        public Board()
        {
           m_GameBoard = new Square[0,0];
        }

        public Board(int i_BoardSize)
        {
            m_BoardSize = i_BoardSize;
            m_GameBoard = new Square[m_BoardSize, m_BoardSize];
            AssignSquareObjectsInBoard();
        }



        public Square[,] GameBoard
        {
            get { return m_GameBoard; }
            //set { m_GameBoard = value; }
        }

        public Square GetSquare(int i_Row, int i_Column)
        {
            return m_GameBoard[i_Row, i_Column];
        }

        public Square this[SquareIndex i_SquareIndex]
        {
            get 
            { 
                return m_GameBoard[i_SquareIndex.RowIndex, i_SquareIndex.ColumnIndex]; 
            }

            set 
            {
                m_GameBoard[i_SquareIndex.RowIndex, i_SquareIndex.ColumnIndex] = value;
            }
        }

        public Square this[int i_RowIndex, int i_ColumnIndex]
        {
            get 
            { 
                return m_GameBoard[i_RowIndex, i_ColumnIndex]; 
            }

            set
            {
                m_GameBoard[i_RowIndex, i_ColumnIndex] = value; 
            }
        }

        public int BoardSize
        {
            get 
            { 
                return m_BoardSize;
            }

            set 
            { 
                m_BoardSize = value; 
            }
        }

        /*public void SetBoard(int i_BoardSize)
        {
            m_BoardSize = i_BoardSize;
            m_GameBoard = new Square[m_BoardSize, m_BoardSize];
            InitializeBoard();
        }*/

        public void AssignSquareObjectsInBoard()
        {
            int rowIndex, columnIndex;

            for (rowIndex = 0; rowIndex < m_BoardSize; rowIndex++)
            {
                for (columnIndex = 0; columnIndex < m_BoardSize; columnIndex++)
                {
                    m_GameBoard[rowIndex, columnIndex] = new Square();
                }
            }
        }

        public void InitializeBoard()
        {
            int rowIndex, emptyLinesIndex; 

            for (rowIndex = 0; rowIndex < (m_BoardSize / 2) - 1; rowIndex++)
            {
                InitializeLineInBoard(rowIndex, eDiscType.XDisc, ePlayerRecognition.FirstPlayer);
            }

            for (emptyLinesIndex = 0; emptyLinesIndex < 2; emptyLinesIndex++)
            {
                InitializeLineInBoard(rowIndex + emptyLinesIndex, eDiscType.None, ePlayerRecognition.None);
            }

            for (rowIndex += 2; rowIndex < m_BoardSize; rowIndex++)
            {
                InitializeLineInBoard(rowIndex, eDiscType.ODisc, ePlayerRecognition.SecondPlayer);
            }
        }

        public void InitializeLineInBoard(int i_RowIndex, eDiscType i_DiscType, ePlayerRecognition i_CurrLineSquaresHolder)
        {
           int squareIndicesParityCalculationResult;

            for (int i_ColumnIndex = 0; i_ColumnIndex < m_BoardSize; i_ColumnIndex++)
            {
                squareIndicesParityCalculationResult = (i_RowIndex + i_ColumnIndex) % 2;
                if (squareIndicesParityCalculationResult == 1)
                {
                    m_GameBoard[i_RowIndex, i_ColumnIndex].SetSquare(true, i_DiscType, i_CurrLineSquaresHolder);

                }

                else //(squareIndicesParityCalculationResult == 0)
                {
                    m_GameBoard[i_RowIndex, i_ColumnIndex].SetSquare(false, eDiscType.None, ePlayerRecognition.None);
                }
               
            }
        }

        public int GetDiscOccurences(eDiscType i_DiscType)
        {
            int counter = 0;

            foreach(Square currSquare in m_GameBoard)
            {
               if(currSquare.DiscType == i_DiscType)
                {
                    counter++;
                }
            }

            return counter;
        }

        public bool SquareExistenceValidation(int i_RowIndex, int i_ColumnIndex) 
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

        }

        public bool SquareExistenceValidation(SquareIndex i_SquareIndex)
        {
            bool isSquareExist;
            bool isRowIndexInBoard;
            bool isColumnIndexInBoard;

            isRowIndexInBoard = RowIndexExistenceValidation(i_SquareIndex.RowIndex);
            isColumnIndexInBoard = ColumnIndexExistenceValidation(i_SquareIndex.ColumnIndex);
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
