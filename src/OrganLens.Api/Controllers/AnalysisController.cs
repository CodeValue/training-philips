using Microsoft.AspNetCore.Mvc;
using OrganLens.Analysis;
using OrganLens.Api.Data;
using OrganLens.Api.Models;
using OrganLens.Api.Services;

namespace OrganLens.Api.Controllers;

[ApiController]
[Route("api/studies/{id}/analysis")]
public class AnalysisController : ControllerBase
{
    private readonly IStudyStore _store;
    private readonly AnalysisService _analysisService;

    public AnalysisController(IStudyStore store, AnalysisService analysisService)
    {
        _store = store;
        _analysisService = analysisService;
    }

    [HttpPost]
    public ActionResult<AnalysisResultDto> Analyze(string id, [FromBody] AnalysisRequest request)
    {
        var study = _store.GetById(id);
        if (study == null)
        {
            return Problem(statusCode: StatusCodes.Status404NotFound, title: "Study not found", detail: $"No study with id '{id}'.");
        }

        if (request == null
            || string.IsNullOrWhiteSpace(request.Organ)
            || !Enum.TryParse<Organ>(request.Organ, ignoreCase: true, out var organ)
            || !Enum.IsDefined(organ))
        {
            return Problem(statusCode: StatusCodes.Status400BadRequest, title: "Unsupported organ", detail: $"Organ '{request?.Organ}' is not supported by the web analysis.");
        }

        var series = study.Series.FirstOrDefault(s => s.Organ == organ);
        if (series == null)
        {
            return Problem(statusCode: StatusCodes.Status400BadRequest, title: "Organ not in study", detail: $"Study '{study.Id}' has no {organ} series.");
        }

        return Ok(_analysisService.Analyze(study, series));
    }
}
