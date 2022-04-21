﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using CheckersGame;

namespace CheckersUI
{
    public class ConsoleInputManager
    {
        StringBuilder m_RawInput;
        bool m_InputStructureIsValid;
        SquareIndex m_SourceIndex;
        SquareIndex m_DestinationIndex;

        /* int[] m_SourceIndex; //Structure - > [ rowIndex, ColIndex ] // Af>Bi
         int[] m_DestinationIndex;*/
        /*Square m_FromSquare;
        Square m_ToSquare;*/

        public ConsoleInputManager()
        {
            m_RawInput = new StringBuilder();
            m_SourceIndex = new SquareIndex();
            m_DestinationIndex = new SquareIndex();
            m_InputStructureIsValid = false;
        }

        public StringBuilder RawInput
        {
            get
            {
                return RawInput; 
            }

            set 
            {
                m_RawInput = value; 
            }
        }

        public SquareIndex SrcIndex
        {
            get
            {
                return m_SourceIndex;
            }
        }

        public SquareIndex DestIndex
        {
            get 
            {
                return m_DestinationIndex; 
            }
        }

        public bool InputStructureIsValid
        {
            get
            {
                return m_InputStructureIsValid; 
            }
        }

        public void LoadNewInput()
        {
            ClearPreviousInput();
            m_RawInput.Append(Console.ReadLine());
            InputStructureValidation();
            if (m_InputStructureIsValid)
            {
                UpdateIndices();
            }
        }

        public void ClearPreviousInput()
        {
            m_RawInput.Clear();
            //Init indices
            m_InputStructureIsValid = false;
        }

        public void UpdateIndices()
        {
            m_SourceIndex.ColumnIndex = LetterToNumberIndexConverter(m_RawInput[0]);
            m_SourceIndex.RowIndex = LetterToNumberIndexConverter(m_RawInput[1]);
            m_DestinationIndex.ColumnIndex = LetterToNumberIndexConverter(m_RawInput[3]);
            m_DestinationIndex.RowIndex = LetterToNumberIndexConverter(m_RawInput[4]);
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
