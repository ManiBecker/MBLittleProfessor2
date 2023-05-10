using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MBLittleProfessor2
{
    public partial class FormMain : Form
    {
        private float fRatio, fDx, fDy;

        public FormMain()
        {
            InitializeComponent();
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

        private void FormMain_Load(object sender, EventArgs e)
        {
            fRatio = (float)pictureBoxBackground.Image.Width / (float)pictureBoxBackground.Image.Height;
            panelBackground.Dock = DockStyle.Fill;
            FormMain_Resize(sender, e);
        }
    }
}
