namespace Interface
{
    partial class ConsultWebpage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsultWebpage));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridViewSimpleSoftware = new System.Windows.Forms.DataGridView();
            this.Open = new System.Windows.Forms.DataGridViewImageColumn();
            this.marqueeProgressBar = new System.Windows.Forms.ProgressBar();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSimpleSoftware)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.webBrowser);
            this.splitContainer1.Size = new System.Drawing.Size(642, 492);
            this.splitContainer1.SplitterDistance = 213;
            this.splitContainer1.TabIndex = 32;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridViewSimpleSoftware, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.marqueeProgressBar, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.47968F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.520325F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(213, 492);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dataGridViewSimpleSoftware
            // 
            this.dataGridViewSimpleSoftware.AllowUserToAddRows = false;
            this.dataGridViewSimpleSoftware.AllowUserToDeleteRows = false;
            this.dataGridViewSimpleSoftware.AllowUserToOrderColumns = true;
            this.dataGridViewSimpleSoftware.AllowUserToResizeRows = false;
            this.dataGridViewSimpleSoftware.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewSimpleSoftware.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewSimpleSoftware.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSimpleSoftware.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Open});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Orange;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewSimpleSoftware.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewSimpleSoftware.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewSimpleSoftware.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewSimpleSoftware.Name = "dataGridViewSimpleSoftware";
            this.dataGridViewSimpleSoftware.ReadOnly = true;
            this.dataGridViewSimpleSoftware.RowHeadersVisible = false;
            this.dataGridViewSimpleSoftware.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSimpleSoftware.Size = new System.Drawing.Size(207, 449);
            this.dataGridViewSimpleSoftware.TabIndex = 0;
            this.dataGridViewSimpleSoftware.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSimpleSoftware_CellDoubleClick);
            // 
            // Open
            // 
            this.Open.HeaderText = "";
            this.Open.Image = ((System.Drawing.Image)(resources.GetObject("Open.Image")));
            this.Open.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Open.Name = "Open";
            this.Open.ReadOnly = true;
            this.Open.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Open.Width = 5;
            // 
            // marqueeProgressBar
            // 
            this.marqueeProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.marqueeProgressBar.Location = new System.Drawing.Point(3, 466);
            this.marqueeProgressBar.MarqueeAnimationSpeed = 10;
            this.marqueeProgressBar.Name = "marqueeProgressBar";
            this.marqueeProgressBar.Size = new System.Drawing.Size(207, 23);
            this.marqueeProgressBar.TabIndex = 1;
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(425, 492);
            this.webBrowser.TabIndex = 0;
            this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
            // 
            // ConsultWebpage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 492);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConsultWebpage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "beSMART Software | Consult Software Webpage";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSimpleSoftware)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.DataGridView dataGridViewSimpleSoftware;
        private System.Windows.Forms.DataGridViewImageColumn Open;
        private System.Windows.Forms.ProgressBar marqueeProgressBar;
    }
}