using System;
using System.Text;
using CheckersGame;

namespace CheckersUI
{
    public class IOManager
    {
        private readonly RawMoveInputManager r_RawMoveInputManager;
        private bool m_GetSecondPlayerName;
        public const int k_MaximumNameLength = 20;
        public const char k_Quit = 'Q';
        public const char k_Continue = 'Y';

        public IOManager()
        {
            r_RawMoveInputManager = new RawMoveInputManager();
        }

        public RawMoveInputManager RawMoveInputManager
        {
            get
            {
                return r_RawMoveInputManager;
            }
        }

        // should start with i_. read the coding standards and use the stylecop !!!!!!
        public void GetGameDetailsProcedure(GameDetails io_GameDetails)
        {
            int gameModeChoice;

            m_GetSecondPlayerName = false;
            io_GameDetails.FirstPlayerName = GetPlayerName();
            io_GameDetails.BoardSize = GetSizeOfBoard();
            gameModeChoice = GetGameMode();

            if (gameModeChoice == 1)
            {
                io_GameDetails.GameMode = CheckersGame.GameLogic.eGameMode.SinglePlayerMode;
                io_GameDetails.SecondPlayerName.Append("The Computer");
            }

            else //gameModeChoice == 2
            {
                io_GameDetails.GameMode = CheckersGame.GameLogic.eGameMode.TwoPlayersMode;
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
                name.Remove(0, name.Length);
                invalidNameMessage.Append("The inserted name is too long! Please enter a valid name:");
                Console.WriteLine(invalidNameMessage);
                name.Append(System.Console.ReadLine());
            }

            return name;
        }

        // $G$ NTT-999 (-5) This method should be private (None of the other classes used this method...)
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

        // $G$ NTT-999 (-5) This method should be private (None of the other classes used this method...)
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

        public bool AskForAnotherRound()
        {
            bool playAnotherRound;
            bool userChoiceIsValid;
            char userChoice;

            PrintAnotherGameRequest();
            userChoiceIsValid = char.TryParse(Console.ReadLine(), out userChoice);
            while (!userChoiceIsValid || !AnotherTurnInputValidation(userChoice))
            {
                PrintInvalidInputMessage();
                PrintAnotherGameRequest();
                userChoiceIsValid = char.TryParse(Console.ReadLine(), out userChoice);
            }

            if (char.ToUpper(userChoice) == k_Quit)
            {
                playAnotherRound = false;
            }

            else
            {
                playAnotherRound = true;
            }

            return playAnotherRound;
        }

        // $G$ NTT-999 (-5) This method should be private (None of the other classes used this method...)
        public bool AnotherTurnInputValidation(char i_UserChoice)
        {
            bool anotherTurnInputIsValid;

            if ((char.ToUpper(i_UserChoice) == k_Continue || char.ToUpper(i_UserChoice) == k_Quit))
            {
                anotherTurnInputIsValid = true;
            }

            else
            {
                anotherTurnInputIsValid = false;
            }

            return anotherTurnInputIsValid;
        }

        // $G$ NTT-999 (-5) This method should be private (None of the other classes used this method...)
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

        //  This method should be private (None of the other classes used this method...)
        public static char GetCharByDiscType(GameLogic.eDiscType i_DiscTypeNum)
        {
            char discTypeChar;

            if (i_DiscTypeNum == GameLogic.eDiscType.XDisc)
            {
                discTypeChar = 'X';
            }

            else if (i_DiscTypeNum == GameLogic.eDiscType.ODisc)
            {
                discTypeChar = 'O';
            }

            else if (i_DiscTypeNum == GameLogic.eDiscType.XKingDisc)
            {
                discTypeChar = 'K';
            }

            else if (i_DiscTypeNum == GameLogic.eDiscType.OKingDisc)
            {
                discTypeChar = 'U';
            }

            else
            {
                discTypeChar = 'N';
            }

            return discTypeChar;
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

        public void PrintBoard(Board i_Board, Player.ePlayerType i_PlayerType)
        {
            int columnIndex = 0;
            char rowLetter = 'a';

            if (i_PlayerType == Player.ePlayerType.Computer)
            {
                System.Threading.Thread.Sleep(3000);
            }

            /// Ex02.ConsoleUtils.Screen.Clear();
            PrintColumnsFrame(i_Board.BoardSize);
            foreach (Square currSquare in i_Board.GameBoard)
            {
                if (columnIndex == 0)
                {
                    PrintNewRowLetter(ref rowLetter);
                }

                PrintSquare(currSquare.DiscType);
                columnIndex++;
                if (columnIndex == i_Board.BoardSize)
                {                   
                    columnIndex = 0;
                    Console.WriteLine(Environment.NewLine);
                    PrintRowBorder(i_Board.BoardSize);
                    Console.WriteLine(Environment.NewLine); 
                }
            }
        }

        //  This method should be private (None of the other classes used this method...)
        public void PrintSquare(GameLogic.eDiscType i_CurrSquareDiscType)
        {
            StringBuilder squareContent = new StringBuilder(); 

            if (i_CurrSquareDiscType != GameLogic.eDiscType.None)
            {
                squareContent.Append(String.Format(" {0} |", GetCharByDiscType(i_CurrSquareDiscType)));
            }

            else
            {
                squareContent.Append("   |");
            }

            Console.Write(squareContent);
        }

        //  This method should be private (None of the other classes used this method...)
        public void PrintNewRowLetter(ref char io_RowLetter)
        {
            StringBuilder rawLetter = new StringBuilder();

            rawLetter.Append(string.Format(" {0} |", io_RowLetter));
            Console.Write(rawLetter);
            io_RowLetter = (char)(io_RowLetter + 1);          
        }

        public void PrintRowBorder(int i_BoardSize)
        {
            int index;
            StringBuilder rowBorder = new StringBuilder();

            rowBorder.Append("====");
            Console.Write(rowBorder);

            for (index = 0; index < i_BoardSize; index++)
            {
                Console.Write(rowBorder);
            }
        }

        //  This method should be private (None of the other classes used this method...)
        public void PrintColumnsFrame(int i_BoardSize)
        {
            int columnIndex;
            char letter = 'A';
            StringBuilder space = new StringBuilder();
            StringBuilder columnLetter;

            space.Append("    ");
            Console.Write(space);
            for (columnIndex = 0; columnIndex < i_BoardSize; columnIndex++)
            {
                columnLetter = new StringBuilder();
                columnLetter.Append(string.Format(" {0} |", letter));
                Console.Write(columnLetter);
                letter = (char)(letter + 1);              
            }

            Console.WriteLine(Environment.NewLine);
            PrintRowBorder(i_BoardSize);
            Console.WriteLine(Environment.NewLine);

        }
            
        ///public void GetMoveInput()
        public PotentialMove GetMoveInput()
        {
            PotentialMove newPotentialMove = new PotentialMove();

            r_RawMoveInputManager.LoadNewInput();
            while (!r_RawMoveInputManager.RawInputIsValid)
            {
                PrintInvalidInputStructure();
                r_RawMoveInputManager.LoadNewInput();
            }

            newPotentialMove.SrcIdx = r_RawMoveInputManager.SourceIndex;
            newPotentialMove.DestIdx = r_RawMoveInputManager.DestinationIndex;

            return newPotentialMove;
        }

        public void PrintSingleGameResult(GameLogic.eGameResult i_GameResult,Player i_FirstPlayer, Player i_SecondPlayer)
        {
            StringBuilder gameResult = new StringBuilder();
            StringBuilder totalScoreMessage = new StringBuilder();

           if(i_GameResult == GameLogic.eGameResult.FirstPlayerWon)
            {
                gameResult.Append(string.Format("The Winner is {0}!", i_FirstPlayer.Name));
                Console.WriteLine(gameResult);
            }

           else if(i_GameResult == GameLogic.eGameResult.SecondPlayerWon)
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

        public void PrintAllGameSessionsResult(GameLogic.eGameResult i_FinalCheckersSessionResult, Player i_FirstPlayer, Player i_SecondPlayer)
        {
            StringBuilder sessionResult = new StringBuilder();
            StringBuilder sessionScore = new StringBuilder();

            if (i_FinalCheckersSessionResult == GameLogic.eGameResult.FirstPlayerWon)
            {
                sessionResult.Append(string.Format("The final Winner is {0}!", i_FirstPlayer.Name));
                Console.WriteLine(sessionResult);
            }

            else if (i_FinalCheckersSessionResult == GameLogic.eGameResult.SecondPlayerWon)
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

        //  This method should be private (None of the other classes used this method...)
        public void PrintScore(Player i_FirstPlayer, Player i_SecondPlayer)
        {
            StringBuilder score = new StringBuilder();

            score.AppendLine(string.Format("{0}'s score: {1}", i_FirstPlayer.Name, i_FirstPlayer.Score));
            score.Append(string.Format("{0}'s score: {1}", i_SecondPlayer.Name, i_SecondPlayer.Score));
            Console.WriteLine(score);
        }

        public void PrintLastMoveByRawInput(StringBuilder i_RawInput, Player i_CurrPlayer)
        {
            StringBuilder lastMoveInfo = new StringBuilder();

            lastMoveInfo.Append(string.Format("{0}'s move was ({1}): {2}", i_CurrPlayer.Name, GetCharByDiscType(i_CurrPlayer.DiscType), i_RawInput));
            Console.WriteLine(lastMoveInfo);
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

        public static void PrintInvalidInputStructure()
        {
            StringBuilder invalidInputStructureMessage = new StringBuilder();

            invalidInputStructureMessage.AppendLine("Sorry, your input structure isn't valid.");
            invalidInputStructureMessage.Append("Please enter a new move: ");
            Console.WriteLine(invalidInputStructureMessage);  
        }

        public static void PrintAnotherGameRequest()
        {
            StringBuilder anotherGameRequest = new StringBuilder();

            anotherGameRequest.AppendLine("Do you want to go for another game?");
            anotherGameRequest.Append("If so, press 'Y' to start a new game. If Not press 'Q' to quit");
            Console.WriteLine(anotherGameRequest);
        }
    }
}
