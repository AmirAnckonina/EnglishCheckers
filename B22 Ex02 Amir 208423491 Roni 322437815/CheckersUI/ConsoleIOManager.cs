using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using CheckersGame;

namespace CheckersUI
{
    public class ConsoleIOManager
    {
        private RawMoveInputManager m_RawMoveInputManager;
        private bool m_GetSecondPlayerName;
        const int k_MaximumNameLength = 20;
        const char k_Quit = 'Q';
        const char k_Continue = 'Y';

        public ConsoleIOManager()
        {
            m_RawMoveInputManager = new RawMoveInputManager();
        }

        public RawMoveInputManager RawMoveInputManager
        {
            get
            {
                return m_RawMoveInputManager;
            }
        }

        public void Welcome()
        {
            StringBuilder welcomeMessage = new StringBuilder();

            welcomeMessage.Append("Welcome to English Checkers Game!");
            Console.WriteLine(welcomeMessage);
        }
        
        public void Goodbye()
        {
            StringBuilder goodbyeMessage = new StringBuilder();

            goodbyeMessage.Append("Thanks for playing! See you next time.");
            Console.WriteLine(goodbyeMessage);
        }

        public void GetGameDetailsProcedure(GameDetails io_GameDetails)
        {
            int gameModeChoice;

            m_GetSecondPlayerName = false;
            io_GameDetails.FirstPlayerName = GetPlayerName();
            io_GameDetails.BoardSize = GetSizeOfBoard();
            gameModeChoice = GetGameMode();

            if (gameModeChoice == 1)
            {
                io_GameDetails.GameMode = CheckersGame.Game.eGameMode.SinglePlayerMode;
                io_GameDetails.SecondPlayerName.Append("The Computer");
            }

            else //gameModeChoice == 2
            {
                io_GameDetails.GameMode = CheckersGame.Game.eGameMode.TwoPlayersMode;
                m_GetSecondPlayerName = true;
                io_GameDetails.SecondPlayerName = GetPlayerName();
            }
        }

        public StringBuilder GetPlayerName()
        {
            StringBuilder name = new StringBuilder();
            StringBuilder enterNameMessage = new StringBuilder();
            StringBuilder invalidNameMessage = new StringBuilder();

            
            if (!m_GetSecondPlayerName)
            {
                enterNameMessage.Append("Please enter your Name: ");
                Console.WriteLine(enterNameMessage);
            }

            else
            {
                enterNameMessage.Append("Please enter the second player name: ");
                Console.WriteLine(enterNameMessage);
            }

            name.Append(System.Console.ReadLine());
            while(name.Length > k_MaximumNameLength)
            {
                name = new StringBuilder();
                invalidNameMessage.Append("The name is not valid! Please Enter another name");
                name.Append(System.Console.ReadLine());
            }
            return name;
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
            StringBuilder invalidMessage = new StringBuilder();

            PrintGameModeChoosingRequest();
            gameModeInputTypeIsValid = int.TryParse(Console.ReadLine(), out numOfPlayers);
            while (!gameModeInputTypeIsValid || (numOfPlayers != 1 && numOfPlayers != 2))
            {
                invalidMessage.Append("Invalid choice!");
                Console.WriteLine(invalidMessage);
                PrintGameModeChoosingRequest();
                gameModeInputTypeIsValid = int.TryParse(Console.ReadLine(), out numOfPlayers);
            }

            return numOfPlayers;
        }

        public static void PrintInvalidInputMessage()
        {
            StringBuilder inputIsNotValidMessage = new StringBuilder();

            inputIsNotValidMessage.Append("Sorry! Your input isn't valid");
            Console.WriteLine(inputIsNotValidMessage);
        }

        public static void PrintRequestForBoardSize()
        {
            StringBuilder optionsOfBoardSizeMessage = new StringBuilder();

            optionsOfBoardSizeMessage.AppendLine("Please pick your Checkers board size: ");
            optionsOfBoardSizeMessage.AppendLine("6 - For 6X6 board");
            optionsOfBoardSizeMessage.AppendLine("8 - For 8X8 board");
            optionsOfBoardSizeMessage.Append("10 - For 10X10 board");
            Console.WriteLine(optionsOfBoardSizeMessage);
        }

        public static void PrintGameModeChoosingRequest()
        {
            StringBuilder optionsOfGameModeMessage = new StringBuilder();

            optionsOfGameModeMessage.AppendLine("Please pick your game mode: ");
            optionsOfGameModeMessage.AppendLine("1 - Single player mode (play against the computer)");
            optionsOfGameModeMessage.Append("2 - Two players mode (play against your friend!)");
            Console.WriteLine(optionsOfGameModeMessage);
        }

        public void PrintWhoseTurn(Player i_CurrPlayer)
        {
            StringBuilder whoseTurnMessage = new StringBuilder();

            if(i_CurrPlayer.PlayerType == Player.ePlayerType.Computer)
            {
                whoseTurnMessage.Append(string.Format("- It's The Computer ({0}) turn!", GetCharByDiscType(i_CurrPlayer.DiscType)));
                Console.WriteLine(whoseTurnMessage);
            }

            else
            {
                whoseTurnMessage.Append(string.Format("- It's {0} turn ({1}), Go Ahead! : ", i_CurrPlayer.Name, GetCharByDiscType(i_CurrPlayer.DiscType)));
               Console.WriteLine(whoseTurnMessage);
            }
        }

        public static void PrintInvalidInputStructure()
        {
            StringBuilder invalidInputStructureMessage = new StringBuilder();

            invalidInputStructureMessage.AppendLine("Sorry, your input structure isn't valid.");
            invalidInputStructureMessage.Append("Please enter a new move: ");
            Console.WriteLine(invalidInputStructureMessage);  
        }

        public void PrintInvalidInputMoveOption(Player.ePlayerType i_CurrentPlayerType)
        {
            StringBuilder invalidInputMoveOptionMessage = new StringBuilder();

            if (i_CurrentPlayerType == Player.ePlayerType.Human)
            {
                invalidInputMoveOptionMessage.AppendLine("Sorry, your move choice isn't valid!");
                invalidInputMoveOptionMessage.Append("Please enter a new valid move: ");
                Console.WriteLine(invalidInputMoveOptionMessage);
            }
        }

        public static bool BoardSizeInputValueValidation(int i_BoardSize)
        {
            bool boardSizeIsValid;

            if (i_BoardSize == 6 || i_BoardSize == 8 || i_BoardSize == 10)
            {
                boardSizeIsValid = true;
            }

            else
            {
                boardSizeIsValid = false;
            }

            return boardSizeIsValid;
        }

        public static char GetCharByDiscType(Game.eDiscType i_DiscTypeNum)
        {
            char discTypeChar;

            if (i_DiscTypeNum == Game.eDiscType.XDisc)
            {
                discTypeChar = 'X';
            }

            else if (i_DiscTypeNum == Game.eDiscType.ODisc)
            {
                discTypeChar = 'O';
            }

            else if (i_DiscTypeNum == Game.eDiscType.XKingDisc)
            {
                discTypeChar = 'K';
            }

            else if (i_DiscTypeNum == Game.eDiscType.OKingDisc)
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
                System.Threading.Thread.Sleep(200);
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
                if (columnIndex == i_Board.BoardSize)
                {                   
                    columnIndex = 0;
                    PrintRowBorder(i_Board.BoardSize);
                }
            }

            Console.WriteLine(Environment.NewLine);
        }

        public void PrintSquare(Game.eDiscType i_CurrSquareDiscType)
        {
            if (i_CurrSquareDiscType != Game.eDiscType.None)
            {
                Console.Write(" {0} | ", GetCharByDiscType(i_CurrSquareDiscType));
            }

            else
            {
                Console.Write("   | ");
            }
        }

        public void PrintNewRowLetter(ref char io_RowLetter)
        {
            StringBuilder rawLetter = new StringBuilder();

            rawLetter.Append(string.Format("{0}|", io_RowLetter));
            Console.Write(rawLetter);
            io_RowLetter = (char)(io_RowLetter + 1);          
        }

        public void PrintRowBorder(int i_BoardSize)
        {
            int index;
            StringBuilder rowBorder;

            Console.WriteLine(Environment.NewLine);
            for (index = 0; index < i_BoardSize; index++)
            {
                rowBorder = new StringBuilder();
                rowBorder.Append("=====");
                Console.Write(rowBorder);
            }
            Console.WriteLine(Environment.NewLine);
        }

        public void PrintColumnsFrame(int i_BoardSize)
        {
            int columnIndex;
            char columnLetter = 'A';
            StringBuilder space = new StringBuilder();
            StringBuilder rowBorder;
            StringBuilder columnNumber;

            space.Append(" ");
            Console.Write(space);
            for (columnIndex = 0; columnIndex < i_BoardSize; columnIndex++)
            {
                columnNumber = new StringBuilder();
                columnNumber.Append(string.Format(" {0} | ", columnLetter));
                Console.Write(columnNumber);
                columnLetter = (char)(columnLetter + 1);              
            }

            Console.WriteLine(Environment.NewLine);
            for (columnIndex = 0; columnIndex < i_BoardSize; columnIndex++)
            {
                rowBorder = new StringBuilder();
                rowBorder.Append("=====");
                Console.Write(rowBorder);
            }

            Console.WriteLine(Environment.NewLine);
        }
            
        public void RequestMoveInput()
        {
            m_RawMoveInputManager.LoadNewInput();
            while (!m_RawMoveInputManager.RawInputIsValid)
            {
                PrintInvalidInputStructure();
                m_RawMoveInputManager.LoadNewInput();
            }
        }

        public void PrintSingleGameResult(Game.eGameResult i_GameResult,Player i_FirstPlayer, Player i_SecondPlayer)
        {
            StringBuilder gameResult = new StringBuilder();
            StringBuilder totalScoreMessage = new StringBuilder();

           if(i_GameResult == Game.eGameResult.FirstPlayerWon)
            {
                gameResult.Append(string.Format("The Winner is {0}!", i_FirstPlayer.Name));
                Console.WriteLine(gameResult);
            }

           else if(i_GameResult == Game.eGameResult.SecondPlayerWon)
            {
                gameResult.Append(string.Format("The Winner is {0}!", i_SecondPlayer.Name));
                Console.WriteLine(gameResult);
            }
            
           else ///The game result is draw
            {
                gameResult.Append("The game result is draw!");
                Console.WriteLine(gameResult);
            }

            totalScoreMessage.Append("The total score till now: ");
            Console.WriteLine(totalScoreMessage);
            PrintScore(i_FirstPlayer, i_SecondPlayer);
        }

        public void PrintAllGameSessionsResult(Game.eGameResult i_FinalCheckersSessionResult, Player i_FirstPlayer, Player i_SecondPlayer)
        {
            StringBuilder sessionResult = new StringBuilder();
            StringBuilder sessionScore = new StringBuilder();

            if (i_FinalCheckersSessionResult == Game.eGameResult.FirstPlayerWon)
            {
                sessionResult.Append(string.Format("The final Winner is {0}!", i_FirstPlayer.Name));
                Console.WriteLine(sessionResult);
            }

            else if (i_FinalCheckersSessionResult == Game.eGameResult.SecondPlayerWon)
            {
                sessionResult.Append(string.Format("The final Winner is {0}!", i_SecondPlayer.Name));
                Console.WriteLine(sessionResult);
            }

            else ///The game result is draw
            {
                sessionResult.Append("This Checkers session final result is a draw!"); 
                Console.WriteLine(sessionResult);
            }

            sessionScore.Append("Final session score: ");
            Console.WriteLine(sessionScore);
            PrintScore(i_FirstPlayer, i_SecondPlayer);
        }

        public void PrintScore(Player i_FirstPlayer, Player i_SecondPlayer)
        {
            StringBuilder score = new StringBuilder();

            score.AppendLine(string.Format("{0}'s score: {1}", i_FirstPlayer.Name, i_FirstPlayer.Score));
            score.Append(string.Format("{0}'s score: {1}", i_SecondPlayer.Name, i_SecondPlayer.Score));
            Console.WriteLine(score);
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

            if (Char.ToUpper(userChoice) == k_Quit)
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

            if ((Char.ToUpper(i_UserChoice) == k_Continue || Char.ToUpper(i_UserChoice) == k_Quit))
            {
                anotherTurnInputIsValid = true;
            }

            else
            {
                anotherTurnInputIsValid = false;
            }

            return anotherTurnInputIsValid;
        }

        public static void PrintAnotherGameRequest()
        {
            StringBuilder anotherGameRequest = new StringBuilder();

            anotherGameRequest.AppendLine("Do you want to go for another game?");
            anotherGameRequest.Append("If so, press 'Y' to start a new game. If Not press 'Q' to quit");
            Console.WriteLine(anotherGameRequest);
        }

        public void PrintLastMoveByRawInput(StringBuilder i_RawInput)
        {
            Console.WriteLine(i_RawInput);
        }
    }
}
