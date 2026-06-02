using Microsoft.AspNetCore.Mvc;
using OrganLens.Api.Data;
using OrganLens.Api.Models;
using OrganLens.Api.Services;

namespace OrganLens.Api.Controllers;

[ApiController]
[Route("api/studies/{id}/measurements")]
public class MeasurementsController : ControllerBase
{
    private readonly IStudyStore _store;
    private readonly MeasurementService _measurementService;

    public MeasurementsController(IStudyStore store, MeasurementService measurementService)
    {
        _store = store;
        _measurementService = measurementService;
    }

    [HttpGet]
    public ActionResult<IReadOnlyList<MeasurementDto>> Get(string id)
    {
        var study = _store.FetchStudyById(id);
        if (study == null)
        {
            return Problem(statusCode: StatusCodes.Status404NotFound, title: "Study not found", detail: $"No study with id '{id}'.");
        }

        return Ok(_measurementService.GetForStudy(study));
    }

    [HttpPost]
    public ActionResult<MeasurementDto> Add(string id, [FromBody] MeasurementRequest request)
    {
        var study = _store.FetchStudyById(id);
        if (study == null)
        {
            return Problem(statusCode: StatusCodes.Status404NotFound, title: "Study not found", detail: $"No study with id '{id}'.");
        }

        var created = _measurementService.Add(study, request);
        return CreatedAtAction(nameof(Get), new { id = study.Id }, created);
    }
}
