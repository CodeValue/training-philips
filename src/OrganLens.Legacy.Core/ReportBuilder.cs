using System;
using System.Collections.Generic;
using System.Text;

namespace OrganLens.Legacy.Core
{
    public class ReportBuilder
    {
        public string Build(StudyRecord study, string organ, Dictionary<string, double> results, List<string> findings)
        {
            var sb = new StringBuilder();
            sb.AppendLine("ORGANLENS ANALYSIS REPORT");
            sb.AppendLine("=========================");
            sb.AppendLine("Patient : " + study.Patient.PatientName + "   ID: " + study.Patient.PatientId);
            sb.AppendLine("Study   : " + study.StudyId + "   " + study.Modality + "   " + study.StudyDate.ToString("dd/MM/yyyy"));
            sb.AppendLine("Organ   : " + organ);
            sb.AppendLine("Slice   : " + study.SliceThicknessMm + " mm");
            sb.AppendLine();
            sb.AppendLine("Measurements:");
            foreach (var kv in results)
            {
                sb.AppendLine("  " + kv.Key.PadRight(18) + kv.Value.ToString("0.0"));
            }

            sb.AppendLine();
            sb.AppendLine("Findings:");
            if (findings.Count == 0)
            {
                sb.AppendLine("  (none)");
            }
            else
            {
                foreach (var f in findings)
                {
                    sb.AppendLine("  - " + f);
                }
            }

            sb.AppendLine();
            sb.AppendLine("Generated " + DateTime.Now.ToString("g"));
            return sb.ToString();
        }
    }
}
