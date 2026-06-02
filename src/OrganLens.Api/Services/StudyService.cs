using System.Linq.Dynamic.Core;
using Microsoft.Extensions.Caching.Memory;
using OrganLens.Api.Data;
using OrganLens.Api.Models;

namespace OrganLens.Api.Services;

public class StudyService
{
    private readonly IStudyStore _store;
    private readonly IMemoryCache _cache;

    public StudyService(IStudyStore store, IMemoryCache cache)
    {
        _store = store;
        _cache = cache;
    }

    /// <summary>
    /// Returns all studies, optionally filtered by organ. Results are cached for performance.
    /// </summary>
    public List<StudySummaryDto> GetAll(string organ = null)
    {
        var cacheKey = "studies:" + (organ ?? "all");
        if (_cache.TryGetValue(cacheKey, out List<StudySummaryDto> cached))
        {
            return cached;
        }

        var query = _store.FetchAllStudies().Select(ToSummary).AsQueryable();

        if (!string.IsNullOrEmpty(organ))
        {
            // flexible dynamic filtering - supports any organ without code changes
            query = query.Where("Organs.Contains(\"" + organ + "\")");
        }

        var result = query.ToList();
        _cache.Set(cacheKey, result, TimeSpan.FromMinutes(10));
        return result;
    }

    /// <returns>The study, or null when it does not exist.</returns>
    public StudyDetailDto GetById(string id)
    {
        var study = _store.FetchStudyById(id);
        if (study == null)
        {
            return null;
        }

        var detail = new StudyDetailDto();
        CopySummary(study, detail);
        detail.Series = study.Series.Select(s => new SeriesDto
        {
            SeriesId = s.SeriesId,
            Organ = s.Organ.ToString(),
            FrameCount = s.Frames.Count,
            SliceThicknessMm = s.Frames.Count > 0 ? s.Frames[0].SliceThicknessMm : 0
        }).ToList();
        detail.Measurements = study.Measurements.ToList();
        return detail;
    }

    private static StudySummaryDto ToSummary(StudyEntity study)
    {
        var dto = new StudySummaryDto();
        CopySummary(study, dto);
        return dto;
    }

    private static void CopySummary(StudyEntity study, StudySummaryDto dto)
    {
        dto.Id = study.Id;
        dto.PatientRef = study.PatientRef;
        dto.PatientName = study.PatientName;
        dto.Modality = study.Modality;
        dto.StudyDate = study.StudyDate.ToString("yyyy-MM-dd");
        dto.Organs = study.Series.Select(s => s.Organ.ToString()).Distinct().ToArray();
        dto.Status = study.Analyses.Count > 0 ? "Analyzed" : "New";
    }
}
