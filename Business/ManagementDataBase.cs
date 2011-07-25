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
        public static List<int> ids_dos_SoftwareSeleccionados = new List<int>();
        public static Dictionary<int, string> caracteristicas_escolhidas = new Dictionary<int, string>();
        public static Business.DecisionSuport decision = new DecisionSuport();
        public static Dictionary<string, float> tabelaSmartNorm = new Dictionary<string, float>();
        public static Dictionary<string, float> pesosFinaisClassAHP = new Dictionary<string, float>();
        public static string metodo_fase_1 = "smart";

        public static Dictionary<int, Dictionary<string, float>> resultFinal = new Dictionary<int, Dictionary<string, float>>();

        public static Dictionary<int, int> id_characteristics_to_columns = new Dictionary<int, int>();


        public static DataView tableSoftware(bool editable)
        {
            id_characteristics_to_columns = new Dictionary<int, int>();

            int column = 1;
            // actualizar a tabela inicial
            DataTable tabela_Software = new DataTable();
            tabela_Software.Columns.Add("ID");
            column++;
            tabela_Software.Columns.Add("Name");
            column++;
            tabela_Software.Columns.Add("Link");
            column++;

            if (editable == false)
            {
                tabela_Software.Columns["ID"].ReadOnly = true;
                tabela_Software.Columns["Name"].ReadOnly = true;
                tabela_Software.Columns["Link"].ReadOnly = true;
            }



            // adicionar as colunas (nome das caracteristicas)
            foreach (Business.Characteristic c in database.Charac.Values)
            {
                // adiciona a coluna
                tabela_Software.Columns.Add(c.Name);

                // coloca na estrura o id e a coluna correspondente
                id_characteristics_to_columns.Add(c.Id, column);

                // vai incrementrar a coluna para a caracteristica seguinte
                column++;
                if (editable == false)
                {
                    tabela_Software.Columns[c.Name].ReadOnly = true;
                }
            }

            //estrutura que faz corresponder a cadar coluna a informação


            // coluna na estrutura, o numero da coluna e a informação que tem de estar 
            foreach (Business.Software s in database.Software_list.Values)
            {
                Dictionary<int, string> info = new Dictionary<int, string>();
                info.Add(1, "" + s.Id);
                info.Add(2, "" + s.Name);
                info.Add(3, "" + s.Link);

                // adiciona as caracteristicas
                foreach (KeyValuePair<int, string> pair in s.Charac)
                {
                    int coluna = 0;
                    id_characteristics_to_columns.TryGetValue(pair.Key, out coluna);
                    info.Add(coluna, pair.Value);
                }

                // passa para um array, para ser possivel adicionar uma linha
                int num_colunas = database.Charac.Count + 3;
                string[] array = new string[num_colunas];

                // vai colocar por ordem no array o valor de cada coluna
                for (int i = 0; i < num_colunas; i++)
                {
                    string v = ""; 
                    // a coluna 0 é uma checkbox, logo é +1
                    info.TryGetValue(i+1, out v);
                    array[i] = v;
                }

                // adicona uma linha onde o array que recebe está por ordem crescente de coluna
                tabela_Software.Rows.Add(array);

            }

            return new DataView(tabela_Software);
        }


        public static DataView tableSoftwareSimple()
        {
            // actualizar a tabela inicial
            DataTable tabela_Software = new DataTable();
            tabela_Software.Columns.Add("ID");
            tabela_Software.Columns.Add("Name");

            tabela_Software.Columns["ID"].ReadOnly = true;
            tabela_Software.Columns["Name"].ReadOnly = true;


            // adiciona as linhas (info dos Software)
            foreach (Business.Software s in database.Software_list.Values)
            {
                // coloca todas as caracteristicas numa List
                List<string> values = new List<string>();
                values.Add("" + s.Id);
                values.Add(s.Name);

                // passa para um array, para ser possivel adicionar uma linha
                string[] array = values.ToArray();
                tabela_Software.Rows.Add(array);
            }

            return new DataView(tabela_Software);
        }


        public static DataView tableSoftwareWebPage()
        {
            DataTable tabela_Software = new DataTable();
            tabela_Software.Columns.Add("ID");
            tabela_Software.Columns.Add("Name");
            tabela_Software.Columns.Add("Link");

            foreach (Business.Software s in database.Software_list.Values)
            {
                List<string> values = new List<string>();
                values.Add("" + s.Id);
                values.Add(s.Name);
                values.Add(s.Link);

                string[] array = values.ToArray();
                tabela_Software.Rows.Add(array);
            }

            return new DataView(tabela_Software);
        }


        public static DataView tableCharacteristics()
        {
            DataTable tabela_caracteristicas = new DataTable();
            tabela_caracteristicas.Columns.Add("ID");
            tabela_caracteristicas.Columns.Add("Name");
            tabela_caracteristicas.Columns.Add("Type");

            tabela_caracteristicas.Columns["ID"].ReadOnly = true;
            tabela_caracteristicas.Columns["Name"].ReadOnly = true;
            tabela_caracteristicas.Columns["Type"].ReadOnly = true;

            foreach (Characteristic c in database.Charac.Values)
            {
                string t = "";
                if (c.GetType().ToString().Equals("Business.NumericCharacteristic")) t = "Numeric";
                if (c.GetType().ToString().Equals("Business.QualitativeCharacteristic")) t = "Qualitative";
                if (c.GetType().ToString().Equals("Business.YesNoCharacteristic")) t = "Bool";
                tabela_caracteristicas.Rows.Add(c.Id, c.Name, t);
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

        public static DataView tableAddSoftware()
        {
            DataTable tabela_caracteristicas = new DataTable();
            tabela_caracteristicas.Columns.Add("Value");
            tabela_caracteristicas.Columns.Add("ID");
            tabela_caracteristicas.Columns.Add("Name");
            tabela_caracteristicas.Columns.Add("Type");

            tabela_caracteristicas.Columns["ID"].ReadOnly = true;
            tabela_caracteristicas.Columns["Name"].ReadOnly = true;
            tabela_caracteristicas.Columns["Type"].ReadOnly = true;


            Button b = new Button();
            b.Text = "Detaisl";
            int line = 0;
            foreach (Characteristic c in database.Charac.Values)
            {
                string type = "";
                if (c.GetType().ToString().Equals("Business.NumericCharacteristic")) type = "Numeric";
                if (c.GetType().ToString().Equals("Business.QualitativeCharacteristic")) type = "Qualitative";
                if (c.GetType().ToString().Equals("Business.YesNoCharacteristic")) type = "bool";

                tabela_caracteristicas.Rows.Add(null, c.Id, c.Name, type);
                line++;
            }

            DataView v = new DataView(tabela_caracteristicas);

            return v;
        }

        public static DataView tableFinalWeightAHP()
        {
            DataTable pesos = new DataTable();
            pesos.Columns.Add("ID");
            pesos.Columns.Add("Weight");
            foreach (KeyValuePair<string, float> pair in Business.ManagementDataBase.pesosFinaisClassAHP)
            {
                pesos.Rows.Add(pair.Key, pair.Value);
            }

            return new DataView(pesos);

        }

        public static DataView tableFinalWeightSmart()
        {
            DataTable pesos = new DataTable();
            pesos.Columns.Add("ID");
            pesos.Columns.Add("Weight");
            foreach (KeyValuePair<string, float> pair in tabelaSmartNorm)
            {
                pesos.Rows.Add(pair.Key, pair.Value);
            }

            return new DataView(pesos);
        }

        public static DataView refreshTableAHPPriority(string nameC)
        {
            DataTable pesos = new DataTable();
            pesos.Columns.Add(nameC);
            foreach (int id in ids_dos_SoftwareSeleccionados)
            {
                pesos.Columns.Add("" + id);
                pesos.Rows.Add("" + id);
            }

            return new DataView(pesos);
        }



        public static void addIdSoftwareelect(int id)
        {
            ids_dos_SoftwareSeleccionados.Add(id);

        }

        public static int totalSoftwareelect()
        {
            return ids_dos_SoftwareSeleccionados.Count;
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
            return database.infoSoftware_byID(ids_dos_SoftwareSeleccionados);
        }



        public static bool add_characteristics(Business.Characteristic c)
        {
            try
            {
                database.AddCharacteristics(c);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static bool add_software(Business.Software s)
        {
            try
            {
                database.AddSoftware(s);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static bool remove_software(int id)
        {
            try
            {
                database.RemoveSoftware(id);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool remove_characteristics(int id)
        {
            try
            {
                database.RemoveChar(id);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        #region Next ID
        // for generate ID
        public static int next_ID_Characteristics()
        {
            int i = 0;
            foreach (int id in database.Charac.Keys)
            {
                if (id > i) i = id;
            }

            return i + 1;
        }

        public static int next_ID_Software()
        {
            int i = 0;
            foreach (int id in database.Software_list.Keys)
            {
                if (id > i) i = id;
            }

            return i + 1;
        }

        #endregion



        public static string procuraIdCha(string name)
        {
            string r = "";
            foreach (KeyValuePair<int, string> pair in caracteristicas_escolhidas)
            {
                if (pair.Value.Equals(name)) r = "" + pair.Key;
            }

            return r;
        }

        public static Business.Software getSoftware(int id)
        {
            return database.getSoftware(id);
        }

        public static Business.Characteristic getCharacteristics(int id)
        {

            return database.getCharacteristics(id);
        }

        // funções de teste
        // imprimir as características
        public static void mostraCaracterísticas()
        {
            Dictionary<int, Characteristic> cara = database.Charac;
            StringBuilder s = new StringBuilder("***CARACTERISTICAS***\n");

            foreach (Business.Characteristic c in cara.Values)
            {
                s.Append(c.toString());
            }

            MessageBox.Show(s.ToString());

        }


        public static bool existSoftware(int newNumber)
        {
            foreach (int i in database.Software_list.Keys)
            {
                if (newNumber == i) return true;
            }
            return false;
        }

    }
}
