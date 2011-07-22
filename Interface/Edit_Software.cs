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
    public partial class Edit_Software : Form
    {
        private int id_software;

        public Edit_Software(int id)
        {
            InitializeComponent();

            id_software = id;

            dataGridViewCharacteristics.CellValidating +=
            new DataGridViewCellValidatingEventHandler
                (dataGridViewCharacteristics_CellValidating);

            dataGridViewCharacteristics.EditingControlShowing +=
            new DataGridViewEditingControlShowingEventHandler
                (dataGridViewCharacteristics_EditingControlShowing);


            refreshTable();
            fillsInformation();
        }

        #region refresh DataGridView
        public void refreshTable()
        {
            DataGridViewTextBoxColumn id = new DataGridViewTextBoxColumn();
            id.HeaderText = "ID";
            id.Name = "ID";
            id.ReadOnly = true;
            DataGridViewTextBoxColumn name = new DataGridViewTextBoxColumn();
            name.HeaderText = "Name";
            name.Name = "Name";
            name.ReadOnly = true;
            DataGridViewTextBoxColumn type = new DataGridViewTextBoxColumn();
            type.HeaderText = "Type";
            type.Name = "Type";
            type.ReadOnly = true;
            DataGridViewButtonColumn details = new DataGridViewButtonColumn();
            details.HeaderText = "Details";
            details.Name = "Details";
            details.Text = "View Details";
            details.UseColumnTextForButtonValue = true;
            details.ReadOnly = true;

            dataGridViewCharacteristics.Columns.Add("Value", "Value");
            dataGridViewCharacteristics.Columns.AddRange(new System.Windows.Forms.DataGridViewTextBoxColumn[] { id });
            dataGridViewCharacteristics.Columns.AddRange(new System.Windows.Forms.DataGridViewTextBoxColumn[] { name });
            dataGridViewCharacteristics.Columns.AddRange(new System.Windows.Forms.DataGridViewTextBoxColumn[] { type });
            dataGridViewCharacteristics.Columns.AddRange(new System.Windows.Forms.DataGridViewButtonColumn[] { details });

            foreach (Business.Characteristic c in Business.ManagementDataBase.database.Charac.Values)
            {
                DataGridViewRow l = new DataGridViewRow();

                //id
                DataGridViewTextBoxCell c_id = new DataGridViewTextBoxCell();
                c_id.Value = "" + c.Id;

                //name
                DataGridViewTextBoxCell c_name = new DataGridViewTextBoxCell();
                c_name.Value = "" + c.Name;

                //type
                string t = "";
                if (c.GetType().ToString().Equals("Business.NumericCharacteristic")) t = "Numeric";
                if (c.GetType().ToString().Equals("Business.QualitativeCharacteristic")) t = "Qualitative";
                if (c.GetType().ToString().Equals("Business.YesNoCharacteristic")) t = "bool";
                DataGridViewTextBoxCell c_type = new DataGridViewTextBoxCell();
                c_type.Value = t;

                //value

                if (c.GetType().ToString().Equals("Business.NumericCharacteristic"))
                {
                    DataGridViewCell c_value;
                    c_value = new DataGridViewTextBoxCell();
                    l.Cells.Add(c_value);
                }
                if (c.GetType().ToString().Equals("Business.QualitativeCharacteristic"))
                {
                    DataGridViewComboBoxCell c_value = new DataGridViewComboBoxCell();
                    c_value.AutoComplete = true;
                    c_value.DisplayStyleForCurrentCellOnly = true;

                    //c_value.ContentClickUnsharesRow();
                    Business.QualitativeCharacteristic qc = (Business.QualitativeCharacteristic)c;
                    foreach (Business.Value v in qc.Values_A.Values)
                    {
                        c_value.Items.Add(v.Name);
                    }
                    c_value.Value = c_value.Items[0];
                    l.Cells.Add(c_value);

                }
                if (c.GetType().ToString().Equals("Business.YesNoCharacteristic"))
                {
                    DataGridViewComboBoxCell c_value = new DataGridViewComboBoxCell();
                    c_value.AutoComplete = true;
                    c_value.DisplayStyleForCurrentCellOnly = true;

                    c_value.Items.Add("true");
                    c_value.Items.Add("false");
                    c_value.Value = c_value.Items[0];
                    l.Cells.Add(c_value);
                }


                l.Cells.Add(c_id);
                l.Cells.Add(c_name);
                l.Cells.Add(c_type);

                dataGridViewCharacteristics.Rows.Add(l);

            }



        }
        #endregion

        private void fillsInformation()
        {
            Business.Software s = Business.ManagementDataBase.getSoftware(id_software);

            textBoxID.Text = ""+s.Id;
            textBoxName.Text = s.Name;
            textBoxLink.Text = s.Link;

            // para cada caracteristica vai preencher o valor correspondente na tabela
            foreach (KeyValuePair<int, string> pair in s.Charac)
            {
                fillsTableCharacteristics(pair.Key, pair.Value);
            }

        }

        // procura na tabela a caracteristica e altera o valor conforme o que esteja no software
        private void fillsTableCharacteristics(int i, string value)
        {
            foreach (DataGridViewRow line in dataGridViewCharacteristics.Rows)
            {
                try
                {
                    int id_charac = System.Convert.ToInt32(line.Cells[1].Value.ToString());
                    if (id_charac == i)
                    {
                        line.Cells[0].Value = value;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("An error occurred!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
            }

        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            textBoxID.Text = "" + Business.ManagementDataBase.next_ID_Software();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("www.google.com");
        }


        private void dataGridViewCharacteristics_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int col = dataGridViewCharacteristics.CurrentCell.ColumnIndex;
            int lin = dataGridViewCharacteristics.CurrentCell.RowIndex;

            //MessageBox.Show("Col: " + col + "\tLine: " + lin);

            if (col == 4)
            {
                MessageBox.Show("Falta fazer a janela para apresentar a informação");
            }

        }


        /// <summary>
        /// Event handler to allow the embedded control to wire
        /// its own event handlers and/or to set desired style
        /// http://www.sommergyll.com/datagridview-usercontrols/datagridview-with-combobox.htm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewCharacteristics_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            DataGridViewComboBoxEditingControl comboControl = e.Control as DataGridViewComboBoxEditingControl;
            if (comboControl != null)
            {
                // Set the DropDown style to get an editable ComboBox
                if (comboControl.DropDownStyle != ComboBoxStyle.DropDown)
                {
                    comboControl.DropDownStyle = ComboBoxStyle.DropDown;
                }
            }
        }

        /// <summary>
        /// This event handler is called when user has finished 
        /// editing/viewing the current cell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewCharacteristics_CellValidating
            (object sender, DataGridViewCellValidatingEventArgs e)
        {

            DataGridViewComboBoxCell cell =
                dataGridViewCharacteristics.CurrentCell as DataGridViewComboBoxCell;

            if (cell != null &&
                !cell.Items.Contains(e.FormattedValue))
            {

                // Insert the new value into position 0
                // in the item collection of the cell
                /*cell.Items.Insert(0, e.FormattedValue);*/
                // When setting the Value of the cell, the  
                // string is not shown until it has been
                // comitted. The code below will make sure 
                // it is committed directly.
                if (dataGridViewCharacteristics.IsCurrentCellDirty)
                {
                    // Ensure the inserted value will 
                    // be shown directly.
                    // First tell the DataGridView to commit 
                    // itself using the Commit context...
                    dataGridViewCharacteristics.CommitEdit
                        (DataGridViewDataErrorContexts.Commit);
                }
                // ...then set the Value that needs 
                // to be committed in order to be displayed directly.
                /*cell.Value = cell.Items[0];*/
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string msg_error = "";
            msg_error += nameCorrect();
            msg_error += idCorrect();
            msg_error += characteristicsCorrect();

            if (msg_error.Equals("") == false)
            {
                MessageBox.Show(msg_error, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string name = textBoxName.Text;
            int id = System.Convert.ToInt32(textBoxID.Text);
            string link = "";
            link += textBoxLink.Text;

            Dictionary<int, string> v = new Dictionary<int, string>();
            foreach (DataGridViewRow line in dataGridViewCharacteristics.Rows)
            {
                int i = System.Convert.ToInt32(line.Cells[1].Value.ToString());
                string value = line.Cells[0].Value.ToString();
                v.Add(i, value);
            }


            bool rem = Business.ManagementDataBase.remove_software(id_software);
            bool add = false;
            // vai remover e depois adicionar
            if (rem)
            {
                Business.Software s = new Business.Software(id, name, link, v);
                add = Business.ManagementDataBase.add_software(s);

            }
            if (add && rem)
            {
                MessageBox.Show("Software edited.", "Software", MessageBoxButtons.OK, MessageBoxIcon.None);
                this.Close();
            }

            else
            {
                msg_error += "Please check if the ID is not being used by another sodtware.";
                MessageBox.Show(msg_error, "Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private string nameCorrect()
        {
            string name = "";
            name += textBoxName.Text;

            if (name.Equals("")) return "Please enter the Name!\n";

            return "";
        }


        private string idCorrect()
        {
            string id = "";
            id += textBoxID.Text;

            if (id.Equals("")) return "Please enter the ID!\n";

            return "";
        }


        private string characteristicsCorrect()
        {
            foreach (DataGridViewRow l in dataGridViewCharacteristics.Rows)
            {

                if (l.Cells[0].Value == null) return "Enter Value in line " + l.Index + "!";
            }

            return "";
        }


        private void dataGridViewCharacteristics_CellValidating_1(object sender, DataGridViewCellValidatingEventArgs e)
        {
            int c = e.ColumnIndex;
            int l = e.RowIndex;

            string type = dataGridViewCharacteristics.Rows[l].Cells[3].Value.ToString();

            int newNumber = 0;

            if (type.Equals("Numeric"))
            {
                if (!int.TryParse(e.FormattedValue.ToString(), out newNumber))
                {
                    dataGridViewCharacteristics.Rows[l].ErrorText = "The Value is not a number!";
                    MessageBox.Show("The Value is not a number!");
                    e.Cancel = true;
                }
                else
                {
                    dataGridViewCharacteristics.Rows[l].ErrorText = null;
                }
            }

        }


        private void textBoxID_Validating(object sender, CancelEventArgs e)
        {
            int newNumber = -1;

            // if empty don't validating
            if (textBoxID.Text == null || textBoxID.Text.Equals("") == true) return;

            if (!int.TryParse(textBoxID.Text, out newNumber))
            {
                MessageBox.Show("The ID is not a number!\n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonClean_Click(object sender, EventArgs e)
        {
            fillsInformation();
        }



    }
}
