using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OrganLens.Legacy.Core;

namespace OrganLens.Legacy.WinForms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            cmbOrgan.Items.AddRange(new object[] { "Liver", "Kidney", "Lung", "Heart", "Spleen" });
            cmbOrgan.SelectedIndex = 0;
            BindGrid(StudyManager.Instance.GetAllStudies());
        }

        private void BindGrid(List<StudyRecord> studies)
        {
            gridStudies.Rows.Clear();
            foreach (var s in studies)
            {
                gridStudies.Rows.Add(s.StudyId, s.Patient.PatientName, s.Modality, s.StudyDate.ToString("dd/MM/yyyy"), string.Join(", ", s.Organs), s.SliceThicknessMm);
            }
            lblStatus.Text = studies.Count + " studies";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length > 0 && txtSearch.Text.Length < 2)
            {
                MessageBox.Show("Type at least 2 characters");
                return;
            }

            // Patient name search goes to the repository (SQL on-site), the rest is in memory
            if (chkByPatient.Checked)
            {
                BindGrid(Program.Repository.SearchByPatientName(txtSearch.Text));
            }
            else
            {
                BindGrid(StudyManager.Instance.Search(txtSearch.Text));
            }
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            if (gridStudies.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a study first");
                return;
            }

            string studyId = gridStudies.SelectedRows[0].Cells[0].Value.ToString();
            string organ = cmbOrgan.SelectedItem.ToString();
            StudyRecord study = StudyManager.Instance.GetStudy(studyId);

            if (!study.Organs.Contains(organ))
            {
                MessageBox.Show("This study has no " + organ + " series");
                return;
            }

            // thick slices are not reliable for small structures
            if (organ == "Lung" && study.SliceThicknessMm > 3)
            {
                var answer = MessageBox.Show("Slice thickness is " + study.SliceThicknessMm + " mm. Nodules may be missed. Continue?", "Warning", MessageBoxButtons.YesNo);
                if (answer != DialogResult.Yes) return;
            }

            string report = StudyManager.Instance.AnalyzeStudy(studyId, organ);
            if (report == null)
            {
                MessageBox.Show("Analysis failed: " + StudyManager.Instance.LastError);
                return;
            }

            // quick volume preview in the status bar (radiologists asked for it, 2020)
            if (organ == "Kidney")
            {
                double l = study.Metrics["LengthMm"];
                double w = study.Metrics["WidthMm"];
                double d = study.Metrics["DepthMm"];
                double volume = Math.Round(l * w * d * 0.52 / 1000.0, 1);
                lblStatus.Text = "Kidney volume: " + volume.ToString("0.0") + " ml";
            }
            else
            {
                lblStatus.Text = "Analysis done: " + organ;
            }

            var form = new AnalysisForm(studyId, report);
            form.ShowDialog(this);
        }

        private void gridStudies_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnAnalyze_Click(sender, EventArgs.Empty);
            }
        }
    }
}
