using OrganLens.Analysis;
using OrganLens.Api.Models;

namespace OrganLens.Api.Data;

public class StudyEntity
{
    public string Id { get; set; }

    /// <summary>Pseudonymous reference, safe to show.</summary>
    public string PatientRef { get; set; }

    /// <summary>Synthetic here, but treat as PHI: never return it from the API, never log it.</summary>
    public string PatientName { get; set; }

    public string Modality { get; set; }
    public DateTime StudyDate { get; set; }
    public List<ImageSeries> Series { get; set; } = new();
    public List<MeasurementDto> Measurements { get; set; } = new();
    public List<AnalysisResult> Analyses { get; set; } = new();
}
