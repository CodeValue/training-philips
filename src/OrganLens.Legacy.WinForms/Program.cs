using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using OrganLens.Legacy.Core;
using OrganLens.Legacy.Data;

namespace OrganLens.Legacy.WinForms
{
    internal static class Program
    {
        public static IStudyRepository Repository;

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (ConfigurationManager.AppSettings["UseSql"] == "true")
            {
                Repository = new SqlStudyRepository(ConfigurationManager.ConnectionStrings["OrganLensDb"].ConnectionString);
            }
            else
            {
                Repository = new XmlStudyRepository(FindDataFile(ConfigurationManager.AppSettings["DataFile"]));
            }

            StudyManager.Instance.SetStudies(Repository.LoadAll());

            Application.Run(new MainForm());
        }

        // Walk up from bin\Debug\net48 until we find the data folder.
        private static string FindDataFile(string relativePath)
        {
            var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            while (dir != null)
            {
                var candidate = Path.Combine(dir.FullName, relativePath);
                if (File.Exists(candidate)) return candidate;
                dir = dir.Parent;
            }

            MessageBox.Show("Data file not found: " + relativePath);
            Environment.Exit(1);
            return null;
        }
    }
}
