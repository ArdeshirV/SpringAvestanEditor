#region Header

// ArdeshirV.Forms Project
// FormMessage.Designer.cs : Provides a Cutomized MessageBox
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
	partial class FormMessage
	{
		#region Variables

		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		private Button buttonOne;
		private Button buttonTwo;
		private Button buttonThree;
		private Label labelMessage;
		private PictureBox pictureBoxIcon;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelBottom;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelIcon;
		
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
			this.buttonOne = new System.Windows.Forms.Button();
			this.buttonTwo = new System.Windows.Forms.Button();
			this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
			this.buttonThree = new System.Windows.Forms.Button();
			this.labelMessage = new System.Windows.Forms.Label();
			this.tableLayoutPanelBottom = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanelIcon = new System.Windows.Forms.TableLayoutPanel();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
			this.tableLayoutPanelBottom.SuspendLayout();
			this.tableLayoutPanelIcon.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonOne
			// 
			this.buttonOne.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOne.Location = new System.Drawing.Point(4, 3);
			this.buttonOne.Name = "buttonOne";
			this.buttonOne.Size = new System.Drawing.Size(70, 23);
			this.buttonOne.TabIndex = 1;
			this.buttonOne.Text = "One";
			this.buttonOne.UseVisualStyleBackColor = true;
			// 
			// buttonTwo
			// 
			this.buttonTwo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonTwo.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonTwo.Location = new System.Drawing.Point(80, 3);
			this.buttonTwo.Name = "buttonTwo";
			this.buttonTwo.Size = new System.Drawing.Size(69, 23);
			this.buttonTwo.TabIndex = 2;
			this.buttonTwo.Text = "Two";
			this.buttonTwo.UseVisualStyleBackColor = true;
			// 
			// pictureBoxIcon
			// 
			this.pictureBoxIcon.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.pictureBoxIcon.BackColor = System.Drawing.Color.Transparent;
			this.pictureBoxIcon.Image = global::ArdeshirV.Forms.Properties.Resources.IconInfo;
			this.pictureBoxIcon.Location = new System.Drawing.Point(3, 3);
			this.pictureBoxIcon.Name = "pictureBoxIcon";
			this.pictureBoxIcon.Size = new System.Drawing.Size(42, 45);
			this.pictureBoxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBoxIcon.TabIndex = 2;
			this.pictureBoxIcon.TabStop = false;
			// 
			// buttonThree
			// 
			this.buttonThree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonThree.Location = new System.Drawing.Point(155, 3);
			this.buttonThree.Name = "buttonThree";
			this.buttonThree.Size = new System.Drawing.Size(71, 23);
			this.buttonThree.TabIndex = 3;
			this.buttonThree.Text = "Three";
			this.buttonThree.UseVisualStyleBackColor = true;
			// 
			// labelMessage
			// 
			this.labelMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.labelMessage.BackColor = System.Drawing.Color.Transparent;
			this.labelMessage.Location = new System.Drawing.Point(63, 12);
			this.labelMessage.Name = "labelMessage";
			this.labelMessage.Size = new System.Drawing.Size(178, 51);
			this.labelMessage.TabIndex = 0;
			this.labelMessage.Text = "Message";
			this.labelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tableLayoutPanelBottom
			// 
			this.tableLayoutPanelBottom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanelBottom.BackColor = System.Drawing.Color.Transparent;
			this.tableLayoutPanelBottom.ColumnCount = 3;
			this.tableLayoutPanelBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanelBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
			this.tableLayoutPanelBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanelBottom.Controls.Add(this.buttonThree, 2, 0);
			this.tableLayoutPanelBottom.Controls.Add(this.buttonTwo, 1, 0);
			this.tableLayoutPanelBottom.Controls.Add(this.buttonOne, 0, 0);
			this.tableLayoutPanelBottom.Location = new System.Drawing.Point(12, 66);
			this.tableLayoutPanelBottom.Name = "tableLayoutPanelBottom";
			this.tableLayoutPanelBottom.RowCount = 1;
			this.tableLayoutPanelBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelBottom.Size = new System.Drawing.Size(229, 29);
			this.tableLayoutPanelBottom.TabIndex = 4;
			// 
			// tableLayoutPanelIcon
			// 
			this.tableLayoutPanelIcon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left)));
			this.tableLayoutPanelIcon.BackColor = System.Drawing.Color.Transparent;
			this.tableLayoutPanelIcon.ColumnCount = 1;
			this.tableLayoutPanelIcon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanelIcon.Controls.Add(this.pictureBoxIcon, 0, 0);
			this.tableLayoutPanelIcon.Location = new System.Drawing.Point(12, 12);
			this.tableLayoutPanelIcon.Name = "tableLayoutPanelIcon";
			this.tableLayoutPanelIcon.RowCount = 1;
			this.tableLayoutPanelIcon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelIcon.Size = new System.Drawing.Size(48, 51);
			this.tableLayoutPanelIcon.TabIndex = 5;
			// 
			// FormMessage
			// 
			this.AcceptButton = this.buttonOne;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonTwo;
			this.ClientSize = new System.Drawing.Size(253, 107);
			this.ControlBox = false;
			this.Controls.Add(this.tableLayoutPanelIcon);
			this.Controls.Add(this.labelMessage);
			this.Controls.Add(this.tableLayoutPanelBottom);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormMessage";
			this.Text = "FormMessage";
			this.Shown += new System.EventHandler(this.FormMessageShown);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
			this.tableLayoutPanelBottom.ResumeLayout(false);
			this.tableLayoutPanelIcon.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		
		#endregion InitializeComponent
	}
}
