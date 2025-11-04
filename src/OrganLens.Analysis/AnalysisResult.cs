using System;
using System.Collections.Generic;

namespace OrganLens.Analysis
{
    public class AnalysisResult
    {
        public AnalysisResult()
        {
            Measurements = new List<Measurement>();
            Findings = new List<Finding>();
            AnalyzedAtUtc = DateTime.UtcNow;
        }

        public Organ Organ { get; set; }
        public List<Measurement> Measurements { get; set; }
        public List<Finding> Findings { get; set; }
        public DateTime AnalyzedAtUtc { get; set; }
    }
}
