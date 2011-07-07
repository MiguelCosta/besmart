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
    public partial class EditSWList : Form
    {
        public Business.DataBaseUser _dataBase;
        DataTable _tabela_softwares;

        public EditSWList(Business.DataBaseUser dataBase)
        {
            InitializeComponent();

            _dataBase = dataBase;

            refreshTableSoftwares();
        }



        private void refreshTableSoftwares()
        {
            // actualizar a tabela inicial
            _tabela_softwares = new DataTable();
            _tabela_softwares.Columns.Add("ID");
            _tabela_softwares.Columns.Add("Name");
            _tabela_softwares.Columns.Add("Link");

            // adicionar as colunas (nome das caracteristicas)
            foreach (Business.Characteristic c in _dataBase.Charac.Values)
            {
                _tabela_softwares.Columns.Add(c.Name);
            }

            // adiciona as linhas (info dos softwares)
            foreach (Business.Software s in _dataBase.Software_list.Values)
            {
                // coloca todas as caracteristicas numa List
                List<string> values = new List<string>();
                values.Add("" + s.Id);
                values.Add(s.Name);
                values.Add(s.Link);
                foreach (string cV in s.Charac.Values)
                {
                    values.Add(cV);
                }
                // passa para um array, para ser possivel adicionar uma linha
                string[] array = values.ToArray();
                _tabela_softwares.Rows.Add(array);
            }

            // cria uma nova vista para a tabela
            DataView view = new DataView(_tabela_softwares);
            dataGridViewTabelaSoftware.DataSource = view;

        }


        private void dataGridViewTabelaSoftware_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
            string m = "Whta type of characteristic do want to insert?\n1 - Boolean\n2 - Numeric\n3 - Qualitative";
            string r = Microsoft.VisualBasic.Interaction.InputBox(m, "Insert Caracteristics");

            if (r.Equals("1"))
            {
                string x = Microsoft.VisualBasic.Interaction.InputBox("What is the name?", "Insert Caracteristics");
                _tabela_softwares.Columns.Add(x);

                DataView view = new DataView(_tabela_softwares);
                dataGridViewTabelaSoftware.DataSource = view;
            }

            if (r.Equals("2"))
            {
                string x = Microsoft.VisualBasic.Interaction.InputBox("What is the name?", "Insert Caracteristics");
                _tabela_softwares.Columns.Add(x);

                DataView view = new DataView(_tabela_softwares);
                dataGridViewTabelaSoftware.DataSource = view;
            }

            if (r.Equals("3"))
            {
                string x = Microsoft.VisualBasic.Interaction.InputBox("What is the name?\nvalue-valueorder;value-valueorder\nExample:bad-0;good-1\n", "Insert Caracteristics");
                _tabela_softwares.Columns.Add(x);

                DataView view = new DataView(_tabela_softwares);
                dataGridViewTabelaSoftware.DataSource = view;
            }

        }
    }
}
