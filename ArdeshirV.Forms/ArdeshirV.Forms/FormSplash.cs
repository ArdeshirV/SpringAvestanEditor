#region Header

// Copyright© 2002-2020 ArdeshirV@protonmail.com, Licensed under LGPLv3+

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion
//---------------------------------------------------------------------------------------
namespace ArdeshirV.Forms
{
    public partial class FormSplash : FormBase
    {
    	/// <summary>
    	/// FormSplashProcess has been process loading app resources
    	/// and update progressbar below FormSplash
    	/// </summary>
    	/// <param name="pb">ProgressBar below the FormSplash
    	/// that demonstrat loading progress</param>
    	public delegate void FormSplashWndProcess(ProgressBar pb);
    	public delegate void FormSplashEnd();
        //-------------------------------------------------------------------------------    	
        private Timer m_timTimer;
        private Form _formOwner;
        private int intDelay;
        private FormSplashEnd formSplashEnd;
        private FormSplashWndProcess localFormSplashProcess;
        private readonly long intTicks = DateTime.Now.Ticks + 4;
        //-------------------------------------------------------------------------------
        protected FormSplash(IWin32Window windowParent, Image imgSplashImage,
                                      int Delay, FormSplashWndProcess fsp) :
        	base(windowParent)
        {
            InitializeComponent();
            if (!DesignMode)
            {
            	if(fsp == null) {
            		progressBar.Visible = false;
	            	Size = new Size(imgSplashImage.Size.Width,
	            	                imgSplashImage.Size.Height);
            	} else {
	            	localFormSplashProcess += fsp;
	            	Size = new Size(imgSplashImage.Size.Width,
	            	                imgSplashImage.Size.Height + progressBar.Height);
            	}
	            //FollowParentFormBase = true;
            	MoveFormWithMouse = false;
            	intDelay = Delay;
	            m_imgPictureBox.Image = imgSplashImage;
            	StartPosition = FormStartPosition.CenterScreen;
	            m_imgPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
	            
	            m_timTimer = new Timer();
	            m_timTimer.Interval = 200;
	            m_timTimer.Tick += new EventHandler(m_timTimer_Elapsed);
            	Show(_formOwner = windowParent as Form);
            }
        }
        //-------------------------------------------------------------------------------
        private void m_timTimer_Elapsed(object sender, EventArgs e)
        {
            if(localFormSplashProcess != null) {
            	localFormSplashProcess(progressBar);
            	Close();
            } else {
        		if(m_timTimer.Interval == intDelay)
        			Close();
        		else
        			m_timTimer.Interval = intDelay;      		
            }
        }
        //-------------------------------------------------------------------------------
        /// <summary>
        /// Create and show splash form 
        /// </summary>
        /// <param name="formOwner">Owner form</param>
        /// <param name="imgSplashImage">Image on the splash form</param>
        /// <returns>Reference to created splash form</returns>
        public static FormSplash Show(Form formOwner, Image imgSplashImage)
        {
        	return Show(formOwner, imgSplashImage, 3000);
        }
        //-------------------------------------------------------------------------------
        /// <summary>
        /// Create and show splash form 
        /// </summary>
        /// <param name="formOwner">Owner form</param>
        /// <param name="imgSplashImage">Image on the splash form</param>
        /// <param name="Delay">Delay time in miliseconds that splash form is present</param>
        /// <returns>Reference to created splash form</returns>
        public static FormSplash Show(Form formOwner, Image imgSplashImage, int Delay)
        {
            return new FormSplash(formOwner, imgSplashImage, Delay, null);
        }
        //-------------------------------------------------------------------------------
        /// <summary>
        /// Create and show splash form 
        /// </summary>
        /// <param name="formOwner">Owner form</param>
        /// <param name="imgSplashImage">Image on the splash form</param>
        /// <param name="Delay">Delay time in miliseconds that splash form is present</param>
        /// <returns>Reference to created splash form</returns>
        public static FormSplash Show(Form formOwner, Image imgSplashImage,
                                      int Delay, FormSplashWndProcess fsp)
        {
            return new FormSplash(formOwner, imgSplashImage, Delay, fsp);
        }
        //-------------------------------------------------------------------------------
        public ProgressBar Progress
        {
        	get {
        		return progressBar;
        	}
        }
        //-------------------------------------------------------------------------------
        private void M_imgPictureBox_Click(object sender, EventArgs e)
        {
            //Close();
        }
        //-------------------------------------------------------------------------------
        private void FormSplash_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Close();
        }
        //-------------------------------------------------------------------------------
		void FormSplashShown(object sender, EventArgs e)
		{
			m_timTimer.Start();
		}
        //-------------------------------------------------------------------------------
		protected override void OnClosed(EventArgs e)
		{
			if(formSplashEnd != null)
				formSplashEnd();  // Fire
			base.OnClosed(e);
		}
        //-------------------------------------------------------------------------------
		public event FormSplashEnd SplashEnd {
			add {
				if(value != null)
					formSplashEnd += value;
			}
			remove {
				if(value != null)
					formSplashEnd -= value;
			}
		}
    }
}
//---------------------------------------------------------------------------------------
