#region Header

// Form About
// FormAboutData.cs : Provides data holder for form about
// Copyright© 2002-2020 ArdeshirV@protonmail.com, Licensed under LGPLv3+

using System;
using System.IO;
using System.Drawing;
using System.Reflection;
using ArdeshirV.Utilities;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using ArdeshirV.Forms.Properties;
using ArdeshirV.Utilities.QrCode;
using qr=ArdeshirV.Utilities.QrCode;

#endregion
//-----------------------------------------------------------------------------------
namespace ArdeshirV.Forms
{
    /// <summary>
    /// License class contains data about application/component's license
    /// </summary>
    public sealed class License
    {
    	private readonly string _name;
    	private readonly string _text;
    	private readonly Image  _logo;
    	//---------------------------------------------------------------------
    	/// <summary>
    	/// Component's name
    	/// </summary>
    	public string Name { get{ return _name; } }
    	//---------------------------------------------------------------------
    	/// <summary>
    	/// License text 
    	/// </summary>
    	public string Text { get{ return _text; } }
    	//---------------------------------------------------------------------
    	/// <summary>
    	/// License logo
    	/// </summary>
    	public Image  Logo { get{ return _logo; } }
    	//---------------------------------------------------------------------
    	private License() {}
    	//---------------------------------------------------------------------
    	/// <summary>
    	/// Creates License class that contains the component's license information
    	/// </summary>
    	/// <param name="name">License name such as GPL, MIT, Apache...</param>
    	/// <param name="text">License contents and details</param>
    	/// <param name="logo">License logo</param>
    	public License(string name, string text, Image logo)
    	{
    		_name = name;
    		_text = text;
    		_logo = logo;
    	}
    }
    //-------------------------------------------------------------------------------    
    /// <summary>
    /// Copyright class contains data about application/component's copyright 
    /// </summary>
    public sealed class Copyright
    {
    	private readonly Image  _logo;
    	private readonly string _name;
    	private readonly string _version;
    	private readonly string _company;
    	private readonly string _copyright;
    	private readonly string _description;
    	//---------------------------------------------------------------------
    	/// <summary>
    	/// Component's name
    	/// </summary>
    	public string Name { get{ return _name; } }
    	//---------------------------------------------------------------------
    	/// <summary>
    	/// Component's version
    	/// </summary>
    	public string Version { get{ return _version; } }
    	//---------------------------------------------------------------------
    	/// <summary>
    	/// Copyright text
    	/// </summary>
    	public string Copyrights { get{ return _copyright; } }
    	//---------------------------------------------------------------------
    	/// <summary>
    	/// Company name
    	/// </summary>
    	public string Company { get{ return _company; } }
    	//---------------------------------------------------------------------
    	/// <summary>
    	/// Component's description
    	/// </summary>
    	public string Description { get{ return _description; } }
    	//---------------------------------------------------------------------
    	/// <summary>
    	/// Component's logo
    	/// </summary>
    	public Image  Logo { get{ return _logo; } }
    	//---------------------------------------------------------------------
    	private Copyright() {}
    	//---------------------------------------------------------------------
    	/// <summary>
    	/// Creates copyright class that contains the component's copyright information.
    	/// </summary>
    	/// <param name="name">Component's name</param>
    	/// <param name="version">Component's version</param>
    	/// <param name="company">Component's company name</param>
    	/// <param name="copyrights">Copyright text</param>
    	/// <param name="description">Component's description</param>
    	/// <param name="logo">Component's logo</param>
    	public Copyright(string name, string version, string company,
    	                 string copyrights, string description, Image logo)
    	{
    		_name = name;
    		_logo = logo;
    		_company = company;
    		_version = version;
    		_copyright = copyrights;
    		_description = description;
    	}
    	//---------------------------------------------------------------------
    	/// <summary>
    	/// Creates copyright class that contains the component's copyright information.
    	/// This constructor retrive copyright assembly information
    	/// from specified control by reflection.
    	/// </summary>
    	/// <param name="component">Retrive copyright information from this component</param>
    	/// <param name="logo">Component's logo</param>
    	public Copyright(object component, Image logo)
    	{
            AssemblyAttributeAccessors info = new AssemblyAttributeAccessors(
    			Assembly.GetAssembly(component.GetType()));
    		_name = info.AssemblyTitle;
    		_logo = logo;
    		_version = info.AssemblyVersion;
    		_company = info.AssemblyCompany;
    		_copyright = info.AssemblyCopyright;
    		_description = info.AssemblyDescription;
    	}
    }
    //-------------------------------------------------------------------------------
    /// <summary>
    /// Contains a credit name/logo-description pair.
    /// </summary>
    public sealed class Credit
    {
    	private readonly string _name;
    	private readonly Image  _avator;
    	private readonly string _description;
    	//---------------------------------------------------------------------
    	public string Name { get { return _name; } }
    	public Image Avator { get { return _avator; } }
    	public string Description { get { return _description; } }
    	//---------------------------------------------------------------------
    	private Credit() {}
    	//---------------------------------------------------------------------
    	/// <summary>
    	/// Creates a credit entry with name, logo and description.
    	/// </summary>
    	/// <param name="name">Specify the person who has this credit.</param>
    	/// <param name="description">Description about this credit</param>
    	/// <param name="avator">Personal credit avator</param>
    	public Credit(string name, string description, Image avator)
    	{
    		_name = name;
    		_avator = avator;
    		_description = description;
    	}
    }
    //-----------------------------------------------------------------------------------
    /// <summary>
    /// Contains an array of credits
    /// </summary>
    public sealed class Credits
    {
    	private readonly string _name;
    	private readonly Credit[] _creditArr;
    	//-------------------------------------------------------------------------------
    	public string Name { get { return _name; } }
    	public Credit[] Items { get { return _creditArr; } }
    	public Credit this[int index] { get { return _creditArr[index]; } }
    	//-------------------------------------------------------------------------------
    	private Credits() {}
    	//-------------------------------------------------------------------------------
    	/// <summary>
    	/// Creates a credit array entry with names, avators and descritons.
    	/// </summary>
    	/// <param name="name">Component Name that refer to current credit.</param>
    	/// <param name="creditArr">Contains credit name, avator and descriton
    	/// about the current component that specified in name.</param>
    	public Credits(string name, Credit[] creditArr)
    	{
    		_name = name;
    		_creditArr = creditArr;
    	}
    }
    //-------------------------------------------------------------------------------
    /// <summary>
    /// Contains a donation currency-type/address pair.
    /// </summary>
    public sealed class Donation
    {
    	private readonly string _name;
    	private readonly Image  _logo;
    	private readonly string _address;
    	//---------------------------------------------------------------------
    	public string Name { get { return _name; } }
    	public Image Logo { get { return _logo; } }
    	public string Address { get { return _address; } }
    	//---------------------------------------------------------------------
    	private Donation() {}
    	//---------------------------------------------------------------------
    	/// <summary>
    	/// Creates a donation entry with currency-type/address pair.
    	/// </summary>
    	/// <param name="name">Currency type such as PayPal, Bitcoin, ...</param>
    	/// <param name="address">Currency address</param>
    	/// <param name="logo">Currency global logo</param>
    	public Donation(string name, string address, Image logo)
    	{
    		_name = name;
    		_logo = logo;
    		_address = address;
    	}
    }
    //-----------------------------------------------------------------------------------
    /// <summary>
    /// Contains a donation currency-type/address-list pair.
    /// </summary>
    public sealed class Donations
    {
    	private readonly string _name;
    	private readonly Donation[] _donations;
    	//-------------------------------------------------------------------------------
    	public string Name { get { return _name; } }
    	public Donation[] Items { get { return _donations; } }
    	public Donation this[int index] { get { return _donations[index]; } }
    	//-------------------------------------------------------------------------------
    	private Donations() {}
    	//-------------------------------------------------------------------------------
    	/// <summary>
    	/// Creates a donation entry with currency-type/address pair.
    	/// </summary>
    	/// <param name="name">Component Name that refer to donation addresses</param>
    	/// <param name="donations">Currency Name(s), Address(es), Logo(s)</param>
    	public Donations(string name, Donation[] donations)
    	{
    		_name = name;
    		_donations = donations;
    	}
    }
    //-----------------------------------------------------------------------------------
	public sealed class FormAboutData
	{
		private readonly string _URL;
		private readonly string _email;
		private readonly IWin32Window _formOwner;
		private readonly Dictionary<string, License> _licenses;
		private readonly Dictionary<string, Credit[]> _credits;
		private readonly Dictionary<string, Copyright> _copyrights;
		private readonly Dictionary<string, Donation[]> _donations;
		//-------------------------------------------------------------------------------
		public string URL { get { return _URL; } }
		public string Email { get { return _email; } }
		public IWin32Window Owner { get { return _formOwner; } }
		public Dictionary<string, Credit[]> Credits { get { return _credits; } }
		public Dictionary<string, License> Licenses { get { return _licenses; } }
		public Dictionary<string, Copyright> Copyrights { get { return _copyrights; } }
		public Dictionary<string, Donation[]> Donations { get { return _donations; } }
		//-------------------------------------------------------------------------------
		public string AppName
		{
			get {
				string result = string.Empty;
				
				if(Owner != null)
					result = new AssemblyAttributeAccessors(Owner).AssemblyProduct;
				
				return result;
			}
		}
		//-------------------------------------------------------------------------------
		private FormAboutData() {}
		//-------------------------------------------------------------------------------
		public FormAboutData(IWin32Window owner, Copyright[] copyrights,
		                     Credits[] credits, License[] licenses,
		                     Donations[] donations, string URL, string email)
		{
			_URL = URL;
			_email = email;
			_formOwner = owner;
			_credits = new Dictionary<string, Credit[]>();
			_licenses = new Dictionary<string, License>();
			_donations = new Dictionary<string, Donation[]>();
			_copyrights = new Dictionary<string, Copyright>();

			if(donations != null)
				foreach(Donations d in donations)
					_donations.Add(d.Name, d.Items);
			
			if(credits != null)
				foreach(Credits c in credits)
					_credits.Add(c.Name, c.Items);
			
			if(licenses != null)
				foreach(License l in licenses)
					_licenses.Add(l.Name, l);
			
			if(copyrights != null)
				foreach(Copyright c in copyrights)
					_copyrights.Add(c.Name, c);
			
			AddDefaults();
		}
		//-------------------------------------------------------------------------------
		private void AddDefaults()
		{
			AssemblyAttributeAccessors aaa = new AssemblyAttributeAccessors(this);
			_donations.Add(aaa.AssemblyTitle, DefaultDonationList.Items);
			
			_copyrights.Add(aaa.AssemblyTitle, new Copyright(this, Resources.ArdeshirV_Forms_Logo));
			_licenses.Add(aaa.AssemblyTitle, new License("LGPLv3+", Resources.LICENSE, Resources.LGPLv3));
			_credits.Add(aaa.AssemblyTitle, new Credit[] { DefaultCreditList.GetItem("ArdeshirV") });
			
			aaa = new AssemblyAttributeAccessors(qr.QrCode.Ecc.High);
			_copyrights.Add(aaa.AssemblyTitle, new Copyright(qr.QrCode.Ecc.High, qr.Res.Logo));
			_licenses.Add(aaa.AssemblyTitle, new License("MIT", qr.Res.License, qr.Res.LicenseLogo));
			_credits.Add(aaa.AssemblyTitle, new Credit[] {
			             	DefaultCreditList.GetItem("ArdeshirV.QrCode"),
			             	DefaultCreditList.GetItem("Manuel Bleichenbacher"),
			             	DefaultCreditList.GetItem("Nayuki Project")
			});
		}
	}
	//-----------------------------------------------------------------------------------
	/// <summary>
	/// Provides default donation addresses.
	/// </summary>
	public static class DefaultDonationList
	{
		private static readonly Donation[] _donations;
		private static readonly Dictionary<string, Donation> _list;
		//-------------------------------------------------------------------------------
		static DefaultDonationList()
		{
			//***************************************************************************
			// Warning: Nobody is not allowed to change or modify my donation addresses here
			// but you are allowed to add your own donation addresses somewhere else
 			// that refer to your own components. That place is out of this file.
			// Now search for "todo: You are allowed to change..." in your "Task List"
			// for more information and find that sample code about
			// how to adding your own addresses easily above mine.
			// You can find the best sample code in TestForms/FormMainTest.cs file.
			_donations = new Donation[] {
				new Donation("Bitcoin", "1GtjrxH6t8om8KwHAHKpcG5SAwVSsm4PEi", Resources.Bitcoin),
				new Donation("Ethereum", "0x6E6465394D14975956cd1BD37ab4E35F2C60300E", Resources.Ethereum),
				new Donation("Nano", "nano_1t7fg3drandk1crg363cn66px1adzsz3reeece8puuecbti4ysnyszbikry5", Resources.Nano),
				new Donation("TrueUSD", "0x6E6465394D14975956cd1BD37ab4E35F2C60300E", Resources.TrueUSD),
				new Donation("Monero", "42dJ7dWZjHyV4bzL4uVcS6cYnNfSvtXQyJaZ83yj4xALMiZoEMuja5SdcMHZS3Ai1eDCF9D5RDzgrHoyngckf38yC2MM42y", Resources.Monero),
				new Donation("Litecoin", "LdfYVr2Lgyuwp9K5Dk4aUsUbUQ2Rr5TA1n", Resources.Litecoin),
				new Donation("BitcoinCash", "bitcoincash:qzqsse2w6vqkqylfvkuqdvmgpl34zyq2cvwneunpyl", Resources.BitcoinCash),
				new Donation("Stellar", "GCMALSIYBJT74K5SKJFHMLBJRWFCO6EPIKQ4DO6C7ER4QAYIMTQSOOWI", Resources.Stellar),
				new Donation("Ripple", "raBw695Qnkt5RNTxTcyGFPzC5yFFHYztfR", Resources.Ripple),
				new Donation("Cardano", "DdzFFzCqrhsoSn5qvqRTovUkr1XHWy9LvbvDHVYFoFFyxAJkf1bT4X4ySpm8DwFqDo3EWAn934W9WfipbSNiXTcRzEqLP1y5KyAfMByJ", Resources.Cardano),
				new Donation("Neo", "AXqbLBWhcrFrjumvrm8nW9iYaUFoKML17b", Resources.Neo),
				new Donation("Tron", "TCB6uBci8mw1P4e2xikSGbyHcFjNCFphXr", Resources.Tron),
				new Donation("NEM", "NDXCTS2ITWO4J33YPL7AMHSGOIFRJSTBWSDZ4C3P", Resources.NEM),
				new Donation("Qtum", "QddGvqLg5yTi2J2DvixZVFgA1rpm5RAq2x", Resources.Qtum),
				new Donation("USDTether", "0x6E6465394D14975956cd1BD37ab4E35F2C60300E", Resources.USDTether),
				new Donation("Binance", "bnb15qwa7kl46ledh5zqqynmy2tqw92mhzuwmap6sf", Resources.Binance),
				new Donation("Dash", "XhsE3ntLYdKMbT4JptTvURqyu9RKuypDCf", Resources.Dash),
				new Donation("Verge", "DRviBtskt1M6BBTpAoNvF3qLrpKTN3mb7P", Resources.Verge),
				new Donation("ZCash", "t1ZnspKa3XFijtUAG6yKHsM3y6Bdb7C8HKA", Resources.ZCash)
			};
			_list = new Dictionary<string, Donation>();
			foreach(Donation d in _donations)
				_list.Add(d.Name, d);
			//************************************************************************
		}
		//-------------------------------------------------------------------------------
		public static Donation[] Items {
			get {
				return _donations;
			}
		}
		//-------------------------------------------------------------------------------
		public static Donation GetItem(string Name) {
			return _list[Name];
		}
		//-------------------------------------------------------------------------------
		public static bool IsExists(string Name) {
			return _list.ContainsKey(Name);
		}
	}
	//-----------------------------------------------------------------------------------
	/// <summary>
	/// Provides default credit information that are used
	/// in this project and relative projects.
	/// </summary>
	public static class DefaultCreditList
	{
		private static readonly Credit[] _credits;
		private static readonly Dictionary<string, Credit> _list;
		//-------------------------------------------------------------------------------
		static DefaultCreditList()
		{
			//***************************************************************************
			// Warning: Nobody is not allowed to change or modify credits that
			// are mentioned here but you are allowed to add your own credit
			// somewhere else that refer to your own components.
			// That place is out of this file.
			// Now search for "todo: You are allowed to change..." in your "Task List"
			// in FormMainTest.cs for more information and find that sample code about
			// how to adding your own credit information easily above mine.
			// You can find the best sample code in TestForms/FormMainTest.cs file.
			const string strCreditArdeshirVQrCode=
@"ArdeshirV downgrade the https://github.com/manuelbl/QrCodeGenerator project to C# 2.0 to use it with ArdeshirV.Forms Project.
QrCode: https://ardeshirv.github.io/ArdeshirV.Utility.QrCode
Github: https://github.com/ArdeshirV/ArdeshirV.Utility.QrCode
Email: ArdeshirV@protonmail.com";

			const string strCreditArdeshirV  =
@"ArdeshirV is 'ArdeshirV.Forms' founder and developer.
ArdeshirV.Forms: 
Github: https://github.com/ArdeshirV/ArdeshirV.Forms
Email: ArdeshirV@protonmail.com";

			const string strCreditNayuki =
@"The original QrCode project is created in Nayuki project with Java by Nayuki.
Project Nayuki: https://www.nayuki.io/page/qr-code-generator-library
Github: https://github.com/nayuki/QR-Code-generator
Email: me@nayuki.io";

			const string strCreditBleichenbacher =
@"Manuel Bleichenbacher converted original Nayuki project about QrCodeGenerator from Java to C#.
QrCodeGenerator: https://github.com/manuelbl/QrCodeGenerator";

			_credits = new Credit[] {
				new Credit("ArdeshirV.QrCode", strCreditArdeshirVQrCode, Resources.ArdeshirV),
				new Credit("ArdeshirV", strCreditArdeshirV, Resources.ArdeshirV),
				new Credit("Nayuki Project", strCreditNayuki, Res.NayukiAvator),
				new Credit("Manuel Bleichenbacher", strCreditBleichenbacher, Res.ManuelBleichenbacherAvator)
			};
			
			_list = new Dictionary<string, Credit>();
			foreach(Credit c in _credits)
				_list.Add(c.Name, c);
			//***************************************************************************
		}
		//-------------------------------------------------------------------------------
		/// <summary>
		/// Gets all items.
		/// </summary>
		public static Credit[] Items {
			get {
				return _credits;
			}
		}
		//-------------------------------------------------------------------------------
		/// <summary>
		/// Gets an item that is specified by a name.
		/// </summary>
		/// <param name="strCreditName">Credit name.</param>
		/// <returns>Credit with specified name.</returns>
		public static Credit GetItem(string strCreditName) {
			return _list[strCreditName];
		}
		//-------------------------------------------------------------------------------
		/// <summary>
		/// Check the item with specified name is exists or not.
		/// </summary>
		/// <param name="strCreditName">Credit name</param>
		/// <returns>true if credit with specified name is exists</returns>
		public static bool IsExists(string strCreditName) {
			return _list.ContainsKey(strCreditName);
		}
	}
}
//---------------------------------------------------------------------------------------