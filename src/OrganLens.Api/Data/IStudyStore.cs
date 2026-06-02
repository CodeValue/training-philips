using OrganLens.Analysis;
using OrganLens.Api.Models;

namespace OrganLens.Api.Data;

public interface IStudyStore
{
    IReadOnlyList<StudyEntity> FetchAllStudies();

    /// <returns>The study, or null when it does not exist.</returns>
    StudyEntity FetchStudyById(string id);

    MeasurementDto AddMeasurement(string studyId, MeasurementDto measurement);

    void AddAnalysis(string studyId, AnalysisResult result);
}
