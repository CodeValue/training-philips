using System;
using System.Linq;

namespace OrganLens.Analysis.Analyzers
{
    public class LungAnalyzer : IOrganAnalyzer
    {
        public const double ReportableNoduleMm = 6.0;

        public Organ Organ
        {
            get { return Organ.Lung; }
        }

        public AnalysisResult Analyze(ImageSeries series)
        {
            var result = new AnalysisResult { Organ = Organ.Lung };

            // Nodule candidates come from the segmentation step as Nodule1Mm, Nodule2Mm, ...
            var nodules = series.Metadata
                .Where(kv => kv.Key.StartsWith("Nodule", StringComparison.Ordinal) && kv.Key.EndsWith("Mm", StringComparison.Ordinal))
                .OrderBy(kv => kv.Key, StringComparer.Ordinal)
                .Select(kv => kv.Value)
                .ToList();

            result.Measurements.Add(new Measurement("NoduleCount", nodules.Count, "count"));

            var confidence = ConfidenceModel.ForSeries(series);
            foreach (var diameter in nodules.Where(d => d >= ReportableNoduleMm))
            {
                result.Findings.Add(new Finding
                {
                    Organ = Organ.Lung,
                    Code = "LUNG-NODULE",
                    Description = "Pulmonary nodule of " + diameter + " mm.",
                    Confidence = confidence,
                    Severity = diameter >= 8.0 ? Severity.High : Severity.Low
                });
            }

            return result;
        }
    }
}
