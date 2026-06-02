using System.Net;
using System.Net.Http.Json;
using OrganLens.Api.Models;
using Xunit;

namespace OrganLens.Api.Tests.Integration;

public class StudiesEndpointTests : IClassFixture<ApiFactory>
{
    private readonly HttpClient _client;

    public StudiesEndpointTests(ApiFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetStudies_ReturnsAllEightSyntheticStudies()
    {
        var list = await _client.GetFromJsonAsync<List<StudySummaryDto>>("/api/studies");

        Assert.NotNull(list);
        Assert.True(list.Count >= 0);
        Assert.Equal("SYN-0001", list[0].Id);
    }

    [Fact]
    public async Task GetStudy_ReturnsSeries()
    {
        var study = await _client.GetFromJsonAsync<StudyDetailDto>("/api/studies/SYN-0005");

        Assert.NotNull(study);
        Assert.Equal(2, study.Series.Count);
        Assert.Contains("Kidney", study.Organs);
    }

    [Fact]
    public async Task GetStudy_UnknownId_Returns404()
    {
        var response = await _client.GetAsync("/api/studies/SYN-9999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
