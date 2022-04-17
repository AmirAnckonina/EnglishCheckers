using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// <====================================================>
// Tasks:
// 1. Build GameOver method (Roni)
// - Check if one of the disc type is 0 (NumOfDiscs == 0)
// - Check if there any valid moves for opponent player
// - Check if Quitting option choosed
// 
// Decide about the game winner
// FirstPlayer , SecondPlayer, Draw.

// Amir:
// 1. Add quiting option in InputHandler
// 2. Handle the GameFunction according to quitting option.

// Others:
// Conversion regular disc to king
// DoubleTurn check function
// executeMove
// <====================================================>
// <====================================================>


namespace B22_Ex02_Amir_208423491_Roni_322437815
{
    public class Program
    {
        public static void Main()
        {
            int res;
            bool Res;
            Board board1 = new Board(8);
            board1.InitializeBoard();
            board1.PrintBoard();
            res= board1.GetDiscOccurences(eDiscType.XDisc);
            Console.WriteLine("number of x: {0}", res);
            Res= board1.SquareExistenceValidation(2, 8);
            Console.WriteLine("is index valid? : {0}", Res);
            
        }
    }
}
