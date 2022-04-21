using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameManagement
{
    public class Program
    {
        public static void Main()
        {
            CheckersGameManager gameManager = new CheckersGameManager();

            gameManager.RunFullGameSessions();



            /*int res;
            bool Res;
            Board board1 = new Board(8);
            board1.InitializeBoard();
            board1.PrintBoard();
            res= board1.GetDiscOccurences(eDiscType.XDisc);
            Console.WriteLine("number of x: {0}", res);
            Res= board1.SquareExistenceValidation(2, 8);
            Console.WriteLine("is index valid? : {0}", Res);*/

        }
    }
}
