using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Business
{
    [Serializable()]
    public class QualitativeCharacteristic : Characteristic, ISerializable
    {
        private Dictionary<string, Value> _values;

        /**
         * Constructor default
         * */
        public QualitativeCharacteristic() :
            base(0, "")
        {
            _values = new Dictionary<string, Value>();
        }

        /**
         * Constructor with parameters
         * */
        public QualitativeCharacteristic(int id, string name, Dictionary<string, Value> values) :
            base(id, name)
        {
            _values = values;
        }

        /**
         * Constructor with Qualitative_Characteristic
         * */

        public QualitativeCharacteristic(QualitativeCharacteristic nc) :
            base(nc.Id, nc.Name)
        {
            _values = nc.Values_A;
        }

        /**
         * Deserialization Constructor 
         * */
        public QualitativeCharacteristic(SerializationInfo info, StreamingContext ctxt) :
            base(info, ctxt)  {
                _values = (Dictionary<string, Value>)info.GetValue("Values", typeof(Dictionary<string, Value>));
        }

        public Dictionary<string, Value> Values_A
        {
            get { return _values; }
            set { _values = value; }
        }

        public void addValue(Value v)
        {
            _values.Add(v.Name, v);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
            info.AddValue("Values", _values);
        }

        /**
         * Method clone
         * */
        public QualitativeCharacteristic clone()
        {
            return new QualitativeCharacteristic(this);
        }

        public bool equals(Object o)
        {
            if (this == o) return true;
            if (o == null || o.GetType() != this.GetType()) return false;

            QualitativeCharacteristic n = (QualitativeCharacteristic)o;

            if (_id == n.Id && _name.Equals(n.Name) && _values.Equals(n.Values_A)) return true;

            return false;
        }

        public override string toString()
        {
            StringBuilder s = new StringBuilder("Characteristic\n");
            s.Append(_name);
            s.Append("\n");
            s.Append(_id);
            s.Append("\n");
            foreach(Value v in _values.Values)
            {
                s.Append(v.Name);
                s.Append("\t");
                s.Append(v.Classification);
                s.Append("\n");
            }
            return s.ToString();
        }
    }
}
