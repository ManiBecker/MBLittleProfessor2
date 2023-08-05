/****************************************************************************\

 Projekt : MBLittleProfessor
 Sprache : C#
 Compiler: MS Visual Studio 2008
 Autor   : Manfred Becker
 E-Mail  : mani.becker@web.de
 Url     : https://github.com/ManiBecker/MBLittleProfessor2
 Modul   : MBLittleProfessor2.cs
 Version : 1.00
 Datum   : 15.05.2023
 Änderung: 05.08.2023, 25.05.2023

\****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBLittleProfessor2
{
    class MBLittleProfessor2
    {
        #region PublicClassMembers
        public enum MathOperation { ADD, SUB, MUL, DIV }; // Declaration of the operations +,-,*,/
        public const int MinLevel = 1; // Declaration of the minimum level value
        public const int MaxLevel = 5; // Declaration of the maximum level value
        public const int MaxNumberOfCalculations = 5; // Declaration of the number of calculations
        public const int MaxNumberOfTrials = 2; // Declaration of the number of failed tests
        #endregion // PublicClassMembers

        #region PrivateClassMembers
        private string[] grading = new string[] { "very good!", "good", "satisfactory", "sufficient", "poor", "insufficient" };
        private int nLevel;
        private MathOperation eOP;
        private int nOP1, nOP2, nMinOP1, nMinOP2, nMaxOP1, nMaxOP2, nResult, nInput, nOP1X1, nOP1X1Input;
        private int nOP1old, nOP2old;
        private string sExercise, sInput, sHint, sRating, sIcon;
        private int NumberOfCalculations, NumberOfCorrectCalculations, NumberOfFalseCalculations, NumberOfTrials;
        private int nNextCalculationTicker, nSameCalculationTicker, nShowCompleteResultTicker, nSolutionNoteTicker, n1x1CalculationTicker;
        private int nShowCompleteResultTickerDivider, n1x1CalculationTickerDivider;
        private int nRatingIndex = 4, nRatingDirection = -1;
        #endregion // PrivateClassMembers

        #region Constructor
        public MBLittleProfessor2()
        {
            Start();
        }
        #endregion // Constructor

        #region GetterMethods
        public string GetLevel()
        {
            if (sHint == "Error!")
                return "";
            else
                return ("    " + nLevel.ToString()).Substring(5-nLevel,nLevel); 
        }
        public int GetOP1() { return nOP1; }
        public int GetOP2() { return nOP2; }
        public string GetResult() { return nResult.ToString(); }
        public string GetHint() { return sHint; }
        public string GetRating()
        {
            if (sHint == "Error!")
                return "";
            else
                return sRating; 
        }
        public string GetIcon() { return sIcon; }
        public int GetNumberOfCalculations() { return NumberOfCalculations; }
        public int GetNumberOfCorrectCalculations() { return NumberOfCorrectCalculations; }
        public int GetNumberOfFalseCalculations() { return NumberOfFalseCalculations; }
        public int GetNumberOfTrials() { return NumberOfTrials; }
        public int GetNextCalculationTicker() { return nNextCalculationTicker; }
        public int GetSameCalculationTicker() { return nSameCalculationTicker; }
        public int GetShowCompleteResultTicker() { return nShowCompleteResultTicker; }
        public int GetSolutionNoteTicker() { return nSolutionNoteTicker; }
        public int Get1x1CalculationTicker() { return n1x1CalculationTicker; }
        public string GetInput() { return sInput; }
        public string GetExercise() { return sExercise; }
        public string GetDisplay()
        {
            if (sHint == "Error!")
                return "       Err";
            else
                return sExercise + sInput; 
        }

        public string GetMathOperation()
        {
            string sResult = "";

            switch (eOP)
            {
                case MathOperation.ADD:
                    sResult = "+";
                    break;
                case MathOperation.SUB:
                    sResult = "-";
                    break;
                case MathOperation.MUL:
                    sResult = "x";
                    break;
                case MathOperation.DIV:
                    sResult = "/";
                    break;
            }

            return sResult;
        }
        #endregion // GetterMethods

        #region DecreaseTickerMethods
        public void DecNextCalculationTicker()
        {
            nNextCalculationTicker--;

            if (nNextCalculationTicker > 0)
            {
                if (sHint == "Correct!")
                {
                    if (sIcon == "1")
                        sIcon = "2";
                    else if (sIcon == "2")
                        sIcon = "3";
                    else if (sIcon == "3")
                        sIcon = "4";
                    else
                        sIcon = "1";
                }
            }
        }

        public void DecSameCalculationTicker()
        {
            nSameCalculationTicker--;

            if (nSameCalculationTicker > 0)
            {
                if (sHint == "Error!")
                {
                    sIcon = "0";
                }
            }
        }

        public void DecShowCompleteResultTicker()
        {
            nShowCompleteResultTicker--;

            if (NumberOfFalseCalculations == 0)
            {
                if (nShowCompleteResultTicker > 300)
                {
                    if (sIcon == "1")
                        sIcon = "2";
                    else if (sIcon == "2")
                        sIcon = "3";
                    else if (sIcon == "3")
                        sIcon = "4";
                    else
                        sIcon = "1";

                    nShowCompleteResultTickerDivider++;
                    if (nShowCompleteResultTickerDivider >= 6)
                    {
                        nShowCompleteResultTickerDivider = 0;
                        sRating = "    *    ".Substring(nRatingIndex, 5);
                        nRatingIndex = nRatingIndex + nRatingDirection;
                        if (nRatingIndex == 0 || nRatingIndex == 4) nRatingDirection = nRatingDirection * -1;
                    }
                }
                else if (nShowCompleteResultTicker > 0)
                {
                    sIcon = "1";
                    sRating = "*****";
                }
            }
        }

        public void DecSolutionNoteTicker()
        {
            nSolutionNoteTicker--;

            if (nSolutionNoteTicker > 0)
            {
                if (sHint == "Error!")
                {
                    sIcon = "0";
                }
            }
        }

        public void Dec1x1CalculationTicker()
        {

            if (n1x1CalculationTicker > 0 && n1x1CalculationTicker <= 4)
            {
                n1x1CalculationTickerDivider++;
                if (n1x1CalculationTickerDivider >= 40)
                {
                    n1x1CalculationTickerDivider = 0;
                    n1x1CalculationTicker--;
                }
                if (n1x1CalculationTicker == 0) n1x1CalculationTicker = 4;
                eOP = (MathOperation)n1x1CalculationTicker - 1;


                sExercise = "   " + GetMathOperation() + "  =";
                sHint = "+,-,*,/ ?";
            }
            else if (n1x1CalculationTicker > 4 && n1x1CalculationTicker <= 300)
            {

                if (n1x1CalculationTicker == 300)
                {
                    Random rand = new Random();
                    nOP1X1 = rand.Next(1, nMaxOP2);
                    nOP2 = nOP1X1;
                    nOP1X1Input = -1;
                    SetLevel(nLevel);
                }

                n1x1CalculationTicker--;
                if (n1x1CalculationTicker == 4)
                {
                    n1x1CalculationTicker = 0;
                    ResetInput();
                }
            }
        }
        #endregion // DecreaseTickerMethods

        #region SetterMethods
        private void SetMathOperation(MathOperation op)
        {
            eOP = op;
        }

        // Set the game level, determine the two operators and calculate the result
        public void SetLevel(int level)
        {
            // Level may only be between 1 and 5
            if (level < MinLevel)
                nLevel = MinLevel;
            else if (level > MaxLevel)
                nLevel = MaxLevel;
            else
                nLevel = level;

            // depending on the operation and lavel, limit the operands in value
            switch (eOP)
            {
                case MathOperation.ADD:
                    switch (nLevel)
                    {
                        case 1: nMinOP1 = 0; nMinOP2 = 0; nMaxOP1 = 9; nMaxOP2 = 9; break;
                        case 2: nMinOP1 = 1; nMinOP2 = 1; nMaxOP1 = 49; nMaxOP2 = 49; break;
                        case 3: nMinOP1 = 5; nMinOP2 = 5; nMaxOP1 = 99; nMaxOP2 = 99; break;
                        case 4: nMinOP1 = 10; nMinOP2 = 10; nMaxOP1 = 199; nMaxOP2 = 99; break;
                        case 5: nMinOP1 = 200; nMinOP2 = 10; nMaxOP1 = 999; nMaxOP2 = 99; break;
                    }
                    break;
                case MathOperation.SUB:
                    switch (nLevel)
                    {
                        case 1: nMinOP1 = 1; nMinOP2 = 0; nMaxOP1 = 19; nMaxOP2 = 9; break;
                        case 2: nMinOP1 = 10; nMinOP2 = 0; nMaxOP1 = 99; nMaxOP2 = 99; break;
                        case 3: nMinOP1 = 10; nMinOP2 = 0; nMaxOP1 = 99; nMaxOP2 = 99; break;
                        case 4: nMinOP1 = 100; nMinOP2 = 10; nMaxOP1 = 199; nMaxOP2 = 99; break;
                        case 5: nMinOP1 = 300; nMinOP2 = 10; nMaxOP1 = 999; nMaxOP2 = 99; break;
                    }
                    break;
                case MathOperation.MUL:
                    switch (nLevel)
                    {
                        case 1: nMinOP1 = 0; nMinOP2 = 0; nMaxOP1 = 9; nMaxOP2 = 9; break;
                        case 2: nMinOP1 = 1; nMinOP2 = 0; nMaxOP1 = 49; nMaxOP2 = 49; break;
                        case 3: nMinOP1 = 5; nMinOP2 = 0; nMaxOP1 = 99; nMaxOP2 = 99; break;
                        case 4: nMinOP1 = 10; nMinOP2 = 0; nMaxOP1 = 199; nMaxOP2 = 99; break;
                        case 5: nMinOP1 = 200; nMinOP2 = 10; nMaxOP1 = 999; nMaxOP2 = 99; break;
                    }
                    break;
                case MathOperation.DIV:
                    switch (nLevel)
                    {
                        case 1: nMinOP1 = 0; nMinOP2 = 1; nMaxOP1 = 99; nMaxOP2 = 9; break;
                        case 2: nMinOP1 = 0; nMinOP2 = 1; nMaxOP1 = 99; nMaxOP2 = 9; break;
                        case 3: nMinOP1 = 0; nMinOP2 = 1; nMaxOP1 = 199; nMaxOP2 = 9; break;
                        case 4: nMinOP1 = 0; nMinOP2 = 10; nMaxOP1 = 199; nMaxOP2 = 99; break;
                        case 5: nMinOP1 = 0; nMinOP2 = 10; nMaxOP1 = 999; nMaxOP2 = 99; break;
                    }
                    break;
            }

            // Create new random numbers and make sure that they are not identical to the previous random numbers
            do
            {
                Random rand = new Random();
                nOP1 = rand.Next(nMinOP1, nMaxOP1+1);
                nOP2 = rand.Next(nMinOP2, nMaxOP2+1);
                if (nOP1X1 > 0) nOP2 = nOP1X1;
                switch (eOP)
                {
                    case MathOperation.ADD:
                        nResult = nOP1 + nOP2;
                        break;
                    case MathOperation.SUB:
                        while (nOP1 < nOP2)
                        {
                            nOP1 = rand.Next(nMinOP1, nMaxOP1+1);
                            nOP2 = rand.Next(nMinOP2, nMaxOP2+1);
                            if (nOP1X1 > 0)
                            {
                                nOP2 = nOP1X1;
                            }
                        }
                        nResult = nOP1 - nOP2;
                        break;
                    case MathOperation.MUL:
                        while (nOP1 * nOP2 > 10000)
                        {
                            nOP1 = rand.Next(nMinOP1, nMaxOP1+1);
                            nOP2 = rand.Next(nMinOP2, nMaxOP2+1);
                            if (nOP1X1 > 0) nOP2 = nOP1X1;
                        }
                        nResult = nOP1 * nOP2;
                        break;
                    case MathOperation.DIV:
                        while (nOP1 % nOP2 > 0)
                        {
                            nOP1 = rand.Next(nMinOP1, nMaxOP1+1);
                        }
                        nResult = (int)(nOP1 / nOP2);
                        break;
                }
            } while (nOP1old == nOP1 && nOP2old == nOP2);
            
            nOP1old = nOP1; nOP2old = nOP2;

            ResetInput();
        }
        #endregion // SetterMethods

        #region GameFunctions
        // Start or reset the game
        public void Start()
        {
            eOP = MathOperation.ADD;
            n1x1CalculationTicker = 0;
            nOP1X1 = -1;
            nOP1X1Input = -1;
            nOP1old = nOP2old = -1;
            nLevel = 0;
            NextLevel();
        }

        // Increase game level
        public void NextLevel()
        {
            ResetCalculations();
            if (nLevel == MaxLevel)
                SetLevel(MinLevel);
            else
                SetLevel(nLevel + 1);
        }

        // Next 1x1 task
        public void Next1x1()
        {
            ResetCalculations();
            n1x1CalculationTicker = 1;
        }

        // Next addition
        public void NextAddition()
        {
            if (n1x1CalculationTicker > 0 && n1x1CalculationTicker <= 4)
            {
                n1x1CalculationTicker = 300;
            }
            ResetCalculations();
            SetMathOperation(MathOperation.ADD);
            SetLevel(nLevel);
        }

        // Next subtraction
        public void NextSubtraction()
        {
            if (n1x1CalculationTicker > 0 && n1x1CalculationTicker <= 4)
            {
                n1x1CalculationTicker = 300;
            }
            ResetCalculations();
            SetMathOperation(MathOperation.SUB);
            SetLevel(nLevel);
        }

        // Next multiplication
        public void NextMultiplication()
        {
            if (n1x1CalculationTicker > 0 && n1x1CalculationTicker <= 4)
            {
                n1x1CalculationTicker = 300;
            }
            ResetCalculations();
            SetMathOperation(MathOperation.MUL);
            SetLevel(nLevel);
        }

        // Next division
        public void NextDivision()
        {
            if (n1x1CalculationTicker > 0 && n1x1CalculationTicker <= 4)
            {
                n1x1CalculationTicker = 300;
            }
            ResetCalculations();
            SetMathOperation(MathOperation.DIV);
            SetLevel(nLevel);
        }

        // Set input value 0 to 9
        public int SetInput(int value)
        {
            int nReturnValue = -1;

            if (nNextCalculationTicker + nSameCalculationTicker + nShowCompleteResultTicker + nSolutionNoteTicker > 0)
            {
                return nReturnValue;
            }

            if (n1x1CalculationTicker > 0)
            {
                if (nOP1X1Input == -1 && value > 0)
                    nOP1X1Input = value;
                else if (nOP1X1Input > 0 && nOP1X1Input * 10 + value <= nMaxOP2)
                    nOP1X1Input = nOP1X1Input * 10 + value;
                else
                    return nReturnValue;

                if (nOP1X1Input != -1)
                {
                    nOP1X1 = nOP1X1Input;
                    SetLevel(nLevel);
                    nReturnValue = nOP1X1Input;
                }

                return nReturnValue;
            }

            string sResult = nResult.ToString();
            sInput = nInput.ToString();
            if (nInput == -1) sInput = "";
            sInput = sInput + value.ToString();

            for (int i = 0; i < sInput.Length; i++)
            {
                nInput = int.Parse(sInput.Substring(0, i + 1));
                if (sInput[i] != sResult[i])
                {
                    nReturnValue = -1;
                    sHint = "Error!";
                    sIcon = "0";
                    NumberOfTrials++;
                    if (NumberOfTrials == MaxNumberOfTrials)
                    {
                        NumberOfCalculations++;
                        NumberOfFalseCalculations++;
                        sRating = sRating + "-";
                        nSolutionNoteTicker = 50;
                    }
                    else
                        nSameCalculationTicker = 50;

                    break;
                }
            }

            if (sInput == sResult)
            {
                nReturnValue = nInput;
                sHint = "Correct!";
                sIcon = "1";
                NumberOfCalculations++;
                NumberOfCorrectCalculations++;
                NumberOfTrials = 0;
                sRating = sRating + "*";
                nNextCalculationTicker = 60;
            }

            sInput = sInput.PadLeft(4);

            return nReturnValue;
        }
        #endregion // GameFunctions

        #region GameHelperFunctions
        // Reset calculations
        public void ResetCalculations()
        {
            NumberOfCalculations = 0;
            NumberOfCorrectCalculations = 0;
            NumberOfFalseCalculations = 0;
            NumberOfTrials = 0;
            sRating = "";
        }

        // Determine next calculation
        public void NextCalculation()
        {
            if (NumberOfCalculations == MaxNumberOfCalculations)
            {
                ShowCompleteResult();
            }
            else
            {
                NumberOfTrials = 0;
                SetLevel(nLevel);
            }
        }

        // Repeat calculation
        public void SameCalculation()
        {
            if (NumberOfCalculations == MaxNumberOfCalculations)
            {
                ShowCompleteResult();
            }
            else
            {
                ResetInput();
            }
        }

        // Reset input and assemble task calculation
        public void ResetInput()
        {
            if (n1x1CalculationTicker > 0)
            {
                sHint = "OP2?";
                sExercise = "   " + GetMathOperation() + nOP2.ToString().PadLeft(2) + "=";
            }
            else
            {
                sHint = "Input?";
                sIcon = "1";
                sExercise = nOP1.ToString().PadLeft(3) + GetMathOperation() + nOP2.ToString().PadLeft(2) + "=";
            }

            sInput = "";
            nInput = -1;
        }

        // Detect random operation
        private MathOperation SelectRandomMathOperation()
        {
            Random rand = new Random();
            MathOperation op = (MathOperation)rand.Next(4);
            return op;
        }

        // Determine random game level
        private int SelectRandomLevel()
        {
            Random rand = new Random();
            int level = rand.Next(1, 6);
            return level;
        }

        // Show score
        private void ShowCompleteResult()
        {
            int nGrade = MaxNumberOfCalculations - NumberOfCorrectCalculations;

            sExercise = String.Format("+" + NumberOfCorrectCalculations.ToString()).PadLeft(6);
            sInput = "  -" + NumberOfFalseCalculations.ToString();

            sHint = grading[nGrade];

            if (NumberOfFalseCalculations == 0)
                nShowCompleteResultTicker = 600;
            else
                nShowCompleteResultTicker = 600;
        }

        // Note for the correct solution
        public void ShowSolutionNote()
        {
            sHint = "Solution";
            sIcon = "3";
            sInput = nResult.ToString().PadLeft(4);

            nNextCalculationTicker = 100;
        }
        #endregion // GameHelperFunctions
    }
}
