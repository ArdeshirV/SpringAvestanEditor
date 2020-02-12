#region Header

// FormAbout.cs : Provides Advanced Form About Box
// Copyright© 2002-2020 ArdeshirV@protonmail.com, Licensed under LGPLv3+

using System;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Diagnostics;
using ArdeshirV.Controls;
using ArdeshirV.Utilities;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Imaging;
using ArdeshirV.Forms.Properties;
using System.Collections.Generic;
using qr=ArdeshirV.Utilities.QrCode;

#endregion
//---------------------------------------------------------------------------------------
namespace ArdeshirV.Forms
{
    /// <summary>
    /// About Form
    /// </summary>
    public class FormAbout : FormBase
    {
        #region Variables

        private ImageList _ImageListCurrencies;
        private ImageList _imageListCreditsAvators;
        private Donation _donationSelected = null;
        private FormAboutData data;
        private Button m_btnOk;
        private Button m_btnSysteminfo;
        private Label m_lblApplicationName;
        private PictureBox pictureBoxAppIcon;
        private static bool s_blnIsExists = false;
        private TabControl tabControl;
        private TabPage tabPageCopyright;
        private TabPage tabPageDonation;
        private TabPage tabPageLicense;
        private PictureBox pictureBoxLicense;
        private RichTextBox richTextBoxLicense;
        private ComboBox comboBoxCopyrights;
        private ComboBox comboBoxLicenses;
        private RichTextBox richTextBoxDonation;
        private Button buttonCopyrightCopy;
        private Button buttonDonationCopy;
        private Button buttonLicenseCopy;
        private RichTextBox textBoxCopyright;
        private RichTextBox richTextBoxCopyrightDescription;
        private ToolTip toolTip;
        private PictureBox pictureBoxDonation;
        private RichTextBox textBoxVersion;
        private ComboBox comboBoxDonation;
        private ComboBoxImage comboBoxDonationCurrencies;
        private IContainer components;
        private RichTextBox richTextBoxCompany;
        private Label labelCompany;
        private Label labelVersion;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ContextMenuStrip contextMenuStripDonation;
        private ToolStripMenuItem toolStripMenuDonationSaveAs;
        private ToolStripMenuItem copyToolStripMenuItemDonation;
        private ContextMenuStrip contextMenuStripCopyToClipboard;
        private const string _stringQRTip =
        	"Right click to save donation address as QR code";
        private readonly string m_strSystemInfo =
        	Environment.SystemDirectory + "\\msinfo32.exe";
        private System.Windows.Forms.Label labelContactUs;
        private System.Windows.Forms.Label labelHomePage;
        private System.Windows.Forms.Label labelCopyrightComponent;
        private System.Windows.Forms.Label labelDonationComponent;
        private System.Windows.Forms.Label labelLicenseComponent;
        private System.Windows.Forms.Label labelDonationPayment;
        private System.Windows.Forms.Label labelDonationDescription;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel linkLabelURL;
        private System.Windows.Forms.LinkLabel linkLabelEmail;
        private System.Windows.Forms.TabPage tabPageCredits;
        private System.Windows.Forms.ComboBox comboBoxCreditComponent;
        private System.Windows.Forms.Button buttonCreditCopy;
        private ArdeshirV.Controls.ComboBoxImage comboBoxImageCreditNames;
        private System.Windows.Forms.PictureBox pictureBoxCredit;
        private System.Windows.Forms.RichTextBox richTextBoxCreditData;
        private System.Windows.Forms.Label labelCreditComponents;
        private System.Windows.Forms.Label labelCreditNames;

        #endregion
        //-------------------------------------------------------------------------------
        #region Constructor

        protected FormAbout(FormAboutData data) : base(data.Owner)
        {
        	s_blnIsExists = true;
            InitializeComponent();
            InitFormAbout(data);

            Form form = data.Owner as Form;
            if (form != null) {
            	Icon = form.Icon;
            	StartPosition = FormStartPosition.CenterParent;
				ShowDialog(form);
            } else {
            	StartPosition = FormStartPosition.CenterScreen;
				ShowDialog();
            }
        }

        #endregion
        //-------------------------------------------------------------------------------
        #region Utility functions

        protected void InitFormAbout(FormAboutData d) 
        {
            data = d;
            SetURL(linkLabelURL, d.URL);
            SetEmail(linkLabelEmail, d.Email);
            m_lblApplicationName.Text = d.AppName;
            Text = String.Format("About {0}", d.AppName);
            m_btnSysteminfo.Enabled = File.Exists(m_strSystemInfo);
            
            comboBoxDonation.Items.Clear();
            comboBoxDonationCurrencies.Items.Clear();
            foreach(string stringDonationTargetComponent in d.Donations.Keys)
            	comboBoxDonation.Items.Add(stringDonationTargetComponent);

            comboBoxLicenses.Items.Clear();
            foreach(string stringLicenseTargetComponent in d.Licenses.Keys)
            	comboBoxLicenses.Items.Add(stringLicenseTargetComponent);
            
            comboBoxCopyrights.Items.Clear();
            foreach(string stringCopyrightTargetComponent in d.Copyrights.Keys)
            	comboBoxCopyrights.Items.Add(stringCopyrightTargetComponent);
            
            comboBoxCreditComponent.Items.Clear();
            comboBoxImageCreditNames.Items.Clear();
            foreach(string stringCreditName in d.Credits.Keys)
            	comboBoxCreditComponent.Items.Add(stringCreditName);

            _ImageListCurrencies = new ImageList();
            _imageListCreditsAvators = new ImageList();
			comboBoxDonationCurrencies.Images = _ImageListCurrencies;
			comboBoxImageCreditNames.Images = _imageListCreditsAvators;
            comboBoxDonation.SelectedIndex = comboBoxDonation.Items.Count > 0? 0: -1;
            comboBoxLicenses.SelectedIndex = comboBoxLicenses.Items.Count > 0? 0: -1;
            comboBoxCopyrights.SelectedIndex = comboBoxCopyrights.Items.Count > 0? 0: -1;
            comboBoxCreditComponent.SelectedIndex =
            	comboBoxCreditComponent.Items.Count > 0? 0: -1;
            
            if(data.Copyrights.Count <= 0)
            	tabControl.TabPages.Remove(tabPageCopyright);
            
            if(data.Credits.Count <= 0)
            	tabControl.TabPages.Remove(tabPageCredits);
            
            if(data.Donations.Count <= 0)
            	tabControl.TabPages.Remove(tabPageDonation);
            
            if(data.Licenses.Count <= 0)
            	tabControl.TabPages.Remove(tabPageLicense);
        }
        //-------------------------------------------------------------------------------
        public static FormAbout Show(FormAboutData Data)
        {
        	return (s_blnIsExists)? null: new FormAbout(Data);
        }
        //-------------------------------------------------------------------------------
        private void UpdateTabControlBackColor(bool boolFormIsActive)
    	{
        	foreach(TabPage tab in tabControl.TabPages)
        		tab.BackColor = GetMidleColor(boolFormIsActive);
        }
        //-------------------------------------------------------------------------------
		private Color GetMidleColor(bool boolFormIsActive)
		{
        	Color c1, c2;

        	if(!boolFormIsActive) {
        		c2 = BackgoundEndGradientColor;
        		c1 = BackgoundStartGradientColor;
        	} else {
        		c2 = BackgoundInactiveEndGradientColor;
        		c1 = BackgoundInactiveStartGradientColor;
        	}
        	
    		Color color = Color.FromArgb(
    			(c1.A + c2.A) / 2,
    			(c1.R + c2.R) / 2,
    			(c1.G + c2.G) / 2,
    			(c1.B + c2.B) / 2
    		);
        	
        	return color;
		}
        //-------------------------------------------------------------------------------
		void ButtonCopyClick(object sender, EventArgs e)
		{
			Clipboard.SetText(textBoxCopyright.Text);
		}
        //-------------------------------------------------------------------------------
		void ButtonCreditCopyClick(object sender, EventArgs e)
		{
			Clipboard.SetText(richTextBoxCreditData.Text);
		}
		//-------------------------------------------------------------------------------
		void ButtonDonationCopyClick(object sender, EventArgs e)
		{
			Clipboard.SetText(richTextBoxDonation.Text);
		}
		//-------------------------------------------------------------------------------
		void ButtonLicenseCopyClick(object sender, EventArgs e)
		{
			Clipboard.SetText(richTextBoxLicense.Text);
		}
        //-------------------------------------------------------------------------------
        private static void ShowSystemInfoDialog()
        {
            string l_strSystemInfo = Environment.SystemDirectory + "\\msinfo32.exe";

            if (File.Exists(l_strSystemInfo))
                Process.Start(l_strSystemInfo);
            else
                throw new FileNotFoundException(string.Format(
            		"File {0} missing!", l_strSystemInfo));
        }
        //-------------------------------------------------------------------------------
		private void SetURL(LinkLabel linkLabelURL_, string URL)
		{
			if(!string.IsNullOrEmpty(URL)) {
            	linkLabelURL_.Text = URL;

            	string stringURL = Extractor.ExtractFirstURL(URL);
            	if(stringURL != string.Empty) {	            	
		            int intWebsiteIndex =
		            	URL.ToLower().IndexOf(stringURL.ToLower(),
		            	                      StringComparison.Ordinal);
	
		            if(intWebsiteIndex >= 0) {
		                int intPos = intWebsiteIndex;
		                linkLabelURL_.LinkArea = new LinkArea(
		                	intPos, stringURL.Length);
		            }
            	}
			} else
				labelHomePage.Visible =
				linkLabelURL_.Visible = false;
		}
		//-------------------------------------------------------------------------------
		private void SetEmail(LinkLabel linkLabelEmail_, string Email)
		{
			if(!string.IsNullOrEmpty(Email)) {
            	linkLabelEmail_.Text = Email;

            	string stringEmail = Extractor.ExtractFirstEmail(Email);
            	if(stringEmail != string.Empty) {	            	
		            int intEmailIndex = Email.ToLower().IndexOf(
	            		stringEmail.ToLower(), StringComparison.Ordinal);
	
		            if(intEmailIndex >= 0) {
		                int intPos = intEmailIndex;
		                linkLabelEmail_.LinkArea = new LinkArea(
		                	intPos, stringEmail.Length);
		            }
            	}
            } else
				labelContactUs.Visible =
				linkLabelEmail_.Visible = false;
		}
        //-------------------------------------------------------------------------------
		void FormAboutShown(object sender, EventArgs e)
		{
        	linkLabelEmail.Height =
        	linkLabelURL.Height = 16;
        	// Below code is neccesary for portability with Mono
        	ClientSize = new Size(634, 256);
		}
        //-------------------------------------------------------------------------------
        #region SetCopyrightText
        /*private void SetCopyrightText(LinkLabel linkLabel, string Copyright)
        {
        	// Finding and highlight the Email/URL in copyright text automatically
        	linkLabel.Text = Copyright;
        	string stringURL = Extractor.ExtractFirstURL(Copyright);
    		int intStartURLPos = Copyright.ToLower().IndexOf(
    			stringURL, StringComparison.Ordinal);
    		
    		if(stringURL != string.Empty && intStartURLPos >= 0) {
    			linkLabel.LinkArea =
    				new LinkArea(intStartURLPos, stringURL.Length);
    		} else {
        		string stringEmail = Extractor.ExtractFirstEmail(
    				textBoxCopyright.Text);
        		int intStartEmailPos = linkLabel.Text.ToLower().IndexOf(
        			stringEmail, StringComparison.Ordinal);
    		
    			if(stringEmail != string.Empty && intStartEmailPos >= 0) {
    				linkLabel.LinkArea =
    					new LinkArea(intStartEmailPos, stringEmail.Length);
    			}
    		}
        }
        //-------------------------------------------------------------------------------
        private void M_lblCopyright_LinkClicked(object sender,
        	LinkLabelLinkClickedEventArgs e)
        {
    		string stringURL = Extractor.ExtractFirstURL(textBoxCopyright.Text);
    		int intStartURLPos = textBoxCopyright.Text.ToLower().IndexOf(
    			stringURL.ToLower(), StringComparison.Ordinal);
    		
    		if(stringURL != string.Empty && intStartURLPos >= 0) {
    			//textBoxCopyright.LinkVisited = true;
    			System.Diagnostics.Process.Start(stringURL);
    		} else {
        		string stringEmail = Extractor.ExtractFirstEmail(
    				textBoxCopyright.Text);
        		int intStartEmailPos = textBoxCopyright.Text.ToLower().IndexOf(
    				stringEmail.ToLower(), StringComparison.Ordinal);
    		
    			if(stringEmail != string.Empty && intStartEmailPos >= 0) {
    				//textBoxCopyright.LinkVisited = true;
    				System.Diagnostics.Process.Start("mailto:" + stringEmail);
    			}
    		}
        }*/
        #endregion SetCopyrightText

        #endregion
        //-------------------------------------------------------------------------------
        #region Event Handlers

		void ComboBoxCopyrightsSelectedIndexChanged(object sender, EventArgs e)
		{
			if(comboBoxCopyrights.SelectedIndex >= 0) {
				string stringSelectedCopyright = (string)comboBoxCopyrights.Items[
					comboBoxCopyrights.SelectedIndex];
				if(data.Copyrights.ContainsKey(stringSelectedCopyright)) {
					Copyright copyright = data.Copyrights[stringSelectedCopyright];
					//SetCopyrightText(textBoxCopyright, copyright.Copyrights);
					if(string.IsNullOrEmpty(copyright.Copyrights)) {
						textBoxCopyright.Visible = false;
					} else {
						textBoxCopyright.Clear();
						textBoxCopyright.Text = copyright.Copyrights;
						textBoxCopyright.Visible = true;
					}
					
					if(string.IsNullOrEmpty(copyright.Version)) {
						textBoxVersion.Visible =
						labelVersion.Visible = false;
					} else {
						textBoxVersion.Clear();
						textBoxVersion.Text = copyright.Version;
						textBoxVersion.Visible =
						labelVersion.Visible = true;
					}
					
					if(string.IsNullOrEmpty(copyright.Company)) {
						richTextBoxCompany.Visible =
						labelCompany.Visible = false;
					} else {
						richTextBoxCompany.Clear();
						richTextBoxCompany.Text = copyright.Company;
						richTextBoxCompany.Visible =
						labelCompany.Visible = true;
					}

					if(string.IsNullOrEmpty(copyright.Description)) {
						richTextBoxCopyrightDescription.Visible = false;
					} else {
						richTextBoxCopyrightDescription.Clear();
						richTextBoxCopyrightDescription.Text = copyright.Description;
						richTextBoxCopyrightDescription.Visible = true;
					}
					
					if(copyright.Logo == null) {
						if(data.Owner != null && data.Owner is Form) {
							Form owner = data.Owner as Form;
							if(owner.Icon != null) {
								pictureBoxAppIcon.SizeMode = PictureBoxSizeMode.CenterImage;
								pictureBoxAppIcon.Image = owner.Icon.ToBitmap();
							} else {
								pictureBoxAppIcon.Visible = false;
							}
						}
					} else {
						pictureBoxAppIcon.SizeMode = PictureBoxSizeMode.StretchImage;
						pictureBoxAppIcon.Image = copyright.Logo;
					}
				}
			}
		}
        //-------------------------------------------------------------------------------
		void ComboBoxLicensesSelectedIndexChanged(object sender, EventArgs e)
		{
			if(comboBoxLicenses.SelectedIndex >= 0) {
				string stringSelectedLicense = (string)comboBoxLicenses.Items[
					comboBoxLicenses.SelectedIndex];
				if(data.Licenses.ContainsKey(stringSelectedLicense)) {
					License license = data.Licenses[stringSelectedLicense];
					richTextBoxLicense.Clear();
					if(string.IsNullOrEmpty(license.Text))
						return;
					richTextBoxLicense.Text = license.Text;
					if(license.Logo == null)
						pictureBoxLicense.Visible = false;
					else
						pictureBoxLicense.Image = license.Logo;
					
				}
			}
		}
        //-------------------------------------------------------------------------------
		void ComboBoxDonationsSelectedIndexChanged(object sender, EventArgs e)
		{
			if(comboBoxDonation.SelectedIndex >= 0) {
				string stringSelectedDonations = (string)comboBoxDonation.Items[
					comboBoxDonation.SelectedIndex];
				if(data.Donations.ContainsKey(stringSelectedDonations)) {
					Donation[] donations = data.Donations[stringSelectedDonations];
					comboBoxDonationCurrencies.Items.Clear();
					_ImageListCurrencies.Images.Clear();
					foreach(Donation d in donations) {
						comboBoxDonationCurrencies.Items.Add(d.Name);
						if(d.Logo != null)
							_ImageListCurrencies.Images.Add(d.Name, d.Logo);
					}
					if(comboBoxDonationCurrencies.Items.Count > 0)
						comboBoxDonationCurrencies.SelectedIndex = 0;
				}
			}
		}
        //-------------------------------------------------------------------------------
		void ComboBoxDonationCurrenciesSelectedIndexChanged(object sender, EventArgs e)
		{
			_donationSelected = null;
			toolTip.SetToolTip(pictureBoxDonation, string.Empty);
			if(comboBoxDonation.SelectedIndex >= 0) {
				string stringSelectedDonations = (string)comboBoxDonation.Items[
					comboBoxDonation.SelectedIndex];
				if(data.Donations.ContainsKey(stringSelectedDonations)) {
					Donation[] donations = data.Donations[stringSelectedDonations];
					if(comboBoxDonationCurrencies.SelectedIndex >= 0) {
						string stringSelectedDonation =
							(string)comboBoxDonationCurrencies.Items[
								comboBoxDonationCurrencies.SelectedIndex];
						foreach(Donation d in donations) {
							if(d.Name == stringSelectedDonation) {
								_donationSelected = d;
								richTextBoxDonation.Clear();
								richTextBoxDonation.Text = d.Address;
								pictureBoxDonation.Image = GetQRCodeImage(d.Address);
								toolTip.SetToolTip(pictureBoxDonation, _stringQRTip);
								break;
							}
						}
					}
				}
			}
		}
        //-------------------------------------------------------------------------------
		void ComboBoxImageCreditNamesSelectedIndexChanged(object sender, EventArgs e)
		{
			if(comboBoxCreditComponent.SelectedIndex >= 0) {
				string stringSelectedCreditComponent = (string)comboBoxCreditComponent.Items[
					comboBoxCreditComponent.SelectedIndex];
				if(data.Credits.ContainsKey(stringSelectedCreditComponent)) {
					Credit[] credits = data.Credits[stringSelectedCreditComponent];
					if(comboBoxImageCreditNames.SelectedIndex >= 0) {
						string stringSelectedCredit =
							(string)comboBoxImageCreditNames.Items[
								comboBoxImageCreditNames.SelectedIndex];
						foreach(Credit c in credits) {
							if(c.Name == stringSelectedCredit) {
								richTextBoxCreditData.Clear();
								richTextBoxCreditData.Text = c.Description;
								pictureBoxCredit.Image = c.Avator;
								break;
							}
						}
					}
				}
			}
		}
        //-------------------------------------------------------------------------------
		void ComboBoxCreditComponentSelectedIndexChanged(object sender, EventArgs e)
		{
			if(comboBoxCreditComponent.SelectedIndex >= 0) {
				string stringSelectedComponent = (string)comboBoxCreditComponent.Items[
					comboBoxCreditComponent.SelectedIndex];
				if(data.Credits.ContainsKey(stringSelectedComponent)) {
					Credit[] credits = data.Credits[stringSelectedComponent];
					comboBoxImageCreditNames.Items.Clear();
					_imageListCreditsAvators.Images.Clear();
					foreach(Credit c in credits) {
						comboBoxImageCreditNames.Items.Add(c.Name);
						if(c.Avator != null)
							_imageListCreditsAvators.Images.Add(c.Name, c.Avator);
					}
					if(comboBoxImageCreditNames.Items.Count > 0)
						comboBoxImageCreditNames.SelectedIndex = 0;
				}
			}
		}
        //-------------------------------------------------------------------------------
		private Image GetQRCodeImage(string Data)
		{
			return qr.QrCode.EncodeText(Data, qr.QrCode.Ecc.High).ToBitmap(4, 1);
		}
        //-------------------------------------------------------------------------------
        private void m_lnkMalieTo_LinkClicked(object sender,
        	LinkLabelLinkClickedEventArgs e)
        {
        	string stringEmail = Extractor.ExtractFirstEmail(data.Email);
        	if(stringEmail != string.Empty) {
	            (sender as LinkLabel).LinkVisited = true;
	            System.Diagnostics.Process.Start("mailto:" + stringEmail);
        	}
        }
        //-------------------------------------------------------------------------------
        private void m_lnkTechnicalSupport_LinkClicked(object sender,
        	LinkLabelLinkClickedEventArgs e)
        {
        	string stringURL = Extractor.ExtractFirstURL(data.URL);
        	if(stringURL != string.Empty) {
	            (sender as LinkLabel).LinkVisited = true;
	            System.Diagnostics.Process.Start(stringURL);
        	}
        }
        //-------------------------------------------------------------------------------
		void FormAboutDeactivate(object sender, EventArgs e)
		{
			UpdateTabControlBackColor(IsActivated);
		}
		//-------------------------------------------------------------------------------
		void FormAboutActivated(object sender, EventArgs e)
		{
			UpdateTabControlBackColor(IsActivated);	
		}
        //-------------------------------------------------------------------------------
		private void FormAboutOnBackgroundGradientColorChange(object sender, EventArgs e)
		{
			UpdateTabControlBackColor(IsActivated);
		}
        //-------------------------------------------------------------------------------
		void ToolStripMenuDonationSaveAsClick(object sender, EventArgs e)
		{
			if(pictureBoxDonation.Image != null && _donationSelected != null) {
				SaveFileDialog sfd = new SaveFileDialog();
				sfd.Title = "Save Donation address as QR Code";
				sfd.Filter = "PNG (*.png)|*.png";
				sfd.DefaultExt = "png";
				sfd.AddExtension = true;
				sfd.ValidateNames = true;
				sfd.OverwritePrompt = true;
				sfd.CheckPathExists = true;
				sfd.FileName = string.Format("{0} {1}",
                     _donationSelected.Name,
                     _donationSelected.Address).Replace(':', ' ');
				
				if(sfd.ShowDialog(this) == DialogResult.OK)
					pictureBoxDonation.Image.Save(sfd.FileName, ImageFormat.Png);
			}
		}
        //-------------------------------------------------------------------------------
		void CopyToolStripMenuItemDonationClick(object sender, EventArgs e)
		{
			if(pictureBoxDonation.Image != null && _donationSelected != null)
				Clipboard.SetImage(pictureBoxDonation.Image);
		}
        //-------------------------------------------------------------------------------
		void ContextMenuStripDonationOpening(object sender, CancelEventArgs e)
		{
			toolStripMenuDonationSaveAs.Enabled = 
			copyToolStripMenuItemDonation.Enabled =
				(pictureBoxDonation.Image != null && _donationSelected != null);
		}
        //-------------------------------------------------------------------------------
		void CopyToolStripMenuItemClick(object sender, EventArgs e)
		{
			RichTextBox rtb = ((ContextMenuStrip)(((ToolStripMenuItem)sender).
			                  Owner)).SourceControl as RichTextBox;
			if(rtb != null)
				if(rtb.SelectedText != string.Empty)
					Clipboard.SetText(rtb.SelectedText);
				else
					Clipboard.SetText(rtb.Text);
		}
        //-------------------------------------------------------------------------------
		void rtbLinkClick(object sender, LinkClickedEventArgs e)
		{
			Process.Start(e.LinkText);
		}
        //-------------------------------------------------------------------------------
        private void btnOk_Click(object sender, System.EventArgs e)
        {
        	DialogResult = DialogResult.OK;
        }
        //-------------------------------------------------------------------------------
        private void btnSysteminfo_Click(object sender, System.EventArgs e)
        {
            ShowSystemInfoDialog();
        }

        #endregion
        //-------------------------------------------------------------------------------
        #region overrided functions

        protected override void OnClosed(EventArgs e)
        {
            s_blnIsExists = false;
            base.OnClosed(e);
        }
        //-------------------------------------------------------------------------------
        protected override void OnShown(EventArgs e)
        {
			UpdateTabControlBackColor(false);
        	base.OnShown(e);
        }
        //-------------------------------------------------------------------------------
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (components != null)
                    components.Dispose();

            base.Dispose(disposing);
        }

        #endregion
        //-------------------------------------------------------------------------------
        #region Properties

        public static bool IsExists
        {
        	get {
        		return s_blnIsExists;
        	}
        }
        
        #endregion
        //-------------------------------------------------------------------------------
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        	this.components = new System.ComponentModel.Container();
        	this.pictureBoxAppIcon = new System.Windows.Forms.PictureBox();
        	this.contextMenuStripDonation = new System.Windows.Forms.ContextMenuStrip(this.components);
        	this.copyToolStripMenuItemDonation = new System.Windows.Forms.ToolStripMenuItem();
        	this.toolStripMenuDonationSaveAs = new System.Windows.Forms.ToolStripMenuItem();
        	this.m_btnOk = new System.Windows.Forms.Button();
        	this.m_btnSysteminfo = new System.Windows.Forms.Button();
        	this.m_lblApplicationName = new System.Windows.Forms.Label();
        	this.tabControl = new System.Windows.Forms.TabControl();
        	this.tabPageCopyright = new System.Windows.Forms.TabPage();
        	this.comboBoxCopyrights = new System.Windows.Forms.ComboBox();
        	this.textBoxCopyright = new System.Windows.Forms.RichTextBox();
        	this.contextMenuStripCopyToClipboard = new System.Windows.Forms.ContextMenuStrip(this.components);
        	this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        	this.richTextBoxCompany = new System.Windows.Forms.RichTextBox();
        	this.textBoxVersion = new System.Windows.Forms.RichTextBox();
        	this.buttonCopyrightCopy = new System.Windows.Forms.Button();
        	this.richTextBoxCopyrightDescription = new System.Windows.Forms.RichTextBox();
        	this.labelCopyrightComponent = new System.Windows.Forms.Label();
        	this.labelCompany = new System.Windows.Forms.Label();
        	this.labelVersion = new System.Windows.Forms.Label();
        	this.tabPageCredits = new System.Windows.Forms.TabPage();
        	this.comboBoxCreditComponent = new System.Windows.Forms.ComboBox();
        	this.buttonCreditCopy = new System.Windows.Forms.Button();
        	this.comboBoxImageCreditNames = new ArdeshirV.Controls.ComboBoxImage();
        	this.pictureBoxCredit = new System.Windows.Forms.PictureBox();
        	this.richTextBoxCreditData = new System.Windows.Forms.RichTextBox();
        	this.labelCreditComponents = new System.Windows.Forms.Label();
        	this.labelCreditNames = new System.Windows.Forms.Label();
        	this.tabPageDonation = new System.Windows.Forms.TabPage();
        	this.labelDonationDescription = new System.Windows.Forms.Label();
        	this.comboBoxDonation = new System.Windows.Forms.ComboBox();
        	this.buttonDonationCopy = new System.Windows.Forms.Button();
        	this.comboBoxDonationCurrencies = new ArdeshirV.Controls.ComboBoxImage();
        	this.pictureBoxDonation = new System.Windows.Forms.PictureBox();
        	this.richTextBoxDonation = new System.Windows.Forms.RichTextBox();
        	this.labelDonationComponent = new System.Windows.Forms.Label();
        	this.labelDonationPayment = new System.Windows.Forms.Label();
        	this.tabPageLicense = new System.Windows.Forms.TabPage();
        	this.comboBoxLicenses = new System.Windows.Forms.ComboBox();
        	this.buttonLicenseCopy = new System.Windows.Forms.Button();
        	this.pictureBoxLicense = new System.Windows.Forms.PictureBox();
        	this.richTextBoxLicense = new System.Windows.Forms.RichTextBox();
        	this.labelLicenseComponent = new System.Windows.Forms.Label();
        	this.toolTip = new System.Windows.Forms.ToolTip(this.components);
        	this.labelContactUs = new System.Windows.Forms.Label();
        	this.labelHomePage = new System.Windows.Forms.Label();
        	this.panel1 = new System.Windows.Forms.Panel();
        	this.linkLabelURL = new System.Windows.Forms.LinkLabel();
        	this.linkLabelEmail = new System.Windows.Forms.LinkLabel();
        	((System.ComponentModel.ISupportInitialize)(this.pictureBoxAppIcon)).BeginInit();
        	this.contextMenuStripDonation.SuspendLayout();
        	this.tabControl.SuspendLayout();
        	this.tabPageCopyright.SuspendLayout();
        	this.contextMenuStripCopyToClipboard.SuspendLayout();
        	this.tabPageCredits.SuspendLayout();
        	((System.ComponentModel.ISupportInitialize)(this.pictureBoxCredit)).BeginInit();
        	this.tabPageDonation.SuspendLayout();
        	((System.ComponentModel.ISupportInitialize)(this.pictureBoxDonation)).BeginInit();
        	this.tabPageLicense.SuspendLayout();
        	((System.ComponentModel.ISupportInitialize)(this.pictureBoxLicense)).BeginInit();
        	this.panel1.SuspendLayout();
        	this.SuspendLayout();
        	// 
        	// pictureBoxAppIcon
        	// 
        	this.pictureBoxAppIcon.BackColor = System.Drawing.Color.White;
        	this.pictureBoxAppIcon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        	this.pictureBoxAppIcon.InitialImage = null;
        	this.pictureBoxAppIcon.Location = new System.Drawing.Point(6, 6);
        	this.pictureBoxAppIcon.Name = "pictureBoxAppIcon";
        	this.pictureBoxAppIcon.Size = new System.Drawing.Size(128, 128);
        	this.pictureBoxAppIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        	this.pictureBoxAppIcon.TabIndex = 6;
        	this.pictureBoxAppIcon.TabStop = false;
        	// 
        	// contextMenuStripDonation
        	// 
        	this.contextMenuStripDonation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.copyToolStripMenuItemDonation,
			this.toolStripMenuDonationSaveAs});
        	this.contextMenuStripDonation.Name = "contextMenuStripDonation";
        	this.contextMenuStripDonation.Size = new System.Drawing.Size(179, 48);
        	this.contextMenuStripDonation.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStripDonationOpening);
        	// 
        	// copyToolStripMenuItemDonation
        	// 
        	this.copyToolStripMenuItemDonation.Image = global::ArdeshirV.Forms.Properties.Resources.Copy;
        	this.copyToolStripMenuItemDonation.Name = "copyToolStripMenuItemDonation";
        	this.copyToolStripMenuItemDonation.Size = new System.Drawing.Size(178, 22);
        	this.copyToolStripMenuItemDonation.Text = "&Copy QR Image";
        	this.copyToolStripMenuItemDonation.Click += new System.EventHandler(this.CopyToolStripMenuItemDonationClick);
        	// 
        	// toolStripMenuDonationSaveAs
        	// 
        	this.toolStripMenuDonationSaveAs.Image = global::ArdeshirV.Forms.Properties.Resources.Save_Picture;
        	this.toolStripMenuDonationSaveAs.Name = "toolStripMenuDonationSaveAs";
        	this.toolStripMenuDonationSaveAs.Size = new System.Drawing.Size(178, 22);
        	this.toolStripMenuDonationSaveAs.Text = "Save QR &As Image...";
        	this.toolStripMenuDonationSaveAs.Click += new System.EventHandler(this.ToolStripMenuDonationSaveAsClick);
        	// 
        	// m_btnOk
        	// 
        	this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        	this.m_btnOk.BackColor = System.Drawing.Color.Transparent;
        	this.m_btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
        	this.m_btnOk.Location = new System.Drawing.Point(542, 9);
        	this.m_btnOk.Name = "m_btnOk";
        	this.m_btnOk.Size = new System.Drawing.Size(80, 25);
        	this.m_btnOk.TabIndex = 5;
        	this.m_btnOk.Text = "&OK";
        	this.m_btnOk.UseVisualStyleBackColor = false;
        	this.m_btnOk.Click += new System.EventHandler(this.btnOk_Click);
        	// 
        	// m_btnSysteminfo
        	// 
        	this.m_btnSysteminfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        	this.m_btnSysteminfo.BackColor = System.Drawing.Color.Transparent;
        	this.m_btnSysteminfo.Location = new System.Drawing.Point(456, 9);
        	this.m_btnSysteminfo.Name = "m_btnSysteminfo";
        	this.m_btnSysteminfo.Size = new System.Drawing.Size(80, 25);
        	this.m_btnSysteminfo.TabIndex = 4;
        	this.m_btnSysteminfo.Text = "&System Info";
        	this.toolTip.SetToolTip(this.m_btnSysteminfo, "Shows information about current system");
        	this.m_btnSysteminfo.UseVisualStyleBackColor = false;
        	this.m_btnSysteminfo.Click += new System.EventHandler(this.btnSysteminfo_Click);
        	// 
        	// m_lblApplicationName
        	// 
        	this.m_lblApplicationName.BackColor = System.Drawing.Color.Transparent;
        	this.m_lblApplicationName.Dock = System.Windows.Forms.DockStyle.Top;
        	this.m_lblApplicationName.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.m_lblApplicationName.Location = new System.Drawing.Point(0, 0);
        	this.m_lblApplicationName.Name = "m_lblApplicationName";
        	this.m_lblApplicationName.Size = new System.Drawing.Size(634, 39);
        	this.m_lblApplicationName.TabIndex = 0;
        	this.m_lblApplicationName.Text = "App Name";
        	this.m_lblApplicationName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        	// 
        	// tabControl
        	// 
        	this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
        	this.tabControl.Controls.Add(this.tabPageCopyright);
        	this.tabControl.Controls.Add(this.tabPageCredits);
        	this.tabControl.Controls.Add(this.tabPageDonation);
        	this.tabControl.Controls.Add(this.tabPageLicense);
        	this.tabControl.Location = new System.Drawing.Point(12, 42);
        	this.tabControl.Name = "tabControl";
        	this.tabControl.SelectedIndex = 0;
        	this.tabControl.Size = new System.Drawing.Size(610, 166);
        	this.tabControl.TabIndex = 1;
        	// 
        	// tabPageCopyright
        	// 
        	this.tabPageCopyright.BackColor = System.Drawing.Color.Transparent;
        	this.tabPageCopyright.Controls.Add(this.comboBoxCopyrights);
        	this.tabPageCopyright.Controls.Add(this.textBoxCopyright);
        	this.tabPageCopyright.Controls.Add(this.richTextBoxCompany);
        	this.tabPageCopyright.Controls.Add(this.textBoxVersion);
        	this.tabPageCopyright.Controls.Add(this.buttonCopyrightCopy);
        	this.tabPageCopyright.Controls.Add(this.pictureBoxAppIcon);
        	this.tabPageCopyright.Controls.Add(this.richTextBoxCopyrightDescription);
        	this.tabPageCopyright.Controls.Add(this.labelCopyrightComponent);
        	this.tabPageCopyright.Controls.Add(this.labelCompany);
        	this.tabPageCopyright.Controls.Add(this.labelVersion);
        	this.tabPageCopyright.Location = new System.Drawing.Point(4, 22);
        	this.tabPageCopyright.Name = "tabPageCopyright";
        	this.tabPageCopyright.Padding = new System.Windows.Forms.Padding(3);
        	this.tabPageCopyright.Size = new System.Drawing.Size(602, 140);
        	this.tabPageCopyright.TabIndex = 0;
        	this.tabPageCopyright.Text = "   Copyright   ";
        	// 
        	// comboBoxCopyrights
        	// 
        	this.comboBoxCopyrights.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
        	this.comboBoxCopyrights.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.comboBoxCopyrights.FormattingEnabled = true;
        	this.comboBoxCopyrights.Items.AddRange(new object[] {
			"Component\'s Name"});
        	this.comboBoxCopyrights.Location = new System.Drawing.Point(215, 6);
        	this.comboBoxCopyrights.Name = "comboBoxCopyrights";
        	this.comboBoxCopyrights.Size = new System.Drawing.Size(305, 21);
        	this.comboBoxCopyrights.TabIndex = 1;
        	this.comboBoxCopyrights.SelectedIndexChanged += new System.EventHandler(this.ComboBoxCopyrightsSelectedIndexChanged);
        	// 
        	// textBoxCopyright
        	// 
        	this.textBoxCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
        	this.textBoxCopyright.BackColor = System.Drawing.SystemColors.Control;
        	this.textBoxCopyright.ContextMenuStrip = this.contextMenuStripCopyToClipboard;
        	this.textBoxCopyright.Location = new System.Drawing.Point(140, 33);
        	this.textBoxCopyright.Name = "textBoxCopyright";
        	this.textBoxCopyright.ReadOnly = true;
        	this.textBoxCopyright.Size = new System.Drawing.Size(456, 20);
        	this.textBoxCopyright.TabIndex = 3;
        	this.textBoxCopyright.Text = "";
        	this.textBoxCopyright.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbLinkClick);
        	// 
        	// contextMenuStripCopyToClipboard
        	// 
        	this.contextMenuStripCopyToClipboard.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.copyToolStripMenuItem});
        	this.contextMenuStripCopyToClipboard.Name = "contextMenuStripDonation";
        	this.contextMenuStripCopyToClipboard.Size = new System.Drawing.Size(103, 26);
        	// 
        	// copyToolStripMenuItem
        	// 
        	this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
        	this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
        	this.copyToolStripMenuItem.Text = "&Copy";
        	this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItemClick);
        	// 
        	// richTextBoxCompany
        	// 
        	this.richTextBoxCompany.BackColor = System.Drawing.SystemColors.Control;
        	this.richTextBoxCompany.ContextMenuStrip = this.contextMenuStripCopyToClipboard;
        	this.richTextBoxCompany.Location = new System.Drawing.Point(215, 59);
        	this.richTextBoxCompany.Name = "richTextBoxCompany";
        	this.richTextBoxCompany.ReadOnly = true;
        	this.richTextBoxCompany.Size = new System.Drawing.Size(168, 20);
        	this.richTextBoxCompany.TabIndex = 5;
        	this.richTextBoxCompany.Text = "";
        	this.richTextBoxCompany.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbLinkClick);
        	// 
        	// textBoxVersion
        	// 
        	this.textBoxVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.textBoxVersion.ContextMenuStrip = this.contextMenuStripCopyToClipboard;
        	this.textBoxVersion.Location = new System.Drawing.Point(462, 59);
        	this.textBoxVersion.Name = "textBoxVersion";
        	this.textBoxVersion.ReadOnly = true;
        	this.textBoxVersion.Size = new System.Drawing.Size(134, 20);
        	this.textBoxVersion.TabIndex = 7;
        	this.textBoxVersion.Text = "";
        	this.textBoxVersion.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbLinkClick);
        	// 
        	// buttonCopyrightCopy
        	// 
        	this.buttonCopyrightCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.buttonCopyrightCopy.BackColor = System.Drawing.Color.Transparent;
        	this.buttonCopyrightCopy.Location = new System.Drawing.Point(526, 6);
        	this.buttonCopyrightCopy.Name = "buttonCopyrightCopy";
        	this.buttonCopyrightCopy.Size = new System.Drawing.Size(70, 22);
        	this.buttonCopyrightCopy.TabIndex = 2;
        	this.buttonCopyrightCopy.Text = "&Copy";
        	this.toolTip.SetToolTip(this.buttonCopyrightCopy, "Copy selected copyright to clipboard");
        	this.buttonCopyrightCopy.UseVisualStyleBackColor = false;
        	this.buttonCopyrightCopy.Click += new System.EventHandler(this.ButtonCopyClick);
        	// 
        	// richTextBoxCopyrightDescription
        	// 
        	this.richTextBoxCopyrightDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
        	this.richTextBoxCopyrightDescription.BackColor = System.Drawing.SystemColors.Control;
        	this.richTextBoxCopyrightDescription.ContextMenuStrip = this.contextMenuStripCopyToClipboard;
        	this.richTextBoxCopyrightDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.richTextBoxCopyrightDescription.Location = new System.Drawing.Point(140, 85);
        	this.richTextBoxCopyrightDescription.Name = "richTextBoxCopyrightDescription";
        	this.richTextBoxCopyrightDescription.ReadOnly = true;
        	this.richTextBoxCopyrightDescription.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
        	this.richTextBoxCopyrightDescription.Size = new System.Drawing.Size(456, 49);
        	this.richTextBoxCopyrightDescription.TabIndex = 8;
        	this.richTextBoxCopyrightDescription.Text = "Description";
        	this.richTextBoxCopyrightDescription.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbLinkClick);
        	// 
        	// labelCopyrightComponent
        	// 
        	this.labelCopyrightComponent.Location = new System.Drawing.Point(140, 6);
        	this.labelCopyrightComponent.Name = "labelCopyrightComponent";
        	this.labelCopyrightComponent.Size = new System.Drawing.Size(74, 20);
        	this.labelCopyrightComponent.TabIndex = 0;
        	this.labelCopyrightComponent.Text = "Com&ponent:";
        	this.labelCopyrightComponent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        	// 
        	// labelCompany
        	// 
        	this.labelCompany.Location = new System.Drawing.Point(140, 58);
        	this.labelCompany.Name = "labelCompany";
        	this.labelCompany.Size = new System.Drawing.Size(74, 20);
        	this.labelCompany.TabIndex = 4;
        	this.labelCompany.Text = "&Company:";
        	this.labelCompany.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        	// 
        	// labelVersion
        	// 
        	this.labelVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.labelVersion.Location = new System.Drawing.Point(388, 58);
        	this.labelVersion.Name = "labelVersion";
        	this.labelVersion.Size = new System.Drawing.Size(73, 20);
        	this.labelVersion.TabIndex = 6;
        	this.labelVersion.Text = "&Version:";
        	this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        	// 
        	// tabPageCredits
        	// 
        	this.tabPageCredits.Controls.Add(this.comboBoxCreditComponent);
        	this.tabPageCredits.Controls.Add(this.buttonCreditCopy);
        	this.tabPageCredits.Controls.Add(this.comboBoxImageCreditNames);
        	this.tabPageCredits.Controls.Add(this.pictureBoxCredit);
        	this.tabPageCredits.Controls.Add(this.richTextBoxCreditData);
        	this.tabPageCredits.Controls.Add(this.labelCreditComponents);
        	this.tabPageCredits.Controls.Add(this.labelCreditNames);
        	this.tabPageCredits.Location = new System.Drawing.Point(4, 22);
        	this.tabPageCredits.Name = "tabPageCredits";
        	this.tabPageCredits.Size = new System.Drawing.Size(602, 140);
        	this.tabPageCredits.TabIndex = 3;
        	this.tabPageCredits.Text = "   Credits  ";
        	this.tabPageCredits.UseVisualStyleBackColor = true;
        	// 
        	// comboBoxCreditComponent
        	// 
        	this.comboBoxCreditComponent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
        	this.comboBoxCreditComponent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.comboBoxCreditComponent.FormattingEnabled = true;
        	this.comboBoxCreditComponent.Items.AddRange(new object[] {
			"Component\'s Name"});
        	this.comboBoxCreditComponent.Location = new System.Drawing.Point(215, 6);
        	this.comboBoxCreditComponent.Name = "comboBoxCreditComponent";
        	this.comboBoxCreditComponent.Size = new System.Drawing.Size(381, 21);
        	this.comboBoxCreditComponent.TabIndex = 14;
        	this.comboBoxCreditComponent.SelectedIndexChanged += new System.EventHandler(this.ComboBoxCreditComponentSelectedIndexChanged);
        	// 
        	// buttonCreditCopy
        	// 
        	this.buttonCreditCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.buttonCreditCopy.BackColor = System.Drawing.Color.Transparent;
        	this.buttonCreditCopy.Location = new System.Drawing.Point(526, 33);
        	this.buttonCreditCopy.Name = "buttonCreditCopy";
        	this.buttonCreditCopy.Size = new System.Drawing.Size(70, 22);
        	this.buttonCreditCopy.TabIndex = 17;
        	this.buttonCreditCopy.Text = "&Copy";
        	this.toolTip.SetToolTip(this.buttonCreditCopy, "Copy selected credit information to clipboard");
        	this.buttonCreditCopy.UseVisualStyleBackColor = false;
        	this.buttonCreditCopy.Click += new System.EventHandler(this.ButtonCreditCopyClick);
        	// 
        	// comboBoxImageCreditNames
        	// 
        	this.comboBoxImageCreditNames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
        	this.comboBoxImageCreditNames.BackColor = System.Drawing.SystemColors.Control;
        	this.comboBoxImageCreditNames.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
        	this.comboBoxImageCreditNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.comboBoxImageCreditNames.FormattingEnabled = true;
        	this.comboBoxImageCreditNames.Images = null;
        	this.comboBoxImageCreditNames.Items.AddRange(new object[] {
			"Bitcoin"});
        	this.comboBoxImageCreditNames.Location = new System.Drawing.Point(215, 33);
        	this.comboBoxImageCreditNames.Name = "comboBoxImageCreditNames";
        	this.comboBoxImageCreditNames.Size = new System.Drawing.Size(305, 21);
        	this.comboBoxImageCreditNames.TabIndex = 16;
        	this.comboBoxImageCreditNames.SelectedIndexChanged += new System.EventHandler(this.ComboBoxImageCreditNamesSelectedIndexChanged);
        	// 
        	// pictureBoxCredit
        	// 
        	this.pictureBoxCredit.BackColor = System.Drawing.Color.White;
        	this.pictureBoxCredit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        	this.pictureBoxCredit.ContextMenuStrip = this.contextMenuStripDonation;
        	this.pictureBoxCredit.InitialImage = null;
        	this.pictureBoxCredit.Location = new System.Drawing.Point(6, 6);
        	this.pictureBoxCredit.Name = "pictureBoxCredit";
        	this.pictureBoxCredit.Size = new System.Drawing.Size(128, 128);
        	this.pictureBoxCredit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        	this.pictureBoxCredit.TabIndex = 19;
        	this.pictureBoxCredit.TabStop = false;
        	// 
        	// richTextBoxCreditData
        	// 
        	this.richTextBoxCreditData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
        	this.richTextBoxCreditData.BackColor = System.Drawing.SystemColors.Control;
        	this.richTextBoxCreditData.ContextMenuStrip = this.contextMenuStripCopyToClipboard;
        	this.richTextBoxCreditData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.richTextBoxCreditData.Location = new System.Drawing.Point(140, 60);
        	this.richTextBoxCreditData.Name = "richTextBoxCreditData";
        	this.richTextBoxCreditData.ReadOnly = true;
        	this.richTextBoxCreditData.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
        	this.richTextBoxCreditData.Size = new System.Drawing.Size(456, 74);
        	this.richTextBoxCreditData.TabIndex = 18;
        	this.richTextBoxCreditData.Text = "Credits data goes here";
        	this.richTextBoxCreditData.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbLinkClick);
        	// 
        	// labelCreditComponents
        	// 
        	this.labelCreditComponents.Location = new System.Drawing.Point(140, 6);
        	this.labelCreditComponents.Name = "labelCreditComponents";
        	this.labelCreditComponents.Size = new System.Drawing.Size(74, 20);
        	this.labelCreditComponents.TabIndex = 13;
        	this.labelCreditComponents.Text = "Com&ponent:";
        	this.labelCreditComponents.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        	// 
        	// labelCreditNames
        	// 
        	this.labelCreditNames.Location = new System.Drawing.Point(140, 33);
        	this.labelCreditNames.Name = "labelCreditNames";
        	this.labelCreditNames.Size = new System.Drawing.Size(74, 20);
        	this.labelCreditNames.TabIndex = 15;
        	this.labelCreditNames.Text = "&Name:";
        	this.labelCreditNames.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        	// 
        	// tabPageDonation
        	// 
        	this.tabPageDonation.BackColor = System.Drawing.Color.Transparent;
        	this.tabPageDonation.Controls.Add(this.labelDonationDescription);
        	this.tabPageDonation.Controls.Add(this.comboBoxDonation);
        	this.tabPageDonation.Controls.Add(this.buttonDonationCopy);
        	this.tabPageDonation.Controls.Add(this.comboBoxDonationCurrencies);
        	this.tabPageDonation.Controls.Add(this.pictureBoxDonation);
        	this.tabPageDonation.Controls.Add(this.richTextBoxDonation);
        	this.tabPageDonation.Controls.Add(this.labelDonationComponent);
        	this.tabPageDonation.Controls.Add(this.labelDonationPayment);
        	this.tabPageDonation.Location = new System.Drawing.Point(4, 22);
        	this.tabPageDonation.Name = "tabPageDonation";
        	this.tabPageDonation.Padding = new System.Windows.Forms.Padding(3);
        	this.tabPageDonation.Size = new System.Drawing.Size(602, 140);
        	this.tabPageDonation.TabIndex = 1;
        	this.tabPageDonation.Text = "   Donation   ";
        	// 
        	// labelDonationDescription
        	// 
        	this.labelDonationDescription.Location = new System.Drawing.Point(139, 59);
        	this.labelDonationDescription.Name = "labelDonationDescription";
        	this.labelDonationDescription.Size = new System.Drawing.Size(459, 20);
        	this.labelDonationDescription.TabIndex = 5;
        	this.labelDonationDescription.Text = "If you find this app useful and if you want to support it; You can send your dona" +
	"tions here:";
        	this.labelDonationDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        	// 
        	// comboBoxDonation
        	// 
        	this.comboBoxDonation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
        	this.comboBoxDonation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.comboBoxDonation.FormattingEnabled = true;
        	this.comboBoxDonation.Items.AddRange(new object[] {
			"Component\'s Name"});
        	this.comboBoxDonation.Location = new System.Drawing.Point(215, 6);
        	this.comboBoxDonation.Name = "comboBoxDonation";
        	this.comboBoxDonation.Size = new System.Drawing.Size(381, 21);
        	this.comboBoxDonation.TabIndex = 1;
        	this.comboBoxDonation.SelectedIndexChanged += new System.EventHandler(this.ComboBoxDonationsSelectedIndexChanged);
        	// 
        	// buttonDonationCopy
        	// 
        	this.buttonDonationCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.buttonDonationCopy.BackColor = System.Drawing.Color.Transparent;
        	this.buttonDonationCopy.Location = new System.Drawing.Point(526, 33);
        	this.buttonDonationCopy.Name = "buttonDonationCopy";
        	this.buttonDonationCopy.Size = new System.Drawing.Size(70, 22);
        	this.buttonDonationCopy.TabIndex = 4;
        	this.buttonDonationCopy.Text = "&Copy";
        	this.toolTip.SetToolTip(this.buttonDonationCopy, "Copy selected donation address to clipboard");
        	this.buttonDonationCopy.UseVisualStyleBackColor = false;
        	this.buttonDonationCopy.Click += new System.EventHandler(this.ButtonDonationCopyClick);
        	// 
        	// comboBoxDonationCurrencies
        	// 
        	this.comboBoxDonationCurrencies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
        	this.comboBoxDonationCurrencies.BackColor = System.Drawing.SystemColors.Control;
        	this.comboBoxDonationCurrencies.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
        	this.comboBoxDonationCurrencies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.comboBoxDonationCurrencies.FormattingEnabled = true;
        	this.comboBoxDonationCurrencies.Images = null;
        	this.comboBoxDonationCurrencies.Items.AddRange(new object[] {
			"Bitcoin"});
        	this.comboBoxDonationCurrencies.Location = new System.Drawing.Point(215, 33);
        	this.comboBoxDonationCurrencies.Name = "comboBoxDonationCurrencies";
        	this.comboBoxDonationCurrencies.Size = new System.Drawing.Size(305, 21);
        	this.comboBoxDonationCurrencies.TabIndex = 3;
        	this.comboBoxDonationCurrencies.SelectedIndexChanged += new System.EventHandler(this.ComboBoxDonationCurrenciesSelectedIndexChanged);
        	// 
        	// pictureBoxDonation
        	// 
        	this.pictureBoxDonation.BackColor = System.Drawing.Color.White;
        	this.pictureBoxDonation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        	this.pictureBoxDonation.ContextMenuStrip = this.contextMenuStripDonation;
        	this.pictureBoxDonation.InitialImage = null;
        	this.pictureBoxDonation.Location = new System.Drawing.Point(6, 6);
        	this.pictureBoxDonation.Name = "pictureBoxDonation";
        	this.pictureBoxDonation.Size = new System.Drawing.Size(128, 128);
        	this.pictureBoxDonation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        	this.pictureBoxDonation.TabIndex = 12;
        	this.pictureBoxDonation.TabStop = false;
        	// 
        	// richTextBoxDonation
        	// 
        	this.richTextBoxDonation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
        	this.richTextBoxDonation.BackColor = System.Drawing.SystemColors.Control;
        	this.richTextBoxDonation.ContextMenuStrip = this.contextMenuStripCopyToClipboard;
        	this.richTextBoxDonation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.richTextBoxDonation.Location = new System.Drawing.Point(140, 80);
        	this.richTextBoxDonation.Name = "richTextBoxDonation";
        	this.richTextBoxDonation.ReadOnly = true;
        	this.richTextBoxDonation.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
        	this.richTextBoxDonation.Size = new System.Drawing.Size(456, 54);
        	this.richTextBoxDonation.TabIndex = 6;
        	this.richTextBoxDonation.Text = "1GtjrxH6t8om8KwHAHKpcG5SAwVSsm4PEi";
        	this.richTextBoxDonation.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbLinkClick);
        	// 
        	// labelDonationComponent
        	// 
        	this.labelDonationComponent.Location = new System.Drawing.Point(140, 6);
        	this.labelDonationComponent.Name = "labelDonationComponent";
        	this.labelDonationComponent.Size = new System.Drawing.Size(74, 20);
        	this.labelDonationComponent.TabIndex = 0;
        	this.labelDonationComponent.Text = "Com&ponent:";
        	this.labelDonationComponent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        	// 
        	// labelDonationPayment
        	// 
        	this.labelDonationPayment.Location = new System.Drawing.Point(140, 33);
        	this.labelDonationPayment.Name = "labelDonationPayment";
        	this.labelDonationPayment.Size = new System.Drawing.Size(74, 20);
        	this.labelDonationPayment.TabIndex = 2;
        	this.labelDonationPayment.Text = "&Currency:";
        	this.labelDonationPayment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        	// 
        	// tabPageLicense
        	// 
        	this.tabPageLicense.BackColor = System.Drawing.Color.Transparent;
        	this.tabPageLicense.Controls.Add(this.comboBoxLicenses);
        	this.tabPageLicense.Controls.Add(this.buttonLicenseCopy);
        	this.tabPageLicense.Controls.Add(this.pictureBoxLicense);
        	this.tabPageLicense.Controls.Add(this.richTextBoxLicense);
        	this.tabPageLicense.Controls.Add(this.labelLicenseComponent);
        	this.tabPageLicense.Location = new System.Drawing.Point(4, 22);
        	this.tabPageLicense.Name = "tabPageLicense";
        	this.tabPageLicense.Padding = new System.Windows.Forms.Padding(3);
        	this.tabPageLicense.Size = new System.Drawing.Size(602, 140);
        	this.tabPageLicense.TabIndex = 2;
        	this.tabPageLicense.Text = "   License   ";
        	// 
        	// comboBoxLicenses
        	// 
        	this.comboBoxLicenses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
        	this.comboBoxLicenses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.comboBoxLicenses.FormattingEnabled = true;
        	this.comboBoxLicenses.Items.AddRange(new object[] {
			"Component\'s Name"});
        	this.comboBoxLicenses.Location = new System.Drawing.Point(215, 6);
        	this.comboBoxLicenses.Name = "comboBoxLicenses";
        	this.comboBoxLicenses.Size = new System.Drawing.Size(305, 21);
        	this.comboBoxLicenses.TabIndex = 1;
        	this.comboBoxLicenses.SelectedIndexChanged += new System.EventHandler(this.ComboBoxLicensesSelectedIndexChanged);
        	// 
        	// buttonLicenseCopy
        	// 
        	this.buttonLicenseCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.buttonLicenseCopy.BackColor = System.Drawing.Color.Transparent;
        	this.buttonLicenseCopy.Location = new System.Drawing.Point(526, 6);
        	this.buttonLicenseCopy.Name = "buttonLicenseCopy";
        	this.buttonLicenseCopy.Size = new System.Drawing.Size(70, 22);
        	this.buttonLicenseCopy.TabIndex = 2;
        	this.buttonLicenseCopy.Text = "&Copy";
        	this.toolTip.SetToolTip(this.buttonLicenseCopy, "Copy selectet license to clipboard");
        	this.buttonLicenseCopy.UseVisualStyleBackColor = false;
        	this.buttonLicenseCopy.Click += new System.EventHandler(this.ButtonLicenseCopyClick);
        	// 
        	// pictureBoxLicense
        	// 
        	this.pictureBoxLicense.BackColor = System.Drawing.Color.White;
        	this.pictureBoxLicense.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        	this.pictureBoxLicense.InitialImage = null;
        	this.pictureBoxLicense.Location = new System.Drawing.Point(6, 6);
        	this.pictureBoxLicense.Name = "pictureBoxLicense";
        	this.pictureBoxLicense.Size = new System.Drawing.Size(128, 128);
        	this.pictureBoxLicense.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        	this.pictureBoxLicense.TabIndex = 8;
        	this.pictureBoxLicense.TabStop = false;
        	// 
        	// richTextBoxLicense
        	// 
        	this.richTextBoxLicense.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
        	this.richTextBoxLicense.BackColor = System.Drawing.SystemColors.Control;
        	this.richTextBoxLicense.ContextMenuStrip = this.contextMenuStripCopyToClipboard;
        	this.richTextBoxLicense.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.richTextBoxLicense.Location = new System.Drawing.Point(140, 33);
        	this.richTextBoxLicense.Name = "richTextBoxLicense";
        	this.richTextBoxLicense.ReadOnly = true;
        	this.richTextBoxLicense.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
        	this.richTextBoxLicense.Size = new System.Drawing.Size(456, 101);
        	this.richTextBoxLicense.TabIndex = 3;
        	this.richTextBoxLicense.Text = "License Details";
        	this.richTextBoxLicense.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbLinkClick);
        	// 
        	// labelLicenseComponent
        	// 
        	this.labelLicenseComponent.Location = new System.Drawing.Point(140, 6);
        	this.labelLicenseComponent.Name = "labelLicenseComponent";
        	this.labelLicenseComponent.Size = new System.Drawing.Size(74, 20);
        	this.labelLicenseComponent.TabIndex = 0;
        	this.labelLicenseComponent.Text = "Com&ponent:";
        	this.labelLicenseComponent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        	// 
        	// labelContactUs
        	// 
        	this.labelContactUs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        	this.labelContactUs.BackColor = System.Drawing.Color.Transparent;
        	this.labelContactUs.Location = new System.Drawing.Point(2, 3);
        	this.labelContactUs.Name = "labelContactUs";
        	this.labelContactUs.Size = new System.Drawing.Size(74, 19);
        	this.labelContactUs.TabIndex = 0;
        	this.labelContactUs.Text = "&Contact us:";
        	this.labelContactUs.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        	// 
        	// labelHomePage
        	// 
        	this.labelHomePage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        	this.labelHomePage.BackColor = System.Drawing.Color.Transparent;
        	this.labelHomePage.Location = new System.Drawing.Point(2, 22);
        	this.labelHomePage.Name = "labelHomePage";
        	this.labelHomePage.Size = new System.Drawing.Size(74, 19);
        	this.labelHomePage.TabIndex = 2;
        	this.labelHomePage.Text = "Home Page:";
        	this.labelHomePage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        	// 
        	// panel1
        	// 
        	this.panel1.BackColor = System.Drawing.Color.Transparent;
        	this.panel1.Controls.Add(this.labelContactUs);
        	this.panel1.Controls.Add(this.labelHomePage);
        	this.panel1.Controls.Add(this.linkLabelURL);
        	this.panel1.Controls.Add(this.linkLabelEmail);
        	this.panel1.Controls.Add(this.m_btnSysteminfo);
        	this.panel1.Controls.Add(this.m_btnOk);
        	this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
        	this.panel1.Location = new System.Drawing.Point(0, 210);
        	this.panel1.Name = "panel1";
        	this.panel1.Size = new System.Drawing.Size(634, 46);
        	this.panel1.TabIndex = 2;
        	// 
        	// linkLabelURL
        	// 
        	this.linkLabelURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
        	this.linkLabelURL.Location = new System.Drawing.Point(75, 25);
        	this.linkLabelURL.Name = "linkLabelURL";
        	this.linkLabelURL.Size = new System.Drawing.Size(379, 20);
        	this.linkLabelURL.TabIndex = 3;
        	this.linkLabelURL.TabStop = true;
        	this.linkLabelURL.Text = "Support web site goes here";
        	this.linkLabelURL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkTechnicalSupport_LinkClicked);
        	// 
        	// linkLabelEmail
        	// 
        	this.linkLabelEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
        	this.linkLabelEmail.Location = new System.Drawing.Point(75, 6);
        	this.linkLabelEmail.Name = "linkLabelEmail";
        	this.linkLabelEmail.Size = new System.Drawing.Size(379, 16);
        	this.linkLabelEmail.TabIndex = 1;
        	this.linkLabelEmail.TabStop = true;
        	this.linkLabelEmail.Text = "Email address goes here";
        	this.linkLabelEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkMalieTo_LinkClicked);
        	// 
        	// FormAbout
        	// 
        	this.ClientSize = new System.Drawing.Size(634, 256);
        	this.ControlBox = false;
        	this.Controls.Add(this.m_lblApplicationName);
        	this.Controls.Add(this.tabControl);
        	this.Controls.Add(this.panel1);
        	this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        	this.MaximizeBox = false;
        	this.MinimizeBox = false;
        	this.Name = "FormAbout";
        	this.ShowInTaskbar = false;
        	this.Text = "About";
        	this.OnBackgroundGradientColorChange += new System.EventHandler(this.FormAboutOnBackgroundGradientColorChange);
        	this.Activated += new System.EventHandler(this.FormAboutActivated);
        	this.Deactivate += new System.EventHandler(this.FormAboutDeactivate);
        	this.Shown += new System.EventHandler(this.FormAboutShown);
        	((System.ComponentModel.ISupportInitialize)(this.pictureBoxAppIcon)).EndInit();
        	this.contextMenuStripDonation.ResumeLayout(false);
        	this.tabControl.ResumeLayout(false);
        	this.tabPageCopyright.ResumeLayout(false);
        	this.contextMenuStripCopyToClipboard.ResumeLayout(false);
        	this.tabPageCredits.ResumeLayout(false);
        	((System.ComponentModel.ISupportInitialize)(this.pictureBoxCredit)).EndInit();
        	this.tabPageDonation.ResumeLayout(false);
        	((System.ComponentModel.ISupportInitialize)(this.pictureBoxDonation)).EndInit();
        	this.tabPageLicense.ResumeLayout(false);
        	((System.ComponentModel.ISupportInitialize)(this.pictureBoxLicense)).EndInit();
        	this.panel1.ResumeLayout(false);
        	this.ResumeLayout(false);

        }
        
        #endregion
    }
}
//---------------------------------------------------------------------------------------
