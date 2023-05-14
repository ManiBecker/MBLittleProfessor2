/****************************************************************************\

 Projekt : MBLittleProfessor
 Sprache : C#
 Compiler: MS Visual Studio 2008
 Autor   : Manfred Becker
 E-Mail  : mani.becker@web.de
 Url     : https://github.com/ManiBecker/MBLittleProfessor
 Modul   : MBLittleProfessor2.cs
 Version : 1.00
 Datum   : 15.05.2023
 Änderung: 15.05.2023

\****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBLittleProfessor2
{
    class MBLittleProfessor2
    {
        public enum MathOperation { ADD, SUB, MUL, DIV }; // Deklaration des Operationen +,-,*,/
        public const int MinLevel = 1; // Deklaration des minimalen Level Wert
        public const int MaxLevel = 5; // Deklaration des maximalen Level Wert
        public const int MaxNumberOfCalculations = 5; // Deklaration der Anzahl an Berechnungen
        public const int MaxNumberOfTrials = 2; // Deklaration der Anzahl an Fehlveruchen
        private string[] grading = new string[] { "sehr gut!", "gut", "befriedigend", "ausreichend", "mangelhaft", "ungenügend" };

        // Private Variable
        private int nLevel;
        private MathOperation eOP;
        private int nOP1, nOP2, nMinOP1, nMinOP2, nMaxOP1, nMaxOP2, nResult, nInput, nOP1X1, nOP1X1Input;
        private int nOP1old, nOP2old;
        private string sExercise, sInput, sHint, sRating, sIcon;
        private int NumberOfCalculations, NumberOfCorrectCalculations, NumberOfFalseCalculations, NumberOfTrials;
        private int nNextCalculationTicker, nSameCalculationTicker, nShowCompleteResultTicker, nSolutionNoteTicker, n1x1CalculationTicker;
        private int nRatingIndex = 4, nRatingDirection = -1;

        // Konstruktor
        public MBLittleProfessor2()
        {
            Start();
        }
        
        // Getter-Methoden
        public string GetLevel() { return nLevel.ToString(); }
        public int GetOP1() { return nOP1; }
        public int GetOP2() { return nOP2; }
        public string GetResult() { return nResult.ToString(); }
        public string GetHint() { return sHint; }
        public string GetRating() { return sRating; }
        public string GetIcon() { return sIcon; }
        public int GetNumberOfCalculations() { return NumberOfCalculations; }
        public int GetNumberOfCorrectCalculations() { return NumberOfCorrectCalculations; }
        public int GetNumberOfFalseCalculations() { return NumberOfFalseCalculations; }
        public int GetNumberOfTrials() { return NumberOfTrials; }
        public int GetNextCalculationTicker() { return nNextCalculationTicker; }
        public void DecNextCalculationTicker()
        { 
            nNextCalculationTicker--;

            if(nNextCalculationTicker > 0)
            {
                if (sHint == "Correct!")
                {
                    if (sIcon == "J")
                        sIcon = "K"; // :|
                    else
                        sIcon = "J"; // :)
                }
            }
        }
        public int GetSameCalculationTicker() { return nSameCalculationTicker; }
        public void DecSameCalculationTicker()
        { 
            nSameCalculationTicker--;
            
            if (nSameCalculationTicker > 0)
            {
                if (sHint == "Error!")
                {
                    if (sIcon == "L")
                        sIcon = "K"; // :|
                    else
                        sIcon = "L"; // :(
                }
            }
        }
        public int GetShowCompleteResultTicker() { return nShowCompleteResultTicker; }
        public void DecShowCompleteResultTicker()
        { 
            nShowCompleteResultTicker--;

            if (nShowCompleteResultTicker > 0 && NumberOfFalseCalculations == 0)
            {
                sRating = "    *    ".Substring(nRatingIndex, 5);
                nRatingIndex = nRatingIndex + nRatingDirection;
                if (nRatingIndex == 0 || nRatingIndex == 4) nRatingDirection = nRatingDirection * -1;

                if (sIcon == "J")
                    sIcon = "K"; // :|
                else
                    sIcon = "J"; // :)
            }
        }
        public int GetSolutionNoteTicker() { return nSolutionNoteTicker; }
        public void DecSolutionNoteTicker() 
        { 
            nSolutionNoteTicker--;

            if (nSolutionNoteTicker > 0)
            {
                if (sHint == "Error!")
                {
                    if (sIcon == "L")
                        sIcon = "K"; // :|
                    else
                        sIcon = "L"; // :(
                }
            }
        }
        public int Get1x1CalculationTicker() { return n1x1CalculationTicker; }
        public string GetInput() { return sInput; }
        public string GetExercise() { return sExercise; }
        public string GetDisplay() { return sExercise + sInput; }

        public void Dec1x1CalculationTicker()
        {

            if (n1x1CalculationTicker > 0 && n1x1CalculationTicker <= 4)
            {
                n1x1CalculationTicker--; 
                if (n1x1CalculationTicker == 0) n1x1CalculationTicker = 4;
                eOP = (MathOperation)n1x1CalculationTicker - 1;


                sExercise = "   " + GetMathOperation() + "  =";
                sHint = "+,-,*,/ ?";
            }
            else if (n1x1CalculationTicker > 4 && n1x1CalculationTicker <= 40)
            {
                if (n1x1CalculationTicker == 40)
                {
                    Random rand = new Random();
                    nOP1X1 = rand.Next(1, nMaxOP2);
                    nOP1X1Input = -1;
                }
                n1x1CalculationTicker--;
                if (n1x1CalculationTicker == 4)
                {
                    n1x1CalculationTicker = 0;
                    ResetInput();
                }
            }
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


        // Setter-Methoden
        private void SetMathOperation(MathOperation op)
        {
            eOP = op;
        }

        // Spielstufe setzen, die beiden Operatoren ermitteln und das Ergebnis berechnen
        public void SetLevel(int level)
        {
            // Level darf nur zwischen 1 und 5 sein
            if (level < MinLevel)
                nLevel = MinLevel;
            else if (level > MaxLevel)
                nLevel = MaxLevel;
            else
                nLevel = level;

            // je nach Lavel die Operanden im Wert eingrenzen
            switch (nLevel)
            {
                case 1: nMinOP1 = 0; nMinOP2 = 0; nMaxOP1 = 10; nMaxOP2 = 10; if (eOP == MathOperation.DIV || nOP1X1 > 0) nMaxOP1 = 50; break;
                case 2: nMinOP1 = 1; nMinOP2 = 1; nMaxOP1 = 100; nMaxOP2 = 20; break;
                case 3: nMinOP1 = 10; nMinOP2 = 1; nMaxOP1 = 100; nMaxOP2 = 50; if (eOP == MathOperation.DIV) { nMaxOP1 = 200; nMaxOP2 = 20; } break;
                case 4: nMinOP1 = 10; nMinOP2 = 10; nMaxOP1 = 200; nMaxOP2 = 100; break;
                case 5: nMinOP1 = 100; nMinOP2 = 10; nMaxOP1 = 1000; nMaxOP2 = 100; break;
            }

            // Bei einer Division darf der zweite Operand nie 0 sein
            if (eOP == MathOperation.DIV && nMinOP2 == 0)
            {
                nMinOP2 = 1; //Division / 0 verhindern
            }

            do
            {
                Random rand = new Random();
                nOP1 = rand.Next(nMinOP1, nMaxOP1);
                nOP2 = rand.Next(nMinOP2, nMaxOP2);
                if (nOP1X1 > 0) nOP2 = nOP1X1;
                switch (eOP)
                {
                    case MathOperation.ADD:
                        nResult = nOP1 + nOP2;
                        break;
                    case MathOperation.SUB:
                        while (nOP1 < nOP2)
                        {
                            nOP1 = rand.Next(nMinOP1, nMaxOP1);
                            nOP2 = rand.Next(nMinOP2, nMaxOP2);
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
                            nOP1 = rand.Next(nMinOP1, nMaxOP1);
                            nOP2 = rand.Next(nMinOP2, nMaxOP2);
                            if (nOP1X1 > 0) nOP2 = nOP1X1;
                        }
                        nResult = nOP1 * nOP2;
                        break;
                    case MathOperation.DIV:
                        while (nOP1 % nOP2 > 0)
                        {
                            nOP1 = rand.Next(nMinOP1, nMaxOP1);
                        }
                        nResult = (int)(nOP1 / nOP2);
                        break;
                }
            } while (nOP1old == nOP1 && nOP2old == nOP2);
            
            nOP1old = nOP1; nOP2old = nOP2;

            ResetInput();
        }

        // Eingabe zurücksetzen und Aufgabenrechnung zusammensetzen
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
                sIcon = "?";
                sExercise = nOP1.ToString().PadLeft(3) + GetMathOperation() + nOP2.ToString().PadLeft(2) + "=";
            }

            sInput = "";
            nInput = -1;
        }
        
        // Spielstufe erhöhen
        public void NextLevel()
        {
            ResetCalculations();
            if (nLevel == MaxLevel)
                SetLevel(MinLevel);
            else
                SetLevel(nLevel + 1);
        }

        // Spielergebnis anzeigen
        private void ShowCompleteResult()
        {
            int nGrade = MaxNumberOfCalculations - NumberOfCorrectCalculations;

            sExercise = String.Format("+" + NumberOfCorrectCalculations.ToString()).PadLeft(6);
            sInput = "  -" + NumberOfFalseCalculations.ToString();

            sHint = grading[nGrade];

            if (nGrade == 0 || nGrade == 1)
                sIcon = "J"; // :)
            else if (nGrade == 2 || nGrade == 3)
                sIcon = "K"; // :|
            else
                sIcon = "L"; // :(

            nShowCompleteResultTicker = 40;
        }

        // Nächste Berechnung ermitteln
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

        // Berechnung wiederholen
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

        // Hinweis zur korrekten Lösung
        public void ShowSolutionNote()
        {
            sHint = "Solution";
            sIcon = "K"; // :|
            sInput = nResult.ToString().PadLeft(4);

            nNextCalculationTicker = 20;
        }

        // Nächste Addition
        public void NextAddition()
        {
            if (n1x1CalculationTicker > 0 && n1x1CalculationTicker <= 4)
            {
                n1x1CalculationTicker = 40;
            }
            ResetCalculations();
            SetMathOperation(MathOperation.ADD);
            SetLevel(nLevel);
        }

        // Nächste Subtraktion
        public void NextSubtraction()
        {
            if (n1x1CalculationTicker > 0 && n1x1CalculationTicker <= 4)
            {
                n1x1CalculationTicker = 40;
            }
            ResetCalculations();
            SetMathOperation(MathOperation.SUB);
            SetLevel(nLevel);
        }

        // Nächste Multiplikation
        public void NextMultiplication()
        {
            if (n1x1CalculationTicker > 0 && n1x1CalculationTicker <= 4)
            {
                n1x1CalculationTicker = 40;
            }
            ResetCalculations();
            SetMathOperation(MathOperation.MUL);
            SetLevel(nLevel);
        }

        // Nächste Division
        public void NextDivision()
        {
            if (n1x1CalculationTicker > 0 && n1x1CalculationTicker <= 4)
            {
                n1x1CalculationTicker = 40;
            }
            ResetCalculations();
            SetMathOperation(MathOperation.DIV);
            SetLevel(nLevel);
        }

        // Nächste 1x1 Aufgabe
        public void Next1x1()
        {
            ResetCalculations();
            n1x1CalculationTicker = 1;
        }

        // Zufällige Operation ermitteln
        private MathOperation SelectRandomMathOperation()
        {
            Random rand = new Random();
            MathOperation op = (MathOperation)rand.Next(4);
            return op;
        }

        // Zufällige Spielstufe ermitteln
        private int SelectRandomLevel()
        {
            Random rand = new Random();
            int level = rand.Next(1, 6);
            return level;
        }

        // Eingabe überprüfen
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
                    sIcon = "L"; // :(
                    NumberOfTrials++;
                    if (NumberOfTrials == MaxNumberOfTrials)
                    {
                        NumberOfCalculations++;
                        NumberOfFalseCalculations++;
                        sRating = sRating + "-";
                        nSolutionNoteTicker = 20;
                    }
                    else
                        nSameCalculationTicker = 20;

                    break;
                }
            }

            if (sInput == sResult)
            {
                nReturnValue = nInput;
                sHint = "Correct!";
                sIcon = "J"; // :)
                NumberOfCalculations++;
                NumberOfCorrectCalculations++;
                NumberOfTrials = 0;
                sRating = sRating + "*";
                nNextCalculationTicker = 20;
            }

            sInput = sInput.PadLeft(4);

            return nReturnValue;
        }

        // Berechnungen zurücksetzen
        public void ResetCalculations()
        {
            NumberOfCalculations = 0;
            NumberOfCorrectCalculations = 0;
            NumberOfFalseCalculations = 0;
            NumberOfTrials = 0;
            sRating = "";
        }

        // Start bze. Reset des Spiels
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
    }
}
