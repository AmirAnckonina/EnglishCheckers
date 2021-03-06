using System;
using System.Text;

namespace CheckersGame
{
    public class Board
    {
        private readonly Square[,] r_GameBoard;
        private readonly int r_BoardSize;

        public Board(int i_BoardSize)
        {
            r_BoardSize = i_BoardSize;
            r_GameBoard = new Square[r_BoardSize, r_BoardSize];
            AssignSquareObjectsInBoard();
        }

        public Square[,] GameBoard
        {
            get 
            { 
                return r_GameBoard;
            }
        }

        public Square this[SquareIndex i_SquareIndex]
        {
            get 
            { 
                return r_GameBoard[i_SquareIndex.RowIdx, i_SquareIndex.ColumnIdx]; 
            }

            set 
            {
                r_GameBoard[i_SquareIndex.RowIdx, i_SquareIndex.ColumnIdx] = value;
            }
        }

        public Square this[int i_RowIndex, int i_ColumnIndex]
        {
            get 
            { 
                return r_GameBoard[i_RowIndex, i_ColumnIndex]; 
            }

            set
            {
                r_GameBoard[i_RowIndex, i_ColumnIndex] = value; 
            }
        }

        public int BoardSize
        {
            get 
            { 
                return r_BoardSize;
            }
        }

        private void AssignSquareObjectsInBoard()
        {
            int rowIndex, columnIndex;

            for (rowIndex = 0; rowIndex < r_BoardSize; rowIndex++)
            {
                for (columnIndex = 0; columnIndex < r_BoardSize; columnIndex++)
                {
                    r_GameBoard[rowIndex, columnIndex] = new Square();
                }
            }
        }

        public void InitializeBoard(Player i_FirstPlayer, Player i_SecondPlayer)
        {
            int rowIndex, emptyLinesIndex; 

            for (rowIndex = 0; rowIndex < (r_BoardSize / 2) - 1; rowIndex++)
            {
                InitializeLineInBoard(rowIndex, i_FirstPlayer.DiscType, i_FirstPlayer.PlayerRecognition);
            }

            for (emptyLinesIndex = 0; emptyLinesIndex < 2; emptyLinesIndex++)
            {
                InitializeLineInBoard(rowIndex + emptyLinesIndex, GameLogic.eDiscType.None, Player.ePlayerRecognition.None);
            }

            for (rowIndex += 2; rowIndex < r_BoardSize; rowIndex++)
            {
                InitializeLineInBoard(rowIndex, i_SecondPlayer.DiscType, i_SecondPlayer.PlayerRecognition);
            }
        }

        private void InitializeLineInBoard(int i_RowIndex, GameLogic.eDiscType i_DiscType, Player.ePlayerRecognition i_CurrLineSquaresHolder)
        {
            int squareIndicesParityCalculationResult;

            for (int columnIdx = 0; columnIdx < r_BoardSize; columnIdx++)
            {
                squareIndicesParityCalculationResult = (i_RowIndex + columnIdx) % 2;
                if (squareIndicesParityCalculationResult == 1)
                {
                    r_GameBoard[i_RowIndex, columnIdx].SetSquare(true, i_DiscType, i_CurrLineSquaresHolder, i_RowIndex, columnIdx);

                }

                else //(squareIndicesParityCalculationResult == 0)
                {
                    r_GameBoard[i_RowIndex, columnIdx].SetSquare(false, GameLogic.eDiscType.None, Player.ePlayerRecognition.None, i_RowIndex, columnIdx);
                }
            }
        }

        public int GetDiscOccurences(GameLogic.eDiscType i_DiscType)
        {
            int counter = 0;

            foreach(Square currSquare in r_GameBoard)
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

            isRowIndexInBoard = RowIndexExistenceValidation(i_SquareIndex.RowIdx);
            isColumnIndexInBoard = ColumnIndexExistenceValidation(i_SquareIndex.ColumnIdx);
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

        private bool RowIndexExistenceValidation(int i_RowIndex)
        {
            bool isRowIndexExist;

            if (i_RowIndex >= 0 && i_RowIndex < BoardSize)
            {
                isRowIndexExist = true;
            }

            else
            {
                isRowIndexExist = false;
            }

            return isRowIndexExist;
        }

        private bool ColumnIndexExistenceValidation(int i_ColumnIndex)
        {
            bool isColumnIndexExist;

            if (i_ColumnIndex >= 0 && i_ColumnIndex < r_BoardSize)
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
