using System;
using System.Collections.Generic;
using System.Text;

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

        public event EventHandler GameOver;

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

        public void SetGameObjects(StringBuilder i_FirstPlayerName, StringBuilder i_SecondPlayerName,int i_BoardSize)
        {
            Player.ePlayerType secondPlayerType;

            if (m_GameMode == eGameMode.TwoPlayersMode)
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
                Player.ePlayerType.Computer,
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

            /// Add Initialize to Both players PotentialMoves Lists

            m_FirstPlayer.InitializeCurrentHoldingIndices(m_Board);
            m_SecondPlayer.InitializeCurrentHoldingIndices(m_Board);
        }

        public void LoadSpecificNewPotentialMove(PotentialMove i_PotentialMove)
        {
            r_MoveManager.SrcIdx = i_PotentialMove.SrcIdx;
            r_MoveManager.DestIdx = i_PotentialMove.DestIdx;
        }

        public void GenerateAndLoadNewPotentialMove()
        {
            PotentialMove newPotentialMove = new PotentialMove();

            newPotentialMove.SrcIdx = r_MoveManager.RecurringTurnNewSrcIdx;
            newPotentialMove.DestIdx = r_MoveManager.DestIdx;
            if (m_IsRecurringTurn)
            {
                /// immediately load the SquareIndex we reached in the last move.
                /// Get the last DestIdx
                /// Get From CurrPlayer EatingMovesList all PotenitialMoves with SrcIdx as the just updated DestIdx
                /// Generate Random from this Potential Moves one.
                LoadSpecificNewPotentialMove(newPotentialMove);
            }

            else if (r_MoveManager.OnlyEatingIsValid)
            {
                /// In case there is an option to eat
                /// Generate one fromCurrPlayer EatingMovesList
                GenerateAndLoadNewRandomEatingMove();
            }

            else
            {
                /// Generate from CurrPlayer SimpleMovesList
                GenerateAndLoadNewRandomSimpleMove();
            }
        }

        public void GenerateAndLoadNewRandomEatingMove()
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

        public void GenerateAndLoadNewRandomSimpleMove()
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
       
        public void PostMoveProcedure()
        {
            r_MoveManager.ReachedLastLineValidationAndUpdate(m_Board, m_CurrentPlayer);
            if (r_MoveManager.EatingMoveOccurred())
            {
                m_RivalPlayer.NumOfDiscs--;
                m_RivalPlayer.RemoveIndexFromCurrentHoldingSquareIndices(r_MoveManager.EatedSquareIdx);
            }
            m_CurrentPlayer.UpdateCurrentHoldingSquareIndices(r_MoveManager.SrcIdx, r_MoveManager.DestIdx);
        }

        public bool RecurringTurnPossibilityValidation() 
        {
            bool recurringTurnIsPossible;

            if (r_MoveManager.EatingMoveOccurred() && r_MoveManager.RecurringTurnPossibiltyCheck(r_MoveManager.DestIdx ,m_Board, m_CurrentPlayer))
            {
                r_MoveManager.RecurringTurnNewSrcIdx.CopySquareIndices(r_MoveManager.SrcIdx);
                recurringTurnIsPossible = true;
                m_IsRecurringTurn = true;
            }

            else
            {
                recurringTurnIsPossible = false;
                m_IsRecurringTurn = false;
            }

            return recurringTurnIsPossible;
        }

        public void TurnsSetup()
        {
            m_CurrentPlayer = m_SecondPlayer; 
            m_RivalPlayer = m_FirstPlayer;
        }

        public void SwitchTurn()
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
        }

        public bool GameOverCheck(bool i_IsQPressed)
        {
            bool isGameOver;

            if (i_IsQPressed) 
            {
                isGameOver = true;
                SaveSingleGameResult(m_RivalPlayer.PlayerRecognition);
            }

            else if (m_RivalPlayer.NumOfDiscs == 0)
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

        public void SaveSingleGameResult(Player.ePlayerRecognition i_WinnerPlayerRecognition)
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

        public bool PlayerMovementPossibilityCheck(Player i_Player)
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

        public bool CurrentPlayerAnyEatingMovePossibilityCheck()
        {
            bool playerCanMakeEatingMove;

            /// Set first to false so if "true" will be returned from a single SquareIndex check,
            /// we will go out from the loop via break.
            playerCanMakeEatingMove = false;
            foreach (SquareIndex currSquareIndex in m_CurrentPlayer.CurrentHoldingSquareIndices)
            {
                playerCanMakeEatingMove = r_MoveManager.AnyEatingMovePossibiltyCheckByIndex(currSquareIndex, m_Board, m_CurrentPlayer);
                if (playerCanMakeEatingMove)
                {
                    r_MoveManager.OnlyEatingIsValid = true;
                    break;
                }
            }

            if (playerCanMakeEatingMove)
            {
                r_MoveManager.OnlyEatingIsValid = true;
            }

            else
            {
                r_MoveManager.OnlyEatingIsValid = false;
            }

            return playerCanMakeEatingMove;
        }

        public void ScoreCalculationAndUpdate()
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
        }

        public void SaveFinalCheckersGameResult()
        {
            if (m_FirstPlayer.Score > m_SecondPlayer.Score)
            {
                m_FinalCheckersSessionResult = eGameResult.FirstPlayerWon;
            }

            else if (m_SecondPlayer.Score > m_FirstPlayer.Score)
            {
                m_FinalCheckersSessionResult = eGameResult.SecondPlayerWon;
            }

            else 
            {
                m_FinalCheckersSessionResult = eGameResult.Draw;
            }
        }

        /*private void UpdatePlayerPotentialMovesLists(Player io_Player)
        {
            /// Run on board Matrix,
            /// Each square that in player's holding check 4 SimpleMoves Options, 4 EatingMovesOption
            /// Using Propeties Clear both Lists
            foreach (Square sqr in m_Board.GameBoard)
            {
                if (sqr.SquareHolder == io_Player.PlayerRecognition)
                {
                    AddAllEatingPotentialMovesToThisSrcIdx(io_Player, sqr);
                    AddAllSimplePotentialMovesToThisSrcIdx(io_Player, sqr);
                }
            }
        }

        private void AddAllEatingPotentialMovesToThisSrcIdx(Player io_Player, Square i_Sqr)
        {
           *//* PotentialMove newPotentialMove(i_Sqr.); /// = new PotentialMove();

            newPotentialMove.SrcIdx = i_Sqr.SquareIndex;

            newPotentialMove.DestIdx.RowIdx = i_Sqr.SquareIndex.RowIdx - 1;
            newPotentialMove.DestIdx.ColumnIdx = i_Sqr.SquareIndex.ColumnIdx - 1;*//*
        }

        private void AddAllSimplePotentialMovesToThisSrcIdx(Player io_Player, Square i_Sqr)
        {

        }*/
    }

}

