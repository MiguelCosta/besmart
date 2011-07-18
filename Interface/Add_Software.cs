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

            refreshTable();
        }

        public void refreshTable()
        {
            dataGridViewCharacteristics.DataSource = Business.ManagementDataBase.tableAddSoftware();

            System.Windows.Forms.DataGridViewButtonColumn details;
            details = new System.Windows.Forms.DataGridViewButtonColumn();
            dataGridViewCharacteristics.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            details});
 
            details.HeaderText = "Details";
            details.Name = "Details";
            details.Text = "View Details";
            details.UseColumnTextForButtonValue = true;
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

            if (col == 0)
            {
                MessageBox.Show("Falta fazer a janela para apresentar a informação");
            }
            
        }
    }
}
