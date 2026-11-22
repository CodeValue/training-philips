using OrganLens.Analysis;
using OrganLens.Api.Data;
using OrganLens.Api.Models;

namespace OrganLens.Api.Services;

public class AnalysisService
{
    private readonly AnalyzerRegistry _registry;
    private readonly IStudyStore _store;
    private readonly ReviewPolicy _reviewPolicy;
    private readonly ILogger<AnalysisService> _logger;

    public AnalysisService(AnalyzerRegistry registry, IStudyStore store, ReviewPolicy reviewPolicy, ILogger<AnalysisService> logger)
    {
        _registry = registry;
        _store = store;
        _reviewPolicy = reviewPolicy;
        _logger = logger;
    }

    public AnalysisResultDto Analyze(StudyEntity study, ImageSeries series)
    {
        _logger.LogInformation("Analyzing {Organ} for patient {PatientName} ({StudyId})", series.Organ, study.PatientName, study.Id);

        var analyzer = _registry.Get(series.Organ);
        var result = analyzer.Analyze(series);
        _store.AddAnalysis(study.Id, result);

        return ToDto(study.Id, result);
    }

    private AnalysisResultDto ToDto(string studyId, AnalysisResult result)
    {
        return new AnalysisResultDto
        {
            StudyId = studyId,
            RequiresReview = _reviewPolicy.RequiresReview(result.Findings),
            Organ = result.Organ.ToString(),
            AnalyzedAtUtc = result.AnalyzedAtUtc,
            Measurements = result.Measurements.Select(m => new MeasurementValueDto
            {
                Name = m.Name,
                Value = m.Value,
                Unit = m.Unit
            }).ToList(),
            Findings = result.Findings.Select(f => new FindingDto
            {
                Code = f.Code,
                Description = f.Description,
                Confidence = f.Confidence,
                Severity = f.Severity.ToString()
            }).ToList()
        };
    }
}
