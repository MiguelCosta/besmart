using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace Interface
{

    public partial class chooseProcess : Form
    {
        private Business.DataBaseUser _dataBase;
        public List<int> ids_dos_softwaresSeleccionados;
        public Dictionary<int, string> caracteristicas_escolhidas;
        public Business.DecisionSuport decision;
        public Dictionary<string, float> tabelaSmartNorm;
        public Dictionary<string, float> pesosFinaisClassAHP;
        public string metodo_fase_1 = "smart";

        public Dictionary<int, Dictionary<string, float>> resultFinal;

        public chooseProcess(Business.DataBaseUser dataBase)
        {
            InitializeComponent();
            
            // estruturas auxiliares para calculo da decisão
            decision = new Business.DecisionSuport();
            tabelaSmartNorm = new Dictionary<string, float>();

            _dataBase = dataBase;

            // configurações iniciais
            refreshTableSoftwares();
            refreshTableCaracteristics();
            buttonTestCons.Enabled = false;
            buttonNextDefinitonWeigths.Enabled = false;
            buttonFinish.Enabled = false;
            buttonTestConsitencyAHP.Enabled = false;
            buttonNextChooseSoftwares.Enabled = false;
            // formata as tabelas
            // smart();
        }

        private void refreshTableSoftwares()
        {
            // actualizar a tabela inicial
            DataTable tabela_softwares = new DataTable();
            tabela_softwares.Columns.Add("ID");
            tabela_softwares.Columns.Add("Name");
            tabela_softwares.Columns.Add("Link");

            // adicionar as colunas (nome das caracteristicas)
            foreach (Business.Characteristic c in _dataBase.Charac.Values)
            {
                tabela_softwares.Columns.Add(c.Name);
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
                tabela_softwares.Rows.Add(array);
            }

            // cria uma nova vista para a tabela
            DataView view = new DataView(tabela_softwares);
            dataGridViewTabelaSoftware.DataSource = view;

        }

        private void refreshTableCaracteristics()
        {
            DataTable tabela_caracteristicas = new DataTable();
            tabela_caracteristicas.Columns.Add("ID");
            tabela_caracteristicas.Columns.Add("Name");
            foreach (Business.Characteristic c in _dataBase.Charac.Values)
            {
                tabela_caracteristicas.Rows.Add(c.Id, c.Name);
            }

            DataView view = new DataView(tabela_caracteristicas);
            dataGridViewCharacteristics.DataSource = view;
        }

        private void refreshTableSmart()
        {
            DataTable pesos = new DataTable();
            pesos.Columns.Add("ID");
            pesos.Columns.Add("Name");
            pesos.Columns["ID"].ReadOnly = true;
            pesos.Columns["Name"].ReadOnly = true;
            foreach (KeyValuePair<int, string> pair in caracteristicas_escolhidas)
            {
                pesos.Rows.Add(pair.Key, pair.Value);
            }

            DataView view = new DataView(pesos);
            dataGridViewSmart.DataSource = view;
        }

        private void refreshTableAHP()
        {
            DataTable pesos = new DataTable();
            pesos.Columns.Add("Best Software");
            foreach (string name in caracteristicas_escolhidas.Values)
            {
                pesos.Columns.Add(name);
                pesos.Rows.Add(name);
            }

            DataView view = new DataView(pesos);
            dataGridViewAHP.DataSource = view;

            int i = 0;
            int num_ca = caracteristicas_escolhidas.Count;

            while (i < num_ca)
            {
                dataGridViewAHP[i + 1, i].Value = "1";
                i++;
            }
        }

        private void FormChooseProcess_FormClosing(object sender, EventArgs e)
        {
            Close();
        }


        private void viewSoftwareWebpageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsultWebpage cwp = new ConsultWebpage(_dataBase);
            cwp.Show();

        }

        public void loadObject(String filename)
        {
            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter bformatter = new BinaryFormatter();

            _dataBase = (Business.DataBaseUser)bformatter.Deserialize(stream);
            stream.Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "beSmart files (*.beSmart)|*.beSmart|All files (*.*)|*.*";
            DialogResult ret = o.ShowDialog();
            String filename = o.FileName;

            if (ret == DialogResult.OK)
            {
                loadObject(filename);
                refreshTableSoftwares();
                refreshTableCaracteristics();
                //MessageBox.Show("Agora já deve estar...!");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void editSoftwareListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditSWList editList = new EditSWList(_dataBase);
            editList.Show();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog();
            s.Filter = "beSmart files (*.beSmart)|*.beSmart|All files (*.*)|*.*";
            DialogResult ret = s.ShowDialog();

            if (ret == DialogResult.OK)
            {
                string name = s.FileName;
                _dataBase.saveInObject(name);

            }
        }

        private void buttonNextChooseSoftwares_Click(object sender, EventArgs e)
        {
            // para apagar a lista já existente
            ids_dos_softwaresSeleccionados = new List<int>();

            // mensagem que vai aparecer dos softwares seleccionados
            string linhas_selecionadas = "Select Softwares ID:\n";

            // vai a todas as linhas das tabelas ver quais estão seleccionadas
            foreach (DataGridViewRow linha in dataGridViewTabelaSoftware.Rows)
            {
                // seleccionada apenas as linhas que tem o checbox activo
                if (linha.Cells[0].Value != null && (bool)linha.Cells[0].Value == true)
                {
                    // convert para int o ID
                    int id = System.Convert.ToInt32(linha.Cells[1].Value);
                    // nome do software
                    string name = linha.Cells[2].Value.ToString();
                    // adiciona o id do software à lista de software
                    ids_dos_softwaresSeleccionados.Add(id);
                    // adiciona à msg o software
                    linhas_selecionadas += id + "\t" + name + "\n";
                }
            }

            // condição para se ter de seleccionar mais de 2 softwares
            if (ids_dos_softwaresSeleccionados.Count < 2 || ids_dos_softwaresSeleccionados.Count > 16)
            {
                MessageBox.Show("Select between 2 and 16 softwares!");
            }
            else
            {
                // apresenta os softwares seleccionados
                MessageBox.Show(linhas_selecionadas);
                // selecciona o separador seguinte
                tabControlSeparates.SelectedTab = tabPageChooseCriteria;
                // aumenta a barra de progresso
                progressBar1.Value = 25;
            }
        }

        private void buttonViewWebPage_Click(object sender, EventArgs e)
        {
            ConsultWebpage cwp = new ConsultWebpage(_dataBase);
            cwp.Show();
        }

        private void buttonPreviewToSoftwares_Click(object sender, EventArgs e)
        {
            tabControlSeparates.SelectedTab = tabPageChooseSoftwares;
            progressBar1.Value = 0;
        }

        private void buttonNextChooseCriteria_Click(object sender, EventArgs e)
        {
            // apagar a estrutura
            caracteristicas_escolhidas = new Dictionary<int, string>();
            caracteristicas_escolhidas.Clear();


            string linhas_selecionadas = "Select Characteristics ID:\n";

            // vai a todas as linhas das tabelas ver quais estão seleccionadas
            foreach (DataGridViewRow linha in dataGridViewCharacteristics.Rows)
            {
                if (linha.Cells[0].Value != null)
                {

                    // convert para int o ID
                    int id = System.Convert.ToInt32(linha.Cells[1].Value);
                    string name = (string)linha.Cells[2].Value;
                    caracteristicas_escolhidas.Add(id, name);
                    linhas_selecionadas += id + "\n";
                }
            }
            //MessageBox.Show(linhas_selecionadas);

            // condição para se ter de seleccionar mais de 2 softwares
            if (caracteristicas_escolhidas.Count < 1)
            {
                MessageBox.Show("Select at least one characteristics!");
            }
            else
            {
                tabControlSeparates.SelectedTab = tabPageClassificaoes;
                progressBar1.Value = 50;
                refreshTableSmart();
                refreshTableAHP();
            }


        }

        private void buttonPreviewDefiniotWeigths_Click(object sender, EventArgs e)
        {
            tabControlSeparates.SelectedTab = tabPageChooseCriteria;
            progressBar1.Value = 25;
        }

        private void buttonNextDefinitonWeigths_Click(object sender, EventArgs e)
        {
            DataTable carc = new DataTable();
            carc.Columns.Add("ID");
            carc.Columns.Add("Name");
            foreach (KeyValuePair<int, string> pair in caracteristicas_escolhidas)
            {
                carc.Rows.Add(pair.Key, pair.Value);
            }

            DataView view = new DataView(carc);
            dataGridViewCaracteristicasPrioridades.DataSource = view;


            tabControlSeparates.SelectedTab = tabPageDefinitionPriorities;
            progressBar1.Value = 75;
        }

        private string procuraIdCha(string name)
        {
            string r = "";
            foreach (KeyValuePair<int, string> pair in caracteristicas_escolhidas)
            {
                if (pair.Value.Equals(name)) r = "" + pair.Key;
            }

            return r;
        }


        private void buttonCalFinalWe_Click(object sender, EventArgs e)
        {
            int flag = 0;
            foreach (DataGridViewColumn coluna in dataGridViewAHP.Columns)
            {
                if (flag == 0)
                {
                    flag = 1;
                }
                else
                {
                    string name = coluna.Name.ToString();
                    string idA = procuraIdCha(name);
                    foreach (DataGridViewRow linha in dataGridViewAHP.Rows)
                    {
                        string nameB = linha.Cells[0].Value.ToString();
                        string idB = procuraIdCha(nameB);
                        string pointsStr = linha.Cells[name].Value.ToString();
                        float pointf = (float)System.Convert.ToDouble(pointsStr);
                        //MessageBox.Show("idA: " + idA + "\tName: " + name + "\nIDB: " + idB + "\tNameB: " + nameB + "\nPoints: " + pointf);
                        decision.registerClassAHP(idA, idB, pointf);

                    }
                }
            }

            Dictionary<string, Dictionary<string, float>> tabelaNormAHP = new Dictionary<string, Dictionary<string, float>>();
            tabelaNormAHP = decision.normalizeAHP(decision.TableAHP);
            pesosFinaisClassAHP = new Dictionary<string, float>();
            pesosFinaisClassAHP = decision.pesosFinais(tabelaNormAHP);


            DataTable pesos = new DataTable();
            pesos.Columns.Add("ID");
            pesos.Columns.Add("Weight");
            foreach (KeyValuePair<string, float> pair in pesosFinaisClassAHP)
            {
                pesos.Rows.Add(pair.Key, pair.Value);
            }

            DataView view = new DataView(pesos);
            dataGridViewPesosAHP.DataSource = view;


            /*
            foreach (KeyValuePair<string, double> pair in pesosFinaisClassAHP)
            {
                MessageBox.Show(pair.Key + "\t" + pair.Value);
            }*/

            // activa o butão de consistência
            buttonTestCons.Enabled = true;
            buttonCalcSmart.Enabled = false;

            metodo_fase_1 = "ahp";
        }



        private void button2_Click(object sender, EventArgs e)
        {
            int linha = dataGridViewCaracteristicasPrioridades.CurrentRow.Index;
            if (linha >= 0)
            {
                string id = dataGridViewCaracteristicasPrioridades["ID", linha].Value.ToString();
                string name = dataGridViewCaracteristicasPrioridades["Name", linha].Value.ToString();
                //MessageBox.Show(id + "\t" + name);
                labelCaracteristicaValueFn.Text = name;
                labelCaracteristicaValueFnID.Text = id;

                labelIDAHP.Text = id;
                labelName_AHP.Text = name;

                refreshTableAHPPriority(name);
            }

            buttonCalculateValueFn.Enabled = true;
            buttonCalcPrioAHP.Enabled = true;
            buttonTestConsitencyAHP.Enabled = false;
            dataGridViewValueFn.DataSource = null;
            dataGridViewPesosAHPFinais.DataSource = null;
        }

        private void refreshTableAHPPriority(string nameC)
        {
            DataTable pesos = new DataTable();
            pesos.Columns.Add(nameC);
            foreach (int id in ids_dos_softwaresSeleccionados)
            {
                pesos.Columns.Add("" + id);
                pesos.Rows.Add("" + id);
            }

            DataView view = new DataView(pesos);
            dataGridViewAHPPriority.DataSource = view;

            int i = 0;
            int num_ca = ids_dos_softwaresSeleccionados.Count;

            while (i < num_ca)
            {
                dataGridViewAHPPriority[i + 1, i].Value = "1";
                i++;
            }
        }

        private void buttonCalculateValueFn_Click(object sender, EventArgs e)
        {
            // maximizar
            if (radioButtonMaximize.Checked)
            {

            }

            // maximizar
            if (radioButtonMinimize.Checked)
            {

            }

        }



        private void buttonCalculateValueFn_Click_1(object sender, EventArgs e)
        {
            buttonCalcPrioAHP.Enabled = false;
            decision.TableSW = _dataBase.softwaresWithCaracteristics(ids_dos_softwaresSeleccionados);

            Dictionary<string, Dictionary<string, int>> tableFilter = new Dictionary<string, Dictionary<string, int>>();

            string id_carac = labelCaracteristicaValueFnID.Text;
            tableFilter = decision.filter(id_carac);
            /*
            string erro = "nada";

            foreach (KeyValuePair<string, Dictionary<string, int>> p in tableFilter)
            {
                erro += "\n" + p.Key;
                foreach (KeyValuePair<string, int> p2 in p.Value)
                {
                    erro += "\n\t" + p2.Key + " " + "int " + p2.Value;
                }

            }
            
            MessageBox.Show(erro);
            */
            int min = decision.calMin(id_carac, tableFilter);
            int max = decision.calMax(id_carac, tableFilter);

            //MessageBox.Show("Min: " + min + "\tMax: " + max);

            Dictionary<string, float> aux = new Dictionary<string, float>();
            // maximizar
            if (radioButtonMaximize.Checked)
            {
                aux = decision.calValueMax(min, max, tableFilter);

            }

            // maximizar
            if (radioButtonMinimize.Checked)
            {
                aux = decision.calValueMin(min, max, tableFilter);
            }

            decision.registerPriority(id_carac, aux);

            DataTable prioridades = new DataTable();
            prioridades.Columns.Add("ID");
            prioridades.Columns.Add("Priority");

            Dictionary<string, float> a;

            decision.TableResult.TryGetValue(id_carac, out a);
            foreach (KeyValuePair<string, float> pair2 in a)
            {
                prioridades.Rows.Add(pair2.Key, pair2.Value);
            }



            DataView view = new DataView(prioridades);
            dataGridViewValueFn.DataSource = view;

            buttonFinish.Enabled = true;


        }

        private void buttonCalcPrioAHP_Click(object sender, EventArgs e)
        {
            buttonCalculateValueFn.Enabled = false;
            int flag = 0;
            foreach (DataGridViewColumn coluna in dataGridViewAHPPriority.Columns)
            {
                if (flag == 0)
                {
                    flag = 1;
                }
                else
                {
                    string idSofA = coluna.Name.ToString();
                    foreach (DataGridViewRow linha in dataGridViewAHPPriority.Rows)
                    {
                        string idSofB = linha.Cells[0].Value.ToString();
                        //MessageBox.Show(idSofB);
                        string pointsStr = linha.Cells[idSofA].Value.ToString();
                        float pointf = (float)System.Convert.ToDouble(pointsStr);
                        //MessageBox.Show("idA: " + idA + "\tName: " + name + "\nIDB: " + idB + "\tNameB: " + nameB + "\nPoints: " + pointf);
                        decision.registerPriorAHP(labelIDAHP.Text, idSofA, idSofB, pointf);

                    }
                }
            }

            Dictionary<string, Dictionary<string, Dictionary<string, float>>> tabelaNormAHP = new Dictionary<string, Dictionary<string, Dictionary<string, float>>>();
            tabelaNormAHP = decision.normalizePriorityAHP(decision.TablePriorAHP);
            decision.pesosPriorFinais(tabelaNormAHP);

            DataTable prioridades = new DataTable();
            prioridades.Columns.Add("ID");
            prioridades.Columns.Add("Priority");

            Dictionary<string, float> a;

            decision.TableResult.TryGetValue(labelIDAHP.Text, out a);
            foreach (KeyValuePair<string, float> pair2 in a)
            {
                prioridades.Rows.Add(pair2.Key, pair2.Value);
            }

            DataView view = new DataView(prioridades);
            dataGridViewPesosAHPFinais.DataSource = view;


            /*
            foreach (KeyValuePair<string, double> pair in pesosFinaisClassAHP)
            {
                MessageBox.Show(pair.Key + "\t" + pair.Value);
            }*/

            // activa o butão de consistência
            buttonTestConsitencyAHP.Enabled = true;

        }

        private void buttonTestConsitencyAHP_Click(object sender, EventArgs e)
        {
            Dictionary<int, double> matrixC = new Dictionary<int, double>();
            Dictionary<int, double> matrixD = new Dictionary<int, double>();
            Dictionary<string, Dictionary<string, float>> aux;
            Dictionary<string, float> aux1;
            decision.TablePriorAHP.TryGetValue(labelIDAHP.Text, out aux);
            decision.TableResult.TryGetValue(labelIDAHP.Text, out aux1);
            matrixC = decision.calculaMatrizC(aux, aux1);
            matrixD = decision.calculaMatrizD(matrixC, aux1);
            double taxa = decision.taxaConsitencia(matrixD);

            if (taxa <= 0.10)
            {
                MessageBox.Show("The consistency Rate is good: " + taxa);
                labelAHPPrioCons.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(81)))), ((int)(((byte)(19)))));
            }
            else
            {
                MessageBox.Show("The consistency Rate is bad: " + taxa);
                labelAHPPrioCons.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            }

            // actualiza a label com a taxa
            labelAHPPrioCons.Text = "" + taxa;

            buttonFinish.Enabled = true;
        }

        private void buttonFinish_Click(object sender, EventArgs e)
        {
            resultFinal = new Dictionary<int, Dictionary<string, float>>();
            if (metodo_fase_1.Equals("smart"))
            {
                resultFinal = decision.analiseFinalSmart(tabelaSmartNorm, decision.TableResult);
            }

            if (metodo_fase_1.Equals("ahp"))
            {
                resultFinal = decision.analiseFinalAHP(pesosFinaisClassAHP, decision.TableResult);
            }


            DataTable final = new DataTable();
            final.Columns.Add("RANK");
            final.Columns.Add("Software");
            final.Columns.Add("Priority");


            foreach (KeyValuePair<int, Dictionary<string, float>> pair in resultFinal)
            {
                Dictionary<string, float> a;
                resultFinal.TryGetValue(pair.Key, out a);
                foreach (KeyValuePair<string, float> pair2 in a)
                {
                    final.Rows.Add(pair.Key, pair2.Key, pair2.Value);
                }
            }

            DataView view = new DataView(final);
            dataGridViewFinal.DataSource = view;

            tabControlSeparates.SelectedTab = tabPageFinal;
            progressBar1.Value = 100;
        }

        private void buttonTestCons_Click(object sender, EventArgs e)
        {
            Dictionary<int, double> matrixC = new Dictionary<int, double>();
            Dictionary<int, double> matrixD = new Dictionary<int, double>();



            matrixC = decision.calculaMatrizC(decision.TableAHP, pesosFinaisClassAHP);
            matrixD = decision.calculaMatrizD(matrixC, pesosFinaisClassAHP);
            double taxa = decision.taxaConsitencia(matrixD);

            if (taxa <= 0.10)
            {
                MessageBox.Show("The consistency Rate is good: " + taxa);
                labelConsistencyRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(81)))), ((int)(((byte)(19)))));
            }
            else
            {
                MessageBox.Show("The consistency Rate is bad: " + taxa);
                labelConsistencyRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            }

            // actualiza a label com a taxa
            labelConsistencyRate.Text = "" + taxa;
            // activa o botão next
            buttonNextDefinitonWeigths.Enabled = true;
        }

        private void buttonCalcSmart_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow linha in dataGridViewSmart.Rows)
            {
                string idChar = linha.Cells[1].Value.ToString();
                int points = System.Convert.ToInt32(linha.Cells[0].Value.ToString());
                decision.registerClass(idChar, points);
            }

            tabelaSmartNorm.Clear();
            tabelaSmartNorm = decision.normalizeSMART(decision.TableCH);


            DataTable pesos = new DataTable();
            pesos.Columns.Add("ID");
            pesos.Columns.Add("Weight");
            foreach (KeyValuePair<string, float> pair in tabelaSmartNorm)
            {
                pesos.Rows.Add(pair.Key, pair.Value);
            }

            DataView view = new DataView(pesos);
            dataGridViewPesosFinaisSmart.DataSource = view;

            buttonNextDefinitonWeigths.Enabled = true;
            buttonCalFinalWe.Enabled = false;

            metodo_fase_1 = "smart";
        }

        private void aHPTutorialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = "C:\\HTML_tutoraials\\AHPtutorial.htm";
            Tutorials t = new Tutorials(url);
            t.Show();
        }

        private void sMARTTutorialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = "C:\\HTML_tutoraials\\SMARTtutorial.htm";
            Tutorials t = new Tutorials(url);
            t.Show();
        }

        private void valueFNTutorialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = "C:\\HTML_tutoraials\\ValueFntutorial.htm";
            Tutorials t = new Tutorials(url);
            t.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox a = new AboutBox();
            a.Show();
        }

        private void startANewComparationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ids_dos_softwaresSeleccionados = new List<int>();
            caracteristicas_escolhidas = new Dictionary<int, string>();
            tabelaSmartNorm = new Dictionary<string, float>();
            pesosFinaisClassAHP = new Dictionary<string, float>();
            refreshTableSoftwares();
            refreshTableCaracteristics();
            ids_dos_softwaresSeleccionados.Clear();
            caracteristicas_escolhidas.Clear();
            decision.TableCH.Clear();
            decision.TableAHP.Clear();
            decision.TableResult.Clear();
            tabelaSmartNorm.Clear();
            pesosFinaisClassAHP.Clear();

            buttonCalFinalWe.Enabled = true;
            buttonNextChooseSoftwares.Enabled = true;
            buttonCalcSmart.Enabled = true;
            buttonTestCons.Enabled = false;
            buttonCalculateValueFn.Enabled = true;
            buttonTestConsitencyAHP.Enabled = false;
            buttonCalcPrioAHP.Enabled = true;


            dataGridViewPesosAHP.DataSource = null;
            dataGridViewPesosFinaisSmart.DataSource = null;
            dataGridViewPesosAHPFinais.DataSource = null;
            dataGridViewValueFn.DataSource = null;
            dataGridViewFinal.DataSource = null;
            
            progressBar1.Value = 0;

            labelAHPPrioCons.Text = "consitencia";
            labelIDAHP.Text = "ID";
            labelName_AHP.Text = "name";
            labelCaracteristicaValueFnID.Text = "ID";
            labelConsistencyRate.Text = "";
            labelCaracteristicaValueFn.Text = "name";
            tabControlSeparates.SelectedTab = tabPageChooseSoftwares;

        }

        private void buttonNextChooseSoftwares_MouseEnter(object sender, EventArgs e)
        {
            buttonNextChooseSoftwares.ForeColor = System.Drawing.Color.Blue;
        }

        private void buttonNextChooseSoftwares_MouseLeave(object sender, EventArgs e)
        {
            buttonNextChooseSoftwares.ForeColor = System.Drawing.Color.Black;
        }

        private void buttonViewWebPage_MouseEnter(object sender, EventArgs e)
        {
            buttonViewWebPage.Font = new Font(buttonViewWebPage.Font, FontStyle.Bold);
            buttonViewWebPage.ForeColor = System.Drawing.Color.Blue;
        }

        private void buttonViewWebPage_MouseLeave(object sender, EventArgs e)
        {
            buttonViewWebPage.Font = new Font(buttonViewWebPage.Font, FontStyle.Regular);
            buttonViewWebPage.ForeColor = System.Drawing.Color.Black;
        }

    }
}
