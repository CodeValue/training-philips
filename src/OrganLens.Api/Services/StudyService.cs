using OrganLens.Api.Data;
using OrganLens.Api.Models;

namespace OrganLens.Api.Services;

public class StudyService
{
    private readonly IStudyStore _store;

    public StudyService(IStudyStore store)
    {
        _store = store;
    }

    public List<StudySummaryDto> GetAll()
    {
        return _store.GetAll().Select(ToSummary).ToList();
    }

    /// <returns>The study, or null when it does not exist.</returns>
    public StudyDetailDto GetById(string id)
    {
        var study = _store.GetById(id);
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
        dto.Modality = study.Modality;
        dto.StudyDate = study.StudyDate.ToString("yyyy-MM-dd");
        dto.Organs = study.Series.Select(s => s.Organ.ToString()).Distinct().ToArray();
        dto.Status = study.Analyses.Count > 0 ? "Analyzed" : "New";
    }
}
