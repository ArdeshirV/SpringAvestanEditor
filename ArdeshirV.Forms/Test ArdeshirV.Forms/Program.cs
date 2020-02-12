#region Header

// ArdeshirV.TestForms Project
// Program.cs: Entry Point
// Copyright© 2002-2020 ArdeshirV@protonmail.com, Licensed under GPLv3+

using System;
using System.Windows.Forms;

#endregion Header
//---------------------------------------------------------------------------------------------
namespace ArdeshirV.TestForms
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        internal static void Main()
        {
        	const string stringMessage = @"
    ArdeshirV.TestForms v2.0 - Shows how to use 'ArdeshirV.Forms' library
    You can find the latest update here: https://ardeshirv.github.io/ArdeshirV.Forms
    Copyright (C) 2002-2020 ArdeshirV@protonmail.com

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.";
        	ConsoleColor colorBackup =  Console.ForegroundColor;
        	Console.ForegroundColor = ConsoleColor.Green;
        	Console.WriteLine(stringMessage);
        	Console.ForegroundColor = colorBackup;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMainTest());
            GC.Collect();
        }
    }
}
