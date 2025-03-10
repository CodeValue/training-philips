using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using OrganLens.Legacy.Core.Utils;

namespace OrganLens.Legacy.Core
{
    /// <summary>
    /// Central manager for studies. Loads, caches, analyses, reports and exports.
    /// (Yes, it does everything. Please don't add more. - D.L. 2021)
    /// </summary>
    public class StudyManager
    {
        private static StudyManager _instance;
        private static readonly object _lock = new object();

        // global cache, shared by all forms
        private static Dictionary<string, StudyRecord> _cache = new Dictionary<string, StudyRecord>();

        // last analysis results per study, used by export
        private Dictionary<string, Dictionary<string, double>> _lastResults = new Dictionary<string, Dictionary<string, double>>();
        private Dictionary<string, List<string>> _lastFindings = new Dictionary<string, List<string>>();

        public string LastError;

        public static StudyManager Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new StudyManager();
                    }
                    return _instance;
                }
            }
        }

        // ---------------------------------------------------------------- loading

        public void SetStudies(List<StudyRecord> studies)
        {
            _cache.Clear();
            foreach (var s in studies)
            {
                _cache[s.StudyId] = s;
            }
            Log("Loaded " + studies.Count + " studies");
        }

        public List<StudyRecord> GetAllStudies()
        {
            return _cache.Values.OrderBy(s => s.StudyId).ToList();
        }

        public StudyRecord GetStudy(string studyId)
        {
            StudyRecord s;
            if (_cache.TryGetValue(studyId, out s)) return s;
            return null;
        }

        public List<StudyRecord> Search(string text)
        {
            if (string.IsNullOrEmpty(text)) return GetAllStudies();

            text = text.ToLower();
            var result = new List<StudyRecord>();
            foreach (var s in _cache.Values)
            {
                if (s.StudyId.ToLower().Contains(text) || s.Patient.PatientName.ToLower().Contains(text) || s.Modality.ToLower() == text)
                {
                    result.Add(s);
                }
            }
            return result.OrderBy(s => s.StudyId).ToList();
        }

        // ---------------------------------------------------------------- analysis

        /// <summary>
        /// Analyse one organ of a study. Returns a text summary for the UI.
        /// organ: "Liver", "Kidney", "Lung", "Heart", "Spleen"
        /// </summary>
        public string AnalyzeStudy(string studyId, string organ)
        {
            LastError = null;
            var study = GetStudy(studyId);
            if (study == null)
            {
                LastError = "Study not found";
                return null;
            }

            var results = new Dictionary<string, double>();
            var findings = new List<string>();

            try
            {
                if (organ == "Liver")
                {
                    double hu = study.Metrics["MeanHu"];
                    results["MeanDensity(HU)"] = MathHelper.Round1(hu);
                    if (hu < 40) findings.Add("Hepatic steatosis suspected");
                }
                else if (organ == "Kidney")
                {
                    double vol = ComputeKidneyVolume(study.Metrics["LengthMm"], study.Metrics["WidthMm"], study.Metrics["DepthMm"]);
                    results["Volume(ml)"] = vol;
                    if (vol > 200) findings.Add("Kidney enlarged");
                }
                else if (organ == "Lung")
                {
                    int count = 0;
                    for (int i = 1; i <= 10; i++)
                    {
                        if (study.Metrics.ContainsKey("Nodule" + i + "Mm"))
                        {
                            count++;
                            double d = study.Metrics["Nodule" + i + "Mm"];
                            if (d >= 6) findings.Add("Nodule " + d + "mm");
                        }
                    }
                    results["Nodules"] = count;
                }
                else if (organ == "Heart")
                {
                    double ef = ComputeEjectionFraction(study.Metrics["EdvMl"], study.Metrics["EsvMl"]);
                    results["EF(%)"] = ef;
                    if (ef < 50) findings.Add("Low ejection fraction");
                }
                else if (organ == "Spleen")
                {
                    double idx = ComputeSpleenIndex(study.Metrics["LengthMm"], study.Metrics["WidthMm"], study.Metrics["ThicknessMm"]);
                    results["SpleenIndex"] = idx;
                    if (idx > 480) findings.Add("Splenomegaly");
                }
                else
                {
                    LastError = "Unknown organ " + organ;
                    return null;
                }
            }
            catch (KeyNotFoundException)
            {
                LastError = "Missing metric for " + organ;
                return null;
            }

            _lastResults[studyId] = results;
            _lastFindings[studyId] = findings;
            Log("Analysed " + studyId + " " + organ + " for " + study.Patient.PatientName);

            return new ReportBuilder().Build(study, organ, results, findings);
        }

        public double ComputeKidneyVolume(double lengthMm, double widthMm, double depthMm)
        {
            // ellipsoid formula, 0.523 ~ PI/6
            double mm3 = lengthMm * widthMm * depthMm * 0.523;
            return MathHelper.Round1(mm3 / 1000.0);
        }

        public double ComputeEjectionFraction(double edv, double esv)
        {
            if (edv == 0) return 0;
            double ratio = (edv - esv) / edv;
            return MathHelper.Round1(ratio * 100);
        }

        public double ComputeSpleenIndex(double lengthMm, double widthMm, double thicknessMm)
        {
            // classic spleen index = L x W x T in cm
            return MathHelper.Round1((lengthMm / 10.0) * (widthMm / 10.0) * (thicknessMm / 10.0));
        }

        public Dictionary<string, double> GetLastResults(string studyId)
        {
            Dictionary<string, double> r;
            if (_lastResults.TryGetValue(studyId, out r)) return r;
            return null;
        }

        // ---------------------------------------------------------------- export

        public bool ExportAnalysisCsv(string studyId, string path)
        {
            try
            {
                var study = GetStudy(studyId);
                var results = GetLastResults(studyId);
                if (study == null || results == null) return false;

                var sb = new StringBuilder();
                sb.AppendLine("StudyId,PatientId,PatientName,Metric,Value");
                foreach (var kv in results)
                {
                    sb.AppendLine(study.StudyId + "," + study.Patient.PatientId + "," + study.Patient.PatientName + "," + kv.Key + "," + kv.Value.ToString(CultureInfo.InvariantCulture));
                }
                File.WriteAllText(path, sb.ToString());
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        // ---------------------------------------------------------------- misc

        public string GetStudyTitle(StudyRecord s)
        {
            return s.StudyId + " - " + s.Patient.PatientName + " - " + s.Modality + " - " + string.Join("/", s.Organs);
        }

        private void Log(string message)
        {
            Trace.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " [StudyManager] " + message);
        }
    }
}
