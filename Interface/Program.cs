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
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Base de Dados Geral do Utulizador
            DataBaseUser dataBase = new DataBaseUser();


            // Apresenta o Login
            Init abertura = new Init(dataBase);
            Application.Run(abertura);


            //MessageBox.Show("#### TODA A INFORMAÇÂO DA BASE DE DADOS ####\n"+dataBase.toString());

            // Apresenta a nova janela
            chooseProcess cp = new chooseProcess(dataBase);
            Application.Run(cp);


            
        }


    }
}
