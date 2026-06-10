using System.Text;
using Microsoft.AspNetCore.Mvc;
using OrganLens.Api.Services;

namespace OrganLens.Api.Controllers;

[ApiController]
[Route("api/studies/{id}/export")]
public class ExportController : ControllerBase
{
    private readonly ExportService _exportService;

    public ExportController(ExportService exportService)
    {
        _exportService = exportService;
    }

    /// <summary>Exports the study's analysis measurements as CSV (WI-1063).</summary>
    [HttpGet]
    public IActionResult Export(string id)
    {
        var csv = _exportService.BuildCsv(id);
        return File(Encoding.UTF8.GetBytes(csv), "text/csv", $"{id}.csv");
    }
}
