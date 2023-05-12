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

        #region LittleProfessorFunctions
        void Start()
        {
            //ToDo...
            MessageBox.Show("Starts new calculation task", "Start", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void Level()
        {
            //ToDo...
            MessageBox.Show("Sets the level of difficulty", "Start", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void Rnd()
        {
            //ToDo...
            MessageBox.Show("Starts new 1x1 task", "Start", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion //LittleProfessorFunctions

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
                        switch (e.KeyCode)
            {
                case Keys.Escape:
                    DialogResult dialogResult = MessageBox.Show("Are you sure?", String.Format("Exit {0}",sPrgName), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Close();
                    }
                    e.Handled = true;
                    break;

                case Keys.F1:
                    Help();
                    e.Handled = true;
                    break;

                case Keys.F2:
                    About();
                    e.Handled = true;
                    break;
            }
        }
        #endregion // FormMain functions

        #region PictureBoxFunctions
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
                if (BUTTONname[(int)button] == "Start")
                    Start();
                else if (BUTTONname[(int)button] == "Level")
                    Level();
                else if (BUTTONname[(int)button] == "1x1")
                    Rnd();
            }

        }
        #endregion // PictureBoxFunctions
    }
}
