using System;
using System.Text;
using CheckersGame;

namespace CheckersUI
{
    public class RawMoveInputManager
    {
        private StringBuilder m_RawInput;
        private bool m_RawInputIsValid;
        private bool m_QuitInserted;
        private SquareIndex r_SourceIndex;
        private SquareIndex r_DestinationIndex;
        private const char k_DirectionSign = '>';

        public RawMoveInputManager()
        {
            m_RawInput = new StringBuilder();
            m_RawInput.Append("     ");
            r_SourceIndex = new SquareIndex();
            r_DestinationIndex = new SquareIndex();
            m_RawInputIsValid = false;
            m_QuitInserted = false;
        }

        public StringBuilder RawInput
        {
            get
            {
                return m_RawInput; 
            }

            set 
            {
                m_RawInput = value; 
            }
        }

        public SquareIndex SourceIndex
        {
            get
            {
                return r_SourceIndex;
            }
        }

        public SquareIndex DestinationIndex
        {
            get 
            {
                return r_DestinationIndex; 
            }
        }

        public bool RawInputIsValid
        {
            get
            {
                return m_RawInputIsValid; 
            }
        }

        public bool QuitInserted
        {
            get
            {
                return m_QuitInserted;
            }

            set
            {
                m_QuitInserted = value;
            }
        }

        public void LoadNewInput()
        {
            ClearPreviousInput();
            m_RawInput.Append(Console.ReadLine());
            RawInputValidation();
            if (m_RawInputIsValid && !m_QuitInserted) 
            {
                UpdateIndices();
            }
        }

        public void ClearPreviousInput()
        {
            m_RawInput.Remove(0, m_RawInput.Length);
            m_RawInputIsValid = false;
        }

        public void UpdateIndices()
        {
            r_SourceIndex.ColumnIdx = LetterToIndexNumberConverter(m_RawInput[0]);
            r_SourceIndex.RowIdx = LetterToIndexNumberConverter(m_RawInput[1]);
            r_DestinationIndex.ColumnIdx = LetterToIndexNumberConverter(m_RawInput[3]);
            r_DestinationIndex.RowIdx = LetterToIndexNumberConverter(m_RawInput[4]);
        }

        public void RawInputValidation()
        {
            if (InputStructureValidation())
            {
                m_RawInputIsValid = true;
            }

            else
            {
                m_RawInputIsValid = false;
            }
        }

        public bool InputStructureValidation()
        {
            bool inputStructureIsValid;
            
            if (m_RawInput.Length == 1)
            {
                if (DoesQuitInserted())
                { 
                    inputStructureIsValid = true;
                }
                else
                {
                    inputStructureIsValid = false;
                }
            }

            else if (LengthValidation() && MovingFromValidation() && MovingToValidation() && OperatorValidation())
            {
                inputStructureIsValid = true;
            }

            else
            {
                inputStructureIsValid = false;
            }

            return inputStructureIsValid;
        }

        public bool DoesQuitInserted()
        {
            bool quitInserted;

            if (char.ToUpper(m_RawInput[0]) == IOManager.k_Quit)
            {
                quitInserted = m_QuitInserted = true;
            }

            else
            {
                quitInserted = m_QuitInserted = false;
            }

            return quitInserted;
        }

        public bool MovingFromValidation()
        {
            bool movingFromIndexIsValid;

            if (char.IsUpper(m_RawInput[0]) && char.IsLower(m_RawInput[1]))
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

            if (char.IsUpper(m_RawInput[3]) && char.IsLower(m_RawInput[4]))
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

            if (m_RawInput[2] == k_DirectionSign)
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

        public int LetterToIndexNumberConverter(char i_Letter)
        {
            int index;

            index = char.ToUpper(i_Letter) - 65;

            return index;
        }

        public char IndexNumberToUpperCaseLetterConverter(int i_Number)
        {
            char letter;

            letter = (char)(i_Number + 65);

            return letter;
        }

        public char IndexNumberToLowerCaseLetterConverter(int i_Number)
        {
            char letter;

            letter = (char)(i_Number + 97);

            return letter;
        }

        public void LoadLastMoveToRawInput(SquareIndex i_SourceIndex, SquareIndex i_DestinationIndex)
        {
            // also here,  Local variable name shouldn't start with i_.
            int i_SourceColumn = i_SourceIndex.ColumnIdx;
            int i_SourceRow = i_SourceIndex.RowIdx;
            int i_DestColumn = i_DestinationIndex.ColumnIdx;
            int i_DestRow = i_DestinationIndex.RowIdx;

            m_RawInput[0] = IndexNumberToUpperCaseLetterConverter(i_SourceColumn);
            m_RawInput[1] = IndexNumberToLowerCaseLetterConverter(i_SourceRow);
            m_RawInput[2] = k_DirectionSign;
            m_RawInput[3] = IndexNumberToUpperCaseLetterConverter(i_DestColumn);
            m_RawInput[4] = IndexNumberToLowerCaseLetterConverter(i_DestRow);
        }
    }
}
