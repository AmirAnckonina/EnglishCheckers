using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B22_Ex02_Amir_208423491_Roni_322437815
{
    public class TheCheckersGame
    {
        private Board m_Board;
        private GameMenu m_GameMenu;
        private Player[] m_Players;
        private eGameMode m_GameMode;

        public void RunSession()
        {
            InitSingleGame();
            RunSingleGame();
        }

        public void InitSingleGame()
        {
            int gameModePick = 0;

            //GameMenu -> run Welcome function
            //GameMenu -> ask for first player's name
            m_Players[0].Name.Append("name");
            //GameMenu -> ask for boardDimensions
            // m_Board.BoardSize = returnedValue from Menu
            m_Board.InitializeBoard();
            //gameModePick = //GameMenu -> ask whether to play against computer or another player
            SetGameMode(gameModePick);
            SetSecondPlayerProcedure(gameModePick);

            m_Players[0].DiscType = 'X';
            m_Players[1].DiscType = 'O';
            m_Players[0].NumOfDiscs = m_Board.GetDiscOccurences(m_Players[0].DiscType);
            m_Players[1].NumOfDiscs = m_Board.GetDiscOccurences(m_Players[1].DiscType);

        }

        public void SetGameMode(int i_GameModePick)
        { 

            if (i_GameModePick == 1)
            {
                m_GameMode = eGameMode.SinglePlayerMode;
            }

            else //i_GameModePick == 2 
            {
                m_GameMode = eGameMode.TwoPlayersMode;
            }
        }

        public void SetSecondPlayerProcedure(int i_GameModePick)
        { 
            if (i_GameModePick == 1)
            {
                m_Players[1].PlayerType = ePlayerType.Computer;
            }

            else //numOfPlayers == 2 // -> if so, ask for the second player name
            {
                m_Players[1].PlayerType = ePlayerType.Human;
                //GameMenu -> ask for the second player name
                m_Players[1].Name.Append("name2");
            }
        }

        public void RunSingleGame()
        {

            InputHandler input;

            if (m_GameMode == eGameMode.SinglePlayerMode)
            {
                //input.GetFromColIndex();
                
                //m_Board.
                //
            }

            else //if (m_GameMode == eGameMode.TwoPlayersMode)
            {

            }

        }

        //public static bool IsDiscEaten()
        //{

        //}

        //public static bool GameOver()
        //{

        //}

    }

}
