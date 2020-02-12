#region Header

// //////////////////////////////////////////////////////////////////////////////////////
//                                                                                     //
//     Windows Standard Application                                                    //
//     Program.cs : Provides Entry Point                                               //
//                                                                                     //
//-------------------------------------------------------------------------------------//
//                                                                                     //
//    Copyright© 2008-2020 ArdeshirV@protonmail.com, Licensed under GPLv3+             //
//                                                                                     //
// //////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Windows.Forms;
using ArdeshirV.SpringAvestanEditor;

#endregion
//---------------------------------------------------------------------------------------
namespace ArdeshirV.SpringAvestanEditor
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        internal static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMainForm(args));
        }
    }
}









