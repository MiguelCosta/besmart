using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Business
{
    public static class ManagementDataBase
    {
        public static Business.DataBaseUser database = new DataBaseUser();
        public static List<int> ids_dos_softwaresSeleccionados = new List<int>();
        public static Dictionary<int, string> caracteristicas_escolhidas = new Dictionary<int, string>();
        public static Business.DecisionSuport decision = new DecisionSuport();
        public static Dictionary<string, float> tabelaSmartNorm = new Dictionary<string, float>();
        public static Dictionary<string, float> pesosFinaisClassAHP = new Dictionary<string, float>();
        public static string metodo_fase_1 = "smart";

        public static Dictionary<int, Dictionary<string, float>> resultFinal = new Dictionary<int, Dictionary<string, float>>();


        public static DataView tableSoftwares(bool editable)
        {
            // actualizar a tabela inicial
            DataTable tabela_softwares = new DataTable();
            tabela_softwares.Columns.Add("ID");
            tabela_softwares.Columns.Add("Name");
            tabela_softwares.Columns.Add("Link");
            if (editable == false)
            {
                tabela_softwares.Columns["ID"].ReadOnly = true;
                tabela_softwares.Columns["Name"].ReadOnly = true;
                tabela_softwares.Columns["Link"].ReadOnly = true;
            }

            // adicionar as colunas (nome das caracteristicas)
            foreach (Business.Characteristic c in database.Charac.Values)
            {
                tabela_softwares.Columns.Add(c.Name);
                if (editable == false)
                {
                    tabela_softwares.Columns[c.Name].ReadOnly = true;
                }
            }

            // adiciona as linhas (info dos softwares)
            foreach (Business.Software s in database.Software_list.Values)
            {
                // coloca todas as caracteristicas numa List
                List<string> values = new List<string>();
                values.Add("" + s.Id);
                values.Add(s.Name);
                values.Add(s.Link);
                foreach (string cV in s.Charac.Values)
                {
                    values.Add(cV);
                }
                // passa para um array, para ser possivel adicionar uma linha
                string[] array = values.ToArray();
                tabela_softwares.Rows.Add(array);
            }

            return new DataView(tabela_softwares);
        }


        public static DataView tableSoftwaresWebPage()
        {
            DataTable tabela_softwares = new DataTable();
            tabela_softwares.Columns.Add("ID");
            tabela_softwares.Columns.Add("Name");
            tabela_softwares.Columns.Add("Link");

            foreach (Business.Software s in database.Software_list.Values)
            {
                List<string> values = new List<string>();
                values.Add("" + s.Id);
                values.Add(s.Name);
                values.Add(s.Link);

                string[] array = values.ToArray();
                tabela_softwares.Rows.Add(array);
            }

            return new DataView(tabela_softwares);
        }


        public static DataView tableCharacteristics()
        {
            DataTable tabela_caracteristicas = new DataTable();
            tabela_caracteristicas.Columns.Add("ID");
            tabela_caracteristicas.Columns.Add("Name");
            foreach (Characteristic c in database.Charac.Values)
            {
                tabela_caracteristicas.Rows.Add(c.Id, c.Name);
            }

            return new DataView(tabela_caracteristicas);

        }


        public static DataView tableSmart()
        {
            DataTable pesos = new DataTable();
            pesos.Columns.Add("ID");
            pesos.Columns.Add("Name");
            pesos.Columns["ID"].ReadOnly = true;
            pesos.Columns["Name"].ReadOnly = true;

            foreach (KeyValuePair<int, string> pair in caracteristicas_escolhidas)
            {
                pesos.Rows.Add(pair.Key, pair.Value);
            }

            return new DataView(pesos);
        }


        public static DataView tableAHP()
        {
            DataTable pesos = new DataTable();
            pesos.Columns.Add("Best Software");
            foreach (string name in caracteristicas_escolhidas.Values)
            {
                pesos.Columns.Add(name);
                pesos.Rows.Add(name);
            }

            return new DataView(pesos);
        }


        public static DataView tableCaracteristicasPrioridades()
        {
            DataTable carc = new DataTable();
            carc.Columns.Add("ID");
            carc.Columns.Add("Name");
            foreach (KeyValuePair<int, string> pair in caracteristicas_escolhidas)
            {
                carc.Rows.Add(pair.Key, pair.Value);
            }

            return new DataView(carc);
        }

        public static void addIdSoftwareSelect(int id)
        {
            ids_dos_softwaresSeleccionados.Add(id);

        }

        public static int totalSoftwareSelect()
        {
            return ids_dos_softwaresSeleccionados.Count;
        }

        public static int totalCharacteristcSelect()
        {
            return caracteristicas_escolhidas.Count;
        }

        // load file
        public static void loadObject(String filename)
        {
            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter bformatter = new BinaryFormatter();

            Business.ManagementDataBase.database = (Business.DataBaseUser)bformatter.Deserialize(stream);
            stream.Close();
        }

        // info software select
        public static Dictionary<int, Software> infoSoftware_byID()
        {
            return database.infoSoftware_byID(ids_dos_softwaresSeleccionados);
        }


    }
}
