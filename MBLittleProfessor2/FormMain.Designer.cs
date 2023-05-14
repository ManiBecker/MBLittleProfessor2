namespace MBLittleProfessor2
{
    partial class FormMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.panelBackground = new System.Windows.Forms.Panel();
            this.pictureBoxBackground = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timerDisplay = new System.Windows.Forms.Timer(this.components);
            this.panelBackground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackground)).BeginInit();
            this.SuspendLayout();
            // 
            // panelBackground
            // 
            this.panelBackground.Controls.Add(this.pictureBoxBackground);
            this.panelBackground.Location = new System.Drawing.Point(12, 12);
            this.panelBackground.Name = "panelBackground";
            this.panelBackground.Size = new System.Drawing.Size(304, 497);
            this.panelBackground.TabIndex = 1;
            // 
            // pictureBoxBackground
            // 
            this.pictureBoxBackground.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBoxBackground.Image = global::MBLittleProfessor2.Properties.Resources.TI_Little_Professor_1991;
            this.pictureBoxBackground.Location = new System.Drawing.Point(16, 18);
            this.pictureBoxBackground.Name = "pictureBoxBackground";
            this.pictureBoxBackground.Size = new System.Drawing.Size(272, 461);
            this.pictureBoxBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxBackground.TabIndex = 1;
            this.pictureBoxBackground.TabStop = false;
            this.pictureBoxBackground.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxBackground_MouseMove);
            this.pictureBoxBackground.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxBackground_MouseClick);
            this.pictureBoxBackground.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxBackground_Paint);
            // 
            // timerDisplay
            // 
            this.timerDisplay.Tick += new System.EventHandler(this.timerDisplay_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 533);
            this.Controls.Add(this.panelBackground);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "MBLittleProfessor2";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.panelBackground.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackground)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBackground;
        private System.Windows.Forms.PictureBox pictureBoxBackground;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer timerDisplay;

    }
}

