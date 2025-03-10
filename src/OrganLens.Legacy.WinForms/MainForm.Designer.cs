namespace OrganLens.Legacy.WinForms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.gridStudies = new System.Windows.Forms.DataGridView();
            this.colStudyId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPatient = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colModality = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrgans = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSlice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.chkByPatient = new System.Windows.Forms.CheckBox();
            this.cmbOrgan = new System.Windows.Forms.ComboBox();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridStudies)).BeginInit();
            this.SuspendLayout();
            //
            // gridStudies
            //
            this.gridStudies.AllowUserToAddRows = false;
            this.gridStudies.AllowUserToDeleteRows = false;
            this.gridStudies.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridStudies.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridStudies.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStudyId,
            this.colPatient,
            this.colModality,
            this.colDate,
            this.colOrgans,
            this.colSlice});
            this.gridStudies.Location = new System.Drawing.Point(12, 41);
            this.gridStudies.MultiSelect = false;
            this.gridStudies.Name = "gridStudies";
            this.gridStudies.ReadOnly = true;
            this.gridStudies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridStudies.Size = new System.Drawing.Size(760, 360);
            this.gridStudies.TabIndex = 3;
            this.gridStudies.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridStudies_CellDoubleClick);
            //
            // colStudyId
            //
            this.colStudyId.HeaderText = "Study";
            this.colStudyId.Name = "colStudyId";
            this.colStudyId.ReadOnly = true;
            //
            // colPatient
            //
            this.colPatient.HeaderText = "Patient";
            this.colPatient.Name = "colPatient";
            this.colPatient.ReadOnly = true;
            //
            // colModality
            //
            this.colModality.HeaderText = "Modality";
            this.colModality.Name = "colModality";
            this.colModality.ReadOnly = true;
            //
            // colDate
            //
            this.colDate.HeaderText = "Date";
            this.colDate.Name = "colDate";
            this.colDate.ReadOnly = true;
            //
            // colOrgans
            //
            this.colOrgans.HeaderText = "Organs";
            this.colOrgans.Name = "colOrgans";
            this.colOrgans.ReadOnly = true;
            //
            // colSlice
            //
            this.colSlice.HeaderText = "Slice (mm)";
            this.colSlice.Name = "colSlice";
            this.colSlice.ReadOnly = true;
            //
            // txtSearch
            //
            this.txtSearch.Location = new System.Drawing.Point(12, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(220, 20);
            this.txtSearch.TabIndex = 0;
            //
            // btnSearch
            //
            this.btnSearch.Location = new System.Drawing.Point(238, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            //
            // chkByPatient
            //
            this.chkByPatient.AutoSize = true;
            this.chkByPatient.Location = new System.Drawing.Point(320, 14);
            this.chkByPatient.Name = "chkByPatient";
            this.chkByPatient.Size = new System.Drawing.Size(110, 17);
            this.chkByPatient.TabIndex = 2;
            this.chkByPatient.Text = "By patient name";
            this.chkByPatient.UseVisualStyleBackColor = true;
            //
            // cmbOrgan
            //
            this.cmbOrgan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbOrgan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrgan.Location = new System.Drawing.Point(12, 412);
            this.cmbOrgan.Name = "cmbOrgan";
            this.cmbOrgan.Size = new System.Drawing.Size(140, 21);
            this.cmbOrgan.TabIndex = 4;
            //
            // btnAnalyze
            //
            this.btnAnalyze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAnalyze.Location = new System.Drawing.Point(158, 410);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(90, 23);
            this.btnAnalyze.TabIndex = 5;
            this.btnAnalyze.Text = "Analyze";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            //
            // lblStatus
            //
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Location = new System.Drawing.Point(260, 415);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(512, 18);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Ready";
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 445);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnAnalyze);
            this.Controls.Add(this.cmbOrgan);
            this.Controls.Add(this.chkByPatient);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.gridStudies);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OrganLens 3.7 (synthetic data)";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridStudies)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView gridStudies;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStudyId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPatient;
        private System.Windows.Forms.DataGridViewTextBoxColumn colModality;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrgans;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSlice;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.CheckBox chkByPatient;
        private System.Windows.Forms.ComboBox cmbOrgan;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.Label lblStatus;
    }
}
