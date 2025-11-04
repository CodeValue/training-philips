namespace OrganLens.Analysis
{
    public enum Severity
    {
        Info,
        Low,
        Medium,
        High
    }

    public class Finding
    {
        public Organ Organ { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// 0..1. See <see cref="ConfidenceModel"/>.
        /// </summary>
        public double Confidence { get; set; }

        public Severity Severity { get; set; }
    }
}
