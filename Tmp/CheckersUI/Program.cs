using System;
using System.Text;

namespace CheckersUI
{
    // $G$ SFN-007 (-14) Invalid input is not handled as required. -> accepts names with spaces, and empty names.
    // $G$ CSS-999 (-10) Standards are not kept as required - i can see clearly that you are not using stylecop. read the coding standards and use StyleCop.
    // there are mistakes all over your code.
    // there is no good use of access modifiers in the methods.
    // The class must have an access modifier.
    public class Program
    {
        public static void Main()
        {
            GameManager gameConsoleUI = new GameManager();

            gameConsoleUI.Run();
        }
    }
}
