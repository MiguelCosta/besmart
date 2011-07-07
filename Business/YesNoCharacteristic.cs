using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Business
{
    [Serializable()]
    public class YesNoCharacteristic : Characteristic, ISerializable
    {
        private bool _state;

        /**
         * Constructor default
         * */
        public YesNoCharacteristic():
            base(0,"") {
                _state = false;
        }

        /**
         * Constructor with parameters
         * */
        public YesNoCharacteristic(int id, string name, bool state):
            base(id, name) {
                _state = state;
        }

        /**
         * Constructor with YesNo_Characteristic
         * */

        public YesNoCharacteristic(YesNoCharacteristic nc) :
            base(nc.Id, nc.Name) {
            _state = nc.State;
        }

        /**
         * Deserialization Constructor 
         * */
        public YesNoCharacteristic(SerializationInfo info, StreamingContext ctxt) :
            base(info, ctxt)  {
            _state = (bool)info.GetValue("State", typeof(bool));
        }


        public bool State
        {
            get { return _state; }
            set { _state = value; }
        }

        public void setState()
        {
            if (_state) _state = false;
            else _state = true;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
            info.AddValue("State", _state);
        }
        

        /**
         * Method clone
         * */

        public YesNoCharacteristic clone()
        {
            return new YesNoCharacteristic(this);
        }

        public override string toString()
        {
            StringBuilder s = new StringBuilder("Characteristic\n");
            s.Append(_name);
            s.Append("\n");
            s.Append(_id);
            s.Append("\n");
            s.Append(_state);
            s.Append("\n");
            return s.ToString();
        }



    }
}
