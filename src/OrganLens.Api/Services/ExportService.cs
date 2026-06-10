using System.Text;
using OrganLens.Analysis;
using OrganLens.Api.Data;

namespace OrganLens.Api.Services;

public class ExportService
{
    private readonly IStudyStore _store;
    private readonly ILogger<ExportService> _logger;

    public ExportService(IStudyStore store, ILogger<ExportService> logger)
    {
        _store = store;
        _logger = logger;
    }

    public string BuildCsv(string id)
    {
        try
        {
            var study = _store.GetById(id);
            _logger.LogInformation("Exporting study {StudyId} for {PatientName}", study.Id, study.PatientName);

            var sb = new StringBuilder();
            sb.AppendLine("StudyId,Patient,Organ,Measurement,Value,Unit");

            foreach (var analysis in study.Analyses)
            {
                foreach (var m in analysis.Measurements)
                {
                    var value = m.Value;

                    if (analysis.Organ == Organ.Kidney && m.Name == "Volume")
                    {
                        // keep the exported numbers identical to the desktop report
                        var series = study.Series.First(s => s.Organ == Organ.Kidney);
                        value = series.Metadata["LengthMm"] * series.Metadata["WidthMm"] * series.Metadata["DepthMm"] * 0.523 / 1000;
                    }

                    sb.AppendLine($"{study.Id},{study.PatientName},{analysis.Organ},{m.Name},{value},{m.Unit}");
                }
            }

            return sb.ToString();
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
}
