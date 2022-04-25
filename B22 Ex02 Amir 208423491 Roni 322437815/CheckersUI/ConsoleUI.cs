using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using CheckersGame;

namespace CheckersUI
{
    public class ConsoleUI
    {
        private ConsoleInputManager m_Input;
        private bool m_IsSecondPlayerName;
        private GameDetails m_GameDetails;

        public ConsoleUI()
        {
            m_Input = new ConsoleInputManager();
            m_GameDetails = new GameDetails();
        }

        public GameDetails GameDetails
        {
            get
            {
                return m_GameDetails;
            }
        }

        public ConsoleInputManager Input
        {
            get
            {
                return m_Input;
            }
        }

        public void Welcome()
        {
            Console.WriteLine("Welcome to English Checkers Game!");
            //System.Threading.Thread.Sleep(3000);
            // Add rules? Game description?
        }

        public void RequestGameDetails()
        {
            int gameModeChoice;

            m_IsSecondPlayerName = false;
            m_GameDetails.FirstPlayerName = GetPlayerName();
            m_GameDetails.BoardSize = GetSizeOfBoard();
            gameModeChoice = GetGameMode();

            if (gameModeChoice == 1)
            {
                m_GameDetails.GameMode = CheckersGame.eGameMode.SinglePlayerMode;
            }

            else //gameModeChoice == 2
            {
                m_GameDetails.GameMode = CheckersGame.eGameMode.TwoPlayersMode;
                m_IsSecondPlayerName = true;
                m_GameDetails.SecondPlayerName = GetPlayerName();
            }
        }

        public StringBuilder GetPlayerName()
        {
            StringBuilder m_Name = new StringBuilder();

            if (!m_IsSecondPlayerName)
            {
                Console.WriteLine("Please enter your Name: ");
            }

            else
            {
                Console.WriteLine("Please enter the second player Name: ");
            }

            m_Name.Append(System.Console.ReadLine());

            return m_Name;
        }

        public int GetSizeOfBoard()
        {
            int boardSize;
            bool boardSizeInputTypeIsValid;

            Console.WriteLine("Please enter the game board size: ");
            boardSizeInputTypeIsValid = int.TryParse(Console.ReadLine(), out boardSize);

            while (!boardSizeInputTypeIsValid || !BoardSizeInputValueValidation(boardSize))
            {
                Console.WriteLine("The input is not valid");
                Console.WriteLine("Please enter the size of the game board");
                boardSizeInputTypeIsValid = int.TryParse(Console.ReadLine(), out boardSize);
            }

            return boardSize;
        }

        public int GetGameMode()
        {
            int numOfPlayers;
            bool gameModeInputTypeIsValid;

            PrintGameModeChoosingRequest();
            gameModeInputTypeIsValid = int.TryParse(Console.ReadLine(), out numOfPlayers);
            //numOfPlayers = int.Parse(Console.ReadLine());

            while (!gameModeInputTypeIsValid || (numOfPlayers != 1 && numOfPlayers != 2))
            {
                Console.WriteLine("Invalid choice!");
                PrintGameModeChoosingRequest();
                gameModeInputTypeIsValid = int.TryParse(Console.ReadLine(), out numOfPlayers);
            }

            return numOfPlayers;
        }

        public void PrintGameModeChoosingRequest()
        {
            Console.WriteLine("Please choose your game mode:");
            Console.WriteLine("1 - Single player mode (play against the computer)");
            Console.WriteLine("2 - Two players mode (play against your friend!)");
        }

        public void PrintWhoseTurn(Player i_CurrPlayer)
        {
            if(i_CurrPlayer.PlayerType == ePlayerType.Computer)
            {
                Console.WriteLine("- It's the Comuter turn!");
            }

            else
            {
               Console.WriteLine("- It's {0} turn, Go Ahead! : ", i_CurrPlayer.Name);
            }
        }

        public void PrintInvalidInputStructure()
        {            
                Console.WriteLine("Sorry, your input structure isn't valid.");
                Console.Write("Please enter a new move: ");          
        }

        public void PrintInvalidInputMoveOption(ePlayerType i_CurrentPlayerType)
        {
            if (i_CurrentPlayerType == ePlayerType.Human)
            {
                Console.WriteLine("Sorry, your move choice isn't valid!");
                Console.WriteLine("Please enter a new valid move: ");
            }
        }

        public bool BoardSizeInputValueValidation(int i_BoardSize)
        {
            bool boardSizeIsValid;

            if (i_BoardSize == 4 || i_BoardSize == 6 || i_BoardSize == 8 || i_BoardSize == 10)
            {
                boardSizeIsValid = true;
            }

            else
            {
                boardSizeIsValid = false;
            }

            return boardSizeIsValid;
        }

        public char GetCharByDiscType(eDiscType i_DiscTypeNum)
        {
            char discTypeChar;

            if (i_DiscTypeNum == eDiscType.XDisc)
            {
                discTypeChar = 'X';
            }

            else if (i_DiscTypeNum == eDiscType.ODisc)
            {
                discTypeChar = 'O';
            }

            else if (i_DiscTypeNum == eDiscType.XKingDisc)
            {
                discTypeChar = 'K';
            }

            else if (i_DiscTypeNum == eDiscType.OKingDisc)
            {
                discTypeChar = 'U';
            }

            else
            {
                discTypeChar = 'N';
            }

            return discTypeChar;
        }

        public void PrintBoard(Board i_Board, ePlayerType i_PlayerType)
        {
            int columnIndex = 0;
            char rowLetter = 'a';

            if (i_PlayerType == ePlayerType.Computer)
            {
                System.Threading.Thread.Sleep(3000);
            }

            Ex02.ConsoleUtils.Screen.Clear();
            PrintColumnsFrame(i_Board.BoardSize);

            foreach (Square currSquare in i_Board.GameBoard)
            {
                if (columnIndex == 0)
                {
                    PrintNewRowLetter(i_Board.BoardSize,ref rowLetter);
                }

                PrintSquare(currSquare.DiscType);
                columnIndex++;
                if (columnIndex == m_GameDetails.BoardSize)
                {                   
                    columnIndex = 0;
                    PrintRowBorder(i_Board.BoardSize);
                }
            }

            Console.WriteLine(" ");
        }

        public void PrintSquare(eDiscType i_CurrSquareDiscType)
        {
            if (i_CurrSquareDiscType != eDiscType.None)
            {
                Console.Write(" {0} | ", GetCharByDiscType(i_CurrSquareDiscType));
            }

                else
            {
                Console.Write("   | ");
            }
        }


        public void PrintNewRowLetter(int i_BoardSize, ref char io_RowLetter)
        {
            Console.Write("{0}|", io_RowLetter);
            io_RowLetter = (char)(io_RowLetter + 1);          
        }

        public void PrintRowBorder(int i_BoardSize)
        {
            int index;

            Console.WriteLine(" ");
            for (index = 0; index < i_BoardSize; index++)
            {
                Console.Write("=====");
            }
            Console.WriteLine(" ");
        }

        public void PrintColumnsFrame(int i_BoardSize)
        {
            int columnIndex;
            char columnLetter = 'A';

            Console.Write("  ");
            for (columnIndex = 0; columnIndex < i_BoardSize; columnIndex++)
            {
                Console.Write(" {0} | ", columnLetter);
                columnLetter = (char)(columnLetter + 1);
            }

            Console.WriteLine("");
            for (columnIndex = 0; columnIndex < i_BoardSize; columnIndex++)
            {
                Console.Write("=====");
            }
            Console.WriteLine("");
        }
            
        public void RequestMoveInput()
        {
            m_Input.LoadNewInput();
            while (!m_Input.RawInputIsValid)
            {
                PrintInvalidInputStructure();
                m_Input.LoadNewInput();
            }
        }

        public void PrintSingleGameResult(eGameResult i_GameResult,Player i_FirstPlayer, Player i_SecondPlayer)
        {
           if(i_GameResult == eGameResult.FirstPlayerWon)
            {
                Console.WriteLine("The Winner is {0} !", i_FirstPlayer.Name);
            }

           else if(i_GameResult == eGameResult.SecondPlayerWon)
            {
                Console.WriteLine("The Winner is {0} !", i_SecondPlayer.Name);
            }
            
           else ///The game result is draw
            {
                Console.WriteLine("The game result is draw!");
            }

             Console.WriteLine("{0}'s score: {1}", i_FirstPlayer.Name, i_FirstPlayer.Score);
             Console.WriteLine("{0}'s score: {1}", i_SecondPlayer.Name, i_SecondPlayer.Score);
        }
    }
}
