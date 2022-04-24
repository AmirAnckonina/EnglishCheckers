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
            Console.WriteLine("- It's {0} turn, Go Ahead! : ", i_CurrPlayer.Name);
        }

        public void PrintInvalidInputStructure()
        {
            Console.WriteLine("Sorry, your input structure isn't valid.");
            Console.Write("Please enter a new move: ");
        }

        public void PrintInvalidInputMoveOption()
        {
            Console.WriteLine("Sorry, your move choice isn't valid!");
            Console.WriteLine("Please enter a new valid move: ");
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

        //public void PrintBoard(Board i_Board)
        //{
        //    /*foreach (Square sqr in i_Board.GameBoard)
        //    {
        //        if (sqr.DiscType != eDiscType.None)
        //        {
        //            Console.Write(" {0} | ", GetCharByDiscType(sqr.DiscType));
        //        }

        //        else
        //        {
        //            Console.Write("   | ");
        //        }
        //    }*/

        //    int row, column, index;
        //    char letter = 'A', currDiscChar;
        //    eDiscType currDiscType;

        //    for (row = 0; row < i_Board.BoardSize; row++)
        //    {
        //        Console.Write("   {0} ", letter);
        //        letter = (char)(letter + 1);
        //    }

        //    Console.WriteLine("");
        //    letter = 'a';
        //    for (row = 0; row < i_Board.BoardSize; row++)
        //    {
        //        Console.Write("{0}|", letter);
        //        letter = (char)(letter + 1);
        //        for (column = 0; column < i_Board.BoardSize; column++)
        //        {
        //            currDiscChar = GetCharByDiscType(i_Board[row, column].DiscType);
        //            currDiscType = i_Board[row, column].DiscType;
        //            if (currDiscType != eDiscType.None)
        //            {
        //                Console.Write(" {0} | ", currDiscChar);
        //            }

        //            else
        //            {
        //                Console.Write("   | ");
        //            }
        //        }

        //        Console.WriteLine(" ");
        //        for (index = 0; index < i_Board.BoardSize; index++)
        //        {
        //            Console.Write("=====");
        //        }

        //        Console.WriteLine(" ");
        //    }
        //}

        public void PrintBoard(Board i_Board, ePlayerType i_PlayerType)
        {
            int row, counter = 0, index;
            char letter = 'A', currDiscChar;
            eDiscType currDiscType;

            if (i_PlayerType == ePlayerType.Computer)
            {
                System.Threading.Thread.Sleep(3000);
            }

            Ex02.ConsoleUtils.Screen.Clear();
            
            for (row = 0; row < i_Board.BoardSize; row++)
            {
                Console.Write("   {0} ", letter);
                letter = (char)(letter + 1);
            }

            Console.WriteLine(" ");
            for (index = 0; index < i_Board.BoardSize; index++)
            {
                Console.Write("-----");
            }

            Console.WriteLine("");
            letter = 'a';
            Console.Write("{0}|", letter);
            letter = (char)(letter + 1);

            foreach (Square currSquare in i_Board.GameBoard)
            {
                currDiscChar = GetCharByDiscType(currSquare.DiscType);
                currDiscType = currSquare.DiscType;

                if (counter == i_Board.BoardSize)
                {
                    Console.WriteLine(" ");
                    for (index = 0; index < i_Board.BoardSize; index++)
                    {
                        Console.Write("=====");
                    }

                    Console.WriteLine(" ");
                    Console.Write("{0}|", letter);
                    letter = (char)(letter + 1);
                    counter = 0;
                }

                if (currDiscType != eDiscType.None)
                {
                    Console.Write(" {0} | ", currDiscChar);
                }

                else
                {
                    Console.Write("   | ");
                }

                counter++;
            }

            Console.WriteLine(" ");
            Console.WriteLine(" ");

            /*if (i_PlayerType == ePlayerType.Computer)
            {
                System.Threading.Thread.Sleep(3000);
            }*/


        }

        //public StringBuilder GetPlayerName(Player i_CurrPlayer)
        //{
        //    return i_CurrPlayer.Name;
        //}

        public void RequestMoveInput()
        {
            m_Input.LoadNewInput();
            while (!m_Input.RawInputIsValid)
            {
                PrintInvalidInputStructure();
                m_Input.LoadNewInput();
            }
        }

        public void PrintSingleGameResult(eGameResult i_GameResult,Player i_FirsPlayer, Player i_SecondPlayer)
        {
           
            Console.WriteLine("The winner is {0}",);
        }
    }
}