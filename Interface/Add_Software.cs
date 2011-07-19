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
    public partial class Add_Software : Form
    {
        public Add_Software()
        {
            InitializeComponent();


            dataGridViewCharacteristics.CellValidating +=
            new DataGridViewCellValidatingEventHandler
                (dataGridViewCharacteristics_CellValidating);

            dataGridViewCharacteristics.EditingControlShowing +=
            new DataGridViewEditingControlShowingEventHandler
                (dataGridViewCharacteristics_EditingControlShowing);


            refreshTable();
        }

        public void refreshTable()
        {
            DataGridViewTextBoxColumn id = new DataGridViewTextBoxColumn();
            id.HeaderText = "ID";
            id.Name = "ID";
            id.ReadOnly = true;
            DataGridViewTextBoxColumn name = new DataGridViewTextBoxColumn();
            name.HeaderText = "Name";
            name.Name = "Name";
            id.ReadOnly = true;
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
                    c_value.Items.Add("True");
                    c_value.Items.Add("False");
                    c_value.Value = c_value.Items[0];
                    l.Cells.Add(c_value);
                }


                l.Cells.Add(c_id);
                l.Cells.Add(c_name);
                l.Cells.Add(c_type);

                dataGridViewCharacteristics.Rows.Add(l);

            }



        }



        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            textBoxID.Text = "" + Business.ManagementDataBase.next_ID_Software();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {

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

            if (col == 0)
            {


            }

        }


        /// <summary>
        /// Event handler to allow the embedded control to wire
        /// its own event handlers and/or to set desired style
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


    }
}
