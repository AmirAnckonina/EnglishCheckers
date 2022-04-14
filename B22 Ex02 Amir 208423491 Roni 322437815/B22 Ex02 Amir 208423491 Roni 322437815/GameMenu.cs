using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B22_Ex02_Amir_208423491_Roni_322437815
{
    public class GameMenu
    {
        public StringBuilder GetPlayerName()
        {
            StringBuilder m_Name = new StringBuilder();

            Console.WriteLine("Please enter your Name");
            m_Name.Append(System.Console.ReadLine());
            return m_Name;
        }

        public int GetSizeOfBoard()
        {
            int m_BoardSize;

            Console.WriteLine("Please enter the size of the game board");
            m_BoardSize= int.Parse(Console.ReadLine());
            while(m_BoardSize != 6 && m_BoardSize != 8 && m_BoardSize != 10)
            {
                Console.WriteLine("The input is not valid");
                Console.WriteLine("Please enter the size of the game board");
                m_BoardSize = int.Parse(Console.ReadLine());
            }
            return m_BoardSize;
        }

        public int GetGameMode()
        {
            int m_NumOfPlayer;

            Console.WriteLine("Please enter the game mode:/n 1-one player (play vs computer)/n 2-tow player (play vs human)");
            m_NumOfPlayer = int.Parse(Console.ReadLine());
            while(m_NumOfPlayer != 1 && m_NumOfPlayer != 2)
            {
                Console.WriteLine("The input is not valid");
                Console.WriteLine("Please enter the game mode:/n 1-one player (play vs computer)/n 2-tow player (play vs human)");
                m_NumOfPlayer = int.Parse(Console.ReadLine());
            }
            return m_NumOfPlayer;
        }

        public void PrintWhoseTurn(Player i_Player)
        {
            Console.WriteLine("It's {0} turn:", i_Player.Name);
        }
    }
}
