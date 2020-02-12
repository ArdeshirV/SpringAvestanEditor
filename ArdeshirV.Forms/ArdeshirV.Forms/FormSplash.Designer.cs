using System.Windows.Forms;

namespace ArdeshirV.Forms
{
    partial class FormSplash
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox m_imgPictureBox;
        private System.Windows.Forms.ProgressBar progressBar;
        //---------------------------------------------------------------------
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }

            base.Dispose(disposing);
        }
        //---------------------------------------------------------------------
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        	this.m_imgPictureBox = new System.Windows.Forms.PictureBox();
        	this.progressBar = new System.Windows.Forms.ProgressBar();
        	((System.ComponentModel.ISupportInitialize)(this.m_imgPictureBox)).BeginInit();
        	this.SuspendLayout();
        	// 
        	// m_imgPictureBox
        	// 
        	this.m_imgPictureBox.BackColor = System.Drawing.Color.Transparent;
        	this.m_imgPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.m_imgPictureBox.Location = new System.Drawing.Point(0, 0);
        	this.m_imgPictureBox.Name = "m_imgPictureBox";
        	this.m_imgPictureBox.Size = new System.Drawing.Size(400, 250);
        	this.m_imgPictureBox.TabIndex = 0;
        	this.m_imgPictureBox.TabStop = false;
        	this.m_imgPictureBox.Click += new System.EventHandler(this.M_imgPictureBox_Click);
        	// 
        	// progressBar
        	// 
        	this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
        	this.progressBar.Location = new System.Drawing.Point(0, 230);
        	this.progressBar.Name = "progressBar";
        	this.progressBar.Size = new System.Drawing.Size(400, 20);
        	this.progressBar.TabIndex = 1;
        	this.progressBar.Click += new System.EventHandler(this.M_imgPictureBox_Click);
        	// 
        	// FormSplash
        	// 
        	this.ClientSize = new System.Drawing.Size(400, 250);
        	this.Controls.Add(this.progressBar);
        	this.Controls.Add(this.m_imgPictureBox);
        	this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        	this.MoveFormWithMouse = false;
        	this.Name = "FormSplash";
        	this.ShowInTaskbar = false;
        	this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        	this.TopMost = true;
        	this.Shown += new System.EventHandler(this.FormSplashShown);
        	this.Click += new System.EventHandler(this.M_imgPictureBox_Click);
        	this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormSplash_KeyPress);
        	((System.ComponentModel.ISupportInitialize)(this.m_imgPictureBox)).EndInit();
        	this.ResumeLayout(false);

        }

        #endregion
    }
}
