using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices; //CustomizeSystemMenu


namespace MBLittleProfessor2
{
    public partial class FormMain : Form
    {
        #region PrivateClassMembers
        /// <summary>
        /// Private class menbers
        /// </summary>
        private static MBLittleProfessor2 clLittleProfessor2;
        private static readonly string sPrgName = "MB Little Professor v2";
        private static readonly string sPrgDate = "(c)12.05.2023";
        private float fRatio, fDx, fDy;
        private int iMouseXPos, iMouseYPos;
        #endregion // PrivateClassMembers

        #region BUTTON-Definition
        enum BUTTON { zero, one, two, three, four, five, six, seven, eight, nine, start, level, rnd, add, sub, mul, div, none };
        private string[] BUTTONname = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "Start", "Level", "1x1", "+", "-", "*", "/" };
        private int[] BUTTONxp1 = { 156, 095, 156, 218, 095, 156, 218, 095, 156, 218, 035, 035, 035, 279, 279, 279, 279 };
        private int[] BUTTONyp1 = { 572, 523, 523, 523, 473, 473, 473, 423, 423, 423, 423, 472, 521, 550, 492, 432, 372 };
        private int[] BUTTONxp2 = { 204, 142, 204, 265, 142, 204, 265, 142, 204, 265, 081, 081, 081, 326, 326, 326, 326 };
        private int[] BUTTONyp2 = { 604, 553, 553, 553, 504, 504, 504, 455, 455, 455, 455, 504, 553, 593, 535, 475, 416 };
        #endregion //BUTTON-Definition

        #region CustomizeSystemMenu
        /// <summary>
        /// Customize the system menu
        /// https://stackoverflow.com/questions/4615940/how-can-i-customize-the-system-menu-of-a-windows-form
        /// </summary>

        // P/Invoke constants
        private const int WM_SYSCOMMAND = 0x112;
        private const int MF_STRING = 0x0;
        private const int MF_SEPARATOR = 0x800;

        // P/Invoke declarations
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool AppendMenu(IntPtr hMenu, int uFlags, int uIDNewItem, string lpNewItem);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool InsertMenu(IntPtr hMenu, int uPosition, int uFlags, int uIDNewItem, string lpNewItem);

        // ID for the About item on the system menu
        private int SYSMENU_HELP_ID = 0x1;
        private int SYSMENU_ABOUT_ID = 0x2;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // Get a handle to a copy of this form's system (window) menu
            IntPtr hSysMenu = GetSystemMenu(this.Handle, false);

            // Add a separator
            AppendMenu(hSysMenu, MF_SEPARATOR, 0, string.Empty);

            // Add the "Help..." menu item
            AppendMenu(hSysMenu, MF_STRING, SYSMENU_HELP_ID, "&Help…\t(F1)");

            // Add the "About..." menu item
            AppendMenu(hSysMenu, MF_STRING, SYSMENU_ABOUT_ID, "&About…\t(F2)");
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if ((m.Msg == WM_SYSCOMMAND) && ((int)m.WParam == SYSMENU_HELP_ID))
            {
                Help();
            }

            if ((m.Msg == WM_SYSCOMMAND) && ((int)m.WParam == SYSMENU_ABOUT_ID))
            {
                About();
            }
        }

        void Help()
        {
            MessageBox.Show(HelpMessage(), "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        string HelpMessage()
        {
            string sMessage = String.Format("Mani's {0} {1}{2}{2}", sPrgName, sPrgDate, Environment.NewLine);
            sMessage += String.Format("Please use one of these F-keys:{0}", Environment.NewLine);
            sMessage += String.Format("F1: Shows this Help-Page{0}", Environment.NewLine);
            sMessage += String.Format("F2: Shows the About-Page{0}", Environment.NewLine);
            sMessage += String.Format("ESC: Exit the {0}{1}{1}", sPrgName, Environment.NewLine);
            return sMessage;
        }

        void About()
        {
            string sMessage = String.Format("Mani's {0} {1}{2}{2}", sPrgName, sPrgDate, Environment.NewLine);
            sMessage += String.Format("A TI Little Professor simulation{0}{0}", Environment.NewLine);
            sMessage += String.Format("MB-Soft{0}All rights reserved (c)2023.", Environment.NewLine);
            MessageBox.Show(sMessage, "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion //CustomizeSystemMenu

        #region FormMain functions
        /// <summary>
        /// FormMain functions
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            fRatio = (float)pictureBoxBackground.Image.Width / (float)pictureBoxBackground.Image.Height;
            panelBackground.Dock = DockStyle.Fill;
            FormMain_Resize(sender, e);

            clLittleProfessor2 = new MBLittleProfessor2();
            timerDisplay.Enabled = true;
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            if ((int)(pictureBoxBackground.Width / fRatio) <= panelBackground.Height)
            {
                pictureBoxBackground.Width = panelBackground.Width;
                pictureBoxBackground.Height = (int)(pictureBoxBackground.Width / fRatio);
            }

            if (pictureBoxBackground.Height >= panelBackground.Height)
            {
                pictureBoxBackground.Width = (int)(panelBackground.Height * fRatio);
                pictureBoxBackground.Height = (int)(pictureBoxBackground.Width / fRatio);
            }

            fDx = (float)pictureBoxBackground.Width / (float)pictureBoxBackground.Image.Width;
            fDy = (float)pictureBoxBackground.Height / (float)pictureBoxBackground.Image.Height;

            pictureBoxBackground.Top = (panelBackground.Height - pictureBoxBackground.Height) / 2;
            pictureBoxBackground.Left = (panelBackground.Width - pictureBoxBackground.Width) / 2;
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            // Überprüfe, ob die Escape-Taste gedrückt wurde
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }

            // Überprüfe, ob die F1-Taste gedrückt wurde
            if (e.KeyCode == Keys.F1)
            {
                Help();
            }

            // Überprüfe, ob die F2-Taste gedrückt wurde
            if (e.KeyCode == Keys.F2)
            {
                About();
            }
            
            // Überprüfen, ob eine Zifferntaste gedrückt wurde (von der Haupttastatur oder der numerischen Tastatur)
            if ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9))
            {
                int nZiffer = -1;

                if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
                {
                    nZiffer = e.KeyCode - Keys.D0;
                }
                else
                {
                    nZiffer = e.KeyCode - Keys.NumPad0;
                }

                if (nZiffer >= 0 && nZiffer <= 9)
                    clLittleProfessor2.SetInput(nZiffer);
            }

            // Überprüfe, ob eine der Funktionstasten +,-,*,/,S,L,R,H gedrückt wurde
            if (e.KeyCode == Keys.Add || (e.Shift == false && e.KeyValue == 187))
            {
                clLittleProfessor2.NextAddition();
            }
            else if (e.KeyCode == Keys.Subtract || (e.Shift == false && e.KeyValue == 189))
            {
                clLittleProfessor2.NextSubtraction();
            }
            else if (e.KeyCode == Keys.Multiply || (e.Shift == true && e.KeyValue == 187))
            {
                clLittleProfessor2.NextMultiplication();
            }
            else if (e.KeyCode == Keys.Divide || (e.Shift == true && e.KeyValue == 55))
            {
                clLittleProfessor2.NextDivision();
            }
            else if (e.KeyCode == Keys.S)
            {
                clLittleProfessor2.Start();
            }
            else if (e.KeyCode == Keys.L)
            {
                clLittleProfessor2.NextLevel();
            }
            else if (e.KeyCode == Keys.R)
            {
                clLittleProfessor2.Next1x1();
            }
            else if (e.KeyCode == Keys.H)
            {
                // checkBoxHint.Checked = !checkBoxHint.Checked;
            }

            pictureBoxBackground.Refresh();
        }
        #endregion // FormMain functions

        #region PictureBoxFunctions
        private void pictureBoxBackground_Paint(object sender, PaintEventArgs e)
        {
            // draw displayed text on image
            using (Font arialFont = new Font("Consolas", 24 * fDx))
            {
                float x = 55 * fDx;
                float y = 175 * fDy - arialFont.Height;
                e.Graphics.DrawString(clLittleProfessor2.GetDisplay(), arialFont, Brushes.Black, x, y);
            }
            // draw level as text on image
            using (Font arialFont = new Font("Consolas", 10 * fDx))
            {
                float x = 75 * fDx;
                float y = 190 * fDy - arialFont.Height;
                e.Graphics.DrawString(clLittleProfessor2.GetLevel(), arialFont, Brushes.Black, x, y);
            }
            // draw rating as text on image
            using (Font arialFont = new Font("Consolas", 20 * fDx))
            {
                float x = 135 * fDx;
                float y = 200 * fDy - arialFont.Height;
                e.Graphics.DrawString(clLittleProfessor2.GetRating(), arialFont, Brushes.Black, x, y);
            }

            // draw icon-image on background
            String icon = clLittleProfessor2.GetIcon();
            Image image = Properties.Resources.TI_Little_Professor_Icon_1;
            if (icon == "J") // :)
                image = Properties.Resources.TI_Little_Professor_Icon_2; 
            else if (icon == "K") // :|
                image = Properties.Resources.TI_Little_Professor_Icon_1;
            else if (icon == "L") // :(
                image = Properties.Resources.TI_Little_Professor_Icon_3;

            using(image) //if (true) //using (Image image = Properties.Resources.TI_Little_Professor_Icon_1)
            {
                float x = 260 * fDx;
                float y = 140 * fDy;
                float w = 46 * fDx;
                float h = 46 * fDy;

                Rectangle imageRect = new Rectangle((int)x, (int)y, (int)w, (int)h);
                e.Graphics.DrawImage(image, imageRect);
            }
        }

        private void pictureBoxBackground_MouseMove(object sender, MouseEventArgs e)
        {
            if (iMouseXPos == (int)(e.X / fDx) && iMouseYPos == (int)(e.Y / fDy))
                return;

            iMouseXPos = (int)(e.X / fDx);
            iMouseYPos = (int)(e.Y / fDy);

            this.Text = String.Format("{0} [x={1} y={2}]", sPrgName, iMouseXPos, iMouseYPos);


            BUTTON button = BUTTON.none;
            for (int i = 0; i < BUTTONname.Length; i++)
            {
                if (e.X >= BUTTONxp1[i] * fDx && e.X < BUTTONxp2[i] * fDx && e.Y >= BUTTONyp1[i] * fDy && e.Y < BUTTONyp2[i] * fDy)
                {
                    button = (BUTTON)i;
                    break;
                }
            }

            if (button != BUTTON.none)
            {
                if (pictureBoxBackground.Cursor != Cursors.Hand)
                    pictureBoxBackground.Cursor = Cursors.Hand;

                toolTip1.SetToolTip(pictureBoxBackground, String.Format("{0}", BUTTONname[(int)button]));
            }

            if (button == BUTTON.none)
            {
                if (pictureBoxBackground.Cursor != Cursors.Default)
                    pictureBoxBackground.Cursor = Cursors.Default;
                if (toolTip1.GetToolTip(pictureBoxBackground) != "")
                    toolTip1.SetToolTip(pictureBoxBackground, "");
            }
        }

        private void pictureBoxBackground_MouseClick(object sender, MouseEventArgs e)
        {
            BUTTON button = BUTTON.none;
            for (int i=0; i<BUTTONname.Length; i++)
            {
                if (e.X >= BUTTONxp1[i] * fDx && e.X < BUTTONxp2[i] * fDx && e.Y >= BUTTONyp1[i] * fDy && e.Y < BUTTONyp2[i] * fDy)
                {
                    button = (BUTTON)i;
                    break;
                }
            }

            if(button!=BUTTON.none)
            {
                if ((int)button >= 0 && (int)button <= 9)
                    clLittleProfessor2.SetInput((int)button);
                else if (button == BUTTON.start)
                    clLittleProfessor2.Start();
                else if (button == BUTTON.level)
                    clLittleProfessor2.NextLevel();
                else if (button == BUTTON.rnd)
                    clLittleProfessor2.Next1x1();
                else if (button == BUTTON.add)
                    clLittleProfessor2.NextAddition();
                else if (button == BUTTON.sub)
                    clLittleProfessor2.NextSubtraction();
                else if (button == BUTTON.mul)
                    clLittleProfessor2.NextMultiplication();
                else if (button == BUTTON.div)
                    clLittleProfessor2.NextDivision();

                pictureBoxBackground.Refresh();
            }

        }
        #endregion // PictureBoxFunctions

        private void timerDisplay_Tick(object sender, EventArgs e)
        {
            if (clLittleProfessor2.GetSolutionNoteTicker() > 0)
            {
                clLittleProfessor2.DecSolutionNoteTicker();

                if (clLittleProfessor2.GetSolutionNoteTicker() == 1)
                {
                    clLittleProfessor2.ShowSolutionNote();
                }
                pictureBoxBackground.Refresh();
            }

            if (clLittleProfessor2.GetNextCalculationTicker() > 0)
            {
                clLittleProfessor2.DecNextCalculationTicker();

                if (clLittleProfessor2.GetNextCalculationTicker() == 1)
                {
                    clLittleProfessor2.NextCalculation();
                }
                pictureBoxBackground.Refresh();
            }

            if (clLittleProfessor2.GetSameCalculationTicker() > 0)
            {
                clLittleProfessor2.DecSameCalculationTicker();

                if (clLittleProfessor2.GetSameCalculationTicker() == 1)
                {
                    clLittleProfessor2.SameCalculation();
                }
                pictureBoxBackground.Refresh();
            }

            if (clLittleProfessor2.Get1x1CalculationTicker() > 0)
            {
                clLittleProfessor2.Dec1x1CalculationTicker();
                pictureBoxBackground.Refresh();
            }

            if (clLittleProfessor2.GetShowCompleteResultTicker() > 0)
            {
                clLittleProfessor2.DecShowCompleteResultTicker();
                if (clLittleProfessor2.GetShowCompleteResultTicker() == 1)
                {
                    clLittleProfessor2.ResetCalculations();
                    clLittleProfessor2.NextCalculation();
                }
                pictureBoxBackground.Refresh();
            }
        }

    }
}
