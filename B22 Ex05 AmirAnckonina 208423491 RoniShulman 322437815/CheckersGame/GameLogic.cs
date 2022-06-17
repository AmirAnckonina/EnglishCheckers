using System;
using System.Collections.Generic;

namespace CheckersGame
{
    public class GameLogic
    {
        public enum eGameMode
        {
            SinglePlayerMode,
            TwoPlayersMode
        }

        public enum eGameResult 
        {
            FirstPlayerWon,
            SecondPlayerWon,
            Draw
        }

        public enum eDiscType
        {
            XDisc,
            ODisc,
            XKingDisc,
            OKingDisc,
            None
        }

        private Board m_Board;
        private Player m_FirstPlayer;
        private Player m_SecondPlayer;
        private Player m_CurrentPlayer;
        private Player m_RivalPlayer;
        private readonly MoveManager r_MoveManager;
        private eGameMode m_GameMode;
        private eGameResult m_SingleGameResult;
        private eGameResult m_FinalCheckersSessionResult;
        private bool m_IsRecurringTurn;

        public event EventHandler SingleGameOver;
        public event EventHandler TurnSwitched;
        public event EventHandler SingleGameInitialized;
        public event EventHandler InvalidMoveInserted;
        public event EventHandler MoveExecuted;
        public event EventHandler CurrPlayerReachedLastLine;

        public GameLogic()
        {
            r_MoveManager = new MoveManager();
        }

        public MoveManager MoveManager
        {
            get
            {
                return r_MoveManager;
            }
        }

        public Player CurrentPlayer
        {
            get
            {
                return m_CurrentPlayer;
            }

            set
            {
                m_CurrentPlayer = value;
            }
        }

        public Player RivalPlayer
        {
            get
            {
                return m_RivalPlayer;
            }

            set
            {
                m_RivalPlayer = value;
            }
        }

        public Player FirstPlayer
        {
            get
            {
                return m_FirstPlayer;
            }

            set
            {
                m_FirstPlayer = value;
            }
        }

        public Player SecondPlayer
        {
            get
            {
                return m_SecondPlayer;
            }

            set
            {
                m_SecondPlayer = value;
            }
        }

        public Board Board
        {
            get
            {
                return m_Board;
            }

            set
            {
                m_Board = value;
            }
        }

        public eGameMode GameMode
        {
            get
            {
                return m_GameMode;
            }

            set
            {
                m_GameMode = value;
            }
        }

        public eGameResult SingleGameResult
        {
            get
            {
                return m_SingleGameResult;
            }

            set
            {
                m_SingleGameResult = value;
            }
        }

        public eGameResult FinalCheckersSessionResult
        {
            get
            {
                return m_FinalCheckersSessionResult;
            }

            set
            {
                m_FinalCheckersSessionResult = value;
            }
        }

        public bool IsRecurringTurn
        {
            get
            {
                return m_IsRecurringTurn;
            }

            set
            {
                m_IsRecurringTurn = value;
            }
        }

        public void SetGameObjects(string i_FirstPlayerName, string i_SecondPlayerName,int i_BoardSize, bool i_Player2IsHuman)
        {
            Player.ePlayerType secondPlayerType;

            if (i_Player2IsHuman)
            {
                secondPlayerType = Player.ePlayerType.Human;
            }

            else /// In case it's a Single Player Game Mode.
            {
                secondPlayerType = Player.ePlayerType.Computer;
            }

            m_FirstPlayer = new Player
                (i_FirstPlayerName,
                GameLogic.eDiscType.ODisc,
                GameLogic.eDiscType.OKingDisc,
                Player.ePlayerType.Human,
                Player.ePlayerMovingDirection.Down,
                Player.ePlayerRecognition.FirstPlayer);
            m_SecondPlayer = new Player
                (i_SecondPlayerName,
                GameLogic.eDiscType.XDisc,
                GameLogic.eDiscType.XKingDisc,
                secondPlayerType,
                Player.ePlayerMovingDirection.Up,
                Player.ePlayerRecognition.SecondPlayer);
            m_Board = new Board(i_BoardSize);
            m_Board.InitializeBoard(m_FirstPlayer , m_SecondPlayer);
            m_FirstPlayer.NumOfDiscs = m_Board.GetDiscOccurences(m_FirstPlayer.DiscType);
            m_SecondPlayer.NumOfDiscs = m_Board.GetDiscOccurences(m_SecondPlayer.DiscType);
            m_FirstPlayer.InitializeCurrentHoldingIndices(m_Board);
            m_SecondPlayer.InitializeCurrentHoldingIndices(m_Board);
            TurnsSetup();
            m_IsRecurringTurn = false;
            ReportSingleGameInitialized();
        }

        public void ResetObjectsBetweenSessions()
        {
            FirstPlayer.NumOfDiscs = 0;
            SecondPlayer.NumOfDiscs = 0;
            FirstPlayer.CurrentHoldingSquareIndices.Clear();
            SecondPlayer.CurrentHoldingSquareIndices.Clear();
            m_Board.InitializeBoard(m_FirstPlayer, m_SecondPlayer);
            m_FirstPlayer.NumOfDiscs = m_Board.GetDiscOccurences(m_FirstPlayer.DiscType);
            m_SecondPlayer.NumOfDiscs = m_Board.GetDiscOccurences(m_SecondPlayer.DiscType);
            m_FirstPlayer.InitializeCurrentHoldingIndices(m_Board);
            m_SecondPlayer.InitializeCurrentHoldingIndices(m_Board);
            m_IsRecurringTurn = false;
            TurnsSetup();
            ReportSingleGameInitialized();
        }

        private void TurnsSetup()
        {
            m_CurrentPlayer = FirstPlayer;
            m_RivalPlayer = SecondPlayer;
        }

        private void ReportSingleGameInitialized()
        {
            GameInitializedEventArgs gameInitializedParams = new GameInitializedEventArgs();

            foreach (Square sqr in m_Board.GameBoard)
            {
                if (sqr.SquareHolder != Player.ePlayerRecognition.None)
                {
                    gameInitializedParams.AddOccuipiedPointToList(sqr);
                }
            }

            OnSingleGameInitialized(gameInitializedParams);
        }

        protected virtual void OnSingleGameInitialized(GameInitializedEventArgs i_GameInitializedParams)
        {
            if (SingleGameInitialized != null)
            {
                SingleGameInitialized.Invoke(this, i_GameInitializedParams);
            }
        }

        public void MoveProcedure(PotentialMove i_PotentialMove = null)
        {
            CurrentPlayerAnyEatingMovePossibilityCheck();
            if (CurrentPlayer.PlayerType == Player.ePlayerType.Human)
            {
                LoadSpecificNewPotentialMove(i_PotentialMove);
            }

            else /// Computer's turn
            {
                GenerateAndLoadNewPotentialMove();
            }

            CompleteMoveProcedure();
        }

        private void CompleteMoveProcedure()
        {
            bool validMove;

            validMove = MoveValidationProcedure();
            if (validMove) // If and only if is valid
            {
                ExecuteMoveProcedure();
                PostMoveProcedure();
            }

            else
            {
                OnInvalidMoveInserted();
            }
        }

        private bool MoveValidationProcedure()
        {
            bool validMove;
            
            if (!m_IsRecurringTurn)
            {
                validMove = r_MoveManager.MoveValidation(m_Board, m_CurrentPlayer);
            }

            else
            {
                validMove = r_MoveManager.RecurringTurnMoveValidation(m_Board, m_CurrentPlayer);
            }

            return validMove;
        }

        private void ExecuteMoveProcedure()
        {
            r_MoveManager.ExecuteMove(m_Board, m_CurrentPlayer);
            ReportMoveExecuted();
        }

        private void ReportMoveExecuted()
        {
            MoveExecutedEventArgs moveExecutedEventArgs = new MoveExecutedEventArgs();

            moveExecutedEventArgs.AddNewOccuipiedPoint(m_Board[r_MoveManager.DestIdx]);
            moveExecutedEventArgs.AddNewEmptyPoint(m_Board[r_MoveManager.SrcIdx]);
            if (r_MoveManager.EatingMoveOccurred())
            {
                moveExecutedEventArgs.AddNewEmptyPoint(m_Board[r_MoveManager.EatedSquareIdx]);
            }

            OnMoveExecuted(moveExecutedEventArgs);
        }

        protected virtual void OnMoveExecuted(MoveExecutedEventArgs i_MUEventArgs)
        {
            if (MoveExecuted != null)
            {
                MoveExecuted.Invoke(this, i_MUEventArgs);
            }
        }

        protected virtual void OnInvalidMoveInserted()
        {
            if (InvalidMoveInserted != null)
            {
                InvalidMoveInserted.Invoke(this, null);
            }
        }

        private void LoadSpecificNewPotentialMove(PotentialMove i_PotentialMove)
        {
            r_MoveManager.SrcIdx = i_PotentialMove.SrcIdx;
            r_MoveManager.DestIdx = i_PotentialMove.DestIdx;
        }

        private void GenerateAndLoadNewPotentialMove()
        {
            PotentialMove newPotentialMove;

            if (m_IsRecurringTurn)
            {
                newPotentialMove = new PotentialMove(r_MoveManager.RecurringTurnNewSrcIdx, r_MoveManager.DestIdx);

                /// immediately load the SquareIndex we reached in the last move.
                LoadSpecificNewPotentialMove(newPotentialMove);
            }

            else if (r_MoveManager.OnlyEatingIsValid)
            {
                /// In case there is an option to eat
                GenerateAndLoadNewRandomEatingMove();
            }

            else
            {
                GenerateAndLoadNewRandomSimpleMove();
            }
        }

        private void GenerateAndLoadNewRandomEatingMove()
        {
            bool isValidEatingMove;
            int generatedIndexFromList;
            var random = new Random();
            SquareIndex currentSquareIndex;
            List<SquareIndex> tempHoldingSquareIndices = new List<SquareIndex>(m_CurrentPlayer.CurrentHoldingSquareIndices);

            generatedIndexFromList = random.Next(tempHoldingSquareIndices.Count);
            currentSquareIndex = tempHoldingSquareIndices[generatedIndexFromList];
            isValidEatingMove = r_MoveManager.AnyEatingMovePossibiltyCheckByIndex(currentSquareIndex, m_Board, m_CurrentPlayer);
            while (!isValidEatingMove)
            {
                tempHoldingSquareIndices.Remove(currentSquareIndex);
                generatedIndexFromList = random.Next(tempHoldingSquareIndices.Count);
                currentSquareIndex = tempHoldingSquareIndices[generatedIndexFromList];
                isValidEatingMove = r_MoveManager.AnyEatingMovePossibiltyCheckByIndex(currentSquareIndex, m_Board, m_CurrentPlayer);
            }
        }

        private void GenerateAndLoadNewRandomSimpleMove()
        {
            bool isValidEatingMove;
            int generatedIndexFromList;
            var random = new Random();
            SquareIndex currentSquareIndex;
            List<SquareIndex> tempHoldingSquareIndices = new List<SquareIndex>(m_CurrentPlayer.CurrentHoldingSquareIndices);

            generatedIndexFromList = random.Next(tempHoldingSquareIndices.Count);
            currentSquareIndex = tempHoldingSquareIndices[generatedIndexFromList];
            isValidEatingMove = r_MoveManager.AnySimpleMovePossibiltyCheck(currentSquareIndex, m_Board, m_CurrentPlayer);
            while (!isValidEatingMove)
            {
                tempHoldingSquareIndices.Remove(currentSquareIndex);
                generatedIndexFromList = random.Next(tempHoldingSquareIndices.Count);
                currentSquareIndex = tempHoldingSquareIndices[generatedIndexFromList];
                isValidEatingMove = r_MoveManager.AnySimpleMovePossibiltyCheck(currentSquareIndex, m_Board, m_CurrentPlayer);
            }
        }

        private void PostMoveProcedure()
        {
            ReachedLastLineProcedure();
            PostEatingMoveProcedure();
            m_CurrentPlayer.UpdateCurrentHoldingSquareIndices(r_MoveManager.SrcIdx, r_MoveManager.DestIdx);
            if (!RecurringTurnPossibilityValidationAndUpdate())
            {
                NonRecurringTurnProcedure();
            }
        }

        private void NonRecurringTurnProcedure()
        {
            if (GameOverCheck())
            {
                ScoreCalculationAndUpdate();
                ReportGameOver();
            }

            else
            {
                SwitchTurn();
            }
        }

        private void PostEatingMoveProcedure()
        {
            if (r_MoveManager.EatingMoveOccurred())
            {
                m_RivalPlayer.NumOfDiscs--;
                m_RivalPlayer.RemoveIndexFromCurrentHoldingSquareIndices(r_MoveManager.EatedSquareIdx);
            }
        }

        private void ReachedLastLineProcedure()
        {
            bool reachedLastLine = r_MoveManager.ReachedLastLineValidationAndUpdate(m_Board, m_CurrentPlayer);

            if (reachedLastLine)
            {
                ReportReachedLastLine();
            }
        }
        
        private void ReportReachedLastLine()
        {
            ReachedLastLineEventArgs reachedLastLineParams = new ReachedLastLineEventArgs(m_Board[r_MoveManager.DestIdx]);

            OnCurrPlayerReachedLastLine(reachedLastLineParams);
        }

        private void ReportGameOver()
        {
            GameOverEventArgs gameOverParams;
            int winnerScore;
            
            if (m_SingleGameResult == eGameResult.FirstPlayerWon)
            {
                winnerScore = FirstPlayer.Score;
            }

            else if (m_SingleGameResult == eGameResult.SecondPlayerWon)
            {
                winnerScore = SecondPlayer.Score;
            }

            else
            {
                winnerScore = 0;
            }

            gameOverParams = new GameOverEventArgs(m_SingleGameResult, winnerScore, m_FirstPlayer.Name, m_SecondPlayer.Name);
            OnSingleGameOver(gameOverParams);
        }

        protected virtual void OnSingleGameOver(GameOverEventArgs i_GameOverEventArgs)
        {
            if (SingleGameOver != null)
            {
                SingleGameOver.Invoke(this, i_GameOverEventArgs);
            }
        }

        protected virtual void OnCurrPlayerReachedLastLine(ReachedLastLineEventArgs i_ReachedLastLineParams)
        {
            if (CurrPlayerReachedLastLine != null)
            {
                CurrPlayerReachedLastLine.Invoke(this, i_ReachedLastLineParams);
            }
        }

        private bool RecurringTurnPossibilityValidationAndUpdate() 
        {
            bool recurringTurnIsPossible;

            if (r_MoveManager.EatingMoveOccurred() && r_MoveManager.RecurringTurnPossibiltyCheck(r_MoveManager.DestIdx ,m_Board, m_CurrentPlayer))
            {
                r_MoveManager.RecurringTurnNewSrcIdx = r_MoveManager.SrcIdx;
                recurringTurnIsPossible = true;
                m_IsRecurringTurn = true;
                if (m_CurrentPlayer.PlayerType == Player.ePlayerType.Computer)
                {
                    MoveProcedure();
                }
            }

            else
            {
                recurringTurnIsPossible = false;
                m_IsRecurringTurn = false;
            }

            return recurringTurnIsPossible;
        }

        private void SwitchTurn()
        {
            if (FirstPlayer == CurrentPlayer) 
            {
                m_CurrentPlayer = m_SecondPlayer;
                m_RivalPlayer = m_FirstPlayer;
            }

            else //Currently, it's the second player turn
            {
                m_CurrentPlayer = m_FirstPlayer;
                m_RivalPlayer = m_SecondPlayer;
            }

            OnSwitchTurn();
        }

        protected virtual void OnSwitchTurn()
        {
            if (TurnSwitched != null)
            {
                TurnSwitched.Invoke(this, null);
            }
        }

        private bool GameOverCheck()
        {
            bool isGameOver;

            if (m_RivalPlayer.NumOfDiscs == 0)
            {
                isGameOver = true;
                SaveSingleGameResult(m_CurrentPlayer.PlayerRecognition);
            }

            else if(!PlayerMovementPossibilityCheck(m_RivalPlayer))
            { 
                isGameOver = true;
                if (!PlayerMovementPossibilityCheck(m_CurrentPlayer))
                {
                    SaveSingleGameResult(Player.ePlayerRecognition.None);
                }

                else /// So the current player can keep moving
                {
                    SaveSingleGameResult(m_CurrentPlayer.PlayerRecognition);
                }
            }

            else
            {
                isGameOver = false;
            }

            return isGameOver;
        }

        private void SaveSingleGameResult(Player.ePlayerRecognition i_WinnerPlayerRecognition)
        {
            if (i_WinnerPlayerRecognition == Player.ePlayerRecognition.FirstPlayer)
            {
                m_SingleGameResult = eGameResult.FirstPlayerWon;
            }

            else if (i_WinnerPlayerRecognition == Player.ePlayerRecognition.SecondPlayer)
            {
                m_SingleGameResult = eGameResult.SecondPlayerWon;
            }

            else /// None of the players won.
            {
                m_SingleGameResult = eGameResult.Draw;
            }
        }

        private bool PlayerMovementPossibilityCheck(Player i_Player)
        {
            bool playerAbleToMove; 

            /// Set first to false so if "true" will be returned from a single SquareIndex check,
            /// we will go out from the loop via break.
            playerAbleToMove = false; 
            foreach (SquareIndex currSquareIndex in i_Player.CurrentHoldingSquareIndices)
            {
                playerAbleToMove = r_MoveManager.AnyMovePossibilityCheckByIndex(currSquareIndex, m_Board, i_Player);
                if (playerAbleToMove)
                {
                    break;
                }
            }

            return playerAbleToMove;
        }

        private bool CurrentPlayerAnyEatingMovePossibilityCheck()
        {
            bool playerCanMakeEatingMove;

            /// Set first to false so if "true" will be returned from a single SquareIndex check,
            /// we will go out from the loop via break.
            playerCanMakeEatingMove = false;
            r_MoveManager.OnlyEatingIsValid = false;
            foreach (SquareIndex currSquareIndex in m_CurrentPlayer.CurrentHoldingSquareIndices)
            {
                playerCanMakeEatingMove = r_MoveManager.AnyEatingMovePossibiltyCheckByIndex(currSquareIndex, m_Board, m_CurrentPlayer);
                if (playerCanMakeEatingMove)
                {
                    r_MoveManager.OnlyEatingIsValid = true;
                    break;
                }
            }

            return playerCanMakeEatingMove;
        }

        private void ScoreCalculationAndUpdate()
        {
            int singleGameScore;
            int firstPlayerTotalDiscValues;
            int secondPlayerTotalDiscValues;

            firstPlayerTotalDiscValues = m_FirstPlayer.CalculatePlayerDiscValuesAfterSingleGame(m_Board);
            secondPlayerTotalDiscValues = m_SecondPlayer.CalculatePlayerDiscValuesAfterSingleGame(m_Board);
            singleGameScore = Math.Abs(firstPlayerTotalDiscValues - secondPlayerTotalDiscValues);
            if(m_SingleGameResult == eGameResult.FirstPlayerWon)
            {
                m_FirstPlayer.Score += singleGameScore;
            }

            else if(m_SingleGameResult == eGameResult.SecondPlayerWon)
            {
                m_SecondPlayer.Score += singleGameScore;
            }
        } 
    }
}

