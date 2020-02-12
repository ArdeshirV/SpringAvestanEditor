#region Header

// Copyright© 2002-2020 ArdeshirV@protonmail.com, Licensed under LGPLv3+
using System;
using System.IO;
using System.Drawing;
using ArdeshirV.Forms;
using ArdeshirV.Utilities;
using System.Windows.Forms;
using AVFR = ArdeshirV.Forms.Properties;

#endregion
//---------------------------------------------------------------------------------------
namespace ArdeshirV.Controls
{
	class ComboBoxImage : ComboBox
	{
		private ImageList _images;
		
		public ComboBoxImage() : base()
	    {
	        DrawMode = DrawMode.OwnerDrawFixed;
	        DropDownStyle = ComboBoxStyle.DropDownList;
	    }
		//-------------------------------------------------------------------------------
	    protected override void OnDrawItem(DrawItemEventArgs e)
	    {
	        e.DrawBackground();
	        e.DrawFocusRectangle();
	        	
	        if(e.Index >= 0) {
	        	string strItem;
	        	try {
		        	strItem = (string)Items[e.Index];
	        	} catch(Exception) {
	        		base.OnDrawItem(e);
	        		return;
	        	}
		        
		        e.Graphics.FillRectangle(new SolidBrush(e.BackColor), e.Bounds);
		        e.Graphics.DrawString(strItem, e.Font, new SolidBrush(e.ForeColor),
		                              e.Bounds.Left + e.Bounds.Height, e.Bounds.Top + 2);
		        
		        Image pic;
		        try {
			        pic = _images.Images[strItem];
	        	} catch(Exception) {
	        		base.OnDrawItem(e);
	        		return;
	        	}
		        
		        if(pic != null)
		        	e.Graphics.DrawImage(pic, e.Bounds.Left, e.Bounds.Top,
		        	                     e.Bounds.Height, e.Bounds.Height);
	        }
	
	        base.OnDrawItem(e);
	    }
		//-------------------------------------------------------------------------------
	    public ImageList Images {
	    	get {
	    		return _images;
	    	}
	    	set {
	    		_images = value;
	    	}
	    }
	}
}
