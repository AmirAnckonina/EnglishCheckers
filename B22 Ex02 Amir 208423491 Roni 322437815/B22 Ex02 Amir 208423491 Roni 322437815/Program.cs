using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// <====================================================>
//
// Tasks:

// Roni -> MoveManager
// 1. Re-Design for MoveValidation inside MoveManager Class
// 2. Fix all the basic validation methods
// 3. Start builiding Moving without and with eating



// Amir -> Player & MoveManagerCopy 
// 1. Build MoveFromOptionPossibilityValidation
// 2. Re-organized TheCheckersGame runGame flow
// 3 Re-Build GameOver main method.
// 4. Add E-Num of PlayerRecognition - VVVVVVVV
// 5. Add this E-num in Square as a "squareHolder" - VVVVVVVVVV
// 6. Handle constructor in Square and Player. ---- Important.
// 7. Check about set Square using indices

// Others:
// Fix all ref occurences to regular paramaetr (i.e. ref Board.....)
// Add Quitting option (input handler)
// Build random choose for computer player
// AI -> smart moves by computer
// DoubleTurn option (after eating)
// Conversion regular disc to king

//GameOver ideas
// - Check if one of the disc type is 0 (NumOfDiscs == 0)
// - Check if there any valid moves for opponent player
// - Check if Quitting option choosed
// 

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
