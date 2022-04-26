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
        private bool m_GetSecondPlayerName;
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
        }
        
        public void Goodbye()
        {
            Console.WriteLine("Thanks for playing! See you next time.");
        }

        public void RequestGameDetails()
        {
            int gameModeChoice;

            m_GetSecondPlayerName = false;
            m_GameDetails.FirstPlayerName = GetPlayerName();
            m_GameDetails.BoardSize = GetSizeOfBoard();
            gameModeChoice = GetGameMode();

            if (gameModeChoice == 1)
            {
                m_GameDetails.GameMode = CheckersGame.Game.eGameMode.SinglePlayerMode;
                m_GameDetails.SecondPlayerName.Append("The Computer");
            }

            else //gameModeChoice == 2
            {
                m_GameDetails.GameMode = CheckersGame.Game.eGameMode.TwoPlayersMode;
                m_GetSecondPlayerName = true;
                m_GameDetails.SecondPlayerName = GetPlayerName();
            }
        }

        public StringBuilder GetPlayerName()
        {
            StringBuilder m_Name = new StringBuilder();

            if (!m_GetSecondPlayerName)
            {
                Console.WriteLine("Please enter your Name: ");
            }

            else
            {
                Console.WriteLine("Please enter the second player name: ");
            }

            m_Name.Append(System.Console.ReadLine());

            return m_Name;
        }

        public int GetSizeOfBoard()
        {
            int boardSize;
            bool boardSizeInputTypeIsValid;

            PrintRequestForBoardSize();
            boardSizeInputTypeIsValid = int.TryParse(Console.ReadLine(), out boardSize);
            while (!boardSizeInputTypeIsValid || !BoardSizeInputValueValidation(boardSize))
            {
                PrintInvalidInputMessage();
                PrintRequestForBoardSize();
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
            while (!gameModeInputTypeIsValid || (numOfPlayers != 1 && numOfPlayers != 2))
            {
                Console.WriteLine("Invalid choice!");
                PrintGameModeChoosingRequest();
                gameModeInputTypeIsValid = int.TryParse(Console.ReadLine(), out numOfPlayers);
            }

            return numOfPlayers;
        }

        public void PrintInvalidInputMessage()
        {
            Console.WriteLine("Sorry! Your input isn't valid");
        }

        public void PrintRequestForBoardSize()
        {
            Console.WriteLine("Please pick your Checkers board size: ");
            Console.WriteLine("6 - For 6X6 board");
            Console.WriteLine("8 - For 8X8 board");
            Console.WriteLine("10 - For 10X10 board");
        }

        public void PrintGameModeChoosingRequest()
        {
            Console.WriteLine("Please pick your game mode: ");
            Console.WriteLine("1 - Single player mode (play against the computer)");
            Console.WriteLine("2 - Two players mode (play against your friend!)");
        }

        public void PrintWhoseTurn(Player i_CurrPlayer)
        {
            if(i_CurrPlayer.PlayerType == Player.ePlayerType.Computer)
            {
                Console.WriteLine("- It's The Computer ({0}) turn!", GetCharByDiscType(i_CurrPlayer.DiscType));
            }

            else
            {
               Console.WriteLine("- It's {0} turn ({1}), Go Ahead! : ", i_CurrPlayer.Name, GetCharByDiscType(i_CurrPlayer.DiscType));
            }
        }

        public void PrintInvalidInputStructure()
        {            
                Console.WriteLine("Sorry, your input structure isn't valid.");
                Console.Write("Please enter a new move: ");          
        }

        public void PrintInvalidInputMoveOption(Player.ePlayerType i_CurrentPlayerType)
        {
            if (i_CurrentPlayerType == Player.ePlayerType.Human)
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

        public void PrintBoard(Board i_Board, Player.ePlayerType i_PlayerType)
        {
            int columnIndex = 0;
            char rowLetter = 'a';

            if (i_PlayerType == Player.ePlayerType.Computer)
            {
                System.Threading.Thread.Sleep(2000);
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

        public void PrintSingleGameResult(Game.eGameResult i_GameResult,Player i_FirstPlayer, Player i_SecondPlayer)
        {
           if(i_GameResult == Game.eGameResult.FirstPlayerWon)
            {
                Console.WriteLine("The Winner is {0}!", i_FirstPlayer.Name);
            }

           else if(i_GameResult == Game.eGameResult.SecondPlayerWon)
            {
                Console.WriteLine("The Winner is {0}!", i_SecondPlayer.Name);
            }
            
           else ///The game result is draw
            {
                Console.WriteLine("The game result is draw!");
            }

            Console.WriteLine("The total score till now: ");
            PrintScore(i_FirstPlayer, i_SecondPlayer);
        }

        public void PrintAllGameSessionsResult(Game.eGameResult i_FinalCheckersSessionResult, Player i_FirstPlayer, Player i_SecondPlayer)
        {
            if (i_FinalCheckersSessionResult == Game.eGameResult.FirstPlayerWon)
            {
                Console.WriteLine("The final Winner is {0}!", i_FirstPlayer.Name);
            }

            else if (i_FinalCheckersSessionResult == Game.eGameResult.SecondPlayerWon)
            {
                Console.WriteLine("The final Winner is {0}!", i_SecondPlayer.Name);
            }

            else ///The game result is draw
            {
                Console.WriteLine("This Checkers session final result is a draw!");
            }

            Console.WriteLine("Final session score: ");
            PrintScore(i_FirstPlayer, i_SecondPlayer);
        }

        public void PrintScore(Player i_FirstPlayer, Player i_SecondPlayer)
        {
            Console.WriteLine("{0}'s score: {1}", i_FirstPlayer.Name, i_FirstPlayer.Score);
            Console.WriteLine("{0}'s score: {1}", i_SecondPlayer.Name, i_SecondPlayer.Score);
        }

        public bool AskForAnotherRound()
        {
            bool playAnotherRound;
            bool userChoiceIsValid;
            char userChoice;

            PrintAnotherGameRequest();
            userChoiceIsValid = Char.TryParse(Console.ReadLine(), out userChoice);
            while (!userChoiceIsValid || !AnotherTurnInputValidation(userChoice))
            {
                PrintInvalidInputMessage();
                PrintAnotherGameRequest();
                userChoiceIsValid = Char.TryParse(Console.ReadLine(), out userChoice);
            }

            if (Char.ToUpper(userChoice) == 'Q')
            {
                playAnotherRound = false;
            }

            else
            {
                playAnotherRound = true;
            }

            return playAnotherRound;
        }

        public bool AnotherTurnInputValidation(char i_UserChoice)
        {
            bool anotherTurnInputIsValid;

            if ((Char.ToUpper(i_UserChoice) == 'Y' || Char.ToUpper(i_UserChoice) == 'Q'))
            {
                anotherTurnInputIsValid = true;
            }

            else
            {
                anotherTurnInputIsValid = false;
            }

            return anotherTurnInputIsValid;
        }

        public void PrintAnotherGameRequest()
        {
            Console.WriteLine("Do you want to go for another game?");
            Console.WriteLine("If so, press 'Y' to start a new game. If Not press 'Q' to quit");
        }
    }
}
