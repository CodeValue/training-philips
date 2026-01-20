using System.Net;
using System.Net.Http.Json;
using OrganLens.Api.Models;
using Xunit;

namespace OrganLens.Api.Tests.Integration;

public class AnalysisEndpointTests : IClassFixture<ApiFactory>
{
    private readonly HttpClient _client;

    public AnalysisEndpointTests(ApiFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Analyze_Kidney_ReturnsVolumeInMl()
    {
        var response = await _client.PostAsJsonAsync("/api/studies/SYN-0002/analysis", new { organ = "Kidney" });
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<AnalysisResultDto>();

        var volume = Assert.Single(result.Measurements, m => m.Name == "Volume");
        Assert.Equal("mL", volume.Unit);
        Assert.Equal(129.6, volume.Value, 1);
        Assert.Empty(result.Findings);
    }

    [Fact]
    public async Task Analysis_Heart_ReturnsEjectionFractionInPercent()
    {
        var response = await _client.PostAsJsonAsync("/api/studies/SYN-0003/analysis", new { organ = "Heart" });
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<AnalysisResultDto>();

        var ef = Assert.Single(result.Measurements, m => m.Name == "EjectionFraction");
        Assert.Equal("%", ef.Unit);
        Assert.InRange(ef.Value, 59.9, 60.1);
    }

    [Fact]
    public async Task Analyze_LowEf_ReportsFinding()
    {
        var response = await _client.PostAsJsonAsync("/api/studies/SYN-0006/analysis", new { organ = "Heart" });
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<AnalysisResultDto>();

        var finding = Assert.Single(result.Findings);
        Assert.Equal("HEART-LOW-EF", finding.Code);
        Assert.Equal(0.60, finding.Confidence, 2);
    }

    [Fact]
    public async Task Analyze_UnknownOrgan_Returns400()
    {
        var response = await _client.PostAsJsonAsync("/api/studies/SYN-0002/analysis", new { organ = "Spleen" });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Analyze_OrganNotInStudy_Returns400()
    {
        var response = await _client.PostAsJsonAsync("/api/studies/SYN-0002/analysis", new { organ = "Heart" });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Analyze_UnknownStudy_Returns404()
    {
        var response = await _client.PostAsJsonAsync("/api/studies/SYN-9999/analysis", new { organ = "Kidney" });

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
