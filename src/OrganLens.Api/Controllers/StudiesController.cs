using Microsoft.AspNetCore.Mvc;
using OrganLens.Api.Models;
using OrganLens.Api.Services;

namespace OrganLens.Api.Controllers;

[ApiController]
[Route("api/studies")]
public class StudiesController : ControllerBase
{
    private readonly StudyService _studyService;

    public StudiesController(StudyService studyService)
    {
        _studyService = studyService;
    }

    [HttpGet]
    public ActionResult<List<StudySummaryDto>> GetAll()
    {
        return Ok(_studyService.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<StudyDetailDto> GetById(string id)
    {
        var study = _studyService.GetById(id);
        if (study == null)
        {
            return Problem(statusCode: StatusCodes.Status404NotFound, title: "Study not found", detail: $"No study with id '{id}'.");
        }

        return Ok(study);
    }
}
