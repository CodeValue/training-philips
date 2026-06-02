using OrganLens.Analysis;
using OrganLens.Api.Models;

namespace OrganLens.Api.Data;

public class InMemoryStudyStore : IStudyStore
{
    private readonly object _lock = new();
    private readonly List<StudyEntity> _studies;
    private int _nextMeasurementId = 1;

    public InMemoryStudyStore(IEnumerable<StudyEntity> seed)
    {
        _studies = seed.ToList();
    }

    public IReadOnlyList<StudyEntity> FetchAllStudies()
    {
        lock (_lock)
        {
            return _studies.OrderBy(s => s.Id).ToList();
        }
    }

    public StudyEntity FetchStudyById(string id)
    {
        lock (_lock)
        {
            return _studies.FirstOrDefault(s => string.Equals(s.Id, id, StringComparison.OrdinalIgnoreCase));
        }
    }

    public MeasurementDto AddMeasurement(string studyId, MeasurementDto measurement)
    {
        lock (_lock)
        {
            var study = _studies.First(s => s.Id == studyId);
            measurement.Id = _nextMeasurementId++;
            study.Measurements.Add(measurement);
            return measurement;
        }
    }

    public void AddAnalysis(string studyId, AnalysisResult result)
    {
        lock (_lock)
        {
            var study = _studies.First(s => s.Id == studyId);
            study.Analyses.Add(result);
        }
    }
}
