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
        #endregion // PrivateClassMembers

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
        #endregion // FormMain functions
    }
}
