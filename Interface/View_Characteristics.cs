﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Interface
{
    public partial class View_Characteristics : Form
    {
        private Add_Characteristics_Numeric num = new Add_Characteristics_Numeric();
        private Add_Characteristics_Yes_No yes_no = new Add_Characteristics_Yes_No();
        private Add_Characteristics_Qualitative qual = new Add_Characteristics_Qualitative();

        private int id_characteristic;

        public View_Characteristics(int id)
        {
            InitializeComponent();

            id_characteristic = id;

            // configurações
            num.Dock = DockStyle.Fill;
            yes_no.Dock = DockStyle.Fill;
            qual.Dock = DockStyle.Fill;

            this.Size = new System.Drawing.Size(this.Size.Width, 300);

            // auto preenche a informação
            fillInformation();

        }

        private void fillInformation()
        {
            Business.Characteristic c = Business.ManagementDataBase.getCharacteristics(id_characteristic);

            string t = "";
            if (c.GetType().ToString().Equals("Business.NumericCharacteristic")) t = "Numeric";
            if (c.GetType().ToString().Equals("Business.QualitativeCharacteristic")) t = "Qualitative";
            if (c.GetType().ToString().Equals("Business.YesNoCharacteristic")) t = "Bool";

            if (t.Equals("Numeric"))
            {
                radioButtonNumeric.Select();
                radioButtonQualitative.Enabled = false;
                radioButtonYesNo.Enabled = false;
                
                num.setId(""+c.Id);
                num.setName(c.Name);
                num.setEnable(false);

            }

            if (t.Equals("Qualitative"))
            {
                radioButtonNumeric.Enabled = false;
                radioButtonQualitative.Select();
                radioButtonYesNo.Enabled = false;
                
                qual.setId("" + c.Id);
                qual.setName(c.Name);
                qual.setValues(c.Id);
                qual.setEnable(false);

            }

            if (t.Equals("Bool"))
            {
                radioButtonNumeric.Enabled = false;
                radioButtonQualitative.Enabled = false;
                radioButtonYesNo.Select();

                yes_no.setId("" + c.Id);
                yes_no.setName(c.Name);
                yes_no.setEnable(false);
            }


        }

        private void radioButtonNumeric_CheckedChanged(object sender, EventArgs e)
        {
            panelCharacteristics.Controls.Clear();
            panelCharacteristics.Controls.Add(num);

        }

        private void radioButtonQualitative_CheckedChanged(object sender, EventArgs e)
        {
            panelCharacteristics.Controls.Clear();
            panelCharacteristics.Controls.Add(qual);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            panelCharacteristics.Controls.Clear();
            panelCharacteristics.Controls.Add(yes_no);
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
            this.Close();
        }


        private string add_characteristics_numeric()
        {
            string msg_error = "";
            string name = num.name();
            int id = num.id();
            int value = 0;

            if (id == -1) msg_error += "ID value is not correct.\n";
            if (name.Equals("")) msg_error += "Name is not correct.\n";

            if (id != -1 && name.Equals("") == false)
            {
                string msg = "The characteristics will be edited. The software will lose these attributes. Want Continue?";
                DialogResult r = MessageBox.Show(msg, "Edit Characteristics", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (r == DialogResult.Yes)
                {
                    bool rem = Business.ManagementDataBase.remove_characteristics(id);
                    if (rem)
                    {
                        Business.Characteristic c = new Business.NumericCharacteristic(id, name, value);
                        bool b = Business.ManagementDataBase.add_characteristics(c);
                        if (b)
                        {
                            MessageBox.Show("Characteristics edited.", "Characteristics", MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                        else
                        {
                            msg_error += "Error adding.\nPlease check if the ID does not exist yet.\n";
                        }
                    }
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
                string msg = "The characteristics will be edited. The software will lose these attributes. Want Continue?";
                DialogResult r = MessageBox.Show(msg, "Edit Characteristics", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (r == DialogResult.Yes)
                {
                    // remove e depois insere
                    bool rem = Business.ManagementDataBase.remove_characteristics(id);
                    // se removeu, vai inserir
                    if (rem)
                    {
                        Business.Characteristic c = new Business.YesNoCharacteristic(id, name, value);
                        bool b = Business.ManagementDataBase.add_characteristics(c);
                        if (b)
                        {
                            MessageBox.Show("Characteristics edited.", "Characteristics", MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                        else
                        {
                            msg_error += "Error adding.\nPlease check if the ID does not exist yet.\n";
                        }
                    }
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
                string msg = "The characteristics will be edited. The software will lose these attributes. Want Continue?";
                DialogResult r = MessageBox.Show(msg, "Edit Characteristics", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (r == DialogResult.Yes)
                {
                    string result = qual.validateValues();

                    // se houver erros
                    if (result.Equals("") == false) msg_error += result;
                    else
                    {
                        bool rem = Business.ManagementDataBase.remove_characteristics(id);
                        if (rem)
                        {
                            Dictionary<String, Business.Value> value = qual.values();
                            Business.Characteristic c = new Business.QualitativeCharacteristic(id, name, value);

                            bool b = Business.ManagementDataBase.add_characteristics(c);

                            if (b)
                            {
                                MessageBox.Show("Characteristics edited.", "Characteristics", MessageBoxButtons.OK, MessageBoxIcon.None);
                            }
                            else
                            {
                                msg_error += "Error adding.\nPlease check if the ID does not exist yet.\n";
                            }
                        }
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


    }
}
