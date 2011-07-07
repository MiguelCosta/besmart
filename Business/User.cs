using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Business
{
    [Serializable()]
    public class User : ISerializable
    {
        private string _username;
        private string _email;
        private string _password;

        /**
         * Constructor default
         * */
        public User()
        {
            _username = "";
            _email = "";
            _password = "";
        }

        /**
         * Constructor with parameters
         * */
        public User(string username, string email, string password)
        {
            _username = username;
            _email = email;
            _password = password;
        }

        /**
         * Constructor with User
         * */
        public User(User u)
        {
            _username = u.Username;
            _email = u.Email;
            _password = u.Password;
        }

        /**
         * Deserialization Constructor 
         * */
        public User(SerializationInfo info, StreamingContext ctxt)
        {
            _username = (string)info.GetValue("Username", typeof(string));
            _email = (string)info.GetValue("EMail", typeof(string));
            _password = (string)info.GetValue("Password", typeof(string));
        }

        /**
         * Methods get and set
         * */
        public string Username
        {
            get { return _username;  }
            set { _username = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Username", _username);
            info.AddValue("EMail", _email);
            info.AddValue("Password", _password);
        }
        

            /**
             * Method clone
             * */
        public User clone()
        {
            return new User(this);
        }

        public bool equals(Object o)
        {
            if (this == o) return true;
            if (o == null || o.GetType()!= this.GetType()) return false;

            User u = (User) o;
            if (_username.Equals(u.Username) && _email.Equals(u.Email) && _password.Equals(u.Password)) return true;

            return false;
        }

        public string toString()
        {
            StringBuilder s = new StringBuilder("User\n");
            s.Append(_username);
            s.Append("\n");
            s.Append(_email);
            return s.ToString();
        }

    }
}
