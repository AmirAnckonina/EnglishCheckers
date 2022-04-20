﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersUI
{
    public class ConsoleUI
    {
        private ConsoleInputManager m_Input;

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
            System.Threading.Thread.Sleep(3000);
            // Add rules? Game description?
        }

        public StringBuilder GetPlayerName()
        {
            StringBuilder m_Name = new StringBuilder();

            Console.WriteLine("Please enter your Name: ");
            m_Name.Append(System.Console.ReadLine());

            return m_Name;
        }

        public int GetSizeOfBoard()
        {
            int m_BoardSize;

            Console.WriteLine("Please enter the size of the game board");
            m_BoardSize = int.Parse(Console.ReadLine());

            while (!IsBoardSizeValid(m_BoardSize)) 
            {
                Console.WriteLine("The input is not valid");
                Console.WriteLine("Please enter the size of the game board");
                m_BoardSize = int.Parse(Console.ReadLine());
            }
            return m_BoardSize;
        }

        public int GetGameMode()
        {
            int numOfPlayers;

            PrintGameModeChoosingRequest();
            numOfPlayers = int.Parse(Console.ReadLine());

            while (numOfPlayers != 1 && numOfPlayers != 2)
            {
                Console.WriteLine("Invalid choice!");
                PrintGameModeChoosingRequest();
                numOfPlayers = int.Parse(Console.ReadLine());
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
            Console.WriteLine("It's {0} turn, Go Ahead! : ", i_CurrPlayer.Name);
        }

        public void PrintInvalidInputStructure()
        {
            Console.WriteLine("Sorry, your input structure isn't valid.");
            Console.Write("Please enter a new move: ");
        }

        public void PrintInvalidInputMoveOption()
        {
            Console.WriteLine("Sorry, your move choice isn't valid!");
            Console.Write("Please enter a new move: ");
        }

        public bool IsBoardSizeValid(int i_BoardSize)
        {
            bool isBoardSizeValid;

            if (i_BoardSize == 6 || i_BoardSize == 8 || i_BoardSize == 10) 
            {
                isBoardSizeValid = true;
            }

            else
            {
                isBoardSizeValid = false;
            }

            return isBoardSizeValid;
        }

        //public void PrintBoard(ref )
        //{
        //    int row, column, index;
        //    char letter = 'A', currDiscChar;
        //    eDiscType currDiscType;

        //    for (row = 0; row < m_BoardSize; row++)
        //    {
        //        Console.Write("   {0} ", letter);
        //        letter = (char)(letter + 1);
        //    }

        //    Console.WriteLine("");
        //    letter = 'a';
        //    for (row = 0; row < m_BoardSize; row++)
        //    {
        //        Console.Write("{0}|", letter);
        //        letter = (char)(letter + 1);
        //        for (column = 0; column < m_BoardSize; column++)
        //        {
        //            currDiscChar = GetCharByDiscType(m_GameBoard[row, column].DiscType);
        //            currDiscType = m_GameBoard[row, column].DiscType;
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
        //        for (index = 0; index < m_BoardSize; index++)
        //        {
        //            Console.Write("=====");
        //        }

        //        Console.WriteLine(" ");
        //    }
        //}

    }
}