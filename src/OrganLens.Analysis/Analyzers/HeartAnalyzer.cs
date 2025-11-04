using OrganLens.Analysis.Calculators;

namespace OrganLens.Analysis.Analyzers
{
    public class HeartAnalyzer : IOrganAnalyzer
    {
        public const double LowEjectionFractionPercent = 50.0;

        public Organ Organ
        {
            get { return Organ.Heart; }
        }

        public AnalysisResult Analyze(ImageSeries series)
        {
            var result = new AnalysisResult { Organ = Organ.Heart };

            var edv = AnalyzerHelpers.RequireMetadata(series, "EdvMl");
            var esv = AnalyzerHelpers.RequireMetadata(series, "EsvMl");

            var ef = EjectionFractionCalculator.Calculate(edv, esv);
            result.Measurements.Add(new Measurement("EjectionFraction", ef, "%"));

            if (ef < LowEjectionFractionPercent)
            {
                result.Findings.Add(new Finding
                {
                    Organ = Organ.Heart,
                    Code = "HEART-LOW-EF",
                    Description = "Reduced left ventricular ejection fraction.",
                    Confidence = ConfidenceModel.ForSeries(series),
                    Severity = Severity.High
                });
            }

            return result;
        }
    }
}
