using System.Windows.Forms;

namespace ArdeshirV.TestForms
{
    partial class FormMainTest
    {        
        #region Dispose

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        /*protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }*/
        
        #endregion
        //-------------------------------------------------------------------------------------
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        	System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMainTest));
        	this.buttonNewForm = new System.Windows.Forms.Button();
        	this.buttonErrorHandlerForm = new System.Windows.Forms.Button();
        	this.buttonFormAbout = new System.Windows.Forms.Button();
        	this.buttonShrinkWidth = new System.Windows.Forms.Button();
        	this.buttonShrinkHeight = new System.Windows.Forms.Button();
        	this.buttonExit = new System.Windows.Forms.Button();
        	this.buttonSplashForm = new System.Windows.Forms.Button();
        	this.m_lblMessage = new System.Windows.Forms.Label();
        	this.ButtonFormMessage = new System.Windows.Forms.Button();
        	this.ButtonInput = new System.Windows.Forms.Button();
        	this.SuspendLayout();
        	// 
        	// buttonNewForm
        	// 
        	this.buttonNewForm.Anchor = System.Windows.Forms.AnchorStyles.None;
        	this.buttonNewForm.BackColor = System.Drawing.Color.Transparent;
        	this.buttonNewForm.Location = new System.Drawing.Point(70, 268);
        	this.buttonNewForm.Name = "buttonNewForm";
        	this.buttonNewForm.Size = new System.Drawing.Size(111, 23);
        	this.buttonNewForm.TabIndex = 8;
        	this.buttonNewForm.Text = "New SpecialForm ...";
        	this.buttonNewForm.UseVisualStyleBackColor = false;
        	this.buttonNewForm.Click += new System.EventHandler(this.buttonNewForm_Click);
        	// 
        	// buttonErrorHandlerForm
        	// 
        	this.buttonErrorHandlerForm.Anchor = System.Windows.Forms.AnchorStyles.None;
        	this.buttonErrorHandlerForm.BackColor = System.Drawing.Color.Transparent;
        	this.buttonErrorHandlerForm.Location = new System.Drawing.Point(70, 210);
        	this.buttonErrorHandlerForm.Name = "buttonErrorHandlerForm";
        	this.buttonErrorHandlerForm.Size = new System.Drawing.Size(111, 23);
        	this.buttonErrorHandlerForm.TabIndex = 6;
        	this.buttonErrorHandlerForm.Text = "Form Error Handler ...";
        	this.buttonErrorHandlerForm.UseVisualStyleBackColor = false;
        	this.buttonErrorHandlerForm.Click += new System.EventHandler(this.buttonErrorHandlerForm_Click);
        	// 
        	// buttonFormAbout
        	// 
        	this.buttonFormAbout.Anchor = System.Windows.Forms.AnchorStyles.None;
        	this.buttonFormAbout.BackColor = System.Drawing.Color.Transparent;
        	this.buttonFormAbout.Location = new System.Drawing.Point(70, 239);
        	this.buttonFormAbout.Name = "buttonFormAbout";
        	this.buttonFormAbout.Size = new System.Drawing.Size(111, 23);
        	this.buttonFormAbout.TabIndex = 7;
        	this.buttonFormAbout.Text = "Form &About ...";
        	this.buttonFormAbout.UseVisualStyleBackColor = false;
        	this.buttonFormAbout.Click += new System.EventHandler(this.buttonFormAbout_Click);
        	// 
        	// buttonShrinkWidth
        	// 
        	this.buttonShrinkWidth.Anchor = System.Windows.Forms.AnchorStyles.None;
        	this.buttonShrinkWidth.BackColor = System.Drawing.Color.Transparent;
        	this.buttonShrinkWidth.Location = new System.Drawing.Point(70, 65);
        	this.buttonShrinkWidth.Name = "buttonShrinkWidth";
        	this.buttonShrinkWidth.Size = new System.Drawing.Size(111, 23);
        	this.buttonShrinkWidth.TabIndex = 1;
        	this.buttonShrinkWidth.Text = "Shrink &Horizontally";
        	this.buttonShrinkWidth.UseVisualStyleBackColor = false;
        	this.buttonShrinkWidth.Click += new System.EventHandler(this.buttonShrinkWidth_Click);
        	// 
        	// buttonShrinkHeight
        	// 
        	this.buttonShrinkHeight.Anchor = System.Windows.Forms.AnchorStyles.None;
        	this.buttonShrinkHeight.BackColor = System.Drawing.Color.Transparent;
        	this.buttonShrinkHeight.Location = new System.Drawing.Point(70, 94);
        	this.buttonShrinkHeight.Name = "buttonShrinkHeight";
        	this.buttonShrinkHeight.Size = new System.Drawing.Size(111, 23);
        	this.buttonShrinkHeight.TabIndex = 2;
        	this.buttonShrinkHeight.Text = "Shrink &Vertically";
        	this.buttonShrinkHeight.UseVisualStyleBackColor = false;
        	this.buttonShrinkHeight.Click += new System.EventHandler(this.buttonShrinkHeight_Click);
        	// 
        	// buttonExit
        	// 
        	this.buttonExit.Anchor = System.Windows.Forms.AnchorStyles.None;
        	this.buttonExit.BackColor = System.Drawing.Color.Transparent;
        	this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        	this.buttonExit.Location = new System.Drawing.Point(70, 297);
        	this.buttonExit.Name = "buttonExit";
        	this.buttonExit.Size = new System.Drawing.Size(111, 23);
        	this.buttonExit.TabIndex = 9;
        	this.buttonExit.Text = "E&xit";
        	this.buttonExit.UseVisualStyleBackColor = false;
        	this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
        	// 
        	// buttonSplashForm
        	// 
        	this.buttonSplashForm.Anchor = System.Windows.Forms.AnchorStyles.None;
        	this.buttonSplashForm.BackColor = System.Drawing.Color.Transparent;
        	this.buttonSplashForm.Location = new System.Drawing.Point(70, 123);
        	this.buttonSplashForm.Name = "buttonSplashForm";
        	this.buttonSplashForm.Size = new System.Drawing.Size(111, 23);
        	this.buttonSplashForm.TabIndex = 3;
        	this.buttonSplashForm.Text = "&Splash Form ...";
        	this.buttonSplashForm.UseVisualStyleBackColor = false;
        	this.buttonSplashForm.Click += new System.EventHandler(this.buttonSplashForm_Click);
        	// 
        	// m_lblMessage
        	// 
        	this.m_lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
        	this.m_lblMessage.BackColor = System.Drawing.SystemColors.ActiveCaption;
        	this.m_lblMessage.FlatStyle = System.Windows.Forms.FlatStyle.System;
        	this.m_lblMessage.Location = new System.Drawing.Point(12, 9);
        	this.m_lblMessage.Name = "m_lblMessage";
        	this.m_lblMessage.Size = new System.Drawing.Size(224, 14);
        	this.m_lblMessage.TabIndex = 0;
        	this.m_lblMessage.Text = "XXX";
        	this.m_lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        	// 
        	// ButtonFormMessage
        	// 
        	this.ButtonFormMessage.Anchor = System.Windows.Forms.AnchorStyles.None;
        	this.ButtonFormMessage.BackColor = System.Drawing.Color.Transparent;
        	this.ButtonFormMessage.Location = new System.Drawing.Point(70, 152);
        	this.ButtonFormMessage.Name = "ButtonFormMessage";
        	this.ButtonFormMessage.Size = new System.Drawing.Size(111, 23);
        	this.ButtonFormMessage.TabIndex = 4;
        	this.ButtonFormMessage.Text = "Form &Message...";
        	this.ButtonFormMessage.UseVisualStyleBackColor = false;
        	this.ButtonFormMessage.Click += new System.EventHandler(this.ButtonFormMessageClick);
        	// 
        	// ButtonInput
        	// 
        	this.ButtonInput.Anchor = System.Windows.Forms.AnchorStyles.None;
        	this.ButtonInput.BackColor = System.Drawing.Color.Transparent;
        	this.ButtonInput.Location = new System.Drawing.Point(70, 181);
        	this.ButtonInput.Name = "ButtonInput";
        	this.ButtonInput.Size = new System.Drawing.Size(111, 23);
        	this.ButtonInput.TabIndex = 5;
        	this.ButtonInput.Text = "Form &Input...";
        	this.ButtonInput.UseVisualStyleBackColor = false;
        	this.ButtonInput.Click += new System.EventHandler(this.ButtonInputClick);
        	// 
        	// FormMainTest
        	// 
        	this.AcceptButton = this.buttonShrinkWidth;
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.BackColor = System.Drawing.SystemColors.Control;
        	this.CancelButton = this.buttonExit;
        	this.ClientSize = new System.Drawing.Size(248, 370);
        	this.Controls.Add(this.ButtonInput);
        	this.Controls.Add(this.ButtonFormMessage);
        	this.Controls.Add(this.m_lblMessage);
        	this.Controls.Add(this.buttonSplashForm);
        	this.Controls.Add(this.buttonExit);
        	this.Controls.Add(this.buttonShrinkHeight);
        	this.Controls.Add(this.buttonShrinkWidth);
        	this.Controls.Add(this.buttonFormAbout);
        	this.Controls.Add(this.buttonErrorHandlerForm);
        	this.Controls.Add(this.buttonNewForm);
        	this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        	this.MinimumSize = new System.Drawing.Size(160, 337);
        	this.Name = "FormMainTest";
        	this.Text = "Test ArdeshirV.Forms";
        	this.Resize += new System.EventHandler(this.TestArdeshirV_Forms_Resize);
        	this.ResumeLayout(false);

        }

        #endregion
        //-------------------------------------------------------------------------------------
        #region Variables

        /// <summary>
        /// Required designer variable.
        /// </summary>
        //private System.ComponentModel.IContainer components = null;

        private Button buttonNewForm;
        private Button buttonErrorHandlerForm;
        private Button buttonFormAbout;
        private Button buttonShrinkWidth;
        private Button buttonShrinkHeight;
        private Button buttonExit;
        private Button buttonSplashForm;
        private Label m_lblMessage;
        private Button ButtonFormMessage;
        private Button ButtonInput;
        
        #endregion
    }
}
