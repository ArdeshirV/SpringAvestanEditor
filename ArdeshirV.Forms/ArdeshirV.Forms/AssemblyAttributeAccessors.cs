#region Header

// File: AssemblyAttributeAccessors.cs
// Provides access to assembly attributes.
// Copyright© 2002-2020 ArdeshirV@protonmail.com, Licensed under LGPLv3+

using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

#endregion
//-----------------------------------------------------------------------------------
namespace ArdeshirV.Utilities
{
	public static class Extractor
	{
		public static string[] ExctractEmails(string Input)
		{
        	return Exctract(Input,
			                @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
		}
		//-------------------------------------------------------------------------------
		public static string[] ExctractURLs(string Input)
		{
        	return Exctract(Input, 
			                @"((http|ftp|https|file):\/\/[\w\-_]+(\.[\w\-_]+)+([\w" +
			                @"\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)");
		}
		//-------------------------------------------------------------------------------
		public static string ExtractFirstURL(string Input)
		{
			string[] URLs = ExctractURLs(Input);
			return URLs.Length > 0 ? URLs[0] : string.Empty;
		}
		//-------------------------------------------------------------------------------
		public static string ExtractFirstEmail(string Input)
		{
			string[] Emails = ExctractEmails(Input);
			return Emails .Length > 0 ? Emails [0] : string.Empty;
		}
		//-------------------------------------------------------------------------------
		public static string[] Exctract(string Input, string regularExp)
		{
			Regex urlRegex = new Regex(regularExp, RegexOptions.IgnoreCase);
        	MatchCollection URLMatches = urlRegex.Matches(Input);
        	
        	int i = 0;
        	string[] values = new string[URLMatches.Count];
        	
        	foreach(Match m in URLMatches)
        		values[i++] = m.Value;
        	
        	return values;
		}
	}
	//-------------------------------------------------------------------------------
    public class AssemblyAttributeAccessors
    {
        #region Variables

        private readonly Assembly m_asmExcuting = null;

        #endregion Variables
        //---------------------------------------------------------------------
        public AssemblyAttributeAccessors(Assembly asmExcuting)
        {
            m_asmExcuting = asmExcuting;
        }
        //---------------------------------------------------------------------
        public AssemblyAttributeAccessors(object obj)
        {
            m_asmExcuting = Assembly.GetAssembly(obj.GetType());
        }
        //---------------------------------------------------------------------
        public string AssemblyTitle
        {
            get
            {
                object[] attributes = m_asmExcuting.GetCustomAttributes(
            		typeof(AssemblyTitleAttribute), false);

                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute =
                    	(AssemblyTitleAttribute)attributes[0];

                    if (titleAttribute.Title != "")
                        return titleAttribute.Title;
                }

                return Path.GetFileNameWithoutExtension(
                	m_asmExcuting.CodeBase);
            }
        }
        //---------------------------------------------------------------------
        public string AssemblyVersion
        {
            get
            {
                return m_asmExcuting.GetName().Version.ToString();
            }
        }
        //---------------------------------------------------------------------
        public string AssemblyDescription
        {
            get
            {
                object[] attributes = m_asmExcuting.GetCustomAttributes(
            		typeof(AssemblyDescriptionAttribute), false);

                if (attributes.Length == 0)
                    return string.Empty;
                else
                	return ((AssemblyDescriptionAttribute)
                        attributes[0]).Description;
            }
        }
        //---------------------------------------------------------------------
        public string AssemblyProduct
        {
            get
            {
                object[] attributes = m_asmExcuting.GetCustomAttributes(
            		typeof(AssemblyProductAttribute), false);

                if (attributes.Length == 0)
                    return string.Empty;
                else
                	return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }
        //---------------------------------------------------------------------
        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = m_asmExcuting.GetCustomAttributes(
            		typeof(AssemblyCopyrightAttribute), false);

                if (attributes.Length == 0)
                    return string.Empty;
                else
                	return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }
        //---------------------------------------------------------------------
        public string AssemblyCompany
        {
            get
            {
                object[] attributes = m_asmExcuting.GetCustomAttributes(
            		typeof(AssemblyCompanyAttribute), false);

                if (attributes.Length == 0)
                    return string.Empty;
                else
                	return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
    }
}
//-----------------------------------------------------------------------------------
