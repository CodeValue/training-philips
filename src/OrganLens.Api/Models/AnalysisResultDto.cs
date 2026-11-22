namespace OrganLens.Api.Models;

public class AnalysisResultDto
{
    public string StudyId { get; set; }
    public string Organ { get; set; }
    public List<MeasurementValueDto> Measurements { get; set; } = new();
    public List<FindingDto> Findings { get; set; } = new();
    public DateTime AnalyzedAtUtc { get; set; }

    /// <summary>True when any finding's confidence is below Analysis:ReviewThreshold.</summary>
    public bool RequiresReview { get; set; }
}

public class MeasurementValueDto
{
    public string Name { get; set; }
    public double Value { get; set; }
    public string Unit { get; set; }
}

public class FindingDto
{
    public string Code { get; set; }
    public string Description { get; set; }

    /// <summary>0..1</summary>
    public double Confidence { get; set; }

    /// <summary>Info, Low, Medium, High</summary>
    public string Severity { get; set; }
}
