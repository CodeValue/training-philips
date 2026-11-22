namespace OrganLens.Api.Services;

/// <summary>
/// Bound from the "Analysis" section of appsettings.json.
/// </summary>
public class AnalysisOptions
{
    public const string SectionName = "Analysis";

    /// <summary>
    /// Findings with confidence below this value need a radiologist's review. Default 0.70.
    /// </summary>
    public double ReviewThreshold { get; set; } = 0.70;
}
