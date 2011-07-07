using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Windows.Forms;

namespace Business
{
    [Serializable()]
    public class DataBaseUser : ISerializable
    {
        private User _user;
        private Dictionary<int, Software> _software_list;
        private Dictionary<int, Characteristic> _charac;

        /**
         * Constructor default
         * */
        public DataBaseUser()
        {
            _user = new User();
            _software_list = new Dictionary<int, Software>();
            _charac = new Dictionary<int, Characteristic>();
        }

        /**
         * Constructor with parameters
         * */
        public DataBaseUser(User user, Dictionary<int, Software> software_list, Dictionary<int, Characteristic> charac)
        {
            _user = user;
            _software_list = software_list;
            _charac = charac;
        }

        /**
         * Constructor with Value
         * */
        public DataBaseUser(DataBaseUser db)
        {
            _user = db.User;
            _software_list = db.Software_list;
            _charac = db.Charac;
        }

        /**
         * Deserialization Constructor 
         * */
        public DataBaseUser(SerializationInfo info, StreamingContext ctxt)
        {
            _user = (User)info.GetValue("User", typeof(User));
            _software_list = (Dictionary<int, Software>)info.GetValue("Software_List", typeof(Dictionary<int, Software>));
            _charac = (Dictionary<int, Characteristic>)info.GetValue("Charac", typeof(Dictionary<int, Characteristic>));
        }

        public User User
        {
            get { return _user; }
            set { _user = value; }
        }

        public Dictionary<int, Software> Software_list
        {
            get { return _software_list; }
            set { _software_list = value; }
        }

        public Dictionary<int, Characteristic> Charac
        {
            get { return _charac; }
            set { _charac = value; }
        }

        public void AddSoftware(Software s)
        {
            _software_list.Add(s.Id, s);
        }

        public void RemoveSoftware(int id)
        {
            _software_list.Remove(id);
        }

        public Software ViewSoftware(Software s)
        {
            return s;
            //ESTE MÉTODO AINDA NÃO ESTÁ IMPLEMENTADO
        }

        public void RemoveChar(int id)
        {
            _charac.Remove(id);
        }

        public void filterDB()
        {
            //ESTE MÉTODO AINDA NÃO ESTÁ IMPLEMENTADO
        }

        public void saveInObject(String filename)
        {
            Stream stream = File.Open(filename, FileMode.Create);
            BinaryFormatter bformatter = new BinaryFormatter();

            bformatter.Serialize(stream, this);
            stream.Close();
        }
        /* * O MÉTODO SEGUINTE SERVE PARA CARREGAR O FICHEIRO
         * PROVAVELMENTE NÃO PODE ESTAR NESTA CLASSE
         * DEVE TER DE ESTAR NA CLASSE ONDE SE CARREGAM FICHEIROS (NA INTERFACE?)
         * Aí, deve-se trocar o this para o nome da variável...
         * O Código vai comentado devido ao erro óbvio
         * */
        /*
        public void loadObject(String filename)
        {
            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter bformatter = new BinaryFormatter();

            this = (DataBaseUser)bformatter.Deserialize(stream);
            stream.Close();
        }*/




        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("User", _user);
            info.AddValue("Charac", _charac);
            info.AddValue("Software_List", _software_list);
        }


        public string toString()
        {
            StringBuilder s = new StringBuilder("DATA BASE\n");
            s.Append(User.toString());
            s.Append("\n\nSOFTWARES:\n");
            foreach (Software soft in _software_list.Values)
            {
                s.Append(soft.toString());
            }
            s.Append("\n\nCHARACTERISTICS:\n");
            foreach (Characteristic c in _charac.Values)
            {
                s.Append(c.toString());
            }
            return s.ToString();
        }



        // quero o id do software com todas as caracteristicas associadas
        public Dictionary<string, Dictionary<string, int>> softwaresWithCaracteristics(List<int> ids_softwares)
        {
            // resultado
            Dictionary<string, Dictionary<string, int>> r = new Dictionary<string, Dictionary<string, int>>();

            // tenho de ir a todos os softwares da lista
            foreach (int id_s in ids_softwares)
            {
                // vou buscar um software com um id da lista
                Software s = null;
                _software_list.TryGetValue(id_s, out s);

                // agora tenho de ir buscar todas as caracteristicas desse software
                Dictionary<int, string> carac_sof = new Dictionary<int, string>();
                carac_sof = s.Charac;

                // vai guardar temporariamente as caracteristicas do software
                Dictionary<string, int> carcteristicas_do_software = new Dictionary<string, int>();

                // depois de ter as caracteristicas é necessário buscar o valor ou valor de ordem
                foreach (KeyValuePair<int, string> pair in carac_sof)
                {
                    int value_order = valueCaracteristics(pair.Key, pair.Value);

                    carcteristicas_do_software.Add("" + pair.Key, value_order);
                }

                // depois de ir buscar toda as caracteristicas vai inserir na estrutura final
                r.Add("" + id_s, carcteristicas_do_software);

            }

            /*
            string result = "";
            foreach (KeyValuePair<String, Dictionary<string, int>> pair in r)
            {
                result += "\nSoftware: " + pair.Key;
                foreach (KeyValuePair<string, int> pair2 in pair.Value)
                {
                    result += "\n\tID: " + pair2.Key + "\tValue: " + pair2.Value;
                }
            }
            MessageBox.Show(result);
            */
            return r;
        }

        // recebe uma caracteristica e atribui um valor de ordem
        private int valueCaracteristics(int id, string value)
        {
            int r = 0;
            Characteristic c = null;
            _charac.TryGetValue(id, out c);
            //MessageBox.Show("Valor:" + value + "\nTipo de Caracteristica: " + c.GetType().Name);
            if (c.GetType().Name.Equals("QualitativeCharacteristic"))
            {
                //MessageBox.Show("Qualidative");
                return valurOrderCaracteristicsQualitative(id, value);
            }

            if (c.GetType().Name.Equals("YesNoCharacteristic"))
            {
                //MessageBox.Show("YesNo");
                if (value.Equals("false"))
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }

            if (c.GetType().Name.Equals("NumericCharacteristic"))
            {
                //MessageBox.Show("Numeric: " + value);
                return System.Convert.ToInt32(value);
            }

            return r;
        }

        // para uma caracteristica Qualitative devolve o valor de ordem
        private int valurOrderCaracteristicsQualitative(int id_carac, string value)
        {
            int r = 0;
            Characteristic c = null;
            _charac.TryGetValue(id_carac, out c);
            QualitativeCharacteristic q = new QualitativeCharacteristic();
            q = (QualitativeCharacteristic)c;
            Dictionary<string, Value> values_c = q.Values_A;


            foreach (Value v in values_c.Values)
            {
                if (value.Equals(v.Name)) return v.Classification;
            }


            return r;
        }

    }

}
