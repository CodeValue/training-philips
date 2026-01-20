namespace OrganLens.Api.Models;

public class MeasurementDto
{
    public int Id { get; set; }
    public string Organ { get; set; }
    public string Kind { get; set; }
    public decimal Value { get; set; }
    public string Unit { get; set; }
    public decimal SliceThicknessMm { get; set; }

    /// <summary>How many slices the measured length spans.</summary>
    public int SlicesCovered { get; set; }

    public DateTime CreatedAtUtc { get; set; }
}
