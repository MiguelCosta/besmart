using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Interface
{
    public partial class Add_Characteristics_Numeric : UserControl
    {
        public Add_Characteristics_Numeric()
        {
            InitializeComponent();
        }


        
        public int id()
        {
            string id_t = textBoxID.Text;
            try
            {
                int i = System.Convert.ToInt32(id_t);
                return i;
            }
            catch (Exception)
            {
                MessageBox.Show("Numeric Value incorrect!");
            }

            return -1;
        }


        public string name()
        {
            return textBoxName.Text;
        }


        public int value()
        {
            string value_t = textBoxValue.Text;
            try
            {
                int i = System.Convert.ToInt32(value_t);
                return i;
            }
            catch (Exception)
            {
                MessageBox.Show("Numeric Value incorrect!");
            }

            return -1;
        }
    }
}
