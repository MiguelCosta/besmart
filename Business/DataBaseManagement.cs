using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Business
{
    [Serializable()]
    public class DataBaseManagement : ISerializable
    {
        private DataBaseUser _db;
        private Dictionary<string, Dictionary<string, int>> _tableSW;

        /**
         * Constructor default
         * */
        public DataBaseManagement()
        {
            _db = new DataBaseUser();
            _tableSW = new Dictionary<string,Dictionary<string,int>>();
        }

        /**
         * Constructor with parameters
         * */
        public DataBaseManagement(DataBaseUser db, Dictionary<string, Dictionary<string, int>> tableSW)
        {
            _db = db;
            _tableSW = tableSW;
        }

        /**
         * Constructor with Value
         * */
        public DataBaseManagement(DataBaseManagement dbm)
        {
            _db = dbm.DB;
            _tableSW = dbm.TableSW;
        }

        /**
         * Deserialization Constructor 
         * */
        public DataBaseManagement(SerializationInfo info, StreamingContext ctxt)
        {
            _db = (DataBaseUser)info.GetValue("DB", typeof(DataBaseUser));
            _tableSW = (Dictionary<string, Dictionary<string, int>>)info.GetValue("TableSW", typeof(Dictionary<string, Dictionary<string, int>>));
        }


        public DataBaseUser DB
        {
            get { return _db; }
            set { _db = value; }
        }

        public Dictionary<string, Dictionary<string, int>> TableSW
        {
            get { return _tableSW; }
            set { _tableSW = value; }
        }

        public DataBaseUser GetChangableDB()
        {
            return _db;
            //ESTE MÉTODO AINDA NÃO ESTÁ IMPLEMENTADO
        }

        public void showChangableDB()
        {
            //ESTE MÉTODO AINDA NÃO ESTÁ IMPLEMENTADO
        }

        public void ValidateChanges()
        {
            //ESTE MÉTODO AINDA NÃO ESTÁ IMPLEMENTADO
        }

        public void ReadChar()
        {
            //ESTE MÉTODO AINDA NÃO ESTÁ IMPLEMENTADO
        }

        public void ValidateChar(Characteristic charac)
        {
             //ESTE MÉTODO AINDA NÃO ESTÁ IMPLEMENTADO
        }

        public void SaveDB()
        {
            //ESTE MÉTODO AINDA NÃO ESTÁ IMPLEMENTADO
        }

        public DataBaseUser NewDataBase(DataBaseUser db)
        {
            //?????
            return new DataBaseUser(db);
        }

        public Dictionary<string, Dictionary<string, int>> FilterDB(Dictionary<string, Software> software_list)
        {
            foreach (string s in software_list.Keys)
            {

            }
            //ESTE MÉTODO AINDA NÃO FAZ COISAS
            return _tableSW;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("DB", _db);
            info.AddValue("TableSW", _tableSW);
        }
        
    }
}
