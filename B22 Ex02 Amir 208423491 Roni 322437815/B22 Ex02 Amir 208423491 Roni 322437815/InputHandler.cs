using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B22_Ex02_Amir_208423491_Roni_322437815
{
    class InputHandler
    {
        StringBuilder m_RawInput;
        bool m_IsInputValid;
        int[] m_FromIndicies;
        int[] m_ToIndicies;

        /*Square m_FromSquare;
        Square m_ToSquare;*/

        public InputHandler()
        {
            m_FromIndicies = new int[2];
            m_ToIndicies = new int[2];
            m_IsInputValid = false;
        }

        public StringBuilder RawInput
        {
            get { return RawInput; }
            set { m_RawInput = value; }
        }

        public bool GetIsInputValid()
        {
            return m_IsInputValid;
        }

        public bool InputValidation()
        {

            return m_IsInputValid;
        }


        public int LetterToNumberIndexConverter(char i_Letter)
        {
            int index;

            // Explanation: 'A' ascii value is 65. So minus 65 will give us index = 0.
            index = Char.ToUpper(i_Letter) - 65;

            return index;

        }
    }
}
