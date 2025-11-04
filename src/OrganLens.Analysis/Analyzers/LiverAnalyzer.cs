using OrganLens.Analysis.Calculators;

namespace OrganLens.Analysis.Analyzers
{
    public class LiverAnalyzer : IOrganAnalyzer
    {
        public const double SteatosisThresholdHu = 40.0;

        public Organ Organ
        {
            get { return Organ.Liver; }
        }

        public AnalysisResult Analyze(ImageSeries series)
        {
            var result = new AnalysisResult { Organ = Organ.Liver };
            var meanHu = LiverDensityCalculator.CalculateMeanHu(series.Frames);
            result.Measurements.Add(new Measurement("MeanDensity", meanHu, "HU"));

            if (meanHu < SteatosisThresholdHu)
            {
                result.Findings.Add(new Finding
                {
                    Organ = Organ.Liver,
                    Code = "LIV-STEATOSIS",
                    Description = "Low liver density - hepatic steatosis suspected.",
                    Confidence = ConfidenceModel.ForSeries(series),
                    Severity = Severity.Medium
                });
            }

            return result;
        }
    }
}
