using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace OrganLens.Api.Tests.Integration;

/// <summary>
/// WI-1077: integration tests for requiresReview on POST /api/studies/{id}/analysis.
/// Written before the feature exists; tests that read requiresReview fail until it is implemented.
/// </summary>
public class ReviewFlagTests
{
    private static async Task<HttpResponseMessage> Analyze(HttpClient client, string studyId, string organ)
    {
        return await client.PostAsJsonAsync($"/api/studies/{studyId}/analysis", new { organ });
    }

    private static async Task<bool> ReadRequiresReview(HttpResponseMessage response)
    {
        using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        return json.RootElement.GetProperty("requiresReview").GetBoolean();
    }

    [Fact]
    public async Task Syn0005Kidney_LowConfidence_RequiresReview()
    {
        using var factory = new ApiFactory();
        var response = await Analyze(factory.CreateClient(), "SYN-0005", "Kidney"); // slice 5.0 mm, confidence 0.68

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(await ReadRequiresReview(response));
    }

    [Fact]
    public async Task Syn0002Kidney_DoesNotRequireReview()
    {
        using var factory = new ApiFactory();
        var response = await Analyze(factory.CreateClient(), "SYN-0002", "Kidney");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(await ReadRequiresReview(response));
    }

    [Fact]
    public async Task Syn0006Heart_LowEfWithLowConfidence_RequiresReview()
    {
        using var factory = new ApiFactory();
        var response = await Analyze(factory.CreateClient(), "SYN-0006", "Heart"); // confidence 0.60, HEART-LOW-EF

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(await ReadRequiresReview(response));
    }

    [Fact]
    public async Task Syn0003Heart_NoFindings_DoesNotRequireReview()
    {
        using var factory = new ApiFactory();
        var response = await Analyze(factory.CreateClient(), "SYN-0003", "Heart"); // confidence 0.44 but no findings

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Syn0006Heart_ThresholdOverriddenTo050_DoesNotRequireReview()
    {
        using var factory = new ApiFactory();
        var client = factory.WithWebHostBuilder(builder =>
                builder.ConfigureAppConfiguration((_, config) =>
                    config.AddInMemoryCollection(new Dictionary<string, string> { ["Analysis:ReviewThreshold"] = "0.50" })))
            .CreateClient();

        var response = await Analyze(client, "SYN-0006", "Heart");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.False(await ReadRequiresReview(response));
    }
}
