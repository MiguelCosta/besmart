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
            catch (Exception) { }

            return -1;
        }


        public string name()
        {
            return textBoxName.Text;
        }

        public void setId(string id)
        {
            textBoxID.Text = id;
        }

        public void setName(string name)
        {
            textBoxName.Text = name;
        }

        public void setEnable(bool enable)
        {
            textBoxID.Enabled = enable;
            textBoxName.Enabled = enable;
            buttonGenerate.Enabled = enable;
        }


        public void clean()
        {
            textBoxID.Clear();
            textBoxName.Clear();
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            textBoxID.Text = ""+ Business.ManagementDataBase.next_ID_Characteristics();
        }
    }
}
