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
    public partial class EditSWList : Form
    {

        public EditSWList()
        {
            InitializeComponent();

            refreshTableSoftware();
            refreshTableCharacteristics();
        }



        private void refreshTableSoftware()
        {
            dataGridViewTabelaSoftware.DataSource = Business.ManagementDataBase.tableSoftwareSimple();
        }

        private void refreshTableCharacteristics()
        {
            dataGridViewCharacteristicsList.DataSource = Business.ManagementDataBase.tableCharacteristics();

        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.ShowDialog();
        }

        private void viewSoftwareWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ConsultWebpage consultwp = new ConsultWebpage();
            //consultwp.Show();
        }

        private void editSoftwareListToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void buttonAddCharacteristics_Click(object sender, EventArgs e)
        {
            Add_Characteristics a = new Add_Characteristics();
            a.ShowDialog();
            refreshTableSoftware();
            refreshTableCharacteristics();
        }

        private void buttonAddnew_Click(object sender, EventArgs e)
        {
            Add_Software a = new Add_Software();
            a.ShowDialog();
            refreshTableSoftware();
            refreshTableCharacteristics();
        }

        private void dataGridViewTabelaSoftware_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow line in dataGridViewTabelaSoftware.Rows)
            {
                if (line.Cells[0].Value != null && line.Cells[0].Value.ToString().Equals("True"))
                {
                    line.Selected = true;
                }
                if (line.Cells[0].Value != null && line.Cells[0].Value.ToString().Equals("False"))
                {
                    line.Selected = false;
                }
            }
        }

        private void dataGridViewTabelaSoftware_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow line in dataGridViewTabelaSoftware.Rows)
            {
                if (line.Cells[0].Value != null && line.Cells[0].Value.ToString().Equals("True"))
                {
                    line.Selected = true;
                }
                if (line.Cells[0].Value != null && line.Cells[0].Value.ToString().Equals("False"))
                {
                    line.Selected = false;
                }
            }

        }

        private void dataGridViewCharacteristicsList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow line in dataGridViewCharacteristicsList.Rows)
            {
                if (line.Cells[0].Value != null && line.Cells[0].Value.ToString().Equals("True"))
                {
                    line.Selected = true;
                }
                if (line.Cells[0].Value != null && line.Cells[0].Value.ToString().Equals("False"))
                {
                    line.Selected = false;
                }
            }
        }

        private void dataGridViewCharacteristicsList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow line in dataGridViewCharacteristicsList.Rows)
            {
                if (line.Cells[0].Value != null && line.Cells[0].Value.ToString().Equals("True"))
                {
                    line.Selected = true;
                }
                if (line.Cells[0].Value != null && line.Cells[0].Value.ToString().Equals("False"))
                {
                    line.Selected = false;
                }
            }
        }

        private void buttonEditSoftware_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow line in dataGridViewTabelaSoftware.Rows)
            {
                if (line.Cells[0].Value != null && line.Cells[0].Value.ToString().Equals("True"))
                {
                    try
                    {
                        int id = System.Convert.ToInt32(line.Cells[1].Value.ToString());
                        Edit_Software d = new Edit_Software(id);
                        d.ShowDialog();
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            refreshTableSoftware();
            refreshTableCharacteristics();
        }

        private void buttonDeleteSoftware_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow line in dataGridViewTabelaSoftware.Rows)
            {
                if (line.Cells[0].Value != null && line.Cells[0].Value.ToString().Equals("True"))
                {
                    try
                    {
                        int id = System.Convert.ToInt32(line.Cells[1].Value.ToString());
                        string msg = "Are you sure you want to remove the software " + id + "?\nThe information can not be recovered.";
                        DialogResult r = MessageBox.Show(msg, "Delete Software", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (r == DialogResult.Yes)
                        {
                            Business.ManagementDataBase.remove_software(id);
                            MessageBox.Show("Software deleted.", "Delete Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            refreshTableSoftware();
            refreshTableCharacteristics();
        }

        private void buttonEditCharacteristics_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow line in dataGridViewCharacteristicsList.Rows)
            {
                if (line.Cells[0].Value != null && line.Cells[0].Value.ToString().Equals("True"))
                {
                    try
                    {
                        int id = System.Convert.ToInt32(line.Cells[1].Value.ToString());
                        Edit_Characteristics edit = new Edit_Characteristics(id);
                        edit.ShowDialog();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error convert to int!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }
            }

            refreshTableSoftware();
            refreshTableCharacteristics();
        }


        private void buttonDeleteCharacteristics_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow line in dataGridViewCharacteristicsList.Rows)
            {
                if (line.Cells[0].Value != null && line.Cells[0].Value.ToString().Equals("True"))
                {
                    try
                    {
                        int id = System.Convert.ToInt32(line.Cells[1].Value.ToString());
                        string msg = "Want to remove characteristics " + id + "?";
                        DialogResult r = MessageBox.Show(msg, "Delete Characteristics", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (r == DialogResult.Yes)
                        {
                            msg = "Characteristic deleted!";
                            Business.ManagementDataBase.remove_characteristics(id);
                            MessageBox.Show(msg, "Delete Characteristics", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error convert to int!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }

            }

            refreshTableSoftware();
            refreshTableCharacteristics();
        }


    }
}
