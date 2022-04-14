﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B22_Ex02_Amir_208423491_Roni_322437815
{
    public class InputHandler
    {
        StringBuilder m_RawInput;
        bool m_InputStructureIsValid;
        int[] m_FromIndex;
        int[] m_ToIndex;

        /*Square m_FromSquare;
        Square m_ToSquare;*/

        public InputHandler()
        {
            m_RawInput = new StringBuilder();
            m_FromIndex = new int[2];
            m_ToIndex = new int[2];
            m_InputStructureIsValid = false;
        }

        public StringBuilder RawInput
        {
            get { return RawInput; }
            set { m_RawInput = value; }
        }

        public bool InputStructureIsValid()
        {
            return m_InputStructureIsValid;
        }

        public void LoadNewInput(StringBuilder i_RawInput)
        {
            ClearPreviousInput();
            m_RawInput = i_RawInput;
            InputStructureValidation();
            if (m_InputStructureIsValid)
            {
                UpdateIndicies();
            }
        }

        public void ClearPreviousInput()
        {
            m_RawInput.Clear();
            m_FromIndex.Initialize();
            m_ToIndex.Initialize();
            m_InputStructureIsValid = false;
        }

        public void UpdateIndicies()
        {
            m_FromIndex[0] = LetterToNumberIndexConverter(m_RawInput[0]);
            m_FromIndex[1] = LetterToNumberIndexConverter(m_RawInput[1]);
            m_ToIndex[0] = LetterToNumberIndexConverter(m_RawInput[3]);
            m_ToIndex[1] = LetterToNumberIndexConverter(m_RawInput[4]);
        }

        public void InputStructureValidation()
        {
            //CARFUL: validate if the first function returning false then the others won't be called.
            if (LengthValidation() && MovingFromValidation() && MovingToValidation() && OperatorValidation())
            {
                m_InputStructureIsValid = true;
            }

            else
            {
                m_InputStructureIsValid = false;
            }
        }

        public bool MovingFromValidation()
        {
            bool movingFromIndexIsValid;
            
            if (Char.IsUpper(m_RawInput[0]) && Char.IsLower(m_RawInput[1]))
            {
                movingFromIndexIsValid = true;
            }

            else
            {
                movingFromIndexIsValid = false;
            }

            return movingFromIndexIsValid;
        }

        public bool MovingToValidation()
        {
            bool movingToIndexIsValid;

            if (Char.IsUpper(m_RawInput[3]) && Char.IsLower(m_RawInput[4]))
            {
                movingToIndexIsValid = true;
            }

            else
            {
                movingToIndexIsValid = false;
            }

            return movingToIndexIsValid;
        }

        public bool OperatorValidation()
        {
            bool operatorIsValid;

            if (m_RawInput[2] == '>')
            {
                operatorIsValid = true;
            }

            else
            {
                operatorIsValid = false;
            }

            return operatorIsValid;
        }

        public bool LengthValidation()
        {
            bool lengthIsValid;

            if (m_RawInput.Length == 5)
            {
                lengthIsValid = true;
            }

            else
            {
                lengthIsValid = false;
            }

            return lengthIsValid;
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
