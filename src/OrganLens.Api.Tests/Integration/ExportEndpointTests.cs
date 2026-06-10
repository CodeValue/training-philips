using System.Net;
using Xunit;

namespace OrganLens.Api.Tests.Integration;

public class ExportEndpointTests : IClassFixture<ApiFactory>
{
    private readonly HttpClient _client;

    public ExportEndpointTests(ApiFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Export_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/studies/SYN-0002/export");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Export_ReturnsCsv()
    {
        var response = await _client.GetAsync("/api/studies/SYN-0002/export");

        Assert.Equal("text/csv", response.Content.Headers.ContentType.MediaType);
    }

    [Fact]
    public async Task Export_HasHeaderRow()
    {
        var csv = await _client.GetStringAsync("/api/studies/SYN-0002/export");

        Assert.StartsWith("StudyId,", csv);
    }
}
