#region Header

// ArdeshirV.Forms Project
// FormInput.Designer.cs : Provides a Cutomized InputBox
// Copyright© 2002-2020 ArdeshirV@protonmail.com, Licensed under LGPLv3+

using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

#endregion
//---------------------------------------------------------------------------------------------
namespace ArdeshirV.Forms
{
	partial class FormInput		
	{
		#region Variables

		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		private Button buttonOK;
		private Button buttonCancel;
		private TextBox textBoxInput;
		private Label labelInputMessage;
		private System.Windows.Forms.Panel panelBack;
		private System.Windows.Forms.Panel panelTopLabel;
		private System.Windows.Forms.Panel panelBackCenter;
		private System.Windows.Forms.Panel panelBottomButtons;
		private System.Windows.Forms.ErrorProvider errorProvider;
		
		#endregion
		//-----------------------------------------------------------------------------------------------
		#region Dispose
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		#endregion Dispose
		//-----------------------------------------------------------------------------------------------
		#region InitializeComponent

		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.labelInputMessage = new System.Windows.Forms.Label();
			this.textBoxInput = new System.Windows.Forms.TextBox();
			this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.panelTopLabel = new System.Windows.Forms.Panel();
			this.panelBottomButtons = new System.Windows.Forms.Panel();
			this.panelBack = new System.Windows.Forms.Panel();
			this.panelBackCenter = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.panelTopLabel.SuspendLayout();
			this.panelBottomButtons.SuspendLayout();
			this.panelBack.SuspendLayout();
			this.panelBackCenter.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(164, 8);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 25);
			this.buttonCancel.TabIndex = 0;
			this.buttonCancel.Text = "&Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.Location = new System.Drawing.Point(83, 8);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 25);
			this.buttonOK.TabIndex = 1;
			this.buttonOK.Text = "&OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.ButtonOKClick);
			// 
			// labelInputMessage
			// 
			this.labelInputMessage.BackColor = System.Drawing.Color.Transparent;
			this.labelInputMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.labelInputMessage.Location = new System.Drawing.Point(0, 5);
			this.labelInputMessage.Name = "labelInputMessage";
			this.labelInputMessage.Size = new System.Drawing.Size(262, 19);
			this.labelInputMessage.TabIndex = 2;
			this.labelInputMessage.Text = "       Enter Input Value: ";
			this.labelInputMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBoxInput
			// 
			this.textBoxInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxInput.Location = new System.Drawing.Point(23, 0);
			this.textBoxInput.Multiline = true;
			this.textBoxInput.Name = "textBoxInput";
			this.textBoxInput.Size = new System.Drawing.Size(216, 22);
			this.textBoxInput.TabIndex = 3;
			this.textBoxInput.Text = "Input value goes here";
			this.textBoxInput.TextChanged += new System.EventHandler(this.TextBoxInputTextChanged);
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// panelTopLabel
			// 
			this.panelTopLabel.BackColor = System.Drawing.Color.Transparent;
			this.panelTopLabel.Controls.Add(this.labelInputMessage);
			this.panelTopLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelTopLabel.Location = new System.Drawing.Point(0, 0);
			this.panelTopLabel.Name = "panelTopLabel";
			this.panelTopLabel.Size = new System.Drawing.Size(262, 24);
			this.panelTopLabel.TabIndex = 4;
			// 
			// panelBottomButtons
			// 
			this.panelBottomButtons.BackColor = System.Drawing.Color.Transparent;
			this.panelBottomButtons.Controls.Add(this.buttonCancel);
			this.panelBottomButtons.Controls.Add(this.buttonOK);
			this.panelBottomButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelBottomButtons.Location = new System.Drawing.Point(0, 46);
			this.panelBottomButtons.Name = "panelBottomButtons";
			this.panelBottomButtons.Size = new System.Drawing.Size(262, 45);
			this.panelBottomButtons.TabIndex = 5;
			// 
			// panelBack
			// 
			this.panelBack.BackColor = System.Drawing.Color.Transparent;
			this.panelBack.Controls.Add(this.panelBackCenter);
			this.panelBack.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelBack.Location = new System.Drawing.Point(0, 24);
			this.panelBack.Name = "panelBack";
			this.panelBack.Size = new System.Drawing.Size(262, 22);
			this.panelBack.TabIndex = 6;
			// 
			// panelBackCenter
			// 
			this.panelBackCenter.Controls.Add(this.textBoxInput);
			this.panelBackCenter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelBackCenter.Location = new System.Drawing.Point(0, 0);
			this.panelBackCenter.Name = "panelBackCenter";
			this.panelBackCenter.Size = new System.Drawing.Size(262, 22);
			this.panelBackCenter.TabIndex = 6;
			// 
			// FormInput
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(262, 91);
			this.ControlBox = false;
			this.Controls.Add(this.panelBack);
			this.Controls.Add(this.panelBottomButtons);
			this.Controls.Add(this.panelTopLabel);
			this.MinimumSize = new System.Drawing.Size(216, 130);
			this.Name = "FormInput";
			this.Text = "Input";
			//this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormInputFormClosing);
			this.Shown += new System.EventHandler(this.FormInputShown);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.panelTopLabel.ResumeLayout(false);
			this.panelBottomButtons.ResumeLayout(false);
			this.panelBack.ResumeLayout(false);
			this.panelBackCenter.ResumeLayout(false);
			this.panelBackCenter.PerformLayout();
			this.ResumeLayout(false);

		}
		
		#endregion InitializeComponent
	}
}
