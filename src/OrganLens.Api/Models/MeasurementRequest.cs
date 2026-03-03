namespace OrganLens.Api.Models;

/// <summary>
/// A measurement entered manually by a radiologist.
/// </summary>
public class MeasurementRequest
{
    public string Organ { get; set; }
    public string Kind { get; set; }
    public decimal Value { get; set; }
    public string Unit { get; set; }
    public decimal SliceThicknessMm { get; set; }
}
