#region Header

// Form Error Handler.cs : Provides Forn Error Handler
// Copyright© 2002-2020 ArdeshirV@protonmail.com, Licensed under LGPLv3+

using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

#endregion
//---------------------------------------------------------------------------------------
namespace ArdeshirV.Forms
{
    /// <summary>
    /// About Form in Vector Project.
    /// </summary>
    public class FormErrorHandler : FormBase
	{
        #region Variables

        private Exception exp;
        private Button m_btnOk;
        private ToolTip toolTip;
        private Button m_btnMore;
        private Button buttonCopy;
		private LinkLabel m_lnkLink;
        private string m_strLink = "";
        private Label m_lblStackTrack;
        private Button buttonInnerExp;
        private TextBox textBoxMessage;
        private IWin32Window hwndOwner;
        private readonly string strFileLine;
		private bool m_blnIsShrinked = false;
        private const int intMinHeight = 191;
        private const int intMaxHeight = 446;
        //private static bool s_blnIsExists = false;
        private System.Windows.Forms.Panel panelTop;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.Label labelReportError;
        private System.Windows.Forms.TextBox textBoxStackTrack;
        #endregion
        //-------------------------------------------------------------------------------
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="expException">Exception</param>
        /// <param name="wOwner">Form parent</param>
        /// <param name="strLinkSite">Link value</param>
        protected FormErrorHandler(Exception expException,
                                   IWin32Window wOwner, string strLinkSite) : base(wOwner)
        {
            InitializeComponent();
            Opacity = 1.0;
            hwndOwner = wOwner;
            exp = expException;
            m_strLink = strLinkSite;
            textBoxMessage.SelectedText = "";
			DialogResult = DialogResult.Cancel;
            //textBoxMessage.Text = expException.Message;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            //textBoxMessage.BackColor = Color.LightGray;
            string strTrack = textBoxStackTrack.Text = expException.StackTrace;

            if(wOwner != null && wOwner is Form) {
            	Form form = wOwner as Form;
            	Text = string.Format("{0} - Error", form.Text);
            }
            else
            	Text = "Error";
            
            const string strLine = ":line";
            string strNewLine = Environment.NewLine;
            string[] stringArrTrackLines = strTrack.Split(new string[]{strNewLine},
				StringSplitOptions.RemoveEmptyEntries);
            
            foreach(string line in stringArrTrackLines) {
            	if(line.IndexOf(strLine, StringComparison.InvariantCulture) >= 0) {
            		int intLastP1 = line.LastIndexOf("\\", StringComparison.InvariantCulture);
            		int intLastP2 = line.LastIndexOf("/", StringComparison.InvariantCulture);
            		int intLastP = 1 + ((intLastP1 > intLastP2)? intLastP1: intLastP2);
            		strFileLine = line.Substring(intLastP);
            		break;
            	}
            }
            
            string strMsgTitle = string.Empty;
            
            if(!string.IsNullOrEmpty(strFileLine))
            	strMsgTitle = string.Concat(
					strFileLine, Environment.NewLine, Environment.NewLine);
            
            textBoxMessage.Text = strMsgTitle + expException.Message;            
            buttonInnerExp.Enabled = (exp.InnerException != null);
            textBoxStackTrack.BackColor = Color.Black;

            if (string.IsNullOrEmpty(m_strLink)) {
				m_lnkLink.Visible =
				labelReportError.Visible = false;
            } else {
	        	//if(wOwner != null)
	        		//m_lnkLink.Text = m_strLink;
	        			//string.Format("Visit {0} on the web", frmOwner.Text);
	        	//else
	            	m_lnkLink.Text = m_strLink;
			}

            ShowDialog(wOwner);
		}

		#endregion
        //-------------------------------------------------------------------------------
        #region Utility functions

        /// <summary>
        /// Shows error handler form
        /// </summary>
        public static FormErrorHandler Show(Form frmOwner,
			Exception exp, string strLinkSite) {
            return new FormErrorHandler(exp, frmOwner, strLinkSite);
        }
        
        /// <summary>
        /// Shows error handler form
        /// </summary>
        public static FormErrorHandler Show(Form frmOwner, Exception exp) {
            return new FormErrorHandler(exp, frmOwner, string.Empty);
        }

        #endregion
        //-------------------------------------------------------------------------------
		#region Event Handlers

		/// <summary>
		/// Occurs whenever OK Button became Click.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOk_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}
		//-------------------------------------------------------------------------------
		/// <summary>
		/// Linke to web site.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void lnkLink_LinkClicked(object sender,LinkLabelLinkClickedEventArgs e)
		{
			m_lnkLink.LinkVisited = true;
			Process.Start(m_strLink);
		}
        //---------------------------------------------------------------------
		/// <summary>
		/// Occurs whenever Dialog has been loaded.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmAbout_Load(object sender, System.EventArgs e)
		{
			//if(s_blnIsExists)
			//	Close();
			//else
			//	s_blnIsExists = true;
		}
		//-------------------------------------------------------------------------------
		/// <summary>
		/// Occured whenever btnMore button has been clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnMore_Click(object sender, System.EventArgs e)
		{
            m_btnMore.Enabled = false;
			m_blnIsShrinked = !m_blnIsShrinked;

			if(Height <= intMinHeight) {
				ShrinkHeightByTime(intMaxHeight);
				m_btnMore.Text = "&Less <<";
			} else {
                ShrinkHeightByTime(intMinHeight);
				m_btnMore.Text = "&More >>";
			}

            //MessageBox.Show(ClientSize.ToString());
            m_btnMore.Enabled = true;
		}
		//-------------------------------------------------------------------------------
		/// <summary>
		/// Occured whnever btnSend button has been clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSend_Click(object sender, System.EventArgs e)
        {
            #region Hidden
            /*
			string l_strMessage;
            System.Net.Mail.MailMessage l_milMessage = new System.Net.Mail.MailMessage();

			l_strMessage =
				"User name: "  + Environment.UserName + "\n" +
				"Current date & time: " + System.DateTime.Now.ToString() + "\n" +
				"-------------------------------Contents------------------------\n" +
				m_txtMessage.Text + "\n" +
				"---------------------------------------------------------------\n";
			l_milMessage.To.Add(m_strTechnicalSupportMail);
			l_milMessage.From = new System.Net.Mail.MailAddress("nobody@somewhere.net");
			l_milMessage.Subject = "[SSTRobot]Automatic Sended from " + m_txtErrorMessage.Text;
			l_milMessage.Body = l_strMessage;
            l_milMessage.BodyEncoding = System.Text.Encoding.Unicode;

			try
			{
                System.Net.Mail.SmtpClient l_stpClient = new System.Net.Mail.SmtpClient();

                l_stpClient.Send(l_milMessage);
			}
			catch(Exception exp)
			{
				MessageBox.Show(this, exp.Message, "Error!", MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
			((Button)sender).Enabled = false;*/
            #endregion
        }
		//-------------------------------------------------------------------------------
        private void FormErrorHandler_FormClosed(object sender, FormClosedEventArgs e)
        {
            //s_blnIsExists = false;
        }
        //---------------------------------------------------------------------
        private void ButtonInnerExp_Click(object sender, EventArgs e)
        {
            if(exp.InnerException != null)
                FormErrorHandler.Show(this, exp.InnerException, m_strLink);
        }
        //---------------------------------------------------------------------
		void buttonCopy_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(textBoxMessage.Text);
		}
		//-------------------------------------------------------------------------------
		void FormErrorHandlerShown(object sender, EventArgs e)
		{
			Height = intMinHeight;
			m_lnkLink.Height = 19;
			//ClientSize = MinSize;
		}

		#endregion
		//-------------------------------------------------------------------------------
		#region overrided functions

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/*protected override void Dispose(bool disposing)
		{
			if(disposing)
				if(components != null)
					components.Dispose();

			base.Dispose(disposing);
		}*/

		#endregion
		//-------------------------------------------------------------------------------
		#region Windows Form Designer generated code

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.m_btnOk = new System.Windows.Forms.Button();
			this.m_btnMore = new System.Windows.Forms.Button();
			this.m_lblStackTrack = new System.Windows.Forms.Label();
			this.buttonInnerExp = new System.Windows.Forms.Button();
			this.textBoxMessage = new System.Windows.Forms.TextBox();
			this.buttonCopy = new System.Windows.Forms.Button();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.m_lnkLink = new System.Windows.Forms.LinkLabel();
			this.labelReportError = new System.Windows.Forms.Label();
			this.textBoxStackTrack = new System.Windows.Forms.TextBox();
			this.panelTop = new System.Windows.Forms.Panel();
			this.panelTop.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_btnOk
			// 
			this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.m_btnOk.BackColor = System.Drawing.Color.Transparent;
			this.m_btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.m_btnOk.Location = new System.Drawing.Point(490, 12);
			this.m_btnOk.Name = "m_btnOk";
			this.m_btnOk.Size = new System.Drawing.Size(82, 23);
			this.m_btnOk.TabIndex = 1;
			this.m_btnOk.Text = "&OK";
			this.m_btnOk.UseVisualStyleBackColor = false;
			this.m_btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// m_btnMore
			// 
			this.m_btnMore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.m_btnMore.BackColor = System.Drawing.Color.Transparent;
			this.m_btnMore.Location = new System.Drawing.Point(490, 99);
			this.m_btnMore.Name = "m_btnMore";
			this.m_btnMore.Size = new System.Drawing.Size(82, 23);
			this.m_btnMore.TabIndex = 4;
			this.m_btnMore.Text = "&More >>";
			this.m_btnMore.UseVisualStyleBackColor = false;
			this.m_btnMore.Click += new System.EventHandler(this.btnMore_Click);
			// 
			// m_lblStackTrack
			// 
			this.m_lblStackTrack.BackColor = System.Drawing.Color.Transparent;
			this.m_lblStackTrack.Location = new System.Drawing.Point(12, 161);
			this.m_lblStackTrack.Name = "m_lblStackTrack";
			this.m_lblStackTrack.Size = new System.Drawing.Size(570, 13);
			this.m_lblStackTrack.TabIndex = 7;
			this.m_lblStackTrack.Text = "&Stack Track:";
			// 
			// buttonInnerExp
			// 
			this.buttonInnerExp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonInnerExp.BackColor = System.Drawing.Color.Transparent;
			this.buttonInnerExp.Location = new System.Drawing.Point(490, 70);
			this.buttonInnerExp.Name = "buttonInnerExp";
			this.buttonInnerExp.Size = new System.Drawing.Size(82, 23);
			this.buttonInnerExp.TabIndex = 3;
			this.buttonInnerExp.Text = "&Inner Error...";
			this.toolTip.SetToolTip(this.buttonInnerExp, "Shows the exception that is the cause of the current exception");
			this.buttonInnerExp.UseVisualStyleBackColor = false;
			this.buttonInnerExp.Click += new System.EventHandler(this.ButtonInnerExp_Click);
			// 
			// textBoxMessage
			// 
			this.textBoxMessage.BackColor = System.Drawing.Color.Black;
			this.textBoxMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBoxMessage.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxMessage.ForeColor = System.Drawing.Color.Red;
			this.textBoxMessage.Location = new System.Drawing.Point(12, 12);
			this.textBoxMessage.Multiline = true;
			this.textBoxMessage.Name = "textBoxMessage";
			this.textBoxMessage.ReadOnly = true;
			this.textBoxMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxMessage.Size = new System.Drawing.Size(472, 110);
			this.textBoxMessage.TabIndex = 0;
			this.textBoxMessage.TabStop = false;
			this.textBoxMessage.Text = "Error Message ...";
			// 
			// buttonCopy
			// 
			this.buttonCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCopy.BackColor = System.Drawing.Color.Transparent;
			this.buttonCopy.Location = new System.Drawing.Point(490, 41);
			this.buttonCopy.Name = "buttonCopy";
			this.buttonCopy.Size = new System.Drawing.Size(82, 23);
			this.buttonCopy.TabIndex = 2;
			this.buttonCopy.Text = "&Copy";
			this.toolTip.SetToolTip(this.buttonCopy, "Copy error message to clipboard");
			this.buttonCopy.UseVisualStyleBackColor = false;
			this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
			// 
			// m_lnkLink
			// 
			this.m_lnkLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.m_lnkLink.BackColor = System.Drawing.Color.Transparent;
			this.m_lnkLink.Location = new System.Drawing.Point(81, 128);
			this.m_lnkLink.Name = "m_lnkLink";
			this.m_lnkLink.Size = new System.Drawing.Size(502, 19);
			this.m_lnkLink.TabIndex = 6;
			this.m_lnkLink.TabStop = true;
			this.m_lnkLink.Text = "Technical Support";
			this.m_lnkLink.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.toolTip.SetToolTip(this.m_lnkLink, "Link/email to technical support");
			this.m_lnkLink.UseCompatibleTextRendering = true;
			this.m_lnkLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLink_LinkClicked);
			// 
			// labelReportError
			// 
			this.labelReportError.BackColor = System.Drawing.Color.Transparent;
			this.labelReportError.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.labelReportError.Location = new System.Drawing.Point(3, 128);
			this.labelReportError.Name = "labelReportError";
			this.labelReportError.Size = new System.Drawing.Size(79, 19);
			this.labelReportError.TabIndex = 5;
			this.labelReportError.Text = "&Report Error:";
			this.labelReportError.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxStackTrack
			// 
			this.textBoxStackTrack.BackColor = System.Drawing.Color.Black;
			this.textBoxStackTrack.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBoxStackTrack.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxStackTrack.ForeColor = System.Drawing.Color.Gold;
			this.textBoxStackTrack.Location = new System.Drawing.Point(12, 177);
			this.textBoxStackTrack.Multiline = true;
			this.textBoxStackTrack.Name = "textBoxStackTrack";
			this.textBoxStackTrack.ReadOnly = true;
			this.textBoxStackTrack.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxStackTrack.Size = new System.Drawing.Size(560, 218);
			this.textBoxStackTrack.TabIndex = 8;
			this.textBoxStackTrack.TabStop = false;
			this.textBoxStackTrack.Text = "Stack Track ...";
			this.textBoxStackTrack.WordWrap = false;
			// 
			// panelTop
			// 
			this.panelTop.BackColor = System.Drawing.Color.Transparent;
			this.panelTop.Controls.Add(this.m_lnkLink);
			this.panelTop.Controls.Add(this.labelReportError);
			this.panelTop.Controls.Add(this.m_btnOk);
			this.panelTop.Controls.Add(this.m_btnMore);
			this.panelTop.Controls.Add(this.buttonInnerExp);
			this.panelTop.Controls.Add(this.textBoxMessage);
			this.panelTop.Controls.Add(this.buttonCopy);
			this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelTop.Location = new System.Drawing.Point(0, 0);
			this.panelTop.Name = "panelTop";
			this.panelTop.Size = new System.Drawing.Size(584, 151);
			this.panelTop.TabIndex = 11;
			// 
			// FormErrorHandler
			// 
			this.ClientSize = new System.Drawing.Size(584, 152);
			this.Controls.Add(this.panelTop);
			this.Controls.Add(this.textBoxStackTrack);
			this.Controls.Add(this.m_lblStackTrack);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormErrorHandler";
			this.Opacity = 0D;
			this.ShowInTaskbar = false;
			this.Text = "Error";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormErrorHandler_FormClosed);
			this.Load += new System.EventHandler(this.frmAbout_Load);
			this.Shown += new System.EventHandler(this.FormErrorHandlerShown);
			this.panelTop.ResumeLayout(false);
			this.panelTop.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		
        #endregion
    }
}
//---------------------------------------------------------------------------------------
