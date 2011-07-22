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
    public partial class Add_Characteristics_Yes_No : UserControl
    {
        public Add_Characteristics_Yes_No()
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

        public void clean()
        {
            textBoxID.Clear();
            textBoxName.Clear();
        }

        private void buttonGenerateID_Click(object sender, EventArgs e)
        {
            int id = Business.ManagementDataBase.next_ID_Characteristics();
            textBoxID.Text = "" + id;
        }

        private void buttonGenerateID_Click_1(object sender, EventArgs e)
        {
            int id = Business.ManagementDataBase.next_ID_Characteristics();
            textBoxID.Text = "" + id;
        }


    }
}
