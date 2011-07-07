using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Business
{
    [Serializable()]
    public class Software : ISerializable
    {
        private int _id;
        private string _name;
        private string _link;
        private Dictionary<int, string> _charac;

        /**
         * Constructor default
         * */
        public Software()
        {
            _id = 0;
            _name = "";
            _link = "";
            _charac = new Dictionary<int, string>();
        }

        /**
         * Constructor with parameters
         * */
        public Software(int id, string name, string link, Dictionary<int, string> charac)
        {
            _id = id;
            _name = name;
            _link = link;
            _charac = charac;
        }

        /**
         * Constructor with Software
         * */
        public Software(Software s)
        {
            _id = s.Id;
            _name = s.Name;
            _link = s.Link;
            _charac = s.Charac;
        }

        /**
         * Deserialization Constructor 
         * */
        public Software(SerializationInfo info, StreamingContext ctxt)
        {
            _id = (int)info.GetValue("Id", typeof(int));
            _name = (string)info.GetValue("Name", typeof(string));
            _link = (string)info.GetValue("Link", typeof(string));
            _charac = (Dictionary<int, string>)info.GetValue("Charac", typeof(Dictionary<int, string>));
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

        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }

        public Dictionary<int, string> Charac
        {
            get { return _charac; }
            set { _charac = value; }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Id", _id);
            info.AddValue("Name", _name);
            info.AddValue("Link", _link);
            info.AddValue("Charac", _charac);
        }

        /**
        * Method clone
        * */

        public Software clone()
        {
            return new Software(this);
        }

        public bool equals(Object o)
        {
            if (this == o) return true;
            if (o == null || o.GetType() != this.GetType()) return false;

            Software s = (Software)o;
            if (_id == s.Id && _name.Equals(s.Name) && _link.Equals(s.Link) && _charac.Equals(s.Charac)) return true;

            return false;
        }

        public string toString()
        {
            StringBuilder s = new StringBuilder("Software\n");
            s.Append(_id);
            s.Append("\n");
            s.Append(_name);
            s.Append("\n");
            s.Append(_link);
            s.Append("\n");

            foreach (KeyValuePair<int, string> pair in _charac)
            {
                s.Append(pair.Key);
                s.Append("\t");
                s.Append(pair.Value);
                s.Append("\n");
            }
            return s.ToString();
        }
    }
}
