using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B22_Ex02_Amir_208423491_Roni_322437815
{
    public class GameMenu
    {
        public StringBuilder GetName()
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
            return m_BoardSize;
        }

        public int GetPlayerType()
        {
            Console.WriteLine("Please choose the type of the player that you will play against");
        }
    }
}
