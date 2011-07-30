using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Business;
using System.Data;

namespace Interface
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string file = "";

            try
            {
                file = args[0];
            }
            catch (Exception) {}

            try
            {
                file = args[1];
            }
            catch (Exception) {} 

            // Show main windows
            chooseProcess cp = new chooseProcess(file);
            Application.Run(cp);

            

            
        }


    }
}
