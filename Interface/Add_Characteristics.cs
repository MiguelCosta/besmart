using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Interface
{
    public partial class Add_Characteristics : Form
    {
        private Add_Characteristics_Numeric num = new Add_Characteristics_Numeric();
        private Add_Characteristics_Yes_No yes_no = new Add_Characteristics_Yes_No();

        public Add_Characteristics()
        {
            InitializeComponent();

            // configurações
            num.Dock = DockStyle.Fill;
            yes_no.Dock = DockStyle.Fill;
        }


        private void radioButtonNumeric_CheckedChanged(object sender, EventArgs e)
        {
            panelCharacteristics.Controls.Clear();
            panelCharacteristics.Controls.Add(num);
        }

        private void radioButtonQualitative_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            panelCharacteristics.Controls.Clear();
            panelCharacteristics.Controls.Add(yes_no);
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (panelCharacteristics.Controls.Contains(num))
            {
                string name = num.name();
                int id = num.id();
                int value = num.value();

                if (id != -1 && value != -1 && name.Equals("") == false)
                {
                    Business.Characteristic c = new Business.NumericCharacteristic(id, name, value);
                    bool b = Business.ManagementDataBase.add_characteristics(c);
                    if (b)
                    {
                        MessageBox.Show("Characteristics added.", "Characteristics", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else
                    {
                        string message = "Error adding.\nPlease check if the ID does not exist yet.";
                        MessageBox.Show(message, "Characteristics", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }


            if (panelCharacteristics.Controls.Contains(yes_no))
            {
                
            }
            

        }

        

        

        
    }
}
