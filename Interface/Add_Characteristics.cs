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
        private Add_Characteristics_Qualitative qual = new Add_Characteristics_Qualitative();

        public Add_Characteristics()
        {
            InitializeComponent();

            // configurações
            num.Dock = DockStyle.Fill;
            yes_no.Dock = DockStyle.Fill;
            qual.Dock = DockStyle.Fill;

            string info = "To add a new characteristic please select its type and fill in its name and identification.";
            label_info.Text = info;

            this.Size = new System.Drawing.Size(500, 400);

        }


        private void radioButtonNumeric_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonNumeric.Checked == true)
            {
                panelCharacteristics.Controls.Clear();
                panelCharacteristics.Controls.Add(num);
            }

        }

        private void radioButtonQualitative_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonQualitative.Checked == true)
            {
                panelCharacteristics.Controls.Clear();
                panelCharacteristics.Controls.Add(qual);
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                panelCharacteristics.Controls.Clear();
                panelCharacteristics.Controls.Add(yes_no);
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            string msg_error = "";
            if (panelCharacteristics.Controls.Contains(num))
            {
                msg_error = add_characteristics_numeric();
            }


            if (panelCharacteristics.Controls.Contains(yes_no))
            {
                msg_error = add_characteristics_yes_no();
            }

            if (panelCharacteristics.Controls.Contains(qual))
            {
                msg_error = add_characteristics_qualitatice();
            }

            if (msg_error.Equals("") == false) MessageBox.Show(msg_error, "Characteristics", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        private string add_characteristics_numeric()
        {
            string msg_error = "";
            string name = num.name();
            int id = num.id();
            int value = 0;

            if (id == -1) msg_error += "ID value is not correct.\n";
            if (value == -1) msg_error += "Default Value is not correct.\n";
            if (name.Equals("")) msg_error += "Name is not correct.\n";

            if (id != -1 && value != -1 && name.Equals("") == false)
            {
                Business.Characteristic c = new Business.NumericCharacteristic(id, name, value);
                bool b = Business.ManagementDataBase.add_characteristics(c);
                if (b)
                {
                    MessageBox.Show("Characteristics added.", "Characteristics", MessageBoxButtons.OK, MessageBoxIcon.None);
                    this.Close();
                }
                else
                {
                    msg_error += "Error adding.\nPlease check if the ID does not exist yet.\n";
                }
            }

            return msg_error;
        }

        private string add_characteristics_yes_no()
        {
            string msg_error = "";
            string name = yes_no.name();
            int id = yes_no.id();
            bool value = false;

            if (id == -1) msg_error += "ID value is not correct.\n";
            if (name.Equals("")) msg_error += "Name is not correct.\n";

            if (id != -1 && name.Equals("") == false)
            {
                Business.Characteristic c = new Business.YesNoCharacteristic(id, name, value);
                bool b = Business.ManagementDataBase.add_characteristics(c);
                if (b)
                {
                    MessageBox.Show("Characteristics added.", "Characteristics", MessageBoxButtons.OK, MessageBoxIcon.None);
                    this.Close();
                }
                else
                {
                    msg_error += "Error adding.\nPlease check if the ID does not exist yet.\n";
                }

            }
            return msg_error;
        }

        private string add_characteristics_qualitatice()
        {
            string msg_error = "";
            string name = qual.name();
            int id = qual.id();

            if (id == -1) msg_error += "ID value is not correct.\n";
            if (name.Equals("")) msg_error += "Name is not correct.\n";

            if (id != -1 && name.Equals("") == false)
            {
                string result = qual.validateValues();

                // se houver erros
                if (result.Equals("") == false) msg_error += result;
                else
                {
                    Dictionary<String, Business.Value> value = qual.values();
                    Business.Characteristic c = new Business.QualitativeCharacteristic(id, name, value);

                    bool b = Business.ManagementDataBase.add_characteristics(c);

                    if (b)
                    {
                        MessageBox.Show("Characteristics added.", "Characteristics", MessageBoxButtons.OK, MessageBoxIcon.None);
                        this.Close();
                    }
                    else
                    {
                        msg_error += "Error adding.\nPlease check if the ID does not exist yet.\n";
                    }
                }
            }
            return msg_error;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonClean_Click(object sender, EventArgs e)
        {
            num.clean();
            qual.clean();
            yes_no.clean();
        }

        private void Add_Characteristics_Load(object sender, EventArgs e)
        {
        }






    }
}
