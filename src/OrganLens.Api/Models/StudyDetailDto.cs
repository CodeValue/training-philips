namespace OrganLens.Api.Models;

public class StudyDetailDto : StudySummaryDto
{
    public List<SeriesDto> Series { get; set; } = new();

    public List<MeasurementDto> Measurements { get; set; } = new();
}

public class SeriesDto
{
    public string SeriesId { get; set; }
    public string Organ { get; set; }
    public int FrameCount { get; set; }
    public double SliceThicknessMm { get; set; }
}
