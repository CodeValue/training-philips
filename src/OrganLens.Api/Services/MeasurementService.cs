using OrganLens.Analysis;
using OrganLens.Api.Data;
using OrganLens.Api.Models;

namespace OrganLens.Api.Services;

public class MeasurementService
{
    private readonly IStudyStore _store;

    public MeasurementService(IStudyStore store)
    {
        _store = store;
    }

    public IReadOnlyList<MeasurementDto> GetForStudy(StudyEntity study)
    {
        return study.Measurements.ToList();
    }

    public MeasurementDto Add(StudyEntity study, MeasurementRequest request)
    {
        var organ = Enum.Parse<Organ>(request.Organ);

        var measurement = new MeasurementDto
        {
            Organ = organ.ToString(),
            Kind = request.Kind,
            Value = request.Value,
            Unit = request.Unit,
            SliceThicknessMm = request.SliceThicknessMm,
            SlicesCovered = (int)(request.Value / request.SliceThicknessMm),
            CreatedAtUtc = DateTime.UtcNow
        };

        return _store.AddMeasurement(study.Id, measurement);
    }
}
