using OrganLens.Analysis.Calculators;

namespace OrganLens.Analysis.Analyzers
{
    public class KidneyAnalyzer : IOrganAnalyzer
    {
        public const double EnlargedThresholdMl = 200.0;

        public Organ Organ
        {
            get { return Organ.Kidney; }
        }

        public AnalysisResult Analyze(ImageSeries series)
        {
            var result = new AnalysisResult { Organ = Organ.Kidney };

            var length = AnalyzerHelpers.RequireMetadata(series, "LengthMm");
            var width = AnalyzerHelpers.RequireMetadata(series, "WidthMm");
            var depth = AnalyzerHelpers.RequireMetadata(series, "DepthMm");

            var volume = KidneyVolumeCalculator.CalculateMl(length, width, depth);
            result.Measurements.Add(new Measurement("Volume", volume, "mL"));

            if (volume > EnlargedThresholdMl)
            {
                result.Findings.Add(new Finding
                {
                    Organ = Organ.Kidney,
                    Code = "KID-ENLARGED",
                    Description = "Kidney volume above expected range.",
                    Confidence = ConfidenceModel.ForSeries(series),
                    Severity = Severity.Medium
                });
            }

            return result;
        }
    }
}
