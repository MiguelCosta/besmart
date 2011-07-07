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
            // DataBaseUser dataBase = new DataBaseUser();

            // Business.ManagmentDataBase.Database = dataBase;

            // Apresenta o Login
            Init abertura = new Init(Business.ManagmentDataBase.database);
            Application.Run(abertura);


            //MessageBox.Show("#### TODA A INFORMAÇÂO DA BASE DE DADOS ####\n"+dataBase.toString());

            // Apresenta a nova janela
            chooseProcess cp = new chooseProcess(Business.ManagmentDataBase.database);
            Application.Run(cp);


            
        }


    }
}
