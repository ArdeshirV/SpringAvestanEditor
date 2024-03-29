#region Header

// //////////////////////////////////////////////////////////////////////////////////////
//                                                                                     //
//     Windows Standard Application                                                    //
//     Main Form.cs : Provides Main Form                                               //
//                                                                                     //
//-------------------------------------------------------------------------------------//
//                                                                                     //
//    Copyright© 2008-2020 ArdeshirV@protonmail.com, Licensed under GPLv3+             //
//                                                                                     //
// //////////////////////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Drawing;
using ArdeshirV.Forms;
using ArdeshirV.Utilities;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Collections.Generic;
using af=ArdeshirV.Forms.Properties;
using res= ArdeshirV.Applications.SpringAvestanEditor.Properties;

#endregion
//---------------------------------------------------------------------------------------
namespace ArdeshirV.Applications.SpringAvestanEditor
{
    public partial class frmMainForm : FormBase
    {
        #region Variables

        private const string m_strSupportMail = "ArdeshirV@protonmail.com";
        private const string m_strLink = "https://ardeshirv.github.io/SpringAvestanEditor/";

        private int l_intSize = 50;
        private bool m_blnDraw = true;
        private bool m_blnDirty = false;
        private Bitmap m_bmpScreen = null;
        private int m_intBlinkPosition = 1;
        private Button[] m_btnButtons = new Button[51];
        private string m_strFilePathName = string.Empty;
        private List<byte> m_lstArrCommands = new List<byte>(30);
        private readonly ImageFormat m_imfImageFormat = ImageFormat.Jpeg;
        private const string m_strImageFilter = 
@"Jpeg (*.jpg)|*.jpg|Bitmap (*.bmp)|*.bmp|Png (*.png)|*.png|Gif 
(*.gif)|*.gif|Memory Bitmap (*.memorybmp)|*.memorybmp";

        #endregion
        //-------------------------------------------------------------------------------
        #region Contructor

        public frmMainForm(string[] args)
        {
            InitializeComponent();

            m_btnButtons = new Button[]
            {
                m_btnNumber1,  m_btnNumber2,  m_btnNumber3,  m_btnNumber4,
                m_btnNumber5,  m_btnNumber6,  m_btnNumber7,  m_btnNumber8,
                m_btnNumber9,  m_btnNumber10, m_btnNumber11, m_btnNumber12,
                m_btnNumber13, m_btnNumber14, m_btnNumber15, m_btnNumber16,
                m_btnNumber17, m_btnNumber18, m_btnNumber19, m_btnNumber20,
                m_btnNumber21, m_btnNumber22, m_btnNumber23, m_btnNumber24,
                m_btnNumber25, m_btnNumber26, m_btnNumber27, m_btnNumber28,
                m_btnNumber29, m_btnNumber30, m_btnNumber31, m_btnNumber32,
                m_btnNumber33, m_btnNumber34, m_btnNumber35, m_btnNumber36,
                m_btnNumber37, m_btnNumber38, m_btnNumber39, m_btnNumber40,
                m_btnNumber41, m_btnNumber42, m_btnNumber43, m_btnNumber44,
                m_btnNumber45, m_btnNumber46, m_btnNumber47, m_btnNumber48,
                m_btnNumber49, m_btnNumber50, m_btnNumber51
            };

            UpdateScreen(false);
            Application.Idle += new EventHandler(Application_Idle);
            m_cmbImageFormats.Items.Add(System.Drawing.Imaging.ImageFormat.Jpeg);
            m_cmbImageFormats.Items.Add(System.Drawing.Imaging.ImageFormat.Bmp);
            m_cmbImageFormats.Items.Add(System.Drawing.Imaging.ImageFormat.Png);
            m_cmbImageFormats.Items.Add(System.Drawing.Imaging.ImageFormat.Gif);
            m_cmbImageFormats.Items.Add(System.Drawing.Imaging.ImageFormat.MemoryBmp);
            m_cmbImageFormats.SelectedIndex = 0;

			m_btnNew.KeyDown += this.frmMainForm_KeyDown;
			m_btnSave.KeyDown += this.frmMainForm_KeyDown;
			m_btnAbout.KeyDown += this.frmMainForm_KeyDown;
			m_btnOpenFile.KeyDown += this.frmMainForm_KeyDown;
            m_btnSaveImage.KeyDown += new KeyEventHandler(this.frmMainForm_KeyDown);

            if (args.Length > 0)
                AnalizeCommandLine(args);

            BackgoundStartGradientColor = Color.White;
            BackgoundInactiveStartGradientColor = Color.White;
            BackgoundInactiveEndGradientColor = Color.Gray;
            BackgoundEndGradientColor = Color.Lime;
        }

        #endregion
        //-------------------------------------------------------------------------------
        #region Overrided Functions

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.Invalidate();
            this.Focus();
        }
        //-------------------------------------------------------------------------------
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            int l_intIndexer = 0;
            m_trbScale_Scroll(null, null);

            foreach (Button btn in m_btnButtons) {
                btn.ImageIndex = l_intIndexer++;
                btn.Click += new EventHandler(this.OnButtonClick);
            }
            UpdateTitleBar();
        }

        #endregion
        //-------------------------------------------------------------------------------
        #region Interface Functions

        #endregion
        //-------------------------------------------------------------------------------
        #region Utility Functions

        private string GetFileName(string strFilePathName)
        {
        	FormMessage.Show(this, strFilePathName);
        	FileInfo info = new FileInfo(strFilePathName);
            return info.Directory + info.Name;;
        }
        //------------------------------------------------------------------------------- 
        private bool DoYouWantToSaveAndContinue()
        {
            bool l_blnResultValue = false;

            if (m_blnDirty)
            {
                DialogResult l_dlrResult = FormMessage.Show(
            		this, "Do you want to save changes?", Text,
            		MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                switch (l_dlrResult)
                {
                    case DialogResult.No:
                        l_blnResultValue = false;
                        break;
                    case DialogResult.Yes:
                        m_btnSave.PerformClick();
                        l_blnResultValue = false;
                        break;
                    case DialogResult.Cancel:
                        l_blnResultValue = true;
                        break;
                    default:
                        l_blnResultValue = false;
                        ShowError(new Exception("Unknown key in message box."));
                        break;
                }
            }

            return l_blnResultValue;
        }
        //-------------------------------------------------------------------------------
        private void ShowError(Exception exp)
        {
            FormErrorHandler.Show(this, exp, m_strLink);
        }
        //-------------------------------------------------------------------------------
        private void AnalizeCommandLine(string[] strArrCommandLine)
        {
            OpenDocument(strArrCommandLine[0]);
        }
        //-------------------------------------------------------------------------------
        private void UpdateScreen(bool blnDrawChars)
        {
            m_blnDraw = false;
            Size l_sizScreen = new Size((int)m_nudW.Value * l_intSize, (int)m_nudH.Value * l_intSize);
            m_bmpScreen = new Bitmap(l_sizScreen.Width, l_sizScreen.Height);
            m_pnlImage.Size = l_sizScreen;
            m_blnDraw = blnDrawChars;
            m_pnlImage.Invalidate();
            m_pnlImage.Select();
            m_blnDraw = true;
        }
        //-------------------------------------------------------------------------------
        private void UpdateTitleBar()
        {
            Text = "Spring Avestan Editor" + ((m_strFilePathName != string.Empty) ?
                " - " + m_strFilePathName : "") + ((m_blnDirty) ? "*" : "");
        }
        //-------------------------------------------------------------------------------
        private void SaveDocument(string strFilePathName)
        {
            try {
                m_blnDirty = false;
                m_strFilePathName = strFilePathName;
                File.Create(strFilePathName).Write(m_lstArrCommands.ToArray(), 0, m_lstArrCommands.Count);
                UpdateTitleBar();
            } catch (Exception exp) {
                ShowError(exp);
            }
        }
        //-------------------------------------------------------------------------------
        private void OpenDocument(string strFilePathName)
        {
            try {
                m_blnDirty = false;
                m_lstArrCommands.Clear();
                m_strFilePathName = strFilePathName;
                FileStream l_stmReader = File.Open(strFilePathName, FileMode.Open);
                byte[] l_bytArrTemp = new byte[l_stmReader.Length];
                l_stmReader.Read(l_bytArrTemp, 0, l_bytArrTemp.Length);
                m_intBlinkPosition = l_bytArrTemp.Length + 1;
                m_lstArrCommands.AddRange(l_bytArrTemp);
                UpdateScreen(true);
                UpdateTitleBar();
            } catch (Exception exp) {
                ShowError(exp);
            }
        }
        //-------------------------------------------------------------------------------
        private System.Drawing.Imaging.ImageFormat GetImageType(int intIndex)
        {
            switch (intIndex)
            {
                case 0:
                    return System.Drawing.Imaging.ImageFormat.Jpeg;
                case 1:
                    return System.Drawing.Imaging.ImageFormat.Bmp;
                case 2:
                    return System.Drawing.Imaging.ImageFormat.Png;
                case 3:
                    return System.Drawing.Imaging.ImageFormat.Gif;
                case 4:
                    return System.Drawing.Imaging.ImageFormat.MemoryBmp;
                case -1:
                default:
                    return null;
            }
        }

        #endregion
        //-------------------------------------------------------------------------------
        #region External Methods

        #endregion
        //-------------------------------------------------------------------------------
        #region Event Handlers

        private void Application_Idle(object sender, EventArgs e)
        {
        }
        //-------------------------------------------------------------------------------
        private void OnButtonClick(object sender, EventArgs e)
        {
            try
            {
                m_blnDirty = true;
                Button l_btnClicked = (Button)sender;
                AddCharacter(byte.Parse((string)l_btnClicked.Tag));
                UpdateScreen(true);
                UpdateTitleBar();
            } catch (Exception exp) {
                ShowError(exp);
            }
        }
        //-------------------------------------------------------------------------------
        private void AddCharacter(byte bytData)
        {
            if (m_lstArrCommands.Count == 0)
            {
                m_intBlinkPosition = 1;
                m_lstArrCommands.Add(bytData);
            }
            else if (m_intBlinkPosition > m_lstArrCommands.Count)
                m_lstArrCommands.Add(bytData);
            else if (m_lstArrCommands.Count >= 1)
                m_lstArrCommands.Insert(m_intBlinkPosition - 1, bytData);

            m_intBlinkPosition++;
        }
        //-------------------------------------------------------------------------------
        private void m_btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog l_sfdSave = new SaveFileDialog();
            l_sfdSave.AddExtension = true;
            l_sfdSave.Filter = "Avestan Files (*.avetan)|*.avestan";
            l_sfdSave.FilterIndex = 1;
            l_sfdSave.Title = "Save Avestan File";

            if (l_sfdSave.ShowDialog(this) == DialogResult.OK)
                SaveDocument(l_sfdSave.FileName);
        }
        //-------------------------------------------------------------------------------
        private void m_btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog l_sfdOpen = new OpenFileDialog();

            l_sfdOpen.Filter = "Avestan Files (*.avestan)|*.avestan";
            l_sfdOpen.Title = "Open Avestan File";
            l_sfdOpen.AddExtension = true;
            l_sfdOpen.FilterIndex = 1;

            if (l_sfdOpen.ShowDialog(this) == DialogResult.OK)
                OpenDocument(l_sfdOpen.FileName);
        }
        //-------------------------------------------------------------------------------
        private void m_btnSaveImage_Click(object sender, EventArgs e)
        {
            SaveFileDialog l_sfdSaveFile = new SaveFileDialog();

            l_sfdSaveFile.AddExtension = true;
            l_sfdSaveFile.Title = "Save Image";
            l_sfdSaveFile.Filter = m_strImageFilter;
            l_sfdSaveFile.FilterIndex = m_cmbImageFormats.SelectedIndex + 1;

            if (l_sfdSaveFile.ShowDialog(this) == DialogResult.OK)
                m_bmpScreen.Save(l_sfdSaveFile.FileName, GetImageType(l_sfdSaveFile.FilterIndex));
        }
        //-------------------------------------------------------------------------------
        private void m_pnlImage_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            frmMainForm_KeyDown(sender, new KeyEventArgs(e.KeyData));
        }
        //-------------------------------------------------------------------------------
        private void PnlImage_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                ImageList l_imlTemp = m_imlLarge;
                Point l_pntBlinkPosition = new Point(0, 0);
                Graphics l_grpOffScreen = Graphics.FromImage(m_bmpScreen);

                int l_intCounter = 1,
                    l_intTop = 0, l_intLeft = 0,
                    l_intColumnCount = (int)Math.Floor((double)(m_bmpScreen.Width / l_intSize));

                if (l_intSize > 30)
                    l_imlTemp = m_imlLarge;
                else
                    l_imlTemp = m_imlImages;

                l_grpOffScreen.FillRectangle(Brushes.White, 0, 0, m_bmpScreen.Width, m_bmpScreen.Height);

                for(int l_intIndexer = 0; l_intIndexer < m_lstArrCommands.Count; l_intIndexer++)
                {
                    l_intTop = 0;
                    l_intLeft = l_intCounter;

                    while (l_intLeft > l_intColumnCount)
                        l_intLeft -= l_intColumnCount;

                    while (l_intTop * l_intColumnCount < l_intCounter)
                        l_intTop++;

                    l_intTop = (l_intTop - 1) * l_intSize;
                    l_intLeft = m_bmpScreen.Width - (l_intLeft) * l_intSize;

                    if (m_lstArrCommands[l_intIndexer] < 53 && m_lstArrCommands[l_intIndexer] > 0 && m_blnDraw)
                        l_grpOffScreen.DrawImage(l_imlTemp.Images[m_lstArrCommands[l_intIndexer] - 1], l_intLeft, l_intTop, l_intSize, l_intSize);

                    if (l_intCounter == m_intBlinkPosition)
                        l_pntBlinkPosition = new Point(l_intLeft, l_intTop + l_intSize - 1);

                    if (l_intCounter++ > m_lstArrCommands.Count - 1)
                        break;
                }

                e.Graphics.DrawImageUnscaled(m_bmpScreen, 0, 0);

                l_intTop = 0;
                l_intLeft = l_intCounter;

                while (l_intLeft > l_intColumnCount)
                    l_intLeft -= l_intColumnCount;

                while (l_intTop * l_intColumnCount < l_intCounter)
                    l_intTop++;

                l_intTop = (l_intTop - 1) * l_intSize;
                l_intLeft = m_bmpScreen.Width - (l_intLeft) * l_intSize;

                if (l_intCounter == m_intBlinkPosition)
                    l_pntBlinkPosition = new Point(l_intLeft, l_intTop + l_intSize - 1);

                if (m_intBlinkPosition > 0)
                    e.Graphics.DrawLine(Pens.Blue, l_pntBlinkPosition.X, l_pntBlinkPosition.Y,
                        l_pntBlinkPosition.X + l_intSize, l_pntBlinkPosition.Y);
            } catch (Exception exp) {
                ShowError(exp);
            }
        }
        //-------------------------------------------------------------------------------
        private void m_trbScale_Scroll(object sender, EventArgs e)
        {
            l_intSize = (m_trbScale.Value) * 10;
            UpdateScreen(false);
        }
        //-------------------------------------------------------------------------------
        private void m_nudW_ValueChanged(object sender, EventArgs e)
        {
            UpdateScreen(false);
        }
        //-------------------------------------------------------------------------------
        private void m_pnlScreen_Scroll(object sender, ScrollEventArgs e)
        {
            m_pnlScreen.Invalidate(false);
        }
        //-------------------------------------------------------------------------------
        private void frmMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = DoYouWantToSaveAndContinue();
        }
        //-------------------------------------------------------------------------------
        private void frmMainForm_Activated(object sender, EventArgs e)
        {
            /*m_stsStatusBar.BackColor = */
            m_trbScale.BackColor = this.BackgoundEndGradientColor;
        }
        //-------------------------------------------------------------------------------
        private void frmMainForm_Deactivate(object sender, EventArgs e)
        {
            //m_txtOutput.Text = m_pnlImage.Size.ToString();
            /*m_stsStatusBar.BackColor = */
            m_trbScale.BackColor = this.BackgoundInactiveEndGradientColor;
        }
        //-------------------------------------------------------------------------------
        private void m_btnAbout_Click(object sender, EventArgs e)
        {
            AssemblyAttributeAccessors aaa = new AssemblyAttributeAccessors(this);
            string stringTitle = aaa.AssemblyTitle;
            FormAboutData formAboutData = new FormAboutData(
                this,
                new Copyright[] { new Copyright(this, res.Resources.SpringAvestanEditorLogo) },
                new Credits[] { new Credits(stringTitle,
                    new Credit[] {
                        new Credit(
                            "ArdeshirV",
@"ArdeshirV is 'Spring Avestan Editor' founder and developer.
Spring Avestan Editor: https://ardeshirv.github.io/SpringAvestanEditor/
Github: https://github.com/ArdeshirV/SpringAvestanEditor
Email: ArdeshirV@protonmail.com",
                            af.Resources.ArdeshirV)
                    }) },
                new Forms.License[] {
                    new Forms.License(stringTitle, res.Resources.LICENSE, res.Resources.GPLv3)
                },
                new Donations[] {
                    new Donations(stringTitle, DefaultDonationList.Items)
                },
                m_strLink,
                m_strSupportMail);
            FormAbout.Show(formAboutData);
        }
        //-------------------------------------------------------------------------------
        private void m_btnNew_Click(object sender, EventArgs e)
        {
            if (m_blnDirty)
            {
                DialogResult l_dlrResult = FormMessage.Show(
                    this, "Do you want to save changes?",
                    Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                switch (l_dlrResult)
                {
                    case DialogResult.No:
                        break;
                    case DialogResult.Yes:
                        m_btnSave.PerformClick();
                        break;
                    case DialogResult.Cancel:
                        return;
                    default:
                        ShowError(new Exception("Unknown key in message box found."));
                        return;
                }
            }

            m_lstArrCommands.Clear();
            m_intBlinkPosition = 1;
            m_strFilePathName = "";
            m_blnDirty = false;
            UpdateScreen(true);
            UpdateTitleBar();
        }
        //-------------------------------------------------------------------------------
        private void m_btnButtons_Click(object sender, EventArgs e)
        {
            m_pnlImage.Select();
        }
        //-------------------------------------------------------------------------------
        #region Key Analizer

        private void frmMainForm_KeyDown(object sender, KeyEventArgs e)
        {
            int l_intColumnCount = 0, l_intTemp;

            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;

                case Keys.F1:
                    m_btnAbout.PerformClick();
                    break;

                case Keys.Left:
                    if (m_lstArrCommands.Count >= m_intBlinkPosition)
                    {
                        ++m_intBlinkPosition;
                        //m_txtOutput.Text = m_intBlinkPosition.ToString();
                        UpdateScreen(false);
                    }
                    break;

                case Keys.Right:
                    if (m_intBlinkPosition > 1)
                    {
                        --m_intBlinkPosition;
                        UpdateScreen(false);
                    }
                    break;

                case Keys.Up:
                    l_intColumnCount = (int)Math.Floor((double)(m_bmpScreen.Width / l_intSize));
                    l_intTemp = m_intBlinkPosition - l_intColumnCount;

                    if (l_intTemp > 0)
                    {
                        m_intBlinkPosition = l_intTemp;
                        UpdateScreen(false);
                    }
                    break;

                case Keys.Down:
                    l_intColumnCount = (int)Math.Floor((double)(m_bmpScreen.Width / l_intSize));
                    l_intTemp = m_intBlinkPosition + l_intColumnCount;

                    if (l_intTemp <= m_lstArrCommands.Count + 1)
                    {
                        m_intBlinkPosition = l_intTemp;
                        UpdateScreen(false);
                    }
                    break;

                case Keys.A:
                    if (e.Shift)
                        m_btnNumber2.PerformClick();
                    else
                        m_btnNumber1.PerformClick();
                    break;

                case Keys.I:
                    if (e.Shift)
                        m_btnNumber4.PerformClick();
                    else
                        m_btnNumber3.PerformClick();
                    break;

                case Keys.O:
                    if (e.Shift)
                        m_btnNumber6.PerformClick();
                    else
                        m_btnNumber5.PerformClick();
                    break;

                case Keys.E:
                    if (e.Shift)
                        m_btnNumber8.PerformClick();
                    else
                        m_btnNumber7.PerformClick();
                    break;

                case Keys.U:
                    if (e.Shift)
                        m_btnNumber9.PerformClick();
                    else
                        m_btnNumber21.PerformClick();
                    break;

                case Keys.X:
                    if (e.Shift)
                        m_btnNumber11.PerformClick();
                    else
                        m_btnNumber10.PerformClick();
                    break;

                case Keys.Q:
                    if (e.Shift)
                        m_btnNumber13.PerformClick();
                    else
                        m_btnNumber12.PerformClick();
                    break;

                case Keys.B:
                    if (e.Shift)
                        m_btnNumber45.PerformClick();
                    else
                        m_btnNumber14.PerformClick();
                    break;

                case Keys.Z:
                    if (e.Shift)
                        m_btnNumber46.PerformClick();
                    else
                        m_btnNumber15.PerformClick();
                    break;

                case Keys.P:
                    if (e.Shift)
                        m_btnNumber16.PerformClick();
                    else
                        m_btnNumber26.PerformClick();
                    break;

                case Keys.C:
                    if (e.Shift)
                        m_btnNumber17.PerformClick();
                    else
                        m_btnNumber47.PerformClick();
                    break;

                case Keys.R:
                    if (e.Shift)
                        m_btnNumber18.PerformClick();
                    else
                        m_btnNumber29.PerformClick();
                    break;

                case Keys.Y:
                    if (e.Shift)
                        m_btnNumber19.PerformClick();
                    else
                        m_btnNumber20.PerformClick();
                    break;

                case Keys.J:
                    if (e.Shift)
                        m_btnNumber23.PerformClick();
                    else
                        m_btnNumber22.PerformClick();
                    break;

                case Keys.N:
                    if (e.Shift)
                        m_btnNumber24.PerformClick();
                    else
                        m_btnNumber39.PerformClick();
                    break;

                case Keys.W:
                    m_btnNumber25.PerformClick();
                    break;

                case Keys.V:
                    if (e.Shift)
                        m_btnNumber36.PerformClick();
                    else
                        m_btnNumber27.PerformClick();
                    break;

                case Keys.S:
                    if (e.Shift)
                        m_btnNumber48.PerformClick();
                    else
                        m_btnNumber28.PerformClick();
                    break;

                case Keys.K:
                    m_btnNumber30.PerformClick();
                    break;

                case Keys.D:
                    if (e.Shift)
                        m_btnNumber32.PerformClick();
                    else
                        m_btnNumber31.PerformClick();
                    break;

                case Keys.H:
                    if (e.Shift)
                        m_btnNumber34.PerformClick();
                    else
                        m_btnNumber33.PerformClick();
                    break;

                case Keys.G:
                    if (e.Shift)
                        m_btnNumber42.PerformClick();
                    else
                        m_btnNumber35.PerformClick();
                    break;

                case Keys.M:
                    if (e.Shift)
                        m_btnNumber37.PerformClick();
                    else
                        m_btnNumber38.PerformClick();
                    break;

                case Keys.T:
                    if (e.Shift)
                        m_btnNumber41.PerformClick();
                    else
                        m_btnNumber40.PerformClick();
                    break;

                case Keys.F:
                    if (e.Shift)
                        m_btnNumber44.PerformClick();
                    else
                        m_btnNumber43.PerformClick();
                    break;

                case Keys.L:
                    m_btnNumber49.PerformClick();
                    break;

                case Keys.Oem1:
                    m_btnNumber51.PerformClick();
                    break;

                case Keys.OemPeriod:
                    m_btnNumber50.PerformClick();
                    break;

                case Keys.Back:
                    try
                    {
                        if (m_lstArrCommands.Count >= 1 && m_intBlinkPosition > 1)
                        {
                            m_blnDirty = true;
                            m_lstArrCommands.RemoveAt(--m_intBlinkPosition - 1);

                            if (m_intBlinkPosition < 1)
                                m_intBlinkPosition = 1;

                            UpdateScreen(false);
                        }
                    }
                    catch (Exception exp)
                    {
                        ShowError(exp);
                    }
                    break;

                case Keys.Delete:
                    try
                    {
                        if (m_lstArrCommands.Count >= 1 && m_lstArrCommands.Count >= m_intBlinkPosition)
                        {
                            m_blnDirty = true;
                            m_lstArrCommands.RemoveAt(m_intBlinkPosition - 1);

                            if (m_intBlinkPosition > m_lstArrCommands.Count + 1)
                                m_intBlinkPosition = m_lstArrCommands.Count + 1;

                            UpdateScreen(false);
                        }
                    }
                    catch (Exception exp)
                    {
                        ShowError(exp);
                    }
                    break;

                case Keys.Space:
                    AddCharacter(52);
                    UpdateScreen(false);
                    break;

                case Keys.Enter:
                    l_intColumnCount = (int)Math.Floor((double)(m_bmpScreen.Width / l_intSize));
                    l_intTemp = m_intBlinkPosition % l_intColumnCount;

                    if (m_intBlinkPosition % l_intColumnCount > 0)
                    {
                        l_intTemp = l_intColumnCount - l_intTemp +1;

                        for (int l_intIndexer = 0; l_intIndexer < l_intTemp; l_intIndexer++)
                            AddCharacter(52);
                    }
                    else
                        AddCharacter(52);

                    UpdateScreen(false);
                    break;

                case Keys.Home:
                    if (e.Control)
                    {
                        m_intBlinkPosition = 1;
                        UpdateScreen(false);
                    }
                    {
                        l_intColumnCount = (int)Math.Floor((double)(m_bmpScreen.Width / l_intSize));
                        int l_intCurrentRow = (int)Math.Floor((double)(m_intBlinkPosition / l_intColumnCount));

                        if (m_intBlinkPosition % l_intColumnCount != 0)
                            m_intBlinkPosition = l_intCurrentRow * l_intColumnCount + 1;
                        else
                            m_intBlinkPosition = (l_intCurrentRow - 1) * l_intColumnCount + 1;

                        UpdateScreen(false);
                    }
                    break;

                case Keys.End:
                    if (e.Control)
                    {
                        m_intBlinkPosition = m_lstArrCommands.Count + 1;
                        UpdateScreen(false);
                    }
                    else
                    {
                        l_intColumnCount = (int)Math.Floor((double)(m_bmpScreen.Width / l_intSize));
                        int l_intCurrentRow = (int)Math.Floor((double)(m_intBlinkPosition / l_intColumnCount)) + 1;
                        int l_intResult = m_intBlinkPosition % l_intColumnCount;

                        if (l_intResult == 0)
                            break;

                        m_intBlinkPosition = l_intCurrentRow * l_intColumnCount;

                        if (m_intBlinkPosition > m_lstArrCommands.Count + 1)
                            m_intBlinkPosition = m_lstArrCommands.Count + 1;

                        UpdateScreen(false);
                    }
                    break;
            }
            UpdateTitleBar();
        }

        #endregion
        //-------------------------------------------------------------------------------
        private void m_btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
        //-------------------------------------------------------------------------------
        #region Properties

        #endregion
    }
}


