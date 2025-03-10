using System;
using System.Collections.Generic;
using System.Data;
using OrganLens.Legacy.Core;

namespace OrganLens.Legacy.Data
{
    /// <summary>
    /// SQL Server repository. Used on-site when App.config UseSql=true.
    /// </summary>
    public class SqlStudyRepository : IStudyRepository
    {
        private readonly string _connectionString;

        public SqlStudyRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<StudyRecord> LoadAll()
        {
            var table = DbHelper.ExecuteQuery(_connectionString,
                "SELECT StudyId, PatientId, PatientName, BirthYear, StudyDate, Modality, SliceThicknessMm FROM Studies");
            return Map(table);
        }

        public List<StudyRecord> SearchByPatientName(string name)
        {
            var sql = "SELECT StudyId, PatientId, PatientName, BirthYear, StudyDate, Modality, SliceThicknessMm " +
                      "FROM Studies WHERE PatientName LIKE '%" + name + "%'";
            var table = DbHelper.ExecuteQuery(_connectionString, sql);
            return Map(table);
        }

        private static List<StudyRecord> Map(DataTable table)
        {
            var list = new List<StudyRecord>();
            if (table == null) return list;

            foreach (DataRow row in table.Rows)
            {
                var s = new StudyRecord();
                s.StudyId = Convert.ToString(row["StudyId"]);
                s.Patient.PatientId = Convert.ToString(row["PatientId"]);
                s.Patient.PatientName = Convert.ToString(row["PatientName"]);
                s.Patient.BirthYear = Convert.ToInt32(row["BirthYear"]);
                s.StudyDate = Convert.ToDateTime(row["StudyDate"]);
                s.Modality = Convert.ToString(row["Modality"]);
                s.SliceThicknessMm = Convert.ToDouble(row["SliceThicknessMm"]);
                // TODO: load organs + metrics from StudyMetrics table
                list.Add(s);
            }

            return list;
        }
    }
}
