using System.Net;
using System.Net.Http.Json;
using OrganLens.Api.Models;
using Xunit;

namespace OrganLens.Api.Tests.Integration;

/// <summary>
/// WI-1077 backend slice: requiresReview on POST /api/studies/{id}/analysis.
/// </summary>
public class RequiresReviewTests : IClassFixture<ApiFactory>
{
    private readonly HttpClient _client;

    public RequiresReviewTests(ApiFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Analyze_Syn0005Kidney_RequiresReview()
    {
        var response = await _client.PostAsJsonAsync("/api/studies/SYN-0005/analysis", new { organ = "Kidney" });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<AnalysisResultDto>();
        Assert.True(result.RequiresReview);
    }

    [Fact]
    public async Task Analyze_Syn0002Kidney_DoesNotRequireReview()
    {
        var response = await _client.PostAsJsonAsync("/api/studies/SYN-0002/analysis", new { organ = "Kidney" });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<AnalysisResultDto>();
        Assert.False(result.RequiresReview);
    }
}
