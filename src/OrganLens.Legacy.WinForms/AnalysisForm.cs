using System;
using System.Windows.Forms;
using OrganLens.Legacy.Core;

namespace OrganLens.Legacy.WinForms
{
    public partial class AnalysisForm : Form
    {
        private readonly string _studyId;

        public AnalysisForm(string studyId, string reportText)
        {
            InitializeComponent();
            _studyId = studyId;
            txtResults.Text = reportText.Replace("\n", Environment.NewLine).Replace("\r\r", "\r");
            Text = "Analysis - " + studyId;
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog())
            {
                dlg.Filter = "CSV files (*.csv)|*.csv";
                dlg.FileName = _studyId + ".csv";
                if (dlg.ShowDialog(this) != DialogResult.OK) return;

                if (StudyManager.Instance.ExportAnalysisCsv(_studyId, dlg.FileName))
                {
                    MessageBox.Show("Exported.");
                }
                else
                {
                    MessageBox.Show("Export failed: " + StudyManager.Instance.LastError);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
