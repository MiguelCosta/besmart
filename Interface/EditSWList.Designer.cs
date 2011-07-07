namespace Interface
{
    partial class EditSWList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditSWList));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBoxCharactList = new System.Windows.Forms.GroupBox();
            this.dataGridViewTabelaSoftware = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.buttonAddCharacteristics = new System.Windows.Forms.Button();
            this.buttonAddnew = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBoxCharactList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTabelaSoftware)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBoxCharactList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(738, 568);
            this.splitContainer1.SplitterDistance = 497;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBoxCharactList
            // 
            this.groupBoxCharactList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxCharactList.Controls.Add(this.dataGridViewTabelaSoftware);
            this.groupBoxCharactList.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxCharactList.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.groupBoxCharactList.Location = new System.Drawing.Point(12, 27);
            this.groupBoxCharactList.Name = "groupBoxCharactList";
            this.groupBoxCharactList.Size = new System.Drawing.Size(714, 454);
            this.groupBoxCharactList.TabIndex = 43;
            this.groupBoxCharactList.TabStop = false;
            this.groupBoxCharactList.Text = "Softwares List";
            // 
            // dataGridViewTabelaSoftware
            // 
            this.dataGridViewTabelaSoftware.BackgroundColor = System.Drawing.SystemColors.WindowFrame;
            this.dataGridViewTabelaSoftware.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTabelaSoftware.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dataGridViewTabelaSoftware.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewTabelaSoftware.Location = new System.Drawing.Point(3, 20);
            this.dataGridViewTabelaSoftware.Name = "dataGridViewTabelaSoftware";
            this.dataGridViewTabelaSoftware.Size = new System.Drawing.Size(708, 431);
            this.dataGridViewTabelaSoftware.TabIndex = 0;
            this.dataGridViewTabelaSoftware.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTabelaSoftware_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.Width = 40;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.buttonAddnew);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.buttonAddCharacteristics);
            this.splitContainer2.Size = new System.Drawing.Size(738, 67);
            this.splitContainer2.SplitterDistance = 363;
            this.splitContainer2.TabIndex = 2;
            // 
            // buttonAddCharacteristics
            // 
            this.buttonAddCharacteristics.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonAddCharacteristics.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddCharacteristics.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAddCharacteristics.Location = new System.Drawing.Point(90, 21);
            this.buttonAddCharacteristics.Name = "buttonAddCharacteristics";
            this.buttonAddCharacteristics.Size = new System.Drawing.Size(159, 25);
            this.buttonAddCharacteristics.TabIndex = 1;
            this.buttonAddCharacteristics.Text = "Add new Characteristics";
            this.buttonAddCharacteristics.UseVisualStyleBackColor = true;
            this.buttonAddCharacteristics.Click += new System.EventHandler(this.buttonAddCharacteristics_Click);
            // 
            // buttonAddnew
            // 
            this.buttonAddnew.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonAddnew.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddnew.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAddnew.Location = new System.Drawing.Point(114, 21);
            this.buttonAddnew.Name = "buttonAddnew";
            this.buttonAddnew.Size = new System.Drawing.Size(142, 25);
            this.buttonAddnew.TabIndex = 0;
            this.buttonAddnew.Text = "Add new Sotfware";
            this.buttonAddnew.UseVisualStyleBackColor = true;
            // 
            // EditSWList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 568);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditSWList";
            this.Text = "Edit Software List";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBoxCharactList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTabelaSoftware)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBoxCharactList;
        private System.Windows.Forms.DataGridView dataGridViewTabelaSoftware;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button buttonAddnew;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.Button buttonAddCharacteristics;
    }
}