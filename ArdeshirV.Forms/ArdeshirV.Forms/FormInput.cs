#region Header

// Form Input
// FormInput.cs : Gets user input values in string variable.
// Copyright© 2002-2020 ArdeshirV@protonmail.com, Licensed under LGPLv3+

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion
//---------------------------------------------------------------------------------------
namespace ArdeshirV.Forms
{
	/// <summary>
	/// Description of FormInput.
	/// </summary>
	public partial class FormInput : FormBase
	{
		public delegate bool IsValidInput(string stringInput, out string stringErrMsg);
		//-------------------------------------------------------------------------------
		#region Variables

		private IsValidInput funcIsValidInput = null;
		private string stringResult = string.Empty;
		private const string stringDefaultInputMessage = "Enter input value: ";
		
		#endregion Variables
		//-------------------------------------------------------------------------------
		#region Constructor

		public FormInput(IWin32Window windowParent) : base(windowParent)
		{
			InitializeComponent();
			textBoxInput.Clear();
			FollowParentFormBase = true;
			labelInputMessage.Text = stringDefaultInputMessage;
			
			if(windowParent is Form)
				StartPosition = FormStartPosition.CenterParent;
			else
				StartPosition = FormStartPosition.CenterScreen;
		}
		
		#endregion Constructor
		//-------------------------------------------------------------------------------
		/// <summary>
		/// Displays an input box with the specified message and size.
		/// </summary>
		/// <param name="windowParent">Owner form.</param>
		/// <param name="Value">The text output.</param>
		/// <returns>One of DialogResult values</returns>
		public static DialogResult Show(IWin32Window windowParent, out string Value) {
			return Show(windowParent, out Value, null, stringDefaultInputMessage,
			            GetCaption(windowParent), string.Empty, Size.Empty);
		}
		//-------------------------------------------------------------------------------
		/// <summary>
		/// Displays an input box with the specified message and size.
		/// </summary>
		/// <param name="windowParent">Owner form.</param>
		/// <param name="Value">The text output.</param>
		/// <param name="IsValid">IsValid is a boolean function that checks input.</param>
		/// <returns>One of DialogResult values</returns>
		public static DialogResult Show(IWin32Window windowParent,
			out string Value, IsValidInput IsValid) {
			return Show(windowParent, out Value, IsValid, stringDefaultInputMessage,
			            GetCaption(windowParent), string.Empty, Size.Empty);
		}
		//-------------------------------------------------------------------------------
		/// <summary>
		/// Displays an input box with the specified message and size.
		/// </summary>
		/// <param name="windowParent">Owner form.</param>
		/// <param name="Value">The text output.</param>
		/// <param name="IsValid">IsValid is a boolean function that checks input.</param>
		/// <param name="InputMessage">The message in input box.</param>
		/// <returns>One of DialogResult values</returns>
		public static DialogResult Show(IWin32Window windowParent,
			out string Value, IsValidInput IsValid, string InputMessage) {
			return Show(windowParent, out Value, IsValid, InputMessage,
			            GetCaption(windowParent), string.Empty, Size.Empty);
		}
		//-------------------------------------------------------------------------------
		/// <summary>
		/// Displays an input box with the specified message and size.
		/// </summary>
		/// <param name="windowParent">Owner form.</param>
		/// <param name="Value">The text output.</param>
		/// <param name="IsValid">IsValid is a boolean function that checks input.</param>
		/// <param name="InputMessage">The message in input box.</param>
		/// <param name="Caption">Form input caption.</param>
		/// <returns>One of DialogResult values</returns>
		public static DialogResult Show(IWin32Window windowParent, out string Value,
		    IsValidInput IsValid, string InputMessage, string Caption) {
			return Show(windowParent, out Value,
			            IsValid, InputMessage, Caption, string.Empty, Size.Empty);
		}
		//-------------------------------------------------------------------------------
		/// <summary>
		/// Displays an input box with the specified message and size.
		/// </summary>
		/// <param name="windowParent">Owner form.</param>
		/// <param name="Value">The text output.</param>
		/// <param name="IsValid">IsValid is a boolean function that checks input.</param>
		/// <param name="InputMessage">The message in input box.</param>
		/// <param name="Caption">Form input caption.</param>
		/// <param name="DefaultValue">Initial text as default value of FormInput.</param>
		/// <returns>One of DialogResult values</returns>
		public static DialogResult Show(IWin32Window windowParent,
			out string Value, IsValidInput IsValid, string InputMessage,
			string Caption, string DefaultValue) {
			return Show(windowParent, out Value, IsValid, InputMessage,
			            Caption, DefaultValue, Size.Empty);
		}
		//-------------------------------------------------------------------------------
		/// <summary>
		/// Displays an input box with the specified message and size.
		/// </summary>
		/// <param name="windowParent">Owner form.</param>
		/// <param name="Value">The text output.</param>
		/// <param name="IsValid">IsValid is a boolean function that checks input.</param>
		/// <param name="InputMessage">The message in input box.</param>
		/// <param name="Caption">Form input caption.</param>
		/// <param name="DefaultValue">Initial text as default value of FormInput.</param>
		/// <param name="formSize">Form input size.</param>
		/// <returns>One of DialogResult values</returns>
		public static DialogResult Show(IWin32Window windowParent,
		    out string Value, IsValidInput IsValid, string InputMessage,
		    string Caption, string DefaultValue, Size formSize)
		{
			FormInput form = new FormInput(windowParent);
			form.labelInputMessage.Text = string.Format("       {0}", InputMessage);
			form.Text = Caption;
			form.textBoxInput.Text = DefaultValue;
			form.funcIsValidInput += IsValid;
			if(formSize != Size.Empty)
				form.Size = formSize;
			DialogResult result = form.ShowDialog(windowParent);
			Value = (result == DialogResult.OK)? form.stringResult: string.Empty;
			return result;
		}
		//-------------------------------------------------------------------------------
		private static string GetCaption(IWin32Window windowParent) {
			return (windowParent is Form && windowParent != null)?
				(windowParent as Form).Text: string.Empty;
		}
		//-------------------------------------------------------------------------------
		void FormInputShown(object sender, EventArgs e)
		{
			textBoxInput.Focus();
			// Below code is neccesary for run normally in Mono
			textBoxInput.Height = panelBackCenter.Height;
		}
		//-------------------------------------------------------------------------------
		void ButtonOKClick(object sender, EventArgs e)
		{
			if(funcIsValidInput != null) {
				string stringErrMsg;
				if(funcIsValidInput(textBoxInput.Text, out stringErrMsg)) {
					stringResult = textBoxInput.Text;
					buttonOK.DialogResult =
					DialogResult = DialogResult.OK;
				} else {
					errorProvider.SetError(textBoxInput, stringErrMsg);
					textBoxInput.SelectAll();
					textBoxInput.Focus();
				}
			} else {
				stringResult = textBoxInput.Text;
				buttonOK.DialogResult =
				DialogResult = DialogResult.OK;
			}
		}
		//-------------------------------------------------------------------------------
		void ButtonCancelClick(object sender, EventArgs e)
		{
			buttonCancel.DialogResult =
			DialogResult = DialogResult.Cancel;
		}
		//-------------------------------------------------------------------------------
		void TextBoxInputTextChanged(object sender, EventArgs e)
		{
			errorProvider.Clear();
		}
	}
}
//---------------------------------------------------------------------------------------