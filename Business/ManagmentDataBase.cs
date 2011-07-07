using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public static class ManagmentDataBase
    {
        public static Business.DataBaseUser database = new DataBaseUser() ;
        public static List<int> ids_dos_softwaresSeleccionados = new List<int>();
        public static Dictionary<int, string> caracteristicas_escolhidas = new Dictionary<int,string>();
        public static Business.DecisionSuport decision = new DecisionSuport();
        public static Dictionary<string, float> tabelaSmartNorm = new Dictionary<string,float>();
        public static Dictionary<string, float> pesosFinaisClassAHP = new Dictionary<string,float>();
        public static string metodo_fase_1 = "smart";

        public static Dictionary<int, Dictionary<string, float>> resultFinal = new Dictionary<int,Dictionary<string,float>>();

        public static Business.DataBaseUser Database
        {
            set { database = value; }
        }




    }
}
