﻿using System;
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
using ZedGraph;
using System.Collections;


namespace Interface {

    public partial class chooseProcess : Form {
        int indexSperate = 0;
        int selectCharacteristics_row = 0;
        string selectCharacteristics_id = "";
        Init inicial = new Init();

        string _file;

        public chooseProcess(string file) {
            InitializeComponent();

            init();

            _file = file;

            openFile();

            inicial.ShowDialog();
        }

        private void openFile() {
            try {
                Business.ManagementDataBase.loadObject(_file);

                // ->>>> alterar isto par um evento, quando a base de dados muda faz refresh das tabelas
                refreshTableSoftware();
                refreshTableCaracteristics();
            } catch(Exception) { }


        }

        private void init() {
            // configurações iniciais
            refreshTableSoftware();
            refreshTableCaracteristics();
            buttonNextDefinitonWeigths.Enabled = false;
            buttonFinish.Enabled = false;
            buttonNextChooseSoftware.Enabled = false;
            dataGridViewTabelaSoftware.Columns[0].Visible = false;

            info();

        }

        private void info() {
            string info1 = "";
            info1 += "For new Comparation: ";
            info1 += "\n1 - Software -> Start New Comparation (Ctrl+N)";
            info1 += "\n2 - Choose between 2 up 16 software you want to be part of the decision process.";
            info1 += "\n3 - Click Next.";
            label_info1.Text = info1;

            string infoChooseCriteria = "";
            infoChooseCriteria += "Choose at least one characteristic to be classified.";
            label_infoChooseCriteria.Text = infoChooseCriteria;

            string definitionOfWeigths = "";
            definitionOfWeigths += "Here you have to define the weights for each characteristic you selected before.";
            definitionOfWeigths += "\nChoose between Smart and AHP method. To learn how the methods work , see the tutorials in Help menu.";
            label_DefinitionOfWeigths.Text = definitionOfWeigths;

            string definitionOfWeightsSmart = "";
            definitionOfWeightsSmart += "Please give 10 points to the  characteristic you consider the least important. To other characteristics give the points according to the first ranked (feature which gave 10 points).";
            definitionOfWeightsSmart += "\nThen is calculated the final weights and got a  table with normalized values.";
            definitionOfWeightsSmart += "\nFinally press next button.";
            label_DefinitionOfWeightsSmart.Text = definitionOfWeightsSmart;

            string definitionOfWeightsAHP = "";
            definitionOfWeightsAHP += "This table pretends to describe the relation between all characteristics chosen.";
            definitionOfWeightsAHP += "\nThe main diagonal of the table associates the same two characteristics, so  is automatically filled.";
            definitionOfWeightsAHP += "\nHere you have to  fill in the part of the table above the main diagonal, and give points to each criterion concerning other.";
            definitionOfWeightsAHP += "\nYou may adopt your own scale or consider  the scale described in AHP Tutorial.";
            definitionOfWeightsAHP += "\nThe part of the table below the main diagonal is automatically fill in  with the inverse values previously assigned.";
            definitionOfWeightsAHP += "\nThen is calculate final weights and got a  table with normalized values, named final weight matrix. After that, is estimated the consistency rate of  this matrix.";
            definitionOfWeightsAHP += "\nIf the value of consistency is good (written in green), you can proceed. If the consistency is bad (written in red), you should change the values to get a better result, or proceed anyway.";
            definitionOfWeightsAHP += "\nFinally press next button.";
            label_DefinitionOfWeightsAHP.Text = definitionOfWeightsAHP;


            string definitionOfPriorities = "";
            definitionOfPriorities += "Here you have to define the priorities for each characteristic you selected before.";
            definitionOfPriorities += "\nFirst select the desired criterion from the table. Then, choose between ValueFn and AHP method. You must do this for each characteristic.";
            definitionOfPriorities += "\nWhen all criteria are classified, you can finish the process pressing the Finish button.";
            definitionOfPriorities += "\nTo learn how the methods work , see the tutorials in Help menu.";
            label_DefinitionOfPriorities.Text = definitionOfPriorities;

            string definitionOfPriotitiesValueFn = "";
            definitionOfPriotitiesValueFn += "Select the option as you want to maximize or minimize the criterion to get the values of the priorities.";
            label_DefinitionOfPrioritiesValueFn.Text = definitionOfPriotitiesValueFn;

            string definitionOfPrioritiesAHP = "";
            definitionOfPrioritiesAHP += "The table pretends to describe the relation between all characteristics chosen.";
            definitionOfPrioritiesAHP += "\nThe main diagonal of the table associates the same two characteristics, so  is automatically filled.";
            definitionOfPrioritiesAHP += "\nHere you have to  fill the part of the table above the main diagonal, and give points to each criterion concerning other.";
            definitionOfPrioritiesAHP += "\nYou may adopt your own scale or consider  the scale described in AHP Tutorial.";
            label_DefinitionOfPrioritiesAHP.Text = definitionOfPrioritiesAHP;
        }

        #region Refresh Tables

        private void refreshTableSoftware() {
            dataGridViewTabelaSoftware.DataSource = Business.ManagementDataBase.tableSoftware(false);
            dataGridViewTabelaSoftware.Columns["Link"].Visible = false;

            if(!dataGridViewTabelaSoftware.Columns.Contains("Link2")) {
                DataGridViewLinkColumn links = new DataGridViewLinkColumn();
                links.UseColumnTextForLinkValue = true;
                links.Name = "LinkClick";
                links.HeaderText = "Link";
                links.ActiveLinkColor = Color.White;
                links.LinkBehavior = LinkBehavior.SystemDefault;
                links.LinkColor = Color.Blue;
                links.TrackVisitedState = true;
                links.VisitedLinkColor = Color.Gray;

                dataGridViewTabelaSoftware.Columns.Add(links);
            }

        }
        private void refreshTableCaracteristics() {
            dataGridViewCharacteristics.DataSource = Business.ManagementDataBase.tableCharacteristics();
            dataGridViewCharacteristics.Columns["ID"].Visible = false;
        }

        #endregion

        #region File

        // open file
        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "beSmart files (*.beSmart)|*.beSmart|All files (*.*)|*.*";
            DialogResult ret = o.ShowDialog();
            String filename = o.FileName;

            if(ret == DialogResult.OK) {
                try {
                    Business.ManagementDataBase.loadObject(filename);

                    // ->>>> alterar isto par um evento, quando a base de dados muda faz refresh das tabelas
                    refreshTableSoftware();
                    refreshTableCaracteristics();
                } catch(Exception) {
                    MessageBox.Show("Error Open File", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) {
            SaveFileDialog s = new SaveFileDialog();
            s.Filter = "beSmart files (*.beSmart)|*.beSmart|All files (*.*)|*.*";
            DialogResult ret = s.ShowDialog();

            if(ret == DialogResult.OK) {
                string name = s.FileName;
                Business.ManagementDataBase.database.saveInObject(name);
            }
        }
        #endregion

        #region Edit information
        private void editSoftwareListToolStripMenuItem_Click(object sender, EventArgs e) {
            EditSWList editList = new EditSWList();
            editList.ShowDialog();
            refreshTableSoftware();
            refreshTableCaracteristics();
        }

        #endregion

        #region New Data Base
        private void newToolStripMenuItem_Click(object sender, EventArgs e) {
            string msg = "Want to create a new Data Base?\nThe information has not saved will be lost.";
            DialogResult r = MessageBox.Show(msg, "New Data Base", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

            if(r == DialogResult.Yes) {
                Business.ManagementDataBase.database = new Business.DataBaseUser();
                init();
            }

        }
        #endregion

        #region Report/suggestion
        private void reportErrorToolStripMenuItem_Click(object sender, EventArgs e) {
            ReportError r = new ReportError();
            r.Show();
        }
        #endregion

        #region Button Next

        private void buttonNextChooseCriteria_Click(object sender, EventArgs e) {
            // limpar a estrutura
            Business.ManagementDataBase.caracteristicas_escolhidas = new Dictionary<int, string>();
            Business.ManagementDataBase.caracteristicas_escolhidas.Clear();

            // vai a todas as linhas das tabelas ver quais estão seleccionadas
            foreach(DataGridViewRow linha in dataGridViewCharacteristics.Rows) {
                if(linha.Cells[0].Value != null && (bool)linha.Cells[0].Value == true) {
                    int id = System.Convert.ToInt32(linha.Cells[1].Value);
                    string name = (string)linha.Cells[2].Value;
                    Business.ManagementDataBase.caracteristicas_escolhidas.Add(id, name);
                }
            }

            // condição para se ter de seleccionar pelo menos uma caracteristica
            if(Business.ManagementDataBase.caracteristicas_escolhidas.Count < 1) {
                MessageBox.Show("Select at least one characteristics!");
            } else {
                buttonNextChooseCriteria_message();
                tabControlSeparates.SelectedTab = tabPageClassificaoes;
                indexSperate = tabControlSeparates.SelectedIndex;
                progressBar1.Value = 50;
                buttonNextDefinitonWeigths.Enabled = false;
                refreshTableSmart();
                refreshTableAHP();
            }
        }

        // messagem que deve aparecer quando se clica no next e aparece sucesso
        private void buttonNextChooseCriteria_message() {
            string message = "Select Characteristics:\n";

            foreach(KeyValuePair<int, string> pair in Business.ManagementDataBase.caracteristicas_escolhidas) {
                message += pair.Key + "\t" + pair.Value + "\n";
            }

            MessageBox.Show(message, "Characteristics", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void buttonNextChooseSoftware_Click(object sender, EventArgs e) {
            try {

                Business.ManagementDataBase.ids_dos_SoftwareSeleccionados = new List<int>();

                foreach(DataGridViewRow linha in dataGridViewTabelaSoftware.Rows) {
                    // seleccionada apenas as linhas que tem o checbox activo
                    if(linha.Cells[0].Value != null && (bool)linha.Cells[0].Value == true) {
                        int id = System.Convert.ToInt32(linha.Cells["ID"].Value);
                        string name = linha.Cells["Name"].Value.ToString();

                        Business.ManagementDataBase.addIdSoftwareelect(id);
                    }
                }

                // verifica se os softwares tem todas as caracteristicas preenchidas
                foreach(int id_s in Business.ManagementDataBase.ids_dos_SoftwareSeleccionados) {
                    Business.Software s = Business.ManagementDataBase.getSoftware(id_s);

                    foreach(int id_c in Business.ManagementDataBase.database.Charac.Keys) {
                        if(!s.Charac.ContainsKey(id_c)) {
                            string msg = "The software selected not all characteristics are completed!\nPlease edit the information in: Software-> Edit Software List.";
                            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                }

                if(Business.ManagementDataBase.totalSoftwareelect() < 2 || Business.ManagementDataBase.totalSoftwareelect() > 16) {
                    MessageBox.Show("Select between 2 and 16 Software!");
                } else {
                    // apresenta os Software seleccionados
                    buttonNextChooseSoftware_message();

                    tabControlSeparates.SelectedTab = tabPageChooseCriteria;

                    indexSperate = tabControlSeparates.SelectedIndex;

                    progressBar1.Value = 25;
                }

            } catch(Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        // messagem que deve aparecer quando se clica no next e aparece sucesso
        private void buttonNextChooseSoftware_message() {
            string message = "Select Software:\n";

            foreach(Business.Software s in Business.ManagementDataBase.infoSoftware_byID().Values) {
                message += s.Id + "\t" + s.Name + "\n";
            }

            MessageBox.Show(message, "Software", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void buttonNextDefinitonWeigths_Click(object sender, EventArgs e) {
            dataGridViewCaracteristicasPrioridades.DataSource = Business.ManagementDataBase.tableCaracteristicasPrioridades();
            dataGridViewCaracteristicasPrioridades.Columns["ID"].Visible = false;
            dataGridViewCaracteristicasPrioridades.Columns["Name"].HeaderText = "Criterias";

            tabControlSeparates.SelectedTab = tabPageDefinitionPriorities;
            indexSperate = tabControlSeparates.SelectedIndex;
            progressBar1.Value = 75;

            // selecciona a 1º caracteistica da tabela
            selectCharacteristics_row = 0;
            buttonSelectCharacteristicsNext_Click(null, null);
            buttonSelectCaracteristicsNext.Enabled = true;

        }


        #endregion

        #region Button Preivous

        private void buttonPreviousToSoftware_Click(object sender, EventArgs e) {
            tabControlSeparates.SelectedTab = tabPageChooseSoftware;
            indexSperate = tabControlSeparates.SelectedIndex;
            progressBar1.Value = 0;
        }

        private void buttonPreviousDefiniotWeigths_Click(object sender, EventArgs e) {
            tabControlSeparates.SelectedTab = tabPageChooseCriteria;
            indexSperate = tabControlSeparates.SelectedIndex;
            progressBar1.Value = 25;
        }

        private void buttonDefinitionOfPriorities_Click(object sender, EventArgs e) {
            tabControlSeparates.SelectedTab = tabPageClassificaoes;
            indexSperate = tabControlSeparates.SelectedIndex;
            progressBar1.Value = 50;
        }

        #endregion

        #region Button Finish
        private void buttonFinish_Click(object sender, EventArgs e) {
            Business.ManagementDataBase.resultFinal = new Dictionary<int, Dictionary<string, float>>();
            if(Business.ManagementDataBase.metodo_fase_1.Equals("smart")) {
                Business.ManagementDataBase.resultFinal = Business.ManagementDataBase.decision.analiseFinal(Business.ManagementDataBase.tabelaSmartNorm, Business.ManagementDataBase.decision.TableResult);
            }

            if(Business.ManagementDataBase.metodo_fase_1.Equals("ahp")) {
                Business.ManagementDataBase.resultFinal = Business.ManagementDataBase.decision.analiseFinal(Business.ManagementDataBase.pesosFinaisClassAHP, Business.ManagementDataBase.decision.TableResult);
            }


            DataTable final = new DataTable();
            final.Columns.Add("RANK");
            final.Columns.Add("Software ID");
            final.Columns.Add("Software Name");
            final.Columns.Add("Priority");


            foreach(KeyValuePair<int, Dictionary<string, float>> pair in Business.ManagementDataBase.resultFinal) {
                Dictionary<string, float> a;
                Business.ManagementDataBase.resultFinal.TryGetValue(pair.Key, out a);
                foreach(KeyValuePair<string, float> pair2 in a) {
                    int id_Soft = 0;
                    int.TryParse(pair2.Key, out id_Soft);
                    Business.Software s = Business.ManagementDataBase.getSoftware(id_Soft);
                    final.Rows.Add(pair.Key, pair2.Key, s.Name, pair2.Value);
                }
            }

            DataView view = new DataView(final);
            dataGridViewFinal.DataSource = view;

            dataGridViewFinalDetails.DataBindings.Clear();
            dataGridViewFinalDetails.DataSource = Business.ManagementDataBase.tableFinalCompose();
            dataGridViewFinal.Columns["Software ID"].Visible = false;

            tabControlSeparates.SelectedTab = tabPageFinal;
            indexSperate = tabControlSeparates.SelectedIndex;
            progressBar1.Value = 100;

            graph();
            graphDetails();

            labelBestSoftware();

            configTableFinal();
            configTableFinalDetails();

            uscGraphBalance bal = new uscGraphBalance();
            bal.Dock = DockStyle.Fill;
            pnlGrahpBalance.Controls.Clear();
            pnlGrahpBalance.Controls.Add(bal);
        }

        private void labelBestSoftware() {
            foreach(DataGridViewRow line in dataGridViewFinal.Rows) {
                if(line.Cells[0].Value.ToString().Equals("1")) {
                    labelBestSoftwareName.Text = line.Cells[2].Value.ToString();
                    return;
                }
            }
        }

        private void configTableFinal() {
            foreach(DataGridViewRow line in dataGridViewFinal.Rows) {
                if(line.Cells[0].Value.ToString().Equals("1")) {
                    foreach(DataGridViewCell c in line.Cells) {
                        c.Style.BackColor = Color.Yellow;
                    }
                }

                float f1;
                float.TryParse(line.Cells[3].Value.ToString(), out f1);

                f1 = f1 * 100;

                line.Cells[3].Value = f1.ToString("0.00") + " %";
            }

            dataGridViewFinal.ReadOnly = true;
        }

        private void configTableFinalDetails() {
            // cores das caracteristicas
            foreach(DataGridViewRow line in dataGridViewFinalDetails.Rows) {
                if(line.Cells[1].Value.ToString().Equals("")) {
                    foreach(DataGridViewCell c in line.Cells) {
                        c.Style.BackColor = Color.Black;
                        c.Style.ForeColor = System.Drawing.Color.White;
                    }
                    float f3;
                    float.TryParse(line.Cells[2].Value.ToString(), out f3);
                    f3 = f3 * 100;
                    line.Cells[2].Value = f3.ToString("0.00") + " %";
                }
            }

            // nomes dos softwares
            foreach(DataGridViewRow line in dataGridViewFinalDetails.Rows) {
                if(line.Cells[1].Value.ToString().Equals("") == false) {
                    int id;
                    int.TryParse(line.Cells[1].Value.ToString(), out id);
                    Business.Software s = Business.ManagementDataBase.getSoftware(id);
                    dataGridViewFinalDetails.Rows[line.Index].Cells[1].Value = s.Name;

                    float f1, f2;
                    float.TryParse(line.Cells[2].Value.ToString(), out f1);
                    float.TryParse(line.Cells[3].Value.ToString(), out f2);

                    f1 = f1 * 100;
                    f2 = f2 * 100;

                    line.Cells[2].Value = f1.ToString("0.00") + " %";
                    line.Cells[3].Value = f2.ToString("0.00") + " %";
                }

            }

            for(int i = 0 ; i < dataGridViewFinalDetails.ColumnCount ; i++) {
                dataGridViewFinalDetails.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dataGridViewFinalDetails.ReadOnly = true;
        }

        private void dataGridViewFinalDetails_DataError(object sender, DataGridViewDataErrorEventArgs e) {

        }


        #endregion

        #region Graph Final

        private void graph() {
            // limpa o gráfico
            zedGraphControlRankingFinal.GraphPane.CurveList.Clear();

            GraphPane myPane = zedGraphControlRankingFinal.GraphPane;

            // títulos
            myPane.Title.Text = "Final Ranking";
            myPane.XAxis.Title.Text = "Software Name";
            myPane.YAxis.Title.Text = "Priority";

            myPane.Legend.IsVisible = false;

            myPane.XAxis.Type = AxisType.Text;
            string[] labels = graphXX();
            myPane.XAxis.Scale.TextLabels = labels;

            myPane.XAxis.Type = AxisType.Text;

            PointPairList list = graphYY();
            BarItem barra = myPane.AddBar("Priority", list, Color.Blue);
            barra.Bar.Fill = new Fill(Color.Blue);
            barra.Bar.Border.GradientFill.IsScaled = true;

            myPane.Chart.Fill = new Fill(Color.YellowGreen, Color.LightGoldenrodYellow, 45F);
            myPane.Fill = new Fill(Color.White, Color.FromArgb(220, 220, 255), 45F);
            zedGraphControlRankingFinal.AxisChange();
        }

        private string[] graphXX() {
            // coloca o nome dos softwares num array de strings
            ArrayList list = new ArrayList();

            foreach(KeyValuePair<int, Dictionary<string, float>> pair in Business.ManagementDataBase.resultFinal) {
                Dictionary<string, float> a;
                Business.ManagementDataBase.resultFinal.TryGetValue(pair.Key, out a);
                foreach(KeyValuePair<string, float> pair2 in a) {
                    int id_Soft = 0;
                    int.TryParse(pair2.Key, out id_Soft);
                    Business.Software s = Business.ManagementDataBase.getSoftware(id_Soft);
                    list.Add(s.Name);
                }
            }


            string[] x = new string[list.Count];

            int pos = 0;
            foreach(string v in list) {
                x[pos] = v;
                pos++;
            }


            return x;
        }

        private PointPairList graphYY() {
            PointPairList list = new PointPairList();

            foreach(KeyValuePair<int, Dictionary<string, float>> pair in Business.ManagementDataBase.resultFinal) {
                Dictionary<string, float> a;
                Business.ManagementDataBase.resultFinal.TryGetValue(pair.Key, out a);
                foreach(KeyValuePair<string, float> pair2 in a) {
                    list.Add(0, pair2.Value);
                }
            }

            return list;
        }

        #endregion

        #region Graph Final Details

        private void graphDetails() {
            // limpa o gráfico
            zedGraphControlRankingDetails.GraphPane.CurveList.Clear();

            GraphPane myPane2 = zedGraphControlRankingDetails.GraphPane;

            // títulos
            myPane2.Title.Text = "Final Ranking";
            myPane2.XAxis.Title.Text = "Software Name";
            myPane2.YAxis.Title.Text = "Priority";

            myPane2.Legend.Position = LegendPos.BottomCenter;
            myPane2.Legend.FontSpec.Size = 15;

            myPane2.XAxis.Type = AxisType.Text;
            string[] labels = graphXXDetails();
            myPane2.XAxis.Scale.TextLabels = labels;

            myPane2.XAxis.Type = AxisType.Text;

            graphYYDetails(myPane2);

            myPane2.Chart.Fill = new Fill(Color.YellowGreen, Color.LightGoldenrodYellow, 45F);
            myPane2.Fill = new Fill(Color.White, Color.FromArgb(220, 220, 255), 45F);

            //myPane2.AxisChange();

            zedGraphControlRankingDetails.GraphPane.AxisChange();
            //zedGraphControlRankingDetails.Show();
            //zedGraphControlRankingDetails.AxisChange();
            //zedGraphControlRankingDetails.SetScrollRangeFromData();
        }

        private string[] graphXXDetails() {
            // coloca o nome dos softwares num array de strings
            ArrayList list = new ArrayList();

            foreach(KeyValuePair<int, Dictionary<string, float>> pair in Business.ManagementDataBase.resultFinal) {
                Dictionary<string, float> a;
                Business.ManagementDataBase.resultFinal.TryGetValue(pair.Key, out a);
                foreach(KeyValuePair<string, float> pair2 in a) {
                    int id_Soft = 0;
                    int.TryParse(pair2.Key, out id_Soft);
                    Business.Software s = Business.ManagementDataBase.getSoftware(id_Soft);
                    list.Add(s.Name);
                }
            }


            string[] x = new string[list.Count];

            int pos = 0;
            foreach(string v in list) {
                x[pos] = v;
                pos++;
            }


            return x;
        }

        private void graphYYDetails(GraphPane myPane) {
            myPane.BarSettings.Type = BarType.Stack;

            // vai buscar uma cor
            int color = 0;

            foreach(int c in Business.ManagementDataBase.caracteristicas_escolhidas.Keys) {
                PointPairList list = new PointPairList();

                list = graphYYDetailsAux("" + c);

                Business.Characteristic ch = Business.ManagementDataBase.getCharacteristics(c);

                BarItem barra = myPane.AddBar(ch.Name, list, myColor(color));
                barra.Bar.Fill = new Fill(myColor(color));
                color++;
            }

            return;
        }

        private PointPairList graphYYDetailsAux(string id_c) {
            PointPairList list = new PointPairList();

            float resultWeight;

            // vai buscar o peso da caracteristica
            Business.ManagementDataBase.decision.TableResultWeight.TryGetValue(id_c, out resultWeight);

            // vou a todos os softwares (string) -> necessário para o resultado ser ordenado
            foreach(Dictionary<string, float> v1 in Business.ManagementDataBase.resultFinal.Values) {
                // este for é necessário apesar de a tabela final de tanking ter apenas um valor aqui...
                foreach(KeyValuePair<string, float> pair_r in v1) {
                    // depois vou a TableResult à respectiva caracteristica buscar o peso dos softwares
                    Dictionary<string, float> a;
                    Business.ManagementDataBase.decision.TableResult.TryGetValue(id_c, out a);

                    foreach(KeyValuePair<string, float> pair in a) {
                        if(pair.Key.Equals(pair_r.Key)) {
                            // se o ID for igual ao que id do software que procuramos vai adicionar
                            // multiplica pelo peso que essa caracteristica tem
                            float p = pair.Value * resultWeight;
                            list.Add(0, p);
                        }
                    }
                }
            }


            return list;
        }

        #endregion

        #region Button Help
        private void aHPTutorialToolStripMenuItem_Click(object sender, EventArgs e) {
            string url = Path.GetFullPath("Files\\Tutorials_html\\AHPtutorial.htm");
            View_HTML t = new View_HTML(url);
            t.Show();
        }

        private void sMARTTutorialToolStripMenuItem_Click(object sender, EventArgs e) {
            string url = Path.GetFullPath("Files\\Tutorials_html\\SMARTtutorial.htm");
            View_HTML t = new View_HTML(url);
            t.Show();
        }

        private void valueFNTutorialToolStripMenuItem_Click(object sender, EventArgs e) {
            string url = Path.GetFullPath("Files\\Tutorials_html\\ValueFntutorial.htm");
            View_HTML t = new View_HTML(url);
            t.Show();
        }

        private void webPageToolStripMenuItem_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("http://code.google.com/p/besmart/");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            AboutBox a = new AboutBox();
            a.Show();
        }
        #endregion

        #region Start New Comparation
        private void startANewComparationToolStripMenuItem_Click(object sender, EventArgs e) {
            Business.ManagementDataBase.ids_dos_SoftwareSeleccionados = new List<int>();
            Business.ManagementDataBase.caracteristicas_escolhidas = new Dictionary<int, string>();
            Business.ManagementDataBase.tabelaSmartNorm = new Dictionary<string, float>();
            Business.ManagementDataBase.pesosFinaisClassAHP = new Dictionary<string, float>();
            refreshTableSoftware();
            refreshTableCaracteristics();
            Business.ManagementDataBase.ids_dos_SoftwareSeleccionados.Clear();
            Business.ManagementDataBase.caracteristicas_escolhidas.Clear();
            Business.ManagementDataBase.decision.TableCH.Clear();
            Business.ManagementDataBase.decision.TableAHP.Clear();
            Business.ManagementDataBase.decision.TableResult.Clear();
            Business.ManagementDataBase.tabelaSmartNorm.Clear();
            Business.ManagementDataBase.pesosFinaisClassAHP.Clear();

            buttonNextChooseSoftware.Enabled = true;


            dataGridViewPesosAHP.DataSource = null;
            dataGridViewPesosFinaisSmart.DataSource = null;
            dataGridViewPesosAHPFinais.DataSource = null;
            dataGridViewValueFn.DataSource = null;
            dataGridViewFinal.DataSource = null;

            progressBar1.Value = 0;

            labelAHPPrioCons.Text = "consitencia";
            labelConsistencyRate.Text = "";
            tabControlSeparates.SelectedTab = tabPageChooseSoftware;
            dataGridViewTabelaSoftware.Columns[0].Visible = true;

            indexSperate = tabControlSeparates.SelectedIndex;

        }

        private void buttonStartNewComparation_Click(object sender, EventArgs e) {
            startANewComparationToolStripMenuItem_Click(null, null);
        }

        #endregion

        #region ViewWebPage

        private void buttonViewWebPage_Click(object sender, EventArgs e) {
            ConsultWebpage cwp = new ConsultWebpage();
            cwp.Show();
        }

        private void viewSoftwareWebpageToolStripMenuItem_Click(object sender, EventArgs e) {
            ConsultWebpage cwp = new ConsultWebpage();
            cwp.Show();

        }
        #endregion

        #region exit
        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            DialogResult r = MessageBox.Show("Want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(r == DialogResult.Yes) {
                this.Dispose();
            }
        }


        private void chooseProcess_FormClosing(object sender, FormClosingEventArgs e) {
            DialogResult r = MessageBox.Show("Want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(r == DialogResult.Yes) {
                this.Dispose();
            } else {   // cancel dispose
                e.Cancel = true;
            }
        }
        #endregion

        #region TabSeparates
        private void tabControlSeparates_Click(object sender, EventArgs e) {
            tabControlSeparates.SelectedIndex = indexSperate;
            MessageBox.Show("Use the buttons Next and Previous for navigate in process.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        #region Choose Software

        private void dataGridViewTabelaSoftware_CellClick(object sender, DataGridViewCellEventArgs e) {
            try {

                foreach(DataGridViewRow line in dataGridViewTabelaSoftware.Rows) {
                    if(line.Cells[0].Value != null && line.Cells[0].Value.ToString().Equals("True")) {
                        line.Selected = true;
                    }
                    if(line.Cells[0].Value != null && line.Cells[0].Value.ToString().Equals("False")) {
                        line.Selected = false;
                    }
                }

                // Para quando se clica na coluna do Link
                if(e.ColumnIndex == dataGridViewTabelaSoftware.Columns["LinkClick"].Index) {
                    View_HTML v = new View_HTML(dataGridViewTabelaSoftware.Rows[e.RowIndex].Cells["Link"].Value.ToString());
                    v.Show();
                }

            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridViewTabelaSoftware_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            foreach(DataGridViewRow line in dataGridViewTabelaSoftware.Rows) {
                if(line.Cells[0].Value != null && line.Cells[0].Value.ToString().Equals("True")) {
                    line.Selected = true;
                }
                if(line.Cells[0].Value != null && line.Cells[0].Value.ToString().Equals("False")) {
                    line.Selected = false;
                }
            }
        }

        /// <summary>
        /// Evento para formatar as celulas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewTabelaSoftware_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {
            try {

                // Formata a coluna do link
                if(dataGridViewTabelaSoftware.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewLinkCell) {
                    e.Value = dataGridViewTabelaSoftware.Rows[e.RowIndex].Cells["Link"].Value;
                }

            } catch(Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Choose Characteristics

        private void dataGridViewCharacteristics_CellClick(object sender, DataGridViewCellEventArgs e) {
            foreach(DataGridViewRow line in dataGridViewCharacteristics.Rows) {
                if(line.Cells[0].Value != null && line.Cells[0].Value.ToString().Equals("True")) {
                    line.Selected = true;
                }
                if(line.Cells[0].Value != null && line.Cells[0].Value.ToString().Equals("False")) {
                    line.Selected = false;
                }
            }

        }

        private void dataGridViewCharacteristics_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            foreach(DataGridViewRow line in dataGridViewCharacteristics.Rows) {
                if(line.Cells[0].Value != null && line.Cells[0].Value.ToString().Equals("True")) {
                    line.Selected = true;
                }
                if(line.Cells[0].Value != null && line.Cells[0].Value.ToString().Equals("False")) {
                    line.Selected = false;
                }
            }
        }

        #endregion

        #region Definition of Weights Smart

        private void refreshTableSmart() {
            dataGridViewPesosFinaisSmart.DataBindings.Clear();
            dataGridViewSmart.DataSource = Business.ManagementDataBase.tableSmart();
            dataGridViewSmart.Columns["ID"].Visible = false;
            foreach(DataGridViewRow line in dataGridViewSmart.Rows) {
                line.ErrorText = "Please insert a value.";
            }

        }

        private void dataGridViewSmart_CellValidating(object sender, DataGridViewCellValidatingEventArgs e) {
            int c = e.ColumnIndex;
            int l = e.RowIndex;

            int newNumber = 0;

            if(dataGridViewSmart.Rows[l].Cells[0].Value == null /*|| dataGridViewSmart.CurrentCell.Value.ToString().Equals("") == true*/) return;

            if(c == 0) {
                if(!int.TryParse(e.FormattedValue.ToString(), out newNumber)) {
                    dataGridViewSmart.Rows[l].ErrorText = "The Value is not a number!";
                    MessageBox.Show("The Value is not a number!");
                    e.Cancel = true;
                    return;
                } else {
                    if(newNumber < 10) {
                        dataGridViewSmart.Rows[l].ErrorText = "The value can not be less than 10.";
                        MessageBox.Show("The value can not be less than 10.");
                        e.Cancel = true;
                    } else {
                        dataGridViewSmart.Rows[l].ErrorText = null;
                    }
                }
            }

            verifyTableSmart();

        }


        private void dataGridViewSmart_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            verifyTableSmart();
        }


        private void dataGridViewPesosFinaisSmart_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
        }

        // verifica se está tudo preenchido e calcula os pesos
        private void verifyTableSmart() {
            // se estiver alguma coisa vazia não faz mais nada
            foreach(DataGridViewRow line in dataGridViewSmart.Rows) {
                int n = 0;
                if(line.Cells[0].Value == null) {
                    line.ErrorText = "Please insert a value.";
                    return;
                } else { line.ErrorText = null; }

                if(!int.TryParse(line.Cells[0].Value.ToString(), out n)) {
                    line.ErrorText = "The Value is not a number!";
                    return;
                } else { line.ErrorText = null; }

                if(n < 10) {
                    line.ErrorText = "The value can not be less than 10.";
                    return;
                }
            }

            // para remover possiveis erros que ainda existam
            foreach(DataGridViewRow line in dataGridViewSmart.Rows) {
                line.ErrorText = null;
            }

            // para verificar se existe um 10
            int num_10 = 0;
            foreach(DataGridViewRow line in dataGridViewSmart.Rows) {
                try {
                    int v = -1;
                    int.TryParse(line.Cells[0].Value.ToString(), out v);
                    if(v == 10) num_10++;
                } catch(Exception) {
                    return;
                }
            }

            // se não existir altera a msg
            if(num_10 < 1) {
                label_DefinitionOfWeigths.Text = "Assign 10 to a characteristics.";
                return;
            }


            // para fazer os calulos
            Business.ManagementDataBase.decision.TableCH.Clear();
            foreach(DataGridViewRow linha in dataGridViewSmart.Rows) {
                string idChar = linha.Cells[1].Value.ToString();
                int points = System.Convert.ToInt32(linha.Cells[0].Value.ToString());
                Business.ManagementDataBase.decision.registerClass(idChar, points);
            }

            Business.ManagementDataBase.tabelaSmartNorm.Clear();
            Business.ManagementDataBase.tabelaSmartNorm = Business.ManagementDataBase.decision.normalizeSMART(Business.ManagementDataBase.decision.TableCH);

            dataGridViewPesosFinaisSmart.DataSource = Business.ManagementDataBase.tableFinalWeightSmart();
            dataGridViewPesosFinaisSmart.Columns["ID"].Visible = false;

            buttonNextDefinitonWeigths.Enabled = true;

            if(tabControlSmartAHP.SelectedIndex == 0) {
                Business.ManagementDataBase.metodo_fase_1 = "smart";

                string inf = "Currently smart method chosen.";
                label_DefinitionOfWeigths.Text = inf;

            }

        }


        #endregion

        #region Definition of Weights AHP
        private void refreshTableAHP() {
            dataGridViewPesosAHP.DataBindings.Clear();
            dataGridViewAHP.DataBindings.Clear();
            dataGridViewAHP.DataSource = Business.ManagementDataBase.tableAHP();

            int i = 0;
            int num_ca = Business.ManagementDataBase.totalCharacteristcSelect();

            // 
            while(i < num_ca) {
                dataGridViewAHP[i + 1, i].Value = "1";
                dataGridViewAHP[i + 1, i].ReadOnly = true;
                i++;
            }

            for(i = 0 ; i < dataGridViewAHP.ColumnCount ; i++) {
                dataGridViewAHP.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            colorTableAHP();
        }

        private void dataGridViewAHP_CellValidating(object sender, DataGridViewCellValidatingEventArgs e) {
            verifyTableAHP();
        }

        private void dataGridViewAHP_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            verifyTableAHP();
        }

        private void verifyTableAHP() {
            // vai a todas as linhas
            foreach(DataGridViewRow line in dataGridViewAHP.Rows) {
                // vai a cada célula da linha
                foreach(DataGridViewCell c in line.Cells) {
                    int column = c.ColumnIndex;
                    int row = c.RowIndex;

                    // se dif = 1 é diagonal, se >1 é acima da diagonal, se < 1 abaixo da diagonal
                    int dif = column - row;

                    // se for diagonal fica a 1
                    if(dif == 1) {
                        dataGridViewAHP.Rows[row].Cells[column].Value = "1";
                        dataGridViewAHP.Rows[row].Cells[column].ReadOnly = true;
                    }

                    // para a digonal superior
                    if(column >= 1 && dif > 1) {
                        // verifica se está preenchida
                        if(c.Value != null && c.Value.ToString().Equals("") == false) {
                            // pega no valor da célula e cria o novo
                            string v = c.Value.ToString();
                            string v1 = "1/" + v;

                            float v_float = stringToFloat(v);
                            float v1_float = 1 / v_float;
                            // apaga os valores
                            dataGridViewAHP.Rows[row].Cells[column].Value = null;
                            dataGridViewAHP.Rows[column - 1].Cells[row + 1].Value = null;
                            // coloca o valor na célula correspondente
                            dataGridViewAHP.Rows[row].Cells[column].Value = v_float;
                            dataGridViewAHP.Rows[column - 1].Cells[row + 1].Value = v1_float;
                        }

                    }

                    // para a digonal inferior não permite alterar
                    if(dif < 1) {
                        dataGridViewAHP.Rows[row].Cells[column].ReadOnly = true;
                    }

                }

            }

            // altera as cores da tabela
            colorTableAHP();
            caulatePesosAHP();

        }

        private void colorTableAHP() {
            // vai a todas as linhas
            foreach(DataGridViewRow line in dataGridViewAHP.Rows) {
                // vai a cada célula da linha
                foreach(DataGridViewCell c in line.Cells) {
                    int column = c.ColumnIndex;
                    int row = c.RowIndex;
                    // se dif = 1 é diagonal, se >1 é acima da diagonal, se < 1 abaixo da diagonal
                    int dif = column - row;

                    // para a digonal superior
                    if(column >= 1 && dif > 1) {
                        dataGridViewAHP.Rows[row].Cells[column].Style.BackColor = Color.Gold;
                        // verifica se está preenchida
                        if(c.Value != null && c.Value.ToString().Equals("") == false) {
                            int r = 0;
                            int g = 255;
                            int b = 18;
                            dataGridViewAHP.Rows[row].Cells[column].Style.BackColor = System.Drawing.Color.FromArgb(r, g, b);
                        }
                    }

                    // para a digonal inferior e diagonal
                    if(column >= 1 && dif <= 1) {
                        int r = 0;
                        int g = 170;
                        int b = 255;
                        dataGridViewAHP.Rows[row].Cells[column].Style.BackColor = System.Drawing.Color.FromArgb(r, g, b);
                    }

                    // para a 1ª coluna
                    if(column == 0) {
                        int r = 222;
                        int g = 222;
                        int b = 222;
                        dataGridViewAHP.Rows[row].Cells[column].Style.BackColor = System.Drawing.Color.FromArgb(r, g, b);
                    }

                }

            }

        }

        private void caulatePesosAHP() {
            // verifica se pode calcular
            foreach(DataGridViewRow line in dataGridViewAHP.Rows) {
                foreach(DataGridViewCell cell in line.Cells) {
                    // para não verificar a primeira coluna
                    if(cell.ColumnIndex > 0) {
                        float v = stringToFloat(cell.Value.ToString());
                        // se não for float ou estiver a 0 faz return
                        if(v == -1 || v == 0) return;
                    }

                }

            }

            // vai calcular
            int flag = 0;   // para a preira coluna
            foreach(DataGridViewColumn coluna in dataGridViewAHP.Columns) {
                if(flag == 0) {
                    flag = 1;
                } else {
                    string name = coluna.Name.ToString();
                    string idA = Business.ManagementDataBase.procuraIdCha(name);
                    foreach(DataGridViewRow linha in dataGridViewAHP.Rows) {
                        string nameB = linha.Cells[0].Value.ToString();
                        string idB = Business.ManagementDataBase.procuraIdCha(nameB);
                        string pointsStr = linha.Cells[name].Value.ToString();
                        float pointf = stringToFloat(pointsStr);
                        Business.ManagementDataBase.decision.registerClassAHP(idA, idB, pointf);
                    }
                }
            }

            Dictionary<string, Dictionary<string, float>> tabelaNormAHP = new Dictionary<string, Dictionary<string, float>>();
            tabelaNormAHP = Business.ManagementDataBase.decision.normalizeAHP(Business.ManagementDataBase.decision.TableAHP);
            Business.ManagementDataBase.pesosFinaisClassAHP = new Dictionary<string, float>();

            // alterar para a managmente
            Business.ManagementDataBase.pesosFinaisClassAHP = Business.ManagementDataBase.decision.pesosFinais(tabelaNormAHP);


            dataGridViewPesosAHP.DataSource = Business.ManagementDataBase.tableFinalWeightAHP();
            dataGridViewPesosAHP.Columns["ID"].Visible = false;

            Business.ManagementDataBase.metodo_fase_1 = "ahp";
            label_DefinitionOfWeigths.Text = "Currently AHP method chosen.";
            testConsistency();

        }

        private void testConsistency() {
            try {
                Dictionary<int, double> matrixC = new Dictionary<int, double>();
                Dictionary<int, double> matrixD = new Dictionary<int, double>();

                matrixC = Business.ManagementDataBase.decision.calculaMatrizC(Business.ManagementDataBase.decision.TableAHP, Business.ManagementDataBase.pesosFinaisClassAHP);
                matrixD = Business.ManagementDataBase.decision.calculaMatrizD(matrixC, Business.ManagementDataBase.pesosFinaisClassAHP);
                double taxa = Business.ManagementDataBase.decision.taxaConsitencia(matrixD);

                if(taxa <= 0.10) {
                    int r = 0;
                    int g = 255;
                    int b = 78;
                    labelConsistencyRate.ForeColor = System.Drawing.Color.FromArgb(r, g, b);
                } else {
                    int r = 255;
                    int g = 27;
                    int b = 0;
                    labelConsistencyRate.ForeColor = System.Drawing.Color.FromArgb(r, g, b);
                }

                // actualiza a label com a taxa
                labelConsistencyRate.Text = "" + taxa.ToString("f3");
                // activa o botão next
                buttonNextDefinitonWeigths.Enabled = true;
            } catch(Exception) { }

        }
        #endregion

        #region Definiton Priorities ValueFN

        private void buttonSelectCharacteristicsNext_Click(object sender, EventArgs e) {
            calculataDefinitionOfPriorities_ButtonNext();
        }

        private void buttonSelectCharacteristicsPrevious_Click(object sender, EventArgs e) {
            calculataDefinitionOfPriorities_ButtonPrevious();
        }

        private void calculataDefinitionOfPriorities_ButtonNext() {
            // para não incrementar sempre a caracteristica seleccionada
            if(selectCharacteristics_row < dataGridViewCaracteristicasPrioridades.RowCount) {
                // vai seleccionando a linha que deve
                dataGridViewCaracteristicasPrioridades.Rows[selectCharacteristics_row].Selected = true;

                selectCharacteristics_id = dataGridViewCaracteristicasPrioridades["ID", selectCharacteristics_row].Value.ToString();
                string name = dataGridViewCaracteristicasPrioridades["Name", selectCharacteristics_row].Value.ToString();

                // label com a caracteristica que está seleccionada
                label_Definition_of_Priorities_CharacteristicsSelect.Text = selectCharacteristics_id + " - " + name;

                // active o botão Previous
                buttonSelectCharacteristicsPrevious.Enabled = true;

                // seleccionada a caracteristica minimize
                radioButtonMinimize.Select();

                refreshTableAHPPriority(name);

                dataGridViewValueFn.DataSource = null;
                dataGridViewPesosAHPFinais.DataSource = null;

                // tab do ValueFn
                tabControlPrioridadesFinais.SelectedIndex = 0;
                calculateValueFn();

                // para a seguir ir seleccionar outra linha
                selectCharacteristics_row++;
            }

            // se estiver igual ou maior é porque não tem mais linhas
            if(selectCharacteristics_row >= dataGridViewCaracteristicasPrioridades.RowCount) {
                buttonSelectCaracteristicsNext.Enabled = false;
                buttonFinish.Enabled = true;
            }

        }

        private void calculataDefinitionOfPriorities_ButtonPrevious() {
            // para não decrementar sempre a caracteristica seleccionada
            if(selectCharacteristics_row > 0) {
                // para seleccionar a linha anterior
                selectCharacteristics_row--;

                // vai seleccionando a linha que deve
                dataGridViewCaracteristicasPrioridades.Rows[selectCharacteristics_row].Selected = true;

                // active o botão Next
                buttonSelectCaracteristicsNext.Enabled = true;

                selectCharacteristics_id = dataGridViewCaracteristicasPrioridades["ID", selectCharacteristics_row].Value.ToString();
                string name = dataGridViewCaracteristicasPrioridades["Name", selectCharacteristics_row].Value.ToString();

                // label com a caracteristica que está seleccionada
                label_Definition_of_Priorities_CharacteristicsSelect.Text = selectCharacteristics_id + " - " + name;

                // seleccionada a caracteristica minimize
                radioButtonMinimize.Select();

                refreshTableAHPPriority(name);

                dataGridViewValueFn.DataSource = null;
                dataGridViewPesosAHPFinais.DataSource = null;

                // tab do ValueFn
                tabControlPrioridadesFinais.SelectedIndex = 0;
                calculateValueFn();

            }

            // se estiver igual ou maior é porque não tem mais linhas
            if(selectCharacteristics_row <= 0) {
                buttonSelectCharacteristicsPrevious.Enabled = false;
                buttonFinish.Enabled = true;
            }

        }

        private void calculateValueFn() {
            Business.ManagementDataBase.decision.TableSW = Business.ManagementDataBase.database.SoftwareWithCaracteristics(Business.ManagementDataBase.ids_dos_SoftwareSeleccionados);

            Dictionary<string, Dictionary<string, int>> tableFilter = new Dictionary<string, Dictionary<string, int>>();

            //string id_carac = labelCaracteristicaValueFnID.Text;
            tableFilter = Business.ManagementDataBase.decision.filter(selectCharacteristics_id);

            int min = Business.ManagementDataBase.decision.calMin(selectCharacteristics_id, tableFilter);
            int max = Business.ManagementDataBase.decision.calMax(selectCharacteristics_id, tableFilter);

            Dictionary<string, float> aux = new Dictionary<string, float>();
            // maximizar
            if(radioButtonMaximize.Checked) {
                aux = Business.ManagementDataBase.decision.calValueMax(min, max, tableFilter);

            }

            // maximizar
            if(radioButtonMinimize.Checked) {
                aux = Business.ManagementDataBase.decision.calValueMin(min, max, tableFilter);
            }

            Business.ManagementDataBase.decision.registerPriority(selectCharacteristics_id, aux);

            DataTable prioridades = new DataTable();
            prioridades.Columns.Add("ID");
            prioridades.Columns.Add("Name");
            prioridades.Columns.Add("Priority");

            Dictionary<string, float> a;

            Business.ManagementDataBase.decision.TableResult.TryGetValue(selectCharacteristics_id, out a);
            String nameTmp = "";
            foreach(KeyValuePair<string, float> pair2 in a) {
                nameTmp = Business.ManagementDataBase.getSoftware(int.Parse(pair2.Key)).Name;
                prioridades.Rows.Add(pair2.Key, nameTmp, pair2.Value);
            }

            DataView view = new DataView(prioridades);
            dataGridViewValueFn.DataSource = view;
            dataGridViewValueFn.Columns["ID"].Visible = false;

        }

        private void radioButtonMinimize_CheckedChanged(object sender, EventArgs e) {
            calculateValueFn();
        }

        private void radioButtonMaximize_CheckedChanged(object sender, EventArgs e) {
            calculateValueFn();
        }

        #endregion

        #region Definiton Priorities AHP
        private void dataGridViewAHPPriority_CellValidating(object sender, DataGridViewCellValidatingEventArgs e) {
            verifyTableAHPPriorities();
        }

        private void dataGridViewAHPPriority_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            verifyTableAHPPriorities();
        }

        private void refreshTableAHPPriority(string nameC) {
            dataGridViewAHPPriority.DataBindings.Clear();
            dataGridViewAHPPriority.DataSource = Business.ManagementDataBase.refreshTableAHPPriority(nameC);

            int i = 0;
            int num_ca = Business.ManagementDataBase.ids_dos_SoftwareSeleccionados.Count;

            dataGridViewAHPPriority.AllowUserToOrderColumns = false;

            while(i < num_ca) {
                dataGridViewAHPPriority[i + 1, i].Value = "1";
                dataGridViewAHPPriority[i + 1, i].ReadOnly = true;
                i++;
            }

            for(i = 0 ; i < dataGridViewAHPPriority.ColumnCount ; i++) {
                dataGridViewAHPPriority.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            colorTableAHPPriorities();
        }

        private void verifyTableAHPPriorities() {
            // vai a todas as linhas
            foreach(DataGridViewRow line in dataGridViewAHPPriority.Rows) {
                // vai a cada célula da linha
                foreach(DataGridViewCell c in line.Cells) {
                    int column = c.ColumnIndex;
                    int row = c.RowIndex;

                    // se dif = 1 é diagonal, se >1 é acima da diagonal, se < 1 abaixo da diagonal
                    int dif = column - row;

                    // se for diagonal fica a 1
                    if(dif == 1) {
                        dataGridViewAHPPriority.Rows[row].Cells[column].Value = "1";
                        dataGridViewAHPPriority.Rows[row].Cells[column].ReadOnly = true;
                    }

                    // para a digonal superior
                    if(column >= 1 && dif > 1) {
                        // verifica se está preenchida
                        if(c.Value != null && c.Value.ToString().Equals("") == false) {
                            // pega no valor da célula e cria o novo
                            string v = c.Value.ToString();
                            string v1 = "1/" + v;

                            float v_float = stringToFloat(v);
                            float v1_float = 1 / v_float;
                            // apaga os valores
                            dataGridViewAHPPriority.Rows[row].Cells[column].Value = null;
                            dataGridViewAHPPriority.Rows[column - 1].Cells[row + 1].Value = null;
                            // coloca o valor na célula correspondente
                            dataGridViewAHPPriority.Rows[row].Cells[column].Value = v_float;
                            dataGridViewAHPPriority.Rows[column - 1].Cells[row + 1].Value = v1_float;
                        }

                    }

                    // para a digonal inferior não permite alterar
                    if(dif < 1) {
                        dataGridViewAHPPriority.Rows[row].Cells[column].ReadOnly = true;
                    }

                }

            }

            // altera as cores da tabela
            colorTableAHPPriorities();
            calculateAHPPriorities(); ;

        }

        private void calculateAHPPriorities() {
            // verifica se pode calcular
            foreach(DataGridViewRow line in dataGridViewAHPPriority.Rows) {
                foreach(DataGridViewCell cell in line.Cells) {
                    // para não verificar a primeira coluna
                    if(cell.ColumnIndex > 0) {
                        float v = stringToFloat(cell.Value.ToString());
                        // se não for float ou estiver a 0 faz return
                        if(v == -1 || v == 0) return;
                    }

                }

            }


            // para não ler a primeira coluna
            int flag = 0;
            foreach(DataGridViewColumn coluna in dataGridViewAHPPriority.Columns) {
                if(flag == 0) {
                    flag = 1;
                } else {
                    string idSofA = coluna.Name.ToString();
                    foreach(DataGridViewRow linha in dataGridViewAHPPriority.Rows) {
                        string idSofB = linha.Cells[0].Value.ToString();
                        //MessageBox.Show(idSofB);
                        string pointsStr = linha.Cells[idSofA].Value.ToString();
                        float pointf = (float)System.Convert.ToDouble(pointsStr);
                        //MessageBox.Show("idA: " + idA + "\tName: " + name + "\nIDB: " + idB + "\tNameB: " + nameB + "\nPoints: " + pointf);
                        Business.ManagementDataBase.decision.registerPriorAHP(selectCharacteristics_id, idSofA, idSofB, pointf);

                    }
                }
            }

            Dictionary<string, Dictionary<string, Dictionary<string, float>>> tabelaNormAHP = new Dictionary<string, Dictionary<string, Dictionary<string, float>>>();
            tabelaNormAHP = Business.ManagementDataBase.decision.normalizePriorityAHP(Business.ManagementDataBase.decision.TablePriorAHP);
            Business.ManagementDataBase.decision.pesosPriorFinais(tabelaNormAHP);

            DataTable prioridades = new DataTable();
            prioridades.Columns.Add("ID");
            prioridades.Columns.Add("Name");
            prioridades.Columns.Add("Priority");

            Dictionary<string, float> a;

            Business.ManagementDataBase.decision.TableResult.TryGetValue(selectCharacteristics_id, out a);
            String nameTmp = "";
            foreach(KeyValuePair<string, float> pair2 in a) {
                nameTmp = Business.ManagementDataBase.getSoftware(int.Parse(pair2.Key)).Name;
                prioridades.Rows.Add(pair2.Key, nameTmp, pair2.Value);
            }

            DataView view = new DataView(prioridades);
            dataGridViewPesosAHPFinais.DataSource = view;
            dataGridViewPesosAHPFinais.Columns["ID"].Visible = false;

            calculateTestConcistencyAHPPriorities();

        }

        private void calculateTestConcistencyAHPPriorities() {
            // é chamada na calculateAHPPriorities

            Dictionary<int, double> matrixC = new Dictionary<int, double>();
            Dictionary<int, double> matrixD = new Dictionary<int, double>();
            Dictionary<string, Dictionary<string, float>> aux;
            Dictionary<string, float> aux1;
            Business.ManagementDataBase.decision.TablePriorAHP.TryGetValue(selectCharacteristics_id, out aux);
            Business.ManagementDataBase.decision.TableResult.TryGetValue(selectCharacteristics_id, out aux1);
            matrixC = Business.ManagementDataBase.decision.calculaMatrizC(aux, aux1);
            matrixD = Business.ManagementDataBase.decision.calculaMatrizD(matrixC, aux1);
            double taxa = Business.ManagementDataBase.decision.taxaConsitencia(matrixD);

            if(taxa <= 0.10) {
                //MessageBox.Show("The consistency Rate is good: " + taxa);
                labelAHPPrioCons.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(81)))), ((int)(((byte)(19)))));
            } else {
                //MessageBox.Show("The consistency Rate is bad: " + taxa);
                labelAHPPrioCons.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            }

            // actualiza a label com a taxa
            labelAHPPrioCons.Text = "" + taxa.ToString("f3");
        }

        private void colorTableAHPPriorities() {
            // vai a todas as linhas
            foreach(DataGridViewRow line in dataGridViewAHPPriority.Rows) {
                // vai a cada célula da linha
                foreach(DataGridViewCell c in line.Cells) {
                    int column = c.ColumnIndex;
                    int row = c.RowIndex;
                    // se dif = 1 é diagonal, se >1 é acima da diagonal, se < 1 abaixo da diagonal
                    int dif = column - row;

                    // para a digonal superior
                    if(column >= 1 && dif > 1) {
                        dataGridViewAHPPriority.Rows[row].Cells[column].Style.BackColor = Color.Gold;
                        // verifica se está preenchida
                        if(c.Value != null && c.Value.ToString().Equals("") == false) {
                            int r = 0;
                            int g = 255;
                            int b = 18;
                            dataGridViewAHPPriority.Rows[row].Cells[column].Style.BackColor = System.Drawing.Color.FromArgb(r, g, b);
                        }
                    }

                    // para a digonal inferior e diagonal
                    if(column >= 1 && dif <= 1) {
                        int r = 0;
                        int g = 170;
                        int b = 255;
                        dataGridViewAHPPriority.Rows[row].Cells[column].Style.BackColor = System.Drawing.Color.FromArgb(r, g, b);
                    }

                    // para a 1ª coluna
                    if(column == 0) {
                        int r = 222;
                        int g = 222;
                        int b = 222;
                        dataGridViewAHPPriority.Rows[row].Cells[column].Style.BackColor = System.Drawing.Color.FromArgb(r, g, b);
                    }

                }

            }

        }

        #endregion

        #region Funções úteis

        private float stringToFloat(string s) {
            float r = -1;
            float.TryParse(s, out r);
            return r;
        }


        private Color CreateRandomColor() {
            Random randonGen = new Random();

            int r = randonGen.Next(0, 255);
            int g = randonGen.Next(0, 255);
            int b = randonGen.Next(0, 255);

            Color randomColor = Color.FromArgb(r, g, b);

            return randomColor;
        }

        private Color myColor(int i) {
            int num = 21;
            Color[] colors = {   Color.Blue, 
                                 Color.Red, 
                                 Color.Green, 
                                 Color.Orange, 
                                 Color.Cyan, 
                                 Color.Gold, 
                                 Color.LightPink,
                                 Color.Orange,
                                 Color.White,
                                 Color.Violet,
                                 Color.Yellow,
                                 Color.Navy,
                                 Color.MistyRose,
                                 Color.Salmon,
                                 Color.Sienna,
                                 Color.MediumSeaGreen,
                                 Color.DarkSeaGreen,
                                 Color.DarkRed,
                                 Color.Gainsboro,
                                 Color.LimeGreen,
                                 Color.Maroon
                             };

            // se não tiver o indice no array de cores
            if(i >= num) return CreateRandomColor();

            return colors[i];
        }


        #endregion

        #region Examples
        private void insertSoftwareToolStripMenuItem_Click(object sender, EventArgs e) {
            View_HTML v = new View_HTML("http://www.youtube.com/watch_popup?v=sw-z_DPLmtU&hd=1");
            v.Show();
        }

        private void editSoftwareToolStripMenuItem_Click(object sender, EventArgs e) {
            View_HTML v = new View_HTML("http://www.youtube.com/watch_popup?v=UxkwyXp0nJ8&hd=1");
            v.Show();
        }

        private void deleteSoftwareToolStripMenuItem_Click(object sender, EventArgs e) {
            View_HTML v = new View_HTML("http://www.youtube.com/watch_popup?v=_6QIwx7LCQY&hd=1");
            v.Show();
        }

        private void addCharacteristicsToolStripMenuItem_Click(object sender, EventArgs e) {
            View_HTML v = new View_HTML("http://www.youtube.com/watch_popup?v=N4PNLXoIkYY&hd=1");
            v.Show();
        }


        private void editCharacteristicsToolStripMenuItem_Click(object sender, EventArgs e) {
            View_HTML v = new View_HTML("http://www.youtube.com/watch_popup?v=WwABhgbziuY&hd=1");
            v.Show();
        }

        private void deleteCharacteristicsToolStripMenuItem_Click(object sender, EventArgs e) {
            View_HTML v = new View_HTML("http://www.youtube.com/watch_popup?v=RNpqN7e2jIc&hd=1");
            v.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e) {
            View_HTML v = new View_HTML("http://www.youtube.com/watch_popup?v=PRp_X6ipmvI&hd=1");
            v.Show();
        }

        private void smartToolStripMenuItem_Click(object sender, EventArgs e) {
            View_HTML v = new View_HTML("http://www.youtube.com/watch_popup?v=1Kgjc6NWbKk&hd=1");
            v.Show();
        }

        private void aHPDefinitionOfWeightsToolStripMenuItem_Click(object sender, EventArgs e) {
            View_HTML v = new View_HTML("http://www.youtube.com/watch_popup?v=9R4fZMcOwCw&hd=1");
            v.Show();
        }

        private void vlaueFNDefinitionOfPrioritiesToolStripMenuItem_Click(object sender, EventArgs e) {
            View_HTML v = new View_HTML("http://www.youtube.com/watch_popup?v=Z9qJZoVOI4U&hd=1");
            v.Show();
        }

        private void aHPDefinitionOfPrioritiesToolStripMenuItem_Click(object sender, EventArgs e) {
            View_HTML v = new View_HTML("http://www.youtube.com/watch_popup?v=jBMBio4vaJI&hd=1");
            v.Show();
        }

        private void finalResultsToolStripMenuItem_Click(object sender, EventArgs e) {
            View_HTML v = new View_HTML("http://www.youtube.com/watch_popup?v=BQbI6LMaYRY&hd=1");
            v.Show();
        }

        #endregion

        private void dataGridViewAHPPriority_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {
            try {
                String nameTmp = "";
                switch(e.ColumnIndex) {
                    case 0:
                        e.Value = Business.ManagementDataBase.getSoftware(int.Parse(e.Value.ToString())).Name;
                        break;

                    default:
                        nameTmp = Business.ManagementDataBase.getSoftware(int.Parse(dataGridViewAHPPriority.Columns[e.ColumnIndex].Name)).Name;
                        dataGridViewAHPPriority.Columns[e.ColumnIndex].HeaderText = nameTmp;
                        break;
                }


            } catch(Exception ex) {
            }
        }

        private void dataGridViewFinalDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {
            try {

                String nameTemp = "";

                switch(e.ColumnIndex) {
                    case 0:
                        dataGridViewFinalDetails.Columns[0].Visible = false;
                        dataGridViewFinalDetails.Columns[1].HeaderText = "";
                        break;
                    case 1:
                        if(e.Value.Equals("") && dataGridViewFinalDetails.Rows[e.RowIndex].Cells[1].Style.BackColor.Equals(Color.Black)) {
                            nameTemp = Business.ManagementDataBase.getCharacteristics(int.Parse(dataGridViewFinalDetails.Rows[e.RowIndex].Cells[0].Value.ToString())).Name;
                            e.Value = nameTemp;
                            dataGridViewFinalDetails.Rows[e.RowIndex].Cells[1].Value = nameTemp;
                        }
                        break;

                    default:
                        break;
                }

            } catch(Exception ex) {
            }

        }


    }
}
