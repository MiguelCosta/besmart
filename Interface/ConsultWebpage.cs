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
    public partial class ConsultWebpage : Form
    {
        private Business.DataBaseUser _dataBase;
        public ConsultWebpage(Business.DataBaseUser dataBase)
        {
            InitializeComponent();
            _dataBase = dataBase;

            refreshTable();
        }

        public void refreshTable()
        {
            // actualizar a tabela inicial
            DataTable tabela_softwares = new DataTable();
            tabela_softwares.Columns.Add("ID");
            tabela_softwares.Columns.Add("Name");
            tabela_softwares.Columns.Add("Link");

            // adiciona as linhas (info dos softwares)
            foreach (Business.Software s in _dataBase.Software_list.Values)
            {
                // coloca todas as caracteristicas numa List
                List<string> values = new List<string>();
                values.Add("" + s.Id);
                values.Add(s.Name);
                values.Add(s.Link);

                // passa para um array, para ser possivel adicionar uma linha
                string[] array = values.ToArray();
                tabela_softwares.Rows.Add(array);
            }

            // cria uma nova vista para a tabela
            DataView view = new DataView(tabela_softwares);
            dataGridViewSimpleSoftware.DataSource = view;
        }

        private void ConsultWebpage_FormClosing(Object sender, FormClosingEventArgs e) 
        {
            Close();
        }

        private void ConsultWebpage_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'lI4DataSet.software' table. You can move, or remove it, as needed.
            this.softwareTableAdapter.Fill(this.lI4DataSet.software);

        }

        private void editSoftwareListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditSWList editList = new EditSWList(_dataBase);
            editList.Show();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.ShowDialog();
        }

        private void dataGridViewSimpleSoftware_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int linha = dataGridViewSimpleSoftware.CurrentRow.Index;
            if (linha >= 0)
            {
                string cellValue = dataGridViewSimpleSoftware["Link", linha].Value.ToString();
                webBrowser.Navigate(cellValue);
            }
        }


    }
}
