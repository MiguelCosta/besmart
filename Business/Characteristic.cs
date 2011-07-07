using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Business
{
    [Serializable()]
    public abstract class Characteristic : ISerializable
    {
        protected int _id;
        protected string _name;

        /**
         * Constructor default
         * */
        public Characteristic()
        {
            _id = 0;
            _name = "";
        }

        /**
         * Constructor with parameters
         * */
        public Characteristic(int id, string name)
        {
            _id = id;
            _name = name;
        }

        /**
         * Constructor with Characteristic
         * */
        public Characteristic(Characteristic c)
        {
            _id = c.Id;
            _name = c.Name;
        }


        /**
         * Deserialization Constructor 
         * */
        public Characteristic(SerializationInfo info, StreamingContext ctxt)
        {
            _id = (int)info.GetValue("Id", typeof(int));
            _name = (String)info.GetValue("Name", typeof(string));
        }


        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Id", _id);
            info.AddValue("Name", _name);
        }

        public abstract string toString();
    }
}
