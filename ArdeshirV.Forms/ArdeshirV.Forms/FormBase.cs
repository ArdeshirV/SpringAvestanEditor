#region Header

// ArdeshirV.Forms Project
// FormBase.cs : Provides FormBase with extended abilities
// Copyright© 2002-2020 ArdeshirV@protonmail.com, Licensed under LGPLv3+

using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

#endregion
//---------------------------------------------------------------------------------------
namespace ArdeshirV.Forms
{
    /// <summary>
    /// Provides an extended Form with special properties such as form shrink ability,
    /// background gradient colors and fading ability.  
    /// </summary>
    public class FormBase : Form
    {
        #region Variables

        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool m_blnFollowParentFormBase = true;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private Image m_imgSplash = null;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool m_blnActivated = true;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool m_blnMouseDown = false;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private double m_dblLastOpacity = 0.0;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private const int m_intShrinkValue = 5;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private EventHandler m_dlgShrinkEnd = null;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private EventHandler m_dlgShrinkBegan = null;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private Point m_pntLastLocation = Point.Empty;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool m_blnMoveWithMouseAbility = true;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool m_blnChangingOpacityAbility = true;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private Color m_clrEnd = SystemColors.Highlight;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool m_blnBackgroundGradientColor = true;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private Color m_clrEndInactive = SystemColors.Highlight;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private Color m_clrStart = SystemColors.GradientActiveCaption;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private EventHandler m_dlgBackgroundGradientColorChanged = null;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private Color m_clrStartInactive = SystemColors.GradientInactiveCaption;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private LinearGradientMode m_lgmBackgroundActiveMode = LinearGradientMode.Vertical;
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        private LinearGradientMode m_lgmInactiveBackgroundActiveMode = LinearGradientMode.Vertical;
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        private Form _formParent = null;

        #endregion
        //-------------------------------------------------------------------------------
        #region Constructor

        /// <summary>
        /// Create FormBase instance
        /// </summary>
        protected FormBase()
        {
        	IWin32Window window = Parent;
        	
        	if(window != null) {
        		if(window is Form)
        			_formParent = window as Form;
        		
		    	if (window is FormBase && m_blnFollowParentFormBase)
		            FollowFormBase(window as FormBase);
        	}
        }
        //-------------------------------------------------------------------------------
        /// <summary>
        /// Create FormBase instance
        /// </summary>
        protected FormBase(IWin32Window windowParent)
        {
        	IWin32Window window = windowParent;
        	if (windowParent == null)
        		window = Parent;
        	
    		if(window is Form)
    			_formParent = window as Form;
        		
            if (window is FormBase && m_blnFollowParentFormBase)
                FollowFormBase(window as FormBase);
        }

        #endregion
        //-------------------------------------------------------------------------------
        #region Interface

        public void ShrinkWidthByTime(int intNewWidth)
        {
            if (WindowState == FormWindowState.Normal)
            {
                RaiseShrinkBegan();
                int intInc = 8, intIncHalf = intInc / 2;
                int l_intShrinkWidthSize = (intNewWidth - Width) / intInc;

                if(l_intShrinkWidthSize < 0)
                {
                    intInc = -intInc;
                    intIncHalf = -intIncHalf;
                    l_intShrinkWidthSize = -l_intShrinkWidthSize;
                }
                
                for (int i = 0; i < l_intShrinkWidthSize; i++)
                {
                    if (Width == intNewWidth ||
                        (intInc > 0 && MaximumSize != Size.Empty &&
                	     Width >= MaximumSize.Width) ||
                        (intInc < 0 && MinimumSize != Size.Empty &&
                	     Width <= MinimumSize.Width))
                        break;

                    Application.DoEvents();
                    Thread.Sleep(5);
                    Application.DoEvents();
                    Left -= intIncHalf;
                    Width += intInc;
                }

                Width = intNewWidth;
                RaiseShrinkEnd();
            }
            else
            	Width = intNewWidth;
        }
        //-------------------------------------------------------------------------------
        public void ShrinkHeightByTime(int intNewHeight)
        {
            if (WindowState == FormWindowState.Normal)
            {
                RaiseShrinkBegan();
                int intInc = 8;
                int intIncHalf = intInc / 2;
                int l_intShrinkHeightSize = (intNewHeight - Height) / intInc;

                if (l_intShrinkHeightSize < 0)
                {
                    intInc = -intInc;
                    intIncHalf = -intIncHalf;
                    l_intShrinkHeightSize = -l_intShrinkHeightSize;
                }

                for (int i = 0; i < l_intShrinkHeightSize; i++)
                {
                    if (Height == intNewHeight ||
                        (intInc > 0 && MaximumSize != Size.Empty &&
                	     Height >= MaximumSize.Height) ||
                        (intInc < 0 && MinimumSize != Size.Empty &&
                	     Height <= MinimumSize.Height))
                        break;

                    Application.DoEvents();
                    Thread.Sleep(10);
                    Application.DoEvents();
                    Top -= intIncHalf;
                    Height += intInc;

                    if (Height == intNewHeight)
                        break;
                }

                Height = intNewHeight;
                RaiseShrinkEnd();
            }
            else
                Height = intNewHeight;
        }
        //-------------------------------------------------------------------------------
        public void ShrinkWidth(int intNewWidth)
        {
            if (WindowState == FormWindowState.Normal)
            {
                int l_intShrinkWidthSize = intNewWidth - Width;

                RaiseShrinkBegan();
                Left -= l_intShrinkWidthSize / 2;
                Width += l_intShrinkWidthSize;
                Width = intNewWidth;
                RaiseShrinkEnd();
            }
            else
                Width = intNewWidth;
        }
        //-------------------------------------------------------------------------------
        public void ShrinkHeight(int intNewHeight)
        {
            if (WindowState == FormWindowState.Normal)
            {
                int l_intShrinkHeightSize = intNewHeight - Height;

                RaiseShrinkBegan();
                Top -= l_intShrinkHeightSize / 2;
                Height += l_intShrinkHeightSize;
                Height = intNewHeight;
                RaiseShrinkEnd();
            }
            else
                Height = intNewHeight;
        }
        //-------------------------------------------------------------------------------
        public void FollowFormBase(FormBase frmMasterFormBase)
        {
        	FormBase form = frmMasterFormBase as FormBase;

            if (form != null)
            {
                OpacityChanger = form.OpacityChanger;
                MoveFormWithMouse = form.MoveFormWithMouse;
                BackgroundGradientMode = form.BackgroundGradientMode;
                BackgroundGradientColor = form.BackgroundGradientColor;
                BackgoundEndGradientColor = form.BackgoundEndGradientColor;
                BackgoundStartGradientColor = form.BackgoundStartGradientColor;
                BackgoundInactiveEndGradientColor = form.BackgoundInactiveEndGradientColor;
                BackgoundInactiveStartGradientColor = form.BackgoundInactiveStartGradientColor;
            }
        }

        #endregion
        //-------------------------------------------------------------------------------
        #region External Methods

        // I removed this code to keep my code platform independent
        //[DllImport("user32.dll", EntryPoint = "AnimateWindow", CharSet = CharSet.Unicode)]
        //static extern int AnimateWindow(IntPtr hWnd, long dwTime, long dwFlags);

        //protected void CreateRoundRect()
        //{
        //	IntPtr l_rgnRoundRect = CreateRoundRectRgn(0, 0, Width, Height, 30, 30);
        //	SetWindowRgn(Handle.ToPointer(), l_rgnRoundRect, 1);
        //	DeleteObject(l_rgnRoundRect);
        //}

        #endregion
        //-------------------------------------------------------------------------------
        #region Overrided functions

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Hide();
            AddSpecialMouseEvent(this);

            if (m_blnChangingOpacityAbility) {
                m_dblLastOpacity = Opacity;
                Opacity = 0.0;
            } else {
            	Show();
            }
            
            if(m_imgSplash is Bitmap)
            	FormSplash.Show(this, m_imgSplash);
        }
        //-------------------------------------------------------------------------------
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            m_blnActivated = true;
            Invalidate(true);
        }
        //-------------------------------------------------------------------------------
        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            m_blnActivated = false;
            Invalidate(true);
        }
        //-------------------------------------------------------------------------------
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Invalidate(true);
            Show();
            
            if (m_blnChangingOpacityAbility)
            {
                for (int l_intIndexer = 0; l_intIndexer < 10; l_intIndexer++)
                {
                    System.Threading.Thread.Sleep(90);
                    Application.DoEvents();
                    Opacity += 0.1;

                    if (Opacity >= m_dblLastOpacity)
                        break;
                }

                if(!Disposing && !IsDisposed) {
                	Opacity = m_dblLastOpacity;
                	Focus();
                }
            }
        }
        //-------------------------------------------------------------------------------
        protected override void OnClosed(EventArgs e)
        {
            if (m_blnChangingOpacityAbility)
            {
                for (int l_intIndexer = 0; l_intIndexer < 10; l_intIndexer++)
                {
                    Opacity -= 0.1;
                    Application.DoEvents();
                    Thread.Sleep(90);
                }
            }

            base.OnClosed(e);
            Dispose();
            // Below code is neccessary when we use this component with Mono runtime
            if(_formParent != null)
            	_formParent.Activate();
        }
        //-------------------------------------------------------------------------------
        protected override void OnResize(EventArgs e)
        {
            Invalidate(true);
            base.OnResize(e);
        }
        //-------------------------------------------------------------------------------
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (m_blnBackgroundGradientColor &&
        	    ClientRectangle.Width > 0 && ClientRectangle.Height > 0)
            {
                LinearGradientBrush l_brsGradient;

                if(m_blnActivated)
                    l_brsGradient = new LinearGradientBrush(ClientRectangle,
                		m_clrStart, m_clrEnd, m_lgmBackgroundActiveMode);
                else
                    l_brsGradient = new LinearGradientBrush(ClientRectangle,
                		m_clrStartInactive, m_clrEndInactive, m_lgmBackgroundActiveMode);

                try {
                    e.Graphics.FillRectangle(l_brsGradient, ClientRectangle);
                } finally {
                    l_brsGradient.Dispose();
                }
            }
            else
                base.OnPaintBackground(e);
        }
        //-------------------------------------------------------------------------------
        public int GetTitlebarHeight()
        {
        	Rectangle screenRectangle = RectangleToScreen(ClientRectangle);
			return screenRectangle.Top - Top;
        }

        #endregion
        //-------------------------------------------------------------------------------
        #region Utility Functions

        private void InitializeComponent() { }
        //-------------------------------------------------------------------------------
        private bool IsMovableControl(Control c)
        {
        	return c is Label || c is LinkLabel || c is TabPage || c is TabControl ||
				c is Panel || c is PictureBox || c is ProgressBar || c is GroupBox ||
				c is FlowLayoutPanel || c is TableLayoutPanel || 
        		c is ToolStripContainer || c is ToolStripContentPanel ||
        		c is RadioButton || c is CheckedListBox || 
        		c is CheckBox || c is TableLayoutPanel;
        }
        //-------------------------------------------------------------------------------
        public void AddSpecialMouseEvent(Control ctlDest)
        {
            AddMouseEvents(ctlDest);
            foreach (Control c in ctlDest.Controls) {
            	if (IsMovableControl(c)) {
                    AddSpecialMouseEvent(c);
            	}
            }
        }
        //-------------------------------------------------------------------------------
        public void RemoveSpecialMouseEvent(Control ctlDest)
        {
            RemoveMouseEvents(ctlDest);
            foreach (Control c in ctlDest.Controls) {
            	if (IsMovableControl(c)) {
                    RemoveSpecialMouseEvent(c);
            	}
            }
        }
        //-------------------------------------------------------------------------------
        private void RemoveMouseEvents(Control ctlDest)
        {
            ctlDest.MouseUp -= new MouseEventHandler(OnMouseUp_Event);
            ctlDest.MouseMove -= new MouseEventHandler(OnMouseMove_Event);
            ctlDest.MouseDown -= new MouseEventHandler(OnMouseDown_Event);
        }
        //-------------------------------------------------------------------------------
        private void AddMouseEvents(Control ctlDest)
        {
            ctlDest.MouseUp += new MouseEventHandler(OnMouseUp_Event);
            ctlDest.MouseMove += new MouseEventHandler(OnMouseMove_Event);
            ctlDest.MouseDown += new MouseEventHandler(OnMouseDown_Event);
        }

        #endregion
        //-------------------------------------------------------------------------------
        #region Event Handlers

        private void OnMouseDown_Event(object sender, MouseEventArgs e)
        {
            m_blnMouseDown = true;
            m_pntLastLocation = e.Location;
        }
        //-------------------------------------------------------------------------------
        private void OnMouseMove_Event(object sender, MouseEventArgs e)
        {
            if (m_blnMoveWithMouseAbility && m_blnMouseDown) {
                Top += e.Y - m_pntLastLocation.Y;
                Left += e.X - m_pntLastLocation.X;
            }
        }
        //-------------------------------------------------------------------------------
        private void OnMouseUp_Event(object sender, MouseEventArgs e)
        {
            m_blnMouseDown = false;
        }

        #endregion
        //-------------------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// This form should follow parent special-form properties.
        /// </summary>
        [Browsable(true)]
        [Category("Special")]
        [Description("This form should follow parent special-form properties.")]
        public bool FollowParentFormBase
        {
            get
            {
                return m_blnFollowParentFormBase;
            }
            set
            {
                m_blnFollowParentFormBase = value;
            }
        }
        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets or gets the ending lieaner background color when form is inactive.
        /// </summary>
        [Browsable(true)]
        [Category("Special")]
        [Description("Splash Image the best size is 400 * 250.")]
        public Image SplashImage
        {
            get
            {
                return m_imgSplash;
            }
            set
            {
                m_imgSplash = value;
            }
        }
        //-------------------------------------------------------------------------------------

        /// <summary>
        /// Sets or gets the ending lieaner background color when form is inactive.
        /// </summary>
        [Browsable(true)]
        [Category("Special")]
        [Description("Ending lieaner background color when form is inactive.")]
        public Color BackgoundInactiveEndGradientColor
        {
            get
            {
                return m_clrEndInactive;
            }
            set
            {
                m_clrEndInactive = value;
                RaiseBackgroundGradientColorChanged();
                Invalidate();
            }
        }
        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Starting lieaner background color when form is inactive.
        /// </summary>
        [Browsable(true)]
        [Category("Special")]
        [Description("Starting lieaner background color when form is inactive.")]
        public Color BackgoundInactiveStartGradientColor
        {
            get
            {
                return m_clrStartInactive;
            }
            set
            {
                m_clrStartInactive = value;
                RaiseBackgroundGradientColorChanged();
                Invalidate();
            }
        }
        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Gets last form location when mouse down.
        /// </summary>
        [Browsable(false)]
        [Category("Special")]
        [Description("Last form location when mouse down.")]
        public Point LocationOnMouseDown
        {
            get
            {
                if (m_blnMouseDown)
                    return m_pntLastLocation;
                else
                    return Point.Empty;
            }
        }
        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Mouse is down?
        /// </summary>
        [Browsable(false)]
        [Category("Special")]
        [Description("Mouse is down?")]
        public bool MouseIsDown
        {
            get
            {
                return m_blnMouseDown;
            }
        }
        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the true if form is activated.
        /// </summary>
        [Browsable(false)]
        [Category("Special")]
        [Description("Return true if current form is activated.")]
        public bool IsActivated
        {
            get
            {
                return m_blnActivated;
            }
        }
        //-------------------------------------------------------------------------------------b
        /// <summary>
        /// Gets or sets the "move form with mouse" ability.
        /// </summary>
        [Browsable(true)]
        [Category("Special")]
        [Description("Move form with mouse ability.")]
        public bool MoveFormWithMouse
        {
            set
            {
                m_blnMoveWithMouseAbility = value;
            }
            get
            {
                return m_blnMoveWithMouseAbility;
            }
        }
        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Sets or gets the ending lieaner background color.
        /// </summary>
        [Browsable(true)]
        [Category("Special")]
        [Description("Ending lieaner background color.")]
        public Color BackgoundEndGradientColor
        {
            get
            {
                return m_clrEnd;
            }
            set
            {
                m_clrEnd = value;
                RaiseBackgroundGradientColorChanged();
                Invalidate();
            }
        }
        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Starting lieaner background color.
        /// </summary>
        [Browsable(true)]
        [Category("Special")]
        [Description("Starting lieaner background color.")]
        public Color BackgoundStartGradientColor
        {
            get
            {
                return m_clrStart;
            }
            set
            {
                m_clrStart = value;
                RaiseBackgroundGradientColorChanged();
                Invalidate();
            }
        }
        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Make gradient lieaner background color ability enable/disable.
        /// </summary>
        [Browsable(true)]
        [Category("Special")]
        [Description("Make gradient lieaner background color ability enable/disable.")]
        public bool BackgroundGradientColor
        {
            get
            {
                return m_blnBackgroundGradientColor;
            }
            set
            {
                m_blnBackgroundGradientColor = value;
                RaiseBackgroundGradientColorChanged();
                Invalidate();
            }
        }
        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets opacity changer delay ability.
        /// </summary>
        [Browsable(true)]
        [Category("Special")]
        [Description("Gets or sets opacity changer delay ability.")]
        public bool OpacityChanger
        {
            get
            {
                return m_blnChangingOpacityAbility;
            }
            set
            {
                m_blnChangingOpacityAbility = value;
            }
        }
        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets background drawing mode.
        /// </summary>
        [Browsable(true)]
        [Category("Special")]
        [Description("Gets or sets background drawing mode.")]
        public System.Drawing.Drawing2D.LinearGradientMode BackgroundGradientMode
        {
            get
            {
                return m_lgmBackgroundActiveMode;
            }
            set
            {
                m_lgmBackgroundActiveMode = value;
                RaiseBackgroundGradientColorChanged();
                Invalidate();
            }
        }
        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets background drawing mode when form is inactive.
        /// </summary>
        [Browsable(true)]
        [Category("Special")]
        [Description("Gets or sets background drawing mode when form is inactive.")]
        public System.Drawing.Drawing2D.LinearGradientMode BackgroundInactiveGradientMode
        {
            get
            {
                return m_lgmInactiveBackgroundActiveMode;
            }
            set
            {
                m_lgmInactiveBackgroundActiveMode = value;
                RaiseBackgroundGradientColorChanged();
                Invalidate();
            }
        }

        #endregion
        //-------------------------------------------------------------------------------
        #region Events

        public event EventHandler OnBackgroundGradientColorChange
        {
            add
            {
                m_dlgBackgroundGradientColorChanged += value;
            }
            remove
            {
            	if(m_dlgBackgroundGradientColorChanged != null)
            		// disable once DelegateSubtraction
                	m_dlgBackgroundGradientColorChanged -= value;
            }
        }
        //-------------------------------------------------------------------------------------
        public event EventHandler OnShrinkBegan
        {
            add
            {
                m_dlgShrinkBegan += value;
            }
            remove
            {
            	if(m_dlgShrinkBegan != null)
                	// disable once DelegateSubtraction
                	m_dlgShrinkBegan -= value;
            }
        }
        //-------------------------------------------------------------------------------------
        public event EventHandler OnShrinkEnd
        {
            add
            {
                m_dlgShrinkEnd += value;
            }
            remove
            {
            	if(m_dlgShrinkEnd != null)
                	// disable once DelegateSubtraction
                	m_dlgShrinkEnd -= value;
            }
        }
        //-------------------------------------------------------------------------------------
        private void RaiseBackgroundGradientColorChanged()
        {
            if(m_dlgBackgroundGradientColorChanged != null)
                m_dlgBackgroundGradientColorChanged(this, null);
        }
        //-------------------------------------------------------------------------------------
        private void RaiseShrinkEnd()
        {
            if (m_dlgShrinkEnd != null)
                m_dlgShrinkEnd(this, null);
        }
        //-------------------------------------------------------------------------------------
        private void RaiseShrinkBegan()
        {
            if (m_dlgShrinkBegan != null)
                m_dlgShrinkBegan(this, null);
        }

        #endregion
    }
}
//---------------------------------------------------------------------------------------
