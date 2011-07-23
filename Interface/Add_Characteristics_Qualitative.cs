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
    public partial class Add_Characteristics_Qualitative : UserControl
    {

        public Add_Characteristics_Qualitative()
        {
            InitializeComponent();


            string msg = "Please describe the rating scale for your  new qualitative characteristic.";
            label_info.Text = msg;

        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            int id = Business.ManagementDataBase.next_ID_Characteristics();
            textBoxID.Text = "" + id;
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


        public Dictionary<string, Business.Value> values()
        {
            Dictionary<string, Business.Value> r = new Dictionary<string, Business.Value>();

            foreach (DataGridViewRow line in dataGridViewQualitativeValues.Rows)
            {
                if (line.Cells[0].Value != null)
                {
                    string name = line.Cells[0].Value.ToString();
                    int classification = System.Convert.ToInt32(line.Cells[1].Value.ToString());
                    Business.Value v = new Business.Value(name, classification);
                    r.Add(name, v);
                }
            }
            return r;
        }

        public void setId(string id)
        {
            textBoxID.Text = id;
        }

        public void setName(string name)
        {
            textBoxName.Text = name;
        }

        public void setValues(int id)
        {
            Business.QualitativeCharacteristic c = (Business.QualitativeCharacteristic) Business.ManagementDataBase.getCharacteristics(id);
            foreach(Business.Value v in c.Values_A.Values)
            {
                dataGridViewQualitativeValues.Rows.Add(v.Name, v.Classification);
            }
        }

        public void setEnable(bool enable)
        {
            textBoxID.Enabled = enable;
            textBoxName.Enabled = enable;
            buttonGenerate.Enabled = enable;
            dataGridViewQualitativeValues.ReadOnly = !enable;
        }

        public void clean()
        {
            textBoxID.Clear();
            textBoxName.Clear();
            dataGridViewQualitativeValues.Rows.Clear();

        }

        public string validateValues()
        {
            string s = "";
            int num_line = 1;

            List<string> list_names = new List<string>();
            List<string> list_classification = new List<string>();

            foreach (DataGridViewRow line in dataGridViewQualitativeValues.Rows)
            {
                line.ErrorText = null;

                // erro se uma das linhas tem uma coluna preenchida e outra não
                if (line.Cells[0].Value == null && line.Cells[1].Value != null)
                {
                    s += "In line " + num_line + " insert Value.\n";
                    line.ErrorText += "\nInsert Value.";
                    return s;
                }
                if (line.Cells[0].Value != null && line.Cells[1].Value == null)
                {
                    line.ErrorText += "\nInsert Value Order.";
                    s += "In line " + num_line + " insert Value Order.\n";
                    return s;
                }

                // erro se houver valores repetidos
                if (line.Cells[0].Value != null)
                {
                    string name = line.Cells[0].Value.ToString();
                    string classification = line.Cells[1].Value.ToString();

                    // value repetidos
                    if (list_names.Contains(name))
                    {
                        s += name + " there more than once.\n";
                        line.ErrorText += s + "\n";
                        return s;
                    }
                    else list_names.Add(name);

                    // values order repetidos
                    if (list_classification.Contains(classification))
                    {
                        s += "In Order Value there can be no repeated values​​.\n";
                        line.ErrorText += "\nIn Order Value there can be no repeated values​​.";
                        return s;
                    }
                    else list_classification.Add(classification);

                    // value order não é numerico
                    try
                    {
                        System.Convert.ToInt32(classification);
                    }
                    catch (Exception)
                    {
                        s += "In line" + num_line + " Order Value is not numeric.\n";
                        line.ErrorText += s + "\n";
                        return s;
                    }

                }

                num_line++;
            }

            // é preciso ter duas ou mais caracteristicas, 3 porque começa a 1 e é incrementado mais uma vez do que devia
            if (num_line <= 3)
            {
                s += "Please insert more values.\n";
                return s;
            }

            return s;
        }

    }
}
